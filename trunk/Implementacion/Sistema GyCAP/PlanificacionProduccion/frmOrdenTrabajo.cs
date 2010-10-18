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
    public partial class frmOrdenTrabajo : Form
    {
        private static frmOrdenTrabajo _frmOrdenTrabajo = null;
        private enum estadoUI { pestañaProduccion, pestañaTrabajo, pestañaCierreParcial };
        private estadoUI estadoInterface;
        private Data.dsOrdenTrabajo dsOrdenTrabajo = new GyCAP.Data.dsOrdenTrabajo();
        private Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
        private Data.dsHojaRuta dsHojaRuta = new GyCAP.Data.dsHojaRuta();
        private Data.dsStock dsStock = new GyCAP.Data.dsStock();
        private DataView dvOrdenProduccion, dvOrdenTrabajo, dvCierreParcial, dvEmpleado, dvMaquina;
        private DataView dvEstadoOPBuscar, dvEstadoOTBuscar;
        private int columnIndexProduccion = -1, columnIndexTrabajo = -1, columnIndexCierre = -1;

        #region Inicio

        public frmOrdenTrabajo()
        {
            InitializeComponent();
            Inicializar();
        }

        public static frmOrdenTrabajo Instancia
        {
            get
            {
                if (_frmOrdenTrabajo == null || _frmOrdenTrabajo.IsDisposed)
                {
                    _frmOrdenTrabajo = new frmOrdenTrabajo();
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
                            BLL.OrdenProduccionBLL.IniciarOrdenProduccion(numeroOrdenOP, BLL.DBBLL.GetFechaServidor(), dsOrdenTrabajo, dsStock, dsHojaRuta, dsEstructura);
                            int o = 0;
                        }
                        catch (Entidades.Excepciones.BaseDeDatosException ex)
                        {
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
        
        private void btnCierreParcial_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.pestañaCierreParcial);
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

        #endregion

        #region Cierres Parciales

        private void btnAgregarCierre_Click(object sender, EventArgs e)
        {
            if (dgvOrdenesTrabajo.SelectedRows.Count > 0)
            {
                gbAgregarCierreParcial.Enabled = true;
                LimpiarDatosCierre();
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
                    btnCierreParcial.Enabled = false;
                    btnFinalizar.Enabled = hayDatos;
                    btnCancelar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    estadoInterface = estadoUI.pestañaProduccion;
                    tcOrdenTrabajo.SelectedTab = tpOrdenesProduccion;
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
                    btnCierreParcial.Enabled = hayDatos;
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
            dgvOrdenesTrabajo.AutoGenerateColumns = false;
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
            dgvOrdenesTrabajo.Columns["ORDT_CANTIDADESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrdenesTrabajo.Columns["ORDT_CANTIDADREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAINICIOESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAINICIOREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAFINESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrdenesTrabajo.Columns["ORDT_FECHAFINREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            //Grilla cierres parciales
            dgvCierresParciales.AutoGenerateColumns = false;
            dgvCierresParciales.Columns.Add("ORDT_NUMERO", "Orden Trabajo");
            dgvCierresParciales.Columns.Add("E_CODIGO", "Empleado");
            dgvCierresParciales.Columns.Add("MAQ_CODIGO", "Máquina");
            dgvCierresParciales.Columns.Add("CORD_CANTIDAD", "Cantidad");
            dgvCierresParciales.Columns.Add("CORD_FECHACIERRE", "Fecha");
            dgvCierresParciales.Columns.Add("CORD_HORACIERRE", "Hora");
            dgvCierresParciales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvCierresParciales.AutoGenerateColumns = false;
            dgvCierresParciales.Columns["ORDT_NUMERO"].DataPropertyName = "ORDT_NUMERO";
            dgvCierresParciales.Columns["E_CODIGO"].DataPropertyName = "E_CODIGO";
            dgvCierresParciales.Columns["MAQ_CODIGO"].DataPropertyName = "MAQ_CODIGO";
            dgvCierresParciales.Columns["CORD_CANTIDAD"].DataPropertyName = "CORD_CANTIDAD";
            dgvCierresParciales.Columns["CORD_FECHACIERRE"].DataPropertyName = "CORD_FECHACIERRE";
            dgvCierresParciales.Columns["CORD_HORACIERRE"].DataPropertyName = "CORD_HORACIERRE";
            dgvCierresParciales.Columns["CORD_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCierresParciales.Columns["CORD_FECHACIERRE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCierresParciales.Columns["CORD_HORACIERRE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //Carga de datos iniciales desde la BD
            try
            {
                BLL.EmpleadoBLL.ObtenerEmpleados(dsOrdenTrabajo.EMPLEADOS);
                BLL.MaquinaBLL.ObtenerMaquinas(dsOrdenTrabajo.MAQUINAS);
                BLL.CocinaBLL.ObtenerCocinas(dsOrdenTrabajo.COCINAS);
                BLL.EstadoOrdenTrabajoBLL.ObtenerEstadosOrden(dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO);
                BLL.CentroTrabajoBLL.ObetenerCentrosTrabajo(null, null, null, null, dsHojaRuta.CENTROS_TRABAJOS);
                BLL.OperacionBLL.ObetenerOperaciones(dsHojaRuta.OPERACIONES);
                BLL.ConjuntoBLL.ObtenerConjuntos(dsEstructura.CONJUNTOS);
                BLL.SubConjuntoBLL.ObtenerSubconjuntos(dsEstructura.SUBCONJUNTOS);
                BLL.PiezaBLL.ObtenerPiezas(dsEstructura.PIEZAS);
                BLL.EstadoMovimientoStockBLL.ObtenerEstadosMovimiento(dsStock.ESTADO_MOVIMIENTOS_STOCK);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            //Dataviews - ordenar grillas, agregar routing y umed a ordenes - gonzalo
            dvOrdenProduccion = new DataView(dsOrdenTrabajo.ORDENES_PRODUCCION);
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
                        if (tipoParte == BLL.OrdenTrabajoBLL.parteTipoConjunto) { nombre = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codParte).CONJ_CODIGOPARTE; }
                        else if(tipoParte == BLL.OrdenTrabajoBLL.parteTipoSubconjunto) { nombre = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(codParte).SCONJ_CODIGOPARTE; }
                        else if(tipoParte == BLL.OrdenTrabajoBLL.parteTipoPieza) { nombre = dsEstructura.PIEZAS.FindByPZA_CODIGO(codParte).PZA_CODIGOPARTE; }
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
            if (columnIndexTrabajo != -1) { dgvOrdenesTrabajo.Columns[columnIndexTrabajo].Frozen = true; }
        }

        private void tsmiDesbloquearTrabajo_Click(object sender, EventArgs e)
        {
            if (columnIndexTrabajo != -1) { dgvOrdenesTrabajo.Columns[columnIndexTrabajo].Frozen = false; }
        }

        private void dgvOrdenesTrabajo_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1)
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

        

        #endregion Servicios

        

    }
}
