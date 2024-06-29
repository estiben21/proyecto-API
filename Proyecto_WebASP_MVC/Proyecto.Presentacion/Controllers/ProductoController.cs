using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Presentacion.Models;
using Newtonsoft.Json;
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Proyecto.Presentacion.Controllers
{
    public class ProductoController : Controller
    {
        //Definir la cadena de conexion
        private readonly string? cadena;

        //Cadena conexion hacia el servicio
        Uri baseAddress = new Uri("https://localhost:7204/api");
        private readonly HttpClient _httpClient;

        public ProductoController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
            cadena = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json").Build()
                        .GetConnectionString("cn");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult listadoProductos()
        {
            List<Producto> aProductos = new List<Producto>();
            HttpResponseMessage response =
                _httpClient.GetAsync(_httpClient.BaseAddress + "/Producto/listadoProductos").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                aProductos = JsonConvert.DeserializeObject<List<Producto>>(data);
            }
            return View(aProductos);
        }

        public List<Categoria> aCategorias()
        {
            List<Categoria> aCategorias = new List<Categoria>();
            HttpResponseMessage response =
            _httpClient.GetAsync(_httpClient.BaseAddress + "/Producto/listadoCategorias").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            aCategorias = JsonConvert.DeserializeObject<List<Categoria>>(data);
            return aCategorias;
        }

        [HttpGet]
        public IActionResult nuevoProducto()
        {
            ViewBag.categoria = new SelectList(aCategorias(), "id_categoria", "nom_cate");
            return View(new ProductoO());
        }

        [HttpPost]
        public async Task<IActionResult> nuevoProducto(ProductoO objC)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.categoria = new SelectList(aCategorias(), "id_categoria", "nom_cate");
                return View(objC);
            }
            var json = JsonConvert.SerializeObject(objC);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseC = await
            _httpClient.PostAsync("/api/Producto/nuevoProducto", content);
            if (responseC.IsSuccessStatusCode)
            {
                ViewBag.mensaje = "Producto registrado correctamente..!!!";
            }
            ViewBag.categoria = new SelectList(aCategorias(), "id_categoria", "nom_cate");
            return View(objC);
        }

        [HttpPost]
        public IActionResult DescargarPDF()
        {
            List<Producto> aProductos = new List<Producto>();
            HttpResponseMessage response =
                _httpClient.GetAsync(_httpClient.BaseAddress + "/Producto/listadoProductos").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                aProductos = JsonConvert.DeserializeObject<List<Producto>>(data);
            }

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().ShowOnce().Column(column =>
                    {
                        var rutaImagen = Path.Combine("wwwroot/img/banner.jpg");
                        byte[] imageData = System.IO.File.ReadAllBytes(rutaImagen);

                        column.Item().AlignCenter().Height(100).Image(imageData);
                        column.Item().AlignCenter().Text("REPORTE DE PRODUCTOS").Bold().FontSize(20);
                    });


                    page.Content().PaddingVertical(10).Column(col1 =>
                    {
                        col1.Item().LineHorizontal(0.5f);

                        col1.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background("#257272")
                                .Padding(1).AlignCenter().Text("ID").FontColor("#fff").FontSize(10);

                                header.Cell().Background("#257272")
                                .Padding(1).AlignCenter().Text("Producto").FontColor("#fff").FontSize(10);

                                header.Cell().Background("#257272")
                                .Padding(1).AlignCenter().Text("Descripciòn").FontColor("#fff").FontSize(10);

                                header.Cell().Background("#257272")
                                .Padding(1).AlignCenter().Text("Stock").FontColor("#fff").FontSize(10);

                                header.Cell().Background("#257272")
                                .Padding(1).AlignCenter().Text("Color").FontColor("#fff").FontSize(10);

                                header.Cell().Background("#257272")
                                .Padding(1).AlignCenter().Text("Talla").FontColor("#fff").FontSize(10);

                                header.Cell().Background("#257272")
                                .Padding(1).AlignCenter().Text("Precio").FontColor("#fff").FontSize(10);

                                header.Cell().Background("#257272")
                                .Padding(1).AlignCenter().Text("Categoria").FontColor("#fff").FontSize(10);

                                header.Cell().Background("#257272")
                                .Padding(1).AlignCenter().Text("Imagen").FontColor("#fff").FontSize(10);
                            });

                            foreach (var producto in aProductos)
                            {
                                tabla.Cell().Padding(1).AlignCenter().Text(producto.id_producto.ToString()).FontSize(10);
                                tabla.Cell().Padding(1).AlignCenter().Text(producto.nombre).FontSize(10);
                                tabla.Cell().Padding(1).AlignCenter().Text(producto.descripcion).FontSize(10);
                                tabla.Cell().Padding(1).AlignCenter().Text(producto.stock.ToString()).FontSize(10);
                                tabla.Cell().Padding(1).AlignCenter().Text(producto.color).FontSize(10);
                                tabla.Cell().Padding(1).AlignCenter().Text(producto.talla).FontSize(10);
                                tabla.Cell().Padding(1).AlignCenter().Text(producto.precio.ToString("C")).FontSize(10);
                                tabla.Cell().Padding(1).AlignCenter().Text(producto.nom_cate).FontSize(10);
                                var imageUrl = Url.Content(string.Format("~/img/{0}.jpg", producto.id_producto));
                                var rutaImagenProducto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", $"{producto.id_producto}.jpg");

                                // Leer la imagen
                                if (System.IO.File.Exists(rutaImagenProducto))
                                {
                                    byte[] imagenProductoData = System.IO.File.ReadAllBytes(rutaImagenProducto);
                                    tabla.Cell().Padding(1).AlignCenter().Image(imagenProductoData, ImageScaling.FitWidth);
                                }
                                else
                                {
                                    tabla.Cell().Padding(1).AlignCenter().Text("No Image").FontSize(10);
                                }
                            }
                        });
                    });
                });
            });

            var pdfBytes = document.GeneratePdf();

            return File(pdfBytes, "application/pdf", "ReporteProductos.pdf");
        }

    }
}
