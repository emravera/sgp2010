using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades;
using GyCAP.Entidades.ArbolEstructura;
using GyCAP.Entidades.ArbolOrdenesTrabajo;
using GyCAP.Entidades.BindingEntity;
using GyCAP.Entidades.Excepciones;
using GyCAP.Entidades.Mensajes;

namespace GyCAP.UI.ControlTrabajoEnProceso
{
    public partial class frmControlProduccion : Form
    {
        private static frmControlProduccion _frmOrdenTrabajo = null;
        private enum estadoUI { pestañaProduccion, pestañaTrabajo, pestañaCierreParcial };
        private estadoUI estadoInterface;
        private int columnIndexProduccion = -1, columnIndexTrabajo = -1, columnIndexCierre = -1;
        private CheckBox chkSeleccionarOT = new CheckBox();
        private SortableBindingList<Empleado> listaEmpleados;
        private SortableBindingList<Maquina> listaMaquinas;
        private SortableBindingList<Cocina> listaCocinas;
        private SortableBindingList<OrdenProduccion> listaOrdenesProduccion;
        private SortableBindingList<EstadoOrdenTrabajo> listaEstadosOrden;
        private SortableBindingList<EstadoMovimientoStock> listaEstadosMovimiento;
        private SortableBindingList<UbicacionStock> listaUbicacionesStock;
        private SortableBindingList<HojaRuta> listaHojasRuta;
        private SortableBindingList<Parte> listaPartes;
        private ArbolProduccion arbolProduccion;

        #region Inicio

        public frmControlProduccion()
        {
            InitializeComponent();
            Inicializar();
        }

        public static frmControlProduccion Instancia
        {
            get
            {
                if (_frmOrdenTrabajo == null || _frmOrdenTrabajo.IsDisposed)
                {
                    _frmOrdenTrabajo = new frmControlProduccion();
                }
                else
                {
                    _frmOrdenTrabajo.BringToFront();
                }
                return _frmOrdenTrabajo;
            }
            set
            {
                _frmOrdenTrabajo = value;
            }
        }

        #endregion

        #region Botones menú

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (estadoInterface == estadoUI.pestañaProduccion)
            {
                if (dgvOrdenesProduccion.SelectedRows.Count > 0)
                {
                    OrdenProduccion ordenP = (OrdenProduccion)dgvOrdenesProduccion.SelectedRows[0].DataBoundItem;
                    IList<ExcepcionesPlan> excepciones = null;

                    try
                    {
                        excepciones = BLL.OrdenProduccionBLL.IniciarOrdenProduccion(ordenP, BLL.DBBLL.GetFechaServidor());

                        if (excepciones.Count > 0)
                        {
                            PlanificacionProduccion.frmExcepcionesPlan frmExcepciones = new PlanificacionProduccion.frmExcepcionesPlan();
                            frmExcepciones.TopLevel = false;
                            frmExcepciones.Parent = this.Parent;
                            frmExcepciones.CargarGrilla(excepciones.ToList());
                            frmExcepciones.Show();
                            frmExcepciones.BringToFront();
                        }

                        dgvOrdenesProduccion.Refresh();
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                    }                   
                }
                else { MensajesABM.MsjSinSeleccion("Orden de Producción", MensajesABM.Generos.Femenino, this.Text); }
            }
            else if (estadoInterface == estadoUI.pestañaTrabajo)
            {
                /*if (dgvOrdenesTrabajo.SelectedRows.Count > 0)
                {                    
                    OrdenTrabajo orden = (OrdenTrabajo)dgvOrdenesTrabajo.SelectedRows[0].DataBoundItem;

                    //Controlamos si está en el estado correcto para iniciarse
                    if (orden.Estado.Codigo == (int)OrdenesTrabajoEnum.EstadoOrdenEnum.Generada)
                    {
                        //controlamos si hay dependencias con órdenes anteriore
                    }
                    else
                    {
                        MensajesABM.MsjValidacion("La Orden de Trabajo ya se encuentra iniciada.", this.Text); }
                    }
                }
                else { MensajesABM.MsjSinSeleccion("Orden de Trabajo", MensajesABM.Generos.Femenino, this.Text); }*/
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (dgvOrdenesProduccion.SelectedRows.Count > 0)
            {
                try
                {
                    OrdenProduccion ordenP = (OrdenProduccion)dgvOrdenesProduccion.SelectedRows[0].DataBoundItem;
                    BLL.OrdenProduccionBLL.FinalizarOrdenProduccion(ordenP);
                }
                catch (Entidades.Excepciones.BaseDeDatosException ex)
                {
                    MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                }
            }
            else { MensajesABM.MsjSinSeleccion("Orden de Producción", MensajesABM.Generos.Femenino, this.Text); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //finalizar orden de producción - gonzalo
        }
        
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose(true);
        }

