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
            cmbCampo.Items.Add("Id");
            cmbCampo.Items.Add("Título");
            cmbCampo.Items.Add("Género");
            
        }

        private void dgvDiscos_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvDiscos.CurrentRow != null)
            {

            Discos seleccionado = (Discos)dgvDiscos.CurrentRow.DataBoundItem;
            // pbDiscos.Load(seleccionado.UrlImagenTapa); //es reemplazada por la de abajo
            cargarImagen(seleccionado.UrlImagenTapa); 
            }
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
                ocultarColumnas();
                cargarImagen(listaDiscos[0].UrlImagenTapa);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarColumnas()
        {
            //pbDiscos.Load(listaDiscos[0].UrlImagenTapa);
            dgvDiscos.Columns["UrlImagenTapa"].Visible = false;
            // dgvDiscos.Columns["Id"].Visible = false;
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

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            DiscosNegocio negocio = new DiscosNegocio();
            Discos seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Desea eliminar el disco?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {

                    seleccionado = (Discos)dgvDiscos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    cargar();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }

        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            DiscosNegocio negocio = new DiscosNegocio();
            
            try
            {
            string campo = cmbCampo.SelectedItem.ToString();
            string criterio = cmbCriterio.SelectedItem.ToString();
            string filtro = txtFiltroAvanzado.Text;
                dgvDiscos.DataSource = negocio.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
      
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Discos> listaFiltrada;
            string filtro = txtFiltro.Text;

            if (filtro != "") //if (filtro.Length >= 3) (para que filtre a partir de 3 caracteres)
            {
                //a x se le puede poner cualquier nombre porque es como item.
                listaFiltrada = listaDiscos.FindAll(x => x.Titulo.ToUpper().Contains(filtro.ToUpper()) || x.Genero.Descripcion.ToUpper().Contains(filtro.ToUpper()));
                //listaFiltrada = listaDiscos.FindAll(x => x.Titulo == txtFiltro.Text)
            }
            else
            {
                listaFiltrada = listaDiscos;
            }


            dgvDiscos.DataSource = null;
            dgvDiscos.DataSource = listaFiltrada;
            ocultarColumnas();
        }

        private void cmbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cmbCampo.SelectedItem.ToString();
            if(opcion == "Id")
            {
                cmbCriterio.Items.Clear();
                cmbCriterio.Items.Add("Mayor a");
                cmbCriterio.Items.Add("Menor a");
                cmbCriterio.Items.Add("Igual a");
            }
            else
            {

                cmbCriterio.Items.Clear();
                cmbCriterio.Items.Add("Comienza con");
                cmbCriterio.Items.Add("Termina con");
                cmbCriterio.Items.Add("Contiene");
            }
        }
    }
}

