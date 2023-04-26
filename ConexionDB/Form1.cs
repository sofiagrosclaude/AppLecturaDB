using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio; //Hago que Dominio reconozcaa formulario
using Negocio; //Hago que Dominio reconozca a Negocio


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
            cargar();

        }

        private void dgvDiscos_SelectionChanged(object sender, EventArgs e)
        {
            Discos seleccionado = (Discos)dgvDiscos.CurrentRow.DataBoundItem;
           // pbDiscos.Load(seleccionado.UrlImagenTapa); //es reemplazada por la de abajo
            cargarImagen(seleccionado.UrlImagenTapa); 
        }

        //Si cambio la seleccion de los dos vo
        //id de arriba, comento todo lo que sea cargarImagen

        private void cargar()
        {
            DiscosNegocio negocio = new DiscosNegocio();
            try
            {
                listaDiscos = negocio.listar();
                dgvDiscos.DataSource = listaDiscos; //negocio.listar va a la base de datos y te devuelve una lista, datasource recibe un origen de dato y lo modela en la tabla
                //pbDiscos.Load(listaDiscos[0].UrlImagenTapa);
                dgvDiscos.Columns["UrlImagenTapa"].Visible = false;
               // dgvDiscos.Columns["Id"].Visible = false;
                cargarImagen(listaDiscos[0].UrlImagenTapa);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaDisco alta = new frmAltaDisco();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Discos seleccionado;
            seleccionado = (Discos)dgvDiscos.CurrentRow.DataBoundItem;

            frmAltaDisco modificar = new frmAltaDisco(seleccionado);
            modificar.ShowDialog();
            cargar();
        }
    }
}

