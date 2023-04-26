using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace ConexionDB
{
    public partial class frmAltaDisco : Form
    {
        private Discos disco = null;
        public frmAltaDisco()
        {
            InitializeComponent();
        }

        public frmAltaDisco(Discos disco)
        {
            InitializeComponent();
            this.disco = disco;
            Text = "Modificar Disco";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
         
            DiscosNegocio negocio = new DiscosNegocio();

            try
            {
                if (disco == null)
                    disco = new Discos();

                disco.Titulo = txtNombre.Text;
                disco.CantidadCanciones = int.Parse(txtCanciones.Text);
                disco.UrlImagenTapa = txtImagen.Text;
                disco.Genero = (Estilo)cmbEstilo.SelectedItem;
                


                if (disco.Id != 0)
                {
                    negocio.modificar(disco);
                    MessageBox.Show("Modificado exitosamente.");

                }
                else
                {
                    negocio.agregar(disco);
                    MessageBox.Show("Agregado exitosamente.");
                }

                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaDisco_Load(object sender, EventArgs e)
        {
            EstiloNegocio estiloNegocio = new EstiloNegocio();
            try
            {
                cmbEstilo.DataSource = estiloNegocio.listar();
                //dato escondido
                cmbEstilo.ValueMember = "Id"; //Descripcion.Antes estaba el IdTipo, porque llamé desde Discos Negocio a la consulta sql para que en el desplegable apareciera el Id por defecto
                //lo que quiero que se muestre
                cmbEstilo.DisplayMember = "Descripcion"; //Estilo.
                

                if(disco != null)
                {
                    txtNombre.Text = disco.Titulo;
                    txtCanciones.Text = disco.CantidadCanciones.ToString();
                    txtImagen.Text = disco.UrlImagenTapa;
                    cargarImagen(disco.UrlImagenTapa);
                    cmbEstilo.SelectedValue = disco.Genero.Id; //disco.genero.Descripcion

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void txtImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbNuevoDisco.Load(imagen);
            }
            catch
            {
                pbNuevoDisco.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png");
            }

        }
    }
}
