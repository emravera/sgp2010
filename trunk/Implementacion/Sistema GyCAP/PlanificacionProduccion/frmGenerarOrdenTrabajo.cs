﻿using System;
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
        public static readonly int estadoInicialNuevoAutmatico = 1; //Para generar las OT de forma automática
        public static readonly int estadoInicialNuevoManual = 2; //Para generar OT de forma manual
        Data.dsPlanSemanal dsPlanSemanal = new GyCAP.Data.dsPlanSemanal();
        Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
        Data.dsOrdenTrabajo dsOrdenTrabajo = new GyCAP.Data.dsOrdenTrabajo();
        Data.dsHojaRuta dsHojaRuta = new GyCAP.Data.dsHojaRuta();
        DataView dvPlanAnual, dvMensual, dvPlanSemanal, dvOrdenTrabajo, dvHojaRuta, dvEstructura;
        private int columnIndex = -1;

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
                        CargarPlanSemanal(cbSemanaBuscar.GetSelectedValueInt());
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

        #region Generar órdenes

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            tipoNodo seleccion = (tipoNodo)tvDetallePlan.SelectedNode.Tag;
            int codigo;
            Application.UseWaitCursor = true;
            try
            {
                switch (seleccion)
                {
                    case tipoNodo.anio:
                        break;
                    case tipoNodo.mes:
                        break;
                    case tipoNodo.semana:
                        BLL.OrdenTrabajoBLL.GenerarOrdenTrabajoSemana(Convert.ToInt32(tvDetallePlan.SelectedNode.Name), dsPlanSemanal, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                        break;
                    case tipoNodo.dia:
                        break;
                    case tipoNodo.detalleDia:
                        break;
                    default:
                        break;
                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex) { Application.UseWaitCursor = false; MessageBox.Show(ex.Message); }
            catch (Entidades.Excepciones.OrdenTrabajoException ex) { Application.UseWaitCursor = false; MessageBox.Show(ex.Message); }
            Application.UseWaitCursor = false;
            dvOrdenTrabajo.Table = dsOrdenTrabajo.ORDENES_TRABAJO;
            codigo = 0;
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
                    
                    estadoInterface = estadoUI.nuevoManual;
                    tcOrdenTrabajo.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:
                case estadoUI.modificar:
                    
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnConsultar.Enabled = false;
                    
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
            dgvListaOrdenTrabajo.AutoGenerateColumns = false;
            dgvListaOrdenTrabajo.Columns.Add("ORD_CODIGO", "Código");
            dgvListaOrdenTrabajo.Columns.Add("ORD_FECHAALTA", "Fecha creación");
            dgvListaOrdenTrabajo.Columns.Add("ORD_ORIGEN", "Origen");
            dgvListaOrdenTrabajo.Columns.Add("COCINA", "Cocina");
            dgvListaOrdenTrabajo.Columns.Add("CANTIDAD", "Cantidad");
            dgvListaOrdenTrabajo.Columns.Add("ORD_FECHAINICIOESTIMADA", "Fecha inicio");
            dgvListaOrdenTrabajo.Columns.Add("ORD_FECHAFINESTIMADA", "Fecha fin");
            dgvListaOrdenTrabajo.Columns.Add("EORD_CODIGO", "Estado");
            dgvListaOrdenTrabajo.Columns["ORD_CODIGO"].DataPropertyName = "ORD_CODIGO";
            dgvListaOrdenTrabajo.Columns["ORD_FECHAALTA"].DataPropertyName = "ORD_FECHAALTA";
            dgvListaOrdenTrabajo.Columns["ORD_ORIGEN"].DataPropertyName = "ORD_ORIGEN";
            dgvListaOrdenTrabajo.Columns["COCINA"].DataPropertyName = "DPSEM_CODIGO";
            dgvListaOrdenTrabajo.Columns["CANTIDAD"].DataPropertyName = "DPSEM_CODIGO";
            dgvListaOrdenTrabajo.Columns["ORD_FECHAINICIOESTIMADA"].DataPropertyName = "ORD_FECHAINICIOESTIMADA";
            dgvListaOrdenTrabajo.Columns["ORD_FECHAFINESTIMADA"].DataPropertyName = "ORD_FECHAFINESTIMADA";
            dgvListaOrdenTrabajo.Columns["EORD_CODIGO"].DataPropertyName = "EORD_CODIGO";
            dgvListaOrdenTrabajo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvListaOrdenTrabajo.AllowUserToResizeColumns = true;
            dgvListaOrdenTrabajo.Columns["ORD_FECHAALTA"].MinimumWidth = 110;
            dgvListaOrdenTrabajo.Columns["ORD_FECHAINICIOESTIMADA"].MinimumWidth = 80;
            dgvListaOrdenTrabajo.Columns["ORD_FECHAFINESTIMADA"].MinimumWidth = 80;
            dgvListaOrdenTrabajo.Columns["ORD_FECHAALTA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaOrdenTrabajo.Columns["ORD_FECHAINICIOESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaOrdenTrabajo.Columns["ORD_FECHAFINESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaOrdenTrabajo.Columns["CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

            //Dataviews
            dvPlanAnual = new DataView(dsPlanSemanal.PLANES_ANUALES);
            dvMensual = new DataView(dsPlanSemanal.PLANES_MENSUALES);
            dvPlanSemanal = new DataView(dsPlanSemanal.PLANES_SEMANALES);
            dvOrdenTrabajo = new DataView(dsOrdenTrabajo.ORDENES_TRABAJO);
            dgvListaOrdenTrabajo.DataSource = dvOrdenTrabajo;

            try
            {
                BLL.PlanAnualBLL.ObtenerTodos(dsPlanSemanal.PLANES_ANUALES);
                BLL.CocinaBLL.ObtenerCocinas(dsPlanSemanal.COCINAS);
                BLL.EstadoOrdenTrabajoBLL.ObtenerEstadosOrden(dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO);                
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Generar Orden Trabajo - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cbAnioBuscar.SetDatos(dvPlanAnual, "PAN_CODIGO", "PAN_ANIO", "Seleccione", false);
        }

        private void CargarPlanSemanal(int codigoSemana)
        {
            tvDetallePlan.Nodes.Clear();
            tvDetallePlan.BeginUpdate();
            
            TreeNode nodoSemana = new TreeNode();
            nodoSemana.Name = dsPlanSemanal.PLANES_SEMANALES.FindByPSEM_CODIGO(codigoSemana).PSEM_CODIGO.ToString();
            nodoSemana.Text = "Semana " + dsPlanSemanal.PLANES_SEMANALES.FindByPSEM_CODIGO(codigoSemana).PSEM_SEMANA.ToString();
            nodoSemana.Tag = tipoNodo.semana;

            tvDetallePlan.Nodes.Add(nodoSemana);
            Sistema.FuncionesAuxiliares.QuitarCheckBox(nodoSemana, tvDetallePlan);

            foreach (Data.dsPlanSemanal.DIAS_PLAN_SEMANALRow rowDia in dsPlanSemanal.PLANES_SEMANALES.FindByPSEM_CODIGO(codigoSemana).GetDIAS_PLAN_SEMANALRows())
            {
                TreeNode nodoDia = new TreeNode();
                nodoDia.Name = rowDia.DIAPSEM_CODIGO.ToString();
                nodoDia.Text = rowDia.DIAPSEM_DIA + " - " + rowDia.DIAPSEM_FECHA.ToShortDateString();
                nodoDia.Tag = tipoNodo.dia;
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

            tvDetallePlan.EndUpdate();
            tvDetallePlan.ExpandAll();
        }

        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point punto = new Point((sender as Button).Location.X + 2, (sender as Button).Location.Y + 2);
                (sender as Button).Location = punto;
            }
        }

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point punto = new Point((sender as Button).Location.X - 2, (sender as Button).Location.Y - 2);
                (sender as Button).Location = punto;
            }
        }

        private void dgvListaOrdenTrabajo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != string.Empty)
            {
                string nombre = string.Empty;
                switch (dgvListaOrdenTrabajo.Columns[e.ColumnIndex].Name)
                {
                    case "ORD_FECHAALTA":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "COCINA":
                        nombre = dsPlanSemanal.DETALLE_PLANES_SEMANALES.FindByDPSEM_CODIGO(Convert.ToInt32(e.Value.ToString())).COCINASRow.COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    case "ORD_FECHAINICIOESTIMADA":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "ORD_FECHAFINESTIMADA":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "EORD_CODIGO":
                        nombre = dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.FindByEORD_CODIGO(Convert.ToInt32(e.Value.ToString())).EORD_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "CANTIDAD":
                        nombre = dsPlanSemanal.DETALLE_PLANES_SEMANALES.FindByDPSEM_CODIGO(Convert.ToInt32(e.Value.ToString())).DPSEM_CANTIDADESTIMADA.ToString();
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        #region Menú bloquear columnas
        private void tsmiBloquearColumna_Click(object sender, EventArgs e)
        {
            if (columnIndex != -1) { dgvListaOrdenTrabajo.Columns[columnIndex].Frozen = true; }
        }

        private void tsmiDesbloquearColumna_Click(object sender, EventArgs e)
        {
            if (columnIndex != -1) { dgvListaOrdenTrabajo.Columns[columnIndex].Frozen = false; }
        }

        private void dgvListaOrdenTrabajo_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                columnIndex = e.ColumnIndex;
                if (dgvListaOrdenTrabajo.Columns[columnIndex].Frozen)
                {
                    tsmiBloquearColumna.Checked = true;
                    tsmiDesbloquearColumna.Checked = false;
                }
                else
                {
                    tsmiBloquearColumna.Checked = false;
                    tsmiDesbloquearColumna.Checked = true;
                }
                cmsGrillaOrdenesTrabajo.Show(MousePosition);
            }
        }
        #endregion

        #endregion

    }
}
