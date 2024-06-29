using VentasAPI.Models;

namespace VentasAPI.Repositorio.Interfaces
{
    public interface IProducto
    {
        IEnumerable<Producto> listadoProducto();
        IEnumerable<ProductoO> listadoProductoO();
        ProductoO buscarProducto(int id);
        string nuevoProducto(ProductoO objV);
        string modificaProducto(ProductoO objV);

    }
}
