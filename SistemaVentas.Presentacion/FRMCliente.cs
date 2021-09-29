using SistemaVentas.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Presentacion
{
    public partial class FRMCliente : Form
    {

        private string NombreAnt;
        public FRMCliente()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NPersona.ListarClientes();
                this.Formato();
                this.Limpiar();
                lbltotal_3.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
                //lblTotal.Text = "Total registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }
        private void Formato()
        {
            DgvListado.Columns[0].Visible = false;          
            DgvListado.Columns[1].Width = 50;
            DgvListado.Columns[2].Width = 100;
            DgvListado.Columns[2].HeaderText = "Tipo Persona";
            DgvListado.Columns[3].Width = 170;
            DgvListado.Columns[4].Width = 100;
            DgvListado.Columns[4].HeaderText = "Documento";
            DgvListado.Columns[5].Width = 100;
            DgvListado.Columns[5].HeaderText = "Número Doc.";
            DgvListado.Columns[6].Width = 120;
            DgvListado.Columns[6].HeaderText = "Dirección";
            DgvListado.Columns[7].Width = 100;
            DgvListado.Columns[7].HeaderText = "Teléfono";
            DgvListado.Columns[8].Width = 120;


        }

        private void Buscar()
        {
            try
            {
                DgvListado.DataSource = NPersona.BuscarClientes(TxtBuscar.Text);
                this.Formato();
                //lbltotal_3.Text = "Total registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Limpiar()
        {
            TxtBuscar.Clear();
            txtNombre.Clear();
            txtId.Clear();
            txtNumDocu.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();        
            btnInsertar.Visible = true;
            btnActualizar.Visible = false;
            ErrorIcono.Clear();

            /*CHECK BOX*/
            DgvListado.Columns[0].Visible = false;      
            btnEliminar.Visible = false;
            chkSeleccionar.Checked = false;

        }
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOK(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }    

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (txtNombre.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados.");                 
                    ErrorIcono.SetError(txtNombre, "Ingrese un nombre");                                   
                }
                else
                {
                    rpta = NPersona.Insertar("Cliente",txtNombre.Text.Trim(), cboTipoDocu.Text, txtNumDocu.Text.Trim(), txtDireccion.Text.Trim(), txtTelefono.Text.Trim(), txtEmail.Text.Trim());

                    if (rpta.Equals("OK"))//equals para cadena
                    {
                        this.MensajeOK("Se Registro Correctamente");
                        this.Limpiar();
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                btnActualizar.Visible = true;
                btnInsertar.Visible = false;
                txtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                this.NombreAnt= Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                txtNombre.Text= Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                cboTipoDocu.Text= Convert.ToString(DgvListado.CurrentRow.Cells["Tipo_Documento"].Value);
                txtNumDocu.Text= Convert.ToString(DgvListado.CurrentRow.Cells["Num_Documento"].Value);
                txtDireccion.Text= Convert.ToString(DgvListado.CurrentRow.Cells["Direccion"].Value);
                txtTelefono.Text= Convert.ToString(DgvListado.CurrentRow.Cells["Telefono"].Value);
                txtEmail.Text= Convert.ToString(DgvListado.CurrentRow.Cells["Email"].Value);
                TabGeneral.SelectedIndex = 1;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Seleccione desde la celda nombre." + "| Error: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (txtId.Text==string.Empty || txtNombre.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados.");
                    ErrorIcono.SetError(txtNombre, "Ingrese un nombre");
                    
                }
                else
                {
                    rpta = NPersona.Actualizar(Convert.ToInt32(txtId.Text),"Cliente",this.NombreAnt, txtNombre.Text.Trim(), cboTipoDocu.Text, txtNumDocu.Text.Trim(), txtDireccion.Text.Trim(), txtTelefono.Text.Trim(), txtEmail.Text.Trim());

                    if (rpta.Equals("OK"))//equals para cadena
                    {
                        this.MensajeOK("Se Actualizo Correctamente");
                        this.Limpiar();
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            TabGeneral.SelectedIndex = 0;
        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;               
                btnEliminar.Visible = true;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;            
                btnEliminar.Visible = false;
            }
        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Deseas eliminar el(los) registro(s) ?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string rpta = "";
                    foreach (DataGridViewRow fila in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(fila.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(fila.Cells[1].Value);
                            rpta = NPersona.Eliminar(codigo);

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se elimino el registro: " + fila.Cells[4].Value);
                            }
                            else
                            {
                                this.MensajeError(rpta);
                            }
                        }
                    }
                    this.Listar();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FRMCliente_Load(object sender, EventArgs e)
        {
            this.Listar();
        }
    }
}
