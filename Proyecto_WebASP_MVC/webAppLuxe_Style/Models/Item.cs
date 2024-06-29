using System.ComponentModel;


namespace webAppLuxe_Style.Models
{
    public class Item
    {
        [DisplayName("CODIGO")]
        public int id_producto { get; set; }
        [DisplayName("DESCRIPCION")]
        public string? descripcion { get; set; }
        [DisplayName("TALLA")]
        public string? talla { get; set; }
        [DisplayName("PRECIO")]
        public double precio { get; set; }
        [DisplayName("CANTIDAD")]
        public int cantidad { get; set; }
        [DisplayName("SUBTOTAL")]
        public double subtotal
        {
            get { return precio * cantidad; }
        }
    }
}
