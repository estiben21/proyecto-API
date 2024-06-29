using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Numerics;
using VentasAPI.Models;
using VentasAPI.Repositorio.Interfaces;

namespace VentasAPI.Repositorio.DAO
{
    public class ProductoDAO : IProducto
    {
        //Definir la cadena de conexion
        private readonly string? cadena;

        public ProductoDAO()
        {
            cadena = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json").Build()
                        .GetConnectionString("cn");
        }

        public ProductoO buscarProducto(int id)
        {
            return listadoProductoO().FirstOrDefault(v => v.id_producto == id);
        }

        public IEnumerable<Producto> listadoProducto()
        {
            List<Producto> aProductos = new List<Producto>();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTADOPRODUCTOS", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aProductos.Add(new Producto
                {
                    id_producto = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    descripcion = dr[2].ToString(),
                    stock = int.Parse(dr[3].ToString()),
                    color = dr[4].ToString(),
                    talla = dr[5].ToString(),
                    precio = Double.Parse(dr[6].ToString()),
                    nom_cate = dr[7].ToString(),
                    imagen = dr[8].ToString()
                });
            }
            cn.Close();
            return aProductos;
        }

        public IEnumerable<ProductoO> listadoProductoO()
        {
            List<ProductoO> aProductos = new List<ProductoO>();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTADOPRODUCTOS_O", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aProductos.Add(new ProductoO
                {
                    id_producto = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    descripcion = dr[2].ToString(),
                    stock = int.Parse(dr[3].ToString()),
                    color = dr[4].ToString(),
                    talla = dr[5].ToString(),
                    precio = Double.Parse(dr[6].ToString()),
                    id_categoria = int.Parse(dr[7].ToString()),
                    imagen = dr[8].ToString()

                });
            }
            cn.Close();
            return aProductos;
        }

        public string modificaProducto(ProductoO objV)
        {
            string mensaje = "";
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_MERGE_PRODUCTO", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ide", objV.id_categoria);
                cmd.Parameters.AddWithValue("@nom", objV.nombre);
                cmd.Parameters.AddWithValue("@des", objV.descripcion);
                cmd.Parameters.AddWithValue("@stock", objV.stock);
                cmd.Parameters.AddWithValue("@color", objV.color);
                cmd.Parameters.AddWithValue("@talla", objV.talla);
                cmd.Parameters.AddWithValue("@precio", objV.precio);
                cmd.Parameters.AddWithValue("@cate", objV.id_categoria);
                cmd.Parameters.AddWithValue("@image", objV.imagen);
                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + " Producto actualizado correctamente..!!";
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar..!!" + ex.Message;
            }
            cn.Close();
            return mensaje;
        }

        public string nuevoProducto(ProductoO objV)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_MERGE_PRODUCTO", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ide", objV.id_producto); // Asegúrate de que este valor sea correcto
                    cmd.Parameters.AddWithValue("@nom", objV.nombre);
                    cmd.Parameters.AddWithValue("@des", objV.descripcion);
                    cmd.Parameters.AddWithValue("@stock", objV.stock);
                    cmd.Parameters.AddWithValue("@color", objV.color);
                    cmd.Parameters.AddWithValue("@talla", objV.talla);
                    cmd.Parameters.AddWithValue("@precio", objV.precio);
                    cmd.Parameters.AddWithValue("@cate", objV.id_categoria);
                    cmd.Parameters.AddWithValue("@image", objV.imagen);
                    int n = cmd.ExecuteNonQuery();
                    mensaje = n.ToString() + " Producto registrado correctamente..!!";

                    // Guardar la imagen en la carpeta correspondiente si hay datos válidos
                    if (!string.IsNullOrEmpty(objV.imagen) && objV.imagen.Length > 0)
                    {
                        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Proyecto.Presentacion", "wwwroot", "img", "FOTOPRODUCTO");
                        var imagePath = Path.Combine(folderPath, $"{objV.nombre}.JPG");
                        byte[] imageBytes = Convert.FromBase64String(objV.imagen);
                        System.IO.File.WriteAllBytes(imagePath, imageBytes);
                        objV.imagen = $"~/Imagenes/{objV.nombre}.JPG";
                    }
            
        }
                catch (Exception ex)
                {
                    mensaje = "Error al registrar..!! " + ex.Message;
                }
                cn.Close();
            }
            return mensaje;
        }


        /*public bool Eliminar(int id)
    {
        bool respuesta = true;
        using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("delete from Producto where idProducto = @id", oConexion);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();

                cmd.ExecuteNonQuery();

                respuesta = true;

            }
            catch (Exception ex)
            {
                respuesta = false;
            }

        }

        return respuesta;

    }*/
    }




}
