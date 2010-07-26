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
    public partial class frmPlanAnual : Form
    {
        private static frmPlanAnual _frmPlanAnual = null;
        private Data dsPlanAnual = new GyCAP.Data.dsEstimarDemanda();
        private DataView dvListaPlanes, dvListaDetalle, dvChAnios, dvComboEstimaciones;
        private enum estadoUI { inicio, nuevo, buscar, modificar, calcularPlanificacion };
        private static estadoUI estadoActual;


        public frmPlanAnual()
        {
            InitializeComponent();

            //Inicializamos las grillas
            dgvDetalle.AutoGenerateColumns = false;
            dgvLista.AutoGenerateColumns = false;

            //Para cada Lista
            //Lista de Demandas
            //Agregamos la columnas
            dgvLista.Columns.Add("PAN_CODIGO", "Código");
            dgvLista.Columns.Add("PAN_ANIO", "Año Plan");
            dgvLista.Columns.Add("DEMAN_CODIGO", "Demanda");
            dgvLista.Columns.Add("PAN_FECHACREACION", "Fecha Creación Plan Anual");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["PAN_CODIGO"].DataPropertyName = "PAN_CODIGO";
            dgvLista.Columns["PAN_ANIO"].DataPropertyName = "PAN_ANIO";
            dgvLista.Columns["DEMAN_CODIGO"].DataPropertyName = "DEMAN_CODIGO";
            dgvLista.Columns["PAN_FECHACREACION"].DataPropertyName = "PAN_FECHACREACION";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPlanes= new DataView(dsEstimarDemanda.DEMANDAS_ANUALES);
            dvListaPlanes.Sort = "PAN_ANIO ASC";
            dgvLista.DataSource = dvListaPlanes;

            //Lista de Detalles
            //Agregamos la columnas
            dgvDetalle.Columns.Add("DPAN_CODIGO", "Código");
            dgvDetalle.Columns.Add("DPAN_MES", "Mes");
            dgvDetalle.Columns.Add("DPAN_CANTIDADMES", "Fecha Inicio Plan");


            //Seteamos el modo de tamaño de las columnas
            dgvDetalle.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Llenamos el dataset con las estimaciones
                        
            
            //Seteamos los maxlength de los controles

        }

        
        
    }
}
