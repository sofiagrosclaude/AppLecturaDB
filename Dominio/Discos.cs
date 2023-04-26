using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Dominio
{
    public class Discos
    {
        
        public int Id { get; set; }
        [DisplayName("Título")]
        public string Titulo { get; set; }
        [DisplayName("Cantidad de canciones")]
        public int CantidadCanciones { get; set; }
        [DisplayName("Imagen")]
        public string UrlImagenTapa { get; set; }
      
      
        [DisplayName("Género")]
        public Estilo Genero { get; set; }
        




    }
}
