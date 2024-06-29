using System.ComponentModel.DataAnnotations;

namespace appUsuario.Models
{
    public class Usuarios
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo electrónico no válido debe tener el formato @gmail.com.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria.")]
        [MinLength(3, ErrorMessage = "La clave debe tener al menos 3 caracteres.")]
        public string clave { get; set; }

        [Required(ErrorMessage = "Confirmar clave es obligatorio.")]
        [Compare("clave", ErrorMessage = "La clave y la confirmación de la clave no coinciden.")]
        public string ConfirmarClave { get; set; }
    }
}
