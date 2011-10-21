using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Enumeraciones;

namespace GyCAP.UI.GestionStock
{
    public partial class frmEntregaProducto : Form
    {
        private static frmEntregaProducto _frmEntregaProducto = null;
        private Data.dsEntregaProducto dsEntregaProducto = new GyCAP.Data.dsEntregaProducto();
        private DataView dvListaEntregaBus, dvListadetalleBus, dvListaStock,dvListaPedidos, dvListaDetallePedido, dvListaDetalleEntrega,
                         dvComboCliBus, dvComboEmp, dvComboCli, dvComboUbicaciones;
        private enum estadoUI { inicio, nuevo, buscar, modificar, cargaDetalle };
        private static estadoUI estadoActual;
        private static bool seleccionPestaña = false;
        private static int codigoDetalle = -1, codigoEntrega = -1;

        #region Inicio

        public frmEntregaProducto()
        {
            InitializeComponent();

            //Inicializamos las grillas
            dgvDatosEntrega.AutoGenerateColumns = false;
            dgvDetalleBusqueda.AutoGenerateColumns = false;
            dgvDetallePedido.AutoGenerateColumns = false;
            dgvListaEntregas.AutoGenerateColumns = false;
            dgvPedidos.AutoGenerateColumns = false;
            
            
            //*******************************************************************************
            //                                      LISTAS BUSQUEDA
            //*******************************************************************************

            //Lista de Entregas
            dgvListaEntregas.Columns.Add("ENTREGA_CODIGO", "Código");
            dgvListaEntregas.Columns.Add("ENTREGA_FECHA", "Fecha Entrega");
            dgvListaEntregas.Columns.Add("CLI_CODIGO", "Cliente");
            dgvListaEntregas.Columns.Add("E_CODIGO", "Responsable");
                      
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvListaEntregas.Columns["ENTREGA_CODIGO"].DataPropertyName = "ENTREGA_CODIGO";
            dgvListaEntregas.Columns["ENTREGA_FECHA"].DataPropertyName = "ENTREGA_FECHA";
            dgvListaEntregas.Columns["CLI_CODIGO"].DataPropertyName = "CLI_CODIGO";
            dgvListaEntregas.Columns["E_CODIGO"].DataPropertyName = "E_CODIGO";
            
            //Creamos el dataview y lo asignamos a la grilla
            dvListaEntregaBus = new DataView(dsEntregaProducto.ENTREGA_PRODUCTO);
            dgvListaEntregas.DataSource = dvListaEntregaBus;

            //Lista de Detalles de Entregas
            dgvDetalleBusqueda.Columns.Add("DENT_CODIGO", "Código");
            dgvDetalleBusqueda.Columns.Add("ENTREGA_CODIGO", "Entrega");
            dgvDetalleBusqueda.Columns.Add("DENT_CONTENIDO", "Stock");
            dgvDetalleBusqueda.Columns.Add("DPED_CODIGO", "N° Pedido");
            dgvDetalleBusqueda.Columns.Add("DENT_CANTIDAD", "Cantidad");
                       
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetalleBusqueda.Columns["DENT_CODIGO"].DataPropertyName = "DENT_CODIGO";
            dgvDetalleBusqueda.Columns["ENTREGA_CODIGO"].DataPropertyName = "ENTREGA_CODIGO";
            dgvDetalleBusqueda.Columns["DENT_CONTENIDO"].DataPropertyName = "DENT_CONTENIDO";
            dgvDetalleBusqueda.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";
            dgvDetalleBusqueda.Columns["DENT_CANTIDAD"].DataPropertyName = "DENT_CANTIDAD";

            //Creamos el dataview y lo asignamos a la grilla
            dvListadetalleBus = new DataView(dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO);
            dgvDetalleBusqueda.DataSource = dvListadetalleBus;

            //*******************************************************************************
            //                                      LISTAS DE DATOS
            //*******************************************************************************
                      
            //=================================
            //Lista de Pedidos
            //=================================
            dgvPedidos.Columns.Add("PED_NUMERO", "Número");
            dgvPedidos.Columns.Add("CLI_CODIGO", "Cliente");
            dgvPedidos.Columns.Add("EPED_CODIGO", "Estado");
            dgvPedidos.Columns.Add("PED_FECHA_ALTA", "Fecha Alta");
            dgvPedidos.Columns.Add("PED_FECHAENTREGAREAL", "Fecha Real Entrega");
            
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvPedidos.Columns["PED_NUMERO"].DataPropertyName = "PED_NUMERO";
            dgvPedidos.Columns["CLI_CODIGO"].DataPropertyName = "CLI_CODIGO";
            dgvPedidos.Columns["EPED_CODIGO"].DataPropertyName = "EPED_CODIGO";
            dgvPedidos.Columns["PED_FECHA_ALTA"].DataPropertyName = "PED_FECHA_ALTA";
            dgvPedidos.Columns["PED_FECHAENTREGAREAL"].DataPropertyName = "PED_FECHAENTREGAREAL";
                        
            //Creamos el dataview y lo asignamos a la grilla
            dvListaPedidos = new DataView(dsEntregaProducto.PEDIDOS);
            dgvPedidos.DataSource = dvListaPedidos;

            //=================================
            //Lista de Detalles de Pedido
            //=================================
            dgvDetallePedido.Columns.Add("DPED_CODIGO", "Número");
            dgvDetallePedido.Columns.Add("EDPED_CODIGO", "Estado");
            dgvDetallePedido.Columns.Add("COC_CODIGO", "Cocina");
            dgvDetallePedido.Columns.Add("DPED_CANTIDAD", "Cantidad");
            
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDetallePedido.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";
            dgvDetallePedido.Columns["EDPED_CODIGO"].DataPropertyName = "EDPED_CODIGO";
            dgvDetallePedido.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDetallePedido.Columns["DPED_CANTIDAD"].DataPropertyName = "DPED_CANTIDAD";
            
            //Alineamos las columnas a la derecha
            dgvDetallePedido.Columns["DPED_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDetallePedido = new DataView(dsEntregaProducto.DETALLE_PEDIDOS);
            dgvDetallePedido.DataSource = dvListaDetallePedido;

            //=================================
            // Lista de Detalle de Entregas
            //=================================
            dgvDatosEntrega.Columns.Add("DENT_CODIGO", "Código");
            dgvDatosEntrega.Columns.Add("ENTREGA_CODIGO", "Entrega");
            dgvDatosEntrega.Columns.Add("DENT_CONTENIDO", "Contenido");
            dgvDatosEntrega.Columns.Add("DPED_CODIGO", "Pedido");
            dgvDatosEntrega.Columns.Add("DENT_COCINA", "Cocina");
            dgvDatosEntrega.Columns.Add("DENT_CANTIDAD", "Cantidad");
                   
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDatosEntrega.Columns["DENT_CODIGO"].DataPropertyName = "DENT_CODIGO";
            dgvDatosEntrega.Columns["ENTREGA_CODIGO"].DataPropertyName = "ENTREGA_CODIGO";
            dgvDatosEntrega.Columns["DENT_CONTENIDO"].DataPropertyName = "DENT_CONTENIDO";
            dgvDatosEntrega.Columns["DENT_COCINA"].DataPropertyName = "DENT_COCINA";
            dgvDatosEntrega.Columns["DENT_CANTIDAD"].DataPropertyName = "DENT_CANTIDAD";
            dgvDatosEntrega.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";
            
            //Alineamos las columnas que sean necesarias
            dgvDatosEntrega.Columns["DENT_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatosEntrega.Columns["DPED_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            //Oculramos las columnas que no queremos que se vean
            dgvDatosEntrega.Columns["DENT_CODIGO"].Visible = false;
            dgvDatosEntrega.Columns["ENTREGA_CODIGO"].Visible = false;
            dgvDatosEntrega.Columns["DENT_CONTENIDO"].Visible = false;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDetalleEntrega = new DataView(dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO);
            dgvDatosEntrega.DataSource = dvListaDetalleEntrega;

            //**************************************************************************************
            //                              Cargamos los  DataTable
            //**************************************************************************************

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
            BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsEntregaProducto.UBICACIONES_STOCK, (int)StockEnum.ContenidoUbicacion.Cocina);

            //**************************************************************************************
            //                              Cargamos los Combobox
            //**************************************************************************************

            //=================================
            // Combos de Busqueda
            //=================================
            //Cargamos el combo de los clientes
            dvComboCliBus = new DataView(dsEntregaProducto.CLIENTES);
            cbClienteBus.SetDatos(dvComboCliBus, "cli_codigo", "cli_razonsocial", "Seleccione", false);

            //=================================
            // Combos de Datos
            //=================================
            //Cargo el combo de empleados
            dvComboEmp = new DataView(dsEntregaProducto.EMPLEADOS); 
            string[] cabecera = { "e_apellido" ,"e_nombre"  };
            cbEmpleado.SetDatos(dvComboEmp, "e_codigo",cabecera,",", "Seleccione", false);

            //Cargamos el combo de los clientes
            dvComboCli = new DataView(dsEntregaProducto.CLIENTES);
            cbCliente.SetDatos(dvComboCli, "cli_codigo", "cli_razonsocial", "Seleccione", false);

            //Seteamos la interface
            SetInterface(estadoUI.inicio);
        }

        #endregion

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
                    
                    //Selecciono el tabcontrol
                    tcEntregaProducto.SelectedTab = tpDatos;
                    estadoActual = estadoUI.modificar;
                    break;
            }
        }
        #endregion

