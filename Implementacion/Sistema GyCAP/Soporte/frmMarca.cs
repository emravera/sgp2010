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
    public partial class frmMarca : Form
    {
        private static frmMarca _frmMarca = null;
        private Data.dsMarca dsMarca = new GyCAP.Data.dsMarca();
        private DataView dvListaMarca, dvComboMarca;
        private enum estadoUI { inicio, nuevo, consultar, modificar, };
        private estadoUI estadoInterface;

#region Inicio
        public frmMarca()
        {
            InitializeComponent();


            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("MCA_CODIGO", "Código");
            dgvLista.Columns.Add("CLI_CODIGO", "Cliente");
            dgvLista.Columns.Add("MCA_NOMBRE", "Nombre");
            
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["MCA_CODIGO"].DataPropertyName = "MCA_CODIGO";
            dgvLista.Columns["CLI_CODIGO"].DataPropertyName = "CLI_CODIGO";
            dgvLista.Columns["MCA_NOMBRE"].DataPropertyName = "MCA_NOMBRE";
            
            //Llena el Dataset con Tipo Unidad Medida
            BLL.TipoUnidadMedidaBLL.ObtenerTodos(dsMarca);
            //Creamos el dataview y lo asignamos a la grilla
            dvListaMarca = new DataView(dsMarca.MARCAS);
            dgvLista.DataSource = dvListaMarca;

            //CARGA DE COMBOS
            //Creamos el Dataview y se lo asignamos al combo
            dvComboMarca = new DataView(dsMarca.CLIENTES);
            cbClienteBuscar.DataSource = dvComboMarca;
            cbClienteBuscar.DisplayMember = "CLI_RAZONSOCIAL";
            cbClienteBuscar.ValueMember = "CLI_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbClienteBuscar.SelectedIndex = -1;
            cbClienteBuscar.DropDownStyle = ComboBoxStyle.DropDownList;

            //Combo de Datos
            cbClienteDatos.DataSource = dvComboMarca;
            cbClienteDatos.DisplayMember = "CLI_RAZONSOCIAL";
            cbClienteDatos.ValueMember = "CLI_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbTipoUnidadDatos.SelectedIndex = -1;
            cbTipoUnidadDatos.DropDownStyle = ComboBoxStyle.DropDownList;

            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);

        }

        //Método para evitar la creación de más de una pantalla
        public static frmMarca Instancia
        {
            get
            {
                if (_frmMarca == null || _frmMarca.IsDisposed)
                {
                    _frmMarca = new frmMarca();
                }
                else
                {
                    _frmMarca.BringToFront();
                }
                return _frmMarca;
            }
            set
            {
                _frmMarca = value;
            }
        }
#endregion

#region Botones
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.modificar);
        }

        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }
#endregion


        #region Pestaña Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsMarca.MARCAS.Clear();

                if (rbNombre.Checked == true && txtNombreBuscar.Text != string.Empty)
                {
                    BLL.MarcaBLL.ObtenerTodos(txtNombreBuscar.Text, dsMarca);

                }
                else if (rbCliente.Checked == true && cbTipo.SelectedIndex != -1)
                {
                    BLL.UnidadMedidaBLL.ObtenerTodos(Convert.ToInt32(cbTipo.SelectedValue), dsUnidadMedida);
                }
                else
                {
                    BLL.UnidadMedidaBLL.ObtenerTodos(dsUnidadMedida);
                }

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaUnidad.Table = dsUnidadMedida.UNIDADES_MEDIDA;

                if (dsUnidadMedida.UNIDADES_MEDIDA.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Unidades de Medida con el nombre ingresado.", "Aviso");
                }
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message);
                SetInterface(estadoUI.inicio);
            }
        }








        #endregion


        #region Servicios
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsMarca.MARCAS.Rows.Count == 0)
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
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtCodigo.Text = String.Empty;
                    txtNombre.Text = String.Empty;
                    cbClienteDatos.Enabled = true;
                    cbClienteDatos.SelectedIndex = -1;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcMarca.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    cbClienteDatos.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcMarca.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    cbClienteDatos.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcMarca.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }

        //Método para evitar que se cierrre la pantalla con la X o con ALT+F4
        private void frmMarca_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }

        }
        
#endregion

       
       

    }
}
