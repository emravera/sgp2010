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
    public partial class frmOperacionesFabricacion : Form
    {
        private static frmOperacionesFabricacion _frmOperacionesFabricacion = null;
        private Data.dsOperacionesFabricacion dsOperacionesFabricacion = new GyCAP.Data.dsOperacionesFabricacion();
        private DataView dvListaOperaciones;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, };
        private estadoUI estadoInterface;

        public frmOperacionesFabricacion()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("OPR_NUMERO", "Código");
            dgvLista.Columns.Add("OPR_CODIGO", "Codificación");
            dgvLista.Columns.Add("OPR_NOMBRE", "Nombre");
            dgvLista.Columns.Add("OPR_DESCRIPCION", "Descripción");
            dgvLista.Columns.Add("OPR_HORASREQUERIDAS", "Descripción");

            //Se setean los valores de las columnas 
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["OPR_NUMERO"].DataPropertyName = "OPR_NUMERO";
            dgvLista.Columns["OPR_CODIGO"].DataPropertyName = "OPR_CODIGO";
            dgvLista.Columns["OPR_NOMBRE"].DataPropertyName = "OPR_NOMBRE";
            dgvLista.Columns["OPR_DESCRIPCION"].DataPropertyName = "OPR_DESCRIPCION";
            dgvLista.Columns["OPR_HORASREQUERIDAS"].DataPropertyName = "OPR_HORASREQUERIDAS";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaOperaciones = new DataView(dsOperacionesFabricacion.OPERACIONES);
            dvListaOperaciones.Sort = "OPR_NUMERO ASC";
            dgvLista.DataSource = dvListaOperaciones;

            //Seteamos los maxlenght
            txtNombreBuscar.MaxLength = 80;
            txtCodigoOperacionBuscar.MaxLength = 80;
            txtCodigoOperacion.MaxLength = 80;
            txtNombre.MaxLength = 80;
            txtDescripcion.MaxLength = 300;
    
            //Seteamos la configuracion del numeric UP-down
            numHoras.DecimalPlaces = 2;
            numHoras.Value = 0;

        }
        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
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
            this.Dispose();
        }
        #endregion


        #region Servicios

        //Método para evitar la creación de más de una pantalla
        public static frmOperacionesFabricacion Instancia
        {
            get
            {
                if (_frmOperacionesFabricacion == null || _frmOperacionesFabricacion.IsDisposed)
                {
                    _frmOperacionesFabricacion = new frmOperacionesFabricacion();
                }
                else
                {
                    _frmOperacionesFabricacion.BringToFront();
                }
                return _frmOperacionesFabricacion;
            }
            set
            {
                _frmOperacionesFabricacion = value;
            }
        }

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsOperacionesFabricacion.OPERACIONES.Rows.Count == 0)
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
                    tcMarca.SelectedTab = tpBuscar;

                    //Seteo los controles de búsqueda
                    txtNombreBuscar.Text = string.Empty;
                    txtNombreBuscar.Focus();
                    txtCodigoOperacionBuscar.Text = string.Empty;
                    break;

                case estadoUI.nuevo:
                    estadoInterface = estadoUI.nuevo;
                    tcMarca.SelectedTab = tpDatos;
                    break;
                case estadoUI.nuevoExterno:
                    estadoInterface = estadoUI.nuevoExterno;
                    tcMarca.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:
                    estadoInterface = estadoUI.consultar;
                    tcMarca.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    estadoInterface = estadoUI.modificar;
                    tcMarca.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }
        #endregion

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsOperacionesFabricacion.OPERACIONES.Clear();

                //Llamamos al método de búsqueda de datos
                BLL.OperacionBLL.ObetenerOperaciones(dsOperacionesFabricacion,txtNombreBuscar.Text,txtCodigoOperacionBuscar.Text);
                
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaOperaciones.Table = dsOperacionesFabricacion.OPERACIONES;

                if (dsOperacionesFabricacion.OPERACIONES.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Operaciones de Fabricación con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //Seteamos el estado de la interfaz           
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Operaciones de Fabricación - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

       
       

               
        
    }
}
