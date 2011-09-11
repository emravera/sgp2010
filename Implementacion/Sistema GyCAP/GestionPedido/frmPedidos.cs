﻿using System;
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
        private DataView dvEstadoDetallePedido, dvClientes, dvEstadoPedidoBuscar;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, cargarDetalle };
        private estadoUI estadoInterface;
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

            //Agregamos las columnas y sus propiedades
            dgvLista.Columns.Add("PED_CODIGO", "Código");
            dgvLista.Columns.Add("PED_NUMERO", "Número");
            dgvLista.Columns.Add("CLI_CODIGO", "Cliente");
            dgvLista.Columns.Add("EPED_CODIGO", "Estado");
            dgvLista.Columns.Add("PED_OBSERVACIONES", "Observaciones");

            dgvLista.Columns["PED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PED_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["PED_NUMERO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PED_NUMERO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["CLI_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["EPED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PED_OBSERVACIONES"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Alineacion de los numeros y las fechas en la grilla
            dgvLista.Columns["PED_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["PED_CODIGO"].Visible = false;

            dgvDetallePedido.Columns.Add("DPED_CODIGO", "Codigo");
            dgvDetallePedido.Columns.Add("COC_CODIGO", "Cocina");
            dgvDetallePedido.Columns.Add("DPED_CANTIDAD", "Cantidad");
            dgvDetallePedido.Columns.Add("EDPED_CODIGO", "Estado");

            dgvDetallePedido.Columns["DPED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePedido.Columns["COC_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePedido.Columns["DPED_CANTIDAD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDetallePedido.Columns["DPED_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetallePedido.Columns["EDPED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Alineacion de los numeros y las fechas en la grilla
            dgvDetallePedido.Columns["DPED_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetallePedido.Columns["DPED_CODIGO"].Visible = false;

            dgvCocinas.Columns.Add("COC_CODIGO_PRODUCTO", "Código");
            dgvCocinas.Columns.Add("MOD_CODIGO", "Modelo");
            dgvCocinas.Columns.Add("MCA_CODIGO", "Marca");
            dgvCocinas.Columns["COC_CODIGO_PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvCocinas.Columns["MOD_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvCocinas.Columns["MCA_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Indicamos de dónde van a sacar los datos cada columna
            dgvLista.Columns["PED_CODIGO"].DataPropertyName = "PED_CODIGO";
            dgvLista.Columns["PED_NUMERO"].DataPropertyName = "PED_NUMERO";
            dgvLista.Columns["CLI_CODIGO"].DataPropertyName = "CLI_CODIGO";
            dgvLista.Columns["EPED_CODIGO"].DataPropertyName = "EPED_CODIGO";
            dgvLista.Columns["PED_OBSERVACIONES"].DataPropertyName = "PED_OBSERVACIONES";

            dgvDetallePedido.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";
            dgvDetallePedido.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDetallePedido.Columns["DPED_CANTIDAD"].DataPropertyName = "DPED_CANTIDAD";
            dgvDetallePedido.Columns["EDPED_CODIGO"].DataPropertyName = "EDPED_CODIGO";

            dgvCocinas.Columns["COC_CODIGO_PRODUCTO"].DataPropertyName = "COC_CODIGO_PRODUCTO";
            dgvCocinas.Columns["MOD_CODIGO"].DataPropertyName = "MOD_CODIGO";
            dgvCocinas.Columns["MCA_CODIGO"].DataPropertyName = "MCA_CODIGO";

            //Creamos el dataview y lo asignamos a la grilla
            dvPedido = new DataView(dsCliente.PEDIDOS);
            dvPedido.Sort = "PED_CODIGO ASC";
            dgvLista.DataSource = dvPedido;

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

            cboEstadoBuscar.SetDatos(dvEstadoPedidoBuscar, "EPED_CODIGO", "EPED_NOMBRE", "--TODOS--", true);
            cboEstado.SetDatos(dvEstadoPedidoBuscar, "EPED_CODIGO", "EPED_NOMBRE", "", false);
            cboClientes.SetDatos(dvClientes, "CLI_CODIGO", "CLI_RAZONSOCIAL", "", false);
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
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    dvDetallePedido.RowFilter = "DPED_CODIGO < 0";
                    tcPedido.SelectedTab = tpDatos;
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
                    btnNuevo.Enabled = false;
                    btnNewCliente.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.modificar;
                    tcPedido.SelectedTab = tpDatos;
                    break;
                case estadoUI.cargarDetalle:
                    btnAgregar.Enabled = false;
                    lblCantProducir.Visible = false;
                    lblCantStock.Visible = false;
                    numCantProducir.Visible = false;
                    numCantStock.Visible = false;
                    
                    //Escondemos los numeros y reseteamos los controles
                    numCantProducir.Value=0;
                    numCantStock.Value=0;
                    estadoInterface = estadoUI.cargarDetalle;
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
                    case "PED_FECHAENTREGAPREVISTA":
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
            if (estadoInterface == estadoUI.cargarDetalle)
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

        private void btnSumar_Click(object sender, EventArgs e)
        {
            if (dgvDetallePedido.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigoDetalle = Convert.ToInt32(dvDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_codigo"]);
                //Aumentamos la cantidad                
                dsCliente.DETALLE_PEDIDOS.FindByDPED_CODIGO(codigoDetalle).DPED_CANTIDAD += 1;
                dgvDetallePedido.Refresh();
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion("Debe seleccionar una Cocina de la lista.", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            if (dgvDetallePedido.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                long codigoDetalle = Convert.ToInt64(dvDetallePedido[dgvDetallePedido.SelectedRows[0].Index]["dped_codigo"]);
                //Disminuimos la cantidad
                if (dsCliente.DETALLE_PEDIDOS.FindByDPED_CODIGO(codigoDetalle).DPED_CANTIDAD > 1)
                {
                    dsCliente.DETALLE_PEDIDOS.FindByDPED_CODIGO(codigoDetalle).DPED_CANTIDAD -= 1;
                }
                dgvDetallePedido.Refresh();
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion("Debe seleccionar una Cocina de la lista.", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Validamos el formulario            
            if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
            {
                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando uno nuevo
                    try
                    {
                        //Como ahora tenemos más de una tabla y relacionadas vamos a trabajar diferente
                        //Primero lo agregamos a la tabla Piezas del dataset con código -1, luego la entidad 
                        //PiezaDAL se va a encargar de insertarle el código que corresponda y el stock inicial
                        Data.dsCliente.PEDIDOSRow rowPedido = dsCliente.PEDIDOS.NewPEDIDOSRow();
                        rowPedido.BeginEdit();
                        rowPedido.PED_CODIGO = -1;
                        rowPedido.PED_NUMERO = string.Empty;
                        rowPedido.PED_OBSERVACIONES = txtObservacion.Text.Trim();
                        rowPedido.EPED_CODIGO = cboEstado.GetSelectedValueInt();
                        rowPedido.CLI_CODIGO = long.Parse( cboClientes.GetSelectedValueInt().ToString()); 
                        rowPedido.PED_FECHA_ALTA = DBBLL.GetFechaServidor() ;
                        rowPedido.EndEdit();

                        dsCliente.PEDIDOS.AddPEDIDOSRow(rowPedido);
                        //Todavia no aceptamos los cambios porque necesitamos que queden marcadas como nuevas las filas
                        //para que la entidad PiezaBLL y PiezaDAL sepan cuales insertar
                        BLL.PedidoBLL.Insertar(dsCliente);
                        
                        //Ahora si aceptamos los cambios
                        dsCliente.PEDIDOS.AcceptChanges();
                        dsCliente.DETALLE_PEDIDOS.AcceptChanges();
                        
                        //Y por último seteamos el estado de la interfaz
                        //Vemos cómo se inició el formulario para determinar la acción a seguir
                        if (estadoInterface == estadoUI.nuevoExterno)
                        {
                            //Nuevo desde acceso directo, cerramos el formulario
                            btnSalir.PerformClick();
                        }
                        else
                        {
                            dgvLista.Refresh();

                            //Nuevo desde el mismo formulario, volvemos a la pestaña buscar
                            SetInterface(estadoUI.inicio);
                        }
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        //Ya existe la pieza, descartamos los cambios pero sólo de piezas ya que puede querer
                        //modificar el nombre y/o la terminación e intentar de nuevo con la estructura cargada
                        dsCliente.PEDIDOS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de piezas ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsCliente.PEDIDOS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                    }
                }
                else
                {
                    //Está modificando
                    //Primero obtenemos su código del dataview que está relacionado a la fila seleccionada
                    long codigoPedido = Convert.ToInt64(dvPedido[dgvLista.SelectedRows[0].Index]["ped_codigo"]);
                    
                    //Segundo obtenemos el resto de los datos que puede cambiar el usuario, el detalle se fué
                    //actualizando en el dataset a medida que el usuario ejecutaba una acción
                    dsCliente.PEDIDOS.FindByPED_CODIGO(codigoPedido).PED_NUMERO = txtNumero.Text;
                    dsCliente.PEDIDOS.FindByPED_CODIGO(codigoPedido).CLI_CODIGO = long.Parse(cboClientes.GetSelectedValueInt().ToString());
                    dsCliente.PEDIDOS.FindByPED_CODIGO(codigoPedido).EPED_CODIGO = cboEstado.GetSelectedValueInt();
                    dsCliente.PEDIDOS.FindByPED_CODIGO(codigoPedido).PED_OBSERVACIONES = txtObservacion.Text;
                                        
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.PedidoBLL.Actualizar(dsCliente);
                        
                        //El dataset ya se actualizó en las capas DAL y BLL, aceptamos los cambios
                        dsCliente.PEDIDOS.AcceptChanges();
                        dsCliente.DETALLE_PEDIDOS.AcceptChanges();
                        
                        //Avisamos que estuvo todo ok
                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Pedido", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Modificación);
                                                
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de piezas ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsCliente.PEDIDOS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                    }
                    catch (Entidades.Excepciones.ErrorInesperadoException ex)
                    {
                        //Hubo problemas no esperados, descartamos los cambios de piezas ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsCliente.PEDIDOS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                    }
                }
                dgvLista.Refresh();
            }            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int estado = Convert.ToInt32(dvPedido[dgvLista.SelectedRows[0].Index]["eped_codigo"]);
                if (estado == 1) //Si no esta pendiente no lo puede eliminar PARAMETRIZAR
                {
                    //Preguntamos si está seguro
                    DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjPreguntaAlUsuario("¿Está seguro que desea eliminar el Pedido seleccionado?", this.Text);
                    if (respuesta == DialogResult.Yes)
                    {
                        try
                        {
                            //Obtenemos el codigo
                            long codigo = Convert.ToInt64(dvPedido[dgvLista.SelectedRows[0].Index]["ped_codigo"]);
                            
                            //Lo eliminamos de la DB
                            BLL.PedidoBLL.Eliminar(codigo);
                            
                            //Lo eliminamos de la tabla conjuntos del dataset
                            dsCliente.PEDIDOS.FindByPED_CODIGO(codigo).Delete();
                            dsCliente.PEDIDOS.AcceptChanges();
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
                    string lEstado;
                    lEstado = dsCliente.ESTADO_DETALLE_PEDIDOS.FindByEDPED_CODIGO(Convert.ToDecimal( dvPedido[dgvLista.SelectedRows[0].Index]["eped_codigo"])).EDPED_NOMBRE;
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
            cboClientes.SetDatos(dvClientes, "CLI_CODIGO", "CLI_RAZONSOCIAL", "", false);
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvCocinas.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0 && nudCantidad.Value > 0)
            {
                bool agregarCocina; //variable que indica si se debe agregar la cocina al listado
                
                //Obtenemos el código de la pieza según sea nueva o modificada, lo hacemos acá porque lo vamos a usar mucho
                int pedidoCodigo;
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { pedidoCodigo = -1; }
                else { pedidoCodigo = Convert.ToInt32(dvPedido[dgvLista.SelectedRows[0].Index]["ped_codigo"]); }
                
                //Obtenemos el código de la cocina, también lo vamos a usar mucho
                int cocinaCodigo = Convert.ToInt32(dvCocinas[dgvCocinas.SelectedRows[0].Index]["coc_codigo"]);

                //Primero vemos si el pedido tiene algúna cocina cargada, como ya hemos filtrado el dataview
                //esté sabrá decirnos cuantas filas tiene el conjunto seleccionado                
                if (dvDetallePedido.Count > 0)
                {
                    //Algo tiene, comprobemos que no intente agregar la misma cocina haciendo una consulta al dataset,
                    //no usamos el dataview porque no queremos volver a filtrar los datos y perderlos
                    string filtro = "ped_codigo = " + pedidoCodigo + " AND coc_codigo = " + cocinaCodigo;
                    Data.dsCliente.DETALLE_PEDIDOSRow[] rows = (Data.dsCliente.DETALLE_PEDIDOSRow[])dsCliente.DETALLE_PEDIDOS.Select(filtro);
                    
                    if (rows.Length > 0)
                    {
                        //Ya lo ha agregado, preguntemos si quiere aumentar la cantidad existente o descartar
                        DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjPreguntaAlUsuario("El Pedido ya posee la cocina seleccionada. ¿Desea sumar la cantidad ingresada?",this.Text); ;
                        if (respuesta == DialogResult.Yes)
                        {
                            //Sumemos la cantidad ingresada a la existente, como hay una sola fila seleccionamos la 0 del array
                            rows[0].DPED_CANTIDAD += int.Parse(nudCantidad.Value.ToString());
                            nudCantidad.Value = 0;
                        }
                        //Como ya existe marcamos que no debe agregarse
                        agregarCocina = false;
                    }
                    else
                    {
                        //No lo ha agregado, marcamos que debe agregarse
                        agregarCocina = true;
                    }
                }
                else
                {
                    //No tiene ningúna materia prima agregada, marcamos que debe agregarse
                    agregarCocina = true;
                }

                //Ahora comprobamos si debe agregarse la cocina o no
                if (agregarCocina)
                {
                    Data.dsCliente.DETALLE_PEDIDOSRow row = dsCliente.DETALLE_PEDIDOS.NewDETALLE_PEDIDOSRow();
                    row.BeginEdit();
                    
                    //Agregamos una fila nueva con nuestro código autodecremental, luego al guardar en la db se actualizará
                    row.DPED_CODIGO = codigoDetalle--; //-- para que se vaya autodecrementando en cada inserción
                    row.PED_CODIGO = pedidoCodigo;
                    row.COC_CODIGO = cocinaCodigo;
                    row.EDPED_CODIGO = 1; //Esto tiene que ser un parametro
                    row.DPED_CANTIDAD = int.Parse(nudCantidad.Value.ToString());
                    row.EndEdit();
                    
                    //Agregamos la fila nueva al dataset sin aceptar cambios para que quede marcada como nueva ya que
                    //todavia no vamos a insertar en la db hasta que no haga Guardar
                    dsCliente.DETALLE_PEDIDOS.AddDETALLE_PEDIDOSRow(row);
                    nudCantidad.Value = 0;
                }
                nudCantidad.Value = 0;
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion("Debe seleccionar una Cocina de la lista y asignarle una cantidad mayor a 0.", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
        }       

        #endregion      

        private void btnValidar_Click(object sender, EventArgs e)
        {
            //Hay que validar que se ingrese una fecha y una cantidad total de cocinas
            if (nudCantidad.Value != 0 && sfFechaPrevista.Value != null)
            {
                //Verificamos si hay stock para una cocina determinada para esa fecha




            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjValidacion("Debe completar los datos obligatorios", this.Text);
            }
        }

    }
}
