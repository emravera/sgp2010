using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Mensajes;

namespace GyCAP.UI.EstructuraProducto
{
    public partial class frmColor : Form
    {
        private static frmColor _frmColor = null;
        private Data.dsCocina dsColor = new GyCAP.Data.dsCocina();
        private DataView dvColor;
        private enum estadoUI {inicio, modificar, };
        private estadoUI estadoInterface;

                
        public frmColor()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("COL_NOMBRE", "Nombre");
            dgvLista.Columns["COL_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["COL_NOMBRE"].DataPropertyName = "COL_NOMBRE";
            //Traemos todos los colores al principio
            try
            {
                BLL.ColorBLL.ObtenerTodos(dsColor);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }
            //Creamos el dataview y lo asignamos a la grilla
            dvColor = new DataView(dsColor.COLORES);
            dvColor.Sort = "COL_NOMBRE ASC";
            dgvLista.DataSource = dvColor;
            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);            
        }

        //Método para evitar la creación de más de una pantalla
        public static frmColor Instancia
        {
            get
            {
                if (_frmColor == null || _frmColor.IsDisposed)
                {
                    _frmColor = new frmColor();
                }
                else
                {
                    _frmColor.BringToFront();
                }
                return _frmColor;
            }
            set
            {
                _frmColor = value;
            }
        }
        
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        #region Botones Principales

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigo = Convert.ToInt32(dvColor[dgvLista.SelectedRows[0].Index]["col_codigo"]);
                txtNombre.Text = dsColor.COLORES.FindByCOL_CODIGO(codigo).COL_NOMBRE;
                txtNombre.Focus();
                SetInterface(estadoUI.modificar);
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Color", MensajesABM.Generos.Masculino, this.Text);
            }            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                if (MensajesABM.MsjConfirmaEliminarDatos("Color", MensajesABM.Generos.Masculino, this.Text) == DialogResult.Yes)
                {
                    try
                    {                         
                        //Lo eliminamos de la DB
                        int codigo = Convert.ToInt32(dvColor[dgvLista.SelectedRows[0].Index]["col_codigo"]);
                        BLL.ColorBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsColor.COLORES.FindByCOL_CODIGO(codigo).Delete();
                        dsColor.COLORES.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Color", MensajesABM.Generos.Masculino, this.Text);
            }
        }       

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo
            if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
            {
                Entidades.Color color = new GyCAP.Entidades.Color();
                
                //Revisamos que está haciendo
                if (estadoInterface != estadoUI.modificar)
                {
                    //Está cargando un color nuevo
                    color.Nombre = txtNombre.Text;
                    try
                    {
                        //Primero lo creamos en la db
                        color.Codigo = BLL.ColorBLL.Insertar(color);
                        //Ahora lo agregamos al dataset
                        Data.dsCocina.COLORESRow rowColor = dsColor.COLORES.NewCOLORESRow();
                        //Indicamos que comienza la edición de la fila
                        rowColor.BeginEdit();
                        rowColor.COL_CODIGO = color.Codigo;
                        rowColor.COL_NOMBRE = color.Nombre;
                        //Termina la edición de la fila
                        rowColor.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsColor.COLORES.AddCOLORESRow(rowColor);
                        dsColor.COLORES.AcceptChanges();
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                    }
                }
                else
                {
                    //Está modificando un color
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    color.Codigo = Convert.ToInt32(dvColor[dgvLista.SelectedRows[0].Index]["col_codigo"]);
                    //Segundo obtenemos el nuevo nombre que ingresó el usuario
                    color.Nombre = txtNombre.Text;
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.ColorBLL.Actualizar(color);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsCocina.COLORESRow rowColor = dsColor.COLORES.FindByCOL_CODIGO(color.Codigo);
                        rowColor.BeginEdit();
                        rowColor.COL_NOMBRE = txtNombre.Text;
                        rowColor.EndEdit();
                        dsColor.COLORES.AcceptChanges();
                        //Avisamos que estuvo todo ok
                        MensajesABM.MsjConfirmaGuardar("Color", this.Text, MensajesABM.Operaciones.Modificación);
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Modificación);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Modificación);
                    }
                }
                dgvLista.Refresh();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        #endregion

        #region Servicios

        //Setea la pantalla de acuerdo al estado en que se encuentre
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsColor.COLORES.Rows.Count == 0)
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
                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }
                    txtNombre.Focus();
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

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtNombre.SelectAll();
        }        

        #endregion

    }
}
