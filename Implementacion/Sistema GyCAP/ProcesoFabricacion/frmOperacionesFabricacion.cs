using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Mensajes;

namespace GyCAP.UI.ProcesoFabricacion
{
    public partial class frmOperacionesFabricacion : Form
    {
        private static frmOperacionesFabricacion _frmOperacionesFabricacion = null;
        private Data.dsHojaRuta dsOperacionesFabricacion = new GyCAP.Data.dsHojaRuta();
        private DataView dvListaOperaciones;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, buscar };
        private estadoUI estadoInterface;

        public frmOperacionesFabricacion()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("OPR_CODIGO", "Código");
            dgvLista.Columns.Add("OPR_NOMBRE", "Nombre");
            dgvLista.Columns.Add("OPR_HORASREQUERIDA", "Tiempo (hs)");
            dgvLista.Columns.Add("OPR_DESCRIPCION", "Descripción");            

            //Se setean los valores de las columnas 
            dgvLista.Columns["OPR_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["OPR_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["OPR_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["OPR_HORASREQUERIDA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["OPR_HORASREQUERIDA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["OPR_CODIGO"].MinimumWidth = 100;
            dgvLista.Columns["OPR_NOMBRE"].MinimumWidth = 120;
            dgvLista.Columns["OPR_DESCRIPCION"].MinimumWidth = 160;
            dgvLista.Columns["OPR_HORASREQUERIDA"].MinimumWidth = 100;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
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
                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }
                    tcMarca.SelectedTab = tpBuscar;
                    break;                
                case estadoUI.nuevo:
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnGuardar.Enabled = true;
                    
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
                case estadoUI.nuevoExterno:
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnGuardar.Enabled = true;

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
                    btnGuardar.Enabled = false;
                    
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
                    btnGuardar.Enabled = true;

                    estadoInterface = estadoUI.modificar;
                    tcMarca.SelectedTab = tpDatos;

                    //Los habilito
                    txtCodigoOperacion.ReadOnly = false;
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    numHoras.Enabled = true;
                    txtCodigoOperacion.Focus();
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
                BLL.OperacionBLL.ObetenerOperaciones(dsOperacionesFabricacion, txtNombreBuscar.Text, txtCodigoOperacionBuscar.Text);
                
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaOperaciones.Table = dsOperacionesFabricacion.OPERACIONES;

                if (dsOperacionesFabricacion.OPERACIONES.Rows.Count == 0)
                {
                    MensajesABM.MsjBuscarNoEncontrado("Operaciones de fabricación", this.Text);
                }
                //Seteamos el estado de la interfaz           
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Búsqueda);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {            
           if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
           {               
               //Creo el objeto de operaciones
               Entidades.OperacionFabricacion operacion = new GyCAP.Entidades.OperacionFabricacion();
               operacion.Codificacion = txtCodigoOperacion.Text;
               operacion.Nombre = txtNombre.Text;
               operacion.Descripcion = txtDescripcion.Text;
               operacion.HorasRequeridas = numHoras.Value;
               
               //Pregunto si se esta creando una nueva operacion
               if(estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
               {
                   try
                   {
                       //Lo inserto en la base de datos
                       BLL.OperacionBLL.InsertarOperacion(operacion);
                       Data.dsHojaRuta.OPERACIONESRow rowOP = dsOperacionesFabricacion.OPERACIONES.NewOPERACIONESRow();
                       rowOP.BeginEdit();
                       rowOP.OPR_NUMERO = operacion.Codigo;
                       rowOP.OPR_NOMBRE = operacion.Nombre;
                       rowOP.OPR_CODIGO = operacion.Codificacion;
                       rowOP.OPR_HORASREQUERIDA = operacion.HorasRequeridas;
                       rowOP.OPR_DESCRIPCION = operacion.Descripcion;
                       rowOP.EndEdit();
                       dsOperacionesFabricacion.OPERACIONES.AddOPERACIONESRow(rowOP);
                       dsOperacionesFabricacion.OPERACIONES.AcceptChanges();

                       //Vemos cómo se inició el formulario para determinar la acción a seguir
                       if (estadoInterface == estadoUI.nuevoExterno)
                       {
                           //Nuevo desde acceso directo, cerramos el formulario
                           btnSalir.PerformClick();
                       }
                       else
                       {
                           //Nuevo desde el mismo formulario, volvemos a la pestaña buscar
                           SetInterface(estadoUI.inicio);
                       }
                   }
                   catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                   {
                       MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                   }
                   catch (Entidades.Excepciones.BaseDeDatosException ex)
                   {
                       MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Modificación);
                   }
               }
               else
               {
                   try
                   {
                       operacion.Codigo = Convert.ToInt32(dvListaOperaciones[dgvLista.SelectedRows[0].Index]["opr_numero"]);
                       BLL.OperacionBLL.ModificarOperacion(operacion);
                       Data.dsHojaRuta.OPERACIONESRow rowOP = dsOperacionesFabricacion.OPERACIONES.FindByOPR_NUMERO(operacion.Codigo);
                       rowOP.BeginEdit();
                       rowOP.OPR_NOMBRE = operacion.Nombre;
                       rowOP.OPR_CODIGO = operacion.Codificacion;
                       rowOP.OPR_HORASREQUERIDA = operacion.HorasRequeridas;
                       rowOP.OPR_DESCRIPCION = operacion.Descripcion;
                       rowOP.EndEdit();                       
                       dsOperacionesFabricacion.OPERACIONES.AcceptChanges();

                       MensajesABM.MsjConfirmaGuardar("Operación de fabricación", this.Text, MensajesABM.Operaciones.Modificación);
                   }
                   catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                   {
                       MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                   }
                   catch (Entidades.Excepciones.BaseDeDatosException ex)
                   {
                       MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Modificación);
                   }
               }
               //Vuelvo al estado inicial de la interface
               SetInterface(estadoUI.inicio);
           }
        }        

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string nombre = string.Empty;

            if (!string.IsNullOrEmpty(e.Value.ToString()))
            {
                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                   case "OPR_HORASREQUERIDA":
                        //nombre = Sistema.FuncionesAuxiliares.DecimalHourToString(Convert.ToDecimal(e.Value), "{0}hs:{1}m");
                        //e.Value = nombre;
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
            numHoras.Value = dsOperacionesFabricacion.OPERACIONES.FindByOPR_NUMERO(codigoOperacion).OPR_HORASREQUERIDA;
       }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                if (MensajesABM.MsjConfirmaEliminarDatos("Operación de fabricación", MensajesABM.Generos.Femenino, this.Text) == DialogResult.Yes)
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
                MensajesABM.MsjSinSeleccion("Operación de fabricación", MensajesABM.Generos.Femenino, this.Text);
            }
        }   
        
    }
}
