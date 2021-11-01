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
    public partial class FRMVenta : Form
    {

        private DataTable dtdetalle = new DataTable();
        public FRMVenta()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NVenta.Listar();
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
            DgvListado.Columns[2].Visible = false;
            DgvListado.Columns[0].Width = 100;
            DgvListado.Columns[1].Width = 60;
            DgvListado.Columns[3].Width = 200;
            DgvListado.Columns[4].Width = 250;
            DgvListado.Columns[5].Width = 150;
            DgvListado.Columns[5].HeaderText = "Documento";
            DgvListado.Columns[6].Width = 70;
            DgvListado.Columns[6].HeaderText = "Serie";
            DgvListado.Columns[7].Width = 70;
            DgvListado.Columns[7].HeaderText = "Numero";
            DgvListado.Columns[8].Width = 250;
            DgvListado.Columns[8].HeaderText = "Fecha Emision";
            DgvListado.Columns[9].Width = 150;
            DgvListado.Columns[10].Width = 150;
            DgvListado.Columns[11].Width = 100;
        }

        private void Buscar()
        {
            try
            {                
                if (DgvListado.RowCount!=0)
                {
                    DgvListado.DataSource = NVenta.Buscar(TxtBuscar.Text);
                    this.Formato();
                    lblTotal.Text = "Total registros: " + Convert.ToString(DgvListado.Rows.Count);
                }
                else
                {
                    this.MensajeOK("No existen registros...");
                }               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Limpiar()
        {
            TxtBuscar.Clear();
            txtId.Clear();
            txtCodigo.Clear();
            txtIdCliente.Clear();
            txtNombreCliente.Clear();
            txtSerieComprobante.Clear();
            txtNumComprobante.Clear();
            dtdetalle.Clear();
            txtSubtotal.Text = "0.00";
            txtTotalImpuesto.Text = "0.00";
            txtTotal.Text = "0.00";


            btnInsertar.Visible = true;
            ErrorIcono.Clear();

            /*CHECK BOX*/
            DgvListado.Columns[0].Visible = false;
            btnAnular.Visible = false;
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

        private void CrearTabla()
        {
            this.dtdetalle.Columns.Add("idarticulo", System.Type.GetType("System.Int32"));
            this.dtdetalle.Columns.Add("codigo", System.Type.GetType("System.String"));
            this.dtdetalle.Columns.Add("articulo", System.Type.GetType("System.String"));
            this.dtdetalle.Columns.Add("stock", System.Type.GetType("System.Int32"));
            this.dtdetalle.Columns.Add("cantidad", System.Type.GetType("System.Int32"));
            this.dtdetalle.Columns.Add("precio", System.Type.GetType("System.Decimal"));
            this.dtdetalle.Columns.Add("descuento", System.Type.GetType("System.Decimal"));
            this.dtdetalle.Columns.Add("importe", System.Type.GetType("System.Decimal"));

            DgvDetalle.DataSource = this.dtdetalle;

            DgvDetalle.Columns[0].Visible = false;
            DgvDetalle.Columns[1].HeaderText = "CODIGO";
            DgvDetalle.Columns[1].Width = 100;
            DgvDetalle.Columns[2].HeaderText = "ARTICULO";
            DgvDetalle.Columns[2].Width = 200;
            DgvDetalle.Columns[3].HeaderText = "STOCK";
            DgvDetalle.Columns[3].Width = 80;
            DgvDetalle.Columns[4].HeaderText = "CANTIDAD";
            DgvDetalle.Columns[4].Width = 70;
            DgvDetalle.Columns[5].HeaderText = "PRECIO";
            DgvDetalle.Columns[5].Width = 70;
            DgvDetalle.Columns[6].HeaderText = "DESCUENTO";
            DgvDetalle.Columns[6].Width = 80;
            DgvDetalle.Columns[7].HeaderText = "IMPORTE";
            DgvDetalle.Columns[7].Width = 80;

            DgvDetalle.Columns[1].ReadOnly = true; //READONLY => DE SOLO LECTURA  | NO SE PUEDE MODIFICAR
            DgvDetalle.Columns[2].ReadOnly = true;
            DgvDetalle.Columns[3].ReadOnly = true;
            DgvDetalle.Columns[7].ReadOnly = true;

        }

        private void FRMVenta_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CrearTabla();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            FRMVista_ClienteVenta vista = new FRMVista_ClienteVenta();
            vista.ShowDialog();
            txtIdCliente.Text =Convert.ToString( Variables.IdCliente);
            txtNombreCliente.Text = Variables.NombreCliente;

        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataTable tabla = new DataTable();
                    tabla = NArticulo.BuscarCodigoVenta(txtCodigo.Text.Trim());
                    if (tabla.Rows.Count <= 0)
                    {
                        this.MensajeError("No existe articulos con ese codigo de barras o no hay stock de ese articulo");
                    }
                    else
                    {
                        //agregar al detalle
                        this.AgregarDetalle(Convert.ToInt32(tabla.Rows[0][0]), Convert.ToString(tabla.Rows[0][1]), Convert.ToString(tabla.Rows[0][2]),Convert.ToInt32(tabla.Rows[0][4]), Convert.ToDecimal(tabla.Rows[0][3]));
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void AgregarDetalle(int idarticulo, string codigo, string nombre,int stock, decimal precio)
        {
            bool Agregar = true;
            foreach (DataRow filatemp in dtdetalle.Rows)
            {
                if (Convert.ToInt32(filatemp["idarticulo"]) == idarticulo)
                {
                    Agregar = false;
                    this.MensajeError("El articulo ya ha sido agregado.");
                }
            }

            if (Agregar)
            {
                DataRow fila = dtdetalle.NewRow();
                fila["idarticulo"] = idarticulo;
                fila["codigo"] = codigo;
                fila["articulo"] = nombre;
                fila["stock"] = stock;
                fila["cantidad"] = 1;
                fila["precio"] = precio;
                fila["descuento"] = 0;
                fila["importe"] = precio;
                this.dtdetalle.Rows.Add(fila);
                this.CalcularTotales();
            }


        }
        private void CalcularTotales()
        {
            decimal Total = 0;
            decimal subtotal = 0;

            if (DgvDetalle.Rows.Count == 0)
            {
                Total = 0;
            }
            else
            {
                foreach (DataRow FilaTemp in dtdetalle.Rows)
                {
                    Total = Total + Convert.ToDecimal(FilaTemp["IMPORTE"]);
                }
            }
            subtotal = Total / (1 + Convert.ToDecimal(txtImpuesto.Text));
            txtTotal.Text = Total.ToString("#0.00#");//INDICA FORMATO
            txtSubtotal.Text = subtotal.ToString("#0.00#");
            txtTotalImpuesto.Text = (Total - subtotal).ToString("#0.00#");
        }

        private void btnVerArticulo_Click(object sender, EventArgs e)
        {
            PanelArticulos.Visible = true;
        }

        private void btnCerrarArticulo_Click(object sender, EventArgs e)
        {
            PanelArticulos.Visible = false;
        }

        private void FormatoArticulo()
        {
            dgvArticulos.Columns[1].Visible = false;
            dgvArticulos.Columns[1].Width = 50;
            dgvArticulos.Columns[2].Width = 150;
            dgvArticulos.Columns[2].HeaderText = "Categoria";
            dgvArticulos.Columns[3].Width = 100;
            dgvArticulos.Columns[3].HeaderText = "Código";
            dgvArticulos.Columns[4].Width = 150;
            dgvArticulos.Columns[5].Width = 100;
            dgvArticulos.Columns[5].HeaderText = "Precio Venta";
            dgvArticulos.Columns[6].Width = 60;
            dgvArticulos.Columns[7].Width = 200;
            dgvArticulos.Columns[7].HeaderText = "Descripcion";
            dgvArticulos.Columns[8].Width = 100;
        }
        private void btnFiltrarArticulo_Click(object sender, EventArgs e)
        {
            try
            {
                dgvArticulos.DataSource = NArticulo.BuscarVenta(txtBuscarArticulo.Text.Trim());
                this.FormatoArticulo();
                lblTotalArticulo.Text = "Total Registros: " + Convert.ToString(dgvArticulos.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void dgvArticulos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int idarticulo,stock;
            string codigo, nombre;
            decimal precio; 

            idarticulo = Convert.ToInt32(dgvArticulos.CurrentRow.Cells["ID"].Value);
            codigo = (string)dgvArticulos.CurrentRow.Cells["Codigo"].Value;
            nombre = (string)dgvArticulos.CurrentRow.Cells["Nombre"].Value;
            stock = Convert.ToInt32(dgvArticulos.CurrentRow.Cells["Stock"].Value);
            precio = (decimal)dgvArticulos.CurrentRow.Cells["Precio_Venta"].Value;

            this.AgregarDetalle(idarticulo, codigo, nombre,stock, precio);
        }

        private void DgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataRow fila = dtdetalle.Rows[e.RowIndex];
            string articulo = Convert.ToString(fila["articulo"]);
            int cantidad = Convert.ToInt32(fila["cantidad"]);
            int stock = Convert.ToInt32(fila["stock"]);
            decimal precio = Convert.ToDecimal(fila["precio"]);
            decimal descuento = Convert.ToDecimal(fila["descuento"]);

            if (cantidad > stock)
            {
                cantidad = stock;
                this.MensajeError("La cantidad de venta del articulo " + articulo + " supera el stock disponible " + stock);
                fila["cantidad"] = cantidad;
            }
            fila["importe"] = (precio * cantidad) - descuento;
            this.CalcularTotales();

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (txtIdCliente.Text == string.Empty || txtImpuesto.Text == string.Empty || txtNumComprobante.Text == string.Empty || dtdetalle.Rows.Count == 0)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados.");
                    ErrorIcono.SetError(txtIdCliente, "Seleccione un Cliente");
                    ErrorIcono.SetError(txtImpuesto, "Ingrese un Impuesto");
                    ErrorIcono.SetError(txtNumComprobante, "Ingrese el numero del comprobante");
                    ErrorIcono.SetError(DgvDetalle, "Debe tener al menos un detalle");
                }
                else
                {
                    rpta = NVenta.Insertar(Convert.ToInt32(txtIdCliente.Text), Variables.IdUsuario, cboComprobante.Text, txtSerieComprobante.Text.Trim(), txtNumComprobante.Text.Trim(), Convert.ToDecimal(txtImpuesto.Text), Convert.ToDecimal(txtTotal.Text), dtdetalle);
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
            TabGeneral.SelectedIndex=0;
        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DgMostrarDetalle.DataSource = NVenta.ListarDetalle(Convert.ToInt32(DgvListado.CurrentRow.Cells["ID"].Value));
                decimal total, subtotal;
                decimal impuesto = Convert.ToDecimal(DgvListado.CurrentRow.Cells["Impuesto"].Value);
                total = Convert.ToDecimal(DgvListado.CurrentRow.Cells["Total"].Value);
                subtotal = total / (1 + impuesto);
                txtSubtotalD.Text = subtotal.ToString("#0.00#");
                txtImpuestoD.Text = (total - subtotal).ToString("#0.00#");
                txtTotalD.Text = total.ToString("#0.00#");
                PanelMostrar.Visible = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            PanelMostrar.Visible = false;
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
                btnAnular.Visible = true;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;
                btnAnular.Visible = false;

            }
        }
        private void btnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Deseas Anular el(los) registro(s) ?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string rpta = "";
                    foreach (DataGridViewRow fila in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(fila.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(fila.Cells[1].Value);
                            rpta = NVenta.Anular(codigo);

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se Anuló el registro: " + Convert.ToString(fila.Cells[6].Value) + " - " + Convert.ToString(fila.Cells[7].Value));
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