        #region Controles

        //************************************************************************************
        //                                      METODO DE PARSEO DE GRILLAS
        //************************************************************************************
        
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

        private void dgvDatosEntrega_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.Value != null)
            {
                string nombre;

                switch (dgvDatosEntrega.Columns[e.ColumnIndex].Name)
                {
                    case "DENT_COCINA":
                        nombre = dsEntregaProducto.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
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
                    case "PED_FECHA_ALTA":
                        nombre = Convert.ToDateTime(e.Value).ToShortDateString();
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

        private void dgvListaEntregas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        private void dgvDetalleBusqueda_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        private void dgvStock_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        private void dgvPedidos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        private void dgvDetallePedido_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        private void dgvDatosEntrega_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        #endregion
        
        #region Pestaña Busqueda
        
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
                        Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Entregas de Productos Terminados", this.Text);                        
                    }
                    else
                    {
                        SetInterface(estadoUI.buscar);
                    }
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjValidacion(validacion, this.Text);
                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
                SetInterface(estadoUI.inicio);
            }
        }       

        private void dgvListaEntregas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Se programa la busqueda del detalle de entrega del producto
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
                    Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Detalles de Entrega de Producto", this.Text);
                }
                else
                {
                    //muestro el groupbox del detalle
                    SetInterface(estadoUI.buscar);
                    gbGrillaDetalleBus.Visible = true;                    
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);   
            }
        }
       
        #endregion

        #region Funciones Formulario

        private string validarBusqueda()
        {
            string msjerror = string.Empty;

            if (chCliente.Checked == false && chFechaEntrega.Checked == false)
            {
                msjerror = msjerror + "-Debe seleccionar un criterio de búsqueda\n";
            }
            if (chCliente.Checked == true && cbClienteBus.GetSelectedIndex() == -1)
            {
                msjerror = msjerror + "-Debe seleccionar un cliente del combo\n";
            }
            return msjerror;
        }

        private string ValidarCargaDetalle()
        {
            string msjerror = string.Empty;

            if (cbCliente.GetSelectedIndex() == -1) msjerror = msjerror + "-Debe seleccionar un cliente para la entrega\n";
            if (cbEmpleado.GetSelectedIndex() == -1) msjerror = msjerror + "-Debe seleccionar un responsable de la entrega\n";

            return msjerror;
        }      

        #endregion
        
        #region Pestaña Datos

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
                Entidades.Mensajes.MensajesABM.MsjValidacion(validacion, this.Text);
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
                }
                else
                {
                    gbDetallePedido.Visible = false;
                    btnEntregar.Enabled = false;
                    Entidades.Mensajes.MensajesABM.MsjValidacion("El Pedido seleccionado no tiene detalle", this.Text);                    
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Validación);
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
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Detalle de Entrega de Producto", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);                
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
                    Entidades.Mensajes.MensajesABM.MsjValidacion("No se pueden modificar las cantidades en una entrega de pedido", this.Text);                    
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Detalle de Entrega de Producto", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);                
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
                    Entidades.Mensajes.MensajesABM.MsjValidacion("No se pueden modificar las cantidades en una entrega de pedido", this.Text);                    
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Detalle de Entrega de Producto", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);                
            }
        }

        //Metodo que agrega al detalle desde el pedido
        private void btnEntregar_Click(object sender, EventArgs e)
        {
            if (dgvDetallePedido.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Definimos las variables a utilizar
                int ubicacionStock = 0;
                int cantidad = Convert.ToInt32(dvListaDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_cantidad"]);
                int pedido = Convert.ToInt32(dvListaDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_codigo"]);
                int codigoCocina = Convert.ToInt32(dvListaDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["coc_codigo"]);

                //Tomamos los datos de la fila seleccionada
                if (Convert.ToInt32(dvListaDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["edped_codigo"]) == Convert.ToInt32(BLL.EstadoDetallePedidoBLL.ObtenerCodigoEstado("En Curso")))
                {
                    
                    string nombreCocina = dsEntregaProducto.COCINAS.FindByCOC_CODIGO(codigoCocina).ToString();
                    
                    //Obtenemos la ubicacion de stock sobre la que vamos a generar el movimiento
                    ubicacionStock = BLL.HojaRutaBLL.ObtenerUbicacionStockHoja(nombreCocina);

                    //Creamos y editamos la fila a agregar en el datatable de detalle entrega
                    Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row = dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.NewDETALLE_ENTREGA_PRODUCTORow();
                    row.BeginEdit();
                    row.DENT_CODIGO = codigoDetalle--;
                    row.ENTREGA_CODIGO = codigoEntrega;
                    row.DENT_CONTENIDO = ubicacionStock;
                    row.DPED_CODIGO = pedido;
                    row.DENT_COCINA = codigoCocina.ToString();
                    row.DENT_CANTIDAD = Convert.ToInt32(cantidad);
                    row.EndEdit();

                    dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.AddDETALLE_ENTREGA_PRODUCTORow(row);
                }
                else if (Convert.ToInt32(dvListaDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["edped_codigo"]) == Convert.ToInt32(BLL.EstadoDetallePedidoBLL.ObtenerCodigoEstado("Entrega Stock")))
                {
                    Entidades.Entidad detalle = BLL.EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.DetallePedido, pedido, null);

                    IList<Entidades.MovimientoStock> movimiento = BLL.MovimientoStockBLL.GetMovimientosByOwner(detalle);

                    ubicacionStock = Convert.ToInt32(movimiento.First().Numero);

                    //Creamos y editamos la fila a agregar en el datatable de detalle entrega
                    Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row = dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.NewDETALLE_ENTREGA_PRODUCTORow();
                    row.BeginEdit();
                    row.DENT_CODIGO = codigoDetalle--;
                    row.ENTREGA_CODIGO = codigoEntrega;
                    row.DENT_CONTENIDO = ubicacionStock;
                    row.DPED_CODIGO = pedido;
                    row.DENT_COCINA = codigoCocina.ToString();
                    row.DENT_CANTIDAD = Convert.ToInt32(cantidad);
                    row.EndEdit();

                    dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.AddDETALLE_ENTREGA_PRODUCTORow(row);
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjValidacion("El detalle de pedido no se encuentra terminado.", this.Text);
                }
           
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Detalle de Pedido", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);                
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            //Primero limpiamos los Datasets
            dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Clear();
            dsEntregaProducto.PEDIDOS.Clear();
            dsEntregaProducto.DETALLE_PEDIDOS.Clear();
            dsEntregaProducto.ENTREGA_PRODUCTO.Clear();

            //Seteamos la interface
            SetInterface(estadoUI.nuevo);
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
                Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Entrega de Producto Terminado", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
               
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
                    //Generar movimiento - Emanuel
                    //BLL.UbicacionStockBLL.ActualizarCantidadesStock(Convert.ToInt32(row.DENT_CONTENIDO), (Convert.ToDecimal(row.DENT_CANTIDAD) * -1),(Convert.ToDecimal(row.DENT_CANTIDAD) * -1));
                }

                //Si se modificaron filas entonces SUMO las cantidades anteriores y luego resto las cantidades actuales
                foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow[])dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Select(null, null, System.Data.DataViewRowState.ModifiedOriginal))
                {
                    //Actualizo la cantidad de stock
                    //Generar movimiento - Emanuel
                    //BLL.UbicacionStockBLL.ActualizarCantidadesStock(Convert.ToInt32(row.DENT_CONTENIDO), Convert.ToDecimal(row.DENT_CANTIDAD), Convert.ToDecimal(row.DENT_CANTIDAD));
                }
                foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow[])dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    //Actualizo la cantidad de stock
                    //Generar movimiento - Emanuel
                    //BLL.UbicacionStockBLL.ActualizarCantidadesStock(Convert.ToInt32(row.DENT_CONTENIDO), Convert.ToDecimal(row.DENT_CANTIDAD), Convert.ToDecimal(row.DENT_CANTIDAD));
                }

                //Sumo las cantidades de las filas que fueron eliminadas
                foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow[])dsEntregaProducto.DETALLE_ENTREGA_PRODUCTO.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Actualizo la cantidad de stock
                    //Generar movimiento - Emanuel
                    //BLL.UbicacionStockBLL.ActualizarCantidadesStock(Convert.ToInt32(row.DENT_CONTENIDO), Convert.ToDecimal(row.DENT_CANTIDAD), Convert.ToDecimal(row.DENT_CANTIDAD));
                }
                //******************************************************************** FIN ACTUALIZACIONES ****************************************************************************************************************************************

                //Si esta todo bien aceptamos los cambios que se le hacen al dataset
                dsEntregaProducto.AcceptChanges();

                //Vuelvo al estado inicial de la interface
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
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

                //Seteamos la interface a estado modificar
                SetInterface(estadoUI.modificar);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);                
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListaEntregas.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
                {
                    //Preguntamos si está seguro
                    DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjConfirmaEliminarDatos("Entrega de Producto Terminado", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Femenino, this.Text);
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
                        Entidades.Mensajes.MensajesABM.MsjConfirmaEliminar(this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);

                        //Ponemos la ventana en el estado inicial
                        SetInterface(estadoUI.inicio);
                    }
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Entrega de Producto Terminado", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Femenino, this.Text);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
        }
     
        #endregion                  

        
    }
}
