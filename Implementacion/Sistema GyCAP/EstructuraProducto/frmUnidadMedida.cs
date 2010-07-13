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
    public partial class frmUnidadMedida : Form
    {
        private static frmUnidadMedida _frmUnidadMedida = null;
        private Data.dsUnidadMedida dsUnidadMedida = new GyCAP.Data.dsUnidadMedida();
        private DataView dvListaUnidad, dvComboUnidad;
        private enum estadoUI { inicio, nuevo, consultar, modificar, };
        private estadoUI estadoInterface;

        public frmUnidadMedida()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("UMED_CODIGO", "Código");
            dgvLista.Columns.Add("TUMED_CODIGO", "Tipo");
            dgvLista.Columns.Add("UMED_NOMBRE", "Nombre");
            dgvLista.Columns.Add("UMED_ABREVIATURA", "Abreviatura");
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";
            dgvLista.Columns["TUMED_CODIGO"].DataPropertyName = "TUMED_CODIGO";
            dgvLista.Columns["UMED_NOMBRE"].DataPropertyName = "UMED_NOMBRE";
            dgvLista.Columns["UMED_ABREVIATURA"].DataPropertyName = "UMED_ABREVIATURA";
            //Llena el Dataset con Tipo Unidad Medida
            BLL.TipoUnidadMedidaBLL.ObtenerTodos(string.Empty, dsUnidadMedida);            
            //Creamos el dataview y lo asignamos a la grilla
            dvListaUnidad = new DataView(dsUnidadMedida.UNIDADES_MEDIDA);
            dgvLista.DataSource = dvListaUnidad;

            //CARGA DE COMBOS
            //Creamos el Dataview y se lo asignamos al combo
            dvComboUnidad = new DataView(dsUnidadMedida.TIPOS_UNIDADES_MEDIDA);
            cbTipo.DataSource = dvComboUnidad;
            cbTipo.DisplayMember = "TUMED_NOMBRE";
            cbTipo.ValueMember = "TUMED_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbTipo.SelectedIndex = -1;
            cbTipo.DropDownStyle = ComboBoxStyle.DropDownList;

            //Combo de Datos
            cbTipoUnidadDatos.DataSource = dvComboUnidad;
            cbTipoUnidadDatos.DisplayMember = "TUMED_NOMBRE";
            cbTipoUnidadDatos.ValueMember = "TUMED_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbTipoUnidadDatos.SelectedIndex = -1;
            cbTipoUnidadDatos.DropDownStyle = ComboBoxStyle.DropDownList;

            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
        }
        //Método para evitar la creación de más de una pantalla
        public static frmUnidadMedida Instancia
        {
            get
            {
                if (_frmUnidadMedida == null || _frmUnidadMedida.IsDisposed)
                {
                    _frmUnidadMedida = new frmUnidadMedida();
                }
                else
                {
                    _frmUnidadMedida.BringToFront();
                }
                return _frmUnidadMedida;
            }
            set
            {
                _frmUnidadMedida = value;
            }
        }


        private void frmUnidadMedida_Load(object sender, EventArgs e)
        {

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
              //Limpiamos el Dataset
                dsUnidadMedida.UNIDADES_MEDIDA.Clear();

                if (rbNombre.Checked == true && txtNombreBuscar.Text!= string.Empty )
                {
                    BLL.UnidadMedidaBLL.ObtenerTodos(txtNombreBuscar.Text, dsUnidadMedida);
                    
                }
                else if (rbTipo.Checked == true && cbTipo.SelectedIndex != -1)
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
        //Metodo para formatear la grilla que cambia las foreign keys por el nombre
        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;
                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "TUMED_CODIGO":
                        nombre = dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.FindByTUMED_CODIGO(Convert.ToInt32(e.Value)).TUMED_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }

            }

        }
        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTipo.SelectedIndex != -1)
            {
                rbTipo.Checked = true;
                txtNombreBuscar.Text = "";
            }
        }
        private void txtNombreBuscar_TextChanged(object sender, EventArgs e)
        {
            rbNombre.Checked = true;
         }
        #endregion

        #region Pestaña Datos

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

        #endregion


        #region Servicios
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsUnidadMedida.UNIDADES_MEDIDA.Rows.Count == 0)
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
                    tcUnidadMedida.SelectedTab = tpBuscar;
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtCodigo.Text = String.Empty;
                    txtNombre.Text = String.Empty;
                    txtAbreviatura.Text = String.Empty;
                    txtAbreviatura.ReadOnly = false;
                    cbTipoUnidadDatos.Enabled = true;
                    cbTipoUnidadDatos.SelectedIndex = -1;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcUnidadMedida.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    txtAbreviatura.ReadOnly = true;
                    cbTipoUnidadDatos.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcUnidadMedida.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    txtAbreviatura.ReadOnly = false;
                    cbTipoUnidadDatos.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcUnidadMedida.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }
        #endregion

        

        
        

      

       

        

       

        

    }
}

