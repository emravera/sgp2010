using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.ControlTrabajoEnProceso
{
    public partial class frmControlPlanificacion : Form
    {
        private static frmControlPlanificacion _frmControlPlanificacion = null;
        private enum estadoUI { inicio };
        private DataView dvComboPlanAnual, dvComboPlanMensual, dvComboSemana, dvListaPlanSemanal;
        private static estadoUI estadoActual;
        private Data.dsPlanSemanal dsPlanSemanal = new GyCAP.Data.dsPlanSemanal();

        public frmControlPlanificacion()
        {
            InitializeComponent();

            //Lista de PLAN SEMANAL (DIAS)
            //Agregamos la columnas
            dgvDetallePlan.Columns.Add("DIAPSEM_CODIGO", "Código");
            dgvDetallePlan.Columns.Add("PSEM_CODIGO", "N° Semana");
            dgvDetallePlan.Columns.Add("DIAPSEM_DIA", "Día");
            dgvDetallePlan.Columns.Add("DIAPSEM_FECHA", "Fecha");

            //Seteamos el modo de tamaño de las columnas
            dgvDetallePlan.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePlan.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetallePlan.Columns["DIAPSEM_CODIGO"].DataPropertyName = "DIAPSEM_CODIGO";
            dgvDetallePlan.Columns["PSEM_CODIGO"].DataPropertyName = "PSEM_CODIGO";
            dgvDetallePlan.Columns["DIAPSEM_DIA"].DataPropertyName = "DIAPSEM_DIA";
            dgvDetallePlan.Columns["DIAPSEM_FECHA"].DataPropertyName = "DIAPSEM_FECHA";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPlanSemanal = new DataView(dsPlanSemanal.DIAS_PLAN_SEMANAL);
            dgvDetallePlan.DataSource = dvListaPlanSemanal;

            //Metodos para llenar el Dataset
            //Planes Anuales
            BLL.PlanAnualBLL.ObtenerTodos(dsPlanSemanal.PLANES_ANUALES);
            
            //CARGA DE COMBOS
            //Cargamos el combo de busqueda
            dvComboPlanAnual = new DataView(dsPlanSemanal.PLANES_ANUALES);
            cbPlanAnual.SetDatos(dvComboPlanAnual, "pan_codigo", "pan_anio", "Seleccione", false);

        }

        #region Servicios
        public static frmControlPlanificacion Instancia
        {
            get
            {
                if (_frmControlPlanificacion == null || _frmControlPlanificacion.IsDisposed)
                {
                    _frmControlPlanificacion = new frmControlPlanificacion();
                }
                else
                {
                    _frmControlPlanificacion.BringToFront();
                }
                return _frmControlPlanificacion;
            }
            set
            {
                _frmControlPlanificacion = value;
            }
        }
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:

                    break;
            }
        }
        #endregion

      

#region Pestaña Plan Semanal

        private void cbPlanAnual_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (cbPlanAnual.GetSelectedIndex() > -1)
                {
                    //Se deben cargar los planes Mensuales ya creados
                    int codigo = Convert.ToInt32(cbPlanAnual.GetSelectedValue());

                    BLL.PlanMensualBLL.ObtenerPMAnio(dsPlanSemanal.PLANES_MENSUALES, codigo);

                    dvComboPlanMensual = new DataView(dsPlanSemanal.PLANES_MENSUALES);
                    cbPlanMensual.SetDatos(dvComboPlanMensual, "pmes_codigo", "pmes_mes", "pmes_codigo ASC", "Seleccione", false);

                    if (dsPlanSemanal.PLANES_MENSUALES.Rows.Count > 0)
                    {
                        cbPlanAnual.Enabled = false;
                        cbPlanMensual.Enabled = true;
                    }
                    else
                    {
                        cbPlanAnual.Enabled = true;
                        cbPlanMensual.Enabled = false;
                    }

                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Control Plan - Plan Semanal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private void cbPlanMensual_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (cbPlanMensual.GetSelectedIndex() > -1)
                {
                    //Se deben cargar los planes Mensuales ya creados
                    int codigo = Convert.ToInt32(cbPlanMensual.GetSelectedValue());

                    BLL.PlanSemanalBLL.obtenerPS(dsPlanSemanal.PLANES_SEMANALES, codigo);

                    if (dsPlanSemanal.PLANES_SEMANALES.Rows.Count > 0)
                    {

                        dvComboSemana = new DataView(dsPlanSemanal.PLANES_SEMANALES);
                        cbPlanSemanal.SetDatos(dvComboSemana, "psem_codigo", "psem_semana", "Seleccione", false);
                        cbPlanSemanal.Enabled = true;
                        cbPlanMensual.Enabled = false;
                    }
                    else
                    {
                        cbPlanSemanal.Enabled = false;
                        MessageBox.Show("-No hay planes Semanales creados para ese mes", "Información: Plan Semanal - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetInterface(estadoUI.inicio);
                    }

                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Control Planes - Plan Semanal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }

        }
        private void btnCargaDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsPlanSemanal.DIAS_PLAN_SEMANAL.Clear();

                //Obtengo el Codigo del plan Semanal
                int codigo = Convert.ToInt32(cbPlanSemanal.GetSelectedValue());

                //Se busca el Plan Semanal
                BLL.DiasPlanSemanalBLL.obtenerDias(dsPlanSemanal.DIAS_PLAN_SEMANAL, codigo);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaPlanSemanal.Table = dsPlanSemanal.DIAS_PLAN_SEMANAL;

                if (dsPlanSemanal.DIAS_PLAN_SEMANAL.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Días para la semama ingresada.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Semanal - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }


#endregion

        

       



    }
}
