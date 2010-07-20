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
    public partial class frmMateriaPrimaPrincipal : Form
    {
        private static frmMateriaPrimaPrincipal _frmMateriaPrimaPrincipal = null;
        private Data.dsMateriaPrima dsMateriaPrimaPrincipal = new GyCAP.Data.dsMateriaPrima();
        private enum estadoUI { inicio, agregar };
        private DataView dvListaMateriaPrimaPrincipal, dvComboMP;

        public frmMateriaPrimaPrincipal()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("MPPR_CODIGO", "Código");
            dgvLista.Columns.Add("MP_CODIGO", "Materia Prima");
            dgvLista.Columns.Add("MPPR_CANTIDAD", "Cantidad");
            dgvLista.Columns.Add("UMED_CODIGO", "Unidad de Medida");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["MPPR_CODIGO"].DataPropertyName = "MPPR_CODIGO";
            dgvLista.Columns["MP_CODIGO"].DataPropertyName = "MP_CODIGO";
            dgvLista.Columns["MPPR_CANTIDAD"].DataPropertyName = "MPPR_CANTIDAD";
            dgvLista.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";

            //Seteo el Dataview de la Lista
            dvListaMateriaPrimaPrincipal = new DataView(dsMateriaPrimaPrincipal.MATERIASPRIMASPRINCIPALES);
            dgvLista.DataSource = dvListaMateriaPrimaPrincipal;

            //Llena el Dataset con las Materias Primas
            BLL.MateriaPrimaBLL.ObtenerTodos(dsMateriaPrimaPrincipal);

            //Lleno el Dataset con las Unidades de Medida
            BLL.UnidadMedidaBLL.ObtenerTodos(dsMateriaPrimaPrincipal);

            //Seteo la propiedad del Incremento de la cantidad
            numCantidad.Increment=Convert.ToDecimal(0.01);
           

            //Combo de Datos
            dvComboMP = new DataView(dsMateriaPrimaPrincipal.MATERIAS_PRIMAS);
            cbMateriaPrima.DataSource = dvComboMP;
            cbMateriaPrima.DisplayMember = "MP_NOMBRE";
            cbMateriaPrima.ValueMember = "MP_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbMateriaPrima.SelectedIndex = -1;
            cbMateriaPrima.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public static frmMateriaPrimaPrincipal Instancia
        {
            get
            {
                if (_frmMateriaPrimaPrincipal == null || _frmMateriaPrimaPrincipal.IsDisposed)
                {
                    _frmMateriaPrimaPrincipal = new frmMateriaPrimaPrincipal();
                }
                else
                {
                    _frmMateriaPrimaPrincipal.BringToFront();
                }
                return _frmMateriaPrimaPrincipal;
            }
            set
            {
                _frmMateriaPrimaPrincipal = value;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void frmMateriaPrimaPrincipal_Activated(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.agregar);
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    //Seteo el inicio de la pantalla sin que se vea el groupbox de agregar
                    gbLista.Height = 300;
                    gbAgregar.Visible = false;
                    dgvLista.Height = 280;
                    btnNuevo.Enabled = true;
                    cbMateriaPrima.Visible = false;
                    //Si hay datos que deje eliminar
                    if (dsMateriaPrimaPrincipal.MATERIASPRIMASPRINCIPALES.Rows.Count != 0)
                    {
                        btnEliminar.Enabled = true;
                    }
                    else btnEliminar.Enabled = false;
                    
                    //Lleno el Dataset con las Materias Primas Principales Cargadas
                    BLL.MateriaPrimaPrincipalBLL.ObtenerTodos(dsMateriaPrimaPrincipal);

                    break;
                case estadoUI.agregar:
                    //Seteo el inicio de la pantalla sin que se vea el groupbox de agregar
                    gbLista.Height = 190;
                    gbAgregar.Visible = true;
                    dgvLista.Height = 154;
                    btnNuevo.Enabled = false;
                    cbMateriaPrima.Visible = true;
                    cbMateriaPrima.SelectedIndex = -1;
                    numCantidad.Value = 0;
                    //Si hay datos que deje eliminar
                    if (dsMateriaPrimaPrincipal.MATERIASPRIMASPRINCIPALES.Rows.Count != 0)
                    {
                        btnEliminar.Enabled = true;
                    }
                    else btnEliminar.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void cbMateriaPrima_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMateriaPrima.Visible == true && cbMateriaPrima.SelectedValue != null )
            {

                int idMateriaPrima = Convert.ToInt32(cbMateriaPrima.SelectedValue);
                int idUnidadMedida = Convert.ToInt32(dsMateriaPrimaPrincipal.MATERIAS_PRIMAS.FindByMP_CODIGO(idMateriaPrima).UMED_CODIGO);
                string unidadMedida = dsMateriaPrimaPrincipal.UNIDADES_MEDIDA.FindByUMED_CODIGO(idUnidadMedida).UMED_ABREVIATURA;
                lblUnidadMedida.Text = unidadMedida;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Pregunto si tiene seleccionado algo
            if (cbMateriaPrima.SelectedIndex != -1)
            {
                //Pregunto si la cantidad es mayor a cero
                if (numCantidad.Value != 0)
                {
                    //Creamos los objetos a insertar
                    Entidades.MateriaPrimaPrincipal mp_prin = new GyCAP.Entidades.MateriaPrimaPrincipal();
                    Entidades.MateriaPrima mp = new GyCAP.Entidades.MateriaPrima();

                    mp_prin.Cantidad = Convert.ToInt32(numCantidad.Value);
                    //Creo el objeto materia prima
                    mp.CodigoMateriaPrima = Convert.ToInt32(cbMateriaPrima.SelectedValue);
                    mp.Nombre = cbMateriaPrima.SelectedText.ToString();
                    
                    //Se lo asigno a la materia prima principal
                    mp_prin.MateriaPrima = mp;
                    mp_prin.Codigo = 0;

                    try
                    {
                        //lo guardamos en la Base de datos
                        mp_prin.Codigo = BLL.MateriaPrimaPrincipalBLL.Insertar(mp_prin);
                        
                        //Lo ponemos en el Dataset
                        Data.dsMateriaPrima.MATERIASPRIMASPRINCIPALESRow rowMP_Prin = dsMateriaPrimaPrincipal.MATERIASPRIMASPRINCIPALES.NewMATERIASPRIMASPRINCIPALESRow();
                        //Indicamos que comienza la edición de la fila
                        rowMP_Prin.BeginEdit();
                        rowMP_Prin.MPPR_CODIGO = mp_prin.Codigo;
                        rowMP_Prin.MP_CODIGO = mp_prin.MateriaPrima.CodigoMateriaPrima;
                        rowMP_Prin.MPPR_CANTIDAD = Convert.ToDecimal(mp_prin.Cantidad);

                        //Termina la edición de la fila
                        rowMP_Prin.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsMateriaPrimaPrincipal.MATERIASPRIMASPRINCIPALES.AddMATERIASPRIMASPRINCIPALESRow(rowMP_Prin);
                        dsMateriaPrimaPrincipal.MATERIASPRIMASPRINCIPALES.AcceptChanges();

                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);  

                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                else
                {
                    MessageBox.Show("La cantidad debe ser mayor a cero (0)", "Información: Selección de Materia Prima", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            else
            {
                MessageBox.Show("Debe seleccionar una materia Prima", "Información: Selección de Materia Prima", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
                string nombre;
                string unidadMedida;
                string cantidad;

                if (e.Value.ToString() != string.Empty)
                {

                    switch (dgvLista.Columns[e.ColumnIndex].Name)
                    {
                        case "MP_CODIGO":
                            nombre = dsMateriaPrimaPrincipal.MATERIAS_PRIMAS.FindByMP_CODIGO(Convert.ToInt32(e.Value)).MP_NOMBRE;
                            e.Value = nombre;
                            break;
                        case "MPPR_CANTIDAD":
                            cantidad = string.Format("{0:###,##}",e.Value);
                            e.Value = cantidad;
                            break;
                        case "UMED_CODIGO":
                            unidadMedida = dsMateriaPrimaPrincipal.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(e.Value)).UMED_NOMBRE;
                            e.Value = unidadMedida;
                            break;
                        default:
                            break;
                    }

                }
        }
        

       
    }

        
}
