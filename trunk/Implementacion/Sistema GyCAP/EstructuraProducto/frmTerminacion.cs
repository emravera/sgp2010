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
        estadoUI estadoInterface;

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
            dgvLista.Columns["COL_CODIGO"].DataPropertyName = "TE_CODIGO";
            dgvLista.Columns["COL_NOMBRE"].DataPropertyName = "TE_NOMBRE";
            dgvLista.Columns["TE_DESCRIPCION"].DataPropertyName = "TE_DESCRIPCION";
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
                    estadoInterface = estadoUI.inicio;
                    tcABM.SelectedTab = tpBuscar;
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtCodigo.Text = String.Empty;
                    txtNombre.Text = String.Empty;
                    gbGuardarCancelar.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    tcABM.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    gbGuardarCancelar.Enabled = false;
                    estadoInterface = estadoUI.consultar;
                    tcABM.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    gbGuardarCancelar.Enabled = true;
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
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
                        Entidades.Terminacion terminacion = new GyCAP.Entidades.Terminacion();
                        terminacion.Codigo = Convert.ToInt32(dvTerminacion[dgvLista.SelectedRows[0].Index]["TE_CODIGO"]);
                        //terminacion.Nombre = dsTerminacion.TERMINACIONES.FindByTE_CODIGO(terminacion.Codigo).TE_NOMBRE;
                        //terminacion.Descripcion = dsTerminacion.TERMINACIONES.FindByTE_CODIGO(terminacion.Descripcion).TE_DESCRIPCION; 
                        //Lo eliminamos de la DB
                        BLL.TerminacionBLL.Eliminar(terminacion);
                        //Lo eliminamos del dataset
                        dsTerminacion.TERMINACIONES.FindByTE_CODIGO(terminacion.Codigo).Delete();
                        dsTerminacion.TERMINACIONES.AcceptChanges();
                    }
                    catch (BLL.Excepciones.ElementoExistenteException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (BLL.Excepciones.BaseDeDatosException ex)
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


    }
}
