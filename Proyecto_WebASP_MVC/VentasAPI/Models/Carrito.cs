namespace VentasAPI.Models
{
    public class Carrito
    {
        public int id_producto { get; set; }
        public string descripcion { get; set; }
        public int stock { get; set; }
        public string color { get; set; }
        public string talla { get; set; }
        public double precio { get; set; }
    }
}
