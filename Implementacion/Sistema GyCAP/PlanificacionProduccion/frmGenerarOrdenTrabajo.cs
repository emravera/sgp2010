using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GyCAP.UI.PlanificacionProduccion
{
    public partial class frmGenerarOrdenTrabajo : Form
    {        
        private static frmGenerarOrdenTrabajo _frmGenerarOrdenTrabajo = null;
        private enum estadoUI { nuevoAutomatico, nuevoManual, consultar, modificar };
        private estadoUI estadoInterface;
        private enum tipoNodo { anio, mes, semana, dia, detalleDia };
        public static readonly int estadoInicialNuevoAutmatico = 1; //Para generar las OT de fomra automática
        public static readonly int estadoInicialNuevoManual = 2; //Para generar OT de forma manual
        Data.dsPlanSemanal dsPlanSemanal = new GyCAP.Data.dsPlanSemanal();
        Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
        DataView dvPlanAnual, dvMensual, dvPlanSemanal, dvOrdenTrabajo;

        #region Inicio

        public frmGenerarOrdenTrabajo()
        {
            InitializeComponent();
            InicializarDatos();
            SetInterface(estadoUI.nuevoAutomatico);
        }

        public static frmGenerarOrdenTrabajo Instancia
        {
            get
            {
                if (_frmGenerarOrdenTrabajo == null || _frmGenerarOrdenTrabajo.IsDisposed)
                {
                    _frmGenerarOrdenTrabajo = new frmGenerarOrdenTrabajo();
                }
                else
                {
                    _frmGenerarOrdenTrabajo.BringToFront();
                }
                return _frmGenerarOrdenTrabajo;
            }
            set
            {
                _frmGenerarOrdenTrabajo = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevoAutmatico) { SetInterface(estadoUI.nuevoAutomatico); }
            if (estado == estadoInicialNuevoManual) { SetInterface(estadoUI.nuevoManual); }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        #endregion

        #region Búsqueda

        private void cbAnioBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAnioBuscar.GetSelectedIndex() != -1)
            {
                dsPlanSemanal.PLANES_MENSUALES.Clear();
                try
                {
                    BLL.PlanMensualBLL.ObtenerPMAnio(dsPlanSemanal.PLANES_MENSUALES, cbAnioBuscar.GetSelectedValueInt());
                    cbMesBuscar.SetDatos(dvMensual, "PMES_CODIGO", "PMES_MES", "Seleccione", false);
                }
                catch (Entidades.Excepciones.BaseDeDatosException ex)
                {
                    MessageBox.Show(ex.Message, "Error: Generar Orden Trabajo - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbMesBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMesBuscar.GetSelectedIndex() != -1)
            {
                dsPlanSemanal.PLANES_SEMANALES.Clear();
                try
                {
                    BLL.PlanSemanalBLL.obtenerPS(dsPlanSemanal.PLANES_SEMANALES, cbMesBuscar.GetSelectedValueInt());
                    cbSemanaBuscar.SetDatos(dvPlanSemanal, "PSEM_CODIGO", "PSEM_SEMANA", "Seleccione", false);
                }
                catch (Entidades.Excepciones.BaseDeDatosException ex)
                {
                    MessageBox.Show(ex.Message, "Error: Generar Orden Trabajo - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dsPlanSemanal.DIAS_PLAN_SEMANAL.Clear();
            dsPlanSemanal.DETALLE_PLANES_SEMANALES.Clear();

            if ( cbSemanaBuscar.GetSelectedIndex() != -1)
            {
                try
                {
                    BLL.DiasPlanSemanalBLL.obtenerDias(dsPlanSemanal.DIAS_PLAN_SEMANAL, cbSemanaBuscar.GetSelectedValueInt());
                    if (dsPlanSemanal.DIAS_PLAN_SEMANAL.Rows.Count > 0)
                    {
                        foreach (Data.dsPlanSemanal.DIAS_PLAN_SEMANALRow rowDia in dsPlanSemanal.DIAS_PLAN_SEMANAL)
                        {
                            BLL.DetallePlanSemanalBLL.ObtenerDetalle(Convert.ToInt32(rowDia.DIAPSEM_CODIGO), dsPlanSemanal.DETALLE_PLANES_SEMANALES);
                        }
                        CargarPlan(cbMesBuscar.GetSelectedValueInt());
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron Planes de Producción para la semana seleccionada.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Entidades.Excepciones.BaseDeDatosException ex)
                {
                    MessageBox.Show(ex.Message, "Error: Generar Orden Trabajo - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Semana de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Servicios

        //Setea la pantalla de acuerdo al estado en que se encuentre
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.nuevoAutomatico:
                   
                    estadoInterface = estadoUI.nuevoAutomatico;
                    tcOrdenTrabajo.SelectedTab = tpBuscar;
                    break;
                case estadoUI.nuevoManual:
                    
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevoManual;
                    tcOrdenTrabajo.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:
                case estadoUI.modificar:
                    
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcOrdenTrabajo.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }

        private void InicializarDatos()
        {
            //Grilla ordenes trabajo
            dgvListaOrdenTrbajo.Columns.Add("ORD_CODIGO", "Código");
            dgvListaOrdenTrbajo.Columns.Add("CTO_CODIGO", "Centro de trabajo");
            dgvListaOrdenTrbajo.Columns.Add("PAR_CODIGO", "Parte");
            dgvListaOrdenTrbajo.Columns.Add("ORD_CANTIDADESTIMADA", "Cantidad");
            dgvListaOrdenTrbajo.Columns.Add("UMED_CODIGO", "Unidad medida");
            dgvListaOrdenTrbajo.Columns.Add("ORD_FECHAINICIOESTIMADA", "Fecha inicio");
            dgvListaOrdenTrbajo.Columns.Add("ORD_FECHAFINESTIMADA", "Fecha fin");
            dgvListaOrdenTrbajo.Columns["ORD_CODIGO"].DataPropertyName = "ORD_CODIGO";
            dgvListaOrdenTrbajo.Columns["CTO_CODIGO"].DataPropertyName = "CTO_CODIGO";
            dgvListaOrdenTrbajo.Columns["PAR_CODIGO"].DataPropertyName = "PAR_CODIGO";
            dgvListaOrdenTrbajo.Columns["ORD_CANTIDADESTIMADA"].DataPropertyName = "ORD_CANTIDADESTIMADA";
            dgvListaOrdenTrbajo.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";
            dgvListaOrdenTrbajo.Columns["ORD_FECHAINICIOESTIMADA"].DataPropertyName = "ORD_FECHAINICIOESTIMADA";
            dgvListaOrdenTrbajo.Columns["ORD_FECHAFINESTIMADA"].DataPropertyName = "ORD_FECHAFINESTIMADA";
            dgvListaOrdenTrbajo.Columns["ORD_CANTIDADESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvListaOrdenTrbajo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvListaOrdenTrbajo.AllowUserToResizeColumns = true;            

            //Dataviews
            dvPlanAnual = new DataView(dsPlanSemanal.PLANES_ANUALES);
            dvMensual = new DataView(dsPlanSemanal.PLANES_MENSUALES);
            dvPlanSemanal = new DataView(dsPlanSemanal.PLANES_SEMANALES);
            
            dgvListaOrdenTrbajo.DataSource = dvOrdenTrabajo;

            try
            {
                    BLL.PlanAnualBLL.ObtenerTodos(dsPlanSemanal.PLANES_ANUALES);
                    BLL.CocinaBLL.ObtenerCocinas(dsPlanSemanal.COCINAS);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Generar Orden Trabajo - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cbAnioBuscar.SetDatos(dvPlanAnual, "PAN_CODIGO", "PAN_ANIO", "Seleccione", false);
        }

        private void CargarPlan(int codigoMes)
        {
            foreach (Data.dsPlanSemanal.PLANES_MENSUALESRow rowMes in (Data.dsPlanSemanal.PLANES_MENSUALESRow[])dsPlanSemanal.PLANES_MENSUALES.Select("PMES_CODIGO = " + codigoMes))
            {
                tvDetallePlan.Nodes.Clear();
                tvDetallePlan.BeginUpdate();
                TreeNode nodoMes = new TreeNode();
                nodoMes.Name = rowMes.PMES_CODIGO.ToString();
                nodoMes.Text = rowMes.PMES_MES;
                nodoMes.Tag = tipoNodo.mes;
                tvDetallePlan.Nodes.Add(nodoMes);
                Sistema.FuncionesAuxiliares.QuitarCheckBox(nodoMes, tvDetallePlan); 

                foreach (Data.dsPlanSemanal.PLANES_SEMANALESRow rowSemana in rowMes.GetPLANES_SEMANALESRows())
                {
                    TreeNode nodoSemana = new TreeNode();
                    nodoSemana.Name = rowSemana.PMES_CODIGO.ToString();
                    nodoSemana.Text = "Semana " + rowSemana.PSEM_SEMANA.ToString();
                    nodoSemana.Tag = tipoNodo.semana;
                    
                    nodoMes.Nodes.Add(nodoSemana);

                    foreach (Data.dsPlanSemanal.DIAS_PLAN_SEMANALRow rowDia in rowSemana.GetDIAS_PLAN_SEMANALRows())
                    {
                        TreeNode nodoDia = new TreeNode();
                        nodoDia.Name = rowDia.DIAPSEM_CODIGO.ToString();
                        nodoDia.Text = rowDia.DIAPSEM_DIA + " - " + rowDia.DIAPSEM_FECHA.ToShortDateString();
                        nodoSemana.Tag = tipoNodo.dia;
                        nodoSemana.Nodes.Add(nodoDia);

                        foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow rowDetalle in rowDia.GetDETALLE_PLANES_SEMANALESRows())
                        {
                            TreeNode nodoDetalle = new TreeNode();
                            nodoDetalle.Name = rowDetalle.DIAPSEM_CODIGO.ToString();
                            nodoDetalle.Text = "Cocina: " + rowDetalle.COCINASRow.COC_CODIGO_PRODUCTO + " - Cantidad: " + rowDetalle.DPSEM_CANTIDADESTIMADA.ToString();
                            nodoDetalle.Tag = tipoNodo.detalleDia;
                            nodoDia.Nodes.Add(nodoDetalle);
                        }
                    }
                }

                tvDetallePlan.EndUpdate();
                tvDetallePlan.ExpandAll();
            }
        }

        #endregion

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            /*tipoNodo seleccion = (tipoNodo)tvDetallePlan.SelectedNode.Tag;
            int codigo;

            switch (seleccion)
            {
                case tipoNodo.anio:
                    break;
                case tipoNodo.mes:
                    break;
                case tipoNodo.semana:
                    break;
                case tipoNodo.dia:
                    break;
                case tipoNodo.detalleDia:
                    break;
                default:
                    break;
            }*/

            
        }       

    }
}
