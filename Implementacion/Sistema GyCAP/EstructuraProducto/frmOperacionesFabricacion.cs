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
        private enum estadoUI { inicio, nuevo, consultar, modificar, buscar };
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
            dgvLista.Columns.Add("OPR_HORASREQUERIDA", "HS.Req.");

            //Se setean los valores de las columnas 
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["OPR_NUMERO"].DataPropertyName = "OPR_NUMERO";
            dgvLista.Columns["OPR_CODIGO"].DataPropertyName = "OPR_CODIGO";
            dgvLista.Columns["OPR_NOMBRE"].DataPropertyName = "OPR_NOMBRE";
            dgvLista.Columns["OPR_DESCRIPCION"].DataPropertyName = "OPR_DESCRIPCION";
            dgvLista.Columns["OPR_HORASREQUERIDA"].DataPropertyName = "OPR_HORASREQUERIDA";

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

            //Seteamos el estado de la interface
            SetInterface(estadoUI.inicio);

        }
        #region Botones
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
            this.Dispose();
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
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
            bool hayDatos;

            switch (estado)
            {
                case estadoUI.inicio:
            
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
                    txtCodigoOperacionBuscar.Text = string.Empty;
                    txtNombreBuscar.Focus();
                    gbGrillaBuscar.Visible = false;

                    break;

                case estadoUI.buscar:
                   
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
                    gbGrillaBuscar.Visible = true;

                    //Escondo la columna del ID
                    dgvLista.Columns["OPR_NUMERO"].Visible = false;
                    break;
 
                case estadoUI.nuevo:
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    
                    estadoInterface = estadoUI.nuevo;
                    tcMarca.SelectedTab = tpDatos;

                    //inicializo los componentes
                    txtCodigoOperacion.Text = string.Empty;
                    txtNombre.Text = string.Empty;
                    txtDescripcion.Text = string.Empty;
                    numHoras.Value = 0;
                    txtCodigoOperacion.Focus();

                    //Los habilito
                    txtCodigoOperacion.ReadOnly = false;
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    numHoras.Enabled = true;
                    break;

                case estadoUI.consultar:
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = true;
                    
                    estadoInterface = estadoUI.consultar;
                    tcMarca.SelectedTab = tpDatos;

                    //Deshabilito los controles
                    txtCodigoOperacion.ReadOnly = true;
                    txtNombre.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    numHoras.Enabled = false;
                    break;

                case estadoUI.modificar:
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;

                    estadoInterface = estadoUI.modificar;
                    tcMarca.SelectedTab = tpDatos;

                    //Los habilito
                    txtCodigoOperacion.ReadOnly = false;
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    numHoras.Enabled = true;

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
                SetInterface(estadoUI.buscar);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Operaciones de Fabricación - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
               string validacion = ValidarGuardar();

               if (validacion == string.Empty)
               {
                   
                        //Creo el objeto de operaciones
                       Entidades.OperacionFabricacion operacion = new GyCAP.Entidades.OperacionFabricacion();
                       operacion.Codificacion = txtCodigoOperacion.Text;
                       operacion.Nombre = txtNombre.Text;
                       operacion.Descripcion = txtDescripcion.Text;
                       operacion.HorasRequeridas =Convert.ToDecimal(numHoras.Value);

                   //Pregunto si se esta creando una nueva operacion
                   if(estadoInterface==estadoUI.nuevo)
                   {
                       //Lo inserto en la base de datos
                       BLL.OperacionBLL.InsertarOperacion(operacion);
                   }
                   else if (estadoInterface == estadoUI.modificar)
                   {
                       operacion.Codigo = Convert.ToInt32(dvListaOperaciones[dgvLista.SelectedRows[0].Index]["opr_numero"]);
                       BLL.OperacionBLL.ModificarOperacion(operacion);
                   }
                       //Informo que se guardo correctamente
                       MessageBox.Show("Los datos se han almacenado correctamente", "Informacion: Operación de Fabricación - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                       //Vuelvo al estado inicial de la interface
                       SetInterface(estadoUI.inicio);
                                                       
               }
               else
               {
                   MessageBox.Show(validacion, "Error: Operaciones de Fabricación - Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }


            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Operaciones de Fabricación - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private string ValidarGuardar()
        {
            string msjerror = string.Empty;

            //Controlo los textbox
            if (txtCodigoOperacion.Text == string.Empty) msjerror = msjerror + "-El campo Codigo Operación no puede estar vacío\n";
            if (txtDescripcion.Text == string.Empty) msjerror = msjerror + "-El campo Descripción no puede estar vacío\n";
            if (txtNombre.Text == string.Empty) msjerror = msjerror + "-El campo Codigo Nombre no puede estar vacío\n";
            if (numHoras.Value==0) msjerror = msjerror + "-El campo Horas Requeridas no puede estar vacío\n";

            if (msjerror != string.Empty)
            {
                msjerror = "Los errores de validación contrados son:\n" + msjerror;
            }

            return msjerror;
        }

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           decimal cantidad;

            if (e.Value.ToString() != string.Empty)
            {

                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                   case "OPR_HORASREQUERIDA":
                        cantidad = Math.Round(Convert.ToDecimal(e.Value), 2);
                        e.Value = cantidad;
                        break;
                   default:
                        break;
                }

            }
        }

        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
           //Obtengo el codigo de la operacion
            int codigoOperacion = Convert.ToInt32(dvListaOperaciones[e.RowIndex]["opr_numero"]);
                      
           //Seteo los valores a cada control
            txtCodigoOperacion.Text = dsOperacionesFabricacion.OPERACIONES.FindByOPR_NUMERO(codigoOperacion).OPR_CODIGO.ToString();
            txtDescripcion.Text = dsOperacionesFabricacion.OPERACIONES.FindByOPR_NUMERO(codigoOperacion).OPR_DESCRIPCION.ToString();
            txtNombre.Text = dsOperacionesFabricacion.OPERACIONES.FindByOPR_NUMERO(codigoOperacion).OPR_NOMBRE.ToString();
            numHoras.Value =Convert.ToDecimal(dsOperacionesFabricacion.OPERACIONES.FindByOPR_NUMERO(codigoOperacion).OPR_HORASREQUERIDA);
       }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar la Operación de Fabricación seleccionada?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Lo eliminamos de la DB
                        int codigo = Convert.ToInt32(dvListaOperaciones[dgvLista.SelectedRows[0].Index]["opr_numero"]);
                        BLL.OperacionBLL.EliminarOperacion(codigo);
                        
                        //Lo eliminamos del dataset
                        dsOperacionesFabricacion.OPERACIONES.FindByOPR_NUMERO(codigo).Delete();
                        dsOperacionesFabricacion.OPERACIONES.AcceptChanges();
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Eliminacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una operación de Fabricación de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvLista_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnConsultar.PerformClick();
        }
       
       

               
        
    }
}
