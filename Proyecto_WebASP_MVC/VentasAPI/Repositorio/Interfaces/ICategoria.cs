using VentasAPI.Models;

namespace VentasAPI.Repositorio.Interfaces
{
    public interface ICategoria
    {
        //Llenar el combo en la presentacion
        IEnumerable<Categoria> listadoCategoria();
    }
}
