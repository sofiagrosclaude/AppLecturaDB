using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;//agregar la librería

namespace ConexionDB
{
    internal class DiscosNegocio
    {
        public List<Discos> listar() 
        {
            List<Discos> lista = new List<Discos>(); //crea la lista
            SqlConnection conexion = new SqlConnection(); //conexion a la base de datos
            SqlCommand comando = new SqlCommand(); 
            SqlDataReader lector;
            
            try
            {
                conexion.ConnectionString = "server=DESKTOP-VNUL8N7\\SQLEXPRESS; database=DISCOS_DB; integrated security=true"; // puede ser "server=.\\SQLEXPRESS";
                //si me conectara a otra computadora o a mi computadora pero con usuario y contraseña de sql server se pone --> integrated security=false; user; password";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select Id, Titulo, CantidadCanciones, UrlImagenTapa From DISCOS"; //consulta sql, probarla en el sql
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Discos aux = new Discos();
                    aux.Id = lector.GetInt32(0);
                    aux.Titulo = (string)lector["Titulo"];
                    aux.CantidadCanciones = lector.GetInt32(2);
                    aux.UrlImagenTapa = (string)lector["UrlImagenTapa"];
                   
                   
                    lista.Add(aux);
                }

                conexion.Close();
                return lista; //devuelve la lista
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
