using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using appUsuario.Models;
using System.Text.RegularExpressions;

namespace appUsuario.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly string connectionString;

        public UsuariosController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("cn"); // Obtener la cadena de conexión desde IConfiguration
        }

        // GET: /Usuario/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Usuario/Login
        [HttpPost]
        public ActionResult Login(string correo, string clave)
        {
            // Ejemplo básico de autenticación con validaciones de formato de correo
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(clave))
            {
                // Si el correo o la clave son nulos o vacíos, mostrar mensaje de error
                ModelState.AddModelError(string.Empty, "Por favor, ingresa correo y contraseña.");
                return View();
            }
            else if (!IsValidEmail(correo))
            {
                // Si el formato del correo no es válido, mostrar mensaje de error
                ModelState.AddModelError(string.Empty, "El formato del correo electrónico no es válido.");
                return View();
            }
            else if (correo == "estiben@gmail.com" && clave == "123")
            {
                // Usuario autenticado, redirigir a la página principal
                return RedirectToAction("Index", "Home");
            }
            else if (correo != "estiben@gmail.com" && clave != "123")
            {
                // Usuario autenticado, redirigir a la página principal
                return RedirectToAction("", "Producto", new { area = "", project = "Proyecto.Presentacion" });
            }            
            else
            {
                // Autenticación fallida, mostrar mensaje de error
                ModelState.AddModelError(string.Empty, "El correo o la contraseña son incorrectos.");
                return View();
            }
        }

        // Función para validar el formato del correo electrónico
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Verificar que el correo contenga @ y termine con .com o .pe
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.(com|pe)$", RegexOptions.IgnoreCase);
        }

        // GET: /Usuario/Registro
        public ActionResult Registro()
        {
            return View();
        }

        // POST: /Usuario/Registro
        [HttpPost]
        public ActionResult Registro(Usuarios ObjU)
        {
            if (ModelState.IsValid)
            {
                // Guardar el usuario en la base de datos
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("RegistrarUsuario", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", ObjU.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", ObjU.Apellido);
                    cmd.Parameters.AddWithValue("@Correo", ObjU.Correo);
                    cmd.Parameters.AddWithValue("@Contrasena", ObjU.clave); // Usar modelo.Contrasena en lugar de modelo.clave
                    cmd.ExecuteNonQuery();
                }
                TempData["SuccessMessage"] = "Usuario registrado con éxito.";
            }

            return View(ObjU);
        }
    }
}
