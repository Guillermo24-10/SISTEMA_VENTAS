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
    public partial class FRMPrincipal : Form
    {
        private int childFormNumber = 0;
        public int idusuario;
        public int idrol;
        public string nombre;
        public string rol;
        public bool estado;

        public FRMPrincipal()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRMRol frm = new FRMRol();
            frm.MdiParent = this;
            frm.Show();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRMCategoria frm = new FRMCategoria();
            frm.MdiParent = this;
            frm.Show();
        }

        private void articulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRMArticulo frm = new FRMArticulo();
            frm.MdiParent = this;
            frm.Show();
        }

        private void horaFecha_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToLongTimeString();
            lblFecha.Text= DateTime.Now.ToLongDateString();
        }

        private void FRMPrincipal_Load(object sender, EventArgs e)
        {
            stBarraInferior.Text = "Desarrollado por soporte01@jdm.com.pe";
            stBarraInfRight.Text = "Usuario: " + this.nombre;
            MessageBox.Show("Bienvenido: " + this.nombre, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (this.rol.Equals("Administrador"))
            {
                mnuAlmacen.Enabled = true;
                mnuIngresos.Enabled = true;
                mnuAccesos.Enabled = true;
                mnuVentas.Enabled = true;
                mnuConsultas.Enabled = true;
                tsCompras.Enabled = true;
                tsVentas.Enabled = true;
            }
            else
            {
                if (this.rol.Equals("Vendedor"))
                {
                    mnuAlmacen.Enabled = false;
                    mnuIngresos.Enabled = false;
                    mnuAccesos.Enabled = false;
                    mnuVentas.Enabled = true;
                    mnuConsultas.Enabled = true;
                    tsCompras.Enabled = false;
                    tsVentas.Enabled = true;
                }
                else
                {
                    if (this.rol.Equals("Almacenero"))
                    {
                        mnuAlmacen.Enabled = true;
                        mnuIngresos.Enabled = true;
                        mnuAccesos.Enabled = false;
                        mnuVentas.Enabled = false;
                        mnuConsultas.Enabled = true;
                        tsCompras.Enabled = true;
                        tsVentas.Enabled = false;
                    }
                    else
                    {
                        mnuAlmacen.Enabled = false;
                        mnuIngresos.Enabled = false;
                        mnuAccesos.Enabled = false;
                        mnuVentas.Enabled = false;
                        mnuConsultas.Enabled = false;
                        tsCompras.Enabled = false;
                        tsVentas.Enabled = false;
                    }
                }
            }
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRMUsuario frm = new FRMUsuario();
            frm.MdiParent = this;
            frm.Show();
        }

        private void FRMPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult opcion;
            opcion = MessageBox.Show("Deseas salir del Sistema?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (opcion == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRMProveedor frm = new FRMProveedor();
            frm.MdiParent = this;
            frm.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRMCliente frm = new FRMCliente();
            frm.MdiParent = this;
            frm.Show();
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRMIngreso frm = new FRMIngreso();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
