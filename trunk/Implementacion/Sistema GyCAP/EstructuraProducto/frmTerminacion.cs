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
    public partial class frmTerminacion : Form
    {
        private static frmTerminacion _frmABM = null;
        private Data.dsTerminacion dsTerminacion = new GyCAP.Data.dsTerminacion();
        private DataView dvTerminacion;
        private enum estadoUI { inicio, nuevo, consultar, modificar, };
        private estadoUI estadoInterface;

        public frmTerminacion()
        {
            InitializeComponent();
            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("TE_CODIGO", "Código");
            dgvLista.Columns.Add("TE_NOMBRE", "Nombre");
            dgvLista.Columns.Add("TE_DESCRIPCION", "Descripción");
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["TE_CODIGO"].DataPropertyName = "TE_CODIGO";
            dgvLista.Columns["TE_CODIGO"].Width = 0;
            dgvLista.Columns["TE_NOMBRE"].DataPropertyName = "TE_NOMBRE";
            dgvLista.Columns["TE_NOMBRE"].Width = 40;
            dgvLista.Columns["TE_DESCRIPCION"].DataPropertyName = "TE_DESCRIPCION";
            dgvLista.Columns["TE_DESCRIPCION"].Width = 80;
            //Creamos el dataview y lo asignamos a la grilla
            dvTerminacion = new DataView(dsTerminacion.TERMINACIONES);
            dgvLista.DataSource = dsTerminacion;
            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
        }

        #region Servicios

        //Setea la pantalla de acuerdo al estado en que se encuentre
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsTerminacion.TERMINACIONES.Rows.Count == 0)
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
                    tcABM.SelectedTab = tpBuscar;
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    txtCodigo.Text = String.Empty;
                    txtNombre.Text = String.Empty;
                    txtDescripcion.Text = string.Empty;
                    //gbGuardarCancelar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcABM.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    //gbGuardarCancelar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcABM.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    //gbGuardarCancelar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcABM.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }

        //Método para evitar la creación de más de una pantalla
        public static frmTerminacion Instancia
        {
            get
            {
                if (_frmABM == null || _frmABM.IsDisposed)
                {
                    _frmABM = new frmTerminacion();
                }
                else
                {
                    _frmABM.BringToFront();
                }
                return _frmABM;
            }
            set
            {
                _frmABM = value;
            }
        }

        //Evita que el formulario se cierre desde la cruz
        private void frmTerminacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }

        #endregion

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
            this.Dispose(true);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar la terminación seleccionada?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Creamos el objeto terminacion
                        int codigo = Convert.ToInt32(dvTerminacion[dgvLista.SelectedRows[0].Index]["TE_CODIGO"]);
                        //Lo eliminamos de la DB
                        BLL.TerminacionBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsTerminacion.TERMINACIONES.FindByTE_CODIGO(codigo).Delete();
                        dsTerminacion.TERMINACIONES.AcceptChanges();
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
                MessageBox.Show("Debe seleccionar una terminación de la lista.", "Aviso");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo
            if (txtNombre.Text != String.Empty)
            {
                Entidades.Terminacion terminacion = new GyCAP.Entidades.Terminacion();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo)
                {
                    //Está cargando una terminacion nuevo
                    terminacion.Nombre = txtNombre.Text;
                    terminacion.Descripcion = txtDescripcion.Text;
                    try
                    {
                        //Primero lo creamos en la db
                        terminacion.Codigo = BLL.TerminacionBLL.Insertar(terminacion);
                        //Ahora lo agregamos al dataset
                        Data.dsTerminacion.TERMINACIONESRow rowTerminacion = dsTerminacion.TERMINACIONES.NewTERMINACIONESRow();
                        //Indicamos que comienza la edición de la fila
                        rowTerminacion.BeginEdit();
                        rowTerminacion.TE_CODIGO = terminacion.Codigo;
                        rowTerminacion.TE_NOMBRE = terminacion.Nombre;
                        rowTerminacion.TE_DESCRIPCION = terminacion.Descripcion;
                        //Termina la edición de la fila
                        rowTerminacion.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsTerminacion.TERMINACIONES.AddTERMINACIONESRow(rowTerminacion);
                        dsTerminacion.TERMINACIONES.AcceptChanges();
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
                    //Está modificando una terminacion
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    terminacion.Codigo = Convert.ToInt32(dvTerminacion[dgvLista.SelectedRows[0].Index]["te_codigo"]);
                    //Segundo obtenemos el nuevo nombre que ingresó el usuario
                    terminacion.Nombre = txtNombre.Text;
                    terminacion.Descripcion = txtDescripcion.Text;
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.TerminacionBLL.Actualizar(terminacion);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsTerminacion.TERMINACIONESRow rowTerminacion = dsTerminacion.TERMINACIONES.FindByTE_CODIGO(terminacion.Codigo);
                        rowTerminacion.BeginEdit();
                        rowTerminacion.TE_NOMBRE = txtNombre.Text;
                        rowTerminacion.TE_DESCRIPCION = txtDescripcion.Text;
                        rowTerminacion.EndEdit();
                        dsTerminacion.TERMINACIONES.AcceptChanges();
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dsTerminacion = BLL.TerminacionBLL.ObtenerTodos(txtNombreBuscar.Text);
            //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
            //por una consulta a la BD
            dvTerminacion.Table = dsTerminacion.TERMINACIONES;
            if (dsTerminacion.TERMINACIONES.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron terminaciones con el nombre ingresado.");
            }
            SetInterface(estadoUI.inicio);
        }

    }
}
