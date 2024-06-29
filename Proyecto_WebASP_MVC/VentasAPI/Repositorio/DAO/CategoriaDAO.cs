using Microsoft.Data.SqlClient;
using System;
using System.Data;
using VentasAPI.Models;
using VentasAPI.Repositorio.Interfaces;
namespace VentasAPI.Repositorio.DAO
{
    public class CategoriaDAO : ICategoria
    {
        //Definir la cadena de conexion
        private readonly string? cadena;

        public CategoriaDAO()
        {
            cadena = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json").Build()
                        .GetConnectionString("cn");
        }
        public IEnumerable<Categoria> listadoCategoria()
        {
            List<Categoria> aCategoria = new List<Categoria>();
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTADOCATEGORIA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aCategoria.Add(new Categoria
                {
                    id_categoria = int.Parse(dr[0].ToString()),
                    nom_cate = dr[1].ToString()
                });
            }
            cn.Close();
            return aCategoria;
        }
    }
}
