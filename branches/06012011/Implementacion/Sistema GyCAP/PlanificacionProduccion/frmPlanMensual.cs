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
    public partial class frmPlanMensual : Form
    {
        private static frmPlanMensual _frmPlanMensual = null;
        private Data.dsPlanMensual dsPlanMensual = new GyCAP.Data.dsPlanMensual();
        private DataView dvListaPlanes, dvListaDetalle, dvListaDatos, dvComboPlanesAnuales, dvComboCocinas, dvListaPedidos, dvListaDetallePedido;
        private enum estadoUI { inicio, nuevo, buscar, modificar, cargaDetalle };
        private static estadoUI estadoActual;
        private static int cantidadPlanificada; int codigoDetalle = -1;
        private static bool seleccionPestaña = false;
        
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
            //Lista de Demandas
            //Agregamos la columnas
            dgvLista.Columns.Add("PMES_CODIGO", "Código");
            dgvLista.Columns.Add("PAN_CODIGO", "Plan Anual");
            dgvLista.Columns.Add("PAN_ANIO", "Anio Plan Anual");
            dgvLista.Columns.Add("PMES_MES", "Mes del Plan Mensual");
            dgvLista.Columns.Add("PMES_FECHACREACION", "Fecha Creación Plan Mensual");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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
            dgvDetalle.Columns.Add("COC_CODIGO", "Cocina Codigo");
            dgvDetalle.Columns.Add("DPMES_CANTIDADESTIMADA", "Cantidad Estimada");
            dgvDetalle.Columns.Add("DPMES_CANTIDADREAL", "Cantidad Real");
            dgvDetalle.Columns.Add("DPED_CODIGO", "Pedido");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetalle.Columns["DPMES_CODIGO"].DataPropertyName = "DPMES_CODIGO";
            dgvDetalle.Columns["PMES_CODIGO"].DataPropertyName = "PMES_CODIGO";
            dgvDetalle.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDetalle.Columns["DPMES_CANTIDADESTIMADA"].DataPropertyName = "DPMES_CANTIDADESTIMADA";
            dgvDetalle.Columns["DPMES_CANTIDADREAL"].DataPropertyName = "DPMES_CANTIDADREAL";
            dgvDetalle.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";

            //Seteamos el modo de tamaño de las columnas
            dgvDetalle.Columns[0].Visible = false;
            dgvDetalle.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalle.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDetalle.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDetalle.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

             //Creamos el dataview y lo asignamos a la grilla
            dvListaDetalle = new DataView(dsPlanMensual.DETALLE_PLANES_MENSUALES);
            dgvDetalle.DataSource = dvListaDetalle;

            //******************************************************************************************
            //Lista de Datos
            //Agregamos la columnas
            dgvDatos.Columns.Add("DPMES_CODIGO", "Código");
            dgvDatos.Columns.Add("PMES_CODIGO", "Mes");
            dgvDatos.Columns.Add("COC_CODIGO", "Cocina Codigo");
            dgvDatos.Columns.Add("DPMES_CANTIDADESTIMADA", "Cantidad Estimada");
            dgvDatos.Columns.Add("DPMES_CANTIDADREAL", "Cantidad Real");
            dgvDatos.Columns.Add("DPED_CODIGO", "Pedido");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDatos.Columns["DPMES_CODIGO"].DataPropertyName = "DPMES_CODIGO";
            dgvDatos.Columns["PMES_CODIGO"].DataPropertyName = "PMES_CODIGO";
            dgvDatos.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDatos.Columns["DPMES_CANTIDADESTIMADA"].DataPropertyName = "DPMES_CANTIDADESTIMADA";
            dgvDatos.Columns["DPMES_CANTIDADREAL"].DataPropertyName = "DPMES_CANTIDADREAL";
            dgvDatos.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";

            //Seteamos el modo de tamaño de las columnas
            dgvDatos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDatos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDatos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDatos = new DataView(dsPlanMensual.DETALLE_PLANES_MENSUALES);
            dgvDatos.DataSource = dvListaDatos;


            //*********************************** Lista de Pedidos *****************************************
            //Agregamos la columnas
            dgvPedidos.Columns.Add("PED_CODIGO", "Código");
            dgvPedidos.Columns.Add("PED_NUMERO", "Numero");
            dgvPedidos.Columns.Add("CLI_CODIGO", "Cliente");
            dgvPedidos.Columns.Add("EPED_CODIGO", "Estado");
            dgvPedidos.Columns.Add("PED_FECHA_ALTA", "Fecha Alta");
            dgvPedidos.Columns.Add("PED_FECHAENTREGAPREVISTA", "Fecha Entrega");
            dgvPedidos.Columns.Add("PED_FECHAENTREGAREAL", "Fecha Real Entrega");
               

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvPedidos.Columns["PED_CODIGO"].DataPropertyName = "PED_CODIGO";
            dgvPedidos.Columns["PED_NUMERO"].DataPropertyName = "PED_NUMERO";
            dgvPedidos.Columns["CLI_CODIGO"].DataPropertyName = "CLI_CODIGO";
            dgvPedidos.Columns["EPED_CODIGO"].DataPropertyName = "EPED_CODIGO";
            dgvPedidos.Columns["PED_FECHA_ALTA"].DataPropertyName = "PED_FECHA_ALTA";
            dgvPedidos.Columns["PED_FECHAENTREGAPREVISTA"].DataPropertyName = "PED_FECHAENTREGAPREVISTA";
            dgvPedidos.Columns["PED_FECHAENTREGAREAL"].DataPropertyName = "PED_FECHAENTREGAREAL";

            //Seteamos el modo de tamaño de las columnas
            dgvPedidos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPedidos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPedidos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPedidos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPedidos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvPedidos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaPedidos = new DataView(dsPlanMensual.PEDIDOS);
            dgvPedidos.DataSource = dvListaPedidos;

            //*********************************** Lista de Detalle de Pedidos *****************************************
            //Agregamos la columnas
            dgvDetallePedido.Columns.Add("DPED_CODIGO", "Código");
            dgvDetallePedido.Columns.Add("PED_CODIGO", "Pedido");
            dgvDetallePedido.Columns.Add("EDPED_CODIGO", "Estado");
            dgvDetallePedido.Columns.Add("COC_CODIGO", "Cocina");
            dgvDetallePedido.Columns.Add("DPED_CANTIDAD", "Cantidad");
            dgvDetallePedido.Columns.Add("DPED_FECHA_CANCELACION", "Fecha Cancelación");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetallePedido.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";
            dgvDetallePedido.Columns["PED_CODIGO"].DataPropertyName = "PED_CODIGO";
            dgvDetallePedido.Columns["EDPED_CODIGO"].DataPropertyName = "EDPED_CODIGO";
            dgvDetallePedido.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDetallePedido.Columns["DPED_CANTIDAD"].DataPropertyName = "DPED_CANTIDAD";
            dgvDetallePedido.Columns["DPED_FECHA_CANCELACION"].DataPropertyName = "DPED_FECHA_CANCELACION";
            
            //Seteamos el modo de tamaño de las columnas
            dgvDetallePedido.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePedido.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePedido.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePedido.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePedido.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDetallePedido.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            
            //Creamos el dataview y lo asignamos a la grilla
            dvListaDetallePedido = new DataView(dsPlanMensual.DETALLE_PEDIDOS);
            dgvDetallePedido.DataSource = dvListaDetallePedido;
            
            //LLenado de Datasets
            //Llenamos el dataset con los planes anuales
            BLL.PlanAnualBLL.ObtenerTodos(dsPlanMensual.PLANES_ANUALES);

            //Llenamos el detalle del Plan Anual
            BLL.DetallePlanAnualBLL.ObtenerDetalle(dsPlanMensual);

            //Llenamos el dataset de Cocinas
            BLL.CocinaBLL.ObtenerCocinasSinCosto(dsPlanMensual.COCINAS);

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

            //Setemoa el valor de la interface
            SetInterface(estadoUI.inicio);
           
            }

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
                    gbGrillaDemanda.Visible = false;
                    gbGrillaDetalle.Visible = false;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
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
                    gbGrillaDemanda.Visible = hayDatos;
                    gbGrillaDetalle.Visible = false;
                    tcPlanAnual.SelectedTab = tpBuscar;
                    estadoActual = estadoUI.buscar;
                    
                    //Columnas de las grillas
                    //Ponemos las columnas de las grillas en visible false
                    dgvLista.Columns["PMES_CODIGO"].Visible = false;
                    dgvLista.Columns["PAN_CODIGO"].Visible = false;
                    dgvDetalle.Columns["DPMES_CODIGO"].Visible = false;
                    dgvDetalle.Columns["PMES_CODIGO"].Visible = false;
                    dgvDetalle.Columns["DPMES_CANTIDADREAL"].Visible = false;
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
                    txtCantAPlanificar.Enabled = false;
                    txtCantPlanificada.Enabled = false;
                    txtRestaPlanificar.Enabled = false;
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

                    //Escondo las columnas de las grillas de pedidos
                    dgvPedidos.Columns["PED_CODIGO"].Visible = false;
                    dgvPedidos.Columns["PED_FECHAENTREGAREAL"].Visible = false;
                    dgvPedidos.Columns["PED_FECHA_ALTA"].Visible = false;

                    dgvDetallePedido.Columns["DPED_CODIGO"].Visible = false;
                    dgvDetallePedido.Columns["PED_CODIGO"].Visible = false;
                    dgvDetallePedido.Columns["DPED_FECHA_CANCELACION"].Visible = false;
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

                    //Escondo las columnas que no quiero mostrar de la grilla
                    dgvDatos.Columns["DPMES_CODIGO"].Visible = false;
                    dgvDatos.Columns["PMES_CODIGO"].Visible = false;
                    dgvDatos.Columns["DPMES_CANTIDADREAL"].Visible = false;
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
                    MessageBox.Show("No se encontraron Planes Mensuales con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                SetInterface(estadoUI.buscar);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Mensual - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
            catch (Exception)
            {
                MessageBox.Show("El año no tiene el formato Correcto", "Error: Plan Mensual - Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("No se encontraron Detalles para ese Plan Mensual.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(ex.Message, "Error: Plan Mensual - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private void dgvDetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.Value.ToString() != String.Empty)
            {
                switch (dgvDetalle.Columns[e.ColumnIndex].Name)
                {
                    case "COC_CODIGO":
                        string nombre = dsPlanMensual.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }

            }
        }

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
                                       
                    //Verifico si existen pedidos para ese mes
                    DateTime fechaPedidos = Convert.ToDateTime("01/" + cont.ToString() + "/" + anio.ToString());
                    
                    //Busco los pedidos para esa fecha
                    BLL.PedidoBLL.ObtenerPedido(fechaPedidos,dsPlanMensual);

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

                        //Borro los detalles del dataset
                        dsPlanMensual.DETALLE_PLANES_MENSUALES.Clear();

                    }
                    else
                    {
                        MessageBox.Show("Ya existe un plan Mensual para ese año y mes seleccionado", "Error: Plan Mensual - Carga de Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un Plan Anual y un Mes para Cargar el Detalle", "Error: Plan Mensual - Carga de Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Mensual - Carga Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.cargaDetalle);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void rbUnidades_CheckedChanged(object sender, EventArgs e)
        {
            numUnidades.Visible = true;
            numPorcentaje.Visible = false;
            numPorcentaje.Value = 0;
            numUnidades.Value = 0;
        }

        private void rbPorcentaje_CheckedChanged(object sender, EventArgs e)
        {
            numUnidades.Visible = false;
            numPorcentaje.Visible = true;
            numPorcentaje.Value = 0;
            numUnidades.Value = 0;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int codigoPlan=-1; int cantidad;
            try
            {
                //Ejecutamos la validacion para verificar que este todo OK
                ValidarDetalle();

                //Comenzamos con la edición de la fila en si misma
                Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row = dsPlanMensual.DETALLE_PLANES_MENSUALES.NewDETALLE_PLANES_MENSUALESRow();
                row.BeginEdit();
                row.DPMES_CODIGO = codigoDetalle--;
                row.PMES_CODIGO = codigoPlan;
                row.COC_CODIGO =Convert.ToInt32(cbCocinas.GetSelectedValue());
                if (rbUnidades.Checked == true)
                {
                    cantidad= Convert.ToInt32(numUnidades.Value);
                    row.DPMES_CANTIDADESTIMADA =cantidad;                    
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Validación - Carga Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CalcularCantidades(int cantidadDetalle)
        {
            //Tengo los valores
           int cantidadAPlanificar = Convert.ToInt32(txtCantAPlanificar.Text);
           cantidadPlanificada = cantidadPlanificada + cantidadDetalle;
           int restaPlanificar =cantidadAPlanificar - cantidadPlanificada;

            //Los asigno a los textbox
            txtCantPlanificada.Text = cantidadPlanificada.ToString();
            if (restaPlanificar > 0)
            {
                txtRestaPlanificar.Text = restaPlanificar.ToString();
            }
            else txtRestaPlanificar.Text =Convert.ToString(0);

        }

        private void ValidarDetalle()
        {
            string msjerror=string.Empty;

            if (cbCocinas.SelectedIndex == -1) msjerror = msjerror + "-Debe seleccionar un modelo de cocina\n";
            if (rbUnidades.Checked == true)
            {
                if (numUnidades.Value == 0) msjerror = msjerror + "-La cantidad en unidades debe ser mayor a cero\n";
            }
            if (rbPorcentaje.Checked == true)
            {
                if (numPorcentaje.Value == 0) msjerror = msjerror + "-El porcentaje debe ser mayor a cero\n";
            }

           
            //Validamos que no se quiera agregar un modelo que ya está en el dataset
            foreach (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row in (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow[])dsPlanMensual.DETALLE_PLANES_MENSUALES.Select(null, null, System.Data.DataViewRowState.Added))
            {
                if (row["DPED_CODIGO"].ToString() == string.Empty)
                {
                    if (row["COC_CODIGO"].ToString() == Convert.ToString(cbCocinas.GetSelectedValue()))
                    {

                        msjerror = msjerror + "-El modelo de cocina que intenta agregar ya se encuentra en la planificación\n";

                    }
                }
            }

            //Validamos que no se quiera agregar un modelo que ya está en el dataset
            foreach (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row in (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow[])dsPlanMensual.DETALLE_PLANES_MENSUALES.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
            {
                if (row["COC_CODIGO"].ToString() == Convert.ToString(cbCocinas.GetSelectedValue()))
                {
                    msjerror = msjerror + "-El modelo de cocina que intenta agregar ya se encuentra en la planificación\n";
                }
            }
            
            if (msjerror.Length > 0)
            {
                msjerror = "Los errores encontrados son:\n" + msjerror;
                throw new Exception(msjerror);
            }
        }

        private void dgvDatos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                switch (dgvDatos.Columns[e.ColumnIndex].Name)
                {
                    case "COC_CODIGO":
                        string nombre = dsPlanMensual.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    case "DPED_CODIGO":
                        //if(e.Value.ToString()== string.Empty) e.Value = 0;
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
                    default:
                        break;
                }

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
                    MessageBox.Show("Debe seleccionar un modelo de cocina de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No puede eliminar un detalle de pedido del plan mensual si ya fue planificado en el plan semanal", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void btnSumar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dped_codigo"]) == 0)
            {
                if (1 < (Convert.ToInt32(txtCantAPlanificar.Text) - Convert.ToInt32(txtCantPlanificada.Text)))
                {
                    if (dgvDatos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
                    {
                        int codigo = Convert.ToInt32(dvListaDatos[dgvDatos.SelectedRows[0].Index]["dpmes_codigo"]);
                        dsPlanMensual.DETALLE_PLANES_MENSUALES.FindByDPMES_CODIGO(codigo).DPMES_CANTIDADESTIMADA += 1;

                        //Llamo a la función que recalcula los valores
                        CalcularCantidades(1);
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar un modelo de cocina de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("La cantidad de unidades no puede ser mayor de lo que resta planificar", "Información: Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No puede cambiar las cantidades de un pedido", "Información: Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        dsPlanMensual.DETALLE_PLANES_MENSUALES.FindByDPMES_CODIGO(codigo).DPMES_CANTIDADESTIMADA -= 1;

                        //Llamo a la función que recalcula los valores
                        CalcularCantidades(-1);
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar un modelo de cocina de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("La cantidad de unidades no puede ser mayor de lo que ya se planificó", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No puede cambiar las cantidades de un pedido", "Información: Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Creamos el objeto de plan anual
                Entidades.PlanAnual planAnual = new GyCAP.Entidades.PlanAnual();
                planAnual.Codigo =Convert.ToInt32(cbPlanAnual.GetSelectedValue());
                planAnual.Anio =Convert.ToInt32(cbPlanAnual.GetSelectedText());

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
                    planMensual.Codigo=Convert.ToInt32(dvListaPlanes[dgvLista.SelectedRows[0].Index]["pmes_codigo"]);
                    BLL.PlanMensualBLL.GuardarPlanModificado(planMensual, dsPlanMensual);
                }
                //Si esta todo bien aceptamos los cambios que se le hacen al dataset
                dsPlanMensual.AcceptChanges();

                MessageBox.Show("Los datos se han almacenado correctamente", "Informacion: Plan Mensual - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (estadoActual == estadoUI.cargaDetalle || estadoActual==estadoUI.modificar)
                {
                    //Cambio el estado a los detalles de pedido que fueron Planificados
                    foreach (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row in dsPlanMensual.DETALLE_PLANES_MENSUALES.Rows)
                    {
                        if (row["DPED_CODIGO"].ToString() != Convert.ToString(0))
                        {
                            //Actualizo el valor del estado en la BD
                            BLL.DetallePedidoBLL.CambiarEstado(Convert.ToInt32(row.DPED_CODIGO), BLL.EstadoPedidoBLL.EstadoEnCurso);

                            //lo actualizo el el dataset
                            dsPlanMensual.DETALLE_PEDIDOS.FindByDPED_CODIGO(Convert.ToInt32(row.DPED_CODIGO)).BeginEdit();
                            dsPlanMensual.DETALLE_PEDIDOS.FindByDPED_CODIGO(Convert.ToInt32(row.DPED_CODIGO)).EDPED_CODIGO = BLL.EstadoPedidoBLL.EstadoEnCurso;
                            dsPlanMensual.DETALLE_PEDIDOS.FindByDPED_CODIGO(Convert.ToInt32(row.DPED_CODIGO)).EndEdit();
                            dsPlanMensual.DETALLE_PEDIDOS.FindByDPED_CODIGO(Convert.ToInt32(row.DPED_CODIGO)).AcceptChanges();
                        }
                    }
                    //Cambio el estado del pedido que fue planificado si todos los detalles lo fueron
                    int cont = 0; int cantfilas = 0;
                    foreach (Data.dsPlanMensual.PEDIDOSRow pm in dsPlanMensual.PEDIDOS.Rows)
                    {
                        foreach (Data.dsPlanMensual.DETALLE_PEDIDOSRow row in (Data.dsPlanMensual.DETALLE_PEDIDOSRow[])dsPlanMensual.DETALLE_PEDIDOS.Select("ped_codigo=" + pm.PED_CODIGO.ToString()))
                        {
                            if (Convert.ToInt32(row["EDPED_CODIGO"]) == BLL.EstadoPedidoBLL.EstadoEnCurso)
                            {
                                cont++;
                            }
                            cantfilas++;
                        }

                        if (cont == cantfilas)
                        {
                            //Cambio el estado del pedido en la bd
                            BLL.PedidoBLL.CambiarEstadoPedido(Convert.ToInt32(pm.PED_CODIGO), BLL.EstadoPedidoBLL.EstadoEnCurso);

                            //lo actualizo el el dataset
                            pm.BeginEdit();
                            pm.EPED_CODIGO = BLL.EstadoPedidoBLL.EstadoEnCurso;
                            pm.EndEdit();
                            pm.AcceptChanges();
                        }
                        //pongo los contadores en cero
                        cont = 0;
                        cantfilas = 0;
                    }
                }


                //Vuelvo al estado inicial de la interface
                SetInterface(estadoUI.inicio);

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Mensual - Guardado Plan Mensual", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                //Verifico si existen pedidos para ese mes
                DateTime fechaPedidos = Convert.ToDateTime("01/" + cont.ToString() + "/" + anio.ToString());

                //Busco los pedidos para esa fecha
                BLL.PedidoBLL.ObtenerPedido(fechaPedidos, dsPlanMensual);

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
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar el Plan Mensual seleccionada y todo su detalle ?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtengo el Codigo del plan
                        int codigo = Convert.ToInt32(dvListaPlanes[dgvLista.SelectedRows[0].Index]["pmes_codigo"]);

                        //Pregunto si se puede eliminar
                        if (BLL.PlanMensualBLL.ExistePlanSemanal(codigo) == true)
                        {
                            //Elimino el plan anual y su detalle de la BD
                            BLL.PlanMensualBLL.EliminarPlan(codigo);

                            //Limpio el dataset de detalles
                            dsPlanMensual.DETALLE_PLANES_MENSUALES.Clear();

                            //Lo eliminamos del dataset
                            dsPlanMensual.PLANES_MENSUALES.FindByPMES_CODIGO(codigo).Delete();
                            dsPlanMensual.PLANES_MENSUALES.AcceptChanges();

                            //Avisamos que se elimino 
                            MessageBox.Show("Se han eliminado los datos correctamente", "Información: Elemento Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //Ponemos la ventana en el estado inicial
                            SetInterface(estadoUI.inicio);
                        }
                        else { throw new Entidades.Excepciones.ElementoEnTransaccionException(); }

                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        MessageBox.Show(ex.Message, "Advertencia: Elemento en transacción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Plan Mensual de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                //Busco el Codigo del Pedido
                int codigoPedido = Convert.ToInt32(dvListaPedidos[dgvPedidos.SelectedRows[0].Index]["ped_codigo"]);

                //Obtengo el detalle del pedido
                BLL.DetallePedidoBLL.ObtenerDetallePedido(dsPlanMensual.DETALLE_PEDIDOS, codigoPedido);

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
                    MessageBox.Show("El Pedido Seleccionado no tiene Detalle", "Información: Pedido Sin Detalle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Mensual - Carga Detalle Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (e.TabPage == tpDetallePedido && seleccionPestaña==false)
            {
                e.Cancel=true;
            }
            else if (seleccionPestaña == true)
            {
                seleccionPestaña = false;
            }


        }

        private void btnPlanificar_Click(object sender, EventArgs e)
        {
            try
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
                row.EndEdit();
                dsPlanMensual.DETALLE_PLANES_MENSUALES.AddDETALLE_PLANES_MENSUALESRow(row);

                //Metodo que recalcula los valores Ingresados
                CalcularCantidades(cantidad);

                //Seteamos la interface
                tcDatos.SelectedTab = tpPlanificacion;           

             }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Plan Mensual - Carga Detalle Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        
    }
}
