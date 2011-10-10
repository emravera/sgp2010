using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.BLL;

namespace GyCAP.UI.GestionPedido
{
    public partial class frmPedidos : Form
    {
        private Sistema.ControlesUsuarios.AnimadorFormulario animador = new GyCAP.UI.Sistema.ControlesUsuarios.AnimadorFormulario();
        private static frmPedidos _frmPedido = null;
        private Data.dsCliente dsCliente = new GyCAP.Data.dsCliente();
        private DataView dvPedido, dvDetallePedido, dvCocinas, dvEstadoPedido;
        private DataView dvEstadoDetallePedido, dvClientes, dvEstadoPedidoBuscar, dvCbUbicacionStock;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, cargarDetalle, modificarDetalle };
        private estadoUI estadoInterface;
        private int codigoDetalleModificado = 0;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        //Variable que simula el código autodecremental para el detalle, usa valores negativos para no tener problemas con valores existentes
        int codigoDetalle = -1;

        #region Inicio

        public frmPedidos()
        {
            InitializeComponent();

            //Setea todas las grillas y las vistas
            setGrillasVistasCombo();

            //Setea todos los controles necesarios para el efecto de slide
            SetSlide();

            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
        }

        private void SetSlide()
        {
            gbDatos.Parent = slideDatos;
            gbCocinas.Parent = slideAgregar;
            slideControl.AddSlide(slideAgregar);
            slideControl.AddSlide(slideDatos);
            slideControl.Selected = slideDatos;
        }

