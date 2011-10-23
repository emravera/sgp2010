using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.UI.Sistema.Validaciones;

namespace GyCAP.UI.PlanificacionProduccion
{
    public partial class frmPlanMensual : Form
    {
        private static frmPlanMensual _frmPlanMensual = null;
        private Data.dsPlanMensual dsPlanMensual = new GyCAP.Data.dsPlanMensual();
        private DataView dvListaPlanes, dvListaDetalle, dvListaDatos, dvComboPlanesAnuales, dvComboCocinas, dvListaPedidos, dvListaDetallePedido;
        private enum estadoUI { inicio, nuevo, buscar, modificar, cargaDetalle };
        private static estadoUI estadoActual;
        private static int cantidadPlanificada; int codigoDetalle = -1;
        private static bool seleccionPestaña = false, checkeoExcepciones = false;
                
        #region Inicio

        public frmPlanMensual()
        {
            InitializeComponent();

            //Inicializamos las grillas
            dgvDetalle.AutoGenerateColumns = false;
            dgvLista.AutoGenerateColumns = false;
            dgvDatos.AutoGenerateColumns = false;
            dgvPedidos.AutoGenerateColumns = false;
            dgvDetallePedido.AutoGenerateColumns = false;

            //Para cada Lista
            //Agregamos la columnas
            dgvLista.Columns.Add("PMES_CODIGO", "Código");
            dgvLista.Columns.Add("PAN_CODIGO", "Plan Anual");
            dgvLista.Columns.Add("PAN_ANIO", "Año Plan Anual");
            dgvLista.Columns.Add("PMES_MES", "Mes del Plan Mensual");
            dgvLista.Columns.Add("PMES_FECHACREACION", "Fecha Creación Plan Mensual");
                   
            //Oculto la columnas que no se van a mostrar
            dgvLista.Columns["PMES_CODIGO"].Visible = false;
            dgvLista.Columns["PAN_CODIGO"].Visible = false;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["PMES_CODIGO"].DataPropertyName = "PMES_CODIGO";
            dgvLista.Columns["PAN_CODIGO"].DataPropertyName = "PAN_CODIGO";
            dgvLista.Columns["PAN_ANIO"].DataPropertyName = "PAN_ANIO";
            dgvLista.Columns["PMES_MES"].DataPropertyName = "PMES_MES";
            dgvLista.Columns["PMES_FECHACREACION"].DataPropertyName = "PMES_FECHACREACION";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPlanes = new DataView(dsPlanMensual.PLANES_MENSUALES);
            dgvLista.DataSource = dvListaPlanes;

            //Lista de Detalles
            //Agregamos la columnas
            dgvDetalle.Columns.Add("DPMES_CODIGO", "Código");
            dgvDetalle.Columns.Add("PMES_CODIGO", "Mes");
            dgvDetalle.Columns.Add("COC_CODIGO", "Cocina");
            dgvDetalle.Columns.Add("DPMES_CANTIDADESTIMADA", "Cantidad Estimada");
            dgvDetalle.Columns.Add("DPMES_CANTIDADREAL", "Cantidad Real");
            dgvDetalle.Columns.Add("DPED_CODIGO", "Pedido");
            dgvDetalle.Columns.Add("DPED_FECHA_INICIO", "Fecha Prod.");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetalle.Columns["DPMES_CODIGO"].DataPropertyName = "DPMES_CODIGO";
            dgvDetalle.Columns["PMES_CODIGO"].DataPropertyName = "PMES_CODIGO";
            dgvDetalle.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDetalle.Columns["DPMES_CANTIDADESTIMADA"].DataPropertyName = "DPMES_CANTIDADESTIMADA";
            dgvDetalle.Columns["DPMES_CANTIDADREAL"].DataPropertyName = "DPMES_CANTIDADREAL";
            dgvDetalle.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";
            dgvDetalle.Columns["DPED_FECHA_INICIO"].DataPropertyName = "DPED_FECHA_INICIO";
     
            //Seteamos las alineaciones de las columnas
            dgvDetalle.Columns["DPMES_CANTIDADESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalle.Columns["DPMES_CANTIDADREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalle.Columns["DPED_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            //Ocultamos las columnas de Detalle de Pedido
            dgvDetalle.Columns["DPMES_CODIGO"].Visible = false;
            dgvDetalle.Columns["PMES_CODIGO"].Visible = false;
            dgvDetalle.Columns["DPMES_CANTIDADREAL"].Visible = false;

             //Creamos el dataview y lo asignamos a la grilla
            dvListaDetalle = new DataView(dsPlanMensual.DETALLE_PLANES_MENSUALES);
            dgvDetalle.DataSource = dvListaDetalle;

            //******************************************************************************************
            //                                      Lista de Datos
            //******************************************************************************************

            //Agregamos la columnas
            dgvDatos.Columns.Add("DPMES_CODIGO", "Código");
            dgvDatos.Columns.Add("PMES_CODIGO", "Mes");
            dgvDatos.Columns.Add("COC_CODIGO", "Cocina");
            dgvDatos.Columns.Add("DPMES_CANTIDADESTIMADA", "Cantidad Estimada");
            dgvDatos.Columns.Add("DPMES_CANTIDADREAL", "Cantidad Real");
            dgvDatos.Columns.Add("DPED_CODIGO", "Pedido");
            dgvDatos.Columns.Add("DPED_FECHA_INICIO", "Fecha Prod.");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDatos.Columns["DPMES_CODIGO"].DataPropertyName = "DPMES_CODIGO";
            dgvDatos.Columns["PMES_CODIGO"].DataPropertyName = "PMES_CODIGO";
            dgvDatos.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDatos.Columns["DPMES_CANTIDADESTIMADA"].DataPropertyName = "DPMES_CANTIDADESTIMADA";
            dgvDatos.Columns["DPMES_CANTIDADREAL"].DataPropertyName = "DPMES_CANTIDADREAL";
            dgvDatos.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";
            dgvDatos.Columns["DPED_FECHA_INICIO"].DataPropertyName = "DPED_FECHA_INICIO";

            //Seteamos la alineacion de las columnas numericas
            dgvDatos.Columns["DPMES_CANTIDADESTIMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns["DPMES_CANTIDADREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns["DPED_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            //Creamos el dataview y lo asignamos a la grilla
            dvListaDatos = new DataView(dsPlanMensual.DETALLE_PLANES_MENSUALES);
            dgvDatos.DataSource = dvListaDatos;

            //*********************************** Lista de Pedidos *****************************************
            //Agregamos la columnas
            dgvPedidos.Columns.Add("PED_NUMERO", "Número");
            dgvPedidos.Columns.Add("CLI_CODIGO", "Cliente");
            dgvPedidos.Columns.Add("EPED_CODIGO", "Estado");
            dgvPedidos.Columns.Add("PED_FECHA_ALTA", "Fecha Alta");                         

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvPedidos.Columns["PED_NUMERO"].DataPropertyName = "PED_NUMERO";
            dgvPedidos.Columns["CLI_CODIGO"].DataPropertyName = "CLI_CODIGO";
            dgvPedidos.Columns["EPED_CODIGO"].DataPropertyName = "EPED_CODIGO";
            dgvPedidos.Columns["PED_FECHA_ALTA"].DataPropertyName = "PED_FECHA_ALTA";
            
            //Seteamos la alineacion de las columnas numericas
            dgvPedidos.Columns["PED_NUMERO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPedidos = new DataView(dsPlanMensual.PEDIDOS);
            dgvPedidos.DataSource = dvListaPedidos;

            //*********************************** Lista de Detalle de Pedidos *****************************************
            //Agregamos la columnas
            dgvDetallePedido.Columns.Add("DPED_CODIGO", "Núm. Detalle");
            dgvDetallePedido.Columns.Add("COC_CODIGO", "Cocina");
            dgvDetallePedido.Columns.Add("DPED_CANTIDAD", "Cantidad");
            dgvDetallePedido.Columns.Add("DPED_FECHA_INICIO", "Fecha Inicio");
            
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetallePedido.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";
            dgvDetallePedido.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDetallePedido.Columns["DPED_CANTIDAD"].DataPropertyName = "DPED_CANTIDAD";
            dgvDetallePedido.Columns["DPED_FECHA_INICIO"].DataPropertyName = "DPED_FECHA_INICIO";
                       
            //Seteamos la alineación de las columnas numéricas
            dgvDetallePedido.Columns["DPED_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetallePedido.Columns["DPED_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            //Creamos el dataview y lo asignamos a la grilla
            dvListaDetallePedido = new DataView(dsPlanMensual.DETALLE_PEDIDOS);
            dgvDetallePedido.DataSource = dvListaDetallePedido;
            
            //LLenado de Datasets
            //Llenamos el dataset con los planes anuales
            BLL.PlanAnualBLL.ObtenerTodos(dsPlanMensual.PLANES_ANUALES);

            //Llenamos el detalle del Plan Anual
            BLL.DetallePlanAnualBLL.ObtenerDetalle(dsPlanMensual.DETALLE_PLAN_ANUAL);

            //Llenamos el dataset de Cocinas
            BLL.CocinaBLL.ObtenerCocinas(dsPlanMensual.COCINAS);

            //Llenamos el dataset de Clientes
            BLL.ClienteBLL.ObtenerTodos(dsPlanMensual.CLIENTES);

            //Llenamos el datatable con los estados de pedidos
            BLL.EstadoPedidoBLL.ObtenerTodos(dsPlanMensual.ESTADO_PEDIDOS);

            //Llenamos el datatable con los estados de detalle Pedido
            BLL.EstadoDetallePedidoBLL.ObtenerTodos(dsPlanMensual.ESTADO_DETALLE_PEDIDOS);

            //Cargamos el combo de los meses
            string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            int cont = 0; int[] valores = new int[12];

            foreach (string l in Meses)
            {
                valores[cont] = cont;
                cont++;
            }
            
            //Cargamos el combo de los planes anuales
            //Cargamos el combo
            dvComboPlanesAnuales = new DataView(dsPlanMensual.PLANES_ANUALES);
            cbPlanAnual.SetDatos(dvComboPlanesAnuales, "pan_codigo", "pan_anio", "Seleccione", false);

            //Cargo el combo con las cocinas
            dvComboCocinas = new DataView(dsPlanMensual.COCINAS);
            cbCocinas.SetDatos(dvComboCocinas, "coc_codigo", "coc_codigo_producto", "Seleccione", false);

            //Cargo los combos de los meses
            cbMes.SetDatos(Meses, valores, "Seleccione", false);
            cbMesDatos.SetDatos(Meses, valores, "Seleccione", false);
            
            //Seteamos los maxlength de los controles y los tipos de numeros
            txtAnioBuscar.MaxLength = 4;

            //Seteamos los valores máximos de los numeric
            numPorcentaje.Tag = new Sistema.Validaciones.NumericLimitValues("0", NumericLimitValues.IncludeExclude.Inclusivo, "100", NumericLimitValues.IncludeExclude.Inclusivo);
            numUnidades.Tag = new Sistema.Validaciones.NumericLimitValues("0", NumericLimitValues.IncludeExclude.Inclusivo, "9000000", NumericLimitValues.IncludeExclude.Inclusivo);

            //Setemoa el valor de la interface
            SetInterface(estadoUI.inicio);
        }

        #endregion 

        #region Servicios

        //Método para evitar la creación de más de una pantalla
        public static frmPlanMensual Instancia
        {
            get
            {
                if (_frmPlanMensual == null || _frmPlanMensual.IsDisposed)
                {
                    _frmPlanMensual = new frmPlanMensual();
                }
                else
                {
                    _frmPlanMensual.BringToFront();
                }
                return _frmPlanMensual;
            }
            set
            {
                _frmPlanMensual = value;
            }
        }

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                //Cuando Arranca la pantalla
                case estadoUI.inicio:
                    txtAnioBuscar.Text = string.Empty;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;

                    //Limpiamos los datasets de Plan mensual y Detalle de plan mensual
                    dsPlanMensual.DETALLE_PLANES_MENSUALES.Clear();
                    dsPlanMensual.PLANES_MENSUALES.Clear();

                    cbMes.SetSelectedIndex(-1);
                    tcPlanAnual.SelectedTab = tpBuscar;
                    estadoActual = estadoUI.inicio;
                    break;

                //Cuando termina de Buscar
                case estadoUI.buscar:
                    btnNuevo.Enabled = true;
                    bool hayDatos;
                    if (dsPlanMensual.PLANES_MENSUALES.Rows.Count > 0)
                    {
                        hayDatos = true;
                    }
                    else hayDatos = false;
                    btnConsultar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnModificar.Enabled = hayDatos;
                    tcPlanAnual.SelectedTab = tpBuscar;
                    estadoActual = estadoUI.buscar;
                    break;

                //Cuando se carga el Detalle
                case estadoUI.cargaDetalle:
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    tcPlanAnual.SelectedTab = tpDatos;
                    estadoActual = estadoUI.cargaDetalle;
                    
                    //Manejo de Controles
                    //Groupbox
                    gbDatosPrincipales.Enabled = false;
                    gbCantidades.Visible = true;
                    gbCargaDetalle.Visible = true;
                    gbDetalleGrilla.Visible = true;
                    gbBotones.Visible = true;
                    
                    //Textbox
                    txtCantAPlanificar.Text = string.Empty;
                    txtCantPlanificada.Text = string.Empty;
                    txtRestaPlanificar.Text = string.Empty;
                    txtCantAPlanificar.ReadOnly = true;
                    txtCantPlanificada.ReadOnly=true;
                    txtRestaPlanificar.ReadOnly=true;
                    txtCapMes.ReadOnly = true;
                    //Combo
                    cbCocinas.SetSelectedIndex(-1);
                    //Numeric 
                    numPorcentaje.Value = 0;
                    //Radiobuttons
                    rbUnidades.Checked = true;
                    //Busco la tab seleccinada
                    tcDatos.Visible = true;
                    tcDatos.SelectedTab = tpPlanificacion;
                    
                    //Escondo las columnas que no quiero mostrar de la grilla
                    dgvDatos.Columns["DPMES_CODIGO"].Visible = false;
                    dgvDatos.Columns["PMES_CODIGO"].Visible = false;
                    dgvDatos.Columns["DPMES_CANTIDADREAL"].Visible = false;
                    break;
                    
                case estadoUI.modificar:
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    tcPlanAnual.SelectedTab = tpDatos;
                    SetInterface(estadoUI.cargaDetalle);
                    estadoActual = estadoUI.modificar;
                    //Radiobuttons
                    rbUnidades.Checked = true;                    
                    break;

                case estadoUI.nuevo:
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    tcPlanAnual.SelectedTab = tpDatos;
                    
                    //Manejo los controles
                    cbMesDatos.SetSelectedIndex(-1);
                    cbPlanAnual.SetSelectedIndex(-1);
                    tcDatos.Visible = false;
                    
                    gbDatosPrincipales.Enabled = true;
                    gbCantidades.Visible = false;
                    gbCargaDetalle.Visible = false;
                    gbDetalleGrilla.Visible = false;
                    gbBotones.Visible = false;
                    estadoActual = estadoUI.nuevo;
                    break;               

                default:
                    break;
            }
        }
        #endregion

        #region Controles
       
        //Detalle de Planes Mensuales Busqueda
        private void dgvDetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string nombre;

            if (e.Value != null && e.Value != DBNull.Value)
            {
                switch (dgvDetalle.Columns[e.ColumnIndex].Name)
                {
                    case "COC_CODIGO":
                        nombre = dsPlanMensual.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    case "DPED_FECHA_INICIO":
                        nombre = Convert.ToDateTime(e.Value).ToShortDateString();
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvDatos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string nombre;

            if (e.Value != null && e.Value != DBNull.Value)
            {
                switch (dgvDatos.Columns[e.ColumnIndex].Name)
                {
                    case "COC_CODIGO":
                        nombre = dsPlanMensual.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    case "DPED_FECHA_INICIO":
                        nombre = Convert.ToDateTime(e.Value).ToShortDateString();
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvPedidos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                string nombre;

                switch (dgvPedidos.Columns[e.ColumnIndex].Name)
                {
                    case "COC_CODIGO":
                        nombre = dsPlanMensual.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    case "EPED_CODIGO":
                        nombre = dsPlanMensual.ESTADO_PEDIDOS.FindByEPED_CODIGO(Convert.ToInt32(e.Value)).EPED_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "CLI_CODIGO":
                        nombre = dsPlanMensual.CLIENTES.FindByCLI_CODIGO(Convert.ToInt32(e.Value)).CLI_RAZONSOCIAL;
                        e.Value = nombre;
                        break;
                    case "PED_FECHA_ALTA":
                        nombre = Convert.ToDateTime(e.Value).ToShortDateString();
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }
        private void dgvDetallePedido_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                string nombre;

                switch (dgvDetallePedido.Columns[e.ColumnIndex].Name)
                {
                    case "COC_CODIGO":
                        nombre = dsPlanMensual.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    case "EDPED_CODIGO":
                        nombre = dsPlanMensual.ESTADO_DETALLE_PEDIDOS.FindByEDPED_CODIGO(Convert.ToInt32(e.Value)).EDPED_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void tcDatos_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tpDetallePedido && seleccionPestaña == false)
            {
                e.Cancel = true;
            }
            else if (seleccionPestaña == true)
            {
                seleccionPestaña = false;
            }
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

        private void numUnidades_Enter(object sender, EventArgs e)
        {
            numUnidades.Select(0, 10);
        }

        private void numPorcentaje_Enter(object sender, EventArgs e)
        {
            numPorcentaje.Select(0, 10);
        }

        private void dgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        private void dgvDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        private void dgvDetallePedido_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        private void dgvPedidos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        private void rbUnidades_CheckedChanged(object sender, EventArgs e)
        {
            numUnidades.Enabled = true;
            numPorcentaje.Enabled = false;
            numPorcentaje.Value = 0;
            numUnidades.Value = 0;
        }

        private void rbPorcentaje_CheckedChanged(object sender, EventArgs e)
        {
            numUnidades.Enabled = false;
            numPorcentaje.Enabled = true;
            numPorcentaje.Value = 0;
            numUnidades.Value = 0;
        }

        #endregion

        #region Pestaña Busqueda

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int anio; string mes;

                //Limpiamos el Dataset
                dsPlanMensual.PLANES_MENSUALES.Clear();

                if (txtAnioBuscar.Text != string.Empty)
                {
                    //Valido que el año
                    if (txtAnioBuscar.Text.Length != 4) throw new Exception();

                   anio = Convert.ToInt32(txtAnioBuscar.Text);
                }
                else
                {
                    anio = 0;

                }
                if (cbMes.SelectedIndex != -1)
                {
                    mes = cbMes.GetSelectedText();
                }
                else
                {
                    mes = string.Empty;
                }

                BLL.PlanMensualBLL.ObtenerTodos(anio, mes, dsPlanMensual);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaPlanes.Table = dsPlanMensual.PLANES_MENSUALES;

                if (dsPlanMensual.PLANES_MENSUALES.Rows.Count == 0)
                {
                    Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Planes Mensuales", this.Text);
                }

                SetInterface(estadoUI.buscar);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);                
            }
            catch (Exception)
            {
                Entidades.Mensajes.MensajesABM.MsjValidacion("El año no tiene el formato Correcto", this.Text);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Se programa la busqueda del detalle
                //Limpiamos el Dataset
                dsPlanMensual.DETALLE_PLANES_MENSUALES.Clear();

                int codigo = Convert.ToInt32(dvListaPlanes[dgvLista.SelectedRows[0].Index]["pmes_codigo"]);

                //Se llama a la funcion de busqueda del detalle
                BLL.DetallePlanMensualBLL.ObtenerDetalle(codigo, dsPlanMensual);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaDetalle.Table = dsPlanMensual.DETALLE_PLANES_MENSUALES;

                if (dsPlanMensual.DETALLE_PLANES_MENSUALES.Rows.Count == 0)
                {
                    Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Detalles de Plan Mensual", this.Text);
                }
                else
                {
                    //muestro el groupbox del detalle
                    gbGrillaDetalle.Visible = true;
                    SetInterface(estadoUI.buscar);
                    gbGrillaDetalle.Visible = true;
                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
            }
        }
        #endregion

        #region Funciones Formulario
        
        private void CalcularCantidades(int cantidadDetalle)
        {
            //Tengo los valores
            int cantidadAPlanificar = Convert.ToInt32(txtCantAPlanificar.Text);
            cantidadPlanificada = cantidadPlanificada + cantidadDetalle;
            int restaPlanificar = cantidadAPlanificar - cantidadPlanificada;

            //Los asigno a los textbox
            txtCantPlanificada.Text = cantidadPlanificada.ToString();
            if (restaPlanificar > 0)
            {
                txtRestaPlanificar.Text = restaPlanificar.ToString();
            }
            else txtRestaPlanificar.Text = Convert.ToString(0);
        }

        private string ValidarDetalle()
        {
            string msjerror = string.Empty;
            int cantidadPorcentaje=0;

            if (cbCocinas.SelectedIndex == -1) msjerror = msjerror + "-Debe seleccionar un modelo de cocina\n";
            
            if (rbUnidades.Checked == true)
            {
                if (numUnidades.Value == 0) msjerror = msjerror + "-La cantidad en unidades debe ser mayor a cero\n";
                else if (numUnidades.Value > Convert.ToInt32(txtCapMes.Text))
                {
                    msjerror = msjerror + "-La cantidad de unidades no puede superar la capacidad del mes.\n";
                }
            }
            if (rbPorcentaje.Checked == true)
            {
                cantidadPorcentaje = Convert.ToInt32(Math.Round(Convert.ToDecimal(txtCantAPlanificar.Text) * (numPorcentaje.Value / 100),0));
                if (numPorcentaje.Value == 0) msjerror = msjerror + "-El porcentaje debe ser mayor a cero\n";
                else if (cantidadPorcentaje > Convert.ToInt32(txtCapMes.Text))
                {
                    msjerror = msjerror + "-La cantidad de unidades no puede superar la capacidad del mes.\nCAPACIDAD: " + txtCapMes.Text + "\n" + "CANT. INGRESADA: " + cantidadPorcentaje.ToString();
                }                
            }

            //Validamos que la suma de las cantidades no sea mayor a la capacidad de la planta
            int sumador = 0;
            foreach (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row in (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow[])dsPlanMensual.DETALLE_PLANES_MENSUALES.Select(null, null, System.Data.DataViewRowState.CurrentRows))
            {
                sumador += Convert.ToInt32(row.DPMES_CANTIDADESTIMADA);
            }
            if(rbPorcentaje.Checked) { sumador += cantidadPorcentaje;}
            else if (rbUnidades.Checked) { sumador += Convert.ToInt32(numUnidades.Value); }   

            //Validamos que no sea mayor a la capacidad de la planta
            if (sumador > Convert.ToInt32(txtCapMes.Text))
            {
                msjerror = msjerror + "-La cantidad de unidades no puede superar la capacidad del mes.\nCAPACIDAD: " + txtCapMes.Text + "\n" + "CANT. TOTAL INGRESADA: " + sumador.ToString();
            }

            //Validamos que no se quiera agregar un modelo que ya está en el dataset como planificado
            foreach (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row in (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow[])dsPlanMensual.DETALLE_PLANES_MENSUALES.Select(null, null, System.Data.DataViewRowState.CurrentRows))
            {
                if (row["DPED_CODIGO"].ToString() == "0")
                {
                    if (row["COC_CODIGO"].ToString() == Convert.ToString(cbCocinas.GetSelectedValue()))
                    {
                        msjerror = msjerror + "-El modelo de cocina que intenta agregar ya se encuentra en la planificación\n";
                    }
                }
            }       
            

            return msjerror;
        }

        private string ValidarAgregarPedido(int codigoDetallePedido)
        {
            string msjerror = string.Empty;
            
            //Validamos que no se quiera agregar un detalle de pedido que ya fue planificado
            foreach (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row in (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow[])dsPlanMensual.DETALLE_PLANES_MENSUALES.Select(null, null, System.Data.DataViewRowState.CurrentRows))
            {
                if (Convert.ToInt32(row["DPED_CODIGO"]) == codigoDetallePedido )
                {
                    msjerror = msjerror + "-El detalle de pedido que intenta agregar ya se encuentra en la planificación\n";
                }
            }

            //Validamos que la cantidad que se quiere agregar no sea mayor a la capacidad del mes
            if (Convert.ToInt32(dsPlanMensual.DETALLE_PEDIDOS.FindByDPED_CODIGO(codigoDetallePedido).DPED_CANTIDAD) > Convert.ToInt32(txtCapMes.Text))
            {
                msjerror = msjerror + "-La cantidad que desea ingresar es mayor a la capacidad disponible para el mes. \n";
            }    

            return msjerror;
        }
        #endregion

        #region Pestaña Datos Generales

        private void btnCargaDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                //Seteo la variable global a cero
                cantidadPlanificada = 0;

                //Limpiamos el dataset de detalles
                dsPlanMensual.DETALLE_PLAN_ANUAL.Clear();

                if (cbPlanAnual.SelectedIndex != -1 && cbMesDatos.SelectedIndex != -1)
                {
                    int anio = Convert.ToInt32(cbPlanAnual.GetSelectedText());
                    string mes = cbMesDatos.GetSelectedText();

                    //Metodo que me busca el valuemember de un mes
                    string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                    int cont = 0;
                    foreach (string l in Meses)
                    {
                        if (mes == Meses[cont]) break;
                        cont++;
                    }
                    cont += 1;
                                       
                    //Busco los pedidos para esa fecha
                    BLL.PedidoBLL.ObtenerPedidoFecha(dsPlanMensual.PEDIDOS);

                    if (dsPlanMensual.PEDIDOS.Rows.Count == 0)
                    {
                        dgvPedidos.Visible = false;
                        lblMensaje.Text = "No se encontraron pedidos";
                        btnVerDetalle.Enabled = false;
                    }
                    else
                    {
                        dgvPedidos.Visible = true;
                        lblMensaje.Text = string.Empty;
                        btnVerDetalle.Enabled = true;
                    }

                    //Verifico que no exista ningun plan mensual para esos datos
                    if (BLL.PlanMensualBLL.ExistePlanMensual(anio, mes) == true)
                    {
                        SetInterface(estadoUI.cargaDetalle);

                        //Cargo el valor que debe planificar para ese mes
                        int cantidad = BLL.PlanAnualBLL.ObtenerTodos(anio, mes);
                        txtCantAPlanificar.Text = cantidad.ToString();
                        txtCantPlanificada.Text =Convert.ToString(0);
                        txtRestaPlanificar.Text = cantidad.ToString();

                        //Calculamos la capacidad de ese mes
                        int[] semanasMeses = new int[12];
                        semanasMeses = BLL.DemandaAnualBLL.SemanasAño(anio);
                        txtCapMes.Text = (BLL.FabricaBLL.GetCapacidadSemanalBruta(null, GyCAP.Entidades.Enumeraciones.RecursosFabricacionEnum.TipoHorario.Normal) * semanasMeses[cont-1]).ToString();
                        
                        //Borro los detalles del dataset
                        dsPlanMensual.DETALLE_PLANES_MENSUALES.Clear();
                    }
                    else
                    {
                        Entidades.Mensajes.MensajesABM.MsjValidacion("Ya existe un Plan Mensual para ese año y mes seleccionado", this.Text);
                    }
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Plan Anual y Mes", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                SetInterface(estadoUI.cargaDetalle);
            }
        }
        
        private string Validar()
        {
            string validacion = string.Empty;

            //Validamos que existan detalles a guardar
            if (dsPlanMensual.DETALLE_PLANES_MENSUALES.Rows.Count == 0)
            { validacion = validacion + "-No hay Detalles de Plan Mensual que guardar\n"; }

            //Validamos que no queden detalles de pedido que se tengan que planificar en ese mes
            
            string mes = cbMesDatos.GetSelectedText().ToString();
            int anio = Convert.ToInt32(cbPlanAnual.GetSelectedText());

            //Metodo que me busca el valuemember de un mes
            string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            
            int cont = 0;
            foreach (string l in Meses)
            {
                if (mes == Meses[cont]) break;
                cont++;
            }
            cont += 1;

            //Convertimos y creamos las fechas desde y hasta
            DateTime fechaDesde = Convert.ToDateTime("01/" + cont.ToString() + "/" + anio.ToString());
            DateTime fechaHasta;
            if (cont < 12)
            {
                cont += 1;
                fechaHasta = Convert.ToDateTime("01/" + cont.ToString() + "/" + anio.ToString());
            }
            else
            {
                anio += 1; cont = 1;
                fechaHasta = Convert.ToDateTime("01/" + cont.ToString() + "/" + anio.ToString());
            }

            int cantidadDetalles = BLL.DetallePedidoBLL.ValidarDetallesFecha(fechaDesde,fechaHasta);
            
            //Contamos la cantidad de detalles que se planificaron
            cont = 0;
            foreach (DataRow row in dsPlanMensual.DETALLE_PLANES_MENSUALES.Select("DPED_CODIGO > 0"))
            {
                cont += 1;
            }

            //Verificamos las cantidades que salieron
            if (cantidadDetalles != cont)
            {
                validacion = validacion + "-Existen Detalles de Pedidos que deben comenzar este mes y no se Planificaron\n";
            }

            return validacion;           
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
              string validar = Validar();

              if (validar == string.Empty)
                {
                    //Checkeo las excepciones relacionadas con el Plan Mensual
                    List<Entidades.ExcepcionesPlan> excepciones = new List<GyCAP.Entidades.ExcepcionesPlan>();
                    excepciones = BLL.PlanMensualBLL.CheckeoExcepciones(dsPlanMensual.DETALLE_PLANES_MENSUALES);

                    if (excepciones.Count == 0 || checkeoExcepciones == true)
                    {
                        //Creamos el objeto de plan anual
                        Entidades.PlanAnual planAnual = new GyCAP.Entidades.PlanAnual();
                        planAnual.Codigo = Convert.ToInt32(cbPlanAnual.GetSelectedValue());
                        planAnual.Anio = Convert.ToInt32(cbPlanAnual.GetSelectedText());

                        //Creamos el objeto del plan mensual
                        Entidades.PlanMensual planMensual = new GyCAP.Entidades.PlanMensual();
                        planMensual.FechaCreacion = BLL.DBBLL.GetFechaServidor();
                        planMensual.Mes = cbMesDatos.GetSelectedText();
                        planMensual.PlanAnual = planAnual;

                        if (estadoActual == estadoUI.cargaDetalle)
                        {
                            //Enviamos los datos para guardar
                            BLL.PlanMensualBLL.GuardarPlan(planMensual, dsPlanMensual);
                        }
                        else if (estadoActual == estadoUI.modificar)
                        {
                            planMensual.Codigo = Convert.ToInt32(dvListaPlanes[dgvLista.SelectedRows[0].Index]["pmes_codigo"]);
                            BLL.PlanMensualBLL.GuardarPlanModificado(planMensual, dsPlanMensual);
                        }
                        //Si esta todo bien aceptamos los cambios que se le hacen al dataset
                        dsPlanMensual.AcceptChanges();

                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Plan Mensual", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);

                        if (estadoActual == estadoUI.cargaDetalle || estadoActual == estadoUI.modificar)
                        {
                            //Cambio el estado a los detalles de pedido que fueron Planificados
                            foreach (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row in dsPlanMensual.DETALLE_PLANES_MENSUALES.Rows)
                            {
                                if (row["DPED_CODIGO"].ToString() != Convert.ToString(0))
                                {
                                    //Actualizo el valor del estado en la BD
                                    BLL.DetallePedidoBLL.CambiarEstado(Convert.ToInt32(row.DPED_CODIGO), BLL.EstadoPedidoBLL.EstadoEnCurso);

                                    //Lo actualizo el el dataset
                                    dsPlanMensual.DETALLE_PEDIDOS.FindByDPED_CODIGO(Convert.ToInt32(row.DPED_CODIGO)).BeginEdit();
                                    dsPlanMensual.DETALLE_PEDIDOS.FindByDPED_CODIGO(Convert.ToInt32(row.DPED_CODIGO)).EDPED_CODIGO = BLL.EstadoPedidoBLL.EstadoEnCurso;
                                    dsPlanMensual.DETALLE_PEDIDOS.FindByDPED_CODIGO(Convert.ToInt32(row.DPED_CODIGO)).EndEdit();
                                    dsPlanMensual.DETALLE_PEDIDOS.FindByDPED_CODIGO(Convert.ToInt32(row.DPED_CODIGO)).AcceptChanges();
                                }
                            }

                            //Cambio el estado del pedido que fue planificado si todos los detalles lo fueron
                            int cont = 0;
                            foreach (Data.dsPlanMensual.PEDIDOSRow pm in dsPlanMensual.PEDIDOS.Rows)
                            {
                                foreach (Data.dsPlanMensual.DETALLE_PEDIDOSRow row in (Data.dsPlanMensual.DETALLE_PEDIDOSRow[])dsPlanMensual.DETALLE_PEDIDOS.Select("ped_codigo=" + pm.PED_CODIGO.ToString() + "and edped_codigo=" + BLL.EstadoDetallePedidoBLL.ObtenerCodigoEstado("Pendiente").ToString()))
                                {
                                   cont++;                                                                     
                                }
                                if (cont == 0)
                                {
                                    //Cambio el estado del pedido en la bd
                                    BLL.PedidoBLL.CambiarEstadoPedido(Convert.ToInt32(pm.PED_CODIGO), BLL.EstadoPedidoBLL.EstadoEnCurso);

                                    //Lo actualizo el el dataset
                                    pm.BeginEdit();
                                    pm.EPED_CODIGO = BLL.EstadoPedidoBLL.EstadoEnCurso;
                                    pm.EndEdit();
                                    pm.AcceptChanges();
                                }
                                //pongo los contadores en cero
                                cont = 0;                               
                            }
                        }
                        
                        //Vuelvo al estado inicial de la interface
                        SetInterface(estadoUI.inicio);
                    }
                    else
                    {
                        //Si existen excepciones relacionadas con el Plan mensual
                        PlanificacionProduccion.frmExcepcionesPlan frmExcepciones = new frmExcepcionesPlan();
                        frmExcepciones.TopLevel = false;
                        frmExcepciones.MdiParent = this.MdiParent;
                        frmExcepciones.CargarGrilla(excepciones);
                        frmExcepciones.Show();
                        frmExcepciones.BringToFront();

                        //Cambio el valor de checkeo excepciones a TRUE para que pase una vez
                        checkeoExcepciones = true;
                    }                   
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjValidacion(validar, this.Text);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
            catch (Entidades.Excepciones.CocinaSinEstructuraActivaException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
            catch (Entidades.Excepciones.EstructuraSinMateriaPrimaException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //Datos de la cabecera
            //Selecciono el codigo de la demanda anual
            int codigo = Convert.ToInt32(dvListaPlanes[dgvLista.SelectedRows[0].Index]["pmes_codigo"]);

            int codigoPlanAnual = Convert.ToInt32(dsPlanMensual.PLANES_MENSUALES.FindByPMES_CODIGO(codigo).PAN_CODIGO);
            int anio = Convert.ToInt32(dsPlanMensual.PLANES_ANUALES.FindByPAN_CODIGO(codigoPlanAnual).PAN_ANIO);

            cbPlanAnual.SetSelectedValue(Convert.ToInt32(dsPlanMensual.PLANES_MENSUALES.FindByPMES_CODIGO(codigo).PAN_CODIGO));

            string mes = dsPlanMensual.PLANES_MENSUALES.FindByPMES_CODIGO(codigo).PMES_MES.ToString();

            //Metodo que me busca el valuemember de un mes
            string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            int cont = 0;
            foreach (string l in Meses)
            {
                if (mes == Meses[cont]) break;
                cont++;
            }

            cbMesDatos.SetSelectedIndex(cont);
            
            //Cargo lo que corresponde a los PEDIDOS
           if (cbPlanAnual.SelectedIndex != -1 && cbMesDatos.SelectedIndex != -1)
            {
                cont += 1;
               
                //Busco los pedidos para esa fecha
                BLL.PedidoBLL.ObtenerPedidoFecha(dsPlanMensual.PEDIDOS);

                if (dsPlanMensual.PEDIDOS.Rows.Count == 0)
                {
                    dgvPedidos.Visible = false;
                    lblMensaje.Text = "No se encontraron pedidos";
                    btnVerDetalle.Enabled = false;
                }
                else
                {
                    dgvPedidos.Visible = true;
                    lblMensaje.Text = string.Empty;
                    btnVerDetalle.Enabled = true;
                }
            }

            //Pongo en 0 los null de los pedidos
           foreach (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row in dsPlanMensual.DETALLE_PLANES_MENSUALES.Rows)
           {
               try
               {
                   decimal? pedido = row.DPED_CODIGO;
               }
               catch(Exception)
               {
                   row.BeginEdit();
                   row.DPED_CODIGO = 0;
                   row.EndEdit();
                   row.AcceptChanges();
               }
           }
            //Seteo la interface al estado modificar
            SetInterface(estadoUI.modificar);

            //Datos calculados
            //Cargo el valor que debe planificar para ese mes
            int cantidad = BLL.PlanAnualBLL.ObtenerTodos(anio, mes);
            txtCantAPlanificar.Text = cantidad.ToString();
            txtCantPlanificada.Text = BLL.PlanMensualBLL.CalcularTotal(anio, mes).ToString();
            cantidadPlanificada = Convert.ToInt32(txtCantPlanificada.Text) + cantidadPlanificada;

            int restaPlanificar = cantidad - Convert.ToInt32(txtCantPlanificada.Text);
            if (restaPlanificar > 0) txtRestaPlanificar.Text = restaPlanificar.ToString();
            else txtRestaPlanificar.Text = Convert.ToString(0);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjConfirmaEliminarDatos("Plan Mensual", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtengo el Codigo del plan
                        int codigo = Convert.ToInt32(dvListaPlanes[dgvLista.SelectedRows[0].Index]["pmes_codigo"]);

                        //Pregunto si se puede eliminar
                        if (BLL.PlanMensualBLL.ExistePlanSemanal(codigo) == true)
                        {
                            int codigoPedido = 0, pedido = 0;

                            //Si existen pedidos de planes mensuales planificados les actualizo el estado
                            foreach (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row in dsPlanMensual.DETALLE_PLANES_MENSUALES.Rows)
                            {
                                if (Convert.ToInt32(row.DPED_CODIGO) > 0)
                                {
                                    BLL.DetallePedidoBLL.CambiarEstado(Convert.ToInt32(row.DPED_CODIGO), BLL.EstadoDetallePedidoBLL.ObtenerCodigoEstado("Pendiente"));
                                    pedido = BLL.DetallePedidoBLL.ObtenerIDPedidoDetalle(Convert.ToInt32(row.DPED_CODIGO));
                                }                                
                            }
                            
                            //Cambio el estado del pedido
                            if (pedido != 0)
                            {
                                BLL.PedidoBLL.CambiarEstadoPedido(pedido, BLL.EstadoPedidoBLL.EstadoPendiente);
                            }

                            //Elimino el plan anual y su detalle de la BD
                            BLL.PlanMensualBLL.EliminarPlan(codigo);

                            //Limpio el dataset de detalles
                            dsPlanMensual.DETALLE_PLANES_MENSUALES.Clear();

                            //Lo eliminamos del dataset
                            dsPlanMensual.PLANES_MENSUALES.FindByPMES_CODIGO(codigo).Delete();
                            dsPlanMensual.PLANES_MENSUALES.AcceptChanges();

                            //Avisamos que se elimino 
                            Entidades.Mensajes.MensajesABM.MsjConfirmaEliminar(this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);

                            //Ponemos la ventana en el estado inicial
                            SetInterface(estadoUI.inicio);
                        }
                        else { throw new Entidades.Excepciones.ElementoEnTransaccionException(); }

                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Plan Mensual", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
            }
        }    
        
        #endregion

        #region Pestaña Datos TABS
        
        private void btnPlanificar_Click(object sender, EventArgs e)
        {
            try
            {
                //Validamos lo que quiere planificar
                string msjValidacion = ValidarAgregarPedido(Convert.ToInt32(dvListaDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_codigo"]));

                if(msjValidacion == string.Empty)
                {
                    int codigoPlan = -1; int cantidad;

                    //Comenzamos con la edición de la fila en si misma
                    Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row = dsPlanMensual.DETALLE_PLANES_MENSUALES.NewDETALLE_PLANES_MENSUALESRow();
                    row.BeginEdit();
                    row.DPMES_CODIGO = codigoDetalle--;
                    row.PMES_CODIGO = codigoPlan;
                    row.COC_CODIGO = Convert.ToInt32(dvListaDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["coc_codigo"]);
                    cantidad = Convert.ToInt32(dvListaDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_cantidad"]);
                    row.DPMES_CANTIDADESTIMADA = cantidad;
                    row.DPED_CODIGO = Convert.ToInt32(dvListaDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_codigo"]);
                    row.DPED_FECHA_INICIO = Convert.ToDateTime(dvListaDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_fecha_inicio"]);
                    row.EndEdit();
                    dsPlanMensual.DETALLE_PLANES_MENSUALES.AddDETALLE_PLANES_MENSUALESRow(row);

                    //Metodo que recalcula los valores Ingresados
                    CalcularCantidades(cantidad);                  

                    //Seteamos la interface
                    tcDatos.SelectedTab = tpPlanificacion;
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjValidacion(msjValidacion, this.Text);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Inicio);
            }
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                //Busco el Codigo del Pedido
                int codigoPedido = Convert.ToInt32(dvListaPedidos[dgvPedidos.SelectedRows[0].Index]["ped_numero"]);

                //Obtengo el detalle del pedido
                dsPlanMensual.DETALLE_PEDIDOS.Clear();
                BLL.DetallePedidoBLL.ObtenerDetallePedidoEstado(dsPlanMensual.DETALLE_PEDIDOS, codigoPedido, BLL.EstadoDetallePedidoBLL.ObtenerCodigoEstado("Pendiente"));

                if (dsPlanMensual.DETALLE_PEDIDOS.Rows.Count > 0)
                {
                    //Selecciono el tab del detalle
                    gbDetallePedido.Visible = true;
                    btnPlanificar.Enabled = true;
                    seleccionPestaña = true;
                    tcDatos.SelectedTab = tpDetallePedido;                  
                }
                else
                {
                    gbDetallePedido.Visible = false;
                    btnPlanificar.Enabled = false;
                    Entidades.Mensajes.MensajesABM.MsjValidacion("El pedido seleccionado no tiene detalle.", this.Text);
                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Inicio);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool validacion = true;

            if (Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dped_codigo"]) != 0)
            {
                //Valido que no tenga detalles de planes semanales ya creados
                validacion = BLL.PlanMensualBLL.ExistePlanSemanalPedido(Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dped_codigo"]));
            }

            if (validacion == true)
            {
                if (dgvDatos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
                {
                    int codigo = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dpmes_codigo"]);
                    int cantidad = Convert.ToInt32(dsPlanMensual.DETALLE_PLANES_MENSUALES.FindByDPMES_CODIGO(codigo).DPMES_CANTIDADESTIMADA);
                    //Sumo las cantidades
                    CalcularCantidades(cantidad * -1);

                    //Elimino el dataset
                    dsPlanMensual.DETALLE_PLANES_MENSUALES.FindByDPMES_CODIGO(codigo).Delete();
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Cocina", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Femenino, this.Text);
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Detalle Plan Mensual", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);

            }
        }

        private void btnSumar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dped_codigo"]) == 0)
            {
                if (0 < (Convert.ToInt32(txtCantAPlanificar.Text) - Convert.ToInt32(txtCantPlanificada.Text)))
                {
                    if (dgvDatos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
                    {
                        int codigo = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dpmes_codigo"]);

                        if (dsPlanMensual.DETALLE_PLANES_MENSUALES.FindByDPMES_CODIGO(codigo).DPMES_CANTIDADESTIMADA < Convert.ToInt32(txtCapMes.Text))
                        {
                            dsPlanMensual.DETALLE_PLANES_MENSUALES.FindByDPMES_CODIGO(codigo).DPMES_CANTIDADESTIMADA += 1;

                            //Llamo a la función que recalcula los valores
                            CalcularCantidades(1);
                        }
                        else
                        {
                            Entidades.Mensajes.MensajesABM.MsjValidacion("La cantidad no puede ser mayor a la capacidad de fabricación.", this.Text);
                        }
                    }
                    else
                    {
                        Entidades.Mensajes.MensajesABM.MsjValidacion("No puede eliminar un detalle de pedido del plan mensual si ya fue planificado en el plan semanal", this.Text);
                    }
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjValidacion("La cantidad de unidades no puede ser mayor de lo que resta planificar", this.Text);
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjValidacion("No puede cambiar las cantidades de un pedido", this.Text);
            }
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dped_codigo"]) == 0)
            {
                if (Convert.ToInt32(txtCantPlanificada.Text) >= 1)
                {
                    if (dgvDatos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
                    {
                        int codigo = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dpmes_codigo"]);

                        if (dsPlanMensual.DETALLE_PLANES_MENSUALES.FindByDPMES_CODIGO(codigo).DPMES_CANTIDADESTIMADA <= Convert.ToInt32(txtCapMes.Text))
                        {
                            dsPlanMensual.DETALLE_PLANES_MENSUALES.FindByDPMES_CODIGO(codigo).DPMES_CANTIDADESTIMADA -= 1;

                            //Llamo a la función que recalcula los valores
                            CalcularCantidades(-1);
                        }
                        else
                        {
                            Entidades.Mensajes.MensajesABM.MsjValidacion("La cantidad no puede ser mayor a la capacidad de fabricación.", this.Text);
                        }
                    }
                    else
                    {
                        Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Cocina", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Femenino, this.Text);
                    }
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjValidacion("La cantidad de unidades no puede ser mayor de lo que ya se planificó", this.Text);
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjValidacion("No puede cambiar las cantidades de un pedido", this.Text);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int codigoPlan = -1; int cantidad;

            try
            {
                //Ejecutamos la validacion para verificar que este todo OK
                string msjerror = ValidarDetalle();

                if (msjerror.Length == 0)
                {
                    //Comenzamos con la edición de la fila en si misma
                    Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row = dsPlanMensual.DETALLE_PLANES_MENSUALES.NewDETALLE_PLANES_MENSUALESRow();
                    row.BeginEdit();
                    row.DPMES_CODIGO = codigoDetalle--;
                    row.PMES_CODIGO = codigoPlan;
                    row.COC_CODIGO = Convert.ToInt32(cbCocinas.GetSelectedValue());
                    if (rbUnidades.Checked == true)
                    {
                        cantidad = Convert.ToInt32(numUnidades.Value);
                        row.DPMES_CANTIDADESTIMADA = cantidad;
                    }
                    else
                    {
                        cantidad = Convert.ToInt32(Convert.ToInt32(txtCantAPlanificar.Text) * (numPorcentaje.Value / 100));
                        row.DPMES_CANTIDADESTIMADA = cantidad;

                    }
                    row.DPED_CODIGO = 0;
                    row.EndEdit();
                    dsPlanMensual.DETALLE_PLANES_MENSUALES.AddDETALLE_PLANES_MENSUALESRow(row);

                    //Metodo que recalcula los valores Ingresados
                    CalcularCantidades(cantidad);

                    //Seteamos la interface al estado de carga de detalle
                    cbCocinas.SetSelectedIndex(-1);
                    numPorcentaje.Value = 0;
                    numUnidades.Value = 0;
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjValidacion(msjerror, this.Text);
                }
            }
            catch (Exception ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
        }
        #endregion       
   
    }
}
