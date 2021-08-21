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
    public partial class FRMCategoria : Form
    {
        private string NombreAnt;
        public FRMCategoria()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NCategoria.Listar();
                this.Formato();
                this.Limpiar();
                lblTotal.Text = "Total registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }
        private void Formato()
        {
            DgvListado.Columns[0].Visible = false;
            DgvListado.Columns[1].Visible = false;
            DgvListado.Columns[2].Width = 250;
            DgvListado.Columns[3].Width = 499;
            DgvListado.Columns[3].HeaderText = "Descripción";
            DgvListado.Columns[4].Width = 117;


        }

        private void Buscar()
        {
            try
            {
                DgvListado.DataSource = NCategoria.Buscar(TxtBuscar.Text);
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
            txtDescripcion.Clear();
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

        private void FRMCategoria_Load(object sender, EventArgs e)
        {
            this.Listar();
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
                if(txtNombre.Text == string.Empty)
                {
                   this.MensajeError("Falta ingresar algunos datos, serán remarcados.");
                    ErrorIcono.SetError(txtNombre, "Ingrese un nombre");
                }
                else
                {
                    rpta = NCategoria.Insertar(txtNombre.Text.Trim(),txtDescripcion.Text.Trim());
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            TabGeneral.SelectedIndex = 0;
        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                btnActualizar.Visible = true;
                btnInsertar.Visible = false;
                txtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                txtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                txtDescripcion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Descripcion"].Value);
                TabGeneral.SelectedIndex = 1;
            }
            catch (Exception)
            {

                MessageBox.Show("Seleccione en la celda nombre"); 
            }

           
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (txtNombre.Text == string.Empty || txtId.Text==string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados.");
                    ErrorIcono.SetError(txtNombre, "Ingrese un nombre");
                }
                else
                {
                    rpta = NCategoria.Actualizar(Convert.ToInt32(txtId.Text),this.NombreAnt,txtNombre.Text.Trim(), txtDescripcion.Text.Trim());
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
                opcion = MessageBox.Show("Deseas eliminar el(los) registro(s) ?","Sistema de Ventas",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string rpta="";
                    foreach (DataGridViewRow fila in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(fila.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(fila.Cells[1].Value);
                            rpta = NCategoria.Eliminar(codigo);

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se elimino el registro: " + fila.Cells[2].Value);                               
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
                opcion = MessageBox.Show("Deseas Activar el(los) registro(s) ?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string rpta = "";
                    foreach (DataGridViewRow fila in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(fila.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(fila.Cells[1].Value);
                            rpta = NCategoria.Activar(codigo);

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se Activo el registro: " + fila.Cells[2].Value);
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
                opcion = MessageBox.Show("Deseas Desactivar el(los) registro(s) ?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string rpta = "";
                    foreach (DataGridViewRow fila in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(fila.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(fila.Cells[1].Value);
                            rpta = NCategoria.Desactivar(codigo);

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se Desactivo el registro: " + fila.Cells[2].Value);
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
