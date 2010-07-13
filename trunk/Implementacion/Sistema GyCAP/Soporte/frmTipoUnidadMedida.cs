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
        private enum estadoUI { inicio, nuevo, consultar, modificar, };
        private estadoUI estadoInterface;
        
        public frmTipoUnidadMedida()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("TUMED_CODIGO", "Código");
            dgvLista.Columns.Add("TUMED_NOMBRE", "Nombre");
            
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["TUMED_CODIGO"].DataPropertyName = "TUMED_CODIGO";
            dgvLista.Columns["TUMED_NOMBRE"].DataPropertyName = "TUMED_NOMBRE";
            //Creamos el dataview y lo asignamos a la grilla
            dvTipoUnidadMedida = new DataView(dsUnidadMedida.TIPOS_UNIDADES_MEDIDA);
            dgvLista.DataSource = dvTipoUnidadMedida;
            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
        }

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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        #region Pestaña Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.TipoUnidadMedidaBLL.ObtenerTodos(txtNombreBuscar.Text, dsUnidadMedida);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvTipoUnidadMedida.Table = dsUnidadMedida.TIPOS_UNIDADES_MEDIDA;
                if (dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron tipos de unidad de medida con el nombre ingresado.", "Aviso");
                }
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message);
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar el tipo de unidad de medida seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Creamos el objeto
                        int codigo = Convert.ToInt32(dvTipoUnidadMedida[dgvLista.SelectedRows[0].Index]["tumed_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.TipoUnidadMedidaBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.FindByTUMED_CODIGO(codigo).Delete();
                        dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un tipo de unidad de medida de la lista.", "Aviso");
            }
        }

        #endregion

        #region Pestaña Datos

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo
            if (txtNombre.Text != String.Empty)
            {
                Entidades.TipoUnidadMedida tipoUnidadMedida = new GyCAP.Entidades.TipoUnidadMedida();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo)
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
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message);
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
                        MessageBox.Show("Elemento actualizado correctamente.", "Aviso");
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe completar los datos.", "Aviso");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        #endregion

        #region Servicios

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
                    btnConsultar.Enabled = hayDatos;
                    estadoInterface = estadoUI.inicio;
                    tcTipoUnidadMedida.SelectedTab = tpBuscar;
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtCodigo.Text = String.Empty;
                    txtNombre.Text = String.Empty;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    tcTipoUnidadMedida.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcTipoUnidadMedida.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    gbGuardarCancelar.Enabled = true;
                    estadoInterface = estadoUI.modificar;
                    tcTipoUnidadMedida.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }
               
        //Método para evitar que se cierrre la pantalla con la X o con ALT+F4
        private void frmTipoUnidadMedida_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }

        //Evento RowEnter de la grilla, va cargando los datos en la pestaña Datos a medida que se
        //hace clic en alguna fila de la grilla
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoTipoUnidadMedida = Convert.ToInt32(dvTipoUnidadMedida[e.RowIndex]["tumed_codigo"]);
            txtCodigo.Text = codigoTipoUnidadMedida.ToString();
            txtNombre.Text = dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.FindByTUMED_CODIGO(codigoTipoUnidadMedida).TUMED_NOMBRE;
        }

        //Evento doble clic en la grilla, es igual que si hiciera clic en Consultar
        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        #endregion

    }
}
