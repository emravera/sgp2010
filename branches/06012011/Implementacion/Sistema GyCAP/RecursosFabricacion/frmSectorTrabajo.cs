using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Mensajes;

namespace GyCAP.UI.RecursosFabricacion
{
    public partial class frmSectorTrabajo : Form
    {
        private static frmSectorTrabajo _frmSectorTrabajo = null;
        private Data.dsHojaRuta dsSectorTrabajo = new GyCAP.Data.dsHojaRuta();
        private DataView dvListaSector;//, dvComboSector;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        #region Inicio

        public frmSectorTrabajo()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("SEC_CODIGO", "Código");
            dgvLista.Columns.Add("SEC_NOMBRE", "Nombre");
            dgvLista.Columns.Add("SEC_ABREVIATURA", "Abreviatura");
            dgvLista.Columns.Add("SEC_DESCRIPCION", "Descripción");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvLista.Columns["SEC_CODIGO"].Visible = false;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["SEC_CODIGO"].DataPropertyName = "SEC_CODIGO";
            dgvLista.Columns["SEC_ABREVIATURA"].DataPropertyName = "SEC_ABREVIATURA";
            dgvLista.Columns["SEC_NOMBRE"].DataPropertyName = "SEC_NOMBRE";
            dgvLista.Columns["SEC_DESCRIPCION"].DataPropertyName = "SEC_DESCRIPCION";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaSector = new DataView(dsSectorTrabajo.SECTORES);
            dvListaSector.Sort = "SEC_NOMBRE ASC";
            dgvLista.DataSource = dvListaSector;

            //Seteo el maxlenght de los textbox para que no de error en la bd
            txtDescripcion.MaxLength = 80;
            txtNombre.MaxLength = 30;
            txtAbreviatura.MaxLength = 10;

            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
        }

        //Método para evitar la creación de más de una pantalla
        public static frmSectorTrabajo Instancia
        {
            get
            {
                if (_frmSectorTrabajo == null || _frmSectorTrabajo.IsDisposed)
                {
                    _frmSectorTrabajo = new frmSectorTrabajo();
                }
                else
                {
                    _frmSectorTrabajo.BringToFront();
                }
                return _frmSectorTrabajo;
            }
            set
            {
                _frmSectorTrabajo = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.modificar);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }
        #endregion

        #region Pestaña Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsSectorTrabajo.SECTORES.Clear();

                //Función de búsqueda
                BLL.SectorBLL.ObtenerTodos(txtNombreBuscar.Text, txtAbreviatura.Text, dsSectorTrabajo);

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaSector.Table = dsSectorTrabajo.SECTORES;

