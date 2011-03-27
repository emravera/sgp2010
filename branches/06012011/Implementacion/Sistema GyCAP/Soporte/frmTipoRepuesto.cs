using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Soporte
{
    public partial class frmTipoRepuesto : Form
    {

        private static frmTipoRepuesto _frmTipoRepuesto = null;
        private Data.dsMantenimiento dsMantenimiento = new GyCAP.Data.dsMantenimiento();
        private DataView dvTipoRepuesto;
        private enum estadoUI { inicio, modificar };
        private estadoUI estadoInterface;

        #region Inicio
        public frmTipoRepuesto()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("TREP_NOMBRE", "Nombre");
            dgvLista.Columns.Add("TREP_DESCRIPCION", "Descripción");
            dgvLista.Columns["TREP_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["TREP_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["TREP_NOMBRE"].DataPropertyName = "TREP_NOMBRE";
            dgvLista.Columns["TREP_DESCRIPCION"].DataPropertyName = "TREP_DESCRIPCION";
            //Creamos el dataview y lo asignamos a la grilla
            BLL.TipoRepuestoBLL.ObtenerTodos(dsMantenimiento);
            dvTipoRepuesto = new DataView(dsMantenimiento.TIPOS_REPUESTOS);
            dvTipoRepuesto.Sort = "TREP_NOMBRE ASC";
            dgvLista.DataSource = dvTipoRepuesto;
            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
        }
        #endregion

        #region Botones

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigo = Convert.ToInt32(dvTipoRepuesto[dgvLista.SelectedRows[0].Index]["TREP_CODIGO"]);
                txtNombre.Text = dsMantenimiento.TIPOS_REPUESTOS.FindByTREP_CODIGO(codigo).TREP_NOMBRE;
                txtDescripcion.Text = dsMantenimiento.TIPOS_REPUESTOS.FindByTREP_CODIGO(codigo).TREP_DESCRIPCION ;
                txtNombre.Focus();
                SetInterface(estadoUI.modificar);
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Tipo de repuesto", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjConfirmaEliminarDatos("Tipo de repuesto", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtenemos
                        int codigo = Convert.ToInt32(dvTipoRepuesto[dgvLista.SelectedRows[0].Index]["TREP_CODIGO"]);
                        //Lo eliminamos de la DB
                        BLL.TipoRepuestoBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsMantenimiento.TIPOS_REPUESTOS.FindByTREP_CODIGO(codigo).Delete();
                        dsMantenimiento.TIPOS_REPUESTOS.AcceptChanges();

                        //Mensaje de confirmacion de eliminacion
                        Entidades.Mensajes.MensajesABM.MsjConfirmaEliminar(this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        dsMantenimiento.TIPOS_REPUESTOS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsMantenimiento.TIPOS_REPUESTOS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Tipo de repuesto", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string validacion = Validar();

            //Revisamos que escribió algo
            if (validacion == String.Empty)
            {
                Entidades.TipoRepuesto tipoRepuesto = new GyCAP.Entidades.TipoRepuesto();

                //Revisamos que está haciendo
                if (estadoInterface != estadoUI.modificar)
                {
                    //Está cargando uno nuevo
                    tipoRepuesto.Nombre = txtNombre.Text;
                    tipoRepuesto.Descripcion = txtDescripcion.Text;
                    try
                    {
                        //Primero lo creamos en la db
                        tipoRepuesto.Codigo = BLL.TipoRepuestoBLL.Insertar(tipoRepuesto);
                        //Ahora lo agregamos al dataset
                        Data.dsMantenimiento.TIPOS_REPUESTOSRow rowTipoRepuesto = dsMantenimiento.TIPOS_REPUESTOS.NewTIPOS_REPUESTOSRow();
                        //Indicamos que comienza la edición de la fila
                        rowTipoRepuesto.BeginEdit();
                        rowTipoRepuesto.TREP_CODIGO = tipoRepuesto.Codigo;
                        rowTipoRepuesto.TREP_NOMBRE = tipoRepuesto.Nombre;
                        rowTipoRepuesto.TREP_DESCRIPCION  = tipoRepuesto.Descripcion ;
                        //Termina la edición de la fila
                        rowTipoRepuesto.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsMantenimiento.TIPOS_REPUESTOS.AddTIPOS_REPUESTOSRow(rowTipoRepuesto);
                        dsMantenimiento.TIPOS_REPUESTOS.AcceptChanges();

                        //Avisamos que se guardo correctamente
                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Tipo de repuesto", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);

                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        dsMantenimiento.TIPOS_REPUESTOS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsMantenimiento.TIPOS_REPUESTOS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                    }
                }
                else
                {
                    //Está modificando
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    tipoRepuesto.Codigo = Convert.ToInt32(dvTipoRepuesto[dgvLista.SelectedRows[0].Index]["TREP_CODIGO"]);
                    //Segundo obtenemos el nuevo nombre que ingresó el usuario
                    tipoRepuesto.Nombre = txtNombre.Text;
                    tipoRepuesto.Descripcion= txtDescripcion.Text;
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.TipoRepuestoBLL.Actualizar(tipoRepuesto);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsMantenimiento.TIPOS_REPUESTOSRow rowtipoRepuesto = dsMantenimiento.TIPOS_REPUESTOS.FindByTREP_CODIGO(tipoRepuesto.Codigo);
                        rowtipoRepuesto.BeginEdit();
                        rowtipoRepuesto.TREP_NOMBRE = txtNombre.Text.Trim();
                        rowtipoRepuesto.TREP_DESCRIPCION = txtDescripcion.Text.Trim();
                        rowtipoRepuesto.EndEdit();
                        dsMantenimiento.TIPOS_REPUESTOS.AcceptChanges();

                        //Avisamos que estuvo todo ok
                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Tipo de repuesto", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Modificación);

                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                    }
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjValidacion(validacion, this.Text);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        #endregion

        #region Servicios
        //Método para evitar la creación de más de una pantalla
        public static frmTipoRepuesto Instancia
        {
            get
            {
                if (_frmTipoRepuesto == null || _frmTipoRepuesto.IsDisposed)
                {
                    _frmTipoRepuesto = new frmTipoRepuesto();
                }
                else
                {
                    _frmTipoRepuesto.BringToFront();
                }
                return _frmTipoRepuesto;
            }
            set
            {
                _frmTipoRepuesto = value;
            }
        }

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsMantenimiento.TIPOS_REPUESTOS.Rows.Count == 0)
                    {
                        hayDatos = false;
                    }
                    else
                    {
                        hayDatos = true;
                    }

                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    txtNombre.Text = String.Empty;
                    txtDescripcion.Text = String.Empty;
                    btnCancelar.Enabled = false;
                    dgvLista.Enabled = true;
                    estadoInterface = estadoUI.inicio;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    btnCancelar.Enabled = true;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    dgvLista.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    break;
                default:
                    break;
            }
        }

        private string Validar()
        {
            string erroresValidacion = string.Empty;

            //Control de los blancos de los textbox
            List<string> datos = new List<string>();
            if (txtNombre.Text == string.Empty)
            {
                datos.Add("Nombre");
                erroresValidacion = erroresValidacion + Entidades.Mensajes.MensajesABM.EscribirValidacion(GyCAP.Entidades.Mensajes.MensajesABM.Validaciones.CompletarDatos, datos);
            }

            //Control de espacios en textbox
            List<string> espacios = new List<string>();
            if (txtNombre.Text.Trim().Length == 0)
            {
                espacios.Add("Nombre");
                erroresValidacion = erroresValidacion + Entidades.Mensajes.MensajesABM.EscribirValidacion(GyCAP.Entidades.Mensajes.MensajesABM.Validaciones.SoloEspacios, espacios);
            }
            return erroresValidacion;
        }

        private void frmtipoRepuesto_Activated(object sender, EventArgs e)
        {
            if (txtNombre.Enabled == true)
            {
                txtNombre.Focus();
            }
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtNombre.SelectAll();
        }

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            txtDescripcion.SelectAll();
        }

        #endregion


    }
}
