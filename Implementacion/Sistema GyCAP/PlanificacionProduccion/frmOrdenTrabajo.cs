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
        private enum estadoUI { inicio, pestañaProduccion, pestañaTrabajo, pestañaCierreParcial };
        private estadoUI estadoInterface;
        private Data.dsOrdenTrabajo dsOrdenTrabajo = new GyCAP.Data.dsOrdenTrabajo();
        private Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
        private Data.dsHojaRuta dsHojaRuta = new GyCAP.Data.dsHojaRuta();
        private DataView dvOrdenProduccion, dvOrdenTrabajo, dvCierreParcial, dvEmpleado, dvMaquina;
        private DataView dvEstadoOPBuscar, dvEstadoOTBuscar;
        

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

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsOrdenTrabajo.ORDENES_PRODUCCION.Rows.Count == 0)
                    {
                        hayDatos = false;
                    }
                    else
                    {
                        hayDatos = true;
                    }

                    //btnFinalizar.Enabled = hayDatos;
                    //btnCancelar.Enabled = hayDatos;
                    //btnCierreParcial.Enabled = hayDatos;
                    //btnIniciar.Enabled = hayDatos;
                    estadoInterface = estadoUI.inicio;
                    tcOrdenTrabajo.SelectedTab = tpOrdenesProduccion;
                    break;
                case estadoUI.pestañaProduccion:
                    //txtNombre.ReadOnly = false;
                    //txtDescripcion.ReadOnly = false;
                    //txtNombre.Text = String.Empty;
                    //txtDescripcion.Text = String.Empty;
                    //btnGuardarCierre.Enabled = true;
                    //btnVolver.Enabled = true;
                    //btnIniciar.Enabled = false;
                    //btnCierreParcial.Enabled = false;
                    //btnFinalizar.Enabled = false;
                    //btnCancelar.Enabled = false;
                    estadoInterface = estadoUI.pestañaProduccion;
                    tcOrdenTrabajo.SelectedTab = tpOrdenesTrabajo;
                    break;
                case estadoUI.pestañaTrabajo:
                    //txtNombre.ReadOnly = true;
                    //txtDescripcion.ReadOnly = true;
                    //btnGuardarCierre.Enabled = false;
                    //btnVolver.Enabled = true;
                    estadoInterface = estadoUI.pestañaTrabajo;
                    tcOrdenTrabajo.SelectedTab = tpOrdenesTrabajo;
                    break;
                case estadoUI.pestañaCierreParcial:
                    //txtNombre.ReadOnly = false;
                    //txtDescripcion.ReadOnly = false;
                    //btnGuardarCierre.Enabled = true;
                    //btnVolver.Enabled = true;
                    //btnIniciar.Enabled = false;
                    //btnCierreParcial.Enabled = false;
                    //btnFinalizar.Enabled = false;
                    //btnCancelar.Enabled = false;
                    estadoInterface = estadoUI.pestañaCierreParcial;
                    tcOrdenTrabajo.SelectedTab = tpOrdenesTrabajo;
                    break;
                default:
                    break;
            }
        }

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
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            //Dataviews - ordenar grillas, agregar bloqueo columnas, agregar routing y umed a ordenes - gonzalo
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
            
            SetInterface(estadoUI.inicio);
        }

        #region CellFormattings

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

        #endregion CellFormatings

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (tcOrdenTrabajo.SelectedTab == tpOrdenesProduccion)
            {
                if (dgvOrdenesProduccion.SelectedRows.Count > 0)
                {
                    int numeroOrdenOP = Convert.ToInt32(dvOrdenProduccion[dgvOrdenesProduccion.SelectedRows[0].Index]["ordp_numero"]);
                    if (dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenOP).EORD_CODIGO == BLL.OrdenProduccionBLL.EstadoGenerado)
                    {
                        try
                        {
                            BLL.OrdenProduccionBLL.IniciarOrdenProduccion(numeroOrdenOP, BLL.DBBLL.GetFechaServidor(), dsOrdenTrabajo);
                        }
                        catch (Entidades.Excepciones.BaseDeDatosException ex)
                        {
                            MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio Orden de Producción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else if (tcOrdenTrabajo.SelectedTab == tpOrdenesTrabajo)
            {
                if (dgvOrdenesTrabajo.SelectedRows.Count > 0)
                {
                    int numeroOrdenOT = Convert.ToInt32(dvOrdenTrabajo[dgvOrdenesTrabajo.SelectedRows[0].Index]["ordt_numero"]);
                    if (dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenOT).EORD_CODIGO == BLL.OrdenTrabajoBLL.EstadoGenerado)
                    {
                        //preguntar si desea forzar el inicio de la orden de trabajo
                    }
                }
            }
        }

        

        #endregion Servicios
    }
}
