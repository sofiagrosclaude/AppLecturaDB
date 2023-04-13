using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionDB
{
    public partial class Form1 : Form //CLASE DE LA VENTANA
    {
        private List<Discos> listaDiscos;// atributo que lista los elementos recibidos

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DiscosNegocio negocio = new DiscosNegocio(); 
            listaDiscos = negocio.listar();
            dgvDiscos.DataSource = listaDiscos; //negocio.listar va a la base de datos y te devuelve una lista, datasource recibe un origen de dato y lo modela en la tabla
            //pbDiscos.Load(listaDiscos[0].UrlImagenTapa);
            cargarImagen(listaDiscos[0].UrlImagenTapa);

        }

        private void dgvDiscos_SelectionChanged(object sender, EventArgs e)
        {
            Discos seleccionado = (Discos)dgvDiscos.CurrentRow.DataBoundItem;
           // pbDiscos.Load(seleccionado.UrlImagenTapa); //es reemplazada por la de abajo
            cargarImagen(seleccionado.UrlImagenTapa); 
        }

        //Si cambio la seleccion de los dos void de arriba, comento todo lo que sea cargarImagen
        private void cargarImagen(string imagen)
        {
            try
            {
                pbDiscos.Load(imagen);
            }
            catch
            {
                pbDiscos.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png");
            }
            
        }
    }
}

