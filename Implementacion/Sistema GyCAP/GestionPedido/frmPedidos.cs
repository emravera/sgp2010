using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.GestionPedido
{
    public partial class frmPedidos : Form
    {
        private Sistema.ControlesUsuarios.AnimadorFormulario animador = new GyCAP.UI.Sistema.ControlesUsuarios.AnimadorFormulario();
        private static frmPedidos _frmPedido = null;
        private Data.dsCliente dsCliente = new GyCAP.Data.dsCliente();
        private Data.dsEstadoPedidos dsEstadoPedido = new GyCAP.Data.dsEstadoPedidos();
        private DataView dvPedido, dvDetallePedido, dvCocinas, dvEstadoPedido;
        private DataView dvEstadoDetallePedido, dvClientes, dvEstadoPedidoBuscar;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        //Variable que simula el código autodecremental para el detalle, usa valores negativos para no tener problemas con valores existentes
        int codigoDetalle = -1;

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

                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnConsultar.Enabled = hayDatos;
                    btnNuevo.Enabled = true;
                    slideControl.Selected = slideDatos;
                    estadoInterface = estadoUI.inicio;
                    tcPedido.SelectedTab = tpBuscar;
                    txtNombreBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    setControles(false);
                    dvDetallePedido.RowFilter = "DPED_CODIGO < 0";
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    tcPedido.SelectedTab = tpDatos;
                    txtNumero.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    setControles(false);
                    dvDetallePedido.RowFilter = "DPED_CODIGO < 0";
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcPedido.SelectedTab = tpDatos;
                    txtNumero.Focus();
                    break;
                case estadoUI.consultar:
                    setControles(true);
                    btnGuardar.Enabled = false;
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
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.modificar;
                    tcPedido.SelectedTab = tpDatos;
                    txtNumero.Focus();
                    break;
                default:
                    break;
            }
        }

        private void setControles(bool pValue)
        {
            txtNumero.ReadOnly = pValue;
            txtObservacion.ReadOnly = pValue;
            cboClientes.Enabled = !pValue;
            cboEstado.Enabled = !pValue;
            sfFechaPrevista.Enabled = !pValue;  
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
            dgvLista.Columns.Add("PED_FECHAENTREGAPREVISTA", "Fecha Prevista");
            dgvLista.Columns.Add("EPED_CODIGO", "Estado");
            dgvLista.Columns.Add("PED_OBSERVACIONES", "Observaciones");
            dgvLista.Columns["PED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PED_NUMERO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["CLI_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PED_FECHAENTREGAPREVISTA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PED_FECHAENTREGAPREVISTA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["EPED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PED_OBSERVACIONES"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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
            dgvDetallePedido.Columns["EDPED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvCocinas.Columns.Add("COC_CODIGO_PRODUCTO", "Código");
            dgvCocinas.Columns.Add("MOD_CODIGO", "Modelo");
            dgvCocinas.Columns.Add("MCA_CODIGO", "Marca");
            dgvCocinas.Columns.Add("COC_ESTADO", "Estado");
            dgvCocinas.Columns.Add("COC_COSTO", "Costo");
            dgvCocinas.Columns["COC_CODIGO_PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvCocinas.Columns["MOD_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvCocinas.Columns["MCA_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvCocinas.Columns["COC_COSTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvCocinas.Columns["COC_COSTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCocinas.Columns["COC_ESTADO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna
            dgvLista.Columns["PED_CODIGO"].DataPropertyName = "PED_CODIGO";
            dgvLista.Columns["PED_NUMERO"].DataPropertyName = "PED_NUMERO";
            dgvLista.Columns["CLI_CODIGO"].DataPropertyName = "CLI_CODIGO";
            dgvLista.Columns["PED_FECHAENTREGAPREVISTA"].DataPropertyName = "PED_FECHAENTREGAPREVISTA";
            dgvLista.Columns["EPED_CODIGO"].DataPropertyName = "EPED_CODIGO";
            dgvLista.Columns["PED_OBSERVACIONES"].DataPropertyName = "PED_OBSERVACIONES";

            dgvDetallePedido.Columns["DPED_CODIGO"].DataPropertyName = "DPED_CODIGO";
            dgvDetallePedido.Columns["COC_CODIGO"].DataPropertyName = "COC_CODIGO";
            dgvDetallePedido.Columns["DPED_CANTIDAD"].DataPropertyName = "DPED_CANTIDAD";
            dgvDetallePedido.Columns["EDPED_CODIGO"].DataPropertyName = "EDPED_CODIGO";

            dgvCocinas.Columns["COC_CODIGO_PRODUCTO"].DataPropertyName = "COC_CODIGO_PRODUCTO";
            dgvCocinas.Columns["MOD_CODIGO"].DataPropertyName = "MOD_CODIGO";
            dgvCocinas.Columns["MCA_CODIGO"].DataPropertyName = "MCA_CODIGO";
            dgvCocinas.Columns["COC_ESTADO"].DataPropertyName = "COC_ESTADO";
            dgvCocinas.Columns["COC_COSTO"].DataPropertyName = "COC_COSTO";

            //Creamos el dataview y lo asignamos a la grilla
            dvPedido = new DataView(dsCliente.PEDIDOS);
            dvPedido.Sort = "PED_CODIGO ASC";
            dgvLista.DataSource = dvPedido;

            dvDetallePedido = new DataView(dsCliente.DETALLE_PEDIDOS);
            dgvDetallePedido.DataSource = dvDetallePedido;

            dvCocinas = new DataView(dsCliente.COCINAS);
            dvCocinas.Sort = "COC_CODIGO ASC";
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

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dvDetallePedido = new DataView(dsCliente.DETALLE_PEDIDOS);
            dvEstadoPedidoBuscar = new DataView(dsCliente.ESTADO_PEDIDOS);
            dvEstadoPedido = new DataView(dsCliente.ESTADO_PEDIDOS);
            dvCocinas = new DataView(dsCliente.COCINAS);

            cboEstadoBuscar.SetDatos(dvEstadoPedidoBuscar, "EPED_CODIGO", "EPED_NOMBRE", "--TODOS--", true);
            cboEstado.SetDatos(dvEstadoPedidoBuscar, "EPED_CODIGO", "EPED_NOMBRE", "--TODOS--", true);
            cboClientes.SetDatos(dvClientes, "CLI_CODIGO", "CLI_RAZONSOCIAL", "--TODOS--", true);  

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsCliente.PEDIDOS.Clear();
                dsCliente.DETALLE_PEDIDOS.Clear();

                //Busquemos, no importa si ingresó algo o no, ya se encargarán las otras clases de verificarlo
                BLL.PedidoBLL.ObtenerPedido(txtNombreBuscar.Text,txtNroPedidoBuscar,cboEstadoBuscar.GetSelectedValueInt(),DateTime.Parse ( sfFechaDesde.GetFecha().ToString() ),DateTime.Parse ( sfFechaHasta.GetFecha().ToString()) , dsCliente, true);

                if (dsCliente.PEDIDOS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Piezas con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(ex.Message, "Error: Pedido - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


    }
}
