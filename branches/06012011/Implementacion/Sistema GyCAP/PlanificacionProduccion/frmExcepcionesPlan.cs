using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.PlanificacionProduccion
{
    public partial class frmExcepcionesPlan : Form
    {
       
        public frmExcepcionesPlan()
        {
            InitializeComponent();

            /*
            dgvLista.AutoGenerateColumns = false;

            //Lista de Demandas
            //Agregamos la columnas
            dgvLista.Columns.Add("NOMBRE", "Nombre");
            dgvLista.Columns.Add("TIPO", "Tipo Excepción");
            dgvLista.Columns.Add("DESCRIPCION", "Descripción");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            */
              
        }

        public void CargarGrilla(List<Entidades.ExcepcionesPlan> excepciones)
        {
            dgvLista.AutoGenerateColumns = false;

            //Seteamos la lista generica como fuente de datos de la lista
            dgvLista.DataSource = excepciones;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
