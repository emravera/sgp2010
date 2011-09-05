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
    public partial class frmControlProduccion : Form
    {
        private static frmControlProduccion _frmOrdenTrabajo = null;
        private enum estadoUI { pestañaProduccion, pestañaTrabajo, pestañaCierreParcial };
        private estadoUI estadoInterface;
        private Data.dsOrdenTrabajo dsOrdenTrabajo = new GyCAP.Data.dsOrdenTrabajo();
        //private Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
        private Data.dsHojaRuta dsHojaRuta = new GyCAP.Data.dsHojaRuta();
        private Data.dsStock dsStock = new GyCAP.Data.dsStock();
        private DataView dvOrdenProduccion, dvOrdenTrabajo, dvCierreParcial, dvEmpleado, dvMaquina;
        private DataView dvEstadoOPBuscar, dvEstadoOTBuscar;
        private int columnIndexProduccion = -1, columnIndexTrabajo = -1, columnIndexCierre = -1;
        private CheckBox chkSeleccionarOT = new CheckBox();

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
                    int numeroOrdenOP = Convert.ToInt32(dvOrdenProduccion[dgvOrdenesProduccion.SelectedRows[0].Index]["ordp_numero"]);
                    //Comprobamos si puede iniciarse
                    if (dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenOP).EORD_CODIGO == BLL.OrdenProduccionBLL.EstadoGenerado)
                    {
                        try
                        {
                            //BLL.OrdenProduccionBLL.IniciarOrdenProduccion(numeroOrdenOP, BLL.DBBLL.GetFechaServidor(), dsOrdenTrabajo, dsStock, dsHojaRuta, dsEstructura);
                            dsOrdenTrabajo.AcceptChanges();
                            dsStock.AcceptChanges();
                        }
                        catch (Entidades.Excepciones.BaseDeDatosException ex)
                        {
                            dsOrdenTrabajo.RejectChanges();
                            dsStock.RejectChanges();
                            MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio Orden de Producción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else { MessageBox.Show("La Orden de Producción ya se encuentra iniciada o finalizada.", "Información: Inicio", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                else { MessageBox.Show("Debe seleccionar una Orden de Producción de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            else if (estadoInterface == estadoUI.pestañaTrabajo)
            {
                if (dgvOrdenesTrabajo.SelectedRows.Count > 0)
                {
                    int numeroOrdenOT = Convert.ToInt32(dvOrdenTrabajo[dgvOrdenesTrabajo.SelectedRows[0].Index]["ordt_numero"]);
                    if (dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenOT).EORD_CODIGO == BLL.OrdenTrabajoBLL.EstadoGenerado)
                    {
                        //preguntar si desea forzar el inicio de la orden de trabajo
                    }
                }                
                else { MessageBox.Show("Debe seleccionar una Orden de Trabajo de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (dgvOrdenesProduccion.SelectedRows.Count > 0)
            {
                //Código temporal - gonzalo
                try
                {
                    int numero = Convert.ToInt32(dvOrdenProduccion[dgvOrdenesProduccion.SelectedRows[0].Index]["ORDP_NUMERO"]);
                    BLL.OrdenProduccionBLL.FinalizarOrdenProduccion(numero, dsOrdenTrabajo, dsStock);
                    dsOrdenTrabajo.AcceptChanges();
                    dsStock.AcceptChanges();
                }
                catch (Entidades.Excepciones.BaseDeDatosException ex)
                {
                    dsOrdenTrabajo.RejectChanges();
                    dsStock.RejectChanges();
                    MessageBox.Show(ex.Message, "Error: " + this.Text + " - Finalización de Orden de Producción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else { MessageBox.Show("Debe seleccionar una Orden de Producción de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

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
                dsOrdenTrabajo.ORDENES_PRODUCCION.Clear();
                dsOrdenTrabajo.ORDENES_TRABAJO.Clear();
                BLL.OrdenProduccionBLL.ObtenerOrdenesProduccion(txtCodigoOPBuscar.Text, cboEstadoOPBuscar.GetSelectedValueInt(), cboModoOPBuscar.GetSelectedValueInt(), dtpFechaGeneracionOPBuscar.GetFecha(), dtpFechaDesdeOPBuscar.GetFecha(), dtpFechaHastaOPBuscar.GetFecha(), dsOrdenTrabajo);

                SetInterface(estadoUI.pestañaProduccion);

                if (dsOrdenTrabajo.ORDENES_PRODUCCION.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Órdenes de Producción con los criterios ingresados.", "Información: Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Generar Orden de Producción - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Órdenes de Produccion
        #endregion

        #region Órdenes de Trabajo

        private void btnFiltrarOT_Click(object sender, EventArgs e)
        {
            
            string filtro = string.Empty;

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

            dvOrdenTrabajo.RowFilter = filtro;
            
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            dsOrdenTrabajo.ReporteOrdenTrabajo.Clear();
            int cantidad = 0;
            foreach (DataGridViewRow fila in dgvOrdenesTrabajo.Rows)
            {
                DataGridViewCheckBoxCell cellSelecion = fila.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(cellSelecion.FormattedValue))
                {
                    int numeroOrdenT = Convert.ToInt32(dvOrdenTrabajo[fila.Index]["ordt_numero"]);
                    int numeroOrdenP = Convert.ToInt32(dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenT).ORDP_NUMERO);
                    Data.dsOrdenTrabajo.ReporteOrdenTrabajoRow rowReporte = dsOrdenTrabajo.ReporteOrdenTrabajo.NewReporteOrdenTrabajoRow();
                    rowReporte.CodigoOrdenProduccion = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenP).ORDP_CODIGO;
                    rowReporte.CodigoOrdenTrabajo = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenT).ORDT_CODIGO;
                    rowReporte.Fecha = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenT).ORDT_FECHAINICIOESTIMADA.ToShortDateString();
                    int tipoParte = Convert.ToInt32(dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenT).PAR_TIPO);
                    int codigoParte = Convert.ToInt32(dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenT).PAR_CODIGO);
                    //if (tipoParte == BLL.OrdenTrabajoBLL.parteTipoConjunto) { rowReporte.ParteFabricada = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigoParte).CONJ_CODIGOPARTE; }
                    //else if (tipoParte == BLL.OrdenTrabajoBLL.parteTipoSubconjunto) { rowReporte.ParteFabricada = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codigoParte).SCONJ_CODIGOPARTE; }
                    //else if (tipoParte == BLL.OrdenTrabajoBLL.parteTipoPieza) { rowReporte.ParteFabricada = dsEstructura.PIEZAS.FindByPZA_CODIGO(codigoParte).PZA_CODIGOPARTE; }
                    rowReporte.CentroTrabajo = dsHojaRuta.CENTROS_TRABAJOS.FindByCTO_CODIGO(dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenT).CTO_CODIGO).CTO_NOMBRE;
                    rowReporte.Operacion = dsHojaRuta.OPERACIONES.FindByOPR_NUMERO(dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenT).OPR_NUMERO).OPR_NOMBRE;
                    rowReporte.Cantidad = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenT).ORDT_CANTIDADESTIMADA.ToString();
                    dsOrdenTrabajo.ReporteOrdenTrabajo.AddReporteOrdenTrabajoRow(rowReporte);
                    cantidad++;
                }
            }

            if (cantidad > 0)
            {
                Data.Reportes.crOrdenTrabajo reporte = new GyCAP.Data.Reportes.crOrdenTrabajo();
                reporte.SetDataSource(dsOrdenTrabajo.ReporteOrdenTrabajo.Rows);
                //Creamos la pantalla que muestra todos los reportes y le asignamos el reporte
                Sistema.frmVisorReporte visor = new GyCAP.UI.Sistema.frmVisorReporte();
                visor.crvVisor.ReportSource = reporte;
                visor.ShowDialog();
                visor.Dispose();
                reporte.Dispose();
            }
            else { MessageBox.Show("Debe seleccionar una o más Órdenes de Trabajo.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        #endregion

        #region Cierres Parciales

        private void btnAgregarCierre_Click(object sender, EventArgs e)
        {
            if (dgvOrdenesTrabajo.SelectedRows.Count > 0)
            {
                int numeroOT = Convert.ToInt32(dvOrdenTrabajo[dgvOrdenesTrabajo.SelectedRows[0].Index]["ORDT_NUMERO"]);
                if (dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOT).EORD_CODIGO == BLL.OrdenTrabajoBLL.EstadoEnProceso)
                {
                    gbAgregarCierreParcial.Enabled = true;
                    LimpiarDatosCierre();
                }
                else { MessageBox.Show("La Orden de Trabajo seleccionada no se encuentra En Proceso.", "Información: Estado incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            else { MessageBox.Show("Debe seleccionar una Orden de Trabajo de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnModificarCierre_Click(object sender, EventArgs e)
        {
            if (dgvCierresParciales.SelectedRows.Count > 0)
            {
                gbAgregarCierreParcial.Enabled = true;
            }
            else { MessageBox.Show("Debe seleccionar un Cierre Parcial de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnEliminarCierre_Click(object sender, EventArgs e)
        {
            if (dgvCierresParciales.SelectedRows.Count > 0)
            {
                int codigo = Convert.ToInt32(dvCierreParcial[dgvCierresParciales.SelectedRows[0].Index]["cord_codigo"]);
                //Eliminar - gonzalo
                LimpiarDatosCierre();
            }
            else { MessageBox.Show("Debe seleccionar un Cierre Parcial de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCancelarCierre_Click(object sender, EventArgs e)
        {
            gbAgregarCierreParcial.Enabled = false;
        }

        private void btnGuardarCierre_Click(object sender, EventArgs e)
        {
            string datosOK = string.Empty;
            if (cboEmpleadoCierre.GetSelectedIndex() == -1) { datosOK = "\n* Empleado"; }
            if (nudCantidadCierre.Value == 0) { datosOK += "\n* Cantidad"; }
            if (dtpFechaCierre.EsFechaNull()) { datosOK += "\n* Fecha"; }
            if (dtpHoraCierre.Value.Hour == 0 && dtpHoraCierre.Value.Minute == 0) { datosOK += "\n* Hora"; }
            if (datosOK == string.Empty)
            {
                try
                {                    
                    Data.dsOrdenTrabajo.CIERRE_ORDEN_TRABAJORow rowCierre = dsOrdenTrabajo.CIERRE_ORDEN_TRABAJO.NewCIERRE_ORDEN_TRABAJORow();
                    rowCierre.BeginEdit();
                    rowCierre.CORD_CODIGO = -1;
                    rowCierre.ORDT_NUMERO = Convert.ToInt32(dvOrdenTrabajo[dgvOrdenesTrabajo.SelectedRows[0].Index]["ordt_numero"]);
                    rowCierre.E_CODIGO = cboEmpleadoCierre.GetSelectedValueInt();
                    if (cboMaquinaCierre.GetSelectedIndex() == -1) { rowCierre.SetMAQ_CODIGONull(); }
                    else { rowCierre.MAQ_CODIGO = cboMaquinaCierre.GetSelectedValueInt(); }                    
                    rowCierre.CORD_CANTIDAD = nudCantidadCierre.Value;                    
                    rowCierre.CORD_FECHACIERRE = DateTime.Parse(dtpFechaCierre.GetFecha().ToString());                    
                    rowCierre.CORD_HORACIERRE = Sistema.FuncionesAuxiliares.StringHourToDecimal(dtpHoraCierre.Value.ToShortTimeString());                    
                    rowCierre.CORD_OBSERVACIONES = txtObservacionesCierre.Text;
                    rowCierre.EndEdit();
                    dsOrdenTrabajo.CIERRE_ORDEN_TRABAJO.AddCIERRE_ORDEN_TRABAJORow(rowCierre);
                    BLL.OrdenTrabajoBLL.RegistrarCierreParcial(Convert.ToInt32(rowCierre.ORDT_NUMERO), dsOrdenTrabajo, dsStock);                    
                    dsOrdenTrabajo.CIERRE_ORDEN_TRABAJO.AcceptChanges();
                    dsStock.MOVIMIENTOS_STOCK.AcceptChanges();
                    dsStock.UBICACIONES_STOCK.AcceptChanges();
                }
                catch (Entidades.Excepciones.BaseDeDatosException ex)
                {
                    dsOrdenTrabajo.CIERRE_ORDEN_TRABAJO.RejectChanges();
                    dsStock.UBICACIONES_STOCK.RejectChanges();
                    dsStock.MOVIMIENTOS_STOCK.RejectChanges();
                    MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe completar los datos:\n\n" + datosOK, "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    if (dsOrdenTrabajo.ORDENES_PRODUCCION.Rows.Count == 0)
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
                    if (dsOrdenTrabajo.ORDENES_TRABAJO.Rows.Count == 0)
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
                    if (dsOrdenTrabajo.ORDENES_TRABAJO.Rows.Count == 0)
                    {
                        hayDatos = false;
                        LimpiarDatosCierre();
                    }
                    else
                    {
                        hayDatos = true;
                    }
                    btnAgregarCierre.Enabled = hayDatos;
                    btnModificarCierre.Enabled = hayDatos;
                    btnEliminarCierre.Enabled = hayDatos;
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
            dgvOrdenesProduccion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvOrdenesProduccion.Columns["ORDP_CODIGO"].DataPropertyName = "ORDP_CODIGO";
            dgvOrdenesProduccion.Columns["ORDP_ORIGEN"].DataPropertyName = "ORDP_ORIGEN";
            dgvOrdenesProduccion.Columns["ORDP_FECHAINICIOESTIMADA"].DataPropertyName = "ORDP_FECHAINICIOESTIMADA";
            dgvOrdenesProduccion.Columns["ORDP_FECHAINICIOREAL"].DataPropertyName = "ORDP_FECHAINICIOREAL";
            dgvOrdenesProduccion.Columns["ORDP_FECHAFINESTIMADA"].DataPropertyName = "ORDP_FECHAFINESTIMADA";
            dgvOrdenesProduccion.Columns["ORDP_FECHAFINREAL"].DataPropertyName = "ORDP_FECHAFINREAL";
            dgvOrdenesProduccion.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvOrdenesProduccion.Columns["ORDP_CANTIDADESTIMADA"].DataPropertyName = "ORDP_CANTIDADESTIMADA";
            dgvOrdenesProduccion.Columns["ORDP_CANTIDADREAL"].DataPropertyName = "ORDP_CANTIDADREAL";
            dgvOrdenesProduccion.Columns["EORD_CODIGO"].DataPropertyName = "EORD_CODIGO";
            dgvOrdenesProduccion.Columns["ORDP_FECHAINICIOESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesProduccion.Columns["ORDP_FECHAINICIOREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesProduccion.Columns["ORDP_FECHAFINESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesProduccion.Columns["ORDP_FECHAFINREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesProduccion.Columns["ORDP_CANTIDADESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrdenesProduccion.Columns["ORDP_CANTIDADREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            //Grilla órdenes de trabajo
            DataGridViewCheckBoxColumn columnaCheck = new DataGridViewCheckBoxColumn();
            columnaCheck.Name = "SELECCION";
            columnaCheck.HeaderText = "";
            columnaCheck.ReadOnly = false;
            columnaCheck.TrueValue = true;
            columnaCheck.FalseValue = false;
            columnaCheck.MinimumWidth = 20;
            columnaCheck.Frozen = true;
            
            dgvOrdenesTrabajo.AutoGenerateColumns = false;
            dgvOrdenesTrabajo.Columns.Add(columnaCheck);
            dgvOrdenesTrabajo.Columns.Add("ORDT_CODIGO", "Código");
            dgvOrdenesTrabajo.Columns.Add("ORDT_ORIGEN", "Origen");
            dgvOrdenesTrabajo.Columns.Add("PAR_CODIGO", "Parte");
            dgvOrdenesTrabajo.Columns.Add("ORDT_CANTIDADESTIMADA", "Cantidad estimada");
            dgvOrdenesTrabajo.Columns.Add("ORDT_CANTIDADREAL", "Cantidad real");
            dgvOrdenesTrabajo.Columns.Add("ORDT_FECHAINICIOESTIMADA", "Inicio estimado");
            dgvOrdenesTrabajo.Columns.Add("ORDT_FECHAINICIOREAL", "Inicio real");
            dgvOrdenesTrabajo.Columns.Add("ORDT_FECHAFINESTIMADA", "Fin estimado");
            dgvOrdenesTrabajo.Columns.Add("ORDT_FECHAFINREAL", "Fin real");
            dgvOrdenesTrabajo.Columns.Add("CTO_CODIGO", "Centro trabajo");
            dgvOrdenesTrabajo.Columns.Add("OPR_NUMERO", "Operación");
            dgvOrdenesTrabajo.Columns.Add("EORD_CODIGO", "Estado");
            dgvOrdenesTrabajo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvOrdenesTrabajo.Columns["ORDT_CODIGO"].DataPropertyName = "ORDT_CODIGO";
            dgvOrdenesTrabajo.Columns["ORDT_ORIGEN"].DataPropertyName = "ORDT_ORIGEN";
            dgvOrdenesTrabajo.Columns["PAR_CODIGO"].DataPropertyName = "ORDT_NUMERO";
            dgvOrdenesTrabajo.Columns["ORDT_CANTIDADESTIMADA"].DataPropertyName = "ORDT_CANTIDADESTIMADA";
            dgvOrdenesTrabajo.Columns["ORDT_CANTIDADREAL"].DataPropertyName = "ORDT_CANTIDADREAL";
            dgvOrdenesTrabajo.Columns["ORDT_FECHAINICIOESTIMADA"].DataPropertyName = "ORDT_FECHAINICIOESTIMADA";
            dgvOrdenesTrabajo.Columns["ORDT_FECHAINICIOREAL"].DataPropertyName = "ORDT_FECHAINICIOREAL";
            dgvOrdenesTrabajo.Columns["ORDT_FECHAFINESTIMADA"].DataPropertyName = "ORDT_FECHAFINESTIMADA";
            dgvOrdenesTrabajo.Columns["ORDT_FECHAFINREAL"].DataPropertyName = "ORDT_FECHAFINREAL";                        
            dgvOrdenesTrabajo.Columns["CTO_CODIGO"].DataPropertyName = "CTO_CODIGO";
            dgvOrdenesTrabajo.Columns["OPR_NUMERO"].DataPropertyName = "OPR_NUMERO";
            dgvOrdenesTrabajo.Columns["EORD_CODIGO"].DataPropertyName = "EORD_CODIGO";
            dgvOrdenesTrabajo.Columns["ORDT_CODIGO"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_ORIGEN"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["PAR_CODIGO"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_CANTIDADESTIMADA"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_CANTIDADREAL"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAINICIOESTIMADA"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAINICIOREAL"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAFINESTIMADA"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAFINREAL"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["CTO_CODIGO"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["OPR_NUMERO"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["EORD_CODIGO"].ReadOnly = true;
            dgvOrdenesTrabajo.Columns["ORDT_CANTIDADESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrdenesTrabajo.Columns["ORDT_CANTIDADREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAINICIOESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAINICIOREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAFINESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAFINREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            chkSeleccionarOT.Parent = dgvOrdenesTrabajo;
            chkSeleccionarOT.Location = new Point(5, 12);
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
            dgvCierresParciales.Columns.Add("CORD_HORACIERRE", "Hora");
            dgvCierresParciales.Columns.Add("CORD_OBSERVACIONES", "Observaciones");
            dgvCierresParciales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvCierresParciales.AutoGenerateColumns = false;
            dgvCierresParciales.Columns["ORDT_NUMERO"].DataPropertyName = "ORDT_NUMERO";
            dgvCierresParciales.Columns["E_CODIGO"].DataPropertyName = "E_CODIGO";
            dgvCierresParciales.Columns["MAQ_CODIGO"].DataPropertyName = "MAQ_CODIGO";
            dgvCierresParciales.Columns["CORD_CANTIDAD"].DataPropertyName = "CORD_CANTIDAD";
            dgvCierresParciales.Columns["CORD_FECHACIERRE"].DataPropertyName = "CORD_FECHACIERRE";
            dgvCierresParciales.Columns["CORD_HORACIERRE"].DataPropertyName = "CORD_HORACIERRE";
            dgvCierresParciales.Columns["CORD_OBSERVACIONES"].DataPropertyName = "CORD_OBSERVACIONES";
            dgvCierresParciales.Columns["CORD_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCierresParciales.Columns["CORD_FECHACIERRE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCierresParciales.Columns["CORD_HORACIERRE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCierresParciales.Columns["CORD_OBSERVACIONES"].Resizable = DataGridViewTriState.True;

            //Carga de datos iniciales desde la BD
            try
            {
                BLL.EmpleadoBLL.ObtenerEmpleados(dsOrdenTrabajo.EMPLEADOS);
                BLL.MaquinaBLL.ObtenerMaquinas(dsOrdenTrabajo.MAQUINAS);
                BLL.CocinaBLL.ObtenerCocinas(dsOrdenTrabajo.COCINAS);
                BLL.EstadoOrdenTrabajoBLL.ObtenerEstadosOrden(dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO);
                BLL.CentroTrabajoBLL.ObetenerCentrosTrabajo(null, null, null, null, dsHojaRuta.CENTROS_TRABAJOS);
                BLL.OperacionBLL.ObetenerOperaciones(dsHojaRuta.OPERACIONES);
                //BLL.ConjuntoBLL.ObtenerConjuntos(dsEstructura.CONJUNTOS);
                //BLL.SubConjuntoBLL.ObtenerSubconjuntos(dsEstructura.SUBCONJUNTOS);
                //BLL.PiezaBLL.ObtenerPiezas(dsEstructura.PIEZAS);
                BLL.EstadoMovimientoStockBLL.ObtenerEstadosMovimiento(dsStock.ESTADO_MOVIMIENTOS_STOCK);
                BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsStock.UBICACIONES_STOCK);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            //Dataviews
            dvOrdenProduccion = new DataView(dsOrdenTrabajo.ORDENES_PRODUCCION);
            dvOrdenProduccion.Sort = "ORDP_FECHAINICIOESTIMADA ASC";
            dgvOrdenesProduccion.DataSource = dvOrdenProduccion;
            dvOrdenTrabajo = new DataView(dsOrdenTrabajo.ORDENES_TRABAJO);
            dvOrdenTrabajo.Sort = "ORDT_FECHAINICIOESTIMADA ASC";
            dgvOrdenesTrabajo.DataSource = dvOrdenTrabajo;
            dvCierreParcial = new DataView(dsOrdenTrabajo.CIERRE_ORDEN_TRABAJO);
            dgvCierresParciales.DataSource = dvCierreParcial;
            dvEstadoOPBuscar = new DataView(dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO);
            cboEstadoOPBuscar.SetDatos(dvEstadoOPBuscar, "EORD_CODIGO", "EORD_NOMBRE", "--TODOS--", true);
            dvEmpleado = new DataView(dsOrdenTrabajo.EMPLEADOS);
            string[] display = { "E_APELLIDO", "E_NOMBRE" };
            cboEmpleadoCierre.SetDatos(dvEmpleado, "E_CODIGO", display, ", ", "Seleccione", false);
            dvMaquina = new DataView(dsOrdenTrabajo.MAQUINAS);
            cboMaquinaCierre.SetDatos(dvMaquina, "MAQ_CODIGO", "MAQ_NOMBRE", "Seleccione", false);
            string[] nombres = { "Automático", "Manual" };
            int[] valores = { BLL.OrdenProduccionBLL.OrdenAutomatica, BLL.OrdenProduccionBLL.OrdenManual };
            cboModoOPBuscar.SetDatos(nombres, valores, "--TODOS--", true);
            dvEstadoOTBuscar = new DataView(dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO);
            cboEstadoOTFiltrar.SetDatos(dvEstadoOTBuscar, "EORD_CODIGO", "EORD_NOMBRE", "--TODOS--", true);

            //Por defecto cargamos las órdenes de producción del día y que deben iniciarse
            try
            {
                DateTime hoy = BLL.DBBLL.GetFechaServidor();
                BLL.OrdenProduccionBLL.ObtenerOrdenesProduccion(null, BLL.EstadoOrdenTrabajoBLL.ObtenerEstadoGenerada(), null, null, hoy, hoy, dsOrdenTrabajo);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            SetInterface(estadoUI.pestañaProduccion);
        }

        void chkSeleccionarOT_CheckedChanged(object sender, EventArgs e)
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
                        nombre = dsOrdenTrabajo.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    case "ORDP_FECHAINICIOESTIMADA":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "ORDP_FECHAINICIOREAL":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "ORDP_FECHAFINESTIMADA":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "ORDP_FECHAFINREAL":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "EORD_CODIGO":
                        nombre = dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.FindByEORD_CODIGO(Convert.ToInt32(e.Value)).EORD_NOMBRE;
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
                    case "PAR_CODIGO":
                        int codOrden = Convert.ToInt32(e.Value);
                        decimal codParte = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(codOrden).PAR_CODIGO;
                        decimal tipoParte = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(codOrden).PAR_TIPO;
                        //if (tipoParte == BLL.OrdenTrabajoBLL.parteTipoConjunto) { nombre = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codParte).CONJ_CODIGOPARTE; }
                        //else if(tipoParte == BLL.OrdenTrabajoBLL.parteTipoSubconjunto) { nombre = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codParte).SCONJ_CODIGOPARTE; }
                        //else if(tipoParte == BLL.OrdenTrabajoBLL.parteTipoPieza) { nombre = dsEstructura.PIEZAS.FindByPZA_CODIGO(codParte).PZA_CODIGOPARTE; }
                        e.Value = nombre;
                        break;
                    case "ORDT_FECHAINICIOESTIMADA":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "ORDT_FECHAINICIOREAL":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "ORDT_FECHAFINESTIMADA":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "ORDT_FECHAFINREAL":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "EORD_CODIGO":
                        nombre = dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO.FindByEORD_CODIGO(Convert.ToInt32(e.Value)).EORD_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "CTO_CODIGO":
                        nombre = dsHojaRuta.CENTROS_TRABAJOS.FindByCTO_CODIGO(Convert.ToInt32(e.Value)).CTO_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "OPR_NUMERO":
                        nombre = dsHojaRuta.OPERACIONES.FindByOPR_NUMERO(Convert.ToInt32(e.Value)).OPR_CODIGO;
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
                        nombre = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(Convert.ToInt32(e.Value)).ORDT_CODIGO;
                        e.Value = nombre;
                        break;
                    case "E_CODIGO":
                        nombre = dsOrdenTrabajo.EMPLEADOS.FindByE_CODIGO(Convert.ToInt32(e.Value)).E_APELLIDO;
                        nombre += ", " + dsOrdenTrabajo.EMPLEADOS.FindByE_CODIGO(Convert.ToInt32(e.Value)).E_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "MAQ_CODIGO":
                        nombre = dsOrdenTrabajo.MAQUINAS.FindByMAQ_CODIGO(Convert.ToInt32(e.Value)).MAQ_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "CORD_FECHACIERRE":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "CORD_HORACIERRE":
                        nombre = Sistema.FuncionesAuxiliares.DecimalHourToString(Convert.ToDecimal(e.Value));
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
                int numero = Convert.ToInt32(dvOrdenProduccion[dgvOrdenesProduccion.SelectedRows[0].Index]["ordp_numero"]);
                dvOrdenTrabajo.RowFilter = "ORDP_NUMERO = " + numero;
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
                int numero = Convert.ToInt32(dvOrdenTrabajo[dgvOrdenesTrabajo.SelectedRows[0].Index]["ordt_numero"]);
                dvCierreParcial.RowFilter = "ORDT_NUMERO = " + numero;
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
                int codigo = Convert.ToInt32(dvCierreParcial[dgvCierresParciales.SelectedRows[0].Index]["cord_codigo"]);
                cboEmpleadoCierre.SetSelectedValue(Convert.ToInt32(dsOrdenTrabajo.CIERRE_ORDEN_TRABAJO.FindByCORD_CODIGO(codigo).E_CODIGO));
                cboMaquinaCierre.SetSelectedValue(Convert.ToInt32(dsOrdenTrabajo.CIERRE_ORDEN_TRABAJO.FindByCORD_CODIGO(codigo).MAQ_CODIGO));
                nudCantidadCierre.Value = dsOrdenTrabajo.CIERRE_ORDEN_TRABAJO.FindByCORD_CODIGO(codigo).CORD_CANTIDAD;
                dtpFechaCierre.SetFecha(dsOrdenTrabajo.CIERRE_ORDEN_TRABAJO.FindByCORD_CODIGO(codigo).CORD_FECHACIERRE);
                txtObservacionesCierre.Text = dsOrdenTrabajo.CIERRE_ORDEN_TRABAJO.FindByCORD_CODIGO(codigo).CORD_OBSERVACIONES;
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
            dtpHoraCierre.Value = new DateTime(2000, 1, 1, 0, 0, 0);
        }

        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(txtCodigoOPBuscar.GetType())) { (sender as TextBox).SelectAll(); }
            if (sender.GetType().Equals(txtObservacionesCierre.GetType())) { (sender as RichTextBox).SelectAll(); }
            if (sender.GetType().Equals(nudCantidadCierre.GetType())) { (sender as NumericUpDown).Select(0, 20); }
        }

        #endregion Servicios  

    }
}
