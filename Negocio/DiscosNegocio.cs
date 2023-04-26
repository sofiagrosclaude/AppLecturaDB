using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;//agregar la librería
using Dominio; //Hago que Dominio reconozca a ConexionDB
using System.Diagnostics;

namespace Negocio
{
    public class DiscosNegocio
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
                // comando.CommandText = "Select Id, Titulo, CantidadCanciones, UrlImagenTapa From DISCOS"; //consulta sql, probarla en el sql
                comando.CommandText = "Select D.Id, Titulo, CantidadCanciones, UrlImagenTapa, E.Descripcion as Genero, D.IdEstilo From DISCOS D, ESTILOS E Where E.Id = D.IdEstilo";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Discos aux = new Discos();
                    aux.Id = lector.GetInt32(0);
                    aux.Titulo = (string)lector["Titulo"];
                    aux.CantidadCanciones = lector.GetInt32(2);

                    //VALIDACIÓN DE NULL opcion 1
                    if (!(lector.IsDBNull(lector.GetOrdinal("UrlImagenTapa"))))
                        aux.UrlImagenTapa = (string)lector["UrlImagenTapa"];

                    //validacion de null opcion 2 (no me funciona)
                    //if (lector["UrlImagenTapa"] is DBNull)
                    //aux.UrlImagenTapa = (string)lector["UrlImagenTapa"];

                   

                    aux.Genero = new Estilo();
                    aux.Genero.Id = (int)lector["IdEstilo"]; //(en caso de que llamara a la consulta sql para pedir D.IdTipo)
                    aux.Genero.Descripcion = (string)lector["Genero"];
                    
                   
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


        public void agregar(Discos nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into DISCOS (Titulo, CantidadCanciones, UrlImagenTapa, IdEstilo)values('" + nuevo.Titulo + "', " + nuevo.CantidadCanciones + ", '" + nuevo.UrlImagenTapa + "', @IdEstilo)");
                datos.setearParametro("@IdEstilo", nuevo.Genero.Id);
               
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Discos disc)
        {
                AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update DISCOS set Titulo = @nombre, CantidadCanciones = @canciones, UrlImagenTapa = @imagen, IdEstilo = @idEstilo Where Id = @id");
                datos.setearParametro("@nombre", disc.Titulo);
                datos.setearParametro("@canciones", disc.CantidadCanciones);
                datos.setearParametro("@imagen", disc.UrlImagenTapa);
                datos.setearParametro("@idEstilo", disc.Genero.Id);
                datos.setearParametro("@id", disc.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


    }

   
}
