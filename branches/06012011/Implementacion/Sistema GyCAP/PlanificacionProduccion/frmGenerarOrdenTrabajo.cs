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
        private static frmGenerarOrdenTrabajo _frmGenerarOrdenTrabajoA = null;
        private static frmGenerarOrdenTrabajo _frmGenerarOrdenTrabajoM = null;
        private enum estadoUI { nuevoAutomatico, nuevoManual };
        private estadoUI estadoInterface;
        private enum tipoNodo { anio, mes, semana, dia, detalleDia };
        public static readonly int estadoInicialNuevoAutomatico = 1; //Para generar las OT de forma automática
        public static readonly int estadoInicialNuevoManual = 2; //Para generar OT de forma manual
        Data.dsPlanSemanal dsPlanSemanal = new GyCAP.Data.dsPlanSemanal();
        //Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
        Data.dsOrdenTrabajo dsOrdenTrabajo = new GyCAP.Data.dsOrdenTrabajo();
        Data.dsHojaRuta dsHojaRuta = new GyCAP.Data.dsHojaRuta();
        DataView dvPlanAnual, dvMensual, dvPlanSemanal, dvOrdenProduccion, dvOrdenTrabajo, dvStockDestino;
        private int columnIndex = -1;
        BindingSource sourceOrdenTrabajo = new BindingSource();
        private Sistema.ControlesUsuarios.AnimadorFormulario animador = new GyCAP.UI.Sistema.ControlesUsuarios.AnimadorFormulario();
        private TreeView tvDependenciaSimple, tvDependenciaCompleta, tvOrdenesYEstructura;
        Label lblMensajeOP, lblMensajeOT;
        private CheckBox chkSeleccionarOP = new CheckBox();

        #region Inicio

        public frmGenerarOrdenTrabajo()
        {
            InitializeComponent();
            InicializarDatos();
        }

        public static frmGenerarOrdenTrabajo InstanciaAutomatica
        {
            get
            {
                if (_frmGenerarOrdenTrabajoA == null || _frmGenerarOrdenTrabajoA.IsDisposed)
                {
                    _frmGenerarOrdenTrabajoA = new frmGenerarOrdenTrabajo();
                }
                else
                {
                    _frmGenerarOrdenTrabajoA.BringToFront();
                }
                return _frmGenerarOrdenTrabajoA;
            }
            set
            {
                _frmGenerarOrdenTrabajoA = value;
            }
        }

        public static frmGenerarOrdenTrabajo InstanciaManual
        {
            get
            {
                if (_frmGenerarOrdenTrabajoM == null || _frmGenerarOrdenTrabajoM.IsDisposed)
                {
                    _frmGenerarOrdenTrabajoM = new frmGenerarOrdenTrabajo();
                }
                else
                {
                    _frmGenerarOrdenTrabajoM.BringToFront();
                }
                return _frmGenerarOrdenTrabajoM;
            }
            set
            {
                _frmGenerarOrdenTrabajoM = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevoAutomatico) 
            {
                tcOrdenTrabajo.TabPages.Remove(tpManual);
                this.Text = "Generar Orden de Trabajo Automática";
                estadoInterface = estadoUI.nuevoAutomatico;
            }
            if (estado == estadoInicialNuevoManual) 
            {
                tcOrdenTrabajo.TabPages.Remove(tpAutomatico);
                this.Text = "Generar Orden de Trabajo Manual";
                estadoInterface = estadoUI.nuevoManual;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose(true);
        }

        #endregion

        #region Buscar

        private void cbAnioBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAnioBuscar.GetSelectedIndex() != -1)
            {
                dsPlanSemanal.PLANES_MENSUALES.Clear();
                try
                {
                    BLL.PlanMensualBLL.ObtenerPMAnio(dsPlanSemanal.PLANES_MENSUALES, cbAnioBuscar.GetSelectedValueInt());
                    cbMesBuscar.SetDatos(dvMensual, "PMES_CODIGO", "PMES_MES", "PMES_CODIGO ASC", "Seleccione", false);
                    tvDetallePlan.Nodes.Clear();
                    dsOrdenTrabajo.ORDENES_PRODUCCION.Clear();
                    dsOrdenTrabajo.ORDENES_TRABAJO.Clear();
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
                tvDetallePlan.Nodes.Clear();
                dsOrdenTrabajo.ORDENES_PRODUCCION.Clear();
                dsOrdenTrabajo.ORDENES_TRABAJO.Clear();
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

        private void cbSemanaBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSemanaBuscar.GetSelectedIndex() != -1)
            {
                tvDetallePlan.Nodes.Clear();
                dsOrdenTrabajo.ORDENES_PRODUCCION.Clear();
                dsOrdenTrabajo.ORDENES_TRABAJO.Clear();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dsPlanSemanal.DIAS_PLAN_SEMANAL.Clear();
            dsPlanSemanal.DETALLE_PLANES_SEMANALES.Clear();
            dsOrdenTrabajo.ORDENES_PRODUCCION.Clear();
            dsOrdenTrabajo.ORDENES_PRODUCCION_MANUAL.Clear();
            dsOrdenTrabajo.ORDENES_TRABAJO.Clear();
            chkSeleccionarOP.Checked = false;

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

        private void tvDetallePlan_AfterCheck(object sender, TreeViewEventArgs e)
        {
            tipoNodo tipo = (tipoNodo)e.Node.Tag;
            if (tipo == tipoNodo.semana)
            {
                foreach (TreeNode nodo in e.Node.Nodes)
                {
                    nodo.Checked = e.Node.Checked;
                }
            }
        }

        #endregion

        #region Pestaña Generar Automaticamente

        private void btnGenerarOrdenP_Click(object sender, EventArgs e)
        {
            int generados = 0;
            foreach (TreeNode nodoDia in tvDetallePlan.Nodes[0].Nodes)
            {
                tipoNodo tipo = (tipoNodo)nodoDia.Tag;
                if (tipo == tipoNodo.dia && nodoDia.Checked)
                {
                    try
                    {
                        BLL.OrdenProduccionBLL.GenerarOrdenProduccionDia(Convert.ToInt32(nodoDia.Name), dsPlanSemanal, dsOrdenTrabajo);
                        foreach (TreeNode nodoDetalleDia in nodoDia.Nodes)
                        {
                            nodoDetalleDia.ForeColor = Color.Black;
                        }
                        generados++;
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex) { MessageBox.Show(ex.Message); }
                    catch (Entidades.Excepciones.OrdenTrabajoException ex) { MessageBox.Show(ex.Message); }
                }
            }

            if (generados == 0) { MessageBox.Show("Debe seleccionar la semana o un día.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnSubirPrioridad_Click(object sender, EventArgs e)
        {
            if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
            {
                int codOrdenP = Convert.ToInt32(dvOrdenProduccion[dgvListaOrdenProduccion.SelectedRows[0].Index]["ordp_numero"].ToString());
                dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).ORDP_PRIORIDAD += 1;
            }
            else
            {
                MessageBox.Show("Debe seleccionar una orden de producción de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBajarPrioridad_Click(object sender, EventArgs e)
        {
            if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
            {
                int codOrdenP = Convert.ToInt32(dvOrdenProduccion[dgvListaOrdenProduccion.SelectedRows[0].Index]["ordp_numero"].ToString());
                if (dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).ORDP_PRIORIDAD > 0)
                {
                    dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).ORDP_PRIORIDAD -= 1;
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una orden de producción de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAsignarCantidad_Click(object sender, EventArgs e)
        {
            if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
            {
                int codOrdenP = Convert.ToInt32(dvOrdenProduccion[dgvListaOrdenProduccion.SelectedRows[0].Index]["ordp_numero"].ToString());
                dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).ORDP_CANTIDADESTIMADA = nudCantidadOrdenP.Value;
                txtCantidadOrdenP.Text = nudCantidadOrdenP.Value.ToString();
                nudCantidadOrdenP.Value = 0;                
            }
            else
            {
                MessageBox.Show("Debe seleccionar una orden de producción de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGenerarOrdenT_Click(object sender, EventArgs e)
        {
            int cantidad = 0;
            foreach (DataGridViewRow fila in dgvListaOrdenProduccion.Rows)
            {
                DataGridViewCheckBoxCell cellSelecion = fila.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(cellSelecion.FormattedValue))
                {
                    int codigoOrdenP = Convert.ToInt32(dvOrdenProduccion[fila.Index]["ordp_numero"].ToString());
                    //BLL.OrdenTrabajoBLL.GenerarOrdenesTrabajo(codigoOrdenP, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                    cantidad++;
                }
            }

            if (cantidad == 0) { MessageBox.Show("Debe seleccionar una o más Órdenes de Producción.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        #endregion

        #region Pestaña Generar Manualmente

        #endregion

        #region Pestaña Orden Producción

        private void btnCalcularFechas_Click(object sender, EventArgs e)
        {
            if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
            {
                string mensaje = string.Empty;
                if (cbModoFecha.GetSelectedIndex() == -1) { mensaje = "- Modo de planeación"; }
                if (dtpFechaPlanear.EsFechaNull()) { mensaje = "- Modo de inicio"; }
                if (string.IsNullOrEmpty(mensaje))
                {
                    int codigoP = Convert.ToInt32(dvOrdenProduccion[dgvListaOrdenProduccion.SelectedRows[0].Index]["ordp_numero"].ToString());
                    tvDependenciaSimple = frmArbolOrdenesTrabajo.Instancia.GetArbolDependenciaSimple();
                    tvDependenciaCompleta = frmArbolOrdenesTrabajo.Instancia.GetArbolDependenciaCompleta();
                    tvOrdenesYEstructura = frmArbolOrdenesTrabajo.Instancia.GetArbolOrdenesYEstructura();
                    BLL.OrdenProduccionBLL.GenerarArbolOrdenesTrabajo(codigoP, tvDependenciaSimple, tvDependenciaCompleta, tvOrdenesYEstructura, dsOrdenTrabajo);
                    if (cbModoFecha.GetSelectedValueInt() == 0)
                    {
                        //Planeamos hacia adelante
                        DateTime fecha = DateTime.Parse(DateTime.Parse(dtpFechaPlanear.GetFecha().ToString()).ToShortDateString());
                        //BLL.OrdenProduccionBLL.PlanearFechaHaciaDelante(codigoP, fecha, tvDependenciaCompleta, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                        dvOrdenTrabajo.Table = dsOrdenTrabajo.ORDENES_TRABAJO;
                        sourceOrdenTrabajo.DataSource = dvOrdenTrabajo;
                        CompletarDatosOrdenTrabajo();
                    }
                    else
                    {
                        //Planeamos hacia atrás
                        DateTime fecha = DateTime.Parse(DateTime.Parse(dtpFechaPlanear.GetFecha().ToString()).ToShortDateString());
                        //BLL.OrdenProduccionBLL.PlanearFechaHaciaAtras(codigoP, fecha, tvDependenciaCompleta, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                        dvOrdenTrabajo.Table = dsOrdenTrabajo.ORDENES_TRABAJO;
                        sourceOrdenTrabajo.DataSource = dvOrdenTrabajo;
                        CompletarDatosOrdenTrabajo();
                    }

                    txtFechaInicioOrdenP.Text = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoP).ORDP_FECHAINICIOESTIMADA.ToShortDateString();
                    txtFechaFinOrdenP.Text = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigoP).ORDP_FECHAFINESTIMADA.ToShortDateString();
                }
                else { MessageBox.Show("Debe seleccionar:" + mensaje, "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            else { MessageBox.Show("Debe seleccionar una Orden de Producción de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnAplicarCambios_Click(object sender, EventArgs e)
        {
            if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
            {
                //ver si no existe en la DB, en ese caso actualizar - gonzalo
                int codOrdenP = Convert.ToInt32(dvOrdenProduccion[dgvListaOrdenProduccion.SelectedRows[0].Index]["ordp_numero"].ToString());
                dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).ORDP_CODIGO = txtCodigoOrdenP.Text;
                dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).ORDP_ORIGEN = txtOrigenOrdenP.Text;
                dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).ORDP_OBSERVACIONES = txtObservacionesOrdenP.Text;
                if (cboStockDestino.GetSelectedIndex() != -1) { dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).USTCK_DESTINO = cboStockDestino.GetSelectedValueInt(); }
                else { dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).SetUSTCK_DESTINONull(); }
                //MessageBox.Show("Debe seleccionar una orden de producción de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminarActual_Click(object sender, EventArgs e)
        {
            if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
            {
                int codOrdenP = Convert.ToInt32(dvOrdenProduccion[dgvListaOrdenProduccion.SelectedRows[0].Index]["ordp_numero"].ToString());
                bool eliminar = true;
                if (codOrdenP > 0)
                {
                    try
                    {
                        BLL.OrdenProduccionBLL.Eliminar(codOrdenP);
                        BLL.DetallePlanSemanalBLL.ActualizarEstado(codOrdenP, BLL.DetallePlanSemanalBLL.estadoGenerado);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        eliminar = false;
                        MessageBox.Show(ex.Message, "Error: Generar Orden de Producción - Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (eliminar)
                {
                    foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOT in dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).GetORDENES_TRABAJORows())
                    {
                        rowOT.Delete();
                    }
                    dsPlanSemanal.DETALLE_PLANES_SEMANALES.FindByDPSEM_CODIGO(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).DPSEM_CODIGO).DPSEM_ESTADO = BLL.DetallePlanSemanalBLL.estadoGenerado;
                    tvDetallePlan.Nodes.Find(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).DPSEM_CODIGO.ToString(), true)[0].ForeColor = Color.Red;
                    dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).Delete();
                    MessageBox.Show("La Orden de Producción ha sido eliminada correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarControles();
                }
            }
        }

        private void btnEliminarTodas_Click(object sender, EventArgs e)
        {
            bool eliminar;
            string mensaje = string.Empty;
            IList<decimal> opeliminar = new List<decimal>();
            foreach (Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow rowOP in dsOrdenTrabajo.ORDENES_PRODUCCION.Rows)
            {
                eliminar = true;
                if (rowOP.ORDP_NUMERO > 0)
                {
                    try
                    {
                        BLL.OrdenProduccionBLL.Eliminar(Convert.ToInt32(rowOP.ORDP_NUMERO));
                        BLL.DetallePlanSemanalBLL.ActualizarEstado(Convert.ToInt32(rowOP.DPSEM_CODIGO), BLL.DetallePlanSemanalBLL.estadoGenerado);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException)
                    {
                        eliminar = false;
                        mensaje += "\n- " + rowOP.ORDP_CODIGO;
                    }
                }

                if (eliminar)
                {
                    foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOT in rowOP.GetORDENES_TRABAJORows())
                    {
                        rowOT.Delete();
                    }
                    dsPlanSemanal.DETALLE_PLANES_SEMANALES.FindByDPSEM_CODIGO(rowOP.DPSEM_CODIGO).DPSEM_ESTADO = BLL.DetallePlanSemanalBLL.estadoGenerado;
                    tvDetallePlan.Nodes.Find(rowOP.DPSEM_CODIGO.ToString(), true)[0].ForeColor = Color.Red;
                    opeliminar.Add(rowOP.ORDP_NUMERO);
                }
            }
            foreach (decimal codigo in opeliminar)
            {
                dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigo).Delete();
            }
            
            if (string.IsNullOrEmpty(mensaje))
            {
                MessageBox.Show("Las Órdenes de Producción se eliminaron correctamente.", "Generar Orden de Producción - Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarControles();
            }
            else
            {
                MessageBox.Show("Error al eliminar las siguientes Órdenes de Producción:" + mensaje, "Generar Orden de Producción - Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpiarControles();
            }
        }

        private void btnGuardarActual_Click(object sender, EventArgs e)
        {
            if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
            {
                int codOrdenP = Convert.ToInt32(dvOrdenProduccion[dgvListaOrdenProduccion.SelectedRows[0].Index]["ordp_numero"].ToString());
                string datosOK = string.Empty;
                if (dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).IsORDP_FECHAINICIOESTIMADANull()) { datosOK = "\n* Fechas"; }
                if (dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).IsUSTCK_DESTINONull()) { datosOK += "\n* Stock destino"; }
                if(datosOK == string.Empty) 
                { 
                    GuardarOrden(codOrdenP);
                    MessageBox.Show("La Orden de Producción se ha guardado correctamente.", "Información: Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else { MessageBox.Show("Debe completar los datos:\n\n" + datosOK, "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }
            else { MessageBox.Show("Debe seleccionar una Orden de Producción de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnGuardarTodo_Click(object sender, EventArgs e)
        {
            string datosOK = string.Empty;
            foreach (Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow row in dsOrdenTrabajo.ORDENES_PRODUCCION.Rows)
            {                
                if (row.IsORDP_FECHAINICIOESTIMADANull()) { datosOK += "\n* Fechas de Orden de Producción " + row.ORDP_CODIGO; };
                if (row.IsUSTCK_DESTINONull()) { datosOK += "\n* Stock destino de Orden de Producción" + row.ORDP_CODIGO; };                
            }
            
            if(datosOK == string.Empty)
            {
                foreach (Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow row in dsOrdenTrabajo.ORDENES_PRODUCCION.Rows)
                {
                    GuardarOrden(Convert.ToInt32(row.ORDP_NUMERO));
                }
                MessageBox.Show("Las Órdenes de Producción se han guardado correctamente.", "Información: Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else { MessageBox.Show("Debe completar los datos:\n\n" + datosOK, "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void GuardarOrden(int numeroOrdenProduccion)
        {
            try
            {
                if (dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_NUMERO < 0)
                {
                    //No se ha guardado todavía la orden de producción
                    int codigoPlan = Convert.ToInt32(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).DPSEM_CODIGO);
                    BLL.OrdenProduccionBLL.Insertar(numeroOrdenProduccion, dsOrdenTrabajo);
                    BLL.DetallePlanSemanalBLL.ActualizarEstado(codigoPlan, BLL.DetallePlanSemanalBLL.estadoConOrden);
                    dsPlanSemanal.DETALLE_PLANES_SEMANALES.FindByDPSEM_CODIGO(codigoPlan).DPSEM_ESTADO = BLL.DetallePlanSemanalBLL.estadoConOrden;
                }
                else
                {
                    //Ya se había guardado la orden de producción - actualizar - gonzalo
                }                
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Generar Orden de Producción - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        #endregion

        #region Pestaña Orden Trabajo
        
        private void bnMoveNextItem_Click(object sender, EventArgs e)
        {
            CompletarDatosOrdenTrabajo();
            if (animador.EsVisible()) 
            {
                Data.dsOrdenTrabajo.ORDENES_TRABAJORow row = (Data.dsOrdenTrabajo.ORDENES_TRABAJORow)(sourceOrdenTrabajo.Current as DataRowView).Row;
                frmArbolOrdenesTrabajo.Instancia.SeleccionarOrdenTrabajo(Convert.ToInt32(row.ORDT_NUMERO));
            }
        }

        private void bnMovePreviousItem_Click(object sender, EventArgs e)
        {
            CompletarDatosOrdenTrabajo();
            if (animador.EsVisible())
            {
                Data.dsOrdenTrabajo.ORDENES_TRABAJORow row = (Data.dsOrdenTrabajo.ORDENES_TRABAJORow)(sourceOrdenTrabajo.Current as DataRowView).Row;
                frmArbolOrdenesTrabajo.Instancia.SeleccionarOrdenTrabajo(Convert.ToInt32(row.ORDT_NUMERO));
            }
        }

        private void bnMoveFirstItem_Click(object sender, EventArgs e)
        {
            CompletarDatosOrdenTrabajo();
            if (animador.EsVisible())
            {
                Data.dsOrdenTrabajo.ORDENES_TRABAJORow row = (Data.dsOrdenTrabajo.ORDENES_TRABAJORow)(sourceOrdenTrabajo.Current as DataRowView).Row;
                frmArbolOrdenesTrabajo.Instancia.SeleccionarOrdenTrabajo(Convert.ToInt32(row.ORDT_NUMERO));
            }
        }

        private void bnMoveLastItem_Click(object sender, EventArgs e)
        {
            CompletarDatosOrdenTrabajo();
            if (animador.EsVisible())
            {
                Data.dsOrdenTrabajo.ORDENES_TRABAJORow row = (Data.dsOrdenTrabajo.ORDENES_TRABAJORow)(sourceOrdenTrabajo.Current as DataRowView).Row;
                frmArbolOrdenesTrabajo.Instancia.SeleccionarOrdenTrabajo(Convert.ToInt32(row.ORDT_NUMERO));
            }
        }

        private void bnAplicar_Click(object sender, EventArgs e)
        {
            Data.dsOrdenTrabajo.ORDENES_TRABAJORow row = (Data.dsOrdenTrabajo.ORDENES_TRABAJORow)(sourceOrdenTrabajo.Current as DataRowView).Row;
            row.ORDT_CODIGO = txtCodigoOrdenT.Text;
            row.ORDT_ORIGEN = txtOrigenOrdenT.Text;
            row.ORDT_OBSERVACIONES = txtObservacionesOrdenT.Text;
        }

        #endregion

        #region Servicios

        #region InicializarDatos
        private void InicializarDatos()
        {
            //Grilla ordenes trabajo
            DataGridViewCheckBoxColumn columnaCheck = new DataGridViewCheckBoxColumn();
            columnaCheck.Name = "SELECCION";
            columnaCheck.HeaderText = "";
            columnaCheck.ReadOnly = false;
            columnaCheck.TrueValue = true;
            columnaCheck.FalseValue = false;
            columnaCheck.MinimumWidth = 20;
            columnaCheck.Frozen = true;
            
            dgvListaOrdenProduccion.AutoGenerateColumns = false;
            dgvListaOrdenProduccion.Columns.Add(columnaCheck);
            dgvListaOrdenProduccion.Columns.Add("ORDP_CODIGO", "Código");
            dgvListaOrdenProduccion.Columns.Add("ORDP_FECHAALTA", "Fecha creación");
            dgvListaOrdenProduccion.Columns.Add("ORDP_ORIGEN", "Origen");
            dgvListaOrdenProduccion.Columns.Add("COC_CODIGO", "Cocina");
            dgvListaOrdenProduccion.Columns.Add("ORDP_CANTIDADESTIMADA", "Cantidad");
            dgvListaOrdenProduccion.Columns.Add("ORDP_FECHAINICIOESTIMADA", "Fecha inicio");
            dgvListaOrdenProduccion.Columns.Add("ORDP_FECHAFINESTIMADA", "Fecha fin");
            dgvListaOrdenProduccion.Columns.Add("ORDP_PRIORIDAD", "Prioridad");
            dgvListaOrdenProduccion.Columns["ORDP_CODIGO"].DataPropertyName = "ORDP_CODIGO";
            dgvListaOrdenProduccion.Columns["ORDP_FECHAALTA"].DataPropertyName = "ORDP_FECHAALTA";
            dgvListaOrdenProduccion.Columns["ORDP_ORIGEN"].DataPropertyName = "ORDP_ORIGEN";
            dgvListaOrdenProduccion.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvListaOrdenProduccion.Columns["ORDP_CANTIDADESTIMADA"].DataPropertyName = "ORDP_CANTIDADESTIMADA";
            dgvListaOrdenProduccion.Columns["ORDP_FECHAINICIOESTIMADA"].DataPropertyName = "ORDP_FECHAINICIOESTIMADA";
            dgvListaOrdenProduccion.Columns["ORDP_FECHAFINESTIMADA"].DataPropertyName = "ORDP_FECHAFINESTIMADA";
            dgvListaOrdenProduccion.Columns["ORDP_PRIORIDAD"].DataPropertyName = "ORDP_PRIORIDAD";
            dgvListaOrdenProduccion.Columns["ORDP_CODIGO"].ReadOnly = true;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAALTA"].ReadOnly = true;
            dgvListaOrdenProduccion.Columns["ORDP_ORIGEN"].ReadOnly = true;
            dgvListaOrdenProduccion.Columns["COC_CODIGO"].ReadOnly = true;
            dgvListaOrdenProduccion.Columns["ORDP_CANTIDADESTIMADA"].ReadOnly = true;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAINICIOESTIMADA"].ReadOnly = true;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAFINESTIMADA"].ReadOnly = true;
            dgvListaOrdenProduccion.Columns["ORDP_PRIORIDAD"].ReadOnly = true;
            dgvListaOrdenProduccion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvListaOrdenProduccion.AllowUserToResizeColumns = true;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAALTA"].MinimumWidth = 110;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAINICIOESTIMADA"].MinimumWidth = 90;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAFINESTIMADA"].MinimumWidth = 90;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAALTA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAINICIOESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAFINESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaOrdenProduccion.Columns["ORDP_CANTIDADESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            chkSeleccionarOP.Parent = dgvListaOrdenProduccion;
            chkSeleccionarOP.Location = new Point(5, 3);
            chkSeleccionarOP.Text = string.Empty;
            chkSeleccionarOP.AutoSize = true;
            chkSeleccionarOP.CheckedChanged += new EventHandler(chkSeleccionarOP_CheckedChanged);

            //Dataviews y combos
            dvPlanAnual = new DataView(dsPlanSemanal.PLANES_ANUALES);
            dvMensual = new DataView(dsPlanSemanal.PLANES_MENSUALES);
            dvPlanSemanal = new DataView(dsPlanSemanal.PLANES_SEMANALES);
            dvOrdenProduccion = new DataView(dsOrdenTrabajo.ORDENES_PRODUCCION);
            dgvListaOrdenProduccion.DataSource = dvOrdenProduccion;
            dvOrdenTrabajo = new DataView(dsOrdenTrabajo.ORDENES_TRABAJO);
            dvStockDestino = new DataView(dsHojaRuta.UBICACIONES_STOCK);
            string[] nombres = { "Hacia adelante", "Hacia atrás" };
            int[] valores = { 0, 1 };
            cbModoFecha.SetDatos(nombres, valores, "Seleccione", false);            
            lblMensajeOP = new Label();
            lblMensajeOT = new Label();
            lblMensajeOP.Text = "Seleccione una Orden de Producción";
            lblMensajeOT.Text = "No hay Órdenes de Trabajo";
            tpOrdenProduccion.Controls.Add(lblMensajeOP);
            tpOrdenTrabajo.Controls.Add(lblMensajeOT);
            lblMensajeOP.Visible = false;
            lblMensajeOP.Font = new Font("Tahoma", 12);
            lblMensajeOT.Visible = false;
            lblMensajeOT.Font = new Font("Tahoma", 12);
            lblMensajeOT.AutoSize = true;
            lblMensajeOP.AutoSize = true;
            Point ubicacion = new Point(tpOrdenProduccion.Size.Width / 3, tpOrdenProduccion.Size.Height / 2);
            lblMensajeOP.Location = ubicacion;
            lblMensajeOT.Location = ubicacion;

            try
            {
                BLL.PlanAnualBLL.ObtenerTodos(dsPlanSemanal.PLANES_ANUALES);
                BLL.CocinaBLL.ObtenerCocinas(dsPlanSemanal.COCINAS);
                BLL.EstadoOrdenTrabajoBLL.ObtenerEstadosOrden(dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO);
                BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsHojaRuta.UBICACIONES_STOCK);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Generar Orden de Producción - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cbAnioBuscar.SetDatos(dvPlanAnual, "PAN_CODIGO", "PAN_ANIO", "Seleccione", false);
            cboStockDestino.SetDatos(dvStockDestino, "USTCK_NUMERO", "USTCK_NOMBRE", "Seleccione", false);
        }

        void chkSeleccionarOP_CheckedChanged(object sender, EventArgs e)
        {
            dgvListaOrdenProduccion.BeginEdit(true);
            foreach (DataGridViewRow fila in dgvListaOrdenProduccion.Rows)
            {
                DataGridViewCheckBoxCell cellSelecion = fila.Cells[0] as DataGridViewCheckBoxCell;
                cellSelecion.Value = chkSeleccionarOP.Checked;
            }
            dgvListaOrdenProduccion.EndEdit();
            dgvListaOrdenProduccion.Refresh();
        }
        #endregion

        #region Cargar plan semanal en treeview
        private void CargarPlanSemanal(int codigoSemana)
        {
            tvDetallePlan.Nodes.Clear();
            tvDetallePlan.BeginUpdate();
            
            TreeNode nodoSemana = new TreeNode();
            nodoSemana.Name = codigoSemana.ToString();
            nodoSemana.Text = "Semana " + dsPlanSemanal.PLANES_SEMANALES.FindByPSEM_CODIGO(codigoSemana).PSEM_SEMANA.ToString();
            nodoSemana.Tag = tipoNodo.semana;

            tvDetallePlan.Nodes.Add(nodoSemana);
            
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
                    nodoDetalle.Name = rowDetalle.DPSEM_CODIGO.ToString();
                    nodoDetalle.Text = "Cocina: " + rowDetalle.COCINASRow.COC_CODIGO_PRODUCTO + " - Cantidad: " + rowDetalle.DPSEM_CANTIDADESTIMADA.ToString();
                    if (rowDetalle.IsDPED_CODIGONull()) { nodoDetalle.Text += " - Origen: Planificación"; }
                    else { nodoDetalle.Text += " - Origen: Pedido de cliente"; }
                    nodoDetalle.Tag = tipoNodo.detalleDia;
                    if (Convert.ToInt32(rowDetalle.DPSEM_ESTADO) == BLL.DetallePlanSemanalBLL.estadoGenerado) { nodoDetalle.ForeColor = Color.Red; }
                    else if (Convert.ToInt32(rowDetalle.DPSEM_ESTADO) == BLL.DetallePlanSemanalBLL.estadoModificado) { nodoDetalle.ForeColor = Color.Yellow; }
                    else if (Convert.ToInt32(rowDetalle.DPSEM_ESTADO) == BLL.DetallePlanSemanalBLL.estadoConOrden) { nodoDetalle.ForeColor = Color.Black; }
                    nodoDia.Nodes.Add(nodoDetalle);
                    nodoDetalle.Checked = false;
                    Sistema.FuncionesAuxiliares.QuitarCheckBox(nodoDetalle, tvDetallePlan);
                }
            }

            tvDetallePlan.EndUpdate();
            tvDetallePlan.ExpandAll();
        }
        #endregion

        #region Completar datos al seleccionar una orden de producción
        private void CompletarDatosOrdenTrabajo()
        {
            if (sourceOrdenTrabajo.Count > 0)
            {
                Data.dsOrdenTrabajo.ORDENES_TRABAJORow row = (Data.dsOrdenTrabajo.ORDENES_TRABAJORow)(sourceOrdenTrabajo.Current as DataRowView).Row;
                if (row.ORDT_NUMERO < 0) { txtNumeroOrdenT.Text = "No asignado..."; }
                else { txtNumeroOrdenT.Text = row.ORDT_NUMERO.ToString(); }
                txtCodigoOrdenT.Text = row.ORDT_CODIGO;
                txtOrigenOrdenT.Text = row.ORDT_ORIGEN;
                if (row.PAR_TIPO == BLL.OrdenProduccionBLL.parteTipoConjunto) 
                { 
                    txtTipoParteOrdenT.Text = "Conjunto";
                    //txtParteOrdenT.Text = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(Convert.ToInt32(row.PAR_CODIGO)).CONJ_CODIGOPARTE;
                    int hoja = 0;//Convert.ToInt32(dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(Convert.ToInt32(row.PAR_CODIGO)).HR_CODIGO);
                    txtHojaRuta.Text = dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(hoja).HR_NOMBRE;
                }
                else if (row.PAR_TIPO == BLL.OrdenProduccionBLL.parteTipoSubconjunto) 
                { 
                    txtTipoParteOrdenT.Text = "Subconjunto";
                    //txtParteOrdenT.Text = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(Convert.ToInt32(row.PAR_CODIGO)).SCONJ_CODIGOPARTE;
                    int hoja = 0;// Convert.ToInt32(dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(Convert.ToInt32(row.PAR_CODIGO)).HR_CODIGO);
                    txtHojaRuta.Text = dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(hoja).HR_NOMBRE;
                }
                else if (row.PAR_TIPO == BLL.OrdenProduccionBLL.parteTipoPieza)
                { 
                    txtTipoParteOrdenT.Text = "Pieza";
                    //txtParteOrdenT.Text = dsEstructura.PIEZAS.FindByPZA_CODIGO(Convert.ToInt32(row.PAR_CODIGO)).PZA_CODIGOPARTE;
                    int hoja = 0;// Convert.ToInt32(dsEstructura.PIEZAS.FindByPZA_CODIGO(Convert.ToInt32(row.PAR_CODIGO)).HR_CODIGO);
                    txtHojaRuta.Text = dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(hoja).HR_NOMBRE;
                }
                txtCantidadOrdenT.Text = row.ORDT_CANTIDADESTIMADA.ToString();
                txtCentroTrabajo.Text = dsHojaRuta.CENTROS_TRABAJOS.FindByCTO_CODIGO(Convert.ToInt32(row.CTO_CODIGO)).CTO_NOMBRE;
                txtOperacion.Text = dsHojaRuta.OPERACIONES.FindByOPR_NUMERO(Convert.ToInt32(row.OPR_NUMERO)).OPR_NOMBRE;
                txtObservacionesOrdenT.Text = row.ORDT_OBSERVACIONES;

                if (!row.IsORDT_FECHAINICIOESTIMADANull()) { txtFechaInicioOrdenT.Text = row.ORDT_FECHAINICIOESTIMADA.ToShortDateString(); }
                else { txtFechaInicioOrdenT.Clear(); }
                if (!row.IsORDT_FECHAFINESTIMADANull()) { txtFechaFinOrdenT.Text = row.ORDT_FECHAFINESTIMADA.ToShortDateString(); }
                else { txtFechaFinOrdenT.Clear(); }
                if (!row.IsORDT_HORAINICIOESTIMADANull()) { txtHoraInicioOrdenT.Text = Sistema.FuncionesAuxiliares.DecimalHourToString(row.ORDT_HORAINICIOESTIMADA); }
                else { txtHoraInicioOrdenT.Clear(); }
                if (!row.IsORDT_HORAFINESTIMADANull()) { txtHoraFinOrdenT.Text = Sistema.FuncionesAuxiliares.DecimalHourToString(row.ORDT_HORAFINESTIMADA); }
                else { txtHoraFinOrdenT.Clear(); }                
            }
        }

        private void CompletarDatosOrdenProduccion(int numeroOrdenProduccion)
        {
            dvOrdenTrabajo.RowFilter = "ordp_numero = " + numeroOrdenProduccion;
            sourceOrdenTrabajo.DataSource = dvOrdenTrabajo;
            bnNavegador.BindingSource = sourceOrdenTrabajo;
            txtCodigoOrdenP.Text = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_CODIGO;
            txtOrigenOrdenP.Text = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_ORIGEN;
            txtEstadoOrdenP.Text = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ESTADO_ORDENES_TRABAJORow.EORD_NOMBRE;
            txtFechaAltaOrdenP.Text = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_FECHAALTA.ToShortDateString();
            int codigoPlan = Convert.ToInt32(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).DPSEM_CODIGO);
            txtCocinaOrdenP.Text = dsPlanSemanal.DETALLE_PLANES_SEMANALES.FindByDPSEM_CODIGO(codigoPlan).COCINASRow.COC_CODIGO_PRODUCTO;
            txtCantidadOrdenP.Text = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_CANTIDADESTIMADA.ToString();
            if (dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).IsORDP_FECHAINICIOESTIMADANull()) { txtFechaInicioOrdenP.Clear(); }
            else { txtFechaInicioOrdenP.Text = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_FECHAINICIOESTIMADA.ToShortDateString(); }
            if (dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).IsORDP_FECHAFINESTIMADANull()) { txtFechaFinOrdenP.Clear();  }
            else { txtFechaFinOrdenP.Text = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_FECHAFINESTIMADA.ToShortDateString(); }
            if (dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).IsUSTCK_DESTINONull()) { cboStockDestino.SetSelectedIndex(-1); }
            else { cboStockDestino.SetSelectedValue(Convert.ToInt32(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).USTCK_DESTINO)); }
            txtObservacionesOrdenP.Text = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_OBSERVACIONES;
            CompletarDatosOrdenTrabajo();
        }
        #endregion

        #region Efecto de los botones
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

        #endregion

        #region Eventos Grilla orden producion
        private void dgvListaOrdenProduccion_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != string.Empty)
            {
                string nombre = string.Empty;
                switch (dgvListaOrdenProduccion.Columns[e.ColumnIndex].Name)
                {
                    case "ORDP_FECHAALTA":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "COC_CODIGO":
                        nombre = dsPlanSemanal.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value.ToString())).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    case "ORDP_FECHAINICIOESTIMADA":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "ORDP_FECHAFINESTIMADA":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvListaOrdenProduccion_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Código cambiado al evento de seleccionar pestaña para evitar un comportamiento extraño al eliminar y demás
            //int codigo = Convert.ToInt32(dvOrdenProduccion[e.RowIndex]["ordp_numero"]);
            //CompletarDatosOrdenProduccion(codigo);
        }

        private void dgvListaOrdenProduccion_SelectionChanged(object sender, EventArgs e)
        {
            //Código cambiado al evento de seleccionar pestaña para evitar un comportamiento extraño al eliminar y demás
            //if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
            //{
            //int codigo = Convert.ToInt32(dvOrdenProduccion[dgvListaOrdenProduccion.SelectedRows[0].Index]["ordp_numero"]);
            //CompletarDatosOrdenProduccion(codigo);
            //}
        }
        #endregion

        #region Menú bloquear columnas
        private void tsmiBloquearColumna_Click(object sender, EventArgs e)
        {
            if (columnIndex != -1) { dgvListaOrdenProduccion.Columns[columnIndex].Frozen = true; }
        }

        private void tsmiDesbloquearColumna_Click(object sender, EventArgs e)
        {
            if (columnIndex != -1) { dgvListaOrdenProduccion.Columns[columnIndex].Frozen = false; }
        }

        private void dgvListaOrdenProduccion_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    columnIndex = e.ColumnIndex;
                    if (dgvListaOrdenProduccion.Columns[columnIndex].Frozen)
                    {
                        tsmiBloquearColumna.Checked = true;
                        tsmiDesbloquearColumna.Checked = false;
                    }
                    else
                    {
                        tsmiBloquearColumna.Checked = false;
                        tsmiDesbloquearColumna.Checked = true;
                    }
                    cmsGrillaOrdenesProduccion.Show(MousePosition);
                }
            }
        }
        #endregion

        #region Ventana Árbol

        private void btnArbol_Click(object sender, EventArgs e)
        {
            if (animador.EsVisible()) { animador.CerrarFormulario(); }
            else
            {
                if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
                {
                    tvDependenciaSimple = frmArbolOrdenesTrabajo.Instancia.GetArbolDependenciaSimple();
                    tvDependenciaCompleta = frmArbolOrdenesTrabajo.Instancia.GetArbolDependenciaCompleta();
                    tvOrdenesYEstructura = frmArbolOrdenesTrabajo.Instancia.GetArbolOrdenesYEstructura();
                    int codOrdenP = Convert.ToInt32(dvOrdenProduccion[dgvListaOrdenProduccion.SelectedRows[0].Index]["ordp_numero"].ToString());
                    BLL.OrdenProduccionBLL.GenerarArbolOrdenesTrabajo(codOrdenP, tvDependenciaSimple, tvDependenciaCompleta, tvOrdenesYEstructura, dsOrdenTrabajo);
                    frmArbolOrdenesTrabajo.Instancia.SetTextoVentana(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codOrdenP).ORDP_ORIGEN);
                    animador.SetFormulario(frmArbolOrdenesTrabajo.Instancia, this, Sistema.ControlesUsuarios.AnimadorFormulario.animacionDerecha, 300, false);
                    animador.MostrarFormulario();
                }
                else { MessageBox.Show("Debe seleccionar una Orden de Producción de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
        }

        public void SeleccionarOrdenTrabajo(int codigoOrdenTrabajo)
        {            
            int fila = sourceOrdenTrabajo.Find("ordt_numero", codigoOrdenTrabajo);
            sourceOrdenTrabajo.Position = fila;
            CompletarDatosOrdenTrabajo();
        }

        #endregion

        #region Mensajes en pestañas
        private void tcOrdenTrabajo_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tpOrdenProduccion)
            {
                if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
                {
                    gbDatosOrdenP.Visible = true;
                    gbFechas.Visible = true;
                    gbOpcionesOP.Visible = true;
                    lblMensajeOP.Visible = false;
                    int numero = Convert.ToInt32(dvOrdenProduccion[dgvListaOrdenProduccion.SelectedRows[0].Index]["ordp_numero"]);
                    CompletarDatosOrdenProduccion(numero);
                }
                else 
                { 
                    gbDatosOrdenP.Visible = false;
                    gbFechas.Visible = false;
                    gbOpcionesOP.Visible = false;
                    lblMensajeOP.Visible = true;
                }
            }
            else if (e.TabPage == tpOrdenTrabajo)
            {
                if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
                {
                    int numero = Convert.ToInt32(dvOrdenProduccion[dgvListaOrdenProduccion.SelectedRows[0].Index]["ordp_numero"]);
                    CompletarDatosOrdenProduccion(numero);
                    
                    if (sourceOrdenTrabajo.Count > 0)
                    {
                        gbDatosOrdenT.Visible = true;
                        lblMensajeOT.Visible = false;
                    }
                    else
                    {
                        gbDatosOrdenT.Visible = false;
                        lblMensajeOT.Visible = true;
                        lblMensajeOT.Text = "No existen Órdenes de Trabajo generadas";
                    }
                }
                else
                {
                    gbDatosOrdenT.Visible = false;
                    lblMensajeOT.Visible = true;
                    lblMensajeOT.Text = "Seleccione una Orden de Producción";
                }
            }
        }
        #endregion

        #region Limpiar controles

        private void LimpiarControles()
        {
            dvOrdenTrabajo.RowFilter = "ordp_numero = 0";
            sourceOrdenTrabajo.DataSource = dvOrdenTrabajo;
            bnNavegador.BindingSource = sourceOrdenTrabajo;
            txtCodigoOrdenP.Clear();
            txtOrigenOrdenP.Clear();
            txtEstadoOrdenP.Clear();
            txtFechaAltaOrdenP.Clear();
            txtCocinaOrdenP.Clear();
            txtCantidadOrdenP.Clear();
            txtFechaInicioOrdenP.Clear();
            txtFechaInicioOrdenP.Clear();
            txtObservacionesOrdenP.Clear();
            cboStockDestino.SetSelectedIndex(-1);
            CompletarDatosOrdenTrabajo();
            tcOrdenTrabajo.SelectedTab = tpAutomatico;
        }

        #endregion

        #endregion


    }
}
