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
    public partial class frmTipoUnidadMedida : Form
    {
        private static frmTipoUnidadMedida _frmTipoUnidadMedida = null;
        private Data.dsUnidadMedida dsUnidadMedida = new GyCAP.Data.dsUnidadMedida();
        private DataView dvTipoUnidadMedida;
        private enum estadoUI { inicio, modificar };
        private estadoUI estadoInterface;

        #region Inicio
        public frmTipoUnidadMedida()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("TUMED_NOMBRE", "Nombre");
            dgvLista.Columns["TUMED_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["TUMED_NOMBRE"].DataPropertyName = "TUMED_NOMBRE";
            //Creamos el dataview y lo asignamos a la grilla
            BLL.TipoUnidadMedidaBLL.ObtenerTodos(dsUnidadMedida);
            dvTipoUnidadMedida = new DataView(dsUnidadMedida.TIPOS_UNIDADES_MEDIDA);
            dvTipoUnidadMedida.Sort = "TUMED_NOMBRE ASC";
            dgvLista.DataSource = dvTipoUnidadMedida;
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
                int codigo = Convert.ToInt32(dvTipoUnidadMedida[dgvLista.SelectedRows[0].Index]["tumed_codigo"]);
                txtNombre.Text = dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.FindByTUMED_CODIGO(codigo).TUMED_NOMBRE;
                txtNombre.Focus();
                SetInterface(estadoUI.modificar);
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Tipo unidad de medida", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
            } 
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjConfirmaEliminarDatos("Tipo unidad de medida", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtenemos
                        int codigo = Convert.ToInt32(dvTipoUnidadMedida[dgvLista.SelectedRows[0].Index]["tumed_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.TipoUnidadMedidaBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.FindByTUMED_CODIGO(codigo).Delete();
                        dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.AcceptChanges();

                        //Mensaje de confirmacion de eliminacion
                        Entidades.Mensajes.MensajesABM.MsjConfirmaEliminar(this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Tipo unidad de medida", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string validacion = Validar();

            //Revisamos que escribió algo
            if (validacion == String.Empty)
            {
                Entidades.TipoUnidadMedida tipoUnidadMedida = new GyCAP.Entidades.TipoUnidadMedida();

                //Revisamos que está haciendo
                if (estadoInterface != estadoUI.modificar)
                {
                    //Está cargando uno nuevo
                    tipoUnidadMedida.Nombre = txtNombre.Text;
                    try
                    {
                        //Primero lo creamos en la db
                        tipoUnidadMedida.Codigo = BLL.TipoUnidadMedidaBLL.Insertar(tipoUnidadMedida);
                        //Ahora lo agregamos al dataset
                        Data.dsUnidadMedida.TIPOS_UNIDADES_MEDIDARow rowTipoUnidadMedida = dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.NewTIPOS_UNIDADES_MEDIDARow();
                        //Indicamos que comienza la edición de la fila
                        rowTipoUnidadMedida.BeginEdit();
                        rowTipoUnidadMedida.TUMED_CODIGO = tipoUnidadMedida.Codigo;
                        rowTipoUnidadMedida.TUMED_NOMBRE = tipoUnidadMedida.Nombre;
                        //Termina la edición de la fila
                        rowTipoUnidadMedida.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.AddTIPOS_UNIDADES_MEDIDARow(rowTipoUnidadMedida);
                        dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.AcceptChanges();

                        //Avisamos que se guardo correctamente
                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Tipo unidad de medida", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);

                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.RejectChanges();
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                    }
                }
                else
                {
                    //Está modificando
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    tipoUnidadMedida.Codigo = Convert.ToInt32(dvTipoUnidadMedida[dgvLista.SelectedRows[0].Index]["tumed_codigo"]);
                    //Segundo obtenemos el nuevo nombre que ingresó el usuario
                    tipoUnidadMedida.Nombre = txtNombre.Text;
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.TipoUnidadMedidaBLL.Actualizar(tipoUnidadMedida);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsUnidadMedida.TIPOS_UNIDADES_MEDIDARow rowTipoUnidadMedida = dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.FindByTUMED_CODIGO(tipoUnidadMedida.Codigo);
                        rowTipoUnidadMedida.BeginEdit();
                        rowTipoUnidadMedida.TUMED_NOMBRE = txtNombre.Text;
                        rowTipoUnidadMedida.EndEdit();
                        dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.AcceptChanges();
                        
                        //Avisamos que estuvo todo ok
                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Tipo unidad de medida", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Modificación);
                        
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
        public static frmTipoUnidadMedida Instancia
        {
            get
            {
                if (_frmTipoUnidadMedida == null || _frmTipoUnidadMedida.IsDisposed)
                {
                    _frmTipoUnidadMedida = new frmTipoUnidadMedida();
                }
                else
                {
                    _frmTipoUnidadMedida.BringToFront();
                }
                return _frmTipoUnidadMedida;
            }
            set
            {
                _frmTipoUnidadMedida = value;
            }
        }

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.Rows.Count == 0)
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
                    btnCancelar.Enabled = false;
                    dgvLista.Enabled = true;
                    estadoInterface = estadoUI.inicio;
                    break;                
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
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

        private void frmTipoUnidadMedida_Activated(object sender, EventArgs e)
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

        #endregion



    }
}
