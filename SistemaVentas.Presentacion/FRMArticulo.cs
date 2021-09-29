using SistemaVentas.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Presentacion
{
    public partial class FRMArticulo : Form
    {
        private string RutaOrigen;
        private string RutaDestino;
        private string Directorio= "E:\\sistema\\";
        private string NombreAnt;
        public FRMArticulo()
        {
            InitializeComponent();
        }
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NArticulo.Listar();
                this.Formato();
                this.Limpiar();
                lblTotal.Text = "Total registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }
        private void Formato() //FORMATO DE LA TABLA
        {
            DgvListado.Columns[0].Visible = false;
            DgvListado.Columns[2].Visible = false;
            DgvListado.Columns[0].Width = 100;
            DgvListado.Columns[1].Width = 50;
            DgvListado.Columns[3].Width = 180;
            DgvListado.Columns[3].HeaderText = "Categoría";
            DgvListado.Columns[4].Width = 100;
            DgvListado.Columns[4].HeaderText = "Código";
            DgvListado.Columns[5].Width = 150;
            DgvListado.Columns[6].Width = 100;
            DgvListado.Columns[6].HeaderText = "Precio Venta";
            DgvListado.Columns[7].Width = 100;        
            DgvListado.Columns[8].Width = 200;
            DgvListado.Columns[8].HeaderText = "Descripción";
            DgvListado.Columns[9].Width = 140;
            DgvListado.Columns[10].Width = 100;
            //DgvListado.Columns[3].HeaderText = "Descripción";
            DgvListado.Columns[4].Width = 117;


        }

        private void Buscar()
        {
            try
            {
                DgvListado.DataSource = NArticulo.Buscar(TxtBuscar.Text);
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
            txtCodigo.Clear();
            PanelCodigo.BackgroundImage = null;
            btnGuardarCodigo.Enabled = true;
            txtPrecioVenta.Clear();
            txtStock.Clear();
            txtImagen.Clear();
            pickImagen.Image = null;
            txtDescripcion.Clear();
            btnInsertar.Visible = true;
            btnActualizar.Visible = false;
            ErrorIcono.Clear();
            this.RutaDestino = "";
            this.RutaOrigen = "";

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
       
        private void CargarCategoria()/*CARGAR COMBOBOX*/
        {
            try
            {
                cboCategoria.DataSource = NCategoria.Seleccionar();
                cboCategoria.ValueMember = "idcategoria";
                cboCategoria.DisplayMember = "nombre";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        
        private void FRMArticulo_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarCategoria();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void btnCargarImagen_Click(object sender, EventArgs e)/*CARGAR IMAGEN AL REGISTRO*/
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter= "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (file.ShowDialog() ==DialogResult.OK)
            {
                pickImagen.Image = Image.FromFile(file.FileName);
                txtImagen.Text = file.FileName.Substring(file.FileName.LastIndexOf("\\") + 1);
                this.RutaOrigen = file.FileName;
            }
        }

        private void btnGenerarCodigo_Click(object sender, EventArgs e)/*GENERAR CODIGO BARRAS*/
        {
            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
            codigo.IncludeLabel = true;
            PanelCodigo.BackgroundImage = codigo.Encode(BarcodeLib.TYPE.CODE128,txtCodigo.Text,Color.Black,Color.White,400,120);
            btnGuardarCodigo.Enabled = true;
        }

        private void btnGuardarCodigo_Click(object sender, EventArgs e)/*GUARDAR CODIGO DE BARRAS*/
        {
            Image imgFinal = (Image)PanelCodigo.BackgroundImage.Clone();

            SaveFileDialog dialogoGuardar = new SaveFileDialog();
            dialogoGuardar.AddExtension = true;
            dialogoGuardar.Filter = "Image PNG (*.png)|*.png";
            dialogoGuardar.ShowDialog();
            if (!string.IsNullOrEmpty(dialogoGuardar.FileName))
            {
                imgFinal.Save(dialogoGuardar.FileName, ImageFormat.Png);
            }
            imgFinal.Dispose();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (cboCategoria.Text == string.Empty || txtNombre.Text==string.Empty || txtPrecioVenta.Text==string.Empty || txtStock.Text==string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados.");
                    ErrorIcono.SetError(cboCategoria, "Seleccione uan categoria");
                    ErrorIcono.SetError(txtNombre, "Ingrese un nombre");
                    ErrorIcono.SetError(txtPrecioVenta, "Ingrese un precio");
                    ErrorIcono.SetError(txtStock, "Ingrese un stock");
                }
                else
                {
                    rpta = NArticulo.Insertar(Convert.ToInt32(cboCategoria.SelectedValue),txtCodigo.Text.Trim(),txtNombre.Text.Trim(),Convert.ToDecimal(txtPrecioVenta.Text),Convert.ToInt32(txtStock.Text),txtDescripcion.Text.Trim(),txtImagen.Text.Trim());
                    if (rpta.Equals("OK"))//equals para cadena
                    {
                        this.MensajeOK("Se Registro Correctamente");
                        if (txtImagen.Text !=string.Empty)
                        {
                            this.RutaDestino = this.Directorio + txtImagen.Text;
                            File.Copy(this.RutaOrigen,this.RutaDestino);
                        }                       
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
                txtId.Text =Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                cboCategoria.SelectedValue = Convert.ToString(DgvListado.CurrentRow.Cells["idcategoria"].Value);
                txtCodigo.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Codigo"].Value);
                this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                txtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                txtPrecioVenta.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Precio_Venta"].Value);
                txtStock.Text= Convert.ToString(DgvListado.CurrentRow.Cells["Stock"].Value);
                txtDescripcion.Text= Convert.ToString(DgvListado.CurrentRow.Cells["Descripcion"].Value);
                string Imagen;
                Imagen= Convert.ToString(DgvListado.CurrentRow.Cells["Imagen"].Value);
                if (Imagen!=string.Empty)
                {
                    pickImagen.Image = Image.FromFile(this.Directorio + Imagen);
                    txtImagen.Text = Imagen;
                }
                else
                {
                    pickImagen.Image = null;
                    txtImagen.Text = "";
                }
                TabGeneral.SelectedIndex = 1;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Seleccione desde la celda nombre." + " | Error: " +ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (cboCategoria.Text == string.Empty || txtNombre.Text == string.Empty || txtPrecioVenta.Text == string.Empty || txtStock.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados.");
                    ErrorIcono.SetError(cboCategoria, "Seleccione uan categoria");
                    ErrorIcono.SetError(txtNombre, "Ingrese un nombre");
                    ErrorIcono.SetError(txtPrecioVenta, "Ingrese un precio");
                    ErrorIcono.SetError(txtStock, "Ingrese un stock");
                }
                else
                {
                    rpta = NArticulo.Actualizar(Convert.ToInt32(txtId.Text),Convert.ToInt32(cboCategoria.SelectedValue), txtCodigo.Text.Trim(),this.NombreAnt, txtNombre.Text.Trim(), Convert.ToDecimal(txtPrecioVenta.Text), Convert.ToInt32(txtStock.Text), txtDescripcion.Text.Trim(), txtImagen.Text.Trim());
                    if (rpta.Equals("OK"))//equals para cadena
                    {
                        this.MensajeOK("Se Actualizo Correctamente");
                        if (txtImagen.Text != string.Empty && RutaOrigen!=string.Empty)
                        {
                            this.RutaDestino = this.Directorio + txtImagen.Text;
                            File.Copy(this.RutaOrigen, this.RutaDestino);
                        }
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(rpta);
                        TabGeneral.SelectedIndex = 0;
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
            TabGeneral.SelectedIndex = 0; //MOSTRAR EL LISTADO INDICE 0 = LISTADO ; 1 = MANTENIMIENTO
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
                    string imagen = "";
                    foreach (DataGridViewRow fila in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(fila.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(fila.Cells[1].Value);
                            imagen = Convert.ToString(fila.Cells[9].Value);
                            rpta = NArticulo.Eliminar(codigo);
                            

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se elimino el registro: " + Convert.ToString(fila.Cells[5].Value));
                                File.Delete(this.Directorio + imagen);
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
                            rpta = NArticulo.Desactivar(codigo);

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se Desactivo el registro: " + Convert.ToString(fila.Cells[5].Value));
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
                            rpta = NArticulo.Activar(codigo);

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se Activo el registro: " + Convert.ToString(fila.Cells[5].Value));
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
