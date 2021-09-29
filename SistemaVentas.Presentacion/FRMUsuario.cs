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
    public partial class FRMUsuario : Form
    {
        private string EmailAnt;

        public FRMUsuario()
        {
            InitializeComponent();
        }
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NUsuario.Listar();
                this.Formato();
                this.Limpiar();
                lbltotal_2.Text = "Total Registros: " + Convert.ToString(DgvListado.Rows.Count);
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
            DgvListado.Columns[2].Visible = false;
            DgvListado.Columns[1].Width = 50;
            DgvListado.Columns[3].Width = 100;
            DgvListado.Columns[4].Width = 170;
            DgvListado.Columns[5].Width = 100;
            DgvListado.Columns[5].HeaderText = "Documento";
            DgvListado.Columns[6].Width = 100;
            DgvListado.Columns[6].HeaderText = "Número Doc.";
            DgvListado.Columns[7].Width = 120;
            DgvListado.Columns[7].HeaderText= "Dirección";
            DgvListado.Columns[8].Width = 100;
            DgvListado.Columns[8].HeaderText = "Teléfono";
            DgvListado.Columns[9].Width = 120;


        }

        private void Buscar()
        {
            try
            {
                DgvListado.DataSource = NUsuario.Buscar(TxtBuscar.Text);
                this.Formato();
                lblTotal.Text = "Total registros: " + Convert.ToString(DgvListado.Rows.Count);
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
            txtClave.Clear();
            btnInsertar.Visible = true;
            btnActualizar.Visible = false;
            ErrorIcono.Clear();

            /*CHECK BOX*/
            DgvListado.Columns[0].Visible = false;
            btnActivar.Visible = false;
            btnDesactivar.Visible = false;
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

        private void CargarRol()
        {
            try
            {
                cboRol.DataSource = NRol.Listar();
                cboRol.ValueMember = "idrol";
                cboRol.DisplayMember = "nombre";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void FRMUsuario_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarRol();
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
                if (cboRol.Text==string.Empty || txtNombre.Text == string.Empty || txtEmail.Text==string.Empty || txtClave.Text==string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados.");
                    ErrorIcono.SetError(cboRol, "Seleccione un rol");
                    ErrorIcono.SetError(txtNombre, "Ingrese un nombre");
                    ErrorIcono.SetError(txtEmail, "Ingrese un email");
                    ErrorIcono.SetError(txtClave, "Ingrese una clave de acceso");
                    
                }
                else
                {
                    rpta = NUsuario.Insertar(Convert.ToInt32(cboRol.SelectedValue),txtNombre.Text.Trim(),cboTipoDocu.Text,txtNumDocu.Text.Trim(),txtDireccion.Text.Trim(),txtTelefono.Text.Trim(),txtEmail.Text.Trim(),txtClave.Text.Trim());

                    if (rpta.Equals("OK"))//equals para cadena
                    {
                        this.MensajeOK("Se Registro Correctamente");                     
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
                btnInsertar.Visible = true;
                txtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                cboRol.SelectedValue = Convert.ToString(DgvListado.CurrentRow.Cells["idrol"].Value);
                txtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                cboTipoDocu.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Tipo_Documento"].Value);
                txtNumDocu.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Num_Documento"].Value);
                txtDireccion.Text= Convert.ToString(DgvListado.CurrentRow.Cells["Direccion"].Value);
                txtTelefono.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Telefono"].Value);
                this.EmailAnt= Convert.ToString(DgvListado.CurrentRow.Cells["Email"].Value);
                txtEmail.Text= Convert.ToString(DgvListado.CurrentRow.Cells["Email"].Value);
                TabGeneral.SelectedIndex = 1;//MOSTRAR EN EL MANTENIMIENTO
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
                if (txtId.Text==string.Empty || cboRol.Text == string.Empty || txtNombre.Text == string.Empty || txtEmail.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados.");
                    ErrorIcono.SetError(cboRol, "Seleccione un rol");
                    ErrorIcono.SetError(txtNombre, "Ingrese un nombre");
                    ErrorIcono.SetError(txtEmail, "Ingrese un email");                 

                }
                else
                {
                    rpta = NUsuario.Actualizar(Convert.ToInt32(txtId.Text),Convert.ToInt32(cboRol.SelectedValue), txtNombre.Text.Trim(), cboTipoDocu.Text, txtNumDocu.Text.Trim(), txtDireccion.Text.Trim(), txtTelefono.Text.Trim(),this.EmailAnt, txtEmail.Text.Trim(), txtClave.Text.Trim());

                    if (rpta.Equals("OK"))//equals para cadena
                    {
                        this.MensajeOK("Se Actualizó Correctamente");
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

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;
                btnActivar.Visible = true;
                btnDesactivar.Visible = true;
                btnEliminar.Visible = true;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;
                btnActivar.Visible = false;
                btnDesactivar.Visible = false;
                btnEliminar.Visible = false;
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
                            rpta = NUsuario.Eliminar(codigo);

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

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Deseas desactivar el(los) registro(s) ?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string rpta = "";
                    foreach (DataGridViewRow fila in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(fila.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(fila.Cells[1].Value);
                            rpta = NUsuario.Desactivar(codigo);

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se desactivo el registro: " + fila.Cells[4].Value);
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

        private void btnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Deseas activar el(los) registro(s) ?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string rpta = "";
                    foreach (DataGridViewRow fila in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(fila.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(fila.Cells[1].Value);
                            rpta = NUsuario.Activar(codigo);

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se activo el registro: " + fila.Cells[4].Value);
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
    }
}