                if (dsSectorTrabajo.SECTORES.Rows.Count == 0)
                {
                    MensajesABM.MsjBuscarNoEncontrado("Sectores de trabajo", this.Text);
                }

                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Búsqueda);
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

                    if (dsSectorTrabajo.SECTORES.Rows.Count == 0)
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
                    tcSectorTrabajo.SelectedTab = tpBuscar;
                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }
                    txtNombreBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    txtAbreviatura.Text = string.Empty;
                    txtAbreviatura.ReadOnly = false;                    
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Text = string.Empty;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcSectorTrabajo.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    txtAbreviatura.Text = string.Empty;
                    txtAbreviatura.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Text = string.Empty;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcSectorTrabajo.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    txtAbreviatura.ReadOnly = true;
                    btnGuardar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcSectorTrabajo.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    txtAbreviatura.ReadOnly = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcSectorTrabajo.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                default:
                    break;
            }
        }
        //Metodos para seleccionar todo lo que contienen los textbox
        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(typeof(TextBox))) { (sender as TextBox).SelectAll(); }
        }        
      
        #endregion

        #region Pestaña Datos

        //Metodo para cargar datos desde la grilla a los controles
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoSector = Convert.ToInt32(dvListaSector[e.RowIndex]["sec_codigo"]);
            txtNombre.Text = dsSectorTrabajo.SECTORES.FindBySEC_CODIGO(codigoSector).SEC_NOMBRE;
            txtDescripcion.Text = dsSectorTrabajo.SECTORES.FindBySEC_CODIGO(codigoSector).SEC_DESCRIPCION;
            txtAbreviatura.Text = dsSectorTrabajo.SECTORES.FindBySEC_CODIGO(codigoSector).SEC_ABREVIATURA;
        }

        //Metodo para la eliminacion
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                if (MensajesABM.MsjConfirmaEliminarDatos("Sector de trabajo", MensajesABM.Generos.Masculino, this.Text) == DialogResult.Yes)
                {
                    try
                    {
                        //Lo eliminamos de la DB
                        int codigo = Convert.ToInt32(dvListaSector[dgvLista.SelectedRows[0].Index]["sec_codigo"]);
                        BLL.SectorBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsSectorTrabajo.SECTORES.FindBySEC_CODIGO(codigo).Delete();
                        dsSectorTrabajo.SECTORES.AcceptChanges();
                        btnVolver.PerformClick();
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
                MensajesABM.MsjSinSeleccion("Sector de trabajo", MensajesABM.Generos.Masculino, this.Text);
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo y selecciono algo en el combo
            if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
            {
                Entidades.SectorTrabajo sector = new GyCAP.Entidades.SectorTrabajo();
               
                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando un sector nuevo
                    sector.Nombre = txtNombre.Text;
                    sector.Descripcion = txtDescripcion.Text;
                    sector.Abreviatura = txtAbreviatura.Text;

                    try
                    {
                        //Primero lo creamos en la db
                        sector.Codigo = BLL.SectorBLL.Insertar(sector);
                        //Ahora lo agregamos al dataset
                        Data.dsHojaRuta.SECTORESRow rowSector = dsSectorTrabajo.SECTORES.NewSECTORESRow();
                        //Indicamos que comienza la edición de la fila
                        rowSector.BeginEdit();
                        rowSector.SEC_CODIGO = sector.Codigo;
                        rowSector.SEC_NOMBRE = sector.Nombre;
                        rowSector.SEC_ABREVIATURA = sector.Abreviatura;
                        rowSector.SEC_DESCRIPCION = sector.Descripcion;
                        //Termina la edición de la fila
                        rowSector.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsSectorTrabajo.SECTORES.AddSECTORESRow(rowSector);
                        dsSectorTrabajo.SECTORES.AcceptChanges();
                        //Y por último seteamos el estado de la interfaz

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
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                    }
                }
                else
                {
                    //Está modificando un sector de trabajo
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    sector.Codigo = Convert.ToInt32(dvListaSector[dgvLista.SelectedRows[0].Index]["sec_codigo"]);
                    
                    //Segundo obtenemos los nuevos datos que ingresó el usuario
                    sector.Nombre = txtNombre.Text;
                    sector.Descripcion = txtDescripcion.Text;
                    sector.Abreviatura = txtAbreviatura.Text;

                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.SectorBLL.Actualizar(sector);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsHojaRuta.SECTORESRow rowSector = dsSectorTrabajo.SECTORES.FindBySEC_CODIGO(sector.Codigo);
                        //Indicamos que comienza la edición de la fila
                        rowSector.BeginEdit();
                        rowSector.SEC_NOMBRE = sector.Nombre;
                        rowSector.SEC_ABREVIATURA = sector.Abreviatura;
                        rowSector.SEC_DESCRIPCION = sector.Descripcion;
                        //Termina la edición de la fila
                        rowSector.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsSectorTrabajo.SECTORES.AcceptChanges();
                        //Avisamos que estuvo todo ok
                        MensajesABM.MsjConfirmaGuardar("Sector de trabajo", this.Text, MensajesABM.Operaciones.Modificación);
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Modificación);
                    }
                }
            }
        }

        #endregion

    }
}
