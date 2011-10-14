using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using GyCAP.Entidades;
using GyCAP.Entidades.ArbolEstructura;
using GyCAP.Entidades.ArbolOrdenesTrabajo;
using GyCAP.Entidades.Mensajes;
using GyCAP.Entidades.Excepciones;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades.BindingEntity;

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
        Data.dsStock dsStock = new GyCAP.Data.dsStock();
        DataView dvPlanAnual, dvMensual, dvPlanSemanal, dvStockDestino;
        private int columnIndex = -1;
        BindingSource sourceOrdenTrabajo = new BindingSource();
        private Sistema.ControlesUsuarios.AnimadorFormulario animador = new GyCAP.UI.Sistema.ControlesUsuarios.AnimadorFormulario();
        private TreeView tvDependenciaSimple, tvOrdenesYEstructura;
        Label lblMensajeOP, lblMensajeOT;
        private CheckBox chkSeleccionarOP = new CheckBox();
        private IList<Cocina> listaCocinas;
        private IList<ArbolProduccion> ordenesProduccion;
        private SortableBindingList<OrdenProduccion> ordenesProduccionSortable = new SortableBindingList<OrdenProduccion>();
        private IList<ExcepcionesPlan> listaExcepciones = new List<ExcepcionesPlan>();

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
                ordenesProduccionSortable.Clear();
                try
                {
                    BLL.PlanMensualBLL.ObtenerPMAnio(dsPlanSemanal.PLANES_MENSUALES, cbAnioBuscar.GetSelectedValueInt());
                    cbMesBuscar.SetDatos(dvMensual, "PMES_CODIGO", "PMES_MES", "PMES_CODIGO ASC", "Seleccione", false);
                    tvDetallePlan.Nodes.Clear();
                }
                catch (Entidades.Excepciones.BaseDeDatosException ex)
                {
                    MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Búsqueda);
                }
            }
        }

        private void cbMesBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMesBuscar.GetSelectedIndex() != -1)
            {
                dsPlanSemanal.PLANES_SEMANALES.Clear();
                ordenesProduccionSortable.Clear();
                tvDetallePlan.Nodes.Clear();
                try
                {
                    BLL.PlanSemanalBLL.obtenerPS(dsPlanSemanal.PLANES_SEMANALES, cbMesBuscar.GetSelectedValueInt());
                    cbSemanaBuscar.SetDatos(dvPlanSemanal, "PSEM_CODIGO", "PSEM_SEMANA", "Seleccione", false);                    
                }
                catch (Entidades.Excepciones.BaseDeDatosException ex)
                {
                    MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Búsqueda);
                }
            }
        }

        private void cbSemanaBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSemanaBuscar.GetSelectedIndex() != -1)
            {
                tvDetallePlan.Nodes.Clear();
                ordenesProduccionSortable.Clear();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dsPlanSemanal.DIAS_PLAN_SEMANAL.Clear();
            dsPlanSemanal.DETALLE_PLANES_SEMANALES.Clear();
            ordenesProduccionSortable.Clear();

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
                        MensajesABM.MsjBuscarNoEncontrado("Plan de Producción", this.Text);
                    }
                }
                catch (Entidades.Excepciones.BaseDeDatosException ex)
                {
                    MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Búsqueda);
                }
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Semana", MensajesABM.Generos.Femenino, this.Text);
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
            try
            {
                int temp = 0;
                listaExcepciones.Clear();
                int[] codigosCocinas = new int[dsPlanSemanal.DETALLE_PLANES_SEMANALES.Rows.Count];
                foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row in dsPlanSemanal.DETALLE_PLANES_SEMANALES.Rows)
                {
                    codigosCocinas[temp] = Convert.ToInt32(row.COC_CODIGO);
                    temp++;
                }

                listaCocinas = BLL.CocinaBLL.GetCocinasByCodigos(codigosCocinas);

                foreach (TreeNode nodoDia in tvDetallePlan.Nodes[0].Nodes)
                {
                    tipoNodo tipo = (tipoNodo)nodoDia.Tag;
                    if (tipo == tipoNodo.dia && nodoDia.Checked)
                    {
                        ordenesProduccion = BLL.OrdenProduccionBLL.GenerarOrdenesProduccion(Convert.ToInt32(nodoDia.Name), dsPlanSemanal, listaCocinas, listaExcepciones);
                        foreach (TreeNode nodoDetalleDia in nodoDia.Nodes)
                        {
                            nodoDetalleDia.ForeColor = System.Drawing.Color.Black;
                        }
                    }
                }

                if (ordenesProduccion == null)
                {
                    MensajesABM.MsjSinSeleccion("Semana o día", MensajesABM.Generos.Femenino, this.Text);
                }
                else
                {
                    foreach (ArbolProduccion item in ordenesProduccion)
                    {
                        ordenesProduccionSortable.Add(item.OrdenProduccion);
                    }

                    if (listaExcepciones.Count > 0)
                    {
                        PlanificacionProduccion.frmExcepcionesPlan frmExcepciones = new frmExcepcionesPlan();
                        frmExcepciones.TopLevel = false;
                        frmExcepciones.Parent = this.Parent;
                        frmExcepciones.CargarGrilla(listaExcepciones.ToList());
                        frmExcepciones.Show();
                        frmExcepciones.BringToFront();
                    }
                }
            }
            catch (BaseDeDatosException ex) { MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Generación); }
        }

        private void btnSubirPrioridad_Click(object sender, EventArgs e)
        {
            if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
            {
                ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].Prioridad += 1;
                dgvListaOrdenProduccion.Refresh();
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Orden de Producción", MensajesABM.Generos.Femenino, this.Text);
            }
        }

        private void btnBajarPrioridad_Click(object sender, EventArgs e)
        {
            if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
            {
                if (ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].Prioridad > 0)
                {
                    ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].Prioridad -= 1;
                    dgvListaOrdenProduccion.Refresh();
                }
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Orden de Producción", MensajesABM.Generos.Femenino, this.Text);
            }
        }

        private void btnGenerarOrdenT_Click(object sender, EventArgs e)
        {
            try
            {
                int selected = 0;
                listaExcepciones.Clear();
                foreach (DataGridViewRow fila in dgvListaOrdenProduccion.Rows)
                {
                    DataGridViewCheckBoxCell cellSelecion = fila.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(cellSelecion.FormattedValue))
                    {
                        int codigoOrdenP = ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].Numero;
                        BLL.OrdenTrabajoBLL.GenerarOrdenesTrabajo(ordenesProduccion.First(p => p.OrdenProduccion.Numero == codigoOrdenP), listaExcepciones);
                        selected++;
                    }
                }

                if (selected == 0)
                {
                    MensajesABM.MsjSinSeleccion("Orden de Producción", MensajesABM.Generos.Femenino, this.Text);
                }
                else
                {
                    if (listaExcepciones.Count > 0)
                    {
                        PlanificacionProduccion.frmExcepcionesPlan frmExcepciones = new frmExcepcionesPlan();
                        frmExcepciones.TopLevel = false;
                        frmExcepciones.Parent = this.Parent;
                        frmExcepciones.CargarGrilla(listaExcepciones.ToList());
                        frmExcepciones.Show();
                        frmExcepciones.BringToFront();
                    }
                }
            }
            catch (BaseDeDatosException ex) { MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Generación); }
        }

        #endregion

        #region Pestaña Generar Manualmente

        #endregion

        #region Pestaña Orden Producción
        
        private void btnAplicarCambios_Click(object sender, EventArgs e)
        {
            if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
            {
                //ver si no existe en la DB, en ese caso actualizar - gonzalo
                ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].Codigo = txtCodigoOrdenP.Text;
                ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].Origen = txtOrigenOrdenP.Text;
                ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].Observaciones = txtObservacionesOrdenP.Text;

                if (cboStockDestino.GetSelectedIndex() != -1)
                {
                    ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].UbicacionStock = new UbicacionStock() { Numero = cboStockDestino.GetSelectedValueInt() };
                }
                else
                {
                    ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].UbicacionStock = null;
                }
                dgvListaOrdenProduccion.Refresh();
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Orden de Producción", MensajesABM.Generos.Femenino, this.Text);
            }
        }

        private void btnEliminarActual_Click(object sender, EventArgs e)
        {
            if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
            {
                int codOrdenP = ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].Numero;
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
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Eliminación);
                    }
                }

                if (eliminar)
                {
                    int codDPSem = ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].DetallePlanSemanal.Codigo;
                    dsPlanSemanal.DETALLE_PLANES_SEMANALES.FindByDPSEM_CODIGO(codDPSem).DPSEM_ESTADO = BLL.DetallePlanSemanalBLL.estadoGenerado;
                    tvDetallePlan.Nodes.Find(codDPSem.ToString(), true)[0].ForeColor = System.Drawing.Color.Red;
                    ordenesProduccionSortable.RemoveAt(dgvListaOrdenProduccion.SelectedRows[0].Index);
                    MensajesABM.MsjConfirmaEliminar(this.Text, MensajesABM.Operaciones.Eliminación);
                    LimpiarControles();
                }
            }
        }

        private void btnEliminarTodas_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.OrdenProduccionBLL.Eliminar(ordenesProduccionSortable.ToList());

                foreach (OrdenProduccion item in ordenesProduccionSortable)
                {
                    dsPlanSemanal.DETALLE_PLANES_SEMANALES.FindByDPSEM_CODIGO(item.DetallePlanSemanal.Codigo).DPSEM_ESTADO = Convert.ToDecimal(PlanificacionEnum.EstadoDetallePlanSemanal.Generado);
                    tvDetallePlan.Nodes.Find(item.DetallePlanSemanal.Codigo.ToString(), true)[0].ForeColor = System.Drawing.Color.Red;
                }

                ordenesProduccionSortable.Clear();                
                MensajesABM.MsjConfirmaEliminar(this.Text, MensajesABM.Operaciones.Eliminación);
                LimpiarControles();
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Eliminación);
            }
        }

        private void btnGuardarActual_Click(object sender, EventArgs e)
        {
            if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
            {
                int codOrdenP = ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].Numero;
                IList<Entidades.Mensajes.ItemValidacion> validaciones = new List<Entidades.Mensajes.ItemValidacion>();
                if (!ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].FechaInicioEstimada.HasValue) { validaciones.Add(new ItemValidacion(MensajesABM.Validaciones.Logica, "Fechas de inicio y finalización")); }
                else { if (ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].FechaInicioEstimada.Value < DateTime.Today) { validaciones.Add(new ItemValidacion(MensajesABM.Validaciones.Logica, "La fecha de inicio no es válida")); } }
                if (ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].UbicacionStock == null) { validaciones.Add(new ItemValidacion(MensajesABM.Validaciones.Seleccion, "Stock destino")); }
                if(validaciones.Count == 0)
                {
                    try
                    {
                        BLL.OrdenProduccionBLL.Guardar(ordenesProduccion.First(p => p.OrdenProduccion.Numero == codOrdenP));
                        MensajesABM.MsjConfirmaGuardar("Orden de Producción", this.Text, MensajesABM.Operaciones.Guardado);
                    }
                    catch (BaseDeDatosException ex) { MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado); }
                }
                else { MensajesABM.MsjValidacion(MensajesABM.EscribirValidacion(validaciones), this.Text); }
            }
            else { MensajesABM.MsjSinSeleccion("Orden de Producción", MensajesABM.Generos.Femenino, this.Text); }
        }

        private void btnGuardarTodo_Click(object sender, EventArgs e)
        {
            IList<Entidades.Mensajes.ItemValidacion> validaciones = new List<Entidades.Mensajes.ItemValidacion>();

            foreach (OrdenProduccion orden in ordenesProduccionSortable)
            {
                if (!orden.FechaInicioEstimada.HasValue) { validaciones.Add(new ItemValidacion(MensajesABM.Validaciones.Logica, "Fechas de la Orden de Producción " + orden.Codigo)); }
                else { if (orden.FechaInicioEstimada.Value < DateTime.Today) { validaciones.Add(new ItemValidacion(MensajesABM.Validaciones.Logica, "La fecha de inicio de la Orden de Producción" + orden.Codigo + "no es válida")); } }
                if (orden.UbicacionStock == null) { validaciones.Add(new ItemValidacion(MensajesABM.Validaciones.Seleccion, "Ubicación de Stock de la Orden de Producción " + orden.Codigo)); }
            }            
            
            if(validaciones.Count == 0)
            {
                foreach (ArbolProduccion item in ordenesProduccion)
                {
                    try
                    {
                        BLL.OrdenProduccionBLL.Guardar(item);
                        MensajesABM.MsjConfirmaGuardar("Órdenes de Producción", this.Text, MensajesABM.Operaciones.Guardado);
                    }
                    catch (BaseDeDatosException ex) { MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado); }
                }                
            }
            else { MensajesABM.MsjValidacion(MensajesABM.EscribirValidacion(validaciones), this.Text); }
        }

        #endregion

        #region Pestaña Orden Trabajo
        
        private void bnMoveNextItem_Click(object sender, EventArgs e)
        {
            CompletarDatosOrdenTrabajo();
            if (animador.EsVisible()) 
            {
                frmArbolOrdenesTrabajo.Instancia.SeleccionarOrdenTrabajo((sourceOrdenTrabajo.Current as OrdenTrabajo).Numero);
            }
        }

        private void bnMovePreviousItem_Click(object sender, EventArgs e)
        {
            CompletarDatosOrdenTrabajo();
            if (animador.EsVisible())
            {
                frmArbolOrdenesTrabajo.Instancia.SeleccionarOrdenTrabajo((sourceOrdenTrabajo.Current as OrdenTrabajo).Numero);
            }
        }

        private void bnMoveFirstItem_Click(object sender, EventArgs e)
        {
            CompletarDatosOrdenTrabajo();
            if (animador.EsVisible())
            {
                frmArbolOrdenesTrabajo.Instancia.SeleccionarOrdenTrabajo((sourceOrdenTrabajo.Current as OrdenTrabajo).Numero);
            }
        }

        private void bnMoveLastItem_Click(object sender, EventArgs e)
        {
            CompletarDatosOrdenTrabajo();
            if (animador.EsVisible())
            {
                frmArbolOrdenesTrabajo.Instancia.SeleccionarOrdenTrabajo((sourceOrdenTrabajo.Current as OrdenTrabajo).Numero);
            }
        }

        private void bnAplicar_Click(object sender, EventArgs e)
        {
            (sourceOrdenTrabajo.Current as OrdenTrabajo).Codigo = txtCodigoOrdenT.Text;
            (sourceOrdenTrabajo.Current as OrdenTrabajo).Origen = txtOrigenOrdenT.Text;
            (sourceOrdenTrabajo.Current as OrdenTrabajo).Observaciones = txtObservacionesOrdenT.Text;
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
            columnaCheck.Width = 25;
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
            dgvListaOrdenProduccion.Columns["ORDP_CODIGO"].DataPropertyName = "Codigo";
            dgvListaOrdenProduccion.Columns["ORDP_FECHAALTA"].DataPropertyName = "FechaAlta";
            dgvListaOrdenProduccion.Columns["ORDP_ORIGEN"].DataPropertyName = "Origen";
            dgvListaOrdenProduccion.Columns["COC_CODIGO"].DataPropertyName = "Cocina";
            dgvListaOrdenProduccion.Columns["ORDP_CANTIDADESTIMADA"].DataPropertyName = "CantidadEstimada";
            dgvListaOrdenProduccion.Columns["ORDP_FECHAINICIOESTIMADA"].DataPropertyName = "FechaInicioEstimada";
            dgvListaOrdenProduccion.Columns["ORDP_FECHAFINESTIMADA"].DataPropertyName = "FechaFinEstimada";
            dgvListaOrdenProduccion.Columns["ORDP_PRIORIDAD"].DataPropertyName = "Prioridad";
            dgvListaOrdenProduccion.Columns["ORDP_CODIGO"].ReadOnly = true;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAALTA"].ReadOnly = true;
            dgvListaOrdenProduccion.Columns["ORDP_ORIGEN"].ReadOnly = true;
            dgvListaOrdenProduccion.Columns["COC_CODIGO"].ReadOnly = true;
            dgvListaOrdenProduccion.Columns["ORDP_CANTIDADESTIMADA"].ReadOnly = true;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAINICIOESTIMADA"].ReadOnly = true;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAFINESTIMADA"].ReadOnly = true;
            dgvListaOrdenProduccion.Columns["ORDP_PRIORIDAD"].ReadOnly = true;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAALTA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAINICIOESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAFINESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaOrdenProduccion.Columns["ORDP_CANTIDADESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvListaOrdenProduccion.Columns["ORDP_PRIORIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaOrdenProduccion.DataSource = ordenesProduccionSortable;
            chkSeleccionarOP.Parent = dgvListaOrdenProduccion;
            chkSeleccionarOP.Location = new Point(7, 10);
            chkSeleccionarOP.Text = string.Empty;
            chkSeleccionarOP.AutoSize = true;
            chkSeleccionarOP.CheckedChanged += new EventHandler(chkSeleccionarOP_CheckedChanged);

            //Dataviews y combos
            dvPlanAnual = new DataView(dsPlanSemanal.PLANES_ANUALES);
            dvMensual = new DataView(dsPlanSemanal.PLANES_MENSUALES);
            dvPlanSemanal = new DataView(dsPlanSemanal.PLANES_SEMANALES);
            dvStockDestino = new DataView(dsStock.UBICACIONES_STOCK);
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
                BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsStock.UBICACIONES_STOCK, (int)StockEnum.ContenidoUbicacion.Cocina);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }

            cbAnioBuscar.SetDatos(dvPlanAnual, "PAN_CODIGO", "PAN_ANIO", "Seleccione", false);
            dvStockDestino.RowFilter = "TUS_CODIGO <> " + (int)StockEnum.TipoUbicacion.Vista;
            cboStockDestino.SetDatos(dvStockDestino, "USTCK_NUMERO", "USTCK_NOMBRE", "Seleccione", false);
        }

        void chkSeleccionarOP_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvListaOrdenProduccion.RowCount > 0)
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
                    if (Convert.ToInt32(rowDetalle.DPSEM_ESTADO) == BLL.DetallePlanSemanalBLL.estadoGenerado) { nodoDetalle.ForeColor = System.Drawing.Color.Red; }
                    else if (Convert.ToInt32(rowDetalle.DPSEM_ESTADO) == BLL.DetallePlanSemanalBLL.estadoModificado) { nodoDetalle.ForeColor = System.Drawing.Color.Yellow; }
                    else if (Convert.ToInt32(rowDetalle.DPSEM_ESTADO) == BLL.DetallePlanSemanalBLL.estadoConOrden) { nodoDetalle.ForeColor = System.Drawing.Color.Black; }
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
                OrdenTrabajo orden = (OrdenTrabajo)sourceOrdenTrabajo.Current;
                if (orden.Numero < 0) { txtNumeroOrdenT.Text = "No asignado..."; }
                else { txtNumeroOrdenT.Text = orden.Numero.ToString(); }
                txtCodigoOrdenT.Text = orden.Codigo;
                txtOrigenOrdenT.Text = orden.Origen;
                txtTipoParteOrdenT.Text = orden.Parte.Tipo.Nombre;
                txtParteOrdenT.Text = orden.Parte.Codigo;
                txtHojaRuta.Text = orden.Parte.HojaRuta.Nombre;                
                txtCantidadOrdenT.Text = orden.CantidadEstimada.ToString();
                txtCentroTrabajo.Text = orden.DetalleHojaRuta.CentroTrabajo.Nombre;
                txtOperacion.Text = orden.DetalleHojaRuta.Operacion.Nombre;
                txtObservacionesOrdenT.Text = orden.Observaciones;
                txtFechaInicioOrdenT.Text = (orden.FechaInicioEstimada.HasValue) ? orden.FechaInicioEstimada.Value.ToShortDateString() : string.Empty;
                txtFechaFinOrdenT.Text = (orden.FechaFinEstimada.HasValue) ? orden.FechaFinEstimada.Value.ToShortDateString() : string.Empty;
                txtTipoOrden.Text = (orden.Tipo == (int)OrdenesTrabajoEnum.TipoOrden.Fabricación) ? "Fabricación" : "Adquisición";
            }
        }

        private void CompletarDatosOrdenProduccion()
        {
            OrdenProduccion orden = (OrdenProduccion)dgvListaOrdenProduccion.SelectedRows[0].DataBoundItem;
            ArbolProduccion arbol = ordenesProduccion.FirstOrDefault(p => p.OrdenProduccion.Numero == orden.Numero);
            if (arbol != null) { sourceOrdenTrabajo.DataSource = arbol.AsOrdenesTrabajoList(); }
            else { sourceOrdenTrabajo.DataSource = null; }
            bnNavegador.BindingSource = sourceOrdenTrabajo;
            txtCodigoOrdenP.Text = orden.Codigo;
            txtOrigenOrdenP.Text = orden.Origen;
            txtEstadoOrdenP.Text = orden.Estado.Nombre;
            txtFechaAltaOrdenP.Text = orden.FechaAlta.ToShortDateString();
            txtCocinaOrdenP.Text = orden.Cocina.CodigoProducto;
            txtCantidadOrdenP.Text = orden.CantidadEstimada.ToString();
            txtFechaInicioOrdenP.Text = (orden.FechaInicioEstimada.HasValue) ? orden.FechaInicioEstimada.Value.ToShortDateString() : string.Empty;
            txtFechaFinOrdenP.Text = (orden.FechaFinEstimada.HasValue) ? orden.FechaFinEstimada.Value.ToShortDateString() : string.Empty;
            if (orden.UbicacionStock == null) { cboStockDestino.SetSelectedIndex(-1); }
            else { cboStockDestino.SetSelectedValue(orden.UbicacionStock.Numero); }
            txtObservacionesOrdenP.Text = orden.Observaciones;
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

        #region Eventos Grilla orden produccion
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
                        nombre = (e.Value as Cocina).CodigoProducto;
                        e.Value = nombre;
                        break;
                    case "ORDP_FECHAINICIOESTIMADA":
                    case "ORDP_FECHAFINESTIMADA":
                        nombre = ((e.Value as DateTime?).HasValue) ? DateTime.Parse(e.Value.ToString()).ToShortDateString() : string.Empty;
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
            //CompletarDatosOrdenProduccion();
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
                    tvOrdenesYEstructura = frmArbolOrdenesTrabajo.Instancia.GetArbolOrdenesYEstructura();
                    int codOrdenP = ordenesProduccionSortable[dgvListaOrdenProduccion.SelectedRows[0].Index].Numero;
                    TreeView tv = ordenesProduccion.FirstOrDefault(p => p.OrdenProduccion.Numero == codOrdenP).AsTreeView();
                    TreeNode nodo = (TreeNode)tv.Nodes[0].Clone();
                    tv.Dispose();
                    tvDependenciaSimple.Nodes.Clear();
                    tvDependenciaSimple.Nodes.Add(nodo);
                    tvDependenciaSimple.ExpandAll();
                    frmArbolOrdenesTrabajo.Instancia.SetTextoVentana(ordenesProduccion.FirstOrDefault(p => p.OrdenProduccion.Numero == codOrdenP).OrdenProduccion.Origen);
                    animador.SetFormulario(frmArbolOrdenesTrabajo.Instancia, this, Sistema.ControlesUsuarios.AnimadorFormulario.animacionDerecha, 300, false);
                    animador.MostrarFormulario();
                }
                else 
                {
                    MensajesABM.MsjSinSeleccion("Orden de Producción", MensajesABM.Generos.Femenino, this.Text); 
                }
            }
        }

        public void SeleccionarOrdenTrabajo(int codigoOrdenTrabajo)
        {
            int fila = (sourceOrdenTrabajo.DataSource as IList<OrdenTrabajo>).IndexOf((sourceOrdenTrabajo.DataSource as IList<OrdenTrabajo>).FirstOrDefault(p => p.Numero == codigoOrdenTrabajo));
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
                    gbOpcionesOP.Visible = true;
                    lblMensajeOP.Visible = false;
                    CompletarDatosOrdenProduccion();
                }
                else 
                { 
                    gbDatosOrdenP.Visible = false;
                    gbOpcionesOP.Visible = false;
                    lblMensajeOP.Visible = true;
                }
            }
            else if (e.TabPage == tpOrdenTrabajo)
            {
                if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
                {
                    CompletarDatosOrdenProduccion();
                    
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
            sourceOrdenTrabajo.DataSource = null;
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