        #endregion

        #region Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (listaOrdenesProduccion != null) { listaOrdenesProduccion.Clear(); }
                listaOrdenesProduccion = BLL.OrdenProduccionBLL.ObtenerOrdenesProduccion(txtCodigoOPBuscar.Text, 
                                                                                        cboEstadoOPBuscar.GetSelectedValueInt(), 
                                                                                        cboModoOPBuscar.GetSelectedValueInt(), 
                                                                                        dtpFechaGeneracionOPBuscar.GetFecha(), 
                                                                                        dtpFechaDesdeOPBuscar.GetFecha(), 
                                                                                        dtpFechaHastaOPBuscar.GetFecha(),
                                                                                        listaCocinas,
                                                                                        listaEstadosOrden,
                                                                                        listaUbicacionesStock);
                
                if (listaOrdenesProduccion.Count == 0)
                {
                    MensajesABM.MsjBuscarNoEncontrado("Órdenes de Producción", this.Text);
                }
                else
                {
                    dgvOrdenesProduccion.DataSource = listaOrdenesProduccion;
                }
                SetInterface(estadoUI.pestañaProduccion);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Búsqueda);
            }
        }

        #endregion

        #region Órdenes de Trabajo

        private void btnFiltrarOT_Click(object sender, EventArgs e)
        {
            /*if (dgvOrdenesProduccion.SelectedRows.Count > 0)
            {
                OrdenProduccion orden = (OrdenProduccion)dgvOrdenesTrabajo.SelectedRows[0].DataBoundItem;
                
                if (txtCodigoOTFiltrar.Text != string.Empty) { filtro = "ORDT_CODIGO LIKE '%" + txtCodigoOTFiltrar.Text + "%'"; }

                if (cboEstadoOTFiltrar.GetSelectedValueInt() != -1)
                {
                    if (filtro == string.Empty) { filtro = "EORD_CODIGO = " + cboEstadoOTFiltrar.GetSelectedValueInt(); }
                    else { filtro += " AND EORD_CODIGO = " + cboEstadoOTFiltrar.GetSelectedValueInt(); }
                }

                if (dtpFechaInicioOTFiltrar.GetFecha() != null)
                {
                    if (filtro == string.Empty) { filtro = "ORDT_FECHAINICIOESTIMADA = '" + DateTime.Parse(dtpFechaInicioOTFiltrar.GetFecha().ToString()).ToShortDateString() + "'"; }
                    else { filtro += " AND ORDT_FECHAINICIOESTIMADA = '" + DateTime.Parse(dtpFechaInicioOTFiltrar.GetFecha().ToString()).ToString("yyyyMMdd") + "'"; }
                }
            }*/                        
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Data.dsOrdenTrabajo.ReporteOrdenTrabajoDataTable dt = new GyCAP.Data.dsOrdenTrabajo.ReporteOrdenTrabajoDataTable();
            int cantidad = 0;

            foreach (DataGridViewRow fila in dgvOrdenesTrabajo.Rows)
            {
                DataGridViewCheckBoxCell cellSelecion = fila.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(cellSelecion.FormattedValue))
                {
                    OrdenTrabajo orden = (OrdenTrabajo)fila.DataBoundItem;
                    Data.dsOrdenTrabajo.ReporteOrdenTrabajoRow rowReporte = dt.NewReporteOrdenTrabajoRow();
                    rowReporte.CodigoOrdenProduccion = (dgvOrdenesProduccion.SelectedRows[0].DataBoundItem as OrdenProduccion).Codigo;
                    rowReporte.CodigoOrdenTrabajo = orden.Codigo;
                    rowReporte.Fecha = orden.FechaInicioEstimada.Value.ToShortDateString();                    
                    rowReporte.ParteFabricada = orden.Parte.Codigo;
                    rowReporte.CentroTrabajo = orden.DetalleHojaRuta.CentroTrabajo.Nombre;
                    rowReporte.Operacion = orden.DetalleHojaRuta.Operacion.Codificacion;
                    rowReporte.Cantidad = orden.CantidadEstimada.ToString();
                    dt.AddReporteOrdenTrabajoRow(rowReporte);
                    cantidad++;
                }
            }

            if (cantidad > 0)
            {
                Data.Reportes.crOrdenTrabajo reporte = new GyCAP.Data.Reportes.crOrdenTrabajo();
                reporte.SetDataSource(dt.Rows);
                //Creamos la pantalla que muestra todos los reportes y le asignamos el reporte
                Sistema.frmVisorReporte visor = new GyCAP.UI.Sistema.frmVisorReporte();
                visor.crvVisor.ReportSource = reporte;
                visor.ShowDialog();
                visor.Dispose();
                reporte.Dispose();
            }
            else { MensajesABM.MsjSinSeleccion("Orden de Trabajo", MensajesABM.Generos.Femenino, this.Text); }
        }

        #endregion

        #region Cierres Parciales

        private void btnAgregarCierre_Click(object sender, EventArgs e)
        {
            if (dgvOrdenesTrabajo.SelectedRows.Count > 0)
            {
                if ((dgvOrdenesTrabajo.SelectedRows[0].DataBoundItem as OrdenTrabajo).Estado.Codigo == (int)OrdenesTrabajoEnum.EstadoOrdenEnum.EnProceso)
                {
                    gbAgregarCierreParcial.Enabled = true;
                    LimpiarDatosCierre();
                }
                else { MensajesABM.MsjValidacion("La Orden de Trabajo seleccionada no se encuentra En Proceso.", this.Text); }
            }
            else { MensajesABM.MsjSinSeleccion("Orden de Trabajo", MensajesABM.Generos.Femenino, this.Text); }
        }

        private void btnModificarCierre_Click(object sender, EventArgs e)
        {
            if (dgvCierresParciales.SelectedRows.Count > 0)
            {
                gbAgregarCierreParcial.Enabled = true;
            }
            else { MensajesABM.MsjSinSeleccion("Cierre parcial", MensajesABM.Generos.Masculino, this.Text); }
        }

        private void btnEliminarCierre_Click(object sender, EventArgs e)
        {
            if (dgvCierresParciales.SelectedRows.Count > 0)
            {
                //Eliminar - gonzalo
                LimpiarDatosCierre();
            }
            else { MensajesABM.MsjSinSeleccion("Cierre parcial", MensajesABM.Generos.Masculino, this.Text); }
        }

        private void btnCancelarCierre_Click(object sender, EventArgs e)
        {
            gbAgregarCierreParcial.Enabled = false;
        }

        private void btnGuardarCierre_Click(object sender, EventArgs e)
        {
            IList<string> validaciones = new List<string>();
            
            if (cboEmpleadoCierre.GetSelectedIndex() == -1) { validaciones.Add("Empleado"); }
            if (cboMaquinaCierre.GetSelectedIndex() == -1) { validaciones.Add("Máquina"); }
            if (nudCantidadCierre.Value == 0) { validaciones.Add("Cantidad"); }
            if (dtpFechaCierre.EsFechaNull()) { validaciones.Add("Fecha"); }
            
            if (validaciones.Count == 0)
            {
                try
                {
                    CierreParcialOrdenTrabajo cierre = new CierreParcialOrdenTrabajo();
                    cierre.Empleado = listaEmpleados.Where(p => p.Codigo == long.Parse(cboEmpleadoCierre.GetSelectedValueInt().ToString())).Single();
                    cierre.Maquina = listaMaquinas.Where(p => p.Codigo == cboMaquinaCierre.GetSelectedValueInt()).Single();
                    cierre.Cantidad = Convert.ToInt32(nudCantidadCierre.Value);
                    cierre.Codigo = -1;
                    cierre.Fecha = DateTime.Parse(dtpFechaCierre.GetFecha().ToString());
                    cierre.OperacionesFallidas = Convert.ToInt32(nudOperacionesFallidas.Value);
                    cierre.Observaciones = txtObservacionesCierre.Text;
                    cierre.OrdenTrabajo = (OrdenTrabajo)dgvOrdenesTrabajo.SelectedRows[0].DataBoundItem;
                    BLL.OrdenTrabajoBLL.RegistrarCierreParcial(cierre, null);
                }
                catch (Entidades.Excepciones.BaseDeDatosException ex)
                {
                    MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                }
            }
            else
            {
                MensajesABM.MsjValidacion(MensajesABM.EscribirValidacion(MensajesABM.Validaciones.CompletarDatos, validaciones), this.Text);
            }
            
            gbAgregarCierreParcial.Enabled = false;
        }

        #endregion Cierres Parciales

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
            bool hayDatos;
            switch (estado)
            {
                case estadoUI.pestañaProduccion:
                    if (dgvOrdenesProduccion.RowCount == 0)
                    {
                        hayDatos = false;
                    }
                    else
                    {
                        hayDatos = true;
                    }

                    btnIniciar.Enabled = hayDatos;
                    btnFinalizar.Enabled = hayDatos;
                    btnCancelar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    estadoInterface = estadoUI.pestañaProduccion;
                    tcOrdenTrabajo.SelectedTab = tpOrdenesProduccion;
                    txtCodigoOPBuscar.Focus();
                    break;
                case estadoUI.pestañaTrabajo:
                    if (dgvOrdenesTrabajo.RowCount == 0)
                    {
                        hayDatos = false;
                    }
                    else
                    {
                        hayDatos = true;
                    }
                    btnIniciar.Enabled = hayDatos;
                    btnFinalizar.Enabled = hayDatos;
                    btnCancelar.Enabled = hayDatos;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.pestañaTrabajo;
                    tcOrdenTrabajo.SelectedTab = tpOrdenesTrabajo;
                    break;
                case estadoUI.pestañaCierreParcial:                    
                    btnAgregarCierre.Enabled = true;
                    btnModificarCierre.Enabled = true;
                    btnEliminarCierre.Enabled = true;
                    gbAgregarCierreParcial.Enabled = false;
                    estadoInterface = estadoUI.pestañaCierreParcial;
                    tcOrdenTrabajo.SelectedTab = tpCierreParcial;
                    break;
                default:
                    break;
            }
        }

        #region Método Inicializar

        private void Inicializar()
        {
            //Grilla órdenes producción
            dgvOrdenesProduccion.AutoGenerateColumns = false;
            dgvOrdenesProduccion.Columns.Add("ORDP_CODIGO", "Código");
            dgvOrdenesProduccion.Columns.Add("ORDP_ORIGEN", "Origen");
            dgvOrdenesProduccion.Columns.Add("ORDP_FECHAINICIOESTIMADA", "Inicio estimado");
            dgvOrdenesProduccion.Columns.Add("ORDP_FECHAINICIOREAL", "Inicio Real");
            dgvOrdenesProduccion.Columns.Add("ORDP_FECHAFINESTIMADA", "Fin estimado");
            dgvOrdenesProduccion.Columns.Add("ORDP_FECHAFINREAL", "Fin real");
            dgvOrdenesProduccion.Columns.Add("COC_CODIGO", "Cocina");
            dgvOrdenesProduccion.Columns.Add("ORDP_CANTIDADESTIMADA", "Cantidad estimada");
            dgvOrdenesProduccion.Columns.Add("ORDP_CANTIDADREAL", "Cantidad real");
            dgvOrdenesProduccion.Columns.Add("EORD_CODIGO", "Estado");
            dgvOrdenesProduccion.Columns["ORDP_CODIGO"].DataPropertyName = "Codigo";
            dgvOrdenesProduccion.Columns["ORDP_ORIGEN"].DataPropertyName = "Origen";
            dgvOrdenesProduccion.Columns["ORDP_FECHAINICIOESTIMADA"].DataPropertyName = "FechaInicioEstimada";
            dgvOrdenesProduccion.Columns["ORDP_FECHAINICIOREAL"].DataPropertyName = "FechaInicioReal";
            dgvOrdenesProduccion.Columns["ORDP_FECHAFINESTIMADA"].DataPropertyName = "FechaFinEstimada";
            dgvOrdenesProduccion.Columns["ORDP_FECHAFINREAL"].DataPropertyName = "FechaFinReal";
            dgvOrdenesProduccion.Columns["COC_CODIGO"].DataPropertyName = "Cocina";
            dgvOrdenesProduccion.Columns["ORDP_CANTIDADESTIMADA"].DataPropertyName = "CantidadEstimada";
            dgvOrdenesProduccion.Columns["ORDP_CANTIDADREAL"].DataPropertyName = "CantidadReal";
            dgvOrdenesProduccion.Columns["EORD_CODIGO"].DataPropertyName = "Estado";
            dgvOrdenesProduccion.Columns["ORDP_FECHAINICIOESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesProduccion.Columns["ORDP_FECHAINICIOREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesProduccion.Columns["ORDP_FECHAFINESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesProduccion.Columns["ORDP_FECHAFINREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesProduccion.Columns["ORDP_CANTIDADESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrdenesProduccion.Columns["ORDP_CANTIDADREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrdenesProduccion.DataSource =  listaOrdenesProduccion;

            //Grilla órdenes de trabajo
            DataGridViewCheckBoxColumn columnaCheck = new DataGridViewCheckBoxColumn();
            columnaCheck.Name = "SELECCION";
            columnaCheck.HeaderText = "";
            columnaCheck.ReadOnly = false;
            columnaCheck.TrueValue = true;
            columnaCheck.FalseValue = false;
            columnaCheck.Width = 25;
            columnaCheck.Frozen = true;
            
            dgvOrdenesTrabajo.AutoGenerateColumns = false;
            dgvOrdenesTrabajo.Columns.Add(columnaCheck);
            dgvOrdenesTrabajo.Columns.Add("ORDT_CODIGO", "Código");
            dgvOrdenesTrabajo.Columns.Add("ORDT_ORIGEN", "Origen");
            dgvOrdenesTrabajo.Columns.Add("PART_CODIGO", "Parte");
            dgvOrdenesTrabajo.Columns.Add("ORDT_CANTIDADESTIMADA", "Cantidad estimada");
            dgvOrdenesTrabajo.Columns.Add("ORDT_CANTIDADREAL", "Cantidad real");
            dgvOrdenesTrabajo.Columns.Add("ORDT_FECHAINICIOESTIMADA", "Inicio estimado");
            dgvOrdenesTrabajo.Columns.Add("ORDT_FECHAINICIOREAL", "Inicio real");
            dgvOrdenesTrabajo.Columns.Add("ORDT_FECHAFINESTIMADA", "Fin estimado");
            dgvOrdenesTrabajo.Columns.Add("ORDT_FECHAFINREAL", "Fin real");
            dgvOrdenesTrabajo.Columns.Add("CTO_CODIGO", "Centro trabajo");
            dgvOrdenesTrabajo.Columns.Add("OPR_NUMERO", "Operación");
            dgvOrdenesTrabajo.Columns.Add("ORDT_TIPO", "Tipo");
            dgvOrdenesTrabajo.Columns.Add("EORD_CODIGO", "Estado");
            dgvOrdenesTrabajo.Columns["ORDT_CODIGO"].DataPropertyName = "Codigo";
            dgvOrdenesTrabajo.Columns["ORDT_ORIGEN"].DataPropertyName = "Origen";
            dgvOrdenesTrabajo.Columns["PART_CODIGO"].DataPropertyName = "Parte";
            dgvOrdenesTrabajo.Columns["ORDT_CANTIDADESTIMADA"].DataPropertyName = "CantidadEstimada";
            dgvOrdenesTrabajo.Columns["ORDT_CANTIDADREAL"].DataPropertyName = "CantidadReal";
            dgvOrdenesTrabajo.Columns["ORDT_FECHAINICIOESTIMADA"].DataPropertyName = "FechaInicioEstimada";
            dgvOrdenesTrabajo.Columns["ORDT_FECHAINICIOREAL"].DataPropertyName = "FechaInicioReal";
            dgvOrdenesTrabajo.Columns["ORDT_FECHAFINESTIMADA"].DataPropertyName = "FechaFinEstimada";
            dgvOrdenesTrabajo.Columns["ORDT_FECHAFINREAL"].DataPropertyName = "FechaFinReal";                        
            dgvOrdenesTrabajo.Columns["CTO_CODIGO"].DataPropertyName = "DetalleHojaRuta";
            dgvOrdenesTrabajo.Columns["OPR_NUMERO"].DataPropertyName = "DetalleHojaRuta";
            dgvOrdenesTrabajo.Columns["ORDT_TIPO"].DataPropertyName = "Tipo";
            dgvOrdenesTrabajo.Columns["EORD_CODIGO"].DataPropertyName = "Estado";
            dgvOrdenesTrabajo.Columns["ORDT_CODIGO"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_ORIGEN"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["PART_CODIGO"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_CANTIDADESTIMADA"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_CANTIDADREAL"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAINICIOESTIMADA"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAINICIOREAL"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAFINESTIMADA"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAFINREAL"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["CTO_CODIGO"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["OPR_NUMERO"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_TIPO"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["EORD_CODIGO"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_CANTIDADESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrdenesTrabajo.Columns["ORDT_CANTIDADREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAINICIOESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAINICIOREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAFINESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAFINREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            chkSeleccionarOT.Parent = dgvOrdenesTrabajo;
            chkSeleccionarOT.Location = new Point(7, 10);
            chkSeleccionarOT.Text = string.Empty;
            chkSeleccionarOT.AutoSize = true;
            chkSeleccionarOT.CheckedChanged += new EventHandler(chkSeleccionarOT_CheckedChanged);

            //Grilla cierres parciales
            dgvCierresParciales.AutoGenerateColumns = false;
            dgvCierresParciales.Columns.Add("ORDT_NUMERO", "Orden Trabajo");
            dgvCierresParciales.Columns.Add("E_CODIGO", "Empleado");
            dgvCierresParciales.Columns.Add("MAQ_CODIGO", "Máquina");
            dgvCierresParciales.Columns.Add("CORD_CANTIDAD", "Cantidad");
            dgvCierresParciales.Columns.Add("CORD_FECHACIERRE", "Fecha");
            dgvCierresParciales.Columns.Add("OPR_FALLIDAS", "Operaciones fallidas");
            dgvCierresParciales.Columns.Add("CORD_OBSERVACIONES", "Observaciones");
            dgvCierresParciales.Columns["ORDT_NUMERO"].DataPropertyName = "OrdenTrabajo";
            dgvCierresParciales.Columns["E_CODIGO"].DataPropertyName = "Empleado";
            dgvCierresParciales.Columns["MAQ_CODIGO"].DataPropertyName = "Maquina";
            dgvCierresParciales.Columns["CORD_CANTIDAD"].DataPropertyName = "Cantidad";
            dgvCierresParciales.Columns["CORD_FECHACIERRE"].DataPropertyName = "Fecha";
            dgvCierresParciales.Columns["OPR_FALLIDAS"].DataPropertyName = "OperacionesFallidas";
            dgvCierresParciales.Columns["CORD_OBSERVACIONES"].DataPropertyName = "Observaciones";
            dgvCierresParciales.Columns["CORD_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCierresParciales.Columns["CORD_FECHACIERRE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCierresParciales.Columns["OPR_FALLIDAS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCierresParciales.Columns["CORD_OBSERVACIONES"].Resizable = DataGridViewTriState.True;
            
            //Carga de datos iniciales desde la BD
            try
            {
                listaEmpleados = BLL.EmpleadoBLL.GetAll();
                listaMaquinas = BLL.MaquinaBLL.GetAll();
                listaCocinas = BLL.CocinaBLL.GetAll();
                listaEstadosOrden = BLL.EstadoOrdenTrabajoBLL.GetAll();
                listaEstadosMovimiento = BLL.EstadoMovimientoStockBLL.GetAll();
                listaUbicacionesStock = BLL.UbicacionStockBLL.GetAll();
                listaPartes = BLL.ParteBLL.GetAll();
                listaHojasRuta = BLL.HojaRutaBLL.GetAll();
            }
            catch (BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }
                        
            string[] nombres = new string[listaEstadosOrden.Count];
            int[] valores = new int[listaEstadosOrden.Count];
            
            for (int i = 0; i < listaEstadosOrden.Count; i++)
			{
                nombres[i] = listaEstadosOrden[i].Nombre;
			    valores[i] = listaEstadosOrden[i].Codigo;
			}

            cboEstadoOPBuscar.SetDatos(nombres, valores, "--TODOS--", true);
            cboEstadoOTFiltrar.SetDatos(nombres, valores, "--TODOS--", true);

            nombres = new string[listaEmpleados.Count];
            valores = new int[listaEmpleados.Count];
            
            for (int i = 0; i < listaEmpleados.Count; i++)
			{
                nombres[i] = string.Concat(listaEmpleados[i].Apellido, ", ", listaEmpleados[i].Nombre);
			    valores[i] = Convert.ToInt32(listaEmpleados[i].Codigo);
			}
            
            cboEmpleadoCierre.SetDatos(nombres, valores, "Seleccione...", false);
            
            nombres = new string[listaMaquinas.Count];
            valores = new int[listaMaquinas.Count];
            
            for (int i = 0; i < listaMaquinas.Count; i++)
			{
                nombres[i] = listaMaquinas[i].Nombre;
			    valores[i] = listaMaquinas[i].Codigo;
			}

            cboMaquinaCierre.SetDatos(nombres, valores, "Seleccione...", false);

            nombres = new string[] { "Automático", "Manual" };
            valores = new int[] { 1, 0 };
            cboModoOPBuscar.SetDatos(nombres, valores, "--TODOS--", true);           
            
            SetInterface(estadoUI.pestañaProduccion);
        }

        void chkSeleccionarOT_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvOrdenesTrabajo.RowCount > 0)
            {
                dgvOrdenesTrabajo.BeginEdit(true);
                foreach (DataGridViewRow fila in dgvOrdenesTrabajo.Rows)
                {
                    DataGridViewCheckBoxCell cellSelecion = fila.Cells[0] as DataGridViewCheckBoxCell;
                    cellSelecion.Value = chkSeleccionarOT.Checked;
                }
                dgvOrdenesTrabajo.EndEdit();
                dgvOrdenesTrabajo.Refresh();
            }
        }       

        #endregion Método Inicializar

        #region CellFormatings, RowEnter de grillas y Selecting de tabcontrol

        private void dgvOrdenesProduccion_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != string.Empty)
            {                
                string nombre = string.Empty;
                switch (dgvOrdenesProduccion.Columns[e.ColumnIndex].Name)
                {
                    
                    case "COC_CODIGO":
                        nombre = (e.Value as Cocina).CodigoProducto;
                        e.Value = nombre;
                        break;
                    case "ORDP_FECHAINICIOESTIMADA":
                    case "ORDP_FECHAINICIOREAL":
                    case "ORDP_FECHAFINESTIMADA":
                    case "ORDP_FECHAFINREAL":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "EORD_CODIGO":
                        nombre = (e.Value as EstadoOrdenTrabajo).Nombre;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }        

        private void dgvOrdenesTrabajo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != string.Empty)
            {
                string nombre = string.Empty;
                switch (dgvOrdenesTrabajo.Columns[e.ColumnIndex].Name)
                {                      
                    case "PART_CODIGO":
                        nombre = (e.Value as Parte).Codigo;
                        e.Value = nombre;
                        break;
                    case "ORDT_FECHAINICIOESTIMADA":
                    case "ORDT_FECHAINICIOREAL":
                    case "ORDT_FECHAFINESTIMADA":
                    case "ORDT_FECHAFINREAL":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "EORD_CODIGO":
                        nombre = (e.Value as EstadoOrdenTrabajo).Nombre;
                        e.Value = nombre;
                        break;
                    case "CTO_CODIGO":
                        nombre = (e.Value as DetalleHojaRuta).CentroTrabajo.Nombre;
                        e.Value = nombre;
                        break;
                    case "OPR_NUMERO":
                        nombre = (e.Value as DetalleHojaRuta).Operacion.Codificacion;
                        e.Value = nombre;
                        break;
                    case "ORDT_TIPO":
                        nombre = (Convert.ToInt32(e.Value) == (int)OrdenesTrabajoEnum.TipoOrden.Fabricación) ? "Fabricación" : "Adquisición";
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvCierresParciales_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != string.Empty)
            {                
                string nombre = string.Empty;
                switch (dgvCierresParciales.Columns[e.ColumnIndex].Name)
                {

                    case "ORDT_NUMERO":
                        nombre = (e.Value as OrdenTrabajo).Codigo;
                        e.Value = nombre;
                        break;
                    case "E_CODIGO":
                        nombre = string.Concat((e.Value as Empleado).Apellido, ", ", (e.Value as Empleado).Nombre);
                        e.Value = nombre;
                        break;
                    case "MAQ_CODIGO":
                        nombre = (e.Value as Maquina).Nombre;
                        e.Value = nombre;
                        break;
                    case "CORD_FECHACIERRE":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvOrdenesProduccion_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOrdenesProduccion.SelectedRows.Count > 0)
            {
                try
                {
                    OrdenProduccion orden = (OrdenProduccion)dgvOrdenesProduccion.SelectedRows[0].DataBoundItem;
                    BLL.OrdenTrabajoBLL.ObtenerOrdenesTrabajo(orden,
                                                              listaEstadosOrden,
                                                              listaPartes,
                                                              listaHojasRuta,
                                                              false);                    
                    
                    dgvOrdenesTrabajo.DataSource = orden.OrdenesTrabajo;
                }
                catch (BaseDeDatosException ex)
                {
                    MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Búsqueda);
                }
            }
        }

        private void dgvOrdenesTrabajo_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Cambiado a SelectiongChanged para evitar exception indexOutOfRange
        }

        private void dgvOrdenesTrabajo_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrdenesTrabajo.SelectedRows.Count > 0)
            {
                OrdenTrabajo orden = (OrdenTrabajo)dgvOrdenesTrabajo.SelectedRows[0].DataBoundItem;
                BLL.CierreParcialOrdenTrabajoBLL.ObtenerCierresParcialesOrdenTrabajo(orden, listaMaquinas, listaEmpleados);
                dgvCierresParciales.DataSource = orden.CierresParciales;
            }
        }

        private void tcOrdenTrabajo_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tpOrdenesProduccion) { SetInterface(estadoUI.pestañaProduccion); }
            if (e.TabPage == tpOrdenesTrabajo) { SetInterface(estadoUI.pestañaTrabajo); }
            if (e.TabPage == tpCierreParcial) { SetInterface(estadoUI.pestañaCierreParcial); }
        }

        private void dgvCierresParciales_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCierresParciales.SelectedRows.Count > 0)
            {
                CierreParcialOrdenTrabajo cierre = (CierreParcialOrdenTrabajo)dgvCierresParciales.SelectedRows[0].DataBoundItem;
                cboEmpleadoCierre.SetSelectedValue(Convert.ToInt32(cierre.Empleado.Codigo));
                cboMaquinaCierre.SetSelectedValue(cierre.Maquina.Codigo);
                nudCantidadCierre.Value = cierre.Cantidad;
                dtpFechaCierre.SetFecha(cierre.Fecha.Value);
                txtObservacionesCierre.Text = cierre.Observaciones;
                nudOperacionesFallidas.Value = cierre.OperacionesFallidas;
            }
        }
        
        #endregion CellFormatings, RowEnter de grillas y Selecting de tabcontrol

        #region Menú bloquear columnas

        //Grilla órdenes producción
        private void tsmiBloquearProduccion_Click(object sender, EventArgs e)
        {
            if (columnIndexProduccion != -1) { dgvOrdenesProduccion.Columns[columnIndexProduccion].Frozen = true; }
        }

        private void tsmiDesbloquearProduccion_Click(object sender, EventArgs e)
        {
            if (columnIndexProduccion != -1) { dgvOrdenesProduccion.Columns[columnIndexProduccion].Frozen = false; }
        }

        private void dgvOrdenesProduccion_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    columnIndexProduccion = e.ColumnIndex;
                    if (dgvOrdenesProduccion.Columns[columnIndexProduccion].Frozen)
                    {
                        tsmiBloquearProduccion.Checked = true;
                        tsmiDesbloquearProduccion.Checked = false;
                    }
                    else
                    {
                        tsmiBloquearProduccion.Checked = false;
                        tsmiDesbloquearProduccion.Checked = true;
                    }
                    cmsProduccion.Show(MousePosition);
                }
            }
        }

        //Grilla órdenes trabajo
        private void tsmiBloquearTrabajo_Click(object sender, EventArgs e)
        {
            if (columnIndexTrabajo != 0) { dgvOrdenesTrabajo.Columns[columnIndexTrabajo].Frozen = true; }
        }

        private void tsmiDesbloquearTrabajo_Click(object sender, EventArgs e)
        {
            if (columnIndexTrabajo != 0) { dgvOrdenesTrabajo.Columns[columnIndexTrabajo].Frozen = false; }
        }

        private void dgvOrdenesTrabajo_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    columnIndexTrabajo = e.ColumnIndex;
                    if (dgvOrdenesTrabajo.Columns[columnIndexTrabajo].Frozen)
                    {
                        tsmiBloquearTrabajo.Checked = true;
                        tsmiDesbloquearTrabajo.Checked = false;
                    }
                    else
                    {
                        tsmiBloquearTrabajo.Checked = false;
                        tsmiDesbloquearTrabajo.Checked = true;
                    }
                    cmsTrabajo.Show(MousePosition);
                }
            }
        }

        //Grilla cierres parciales
        private void tsmiBloquearCierre_Click(object sender, EventArgs e)
        {
            if (columnIndexCierre != -1) { dgvCierresParciales.Columns[columnIndexCierre].Frozen = true; }
        }

        private void tsmiDesbloquearCierre_Click(object sender, EventArgs e)
        {
            if (columnIndexCierre != -1) { dgvCierresParciales.Columns[columnIndexCierre].Frozen = false; }
        }

        private void dgvCierresParciales_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    columnIndexCierre = e.ColumnIndex;
                    if (dgvCierresParciales.Columns[columnIndexCierre].Frozen)
                    {
                        tsmiBloquearCierre.Checked = true;
                        tsmiDesbloquearCierre.Checked = false;
                    }
                    else
                    {
                        tsmiBloquearCierre.Checked = false;
                        tsmiDesbloquearCierre.Checked = true;
                    }
                    cmsCierres.Show(MousePosition);
                }
            }
        }

        #endregion Menú bloquear columnas
        
        private void LimpiarDatosCierre()
        {
            cboEmpleadoCierre.SetSelectedIndex(-1);
            cboMaquinaCierre.SetSelectedIndex(-1);
            nudCantidadCierre.Value = 0;
            txtObservacionesCierre.Clear();
            dtpFechaCierre.SetFechaNull();
            nudOperacionesFallidas.Value = 0;
        }

        private void control_Enter(object sender, EventArgs e)
        {
            /*if (sender.GetType().Equals(txtCodigoOPBuscar.GetType())) { (sender as TextBox).SelectAll(); }
            if (sender.GetType().Equals(txtObservacionesCierre.GetType())) { (sender as RichTextBox).SelectAll(); }
            if (sender.GetType().Equals(nudCantidadCierre.GetType())) { (sender as NumericUpDown).Select(0, 20); }*/
        }

        private void dgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        } 

        #endregion Servicios  

    }
}
