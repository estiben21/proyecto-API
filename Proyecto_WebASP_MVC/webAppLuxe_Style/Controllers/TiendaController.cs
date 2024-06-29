using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using webAppLuxe_Style.Models;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace webAppLuxe_Style.Controllers
{
    public class TiendaController : Controller
    {

        public readonly IConfiguration Configuration;


        public readonly IMemoryCache _cache;
        public readonly string _connectionString;

        public TiendaController(IConfiguration IConfig, IMemoryCache memoryCache, IConfiguration configuration)
        {
            Configuration = IConfig;
            _cache = memoryCache;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public IEnumerable<Articulo> aArticulos()
        {
            List<Articulo> aArticulo = new List<Articulo>();
            SqlConnection cn = new SqlConnection(Configuration["ConnectionStrings:cn"]);
            SqlCommand cmd = new SqlCommand("SP_LISTAARTICULOS", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Articulo objA = new Articulo()
                {
                    id_producto = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    descripcion = dr[2].ToString(),
                    stock = int.Parse(dr[3].ToString()),
                    color = dr[4].ToString(),
                    talla = dr[5].ToString(),
                    precio = Double.Parse(dr[6].ToString()),
                    imagen = dr[7].ToString(),
                    //des_cat = dr[8].ToString()
                };
                aArticulo.Add(objA);
            }
            dr.Close();
            cn.Close();
            return aArticulo;
        }


        // Método para listar los artículos
        public IActionResult CarritoCompras()
        {
            List<Item> carrito;
            if (!_cache.TryGetValue("carrito", out carrito))
            {
                carrito = new List<Item>();
                _cache.Set("carrito", carrito);
            }

            return View(aArticulos());
        }


        public IActionResult Select(int id)
        {
            Articulo objA = aArticulos().FirstOrDefault(a => a.id_producto == id);
            return View(objA);
        }

        public ActionResult Agregar(int id)
        {
            var art = aArticulos().Where(a => a.id_producto == id).FirstOrDefault();
            Item objI = new Item();
            objI.id_producto = art.id_producto;
            objI.descripcion = art.descripcion;
            objI.talla = art.talla;
            objI.precio = art.precio;
            objI.cantidad = 1;

            List<Item> miCarrito;
            var sessionCarrito = HttpContext.Session.GetString("carrito");

            if (string.IsNullOrEmpty(sessionCarrito))
            {
                miCarrito = new List<Item>();
            }
            else
            {
                miCarrito = JsonConvert.DeserializeObject<List<Item>>(sessionCarrito);
            }

            miCarrito.Add(objI);

            HttpContext.Session.SetString("carrito", JsonConvert.SerializeObject(miCarrito));

            return RedirectToAction("CarritoCompras");
        }


        // Método que calcula el subtotal de toda la compra
        public IActionResult Comprar()
        {
            List<Item> carrito;
            if (!_cache.TryGetValue("carrito", out carrito))
            {
                return RedirectToAction("CarritoCompras");
            }

            ViewBag.Monto = carrito.Sum(p => p.subtotal);
            return View(carrito);
        }


        // Método para eliminar un producto del carrito
        public IActionResult Eliminar(int? id = null)
        {
            if (id == null)
            {
                return RedirectToAction("CarritoCompras");
            }

            List<Item> carrito;
            if (!_cache.TryGetValue("carrito", out carrito))
            {
                return RedirectToAction("CarritoCompras");
            }

            var item = carrito.FirstOrDefault(i => i.id_producto == id);
            if (item != null)
            {
                carrito.Remove(item);
                _cache.Set("carrito", carrito);
            }

            return RedirectToAction("Comprar");
        }


        // Método para realizar el pago
        public IActionResult Pago()
        {
            List<Item> detalle;
            if (!_cache.TryGetValue("carrito", out detalle))
            {
                return RedirectToAction("CarritoCompras");
            }

            double total = detalle.Sum(it => it.subtotal);
            ViewBag.Total = total;

            return View(detalle);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
