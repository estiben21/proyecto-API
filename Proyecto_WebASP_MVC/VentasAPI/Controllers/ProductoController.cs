using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentasAPI.Models;
using VentasAPI.Repositorio.DAO;

namespace VentasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet("listadoProductos")]
        public async Task<ActionResult<List<Producto>>> listadoProducto()
        {
            var lista = await Task.Run(() => new ProductoDAO().listadoProducto());
            return Ok(lista);
        }

        [HttpGet("listadoProductosO")]
        public async Task<ActionResult<List<ProductoO>>> listadoProductosO()
        {
            var lista = await Task.Run(() => new ProductoDAO().listadoProductoO());
            return Ok(lista);
        }
        [HttpGet("listadoCategorias")]
        public async Task<ActionResult<List<Categoria>>> listadoCategorias()
        {
            var lista = await Task.Run(() =>
            new CategoriaDAO().listadoCategoria());
            return Ok(lista);
        }
        [HttpPost("nuevoProducto")]
        public async Task<ActionResult<string>> nuevoProducto(ProductoO objV)
        {
            var mensaje = await Task.Run(() =>
                          new ProductoDAO().nuevoProducto(objV));
            return Ok(mensaje);
        }
        [HttpPut("modificaProducto")]
        public async Task<ActionResult<string>> modificaProducto(ProductoO objV)
        {
            var mensaje = await Task.Run(() =>
                          new ProductoDAO().modificaProducto(objV));
            return Ok(mensaje);
        }
        [HttpGet("buscarProducto/{id}")]
        public async Task<ActionResult<List<ProductoO>>> buscarProducto(int id)
        {
            var lista = await Task.Run(() => new ProductoDAO().buscarProducto(id));
            return Ok(lista);
        }
    }

}