        private void setGrillasVistasCombo()
        {
            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            dgvDetallePedido.AutoGenerateColumns = false;
            dgvCocinas.AutoGenerateColumns = false;

            //*******************************************************************************************
            //                                  GRILLA DE BUSQUEDA
            //*******************************************************************************************

            //Agregamos las columnas y sus propiedades
            dgvLista.Columns.Add("PED_CODIGO", "Código");
            dgvLista.Columns.Add("PED_NUMERO", "Número");
            dgvLista.Columns.Add("CLI_CODIGO", "Cliente");
            dgvLista.Columns.Add("EPED_CODIGO", "Estado");
            dgvLista.Columns.Add("PED_FECHA_ALTA", "Fecha Alta");
            dgvLista.Columns.Add("PED_OBSERVACIONES", "Observaciones");

            dgvLista.Columns["PED_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["PED_NUMERO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            //Alineacion de los numeros y las fechas en la grilla
            dgvLista.Columns["PED_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["PED_CODIGO"].Visible = false;

            //Indicamos de dónde van a sacar los datos cada columna
            dgvLista.Columns["PED_CODIGO"].DataPropertyName = "PED_CODIGO";
            dgvLista.Columns["PED_NUMERO"].DataPropertyName = "PED_NUMERO";
            dgvLista.Columns["CLI_CODIGO"].DataPropertyName = "CLI_CODIGO";
            dgvLista.Columns["EPED_CODIGO"].DataPropertyName = "EPED_CODIGO";
            dgvLista.Columns["PED_FECHA_ALTA"].DataPropertyName = "PED_FECHA_ALTA";
            dgvLista.Columns["PED_OBSERVACIONES"].DataPropertyName = "PED_OBSERVACIONES";

            //*******************************************************************************************
            //                              GRILLA DE DETALLE DE PEDIDO
            //*******************************************************************************************

            dgvDetallePedido.Columns.Add("DPED_CODIGO", "Codigo");
            dgvDetallePedido.Columns.Add("COC_CODIGO", "Cocina");
            dgvDetallePedido.Columns.Add("DPED_CANTIDAD", "Cantidad");
            dgvDetallePedido.Columns.Add("EDPED_CODIGO", "Estado");
            dgvDetallePedido.Columns.Add("DPED_FECHA_ENTREGA_PREVISTA", "Fecha Entrega");
            dgvDetallePedido.Columns.Add("DPED_CODIGONEMONICO", "Cod. Nemónico");
            dgvDetallePedido.Columns.Add("DPED_FECHA_CANCELACION", "Fecha Cancelación");
                        
            dgvDetallePedido.Columns["DPED_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;      

            //Indicamos de que columna se obtienen los datos
            dgvDetallePedido.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";
            dgvDetallePedido.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDetallePedido.Columns["DPED_CANTIDAD"].DataPropertyName = "DPED_CANTIDAD";
            dgvDetallePedido.Columns["EDPED_CODIGO"].DataPropertyName = "EDPED_CODIGO";
            dgvDetallePedido.Columns["DPED_FECHA_ENTREGA_PREVISTA"].DataPropertyName = "DPED_FECHA_ENTREGA_PREVISTA";
            dgvDetallePedido.Columns["DPED_CODIGONEMONICO"].DataPropertyName = "DPED_CODIGONEMONICO";
            dgvDetallePedido.Columns["DPED_FECHA_CANCELACION"].DataPropertyName = "DPED_FECHA_CANCELACION";

            //*******************************************************************************************
            //                              GRILLA DE COCINAS
            //*******************************************************************************************

            dgvCocinas.Columns.Add("COC_CODIGO_PRODUCTO", "Código");
            dgvCocinas.Columns.Add("MOD_CODIGO", "Modelo");
            dgvCocinas.Columns.Add("MCA_CODIGO", "Marca");
            
            dgvCocinas.Columns["COC_CODIGO_PRODUCTO"].DataPropertyName = "COC_CODIGO_PRODUCTO";
            dgvCocinas.Columns["MOD_CODIGO"].DataPropertyName = "MOD_CODIGO";
            dgvCocinas.Columns["MCA_CODIGO"].DataPropertyName = "MCA_CODIGO";

            //Creamos el dataview y lo asignamos a la grilla
            dvPedido = new DataView(dsCliente.PEDIDOS);
            dvPedido.Sort = "PED_CODIGO ASC";
            dgvLista.DataSource = dvPedido;
            
            //Escondemos las columnas del detalle
            dgvDetallePedido.Columns["DPED_CODIGO"].Visible = false;
            dgvDetallePedido.Columns["DPED_CODIGONEMONICO"].Visible = false;
            
            dvDetallePedido = new DataView(dsCliente.DETALLE_PEDIDOS);
            dgvDetallePedido.DataSource = dvDetallePedido;

            dvCocinas = new DataView(dsCliente.COCINAS);
            dvCocinas.Sort = "COC_CODIGO_PRODUCTO ASC";
            dgvCocinas.DataSource = dvCocinas;

            dvClientes = new DataView(dsCliente.CLIENTES);
            dvClientes.Sort = "CLI_RAZONSOCIAL";

            dvEstadoPedido = new DataView(dsCliente.ESTADO_PEDIDOS);
            dvEstadoPedido.Sort = "EPED_NOMBRE";

            dvEstadoDetallePedido = new DataView(dsCliente.ESTADO_DETALLE_PEDIDOS);
            dvEstadoDetallePedido.Sort = "EDPED_NOMBRE";
            
            //Obtenemos las terminaciones, los planos, los estados de las piezas, las MP, unidades medidas, hojas ruta
            try
            {
                BLL.EstadoPedidoBLL.ObtenerTodos(dsCliente.ESTADO_PEDIDOS);
                BLL.EstadoDetallePedidoBLL.ObtenerTodos(dsCliente.ESTADO_DETALLE_PEDIDOS);
                BLL.CocinaBLL.ObtenerCocinas(dsCliente.COCINAS);
                BLL.ClienteBLL.ObtenerTodos(dsCliente.CLIENTES);
                BLL.MarcaBLL.ObtenerTodos(dsCliente.MARCAS);
                BLL.ModeloCocinaBLL.ObtenerTodos(dsCliente.MODELOS_COCINAS);

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Inicio);
            }
            dvEstadoPedidoBuscar = new DataView(dsCliente.ESTADO_PEDIDOS);

            //Lleno el Datatable de las Ubicaciones de Stock 
            int codigoUbicacionMP = BLL.ContenidoUbicacionStockBLL.ObtenerCodigoContenido("Cocina");
            BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsCliente.UBICACIONES_STOCK, codigoUbicacionMP);

            cboEstadoBuscar.SetDatos(dvEstadoPedidoBuscar, "EPED_CODIGO", "EPED_NOMBRE", "--TODOS--", true);
            cboEstado.SetDatos(dvEstadoPedidoBuscar, "EPED_CODIGO", "EPED_NOMBRE", "", false);
            cboClientes.SetDatos(dvClientes, "CLI_CODIGO", "CLI_RAZONSOCIAL", "--Seleccionar--", false);

            //Creamos el Dataview y se lo asignamos al combo de ubicaciones de stock
            dvCbUbicacionStock = new DataView(dsCliente.UBICACIONES_STOCK);
            cbUbicacionStock.DataSource = dvCbUbicacionStock;
            cbUbicacionStock.SetDatos(dvCbUbicacionStock, "ustck_numero", "ustck_nombre", "-Seleccionar-", false);
        }

        #endregion

        #region Servicios

        //Método para evitar la creación de más de una pantalla
        public static frmPedidos Instancia
        {
            get
            {
                if (_frmPedido == null || _frmPedido.IsDisposed)
                {
                    _frmPedido = new frmPedidos();
                }
                else
                {
                    _frmPedido.BringToFront();
                }
                return _frmPedido;
            }
            set
            {
                _frmPedido = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
        }

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos = true;

                    if (dsCliente.PEDIDOS.Rows.Count == 0)
                    {
                        hayDatos = false;                        
                    }
                    else
                    {
                        hayDatos = true;                        
                    }

                    //Linea para validacion
                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }

                    limpiarControles(false);
                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnConsultar.Enabled = hayDatos;
                    btnNuevo.Enabled = true;

                    //Escondo las columnas que no quiero que se vean
                    dgvLista.Columns["PED_CODIGO"].Visible = false;
                    dgvDetallePedido.Columns["DPED_CODIGO"].Visible = false;
                    dgvDetallePedido.Columns["DPED_CODIGONEMONICO"].Visible = false;

                    slideControl.Selected = slideDatos;
                    estadoInterface = estadoUI.inicio;
                    tcPedido.SelectedTab = tpBuscar;                    
                    break;

                case estadoUI.nuevo:
                    setControles(false);
                    limpiarControles(true);
                    txtNumero.Text = "No asignado...";
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNewCliente.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnCancelarPedido.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    dvDetallePedido.RowFilter = "DPED_CODIGO < 0";
                    tcPedido.SelectedTab = tpDatos;
                    dgvDetallePedido.Columns["DPED_CODIGO"].Visible = false;
                    dgvDetallePedido.Columns["DPED_CODIGONEMONICO"].Visible = false;                        
                    break;

                case estadoUI.nuevoExterno:
                    setControles(false);
                    limpiarControles(true);
                    txtNumero.Text = "No asignado...";
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnNewCliente.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnCancelarPedido.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevoExterno;
                    dvDetallePedido.RowFilter = "DPED_CODIGO < 0";
                    tcPedido.SelectedTab = tpDatos;
                    break;

                case estadoUI.consultar:
                    setControles(true);
                    btnGuardar.Enabled = false;
                    btnNewCliente.Enabled = false;
                    btnVolver.Enabled = true;
                    panelAcciones.Enabled = false;
                    slideControl.Selected = slideDatos;
                    estadoInterface = estadoUI.consultar;
                    tcPedido.SelectedTab = tpDatos;
                    break;

                case estadoUI.modificar:
                    setControles(false);
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnNewCliente.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnCancelarPedido.Enabled = true;
                    panelAcciones.Enabled = true;
                    dgvDetallePedido.Columns["DPED_CODIGO"].Visible = false;
                    estadoInterface = estadoUI.modificar;
                    tcPedido.SelectedTab = tpDatos;                   
                    break;

                case estadoUI.cargarDetalle:
                    btnAgregar.Enabled = false;
                    btnValidar.Enabled = true;
                    btnCancelarPedido.Enabled = false;
                    lblCantProducir.Visible = false;
                    lblCantStock.Visible = false;
                    numCantProducir.Visible = false;
                    numCantStock.Visible = false;
                    nudCantidad.Enabled = true;
                    lblTotalCocina.Enabled = true;

                    //Escondemos los numeros y reseteamos los controles
                    numCantProducir.Value=0;
                    numCantStock.Value=0;
                    estadoInterface = estadoUI.cargarDetalle;
                    tcPedido.SelectedTab = tpDatos;
                    break;

                case estadoUI.modificarDetalle:
                    btnAgregar.Enabled = true;
                    btnValidar.Enabled = false;
                    btnCancelarPedido.Enabled = true;
                    lblCantProducir.Visible = true;
                    lblCantStock.Visible = true;
                    numCantProducir.Visible = true;
                    numCantStock.Visible = true;
                    nudCantidad.Enabled = false;
                    lblTotalCocina.Enabled = false;

                    //Escondemos los numeros y reseteamos los controles
                    estadoInterface = estadoUI.modificarDetalle;
                    tcPedido.SelectedTab = tpDatos;
                    break;

                default:
                    break;
            }
        }

        private void setControles(bool pValue)
        {
            txtNumero.Enabled = false;
            txtObservacion.ReadOnly = pValue;
            cboClientes.Enabled = !pValue;
            cboEstado.Enabled = !pValue;            
        }

        private void limpiarControles(bool pValue)
        {
            //Limpia los valores cuando se quiere cargar un nuevo pedido
            txtNumero.Text = string.Empty ;
            txtObservacion.Text = string.Empty;
            cboClientes.SelectedIndex = -1 ;
            cboEstado.SelectedIndex = -1;
                        
            if (pValue == true)
            {               
                dgvDetallePedido.Refresh();
                //Pongo al combo el valor del pedido Pendiente
                int estadoPedido = BLL.EstadoPedidoBLL.ObtenerIDDeatalle("Pendiente");
                cboEstado.SetSelectedValue(estadoPedido); 
                cboEstado.Enabled = false;

                cboClientes.SetTexto("Seleccione un Cliente...");
            }
        }

        //***************************************************************************
        //                          METODO DE LOS CONTROLES
        //***************************************************************************
        
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            long codigo = Convert.ToInt64(dvPedido[e.RowIndex]["ped_codigo"]);
            txtNumero.Text = dsCliente.PEDIDOS.FindByPED_CODIGO(codigo).PED_NUMERO;
            cboClientes.SetSelectedValue(Convert.ToInt32(dsCliente.PEDIDOS.FindByPED_CODIGO(codigo).CLI_CODIGO));
            cboEstado.SetSelectedValue(Convert.ToInt32(dsCliente.PEDIDOS.FindByPED_CODIGO(codigo).EPED_CODIGO));
            txtObservacion.Text = dsCliente.PEDIDOS.FindByPED_CODIGO(codigo).PED_OBSERVACIONES;

            //Usemos el filtro del dataview para mostrar sólo las Detalles del Pedido seleccionado
            dvDetallePedido.RowFilter = "PED_CODIGO = " + codigo;
        }

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;

                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "CLI_CODIGO":
                        nombre = dsCliente.CLIENTES.FindByCLI_CODIGO(Convert.ToInt64(e.Value.ToString())).CLI_RAZONSOCIAL;
                        e.Value = nombre;
                        break;
                    case "EPED_CODIGO":
                        nombre = dsCliente.ESTADO_PEDIDOS.FindByEPED_CODIGO(Convert.ToInt64(e.Value.ToString())).EPED_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "PED_FECHA_ALTA":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvDetallePedido_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                switch (dgvDetallePedido.Columns[e.ColumnIndex].Name)
                {
                    case "COC_CODIGO":
                        nombre = dsCliente.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value.ToString())).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                    case "EDPED_CODIGO":
                        nombre = dsCliente.ESTADO_DETALLE_PEDIDOS.FindByEDPED_CODIGO(Convert.ToInt32(e.Value.ToString())).EDPED_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvCocinas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                switch (dgvCocinas.Columns[e.ColumnIndex].Name)
                {
                    case "MOD_CODIGO":
                        nombre = dsCliente.MODELOS_COCINAS.FindByMOD_CODIGO(Convert.ToInt32(e.Value.ToString())).MOD_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "MCA_CODIGO":
                        nombre = dsCliente.MARCAS.FindByMCA_CODIGO(Convert.ToInt32(e.Value.ToString())).MCA_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }
        private void dgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        private void dgvDetallePedido_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        private void dgvCocinas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        #endregion

        #region Pesaña Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsCliente.PEDIDOS.Clear();
                dsCliente.DETALLE_PEDIDOS.Clear();

                //Busquemos, no importa si ingresó algo o no, ya se encargarán las otras clases de verificarlo
                BLL.PedidoBLL.ObtenerPedido(txtNombreBuscar.Text, txtNroPedidoBuscar.Text, cboEstadoBuscar.GetSelectedValueInt(), sfFechaDesde.GetFecha(), sfFechaHasta.GetFecha(), dsCliente, true);

                if (dsCliente.PEDIDOS.Rows.Count == 0)
                {
                    Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Pedidos", this.Text);
                }

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvPedido.Table = dsCliente.PEDIDOS;
                dvDetallePedido.Table = dsCliente.DETALLE_PEDIDOS;
                dvCocinas.Table = dsCliente.COCINAS;

                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
                SetInterface(estadoUI.inicio);        
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.consultar);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.modificar);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose(true);
        }
        #endregion

        #region Pestaña Datos

        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (estadoInterface == estadoUI.cargarDetalle || estadoInterface == estadoUI.modificarDetalle)
            {
                slideControl.BackwardTo("slideDatos");
                nudCantidad.Value = 0;
                panelAcciones.Enabled = true;
            }
            else
            {
                //Descartamos los cambios realizamos hasta el momento sin guardar
                dsCliente.PEDIDOS.RejectChanges();
                dsCliente.DETALLE_PEDIDOS.RejectChanges();
                SetInterface(estadoUI.inicio);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            slideControl.ForwardTo("slideAgregar");
            panelAcciones.Enabled = false;
            SetInterface(estadoUI.cargarDetalle);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDetallePedido.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                long codigoDetalle = Convert.ToInt64(dvDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_codigo"]);
                //Lo borramos pero sólo del dataset                
                dsCliente.DETALLE_PEDIDOS.FindByDPED_CODIGO(codigoDetalle).Delete();
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion("Debe seleccionar una Cocina de la lista.", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación );
            }
        }
        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Validamos el formulario y sus controles           
                if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
                {
                    //Creamos el objeto de Pedido
                    Entidades.Pedido pedido = new Entidades.Pedido();
                    //Creamos el objeto Cliente
                    Entidades.Cliente cliente = new GyCAP.Entidades.Cliente();
                    cliente.Codigo = Convert.ToInt32(cboClientes.GetSelectedValue());
                    pedido.Cliente = cliente;
                    //Creamos el objeto estado de pedido
                    Entidades.EstadoPedido estadoPedido = new GyCAP.Entidades.EstadoPedido();
                    estadoPedido.Codigo = Convert.ToInt32(cboEstado.GetSelectedValue());
                    pedido.EstadoPedido = estadoPedido;
                    pedido.FechaAlta = DBBLL.GetFechaServidor();
                    pedido.Observaciones = txtObservacion.Text.Trim();
                    pedido.Numero = string.Empty;
                    
                    //Revisamos que está haciendo
                    if (estadoInterface == estadoUI.cargarDetalle)
                    {
                        //Esta guardando un Pedido Nuevo
                        
                        //Guardamos el objeto y su detalle completo
                        int codigoPedido = BLL.PedidoBLL.Insertar(pedido, dsCliente.DETALLE_PEDIDOS);

                        //Primero lo agregamos a la tabla Pedidos                 
                        Data.dsCliente.PEDIDOSRow rowPedido = dsCliente.PEDIDOS.NewPEDIDOSRow();
                        rowPedido.BeginEdit();
                        rowPedido.PED_CODIGO = codigoPedido;
                        rowPedido.PED_NUMERO = codigoPedido.ToString();
                        rowPedido.PED_OBSERVACIONES = pedido.Observaciones;
                        rowPedido.EPED_CODIGO = pedido.EstadoPedido.Codigo;
                        rowPedido.CLI_CODIGO = pedido.Cliente.Codigo;
                        rowPedido.PED_FECHA_ALTA = pedido.FechaAlta;
                        rowPedido.EndEdit();

                        dsCliente.PEDIDOS.AddPEDIDOSRow(rowPedido);                    
                    }
                    else if (estadoInterface == estadoUI.modificar || estadoInterface == estadoUI.modificarDetalle)
                    {
                        //Está modificando el pedido
                        //Primero obtenemos su código del dataview que está relacionado a la fila seleccionada
                        pedido.Codigo = Convert.ToInt32(dvPedido[dgvLista.SelectedRows[0].Index]["ped_codigo"]);

                        //Segundo obtenemos el resto de los datos que puede cambiar el usuario, el detalle se fué
                        //actualizando en el dataset a medida que el usuario ejecutaba una acción
                        dsCliente.PEDIDOS.FindByPED_CODIGO(pedido.Codigo).PED_NUMERO = txtNumero.Text;
                        dsCliente.PEDIDOS.FindByPED_CODIGO(pedido.Codigo).CLI_CODIGO = long.Parse(cboClientes.GetSelectedValueInt().ToString());
                        dsCliente.PEDIDOS.FindByPED_CODIGO(pedido.Codigo).EPED_CODIGO = cboEstado.GetSelectedValueInt();
                        dsCliente.PEDIDOS.FindByPED_CODIGO(pedido.Codigo).PED_OBSERVACIONES = txtObservacion.Text;
                                              
                        //Lo actualizamos en la DB 
                        BLL.PedidoBLL.Actualizar(pedido, dsCliente.DETALLE_PEDIDOS);                                              
                    }

                    //El dataset ya se actualizó en las capas DAL y BLL, aceptamos los cambios
                    dsCliente.PEDIDOS.AcceptChanges();
                    dsCliente.DETALLE_PEDIDOS.AcceptChanges();

                    //Avisamos que estuvo todo ok
                    Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Pedido", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);

                    //Y por último seteamos el estado de la interfaz
                    SetInterface(estadoUI.inicio);

                    dgvLista.Refresh(); 
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
            catch (Entidades.Excepciones.ElementoExistenteException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
            catch (Entidades.Excepciones.ErrorInesperadoException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }                    
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int estado = Convert.ToInt32(dvPedido[dgvLista.SelectedRows[0].Index]["eped_codigo"]);
                
                if (estado == BLL.EstadoDetallePedidoBLL.ObtenerCodigoEstado("Pendiente")) 
                {
                    //Preguntamos si está seguro
                    DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjPreguntaAlUsuario("¿Está seguro que desea eliminar el Pedido seleccionado?", this.Text);
                    if (respuesta == DialogResult.Yes)
                    {
                        try
                        {
                            //Obtenemos el codigo
                            int codigo = Convert.ToInt32(dvPedido[dgvLista.SelectedRows[0].Index]["ped_codigo"]);
                            
                            //Lo eliminamos de la DB
                            BLL.PedidoBLL.Eliminar(codigo, dsCliente.DETALLE_PEDIDOS);
                            
                            //Lo eliminamos de la tabla conjuntos del dataset
                            dsCliente.PEDIDOS.FindByPED_CODIGO(codigo).Delete();
                            dsCliente.PEDIDOS.AcceptChanges();

                            //Limpiamos el datatable de los detalles del pedido
                            dsCliente.DETALLE_PEDIDOS.Clear();

                            //Avisamos que se elimino correctamente el pedido
                            Entidades.Mensajes.MensajesABM.MsjConfirmaEliminar(this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
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
                    // No se puede borrar pedidos que no esten pendientes
                    string lEstado = dsCliente.ESTADO_DETALLE_PEDIDOS.FindByEDPED_CODIGO(Convert.ToDecimal( dvPedido[dgvLista.SelectedRows[0].Index]["eped_codigo"])).EDPED_NOMBRE;
                    Entidades.Mensajes.MensajesABM.MsjExcepcion("No puede eliminar un pedido con estado: " + lEstado, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjValidacion("Debe seleccionar un Pedido de la lista.", this.Text);
            }
        }

        private void btnNewCliente_Click(object sender, EventArgs e)
        {
            frmCliente.Instancia.TopLevel = false;
            frmCliente.Instancia.Parent = this.Parent;
            Point ubicacion = this.Location;
            ubicacion.X += 50;
            ubicacion.Y += 50;
            frmCliente.Instancia.Location = ubicacion;
            frmCliente.Instancia.SetEstadoInicial(frmCliente.estadoInicialNuevo);
            frmCliente.Instancia.NuevoCliente += new Action<Entidades.Cliente>(frm_NuevoCliente);
            frmCliente.Instancia.Show();
            frmCliente.Instancia.Focus();
        }

        private void frm_NuevoCliente(Entidades.Cliente cliente)
        {
            //en obj te viene el nuevo cliente
            //aca seteas el elemento seleccionado o lo que quieras hacer
            dsCliente.CLIENTES.Clear();
            BLL.ClienteBLL.ObtenerTodos(dsCliente.CLIENTES);
            cboClientes.SetDatos(dvClientes, "CLI_CODIGO", "CLI_RAZONSOCIAL", "--Seleccionar--", false);
            cboClientes.SetSelectedValue(Convert.ToInt32(cliente.Codigo.ToString()));
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
        #endregion

        #region Pestaña Detalle

        //Valida cuando se intenta agregar un objeto
        private string ValidarAgregar()
        {
            string validacion = string.Empty;

            if (estadoInterface == estadoUI.cargarDetalle)
            {
                if (dgvCocinas.Rows.GetRowCount(DataGridViewElementStates.Selected) == 0) validacion += "-Debe seleccionar una Cocina de la lista.\n";
                if (nudCantidad.Value == 0) validacion += "-La Cantidad Total debe ser mayor a cero.\n";

            }
            else if (estadoInterface == estadoUI.modificarDetalle)
            {
                if (dgvCocinas.Rows.GetRowCount(DataGridViewElementStates.Selected) == 0) validacion += "-Debe seleccionar una Cocina de la lista.\n";
            }           

            return validacion;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string validacion = ValidarAgregar();

            if (validacion == string.Empty )
            {                   
                //Obtenemos el código del pedido
                int pedidoCodigo;
                if (estadoInterface == estadoUI.cargarDetalle ) { pedidoCodigo = -1; }
                else { pedidoCodigo = Convert.ToInt32(dvPedido[dgvLista.SelectedRows[0].Index]["ped_codigo"]); }
                
                //Obtenemos el código de la cocina
                int cocinaCodigo = Convert.ToInt32(dvCocinas[dgvCocinas.SelectedRows[0].Index]["coc_codigo"]);

                //Obtenemos la cantidad a cargar de stock y la de produccion
                int cantidadStock = Convert.ToInt32(numCantStock.Value);
                int cantidadProduccion = Convert.ToInt32(numCantProducir.Value);

                //Si esta en modo de cargar detalle
                if (estadoInterface == estadoUI.cargarDetalle)
                {
                    //Agregamos la fila de movimiento de stock
                    if (cantidadStock > 0)
                    {
                        //Obtenemos el codigo del estado
                        int codigoEstado = BLL.EstadoDetallePedidoBLL.ObtenerCodigoEstado("Entrega Stock");

                        //Se tiene que agregar una fila nueva al detalle de pedidos
                        Data.dsCliente.DETALLE_PEDIDOSRow row = dsCliente.DETALLE_PEDIDOS.NewDETALLE_PEDIDOSRow();
                        row.BeginEdit();

                        //Agregamos una fila nueva con nuestro código autodecremental, luego al guardar en la db se actualizará
                        //-- para que se vaya autodecrementando en cada inserción
                        row.DPED_CODIGO = codigoDetalle--;
                        row.PED_CODIGO = pedidoCodigo;
                        row.COC_CODIGO = cocinaCodigo;
                        row.EDPED_CODIGO = codigoEstado;
                        row.DPED_CANTIDAD = Convert.ToInt32(numCantStock.Value);
                        row.DPED_CODIGONEMONICO = string.Empty;
                        row.DPED_FECHA_ENTREGA_PREVISTA = Convert.ToDateTime(sfFechaPrevista.Value.ToShortDateString());
                        row.UBICACION_STOCK = cbUbicacionStock.GetSelectedValue().ToString();
                        row.EndEdit();

                        //Agregamos la fila nueva al dataset sin aceptar cambios para que quede marcada como nueva ya que
                        //todavia no vamos a insertar en la db hasta que no haga Guardar
                        dsCliente.DETALLE_PEDIDOS.AddDETALLE_PEDIDOSRow(row);
                    }

                    //Agregamos la fila de produccion
                    if (cantidadProduccion > 0)
                    {
                        //Obtenemos el codigo del estado
                        int codigoEstado = BLL.EstadoDetallePedidoBLL.ObtenerCodigoEstado("Pendiente");

                        //Nos fijamos si ya esta agregado
                        string filtro = "ped_codigo = " + pedidoCodigo + " AND coc_codigo = " + cocinaCodigo + "AND edped_codigo = " + codigoEstado;
                        Data.dsCliente.DETALLE_PEDIDOSRow[] rows = (Data.dsCliente.DETALLE_PEDIDOSRow[])dsCliente.DETALLE_PEDIDOS.Select(filtro);

                        if (rows.Length > 0)
                        {
                            //Ya lo ha agregado, preguntemos si quiere aumentar la cantidad existente o descartar
                            DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjPreguntaAlUsuario("El Pedido ya posee la cocina seleccionada. ¿Desea sumar la cantidad ingresada a lo planificado?", this.Text); ;
                            if (respuesta == DialogResult.Yes)
                            {
                                //Sumemos la cantidad ingresada a la existente, como hay una sola fila seleccionamos la 0 del array
                                rows[0].DPED_CANTIDAD += int.Parse(numCantStock.Value.ToString());
                            }
                        }
                        else
                        {
                            //Se tiene que agregar una fila nueva al detalle de pedidos
                            Data.dsCliente.DETALLE_PEDIDOSRow row = dsCliente.DETALLE_PEDIDOS.NewDETALLE_PEDIDOSRow();
                            row.BeginEdit();

                            //Agregamos una fila nueva con nuestro código autodecremental, luego al guardar en la db se actualizará
                            //Se va autodecrementando en cada inserción
                            row.DPED_CODIGO = codigoDetalle--;
                            row.PED_CODIGO = pedidoCodigo;
                            row.COC_CODIGO = cocinaCodigo;
                            row.EDPED_CODIGO = codigoEstado;
                            row.DPED_CANTIDAD = Convert.ToInt32(numCantProducir.Value);
                            row.DPED_CODIGONEMONICO = string.Empty;
                            row.DPED_FECHA_ENTREGA_PREVISTA = Convert.ToDateTime(sfFechaPrevista.Value.ToShortDateString());
                            row.EndEdit();

                            //Agregamos la fila nueva al dataset sin aceptar cambios para que quede marcada como nueva ya que
                            //todavia no vamos a insertar en la db hasta que no haga Guardar
                            dsCliente.DETALLE_PEDIDOS.AddDETALLE_PEDIDOSRow(row);
                        }
                    }
                }
                //Si esta modificando un detalle de pedido
                else if (estadoInterface == estadoUI.modificarDetalle)
                {
                    //Hay que modificar el datatable con los datos nuevos
                    Data.dsCliente.DETALLE_PEDIDOSRow row = dsCliente.DETALLE_PEDIDOS.FindByDPED_CODIGO(Convert.ToDecimal(codigoDetalleModificado));
                    
                    row.BeginEdit();
                    //Modificamos solo aquellos valores que se pueden cambiar
                    row.COC_CODIGO = cocinaCodigo;
                    if (numCantProducir.Value > 0) row.DPED_CANTIDAD = Convert.ToInt32(numCantProducir.Value);
                    else row.DPED_CANTIDAD = Convert.ToInt32(numCantStock.Value);
                    row.DPED_FECHA_ENTREGA_PREVISTA = Convert.ToDateTime(sfFechaPrevista.Value.ToShortDateString());
                    row.EndEdit();                    
                }

                //Se vuelve a la pantalla principal del pedido
                btnVolver.PerformClick();

                //Volvemos la interfaz para que cargue mas detalles
                nudCantidad.Value = 0;
                sfFechaPrevista.Value = DateTime.Now;
                cbUbicacionStock.SetSelectedIndex(-1);
                btnValidar.Enabled = true;
                btnAgregar.Enabled = false;                
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjValidacion(validacion, this.Text);
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            //Defino un que determine la validacion
            string validacion = string.Empty;

            //Hay que validar que se ingrese una fecha y una cantidad total de cocinas
            if (nudCantidad.Value > 0 && sfFechaPrevista.Value != null && cbUbicacionStock.GetSelectedIndex() != -1)
            {
                //Obtenemos la ubicacion de stock, la fecha de necesidad y la cantidad
                int ubicacionStock = Convert.ToInt32(cbUbicacionStock.GetSelectedValue());
                DateTime fechaNecesidad = Convert.ToDateTime(sfFechaPrevista.Value);
                int cantidad = Convert.ToInt32(nudCantidad.Value);

                //Verificamos si hay stock para una cocina determinada para esa fecha
                decimal cantidadStock = Math.Round(BLL.FabricaBLL.GetStockForDay(ubicacionStock, fechaNecesidad), 0);

                if (cantidadStock > 0)
                {
                    //La diferencia hay que colocarla para planificar
                    if (cantidadStock < cantidad)
                    {
                        //Si hay stock lo cargamos en el numeric de cantidad Stock
                        numCantStock.Value = cantidadStock;
                        validacion = validacion + "STOCK: Hay " + cantidadStock + " unidades disponibles para la fecha " + fechaNecesidad.ToShortDateString() + "\n";

                        numCantProducir.Value = (nudCantidad.Value - numCantStock.Value);
                        validacion = validacion + "PLANIFICACION PRODUCCION: " + (nudCantidad.Value - numCantStock.Value).ToString() + " Unidades \n";
                    }
                    else
                    {
                        numCantStock.Value = cantidad;
                        validacion = validacion + "STOCK: Hay " + cantidadStock + " unidades disponibles para la fecha " + fechaNecesidad.ToShortDateString() + "\n";
                    }
                }
                else
                {
                    //No hay stock disponible por eso se coloca cero
                    numCantStock.Value = 0;

                    //Escribimos el mensaje que avisa que no hay stock
                    validacion = validacion + "STOCK: No hay stock disponible para la fecha " + fechaNecesidad.ToShortDateString() + "\n";

                    //La diferencia hay que colocarla para planificar
                    numCantProducir.Value = nudCantidad.Value;
                    validacion = validacion + "PLANIFICACION PRODUCCION: " + nudCantidad.Value.ToString() + " Unidades \n";
                }

                //Mostramos el mensaje resultado de la validación
                Entidades.Mensajes.MensajesABM.MsjValidacion(validacion, this.Text);

                //Habilito los controles que se tienen que ver
                lblCantProducir.Visible = true;
                numCantProducir.Visible = true;
                lblCantStock.Visible = true;
                numCantStock.Visible = true;
                btnAgregar.Enabled = true;
                btnValidar.Enabled = false;
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjValidacion("Debe completar los datos obligatorios", this.Text);
            }
        }

        private void btnModificarDetalle_Click(object sender, EventArgs e)
        {
            if (dgvDetallePedido.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                slideControl.ForwardTo("slideAgregar");
                panelAcciones.Enabled = false;
                SetInterface(estadoUI.modificarDetalle);

                //Cargamos los valores en los controles
                int estado = Convert.ToInt32(dvDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["edped_codigo"]);
                codigoDetalleModificado = Convert.ToInt32(dvDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_codigo"]);

                //Seleccionamos la cocina de la lista
                int codigoCocina = Convert.ToInt32(dvDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["coc_codigo"]);
                dvCocinas.Sort = "coc_codigo";
                int fila = dvCocinas.Find(codigoCocina);
                dgvCocinas.Rows[fila].Selected = true;

                //Modificamos el comportamiento segun sea el estado
                if (estado == BLL.EstadoDetallePedidoBLL.ObtenerCodigoEstado("Pendiente"))
                {
                    numCantProducir.Value = Convert.ToDecimal(dvDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_cantidad"]);
                    numCantStock.Enabled = false;
                    numCantProducir.Enabled = true;
                }
                else if (estado == BLL.EstadoDetallePedidoBLL.ObtenerCodigoEstado("Entrega Stock"))
                {
                    numCantStock.Value = Convert.ToDecimal(dvDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_cantidad"]);
                    numCantProducir.Enabled = false;
                    numCantStock.Enabled = true;
                }
                sfFechaPrevista.Value = Convert.ToDateTime(dvDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_fecha_entrega_prevista"]);
                SetInterface(estadoUI.modificarDetalle);
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjValidacion("Debe seleccionar un Detalle de Pedido", this.Text);
            }
        }

        private void btnCancelarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetallePedido.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
                {
                    //Cancelar un detalle de Pedido
                    DateTime fechaCancelacion = DBBLL.GetFechaServidor();
                    int codigoDetalle = Convert.ToInt32(dvDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_codigo"]);
                    int estadoActual = Convert.ToInt32(dvDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["edped_codigo"]);
                    int codigoPedido = Convert.ToInt32(dvDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["ped_codigo"]);

                    //Defino la fila de detalle de pedido
                    Data.dsCliente.DETALLE_PEDIDOSRow row = dsCliente.DETALLE_PEDIDOS.FindByDPED_CODIGO(Convert.ToDecimal(codigoDetalle));

                    if (estadoActual == BLL.EstadoDetallePedidoBLL.ObtenerCodigoEstado("Pendiente"))
                    {
                        BLL.DetallePedidoBLL.CancelarDetallePedido(codigoDetalle, fechaCancelacion);
                    }
                    else if (estadoActual == BLL.EstadoDetallePedidoBLL.ObtenerCodigoEstado("Pendiente"))
                    {
                        //Cancelamos el detalle de pedido
                        BLL.DetallePedidoBLL.CancelarDetallePedido(codigoDetalle, fechaCancelacion);

                        //Se tienen que eliminar los movimientos de stock asociados a ese detalle
                        int codigoEntidadPedido = BLL.EntidadBLL.ObtenerCodigoEntidad(codigoPedido);

                        //BLL.MovimientoStockBLL.EliminarMovimientosPedido(codigoEntidadPedido);

                        //Generamos un nuevo detalle de pedido                        
                        BLL.PedidoBLL.MovimientoStockPlanificado(row);
                    }          

                    row.BeginEdit();
                    row.DPED_FECHA_CANCELACION =Convert.ToDateTime(fechaCancelacion.ToShortDateString());
                    row.EDPED_CODIGO = BLL.EstadoDetallePedidoBLL.ObtenerCodigoEstado("Cancelado");
                    row.EndEdit();

                    dgvDetallePedido.Refresh();

                    //Muestro un mensaje que se cancelo un detalle de pedido
                    Entidades.Mensajes.MensajesABM.MsjValidacion("Se cancelo el detalle de pedido correctamente", this.Text);
                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjValidacion("Debe seleccionar un Detalle de Pedido", this.Text);
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


