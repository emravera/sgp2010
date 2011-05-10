using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.EstructuraProducto
{
    public partial class frmMateriaPrimaPrincipal : Form
    {
        private static frmMateriaPrimaPrincipal _frmMateriaPrima = null;
        private Data.dsPlanMP dsMateriaPrima = new GyCAP.Data.dsPlanMP();
        private enum estadoUI { inicio, nuevo, modificar };
        private DataView dvListaBusqueda, dvCbUnidadMedida, dvCbTipoUnMedida, dvCbUbicacionStock;
        private static estadoUI estadoInterface;

        #region Inicio Pantalla

        public frmMateriaPrimaPrincipal()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;

            //Agregamos las columnas
            dgvLista.Columns.Add("MP_CODIGO", "Código");
            dgvLista.Columns.Add("MP_NOMBRE", "Materia Prima");
            dgvLista.Columns.Add("MP_DESCRIPCION", "Descripción");
            dgvLista.Columns.Add("UMED_CODIGO", "Unidad de Medida");
            dgvLista.Columns.Add("MP_COSTO", "Costo/Unidad");
            dgvLista.Columns.Add("USTCK_NUMERO", "Ubicacion Stock");
            dgvLista.Columns.Add("MP_ESPRINCIPAL", "Principal");
            dgvLista.Columns.Add("MP_CANTIDAD", "Cantidad");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns["MP_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["MP_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["MP_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["UMED_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["MP_COSTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["USTCK_NUMERO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["MP_ESPRINCIPAL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["MP_CANTIDAD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            //Habilito resize
            dgvLista.Columns["MP_CODIGO"].Resizable = DataGridViewTriState.True;
            dgvLista.Columns["MP_NOMBRE"].Resizable = DataGridViewTriState.True;
            dgvLista.Columns["MP_DESCRIPCION"].Resizable = DataGridViewTriState.True;
            dgvLista.Columns["UMED_CODIGO"].Resizable = DataGridViewTriState.True;
            dgvLista.Columns["MP_COSTO"].Resizable = DataGridViewTriState.True;
            dgvLista.Columns["USTCK_NUMERO"].Resizable = DataGridViewTriState.True;
            dgvLista.Columns["MP_ESPRINCIPAL"].Resizable = DataGridViewTriState.True;
            dgvLista.Columns["MP_CANTIDAD"].Resizable = DataGridViewTriState.True;
            
            //Alineo la columna
            dgvLista.Columns["MP_CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["MP_COSTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //Oculto el Codigo
            dgvLista.Columns[0].Visible = false;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["MP_CODIGO"].DataPropertyName = "MP_CODIGO";
            dgvLista.Columns["MP_NOMBRE"].DataPropertyName = "MP_NOMBRE";
            dgvLista.Columns["MP_DESCRIPCION"].DataPropertyName = "MP_DESCRIPCION";
            dgvLista.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";
            dgvLista.Columns["MP_COSTO"].DataPropertyName = "MP_COSTO";
            dgvLista.Columns["USTCK_NUMERO"].DataPropertyName = "USTCK_NUMERO";
            dgvLista.Columns["MP_ESPRINCIPAL"].DataPropertyName = "MP_ESPRINCIPAL";
            dgvLista.Columns["MP_CANTIDAD"].DataPropertyName = "MP_CANTIDAD";
            
            //Seteo el Dataview de la Lista
            dvListaBusqueda = new DataView(dsMateriaPrima.MATERIAS_PRIMAS);
            dgvLista.DataSource = dvListaBusqueda;

            //LLeno cada uno de los datatable con los datos
            
            //Lleno el DataTable con los Tipos de Unidades de Medida
            BLL.TipoUnidadMedidaBLL.ObtenerTodos(dsMateriaPrima.TIPOS_UNIDADES_MEDIDA);

            //Lleno el Datatable de las Ubicaciones de Stock (El valor 5 esta en la tabla de ubicaciones que tienen MP)
            BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsMateriaPrima.UBICACIONES_STOCK, Convert.ToInt16(5));

            //CARGA DE COMBOS
            //Combo de Datos
            //Creamos el Dataview y se lo asignamos al combo de tipo de unidad de medida
            dvCbTipoUnMedida = new DataView(dsMateriaPrima.TIPOS_UNIDADES_MEDIDA);
            cbTipoUnMedida.DataSource = dvCbTipoUnMedida;
            cbTipoUnMedida.SetDatos(dvCbTipoUnMedida, "tumed_codigo", "tumed_nombre", "-Seleccionar-", false);

            //Creamos el Dataview y se lo asignamos al combo de ubicaciones de stock
            dvCbUbicacionStock = new DataView(dsMateriaPrima.UBICACIONES_STOCK);
            cbUbicacionStock.DataSource = dvCbUbicacionStock;
            cbUbicacionStock.SetDatos(dvCbUbicacionStock, "ustck_numero", "ustck_nombre", "-Seleccionar-", false);
                   
            //Seteo la propiedad del Incremento de la cantidad
            numCosto.DecimalPlaces = 2;
            numCantidad.DecimalPlaces = 2;
            numCosto.Increment = Convert.ToDecimal("0,01");
            numCantidad.Increment = Convert.ToDecimal("0,01");

            //Seteamos el estado de la interface
            SetInterface(estadoUI.inicio);                      
        }

        public static frmMateriaPrimaPrincipal Instancia
        {
            get
            {
                if (_frmMateriaPrima == null || _frmMateriaPrima.IsDisposed)
                {
                    _frmMateriaPrima = new frmMateriaPrimaPrincipal();
                }
                else
                {
                    _frmMateriaPrima.BringToFront();
                }
                return _frmMateriaPrima;
            }
            set
            {
                _frmMateriaPrima = value;
            }
        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void frmMateriaPrimaPrincipal_Activated(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }
               
        private void btnModificar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.modificar);
        }
        
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Busca por defecto todas las materias primas
                int esPrincipal=3;
                dsMateriaPrima.MATERIAS_PRIMAS.Clear();

                if (rbPcipalBuscar.Checked) { esPrincipal = 1; }
                if (rbNOPcipalBuscar.Checked) { esPrincipal = 0; }
                if (rbTodosBuscar.Checked) { esPrincipal = 3; }

                BLL.MateriaPrimaBLL.ObtenerMP(txtNombreBuscar.Text, esPrincipal , dsMateriaPrima.MATERIAS_PRIMAS);
                
                if (dsMateriaPrima.MATERIAS_PRIMAS.Rows.Count == 0)
                {
                    Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Materias Primas", this.Text);
                }
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
                SetInterface(estadoUI.inicio);
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Validamos el formulario
                if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
                {
                    //Creamos el objeto materia prima
                    Entidades.MateriaPrima materiaPrima = new GyCAP.Entidades.MateriaPrima();

                    

                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
            }
        }

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsMateriaPrima.UNIDADES_MEDIDA.Rows.Count == 0)
                    {
                        hayDatos = false;
                    }
                    else
                    {
                        hayDatos = true;
                    }
                    btnNuevo.Enabled = true;
                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    dgvLista.Enabled = true;
                    tcMateriaPrima.SelectedTab = tpBuscar;

                    estadoInterface = estadoUI.inicio;
                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }
                    
                    //Manejo de controles
                    txtNombre.Text = String.Empty;
                    txtNombre.Focus();
                    rbTodosBuscar.Checked = true;
                    break;
                case estadoUI.nuevo:
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;

                    rbNOPcipalDatos.Checked = true;
                    numCantidad.Enabled = false;
                    tcMateriaPrima.SelectedTab = tpDatos;
                    estadoInterface = estadoUI.nuevo;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    dgvLista.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    break;
                default:
                    break;
            }
        }
        
        private void dgvLista_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "UMED_CODIGO":
                        nombre = dsMateriaPrima.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(e.Value)).UMED_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "USTCK_CODIGO":
                        nombre = dsMateriaPrima.UBICACIONES_STOCK.FindByUSTCK_NUMERO(Convert.ToInt32(e.Value)).USTCK_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "MP_ESPRINCIPAL":
                        if (Convert.ToInt32(e.Value) == 0) { e.Value = "NO"; }
                        else if (Convert.ToInt32(e.Value) == 1) { e.Value = "SI"; }
                        break;
                    default:
                        break;
                }
            }
        }

        private void cbTipoUnMedida_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if(cbTipoUnMedida.GetSelectedIndex() != -1)
                {
                    //Obtengo el tipo de unidad de medida seleccionado
                    int tipo =Convert.ToInt32(cbTipoUnMedida.GetSelectedValue());

                    //Limpio el Datatable 
                    dsMateriaPrima.UNIDADES_MEDIDA.Clear();
                    
                    //Llamo a la busqueda de unidades de medida
                    BLL.UnidadMedidaBLL.ObtenerTodos("", tipo, dsMateriaPrima);
                    
                    //Checkeo que existan registros y cargo el combo
                    if (dsMateriaPrima.UNIDADES_MEDIDA.Count > 0)
                    {
                        //Creamos el Dataview y se lo asignamos al combo de unidades de medida
                        dvCbUnidadMedida = new DataView(dsMateriaPrima.UNIDADES_MEDIDA);
                        cbUnidadMedida.DataSource = dvCbUnidadMedida;
                        cbUnidadMedida.SetDatos(dvCbUnidadMedida, "umed_codigo", "umed_nombre", "-Seleccionar-", false);
                    }
                    else
                    {
                        Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Unidades de Medida", this.Text);
                    }
                   
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
            }            
        }

        private void rbPcipalDatos_CheckedChanged(object sender, EventArgs e)
        {
            //Permito que se cargue la cantidad
            numCantidad.Enabled = true;            
        }

        private void rbNOPcipalDatos_CheckedChanged(object sender, EventArgs e)
        {
            //Cuando no es principal no dejo que se cargue nada
            numCantidad.Enabled = false;
            numCantidad.Value = 0;
        }

        #endregion

    }       
        
}
