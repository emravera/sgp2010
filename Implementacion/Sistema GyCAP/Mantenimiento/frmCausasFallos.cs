using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Mantenimiento
{
    public partial class frmCausasFallos : Form
    {
        //Definicion de Variables Globales
        private static frmCausasFallos _frmCausasFallos = null;
        private Data.dsMantenimiento dsMantenimiento = new GyCAP.Data.dsMantenimiento();
        private DataView dvCausaFallo;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
       
        #region Inicio_Pantalla

        public frmCausasFallos()
        {
            InitializeComponent();
            InicializarPantalla();
        }

        private void InicializarPantalla()
        {
            //Seteo la grilla de busqueda
            //Setea el nombre de la Lista
            dgvLista.Text = "Listado de " + this.Text;

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("CF_NUMERO", "Nro.");
            dgvLista.Columns.Add("CF_CODIGO", "Código");
            dgvLista.Columns.Add("CF_NOMBRE", "Causa fallo");
            dgvLista.Columns.Add("CF_DESCRPCION", "Descripción");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns["CF_NUMERO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLista.Columns["CF_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLista.Columns["CF_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLista.Columns["CF_DESCRPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLista.Columns["CF_NUMERO"].Width = 80;
            dgvLista.Columns["CF_CODIGO"].Width = 80;
            dgvLista.Columns["CF_NOMBRE"].Width = 220;
            dgvLista.Columns["CF_DESCRPCION"].Width = 242;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["CF_NUMERO"].DataPropertyName = "REP_CODIGO";
            dgvLista.Columns["CF_CODIGO"].DataPropertyName = "CF_CODIGO";
            dgvLista.Columns["CF_NOMBRE"].DataPropertyName = "CF_NOMBRE";
            dgvLista.Columns["CF_DESCRPCION"].DataPropertyName = "CF_DESCRPCION";

            //Alineacion de los numeros y las fechas en la grilla
            dgvLista.Columns["CF_NUMERO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["CF_NUMERO"].Visible = false;

            //Dataviews
            dvCausaFallo = new DataView(dsMantenimiento.CAUSAS_FALLO);
            dvCausaFallo.Sort = "CF_NOMBRE ASC";
            dgvLista.DataSource = dvCausaFallo;

            //Definicion de Maxlengh de los controles
            txtNombre.MaxLength = 80;
            txtNombreBuscar.MaxLength = 80;
            txtCodigo.MaxLength = 80;
            txtCodigoBuscar.MaxLength = 80;
            txtDescripcion.MaxLength = 199;
            
        }
        #endregion

        #region Servicios
        
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsMantenimiento.CAUSAS_FALLO.Rows.Count == 0)
                    {
                        hayDatos = false;
                    }
                    else
                    {
                        hayDatos = true;
                    }

                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }

                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnConsultar.Enabled = hayDatos;
                    btnNuevo.Enabled = true;
                    estadoInterface = estadoUI.inicio;
                    tcCausaFallo.SelectedTab = tpBuscar;
                    txtNombreBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Text = String.Empty;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Text = String.Empty;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcCausaFallo.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Text = String.Empty;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Text = String.Empty;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcCausaFallo.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    txtCodigo.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    btnGuardar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcCausaFallo.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    txtCodigo.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcCausaFallo.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }

        private void frmCausasFallos_Activated(object sender, EventArgs e)
        {
            if (txtNombreBuscar.Enabled == true)
            {
                txtNombreBuscar.Focus();
            }
        }
              
        public static frmCausasFallos Instancia
        {
            get
            {
                if (_frmCausasFallos == null || _frmCausasFallos.IsDisposed)
                {
                    _frmCausasFallos = new frmCausasFallos();
                }
                else
                {
                    _frmCausasFallos.BringToFront();
                }
                return _frmCausasFallos;
            }
            set
            {
                _frmCausasFallos = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
        }

        #endregion

        #region Pestaña Botones
        //Programacion de Cada uno de los botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.consultar);
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.modificar );
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }
        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjConfirmaEliminarDatos("Causa de fallo", Entidades.Mensajes.MensajesABM.Generos.Femenino, this.Text);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Lo eliminamos de la DB
                        int codigo = Convert.ToInt32(dvCausaFallo[dgvLista.SelectedRows[0].Index]["cf_numero"]);
                        BLL.CausaFalloBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsMantenimiento.CAUSAS_FALLO.FindByCF_NUMERO(codigo).Delete();
                        dsMantenimiento.CAUSAS_FALLO.AcceptChanges();
                        btnVolver.PerformClick();

                        //Mensaje de eliminación correcta
                        Entidades.Mensajes.MensajesABM.MsjConfirmaEliminar(this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Causa de fallo", Entidades.Mensajes.MensajesABM.Generos.Femenino, this.Text);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo y selecciono algo en el combo
            if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
            {
                Entidades.CausaFallo causaFallo = new GyCAP.Entidades.CausaFallo();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando una nueva
                    causaFallo.Nombre = txtNombre.Text.Trim();
                    causaFallo.Codigo = txtCodigo.Text.Trim();
                    causaFallo.Descripcion = txtDescripcion.Text.Trim();

                    try
                    {
                        //Primero lo creamos en la db
                        causaFallo.Numero = BLL.CausaFalloBLL.Insertar(causaFallo);

                        //Ahora lo agregamos al dataset
                        Data.dsMantenimiento.CAUSAS_FALLORow row = dsMantenimiento.CAUSAS_FALLO.NewCAUSAS_FALLORow();
                        //Indicamos que comienza la edición de la fila
                        row.BeginEdit();
                        row.CF_NUMERO = causaFallo.Numero;
                        row.CF_CODIGO = causaFallo.Codigo;
                        row.CF_NOMBRE = causaFallo.Nombre;
                        row.CF_DESCRPCION = causaFallo.Descripcion;
                        //Termina la edición de la fila
                        row.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsMantenimiento.CAUSAS_FALLO.AddCAUSAS_FALLORow(row);
                        dsMantenimiento.CAUSAS_FALLO.AcceptChanges();
                        //Y por último seteamos el estado de la interfaz

                        //Vemos cómo se inició el formulario para determinar la acción a seguir
                        if (estadoInterface == estadoUI.nuevoExterno)
                        {
                            //Avisamos que estuvo todo ok
                            Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Causa de fallo", this.Text, Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                            
                            //Nuevo desde acceso directo, cerramos el formulario
                            btnSalir.PerformClick();
                        }
                        else
                        {
                            //Nuevo desde el mismo formulario, volvemos a la pestaña buscar
                            SetInterface(estadoUI.inicio);
                        }
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                    }
                }
                else
                {
                    //Está modificando
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    causaFallo.Numero = Convert.ToInt32(dvCausaFallo[dgvLista.SelectedRows[0].Index]["cf_numero"]);
                    //Segundo obtenemos los nuevos datos que ingresó el usuario
                    causaFallo.Nombre = txtNombre.Text.Trim();
                    causaFallo.Codigo = txtCodigo.Text.Trim();
                    causaFallo.Descripcion = txtDescripcion.Text.Trim();

                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.CausaFalloBLL.Actualizar(causaFallo);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsMantenimiento.CAUSAS_FALLORow row = dsMantenimiento.CAUSAS_FALLO.FindByCF_NUMERO(causaFallo.Numero);
                        //Indicamos que comienza la edición de la fila
                        row.BeginEdit();
                        row.CF_CODIGO = causaFallo.Codigo;
                        row.CF_NOMBRE = causaFallo.Nombre;
                        row.CF_DESCRPCION = causaFallo.Descripcion;
                        //Termina la edición de la fila
                        row.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsMantenimiento.CAUSAS_FALLO.AcceptChanges();
                        //Avisamos que estuvo todo ok
                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Causa de fallo", this.Text, Entidades.Mensajes.MensajesABM.Operaciones.Modificación);
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                    }
                }
                dvCausaFallo.Table = dsMantenimiento.CAUSAS_FALLO;
            }            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsMantenimiento.CAUSAS_FALLO.Clear();              
                BLL.CausaFalloBLL.ObtenerTodos(txtNombreBuscar.Text, txtCodigoBuscar.Text, dsMantenimiento);
                dvCausaFallo.Table = dsMantenimiento.CAUSAS_FALLO;

                if (dsMantenimiento.CAUSAS_FALLO.Rows.Count == 0)
                {
                    Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Causa de fallo", this.Text);
                }
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
                SetInterface(estadoUI.inicio);
            }
        }

        #endregion

        #region Pestaña Eventos

        private void txtCodigo_Enter(object sender, EventArgs e)
        {
            txtCodigo.SelectAll();
        }

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            txtDescripcion.SelectAll();
        }

        private void txtCodigoBuscar_Enter(object sender, EventArgs e)
        {
            txtCodigoBuscar.SelectAll();
        }

        private void txtNombreBuscar_Enter(object sender, EventArgs e)
        {
            txtNombreBuscar.SelectAll();
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtNombre.SelectAll();
        }
        
        //Metodo que carga los datos desde la grilla hacia a los controles 
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int numero = Convert.ToInt32(dvCausaFallo[e.RowIndex]["cf_numero"]);
            txtNombre.Text = dsMantenimiento.CAUSAS_FALLO.FindByCF_NUMERO(numero).CF_NOMBRE;
            txtCodigo.Text = dsMantenimiento.CAUSAS_FALLO.FindByCF_NUMERO(numero).CF_CODIGO;
            txtDescripcion.Text = dsMantenimiento.CAUSAS_FALLO.FindByCF_NUMERO(numero).CF_DESCRPCION;
        }
        
        #endregion

    }
}
