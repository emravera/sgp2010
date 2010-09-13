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
        public static readonly int estadoInicialNuevoAutmatico = 1; //Para generar las OT de forma automática
        public static readonly int estadoInicialNuevoManual = 2; //Para generar OT de forma manual
        Data.dsPlanSemanal dsPlanSemanal = new GyCAP.Data.dsPlanSemanal();
        Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
        Data.dsOrdenTrabajo dsOrdenTrabajo = new GyCAP.Data.dsOrdenTrabajo();
        Data.dsHojaRuta dsHojaRuta = new GyCAP.Data.dsHojaRuta();
        DataView dvPlanAnual, dvMensual, dvPlanSemanal, dvOrdenProduccion, dvHojaRuta, dvEstructura, dvOrdenTrabajo;
        private int columnIndex = -1;
        BindingSource sourceOrdenTrabajo = new BindingSource();
        private Sistema.ControlesUsuarios.AnimadorFormulario animador = new GyCAP.UI.Sistema.ControlesUsuarios.AnimadorFormulario();
        private TreeView tvDependenciaSimple, tvDependenciaCompleta, tvOrdenesYEstructura;

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
            this.Close();
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
                    cbMesBuscar.SetDatos(dvMensual, "PMES_CODIGO", "PMES_MES", "PMES_CODIGO ASC", "Seleccione", false);
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

        private void dgvListaOrdenTrabajo_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigo = Convert.ToInt32(dvOrdenProduccion[e.RowIndex]["ordp_numero"]);
            dvOrdenTrabajo.RowFilter = "ordp_numero = " + codigo;
            sourceOrdenTrabajo.DataSource = dvOrdenTrabajo;
            bnNavegador.BindingSource = sourceOrdenTrabajo;
            txtNumeroOrdenP.Text = (codigo * -1).ToString();
            txtCodigoOrdenP.Text = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigo).ORDP_CODIGO;
            txtOrigenOrdenP.Text = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigo).ORDP_ORIGEN;
            txtEstadoOrdenP.Text = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigo).ESTADO_ORDENES_TRABAJORow.EORD_NOMBRE;
            dtpFechaAltaOrdenP.SetFecha(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigo).ORDP_FECHAALTA);
            int codigoPlan = Convert.ToInt32(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigo).DPSEM_CODIGO);
            txtCocinaOrdenP.Text = dsPlanSemanal.DETALLE_PLANES_SEMANALES.FindByDPSEM_CODIGO(codigoPlan).COCINASRow.COC_CODIGO_PRODUCTO;
            nudCantidadOrdenP.Value = dsPlanSemanal.DETALLE_PLANES_SEMANALES.FindByDPSEM_CODIGO(codigoPlan).DPSEM_CANTIDADESTIMADA;
            if (!dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigo).IsORDP_FECHAINICIOESTIMADANull())
            { dtpFechaInicioOrdenP.SetFecha(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigo).ORDP_FECHAINICIOESTIMADA); }

            if (!dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigo).IsORDP_FECHAFINESTIMADANull())
            { dtpFechaFinOrdenP.SetFecha(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(codigo).ORDP_FECHAFINESTIMADA); }

            CompletarDatosOrdenTrabajo();
        }

        #endregion

        #region Generar órdenes

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (tvDetallePlan.SelectedNode != null && ((tipoNodo)tvDetallePlan.SelectedNode.Tag) == tipoNodo.dia)
            {
                try
                {
                    BLL.OrdenProduccionBLL.GenerarOrdenTrabajoDia(Convert.ToInt32(tvDetallePlan.SelectedNode.Name), dsPlanSemanal, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                }
                catch (Entidades.Excepciones.BaseDeDatosException ex) { MessageBox.Show(ex.Message); }
                catch (Entidades.Excepciones.OrdenTrabajoException ex) { MessageBox.Show(ex.Message); }

                dvOrdenProduccion.Table = dsOrdenTrabajo.ORDENES_PRODUCCION;
                dvOrdenTrabajo.Table = dsOrdenTrabajo.ORDENES_TRABAJO;
                dgvListaOrdenProduccion.SelectedRows[0].Selected = false;
                foreach (TreeNode nodo in tvDetallePlan.SelectedNode.Nodes)
                {
                    nodo.ForeColor = Color.Black;
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un día de la Semana.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Datos

        private void btnDetalleOrden_Click(object sender, EventArgs e)
        {
            if (dgvListaOrdenProduccion.SelectedRows.Count > 0) { SetInterface(estadoUI.consultar); }
            else { MessageBox.Show("Debe seleccionar una Orden de Trabajo de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            tcOrdenTrabajo.SelectedTab = tpBuscar;
        }

        private void CompletarDatosOrdenTrabajo()
        {
            if (sourceOrdenTrabajo.Count > 0)
            {
                Data.dsOrdenTrabajo.ORDENES_TRABAJORow row = (Data.dsOrdenTrabajo.ORDENES_TRABAJORow)(sourceOrdenTrabajo.Current as DataRowView).Row;
                txtNumeroOrdenT.Text = (row.ORDT_NUMERO * -1).ToString();
                txtCodigoOrdenT.Text = row.ORDT_CODIGO;
                txtOrigenOrdenT.Text = row.ORDT_ORIGEN;
                if (row.PAR_TIPO == BLL.OrdenProduccionBLL.parteTipoConjunto) 
                { 
                    txtTipoParteOrdenT.Text = "Conjunto";
                    txtParteOrdenT.Text = dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(Convert.ToInt32(row.PAR_CODIGO)).CONJ_CODIGOPARTE;
                    int hoja = Convert.ToInt32(dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(Convert.ToInt32(row.PAR_CODIGO)).HR_CODIGO);
                    txtHojaRuta.Text = dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(hoja).HR_NOMBRE;
                }
                else if (row.PAR_TIPO == BLL.OrdenProduccionBLL.parteTipoSubconjunto) 
                { 
                    txtTipoParteOrdenT.Text = "Subconjunto";
                    txtParteOrdenT.Text = dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(Convert.ToInt32(row.PAR_CODIGO)).SCONJ_CODIGOPARTE;
                    int hoja = Convert.ToInt32(dsEstructura.SUBCONJUNTOS.FindBySCONJ_CODIGO(Convert.ToInt32(row.PAR_CODIGO)).HR_CODIGO);
                    txtHojaRuta.Text = dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(hoja).HR_NOMBRE;
                }
                else if (row.PAR_TIPO == BLL.OrdenProduccionBLL.parteTipoPieza)
                { 
                    txtTipoParteOrdenT.Text = "Pieza";
                    txtParteOrdenT.Text = dsEstructura.PIEZAS.FindByPZA_CODIGO(Convert.ToInt32(row.PAR_CODIGO)).PZA_CODIGOPARTE;
                    int hoja = Convert.ToInt32(dsEstructura.PIEZAS.FindByPZA_CODIGO(Convert.ToInt32(row.PAR_CODIGO)).HR_CODIGO);
                    txtHojaRuta.Text = dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(hoja).HR_NOMBRE;
                }
                txtCantidadOrdenT.Text = row.ORDT_CANTIDADESTIMADA.ToString();
                txtCentroTrabajo.Text = dsHojaRuta.CENTROS_TRABAJOS.FindByCTO_CODIGO(Convert.ToInt32(row.CTO_CODIGO)).CTO_NOMBRE;
                txtOperacion.Text = dsHojaRuta.OPERACIONES.FindByOPR_NUMERO(Convert.ToInt32(row.OPR_NUMERO)).OPR_NOMBRE;
                txtObservacionesOrdenT.Text = row.ORDT_OBSERVACIONES;

                if (!row.IsORDT_FECHAINICIOESTIMADANull()) { dtpFechaInicioOrdenT.SetFecha(row.ORDT_FECHAINICIOESTIMADA); }
                if (!row.IsORDT_HORAFINESTIMADANull()) { dtpFechaFinOrdenT.SetFecha(row.ORDT_FECHAFINESTIMADA); }
            }
        }

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

        #endregion

        #region Servicios

        //Setea la pantalla de acuerdo al estado en que se encuentre
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.nuevoAutomatico:
                   
                    estadoInterface = estadoUI.nuevoAutomatico;
                    dtpFechaPlanear.SetFechaNull();
                    
                    tcOrdenTrabajo.SelectedTab = tpBuscar;
                    break;
                case estadoUI.nuevoManual:
                    
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    
                    
                    estadoInterface = estadoUI.nuevoManual;
                    //tcOrdenTrabajo.SelectedTab = tpOrdenManual;
                    break;
                case estadoUI.consultar:
                case estadoUI.modificar:
                    
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    
                    
                    estadoInterface = estadoUI.modificar;
                    tcOrdenTrabajo.SelectedTab = tpOrdenProduccion;
                    break;
                default:
                    break;
            }
        }

        private void InicializarDatos()
        {
            //Grilla ordenes trabajo
            dgvListaOrdenProduccion.AutoGenerateColumns = false;
            dgvListaOrdenProduccion.Columns.Add("ORDP_CODIGO", "Código");
            dgvListaOrdenProduccion.Columns.Add("ORDP_FECHAALTA", "Fecha creación");
            dgvListaOrdenProduccion.Columns.Add("ORDP_ORIGEN", "Origen");
            dgvListaOrdenProduccion.Columns.Add("COCINA", "Cocina");
            dgvListaOrdenProduccion.Columns.Add("CANTIDAD", "Cantidad");
            dgvListaOrdenProduccion.Columns.Add("ORDP_FECHAINICIOESTIMADA", "Fecha inicio");
            dgvListaOrdenProduccion.Columns.Add("ORDP_FECHAFINESTIMADA", "Fecha fin");
            dgvListaOrdenProduccion.Columns.Add("EORD_CODIGO", "Estado");
            dgvListaOrdenProduccion.Columns["ORDP_CODIGO"].DataPropertyName = "ORDP_CODIGO";
            dgvListaOrdenProduccion.Columns["ORDP_FECHAALTA"].DataPropertyName = "ORDP_FECHAALTA";
            dgvListaOrdenProduccion.Columns["ORDP_ORIGEN"].DataPropertyName = "ORDP_ORIGEN";
            dgvListaOrdenProduccion.Columns["COCINA"].DataPropertyName = "DPSEM_CODIGO";
            dgvListaOrdenProduccion.Columns["CANTIDAD"].DataPropertyName = "DPSEM_CODIGO";
            dgvListaOrdenProduccion.Columns["ORDP_FECHAINICIOESTIMADA"].DataPropertyName = "ORDP_FECHAINICIOESTIMADA";
            dgvListaOrdenProduccion.Columns["ORDP_FECHAFINESTIMADA"].DataPropertyName = "ORDP_FECHAFINESTIMADA";
            dgvListaOrdenProduccion.Columns["EORD_CODIGO"].DataPropertyName = "EORD_CODIGO";
            dgvListaOrdenProduccion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvListaOrdenProduccion.AllowUserToResizeColumns = true;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAALTA"].MinimumWidth = 110;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAINICIOESTIMADA"].MinimumWidth = 90;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAFINESTIMADA"].MinimumWidth = 90;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAALTA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAINICIOESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaOrdenProduccion.Columns["ORDP_FECHAFINESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaOrdenProduccion.Columns["CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

            //Dataviews y combos
            dvPlanAnual = new DataView(dsPlanSemanal.PLANES_ANUALES);
            dvMensual = new DataView(dsPlanSemanal.PLANES_MENSUALES);
            dvPlanSemanal = new DataView(dsPlanSemanal.PLANES_SEMANALES);
            dvOrdenProduccion = new DataView(dsOrdenTrabajo.ORDENES_PRODUCCION);
            dgvListaOrdenProduccion.DataSource = dvOrdenProduccion;
            dvOrdenTrabajo = new DataView(dsOrdenTrabajo.ORDENES_TRABAJO);
            string[] nombres = { "Hacia adelante", "Hacia atrás" };
            int[] valores = { 0, 1 };
            cbModoPlanearFecha.SetDatos(nombres, valores, "Seleccione", false);

            try
            {
                BLL.PlanAnualBLL.ObtenerTodos(dsPlanSemanal.PLANES_ANUALES);
                BLL.CocinaBLL.ObtenerCocinas(dsPlanSemanal.COCINAS);
                BLL.EstadoOrdenTrabajoBLL.ObtenerEstadosOrden(dsOrdenTrabajo.ESTADO_ORDENES_TRABAJO);                
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Generar Orden de Producción - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //Sistema.FuncionesAuxiliares.QuitarCheckBox(nodoSemana, tvDetallePlan);

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
                    if (Convert.ToInt32(rowDetalle.DPSEM_ESTADO) == BLL.DetallePlanSemanalBLL.estadoGenerado) { nodoDetalle.ForeColor = Color.Red; }
                    else if (Convert.ToInt32(rowDetalle.DPSEM_ESTADO) == BLL.DetallePlanSemanalBLL.estadoModificado) { nodoDetalle.ForeColor = Color.Yellow; }
                    else if (Convert.ToInt32(rowDetalle.DPSEM_ESTADO) == BLL.DetallePlanSemanalBLL.estadoConOrden) { nodoDetalle.ForeColor = Color.Black; }
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
                switch (dgvListaOrdenProduccion.Columns[e.ColumnIndex].Name)
                {
                    case "ORDP_FECHAALTA":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "COCINA":
                        nombre = dsPlanSemanal.DETALLE_PLANES_SEMANALES.FindByDPSEM_CODIGO(Convert.ToInt32(e.Value.ToString())).COCINASRow.COC_CODIGO_PRODUCTO;
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
            if (columnIndex != -1) { dgvListaOrdenProduccion.Columns[columnIndex].Frozen = true; }
        }

        private void tsmiDesbloquearColumna_Click(object sender, EventArgs e)
        {
            if (columnIndex != -1) { dgvListaOrdenProduccion.Columns[columnIndex].Frozen = false; }
        }

        private void dgvListaOrdenTrabajo_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
                cmsGrillaOrdenesTrabajo.Show(MousePosition);
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
                    BLL.OrdenProduccionBLL.GenerarArbolOrdenes(codOrdenP, tvDependenciaSimple, tvDependenciaCompleta, tvOrdenesYEstructura, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
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

        private void btnCalcularFechas_Click(object sender, EventArgs e)
        {
            if (dgvListaOrdenProduccion.SelectedRows.Count > 0)
            {
                if (cbModoPlanearFecha.GetSelectedIndex() != -1 && !dtpFechaPlanear.EsFechaNull())
                {
                    int codigoP = Convert.ToInt32(dvOrdenProduccion[dgvListaOrdenProduccion.SelectedRows[0].Index]["ordp_numero"].ToString());
                    tvDependenciaSimple = frmArbolOrdenesTrabajo.Instancia.GetArbolDependenciaSimple();
                    tvDependenciaCompleta = frmArbolOrdenesTrabajo.Instancia.GetArbolDependenciaCompleta();
                    tvOrdenesYEstructura = frmArbolOrdenesTrabajo.Instancia.GetArbolOrdenesYEstructura();
                    BLL.OrdenProduccionBLL.GenerarArbolOrdenes(codigoP, tvDependenciaSimple, tvDependenciaCompleta, tvOrdenesYEstructura, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                    if (cbModoPlanearFecha.GetSelectedValueInt() == 0)
                    {
                        //Planeamos hacia adelante
                        DateTime fecha = DateTime.Parse(DateTime.Parse(dtpFechaPlanear.GetFecha().ToString()).ToShortDateString());
                        BLL.OrdenProduccionBLL.PlanearFechaHaciaDelante(codigoP, fecha, tvDependenciaCompleta, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                        dvOrdenTrabajo.Table = dsOrdenTrabajo.ORDENES_TRABAJO;
                        sourceOrdenTrabajo.DataSource = dvOrdenTrabajo;
                        CompletarDatosOrdenTrabajo();
                    }
                    else
                    {
                        //Planeamos hacia atrás
                        DateTime fecha = DateTime.Parse(DateTime.Parse(dtpFechaPlanear.GetFecha().ToString()).ToShortDateString());
                        BLL.OrdenProduccionBLL.PlanearFechaHaciaAtras(codigoP, fecha, tvDependenciaCompleta, dsOrdenTrabajo, dsEstructura, dsHojaRuta);
                        dvOrdenTrabajo.Table = dsOrdenTrabajo.ORDENES_TRABAJO;
                        sourceOrdenTrabajo.DataSource = dvOrdenTrabajo;
                        CompletarDatosOrdenTrabajo();
                    }
                }
                else { MessageBox.Show("Debe seleccionar un modo de planeamiento y una fecha.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            else { MessageBox.Show("Debe seleccionar una Orden de Producción de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
                    
        #endregion

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Las órdenes fueron guardadas correctamente.", "Información: Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
