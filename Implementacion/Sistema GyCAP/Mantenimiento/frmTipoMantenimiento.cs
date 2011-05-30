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
    public partial class frmTipoMantenimiento : Form
    {
        private static frmTipoMantenimiento _frmTipoMantenimiento = null;
        private Data.dsMantenimiento dsMantenimiento = new GyCAP.Data.dsMantenimiento();
        private DataView dvTipoMantenimiento;
        private enum estadoUI { inicio, modificar };
        private estadoUI estadoInterface;

        #region Inicio

        public frmTipoMantenimiento()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            
            //Agregamos las columnas
            dgvLista.Columns.Add("TMAN_NOMBRE", "Nombre");
            dgvLista.Columns.Add("TMAN_DESCRIPCION", "Descripción");
            dgvLista.Columns["TMAN_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLista.Columns["TMAN_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLista.Columns["TMAN_NOMBRE"].Width = 200;
            dgvLista.Columns["TMAN_DESCRIPCION"].Width= 245;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["TMAN_NOMBRE"].DataPropertyName = "TMAN_NOMBRE";
            dgvLista.Columns["TMAN_DESCRIPCION"].DataPropertyName = "TMAN_DESCRIPCION";
            
            //Creamos el dataview y lo asignamos a la grilla
            BLL.TipoMantenimientoBLL.ObtenerTodos(dsMantenimiento);
            dvTipoMantenimiento = new DataView(dsMantenimiento.TIPOS_MANTENIMIENTOS);
            dvTipoMantenimiento.Sort = "TMAN_NOMBRE ASC";
            dgvLista.DataSource = dvTipoMantenimiento;

            //Seteamos el estado de la interfaz

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
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
                int codigo = Convert.ToInt32(dvTipoMantenimiento[dgvLista.SelectedRows[0].Index]["TMAN_CODIGO"]);
                txtNombre.Text = dsMantenimiento.TIPOS_MANTENIMIENTOS.FindByTMAN_CODIGO(codigo).TMAN_NOMBRE;
                txtDescripcion.Text = dsMantenimiento.TIPOS_MANTENIMIENTOS.FindByTMAN_CODIGO(codigo).TMAN_DESCRIPCION ;
                txtNombre.Focus();
                SetInterface(estadoUI.modificar);
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Tipo de mantenimiento", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjConfirmaEliminarDatos("Tipo de mantenimiento", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtenemos
                        int codigo = Convert.ToInt32(dvTipoMantenimiento[dgvLista.SelectedRows[0].Index]["TMAN_CODIGO"]);
                        //Lo eliminamos de la DB
                        BLL.TipoMantenimientoBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsMantenimiento.TIPOS_MANTENIMIENTOS.FindByTMAN_CODIGO(codigo).Delete();
                        dsMantenimiento.TIPOS_MANTENIMIENTOS.AcceptChanges();

                        //Mensaje de confirmacion de eliminacion
                        Entidades.Mensajes.MensajesABM.MsjConfirmaEliminar(this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        dsMantenimiento.TIPOS_MANTENIMIENTOS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsMantenimiento.TIPOS_MANTENIMIENTOS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Tipo de mantenimiento", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo
            if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
            {
                Entidades.TipoMantenimiento tipoMantenimiento = new GyCAP.Entidades.TipoMantenimiento();

                //Revisamos que está haciendo
                if (estadoInterface != estadoUI.modificar)
                {
                    //Está cargando uno nuevo
                    tipoMantenimiento.Nombre = txtNombre.Text.Trim();
                    tipoMantenimiento.Descripcion = txtDescripcion.Text.Trim();
                    try
                    {
                        //Primero lo creamos en la db
                        tipoMantenimiento.Codigo = BLL.TipoMantenimientoBLL.Insertar(tipoMantenimiento);
                        //Ahora lo agregamos al dataset
                        Data.dsMantenimiento.TIPOS_MANTENIMIENTOSRow rowTipoMantenimiento = dsMantenimiento.TIPOS_MANTENIMIENTOS.NewTIPOS_MANTENIMIENTOSRow();
                        //Indicamos que comienza la edición de la fila
                        rowTipoMantenimiento.BeginEdit();
                        rowTipoMantenimiento.TMAN_CODIGO = tipoMantenimiento.Codigo;
                        rowTipoMantenimiento.TMAN_NOMBRE = tipoMantenimiento.Nombre;
                        rowTipoMantenimiento.TMAN_DESCRIPCION  = tipoMantenimiento.Descripcion ;
                        //Termina la edición de la fila
                        rowTipoMantenimiento.EndEdit();
                        
                        //Avisamos que se guardo correctamente
                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Tipo de mantenimiento", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                        
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsMantenimiento.TIPOS_MANTENIMIENTOS.AddTIPOS_MANTENIMIENTOSRow(rowTipoMantenimiento);
                        dsMantenimiento.TIPOS_MANTENIMIENTOS.AcceptChanges();

                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        dsMantenimiento.TIPOS_MANTENIMIENTOS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsMantenimiento.TIPOS_MANTENIMIENTOS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                    }
                }
                else
                {
                    //Está modificando
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    tipoMantenimiento.Codigo = Convert.ToInt32(dvTipoMantenimiento[dgvLista.SelectedRows[0].Index]["TMAN_CODIGO"]);
                    //Segundo obtenemos el nuevo nombre que ingresó el usuario
                    tipoMantenimiento.Nombre = txtNombre.Text.Trim();
                    tipoMantenimiento.Descripcion= txtDescripcion.Text.Trim();
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.TipoMantenimientoBLL.Actualizar(tipoMantenimiento);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsMantenimiento.TIPOS_MANTENIMIENTOSRow rowtipoMantenimiento = dsMantenimiento.TIPOS_MANTENIMIENTOS.FindByTMAN_CODIGO(tipoMantenimiento.Codigo);
                        rowtipoMantenimiento.BeginEdit();
                        rowtipoMantenimiento.TMAN_NOMBRE = txtNombre.Text.Trim();
                        rowtipoMantenimiento.TMAN_DESCRIPCION = txtDescripcion.Text.Trim();
                        rowtipoMantenimiento.EndEdit();
                        dsMantenimiento.TIPOS_MANTENIMIENTOS.AcceptChanges();

                        //Avisamos que estuvo todo ok
                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Tipo de mantenimiento", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Modificación);

                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        dsMantenimiento.TIPOS_MANTENIMIENTOS.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            setBotones();
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            setBotones();
        }

        #endregion

        #region Servicios
        //Método para evitar la creación de más de una pantalla
        public static frmTipoMantenimiento Instancia
        {
            get
            {
                if (_frmTipoMantenimiento == null || _frmTipoMantenimiento.IsDisposed)
                {
                    _frmTipoMantenimiento = new frmTipoMantenimiento();
                }
                else
                {
                    _frmTipoMantenimiento.BringToFront();
                }
                return _frmTipoMantenimiento;
            }
            set
            {
                _frmTipoMantenimiento = value;
            }
        }

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsMantenimiento.TIPOS_MANTENIMIENTOS.Rows.Count == 0)
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
                    txtNombre.Text = String.Empty;
                    txtDescripcion.Text = String.Empty;
                    dgvLista.Enabled = true;
                    estadoInterface = estadoUI.inicio;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    dgvLista.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    break;
                default:
                    break;
            }
        }

        private void frmTipoMantenimiento_Activated(object sender, EventArgs e)
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

        private void setBotones() 
        {
            btnCancelar.Enabled=false;
            btnGuardar.Enabled = false;

            if ((txtNombre.Text != string.Empty ) || (txtDescripcion.Text != string.Empty ))
            {
                btnCancelar.Enabled=true;
                btnGuardar.Enabled = true;
            } 
        }

        #endregion

    }
}
