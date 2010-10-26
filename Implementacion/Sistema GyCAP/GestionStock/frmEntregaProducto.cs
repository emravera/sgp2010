using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.GestionStock
{
    public partial class frmEntregaProducto : Form
    {
        private static frmEntregaProducto _frmEntregaProducto = null;
        private Data.dsEntregaProducto dsEntregaProducto = new GyCAP.Data.dsEntregaProducto();
        private DataView dvListaEntregaBus, dvListadetalleBus, dvListaStock,dvListaPedidos, dvListaDetallePedido, dvListaDetalleEntrega;
        private DataView dvComboCliBus, dvComboEmp, dvComboCli, dvComboUbicaciones;
        private enum estadoUI { inicio, nuevo, buscar, modificar, cargaDetalle };
        private static estadoUI estadoActual;
        private static bool seleccionPestaña = false;
        private static int codigoDetalle = -1; 
        private static int codigoEntrega = -1;
        
        public frmEntregaProducto()
        {
            InitializeComponent();

            //Inicializamos las grillas
            dgvDatosEntrega.AutoGenerateColumns = false;
            dgvDetalleBusqueda.AutoGenerateColumns = false;
            dgvDetallePedido.AutoGenerateColumns = false;
            dgvListaEntregas.AutoGenerateColumns = false;
            dgvPedidos.AutoGenerateColumns = false;
            dgvStock.AutoGenerateColumns = false;

            //Para cada Lista
            //**************************************** LISTAS BUSQUEDA **********************************
            //Lista de Entregas
            //Agregamos la columnas
            dgvListaEntregas.Columns.Add("ENTREGA_CODIGO", "Código");
            dgvListaEntregas.Columns.Add("ENTREGA_FECHA", "Fecha Entrega");
            dgvListaEntregas.Columns.Add("CLI_CODIGO", "Cliente");
            dgvListaEntregas.Columns.Add("E_CODIGO", "Responsable");
            
            //Seteamos el modo de tamaño de las columnas
            dgvListaEntregas.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvListaEntregas.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvListaEntregas.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvListaEntregas.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvListaEntregas.Columns["ENTREGA_CODIGO"].DataPropertyName = "ENTREGA_CODIGO";
            dgvListaEntregas.Columns["ENTREGA_FECHA"].DataPropertyName = "ENTREGA_FECHA";
            dgvListaEntregas.Columns["CLI_CODIGO"].DataPropertyName = "CLI_CODIGO";
            dgvListaEntregas.Columns["E_CODIGO"].DataPropertyName = "E_CODIGO";
            

            //Creamos el dataview y lo asignamos a la grilla
            dvListaEntregaBus = new DataView(dsEntregaProducto.ENTREGA_PRODUCTO);
            dgvListaEntregas.DataSource = dvListaEntregaBus;

            //Lista de Detalles de Entregas
            //Agregamos la columnas
            dgvDetalleBusqueda.Columns.Add("DENT_CODIGO", "Código");
            dgvDetalleBusqueda.Columns.Add("ENTREGA_CODIGO", "Entrega");
            dgvDetalleBusqueda.Columns.Add("DENT_CONTENIDO", "Stock");
            dgvDetalleBusqueda.Columns.Add("DPED_CODIGO", "N° Pedido");
            dgvDetalleBusqueda.Columns.Add("DENT_CANTIDAD", "Cantidad");

            //Seteamos el modo de tamaño de las columnas
            dgvDetalleBusqueda.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleBusqueda.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleBusqueda.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleBusqueda.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetalleBusqueda.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetalleBusqueda.Columns["DENT_CODIGO"].DataPropertyName = "DENT_CODIGO";
            dgvDetalleBusqueda.Columns["ENTREGA_CODIGO"].DataPropertyName = "ENTREGA_CODIGO";
            dgvDetalleBusqueda.Columns["DENT_CONTENIDO"].DataPropertyName = "DENT_CONTENIDO";
            dgvDetalleBusqueda.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";
            dgvDetalleBusqueda.Columns["DENT_CANTIDAD"].DataPropertyName = "DENT_CANTIDAD";

            //Creamos el dataview y lo asignamos a la grilla
            dvListadetalleBus = new DataView(dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO);
            dgvDetalleBusqueda.DataSource = dvListadetalleBus;

            //**************************************** LISTAS DE DATOS **********************************
            //Lista de
            //Agregamos la columnas
            dgvStock.Columns.Add("USTCK_NUMERO", "Numero");
            dgvStock.Columns.Add("USTCK_CODIGO", "Código");
            dgvStock.Columns.Add("USTCK_NOMBRE", "Stock");
            dgvStock.Columns.Add("USTCK_CANTIDADREAL", "Cantidad");
            dgvStock.Columns.Add("UMED_CODIGO", "Unidad Medida");

            //Seteamos el modo de tamaño de las columnas
            dgvStock.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStock.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStock.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStock.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvStock.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvStock.Columns["USTCK_NUMERO"].DataPropertyName = "USTCK_NUMERO";
            dgvStock.Columns["USTCK_CODIGO"].DataPropertyName = "USTCK_CODIGO";
            dgvStock.Columns["USTCK_NOMBRE"].DataPropertyName = "USTCK_NOMBRE";
            dgvStock.Columns["USTCK_CANTIDADREAL"].DataPropertyName = "USTCK_CANTIDADREAL";
            dgvStock.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaStock = new DataView(dsEntregaProducto.UBICACIONES_STOCK);
            dgvStock.DataSource = dvListaStock;

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
            dvListaPedidos = new DataView(dsEntregaProducto.PEDIDOS);
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
            dvListaDetallePedido = new DataView(dsEntregaProducto.DETALLE_PEDIDOS);
            dgvDetallePedido.DataSource = dvListaDetallePedido;

            //*************************************************** Lista de DETALLE ENTREGA ***************************+
            //Agregamos la columnas
            dgvDatosEntrega.Columns.Add("DENT_CODIGO", "Código");
            dgvDatosEntrega.Columns.Add("ENTREGA_CODIGO", "Entrega");
            dgvDatosEntrega.Columns.Add("DENT_CONTENIDO", "Contenido");
            dgvDatosEntrega.Columns.Add("DENT_CANTIDAD", "Cantidad");
            dgvDatosEntrega.Columns.Add("DPED_CODIGO", "Pedido");
            
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDatosEntrega.Columns["DENT_CODIGO"].DataPropertyName = "DENT_CODIGO";
            dgvDatosEntrega.Columns["ENTREGA_CODIGO"].DataPropertyName = "ENTREGA_CODIGO";
            dgvDatosEntrega.Columns["DENT_CONTENIDO"].DataPropertyName = "DENT_CONTENIDO";
            dgvDatosEntrega.Columns["DENT_CANTIDAD"].DataPropertyName = "DENT_CANTIDAD";
            dgvDatosEntrega.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";
            
            //Seteamos el modo de tamaño de las columnas
            dgvDatosEntrega.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatosEntrega.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatosEntrega.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatosEntrega.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatosEntrega.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDetalleEntrega = new DataView(dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO);
            dgvDatosEntrega.DataSource = dvListaDetalleEntrega;

            //************************************************************************************************
            
            //Se llenan los DataTable
            
            //Llenamos el dataset de Clientes
            BLL.ClienteBLL.ObtenerTodos(dsEntregaProducto.CLIENTES);

            //Llenamos el Datatable de Cocinas
            BLL.CocinaBLL.ObtenerCocinas(dsEntregaProducto.COCINAS);

            //Llenamos el datatable con los estados de pedidos
            BLL.EstadoPedidoBLL.ObtenerTodos(dsEntregaProducto.ESTADO_PEDIDOS);

            //Llenamos el datatable con los estados de detalle Pedido
            BLL.EstadoDetallePedidoBLL.ObtenerTodos(dsEntregaProducto.ESTADO_DETALLE_PEDIDOS);

            //Llenamos el datatable de Empleados
            BLL.EmpleadoBLL.ObtenerEmpleados(dsEntregaProducto.EMPLEADOS);

            //Llenamos el datatable de Unidades de Medida
            BLL.UnidadMedidaBLL.ObtenerTodos(dsEntregaProducto.UNIDADES_MEDIDA);

            //Se carga el stock de productos terminados
            BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsEntregaProducto.UBICACIONES_STOCK, BLL.ContenidoUbicacionStockBLL.ContenidoCocina);

            //Cargamos el combo de busqueda
            //Cargamos el combo de los clientes
            dvComboCliBus = new DataView(dsEntregaProducto.CLIENTES);
            cbClienteBus.SetDatos(dvComboCliBus, "cli_codigo", "cli_razonsocial", "Seleccione", false);
            
            //Cargamos los combos de datos
            //Cargo el combo de empleados
            dvComboEmp = new DataView(dsEntregaProducto.EMPLEADOS); 
            string[] cabecera = { "e_apellido" ,"e_nombre"  };
            cbEmpleado.SetDatos(dvComboEmp, "e_codigo",cabecera,",", "Seleccione", false);

            //Cargamos el combo de los clientes
            dvComboCli = new DataView(dsEntregaProducto.CLIENTES);
            cbCliente.SetDatos(dvComboCli, "cli_codigo", "cli_razonsocial", "Seleccione", false);

            //Cargamos el combo de ubicaciones de stock de productos terminados
            dvComboUbicaciones = new DataView(dsEntregaProducto.UBICACIONES_STOCK);
            cbUbicacionesStock.SetDatos(dvComboUbicaciones, "ustck_numero", "ustck_codigo", "Seleccione", false);

            //Seteamos la interface
            SetInterface(estadoUI.inicio);

        }

        #region Servicios
        //Método para evitar la creación de más de una pantalla
        public static frmEntregaProducto Instancia
        {
            get
            {
                if (_frmEntregaProducto == null || _frmEntregaProducto.IsDisposed)
                {
                    _frmEntregaProducto = new frmEntregaProducto();
                }
                else
                {
                    _frmEntregaProducto.BringToFront();
                }
                return _frmEntregaProducto;
            }
            set
            {
                _frmEntregaProducto = value;
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

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                //Cuando Arranca la pantalla
                case estadoUI.inicio:
                    gbGrillaDetalleBus.Visible = false;
                    gbGrillaEntregasBus.Visible = false;
                    
                    cbClienteBus.SetSelectedIndex(-1);
                    chCliente.Checked = false;
                    chFechaEntrega.Checked = false;

                    //Seteo la fecha actual al control de la fecha
                    dtpFechaBusqueda.Value = BLL.DBBLL.GetFechaServidor();

                    //Selecciono el tabcontrol
                    tcEntregaProducto.SelectedTab = tpBuscar;
                    estadoActual = estadoUI.inicio;
                    break;
                case estadoUI.buscar:
                    gbGrillaDetalleBus.Visible = false;
                    gbGrillaEntregasBus.Visible = true;
                    
                    //Escondo las filas en la que esta
                    dgvListaEntregas.Columns["ENTREGA_CODIGO"].Visible = false;

                    //Selecciono el tabcontrol
                    tcEntregaProducto.SelectedTab = tpBuscar;
                    estadoActual = estadoUI.buscar;
                    break;
                case estadoUI.nuevo:
                    //Manejo de controles
                    gbStock.Visible = false;
                    gbDetalleGrilla.Visible = false;
                    gbBotones.Visible = false;
                    gbDatosPrincipales.Visible = true;
                    gbDatosPrincipales.Enabled = true;

                    //Manejo de Combos
                    cbCliente.SetSelectedIndex(-1);
                    cbEmpleado.SetSelectedIndex(-1);

                    //Habilito el boton ver detalle
                    btnVerDetalle.Enabled = true;
                    dgvPedidos.Visible = true;

                    //Seteo la fecha actual al control de la fecha
                    dtpFechaEntrega.Value = BLL.DBBLL.GetFechaServidor();
                    
                    //Selecciono el tabcontrol
                    tcEntregaProducto.SelectedTab = tpDatos;
                    estadoActual = estadoUI.nuevo;
                    break;
                case estadoUI.cargaDetalle:
                    //Manejo de controles
                    gbStock.Visible = true;
                    gbDetalleGrilla.Visible = true;
                    gbBotones.Visible = true;
                    gbDatosPrincipales.Enabled = false;

                    //Si no titne pedidos inhabilito el boton de ver detalle
                    if (dsEntregaProducto.PEDIDOS.Rows.Count == 0)
                    {
                        btnVerDetalle.Enabled = false;
                        dgvPedidos.Visible = false;
                    }

                    //Escondo las columnas de las grillas
                    dgvStock.Columns["USTCK_NUMERO"].Visible = false;
                    dgvStock.Columns["USTCK_CODIGO"].Visible = false;

                    //Escondo las columnas de las grillas de pedidos
                    dgvPedidos.Columns["PED_CODIGO"].Visible = false;
                    dgvPedidos.Columns["PED_FECHAENTREGAREAL"].Visible = false;
                    dgvPedidos.Columns["PED_FECHA_ALTA"].Visible = false;
                    
                    dgvDetallePedido.Columns["PED_CODIGO"].Visible = false;
                    dgvDetallePedido.Columns["DPED_FECHA_CANCELACION"].Visible = false;

                    //Lista del Detalle
                    dgvDatosEntrega.Columns["DENT_CODIGO"].Visible = false;
                    dgvDatosEntrega.Columns["ENTREGA_CODIGO"].Visible = false;

                    //Selecciono el tabcontrol
                    tcEntregaProducto.SelectedTab = tpDatos;
                    estadoActual = estadoUI.cargaDetalle;
                    break;
                case estadoUI.modificar:
                    //Manejo de controles
                    gbStock.Visible = true;
                    gbDetalleGrilla.Visible = true;
                    gbBotones.Visible = true;
                    gbDatosPrincipales.Enabled = false;

                    //Si no titne pedidos inhabilito el boton de ver detalle
                    if (dsEntregaProducto.PEDIDOS.Rows.Count == 0)
                    {
                        btnVerDetalle.Enabled = false;
                        dgvPedidos.Visible = false;
                    }

                    //Escondo las columnas de las grillas
                    dgvStock.Columns["USTCK_NUMERO"].Visible = false;
                    dgvStock.Columns["USTCK_CODIGO"].Visible = false;

                    //Escondo las columnas de las grillas de pedidos
                    dgvPedidos.Columns["PED_CODIGO"].Visible = false;
                    dgvPedidos.Columns["PED_FECHAENTREGAREAL"].Visible = false;
                    dgvPedidos.Columns["PED_FECHA_ALTA"].Visible = false;

                    dgvDetallePedido.Columns["PED_CODIGO"].Visible = false;
                    dgvDetallePedido.Columns["DPED_FECHA_CANCELACION"].Visible = false;

                    //Lista del Detalle
                    dgvDatosEntrega.Columns["DENT_CODIGO"].Visible = false;
                    dgvDatosEntrega.Columns["ENTREGA_CODIGO"].Visible = false;

                    //Selecciono el tabcontrol
                    tcEntregaProducto.SelectedTab = tpDatos;
                    estadoActual = estadoUI.modificar;
                    break;
            }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        #endregion


        #region Pestaña Busqueda
        private string validarBusqueda ()
        {
            string msjerror = string.Empty;

            if (chCliente.Checked == false && chFechaEntrega.Checked == false ) 
            {
                msjerror = msjerror +"-Debe seleccionar un criterio de Busqueda\n";
            }
            if(chCliente.Checked==true && cbClienteBus.GetSelectedIndex() ==-1)
            {
                msjerror = msjerror +"-Debe seleccionar un cliente del combo\n";
            }

            if(msjerror.Length > 0)
            {
                msjerror="Los errores de Validación encontrados son:\n" + msjerror;
            }

            return msjerror;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string validacion = validarBusqueda();

                //Valido que este seleccionado algun criterio de busqueda
                if (validacion== string.Empty)
                {
                    int codigoCliente;
                    DateTime fechaEntrega;

                    //Caso de busqueda por cliente
                    if (chCliente.Checked == true && chFechaEntrega.Checked==false)
                    {
                         //busco el codigo del cliente
                         codigoCliente = Convert.ToInt32(cbClienteBus.GetSelectedValue());
                                               
                        //Llamo a la funcion de búsqueda
                        BLL.EntregaProductoBLL.ObtenerEntregas(codigoCliente, dsEntregaProducto.ENTREGA_PRODUCTO);
                    }

                    //Caso de busqueda por fecha
                    if (chCliente.Checked == false && chFechaEntrega.Checked == true)
                    {
                        //busco la fecha de entrega
                        fechaEntrega = Convert.ToDateTime(dtpFechaBusqueda.Value);

                        //Llamo a la funcion de búsqueda
                        BLL.EntregaProductoBLL.ObtenerEntregas(fechaEntrega, dsEntregaProducto.ENTREGA_PRODUCTO);
                    }

                    //Caso de busqueda por ambos parametros
                    if (chCliente.Checked == true && chFechaEntrega.Checked == true)
                    {
                        //busco el codigo del cliente
                        codigoCliente = Convert.ToInt32(cbCliente.GetSelectedValue());

                        //busco la fecha de entrega
                        fechaEntrega = Convert.ToDateTime(dtpFechaBusqueda.Value);

                        //Llamo a la funcion de búsqueda
                        BLL.EntregaProductoBLL.ObtenerEntregas(codigoCliente, fechaEntrega, dsEntregaProducto.ENTREGA_PRODUCTO);
                    }

                    //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                    //por una consulta a la BD
                    dvListaEntregaBus.Table = dsEntregaProducto.ENTREGA_PRODUCTO;

                    if (dsEntregaProducto.ENTREGA_PRODUCTO.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron Entregas de productos con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetInterface(estadoUI.inicio);
                    }
                    else
                    {
                        SetInterface(estadoUI.buscar);
                    }
                }
                else
                {
                    MessageBox.Show(validacion, "Informacion: Entrega Producto - Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Entrega Producto - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }




        }

        private void dgvListaEntregas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != String.Empty)
            {
                switch (dgvListaEntregas.Columns[e.ColumnIndex].Name)
                {
                    case "CLI_CODIGO":
                        string nombre = dsEntregaProducto.CLIENTES.FindByCLI_CODIGO(Convert.ToInt32(e.Value)).CLI_RAZONSOCIAL;
                        e.Value = nombre;
                        break;
                    case "E_CODIGO":
                        string nom = dsEntregaProducto.EMPLEADOS.FindByE_CODIGO(Convert.ToInt32(e.Value)).E_NOMBRE;
                        string ape = dsEntregaProducto.EMPLEADOS.FindByE_CODIGO(Convert.ToInt32(e.Value)).E_APELLIDO;
                        e.Value = ape + "," + nom;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvListaEntregas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
               try
            {
                //Se programa la busqueda del detalle
                //Limpiamos el Dataset
                dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Clear();

                int codigo = Convert.ToInt32(dvListaEntregaBus[dgvListaEntregas.SelectedRows[0].Index]["entrega_codigo"]);

                //Se llama a la funcion de busqueda del detalle
                BLL.EntregaProductoBLL.ObtenerDetalleEntrega(codigo, dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListadetalleBus.Table = dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO;

                if (dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Detalles para esa entrega.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //muestro el groupbox del detalle
                    SetInterface(estadoUI.buscar);
                    gbGrillaDetalleBus.Visible = true;
                    dgvDetalleBusqueda.Columns["DENT_CODIGO"].Visible = false;
                    dgvDetalleBusqueda.Columns["ENTREGA_CODIGO"].Visible = false;
                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Entrega Producto - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }
       
        #endregion
        #region Pestaña Datos

        private string ValidarCargaDetalle()
        {
            string msjerror = string.Empty;

            if (cbCliente.GetSelectedIndex() == -1) msjerror = msjerror + "-Debe seleccionar un cliente para la entrega\n";
            if (cbEmpleado.GetSelectedIndex() == -1) msjerror = msjerror + "-Debe seleccionar un responsable de la entrega\n";

            if (msjerror.Length > 0)
            {
                msjerror = "Los errores de validacion encontrados son:\n" + msjerror;
            }

            return msjerror;

        }
        
        
        private void btnCargaDetalle_Click(object sender, EventArgs e)
        {
            string validacion = ValidarCargaDetalle();

            if (validacion == string.Empty)
            {
                //Se obtiene el codigo del cliente
                int codigoCliente = Convert.ToInt32(cbCliente.GetSelectedValue());
                
                //Se cargan los pedidos del cliente seleccionado
                BLL.PedidoBLL.ObtenerPedidoCliente(codigoCliente, BLL.EstadoPedidoBLL.EstadoFinalizado, dsEntregaProducto.PEDIDOS);

                SetInterface(estadoUI.cargaDetalle);
            }
            else
            {
                MessageBox.Show(validacion, "Informacion: Entrega Producto - Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        nombre = dsEntregaProducto.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    case "EPED_CODIGO":
                        nombre = dsEntregaProducto.ESTADO_PEDIDOS.FindByEPED_CODIGO(Convert.ToInt32(e.Value)).EPED_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "CLI_CODIGO":
                        nombre = dsEntregaProducto.CLIENTES.FindByCLI_CODIGO(Convert.ToInt32(e.Value)).CLI_RAZONSOCIAL;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }

            }
        }
        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                //Busco el Codigo del Pedido
                int codigoPedido = Convert.ToInt32(dvListaPedidos[dgvPedidos.SelectedRows[0].Index]["ped_codigo"]);

                //Obtengo el detalle del pedido
                BLL.DetallePedidoBLL.ObtenerDetallePedido(dsEntregaProducto.DETALLE_PEDIDOS, codigoPedido);

                if (dsEntregaProducto.DETALLE_PEDIDOS.Rows.Count > 0)
                {
                    //Selecciono el tab del detalle
                    gbDetallePedido.Visible = true;
                    btnEntregar.Enabled = true;
                    seleccionPestaña = true;
                    tcDatos.SelectedTab = tpDetallePedido;

                    //Escondo las columnas que no quiero que se vean
                    dgvDetallePedido.Columns["DPED_CODIGO"].Visible = false;
                }
                else
                {
                    gbDetallePedido.Visible = false;
                    btnEntregar.Enabled = false;
                    MessageBox.Show("El Pedido Seleccionado no tiene Detalle", "Información: Pedido Sin Detalle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Entrega Producto - Carga Detalle Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        nombre = dsEntregaProducto.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    case "EDPED_CODIGO":
                        nombre = dsEntregaProducto.ESTADO_DETALLE_PEDIDOS.FindByEDPED_CODIGO(Convert.ToInt32(e.Value)).EDPED_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }

            }
        }
        private void dgvDetalleBusqueda_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                string nombre;

                switch (dgvDatosEntrega.Columns[e.ColumnIndex].Name)
                {
                    case "DENT_CONTENIDO":
                        nombre = dsEntregaProducto.UBICACIONES_STOCK.FindByUSTCK_NUMERO(Convert.ToInt32(e.Value)).USTCK_CODIGO;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        private void dgvStock_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                string nombre;

                switch (dgvStock.Columns[e.ColumnIndex].Name)
                {
                    case "UMED_CODIGO":
                        nombre = dsEntregaProducto.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(e.Value)).UMED_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "USTCK_CANTIDAD":
                        e.Value = Math.Round(Convert.ToDecimal(e.Value),0);
                        break;

                    default:
                        break;
                }
            }
        }

        //Metodo que valida cuando se agraga ala stock
        private string VelidarAgregarStock()
        {
            string msjerror = string.Empty;

            if (dgvStock.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                decimal ubicacion =Convert.ToDecimal(dvListaStock[dgvStock.SelectedRows[0].Index]["ustck_numero"]);
                foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Rows)
                {
                    if (ubicacion == row.DENT_CONTENIDO && row.DPED_CODIGO == 0)
                    {
                        msjerror = msjerror + "-La ubicación que intenta agregar ya se encuentra en el detalle de entrega de producto \n";
                    }
                }

            }
            else msjerror = msjerror + "-Debe Seleccionar una fila de Stock para agregar\n";

            //Validamos las cantidades
            decimal cantidad = Convert.ToInt32(dvListaStock[dgvStock.SelectedRows[0].Index]["ustck_cantidadreal"]);
            int cantidadEntrega = Convert.ToInt32(numCantidad.Value);

            if (cantidadEntrega > cantidad) msjerror= msjerror + "-La cantidad a entregar no puede ser mayor de lo que hay en stock\n";

            if (msjerror.Length > 0)
            {
                msjerror = "Los errores de validacion encontrados son:\n" + msjerror;
            }

            return msjerror;
        }

        //Metodo que agrega desde el stock a la entrega
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string validar = VelidarAgregarStock();

                if (validar == string.Empty)
                {
                    //Tomamos los datos de la fila seleccionada
                    int ubicacion =Convert.ToInt32(dvListaStock[dgvStock.SelectedRows[0].Index]["ustck_numero"]);
                    decimal cantidad = Convert.ToInt32(dvListaStock[dgvStock.SelectedRows[0].Index]["ustck_cantidadreal"]);

                    //Tomamos la cantidad a entregar
                    int cantidadEntrega = Convert.ToInt32(numCantidad.Value);

                    //Editamos la fila del stock
                    dsEntregaProducto.UBICACIONES_STOCK.FindByUSTCK_NUMERO(ubicacion).USTCK_CANTIDADREAL -= cantidadEntrega;
                    dsEntregaProducto.UBICACIONES_STOCK.AcceptChanges();

                    //Creamos y editamos la fila a agregar en el datatable de detalle entrega
                    Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row = dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.NewDETALLE_ENTREGA_PRODUCTORow();
                    row.BeginEdit();
                    row.DENT_CODIGO = codigoDetalle--;
                    row.ENTREGA_CODIGO = codigoEntrega;
                    row.DENT_CONTENIDO = ubicacion;
                    row.DPED_CODIGO = 0;
                    row.DENT_CANTIDAD = Convert.ToInt32(numCantidad.Value);
                    row.EndEdit();

                    dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.AddDETALLE_ENTREGA_PRODUCTORow(row);

                    //Pongo el control de cantidad en cero
                    numCantidad.Value = 0;
                }
                else
                {
                    MessageBox.Show(validar, "Error: Entrega Producto - Validación Detalle Entrega", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Entrega Producto - Carga Detalle Entrega", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDatosEntrega.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigo = Convert.ToInt32(dvListaDetalleEntrega[dgvDatosEntrega.SelectedRows[0].Index]["dent_codigo"]);
                int codigoStock = Convert.ToInt32(dvListaDetalleEntrega[dgvDatosEntrega.SelectedRows[0].Index]["dent_contenido"]);
                int cantidadStock = Convert.ToInt32(dvListaDetalleEntrega[dgvDatosEntrega.SelectedRows[0].Index]["dent_cantidad"]);
                
                //Actualizo el Stock
                dsEntregaProducto.UBICACIONES_STOCK.FindByUSTCK_NUMERO(codigoStock).USTCK_CANTIDADREAL += cantidadStock;
                dsEntregaProducto.AcceptChanges();

                //Elimino el dataset
                dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.FindByDENT_CODIGO(codigo).Delete();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un detalle de entrega de producto para poder eliminarlo.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSumar_Click(object sender, EventArgs e)
        {
            if (dgvDatosEntrega.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Verifico si deriva de un pedido
                int pedido = Convert.ToInt32(dvListaDetalleEntrega[dgvDatosEntrega.SelectedRows[0].Index]["dped_codigo"]);
                if (pedido == 0)
                {
                    int codigo = Convert.ToInt32(dvListaDetalleEntrega[dgvDatosEntrega.SelectedRows[0].Index]["dent_codigo"]);
                    dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.FindByDENT_CODIGO(codigo).DENT_CANTIDAD += 1;

                    //Actualizo la cantidad de stock
                    int codigoStock = Convert.ToInt32(dvListaDetalleEntrega[dgvDatosEntrega.SelectedRows[0].Index]["dent_contenido"]);
                    dsEntregaProducto.UBICACIONES_STOCK.FindByUSTCK_NUMERO(codigoStock).USTCK_CANTIDADREAL -= 1;
                    dsEntregaProducto.AcceptChanges();
                }
                else
                {
                    MessageBox.Show("No se pueden modificar las cantidades en una entrega de pedido", "Información: Entrega Pedido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un detalle de entrega de producto para poder modificar sus cantidades.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            if (dgvDatosEntrega.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Verifico si deriva de un pedido
                int pedido = Convert.ToInt32(dvListaDetalleEntrega[dgvDatosEntrega.SelectedRows[0].Index]["dped_codigo"]);
                if (pedido == 0)
                {
                    int codigo = Convert.ToInt32(dvListaDetalleEntrega[dgvDatosEntrega.SelectedRows[0].Index]["dent_codigo"]);
                    dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.FindByDENT_CODIGO(codigo).DENT_CANTIDAD -= 1;

                    //Actualizo la cantidad de stock
                    int codigoStock = Convert.ToInt32(dvListaDetalleEntrega[dgvDatosEntrega.SelectedRows[0].Index]["dent_contenido"]);
                    dsEntregaProducto.UBICACIONES_STOCK.FindByUSTCK_NUMERO(codigoStock).USTCK_CANTIDADREAL += 1;
                    dsEntregaProducto.AcceptChanges();

                }
                else
                {
                    MessageBox.Show("No se pueden modificar las cantidades en una entrega de pedido", "Información: Entrega Pedido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un detalle de entrega de producto para poder modificar sus cantidades.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Metodo que agrega al detalle desde el pedido
        private void btnEntregar_Click(object sender, EventArgs e)
        {

            if (dgvDatosEntrega.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                if (cbUbicacionesStock.GetSelectedIndex() == -1)
                {
                    MessageBox.Show("Debe seleccionar una ubicación de stock para poder descontarlo.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Tomamos los datos de la fila seleccionada
                    int ubicacion = Convert.ToInt32(cbUbicacionesStock.GetSelectedValue());
                    int cantidad = Convert.ToInt32(dvListaDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_cantidad"]);
                    int pedido = Convert.ToInt32(dvListaDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_codigo"]);

                    //Actualizamos la cantidad de stock en la pantalla
                    dsEntregaProducto.UBICACIONES_STOCK.FindByUSTCK_NUMERO(ubicacion).USTCK_CANTIDADREAL -= cantidad;
                    dsEntregaProducto.UBICACIONES_STOCK.AcceptChanges();

                    //Creamos y editamos la fila a agregar en el datatable de detalle entrega
                    Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row = dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.NewDETALLE_ENTREGA_PRODUCTORow();
                    row.BeginEdit();
                    row.DENT_CODIGO = codigoDetalle--;
                    row.ENTREGA_CODIGO = codigoEntrega;
                    row.DENT_CONTENIDO = ubicacion;
                    row.DPED_CODIGO = pedido;
                    row.DENT_CANTIDAD = Convert.ToInt32(cantidad);
                    row.EndEdit();

                    dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.AddDETALLE_ENTREGA_PRODUCTORow(row);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un detalle de pedido para poder agregarlo.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            //Volvemos al estado de Nuevo
            
            //Primero limpiamos los Datasets
            dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Clear();
            dsEntregaProducto.PEDIDOS.Clear();
            dsEntregaProducto.DETALLE_PEDIDOS.Clear();
            dsEntregaProducto.ENTREGA_PRODUCTO.Clear();

            //Seteamos la interface
            SetInterface(estadoUI.nuevo);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        //Metodo que guarda tanto la entrega como el detalle de la misma y actualiza los stocks
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
               
                    //Se genera la cabecera de Entrega Producto
                    Entidades.EntregaProducto entrega = new GyCAP.Entidades.EntregaProducto();
                    Entidades.Cliente cliente = new GyCAP.Entidades.Cliente();
                    Entidades.Empleado empleado = new GyCAP.Entidades.Empleado();

                    //Se carga el codigo del cliente
                    cliente.Codigo = Convert.ToInt32(cbCliente.GetSelectedValue());

                    //Se carga el codigo del empleado
                    empleado.Codigo = Convert.ToInt32(cbEmpleado.GetSelectedValue());

                    //Se cargan todos los parametros del objeto entrega producto
                    entrega.Cliente = cliente;
                    entrega.Empleado = empleado;
                    entrega.Fecha = Convert.ToDateTime(dtpFechaEntrega.Value);
                if (estadoActual == estadoUI.cargaDetalle)
                {
                    //Metodo que la cabecera y el detalle de la Entrega del producto
                    BLL.EntregaProductoBLL.GuardarEntrega(entrega, dsEntregaProducto);
                }
                else if (estadoActual == estadoUI.modificar)
                {
                    //Metodo que guarda la cabecera y detalle modificados
                    entrega.Codigo = Convert.ToInt32(dvListaEntregaBus[dgvListaEntregas.SelectedRows[0].Index]["entrega_codigo"]);
                    BLL.EntregaProductoBLL.GuardarEntregaModificada(entrega, dsEntregaProducto);
                }
                

                //Mostramos el mensaje informando que se guardaron los datos
                MessageBox.Show("Los datos se han almacenado correctamente", "Informacion: Plan Mensual - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //********************************************** ACTUALIZACIONES POSTERIORES ****************************************************************************
                //Metodo que actualiza el estado de los pedidos deacuerdo a lo que se guardo
                if (estadoActual == estadoUI.cargaDetalle || estadoActual == estadoUI.modificar)
                {
                    //Cambio el estado a los detalles de pedido que fueron Planificados
                    foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Rows)
                    {
                        if (row["DPED_CODIGO"].ToString() != Convert.ToString(0))
                        {
                            //Actualizo el valor del estado en la BD
                            BLL.DetallePedidoBLL.CambiarEstado(Convert.ToInt32(row.DPED_CODIGO), BLL.EstadoPedidoBLL.EstadoEntregado);

                            //lo actualizo el el dataset
                            dsEntregaProducto.DETALLE_PEDIDOS.FindByDPED_CODIGO(Convert.ToInt32(row.DPED_CODIGO)).BeginEdit();
                            dsEntregaProducto.DETALLE_PEDIDOS.FindByDPED_CODIGO(Convert.ToInt32(row.DPED_CODIGO)).EDPED_CODIGO = BLL.EstadoPedidoBLL.EstadoEntregado;
                            dsEntregaProducto.DETALLE_PEDIDOS.FindByDPED_CODIGO(Convert.ToInt32(row.DPED_CODIGO)).EndEdit();
                            dsEntregaProducto.DETALLE_PEDIDOS.FindByDPED_CODIGO(Convert.ToInt32(row.DPED_CODIGO)).AcceptChanges();
                        }
                    }
                    //Cambio el estado del pedido que fue entregado si todos los detalles lo fueron
                    int cont = 0; int cantfilas = 0;
                    foreach (Data.dsEntregaProducto.PEDIDOSRow pm in dsEntregaProducto.PEDIDOS.Rows)
                    {
                        foreach (Data.dsEntregaProducto.DETALLE_PEDIDOSRow row in (Data.dsEntregaProducto.DETALLE_PEDIDOSRow[])dsEntregaProducto.DETALLE_PEDIDOS.Select("ped_codigo=" + pm.PED_CODIGO.ToString()))
                        {
                            if (Convert.ToInt32(row["EDPED_CODIGO"]) == BLL.EstadoPedidoBLL.EstadoEntregado)
                            {
                                cont++;
                            }
                            cantfilas++;
                        }

                        if (cont == cantfilas)
                        {
                            //Cambio el estado del pedido en la bd
                            BLL.PedidoBLL.CambiarEstadoPedido(Convert.ToInt32(pm.PED_CODIGO), BLL.EstadoPedidoBLL.EstadoEntregado);

                            //lo actualizo el el dataset
                            pm.BeginEdit();
                            pm.EPED_CODIGO = BLL.EstadoPedidoBLL.EstadoEntregado;
                            pm.EndEdit();
                            pm.AcceptChanges();
                        }
                        //pongo los contadores en cero
                        cont = 0;
                        cantfilas = 0;
                    }
                }

                //Se deben actualizar las cantidades de stock en las filas agregadas o nuevas
                foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow[])dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    //Actualizo la cantidad de stock
                    BLL.UbicacionStockBLL.ActualizarCantidadesStock(Convert.ToInt32(row.DENT_CONTENIDO), (Convert.ToDecimal(row.DENT_CANTIDAD) * -1),(Convert.ToDecimal(row.DENT_CANTIDAD) * -1));
                }
                
                //Si se modificaron filas entonces SUMO las cantidades anteriores y luego resto las cantidades actuales
                foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow[])dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Select(null, null, System.Data.DataViewRowState.ModifiedOriginal))
                {
                    //Actualizo la cantidad de stock
                    BLL.UbicacionStockBLL.ActualizarCantidadesStock(Convert.ToInt32(row.DENT_CONTENIDO), Convert.ToDecimal(row.DENT_CANTIDAD), Convert.ToDecimal(row.DENT_CANTIDAD));
                }
                foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow[])dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    //Actualizo la cantidad de stock
                    BLL.UbicacionStockBLL.ActualizarCantidadesStock(Convert.ToInt32(row.DENT_CONTENIDO), Convert.ToDecimal(row.DENT_CANTIDAD), Convert.ToDecimal(row.DENT_CANTIDAD));
                }

                //Sumo las cantidades de las filas que fueron eliminadas
                foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow[])dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Actualizo la cantidad de stock
                    BLL.UbicacionStockBLL.ActualizarCantidadesStock(Convert.ToInt32(row.DENT_CONTENIDO), Convert.ToDecimal(row.DENT_CANTIDAD), Convert.ToDecimal(row.DENT_CANTIDAD));
                }
                //******************************************************************** FIN ACTUALIZACIONES ****************************************************************************************************************************************

                //Si esta todo bien aceptamos los cambios que se le hacen al dataset
                dsEntregaProducto.AcceptChanges();

                //Vuelvo al estado inicial de la interface
                SetInterface(estadoUI.inicio);
                
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Entrega Producto - Carga Detalle Entrega", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgvDatosEntrega_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                string nombre;

                switch (dgvDatosEntrega.Columns[e.ColumnIndex].Name)
                {
                    case "DENT_CONTENIDO":
                        nombre = dsEntregaProducto.UBICACIONES_STOCK.FindByUSTCK_NUMERO(Convert.ToInt32(e.Value)).USTCK_CODIGO;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                //Busco en la Lista de busqueda los datos de la cabecera y los seteo
                int codigoCliente = Convert.ToInt32(dvListaEntregaBus[dgvListaEntregas.SelectedRows[0].Index]["cli_codigo"]);
                int codigoEmpleado = Convert.ToInt32(dvListaEntregaBus[dgvListaEntregas.SelectedRows[0].Index]["e_codigo"]);
                DateTime fechaEntrega = Convert.ToDateTime(dvListaEntregaBus[dgvListaEntregas.SelectedRows[0].Index]["entrega_fecha"]);

                cbCliente.SetSelectedValue(codigoCliente);
                cbEmpleado.SetSelectedValue(codigoEmpleado);
                dtpFechaBusqueda.Value = fechaEntrega;

                //Se cargan los pedidos para el cliente
                BLL.PedidoBLL.ObtenerPedidoCliente(codigoCliente, BLL.EstadoPedidoBLL.EstadoFinalizado, dsEntregaProducto.PEDIDOS);
                
                //Pongo en 0 los null de los pedidos
                foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Rows)
                {
                    try
                    {
                        decimal? pedido = row.DPED_CODIGO;
                    }
                    catch (Exception)
                    {
                        row.BeginEdit();
                        row.DPED_CODIGO = 0;
                        row.EndEdit();
                        row.AcceptChanges();
                    }
                }

                //Seteamos la interface a estado modidificar
                SetInterface(estadoUI.modificar);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Entrega Producto - Modificación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListaEntregas.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
                {
                    //Preguntamos si está seguro
                    DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar la entrega de producto seleccionada y todo su detalle ?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        
                            //Obtengo el Codigo del plan
                            int codigo = Convert.ToInt32(dvListaEntregaBus[dgvListaEntregas.SelectedRows[0].Index]["entrega_codigo"]);
                                                                      
                            //Elimino la entrega del producto y su detalle de la BD
                            BLL.EntregaProductoBLL.EliminarEntrega(codigo);

                            //Limpio el dataset de detalles
                            dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Clear();

                            //Lo eliminamos del dataset
                            dsEntregaProducto.ENTREGA_PRODUCTO.FindByENTREGA_CODIGO(codigo).Delete();
                            dsEntregaProducto.ENTREGA_PRODUCTO.AcceptChanges();

                            //Avisamos que se elimino 
                            MessageBox.Show("Se han eliminado los datos correctamente", "Información: Elemento Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //Ponemos la ventana en el estado inicial
                            SetInterface(estadoUI.inicio);               
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una entrega de producto de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Entrega Producto - Modificación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       
        
    }
}
