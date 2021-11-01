﻿using SistemaVentas.Negocio;
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
    public partial class FRMVista_ClienteVenta : Form
    {
        public FRMVista_ClienteVenta()
        {
            InitializeComponent();
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
        private void FRMVista_ClienteVenta_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Variables.IdCliente = Convert.ToInt32(DgvListado.CurrentRow.Cells["ID"].Value);
            Variables.NombreCliente= Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
            this.Close();
        }
    }
}
