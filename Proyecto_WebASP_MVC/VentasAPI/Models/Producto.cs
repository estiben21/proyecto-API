namespace VentasAPI.Models
{
    public class Producto
    {
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int stock { get; set; }
        public string color { get; set; }
        public string talla { get; set; }
        public double precio { get; set; }
        public string nom_cate { get; set; }
        public string imagen { get; set; }
        
    }
}
