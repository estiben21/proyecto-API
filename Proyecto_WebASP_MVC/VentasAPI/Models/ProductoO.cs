namespace VentasAPI.Models
{
    public class ProductoO
    {
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int stock { get; set; }
        public string color { get; set; }
        public string talla { get; set; }
        public double precio { get; set; }
        public int id_categoria { get; set; }
        public string imagen { get; set; }
        public string base64 { get; set; }
        public string extension { get; set; }
    }
}
