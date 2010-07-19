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
    public partial class frmModeloCocina : Form
    {
        private static frmModeloCocina _frmModeloCocina = null;
        private enum estadoUI { inicio, nuevo, consultar, modificar, };
        private estadoUI estadoInterface;
        private Data.dsModeloCocina dsModeloCocina = new GyCAP.Data.dsModeloCocina();
        private DataView dvModeloCocina;
        
        public frmModeloCocina()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("MOD_CODIGO", "Código");
            dgvLista.Columns.Add("MOD_NOMBRE", "Nombre");
            dgvLista.Columns.Add("MOD_DESCRIPCION", "Descripción");
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLista.Columns[2].Resizable = DataGridViewTriState.True;
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["MOD_CODIGO"].DataPropertyName = "MOD_CODIGO";
            dgvLista.Columns["MOD_NOMBRE"].DataPropertyName = "MOD_NOMBRE";
            dgvLista.Columns["MOD_DESCRIPCION"].DataPropertyName = "MOD_DESCRIPCION";
            //Creamos el dataview y lo asignamos a la grilla
            dvModeloCocina = new DataView(dsModeloCocina.MODELOS_COCINAS);
            dgvLista.DataSource = dvModeloCocina;
            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
        }

        public static frmModeloCocina Instancia
        {
            get
            {
                if (_frmModeloCocina == null || _frmModeloCocina.IsDisposed)
                {
                    _frmModeloCocina = new frmModeloCocina();
                }
                else
                {
                    _frmModeloCocina.BringToFront();
                }
                return _frmModeloCocina;
            }
            set
            {
                _frmModeloCocina = value;
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
                dsModeloCocina.Clear();
                BLL.ModeloCocinaBLL.ObtenerTodos(txtNombreBuscar.Text, dsModeloCocina);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvModeloCocina.Table = dsModeloCocina.MODELOS_COCINAS;
                if (dsModeloCocina.MODELOS_COCINAS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron modelos de cocina con el nombre ingresado.", "Aviso");
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
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar el modelo de cocina seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Creamos el objeto
                        int codigo = Convert.ToInt32(dvModeloCocina[dgvLista.SelectedRows[0].Index]["mod_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.ModeloCocinaBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsModeloCocina.MODELOS_COCINAS.FindByMOD_CODIGO(codigo).Delete();
                        dsModeloCocina.MODELOS_COCINAS.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
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
                MessageBox.Show("Debe seleccionar un modelo de cocina de la lista.", "Aviso");
            }
        }

        #endregion

        #region Pestaña Datos

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo
            if (txtNombre.Text != String.Empty)
            {
                Entidades.ModeloCocina modeloCocina = new GyCAP.Entidades.ModeloCocina();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo)
                {
                    //Está cargando un modelo nuevo
                    modeloCocina.Nombre = txtNombre.Text;
                    modeloCocina.Descripcion = txtDescripcion.Text;
                    try
                    {
                        //Primero lo creamos en la db
                        modeloCocina.Codigo = BLL.ModeloCocinaBLL.Insertar(modeloCocina);
                        //Ahora lo agregamos al dataset
                        Data.dsModeloCocina.MODELOS_COCINASRow rowModeloCocina = dsModeloCocina.MODELOS_COCINAS.NewMODELOS_COCINASRow();
                        //Indicamos que comienza la edición de la fila
                        rowModeloCocina.BeginEdit();
                        rowModeloCocina.MOD_CODIGO = modeloCocina.Codigo;
                        rowModeloCocina.MOD_NOMBRE = modeloCocina.Nombre;
                        rowModeloCocina.MOD_DESCRIPCION = modeloCocina.Descripcion;
                        //Termina la edición de la fila
                        rowModeloCocina.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsModeloCocina.MODELOS_COCINAS.AddMODELOS_COCINASRow(rowModeloCocina);
                        dsModeloCocina.AcceptChanges();;
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
                    //Está modificando un modelo
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    modeloCocina.Codigo = Convert.ToInt32(dvModeloCocina[dgvLista.SelectedRows[0].Index]["mod_codigo"]);
                    //Segundo obtenemos los nuevos datos que ingresó el usuario
                    modeloCocina.Nombre = txtNombre.Text;
                    modeloCocina.Descripcion = txtDescripcion.Text;
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.ModeloCocinaBLL.Actualizar(modeloCocina);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsModeloCocina.MODELOS_COCINASRow rowModeloCocina = dsModeloCocina.MODELOS_COCINAS.FindByMOD_CODIGO(modeloCocina.Codigo);
                        rowModeloCocina.BeginEdit();
                        rowModeloCocina.MOD_NOMBRE = txtNombre.Text;
                        rowModeloCocina.MOD_DESCRIPCION = txtDescripcion.Text;
                        rowModeloCocina.EndEdit();
                        dsModeloCocina.MODELOS_COCINAS.AcceptChanges();
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

                    if (dsModeloCocina.MODELOS_COCINAS.Rows.Count == 0)
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
                    estadoInterface = estadoUI.inicio;
                    tcModeloCocina.SelectedTab = tpBuscar;
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    txtDescripcion.Text = String.Empty;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcModeloCocina.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcModeloCocina.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcModeloCocina.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }

        //Método para evitar que se cierrre la pantalla con la X o con ALT+F4
        private void frmModeloCocina_FormClosing(object sender, FormClosingEventArgs e)
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
            int codigoModeloCocina = Convert.ToInt32(dvModeloCocina[e.RowIndex]["mod_codigo"]);
            txtNombre.Text = dsModeloCocina.MODELOS_COCINAS.FindByMOD_CODIGO(codigoModeloCocina).MOD_NOMBRE;
            txtDescripcion.Text = dsModeloCocina.MODELOS_COCINAS.FindByMOD_CODIGO(codigoModeloCocina).MOD_DESCRIPCION;
        }

        //Evento doble clic en la grilla, es igual que si hiciera clic en Consultar
        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        #endregion
 
    }
}
