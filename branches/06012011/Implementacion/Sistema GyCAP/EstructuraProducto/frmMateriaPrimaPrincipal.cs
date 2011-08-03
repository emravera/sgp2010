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
        private enum estadoUI { inicio, nuevo, modificar, consultar };
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
            dgvLista.Columns["MP_CANTIDAD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


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

            //Lleno el DataTable con las Unidades de Medida
            BLL.UnidadMedidaBLL.ObtenerTodos(dsMateriaPrima.UNIDADES_MEDIDA);

            //Lleno el DataTable con los Tipos de Unidades de Medida
            BLL.TipoUnidadMedidaBLL.ObtenerTodos(dsMateriaPrima.TIPOS_UNIDADES_MEDIDA);

            //Lleno el Datatable de las Ubicaciones de Stock (El valor 5 esta en la tabla de ubicaciones que tienen MP)
            BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsMateriaPrima.UBICACIONES_STOCK);

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
        
        #region Botones Formulario

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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Cargamos los datos a los controles 
                txtNombre.Text = dvListaBusqueda[dgvLista.SelectedRows[0].Index]["mp_nombre"].ToString();
                txtDescripcion.Text = dvListaBusqueda[dgvLista.SelectedRows[0].Index]["mp_descripcion"].ToString();
                numCosto.Value = Convert.ToDecimal(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["mp_costo"]);

                //Cargo todas las unidades de medida
                BLL.UnidadMedidaBLL.ObtenerTodos(dsMateriaPrima.UNIDADES_MEDIDA);

                //Creamos el Dataview y se lo asignamos al combo de unidades de medida
                dvCbUnidadMedida = new DataView(dsMateriaPrima.UNIDADES_MEDIDA);
                cbUnidadMedida.DataSource = dvCbUnidadMedida;
                cbUnidadMedida.SetDatos(dvCbUnidadMedida, "umed_codigo", "umed_nombre", "-Seleccionar-", false);

                cbTipoUnMedida.SetSelectedValue(Convert.ToInt32(dsMateriaPrima.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["umed_codigo"])).TUMED_CODIGO));
                cbUnidadMedida.SetSelectedValue(Convert.ToInt32(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["umed_codigo"]));
                cbUbicacionStock.SetSelectedValue(Convert.ToInt32(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["ustck_numero"]));
                numCosto.Value = Convert.ToDecimal(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["ustck_numero"]);

                if (dvListaBusqueda[dgvLista.SelectedRows[0].Index]["mp_esprincipal"].ToString() == "1")
                {
                    rbPcipalDatos.Checked = true;
                    numCantidad.Value = Convert.ToDecimal(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["mp_cantidad"]);
                }
                else
                {
                    rbNOPcipalDatos.Checked = true;
                    numCantidad.Value = 0;
                }

                SetInterface(estadoUI.consultar);
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Materia Prima", Entidades.Mensajes.MensajesABM.Generos.Femenino, this.Text);
            }

        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Cargamos los datos a los controles 
                txtNombre.Text = dvListaBusqueda[dgvLista.SelectedRows[0].Index]["mp_nombre"].ToString();
                txtDescripcion.Text = dvListaBusqueda[dgvLista.SelectedRows[0].Index]["mp_descripcion"].ToString();
                numCosto.Value = Convert.ToDecimal(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["mp_costo"]);

                //Cargo todas las unidades de medida
                BLL.UnidadMedidaBLL.ObtenerTodos(dsMateriaPrima.UNIDADES_MEDIDA);

                //Creamos el Dataview y se lo asignamos al combo de unidades de medida
                dvCbUnidadMedida = new DataView(dsMateriaPrima.UNIDADES_MEDIDA);
                cbUnidadMedida.DataSource = dvCbUnidadMedida;
                cbUnidadMedida.SetDatos(dvCbUnidadMedida, "umed_codigo", "umed_nombre", "-Seleccionar-", false);

                cbTipoUnMedida.SetSelectedValue(Convert.ToInt32(dsMateriaPrima.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["umed_codigo"])).TUMED_CODIGO));
                cbUnidadMedida.SetSelectedValue(Convert.ToInt32(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["umed_codigo"]));
                cbUbicacionStock.SetSelectedValue(Convert.ToInt32(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["ustck_numero"]));
                numCosto.Value = Convert.ToDecimal(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["ustck_numero"]);

                if (dvListaBusqueda[dgvLista.SelectedRows[0].Index]["mp_esprincipal"].ToString() == "1")
                {
                    rbPcipalDatos.Checked = true;
                    numCantidad.Enabled = true;
                    numCantidad.Value = Convert.ToDecimal(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["mp_cantidad"]);
                }
                else
                {
                    rbNOPcipalDatos.Checked = true;
                    numCantidad.Enabled = false;
                    numCantidad.Value = 0;
                }

                SetInterface(estadoUI.modificar);
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Materia Prima", Entidades.Mensajes.MensajesABM.Generos.Femenino, this.Text);
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
                    Entidades.UbicacionStock ubicacion = new GyCAP.Entidades.UbicacionStock();

                    materiaPrima.Nombre = txtNombre.Text;
                    materiaPrima.Descripcion = txtDescripcion.Text;
                    materiaPrima.CodigoUnidadMedida = Convert.ToInt32(cbUnidadMedida.GetSelectedValue());
                    materiaPrima.Costo = numCosto.Value;
                    ubicacion.Numero = Convert.ToInt32(cbUbicacionStock.GetSelectedValue());
                    materiaPrima.UbicacionStock = ubicacion;
                    if (rbPcipalDatos.Checked == true)
                    {
                        materiaPrima.EsPrincipal = 1;
                        materiaPrima.Cantidad = numCantidad.Value;
                    }
                    else if (rbNOPcipalDatos.Checked == true)
                    {
                        materiaPrima.EsPrincipal = 0;
                        materiaPrima.Cantidad = Convert.ToDecimal(0.00);
                    }

                    //Verificamos si esta guardando un registro nuevo
                    if (estadoInterface == estadoUI.nuevo)
                    {
                        //Lo insertamos en la base de datos
                        BLL.MateriaPrimaBLL.Insertar(materiaPrima);
                        
                        //Agregamos la fila al dataset
                        Data.dsPlanMP.MATERIAS_PRIMASRow row = dsMateriaPrima.MATERIAS_PRIMAS.NewMATERIAS_PRIMASRow();

                        //Editamos la fila
                        row.BeginEdit();
                        row.MP_NOMBRE = materiaPrima.Nombre;
                        row.MP_DESCRIPCION = materiaPrima.Descripcion;
                        row.UMED_CODIGO = materiaPrima.CodigoUnidadMedida;
                        row.MP_COSTO = materiaPrima.Costo;
                        row.USTCK_NUMERO =materiaPrima.UbicacionStock.Numero;
                        row.MP_ESPRINCIPAL = materiaPrima.EsPrincipal;
                        row.MP_CANTIDAD = materiaPrima.Cantidad;
                        row.EndEdit();

                        //Mostramos un mensaje que se guardo todo bien
                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Materia Prima", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);

                        //Recargamos la unidad de medida
                        dsMateriaPrima.UNIDADES_MEDIDA.Clear();
                        
                        //Lleno el DataTable con las Unidades de Medida
                        BLL.UnidadMedidaBLL.ObtenerTodos(dsMateriaPrima.UNIDADES_MEDIDA);
                        
                        //Ponemos la interfaz en el estado de inicio
                        SetInterface(estadoUI.inicio);
                    }
                    else if (estadoInterface == estadoUI.modificar)
                    {
                        //Esta modificando un registro existente
                        materiaPrima.CodigoMateriaPrima= Convert.ToInt32(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["mp_codigo"]);

                        //Lo insertamos en la base de datos
                        BLL.MateriaPrimaBLL.Actualizar(materiaPrima);

                        //Modificamos la fila del dataset
                        Data.dsPlanMP.MATERIAS_PRIMASRow row = dsMateriaPrima.MATERIAS_PRIMAS.FindByMP_CODIGO(materiaPrima.CodigoMateriaPrima);

                        //Editamos la fila
                        row.BeginEdit();
                        row.MP_NOMBRE = materiaPrima.Nombre;
                        row.MP_DESCRIPCION = materiaPrima.Descripcion;
                        row.UMED_CODIGO = materiaPrima.CodigoUnidadMedida;
                        row.MP_COSTO = materiaPrima.Costo;
                        row.USTCK_NUMERO = materiaPrima.UbicacionStock.Numero;
                        row.MP_ESPRINCIPAL = materiaPrima.EsPrincipal;
                        row.MP_CANTIDAD = materiaPrima.Cantidad;
                        row.EndEdit();

                        //Mostramos un mensaje que se guardo todo bien
                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Materia Prima", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);

                        //Ponemos la interfaz en el estado de inicio
                        SetInterface(estadoUI.inicio);                       
                    }
                }
            }
            catch (Entidades.Excepciones.ElementoExistenteException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
            }
            catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
            }
        }
        
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjConfirmaEliminarDatos("Materia Prima", Entidades.Mensajes.MensajesABM.Generos.Femenino, this.Text);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Lo eliminamos de la DB
                        int codigo = Convert.ToInt32(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["mp_codigo"]);
                        BLL.MateriaPrimaBLL.Eliminar(codigo);
                        
                        //Lo eliminamos del dataset
                        dsMateriaPrima.MATERIAS_PRIMAS.FindByMP_CODIGO(codigo).Delete();
                        dsMateriaPrima.AcceptChanges();

                        Entidades.Mensajes.MensajesABM.MsjConfirmaEliminar(this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
                    }
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Materia Prima", Entidades.Mensajes.MensajesABM.Generos.Femenino, this.Text);
            }

        }
        #endregion

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
                    btnConsultar.Enabled = hayDatos;
                    dgvLista.Enabled = true;
                    tcMateriaPrima.SelectedTab = tpBuscar;

                    estadoInterface = estadoUI.inicio;
                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }
                    
                    //Cargo las unidades de medida por posibles errores
                    dsMateriaPrima.UNIDADES_MEDIDA.Clear();
                    BLL.UnidadMedidaBLL.ObtenerTodos(dsMateriaPrima.UNIDADES_MEDIDA);

                    dsMateriaPrima.UBICACIONES_STOCK.Clear();
                    BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsMateriaPrima.UBICACIONES_STOCK);

                    //Bloqueos de los controles
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    cbTipoUnMedida.Enabled = true;
                    cbUnidadMedida.Enabled = true;
                    cbUbicacionStock.Enabled = true;
                    numCosto.Enabled = true;
                    rbNOPcipalDatos.Enabled = true;
                    rbPcipalDatos.Enabled = true;

                    rbTodosBuscar.Checked = true;
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    cbTipoUnMedida.Enabled = true;
                    cbUnidadMedida.Enabled = true;
                    cbUbicacionStock.Enabled = true;
                    numCosto.Enabled = true;
                    rbNOPcipalDatos.Enabled = true;
                    rbPcipalDatos.Enabled = true;
                    
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;

                    rbNOPcipalDatos.Checked = true;
                    numCantidad.Enabled = false;

                    //Manejo de controles
                    txtNombre.Text = String.Empty;
                    txtNombre.Focus();
                    txtDescripcion.Text = string.Empty;
                    cbTipoUnMedida.SetSelectedIndex(-1);
                    cbUbicacionStock.SetSelectedIndex(-1);
                    cbUnidadMedida.SetSelectedIndex(-1);
                    numCantidad.Value = 0;
                    numCosto.Value = 0;
                    rbNOPcipalDatos.Checked = true;
                    tcMateriaPrima.SelectedTab = tpDatos;
                    estadoInterface = estadoUI.nuevo;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    cbTipoUnMedida.Enabled = true;
                    cbUnidadMedida.Enabled = true;
                    cbUbicacionStock.Enabled = true;
                    numCosto.Enabled = true;
                    rbNOPcipalDatos.Enabled = true;
                    rbPcipalDatos.Enabled = true;

                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    dgvLista.Enabled = false;
                    
                    tcMateriaPrima.SelectedTab = tpDatos;
                    estadoInterface = estadoUI.modificar;
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    cbTipoUnMedida.Enabled = false;
                    cbUnidadMedida.Enabled = false;
                    cbUbicacionStock.Enabled = false;
                    numCosto.Enabled = false;
                    rbNOPcipalDatos.Enabled = false;
                    rbPcipalDatos.Enabled = false;
                    numCantidad.Enabled = false;
                    
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    dgvLista.Enabled = false;

                    tcMateriaPrima.SelectedTab = tpDatos;
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
                    case "USTCK_NUMERO":
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
                if (estadoInterface == estadoUI.nuevo)
                {
                    if (cbTipoUnMedida.GetSelectedIndex() != -1)
                    {
                        //Obtengo el tipo de unidad de medida seleccionado
                        int tipo = Convert.ToInt32(cbTipoUnMedida.GetSelectedValue());

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
