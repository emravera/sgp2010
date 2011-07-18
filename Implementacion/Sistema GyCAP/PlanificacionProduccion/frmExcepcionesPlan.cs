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
        }

        public void CargarGrilla(List<Entidades.ExcepcionesPlan> excepciones)
        {
            //Seteamos la lista generica como fuente de datos de la lista
            dgvLista.DataSource = excepciones;
            dgvLista.Invalidate();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }
    }
}
