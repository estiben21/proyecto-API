using System.ComponentModel;

namespace Proyecto.Presentacion.Models
{
    public class Producto
    {
        [DisplayName("CODIGO")]
        public int id_producto { get; set; }
        [DisplayName("PRODUCTO")]
        public string nombre { get; set; }
        [DisplayName("DESCRIPCIÓN")]
        public string descripcion { get; set; }
        [DisplayName("STOCK")]
        public int stock { get; set; }
        [DisplayName("COLOR")]
        public string color { get; set; }
        [DisplayName("TALLA")]
        public string talla { get; set; }
        [DisplayName("PRECIO")]
        public double precio { get; set; }
        [DisplayName("CATEGORIA")]
        public string nom_cate { get; set; }
        [DisplayName("IMAGEN")]
        public string imagen { get; set; }
        
    }
}
