﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Mensajes;

namespace GyCAP.UI.EstructuraProducto
{
    public partial class frmDesignacion : Form
    {
        private static frmDesignacion _frmDesignacion = null;
        private Data.dsCocina dsDesignacion = new GyCAP.Data.dsCocina();
        private DataView dvListaDesignacion, dvComboDesignacion, dvComboDesignacionBuscar;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        #region Inicio

        public frmDesignacion()
        {
            InitializeComponent();


            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("MCA_CODIGO", "Marca");
            dgvLista.Columns.Add("DESIG_NOMBRE", "Nombre");
            dgvLista.Columns.Add("DESIG_DESCRIPCION", "Descripcion");
                        
            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;            

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB            
            dgvLista.Columns["MCA_CODIGO"].DataPropertyName = "MCA_CODIGO";
            dgvLista.Columns["DESIG_NOMBRE"].DataPropertyName = "DESIG_NOMBRE";
            dgvLista.Columns["DESIG_DESCRIPCION"].DataPropertyName = "DESIG_DESCRIPCION";
            
            //Llena el Dataset con las marcas
            try
            {
                BLL.MarcaBLL.ObtenerTodos(dsDesignacion.MARCAS);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }
            
            //Creamos el dataview y lo asignamos a la grilla
            dvListaDesignacion = new DataView(dsDesignacion.DESIGNACIONES);
            dvListaDesignacion.Sort = "DESIG_NOMBRE ASC";
            dgvLista.DataSource = dvListaDesignacion;
            
            //CARGA DE COMBOS
            //Creamos el Dataview y se lo asignamos al combo
            dvComboDesignacionBuscar = new DataView(dsDesignacion.MARCAS);
            cbMarcaBuscar.DataSource = dvComboDesignacionBuscar;
            cbMarcaBuscar.DisplayMember = "MCA_NOMBRE";
            cbMarcaBuscar.ValueMember = "MCA_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbMarcaBuscar.SelectedIndex = -1;
            cbMarcaBuscar.DropDownStyle = ComboBoxStyle.DropDownList;

            //Combo de Datos
            dvComboDesignacion = new DataView(dsDesignacion.MARCAS);
            cbMarcaDatos.DataSource = dvComboDesignacion;
            cbMarcaDatos.DisplayMember = "MCA_NOMBRE";
            cbMarcaDatos.ValueMember = "MCA_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbMarcaDatos.SelectedIndex = -1;
            cbMarcaDatos.DropDownStyle = ComboBoxStyle.DropDownList;

            //Seteo el maxlenght de los textbox para que no de error en la bd
            txtDescripcion.MaxLength = 200;
            txtNombre.MaxLength = 80;

            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
           
        }
        //Método para evitar la creación de más de una pantalla
        public static frmDesignacion Instancia
        {
            get
            {
                if (_frmDesignacion == null || _frmDesignacion.IsDisposed)
                {
                    _frmDesignacion = new frmDesignacion();
                }
                else
                {
                    _frmDesignacion.BringToFront();
                }
                return _frmDesignacion;
            }
            set
            {
                _frmDesignacion = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
        }

        #endregion

        #region Pestaña Buscar

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsDesignacion.DESIGNACIONES.Clear();

                //Se llama a la funcion de busqueda con todos los parametros
                BLL.DesignacionBLL.ObtenerTodos(txtNombreBuscar.Text,Convert.ToInt32(cbMarcaBuscar.SelectedValue), dsDesignacion);
                               
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaDesignacion.Table = dsDesignacion.DESIGNACIONES;

                if (dsDesignacion.DESIGNACIONES.Rows.Count == 0)
                {
                    MensajesABM.MsjBuscarNoEncontrado("Designaciones", this.Text);
                }

                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Búsqueda);
                SetInterface(estadoUI.inicio);
            }
        }
        //Metodo para formatear la grilla que cambia las foreign keys por el nombre
        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != String.Empty)
            {
                string nombre;
                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "MCA_CODIGO":
                        nombre = dsDesignacion.MARCAS.FindByMCA_CODIGO(Convert.ToInt32(e.Value)).MCA_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
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
            if (dgvLista.SelectedRows.Count > 0) { SetInterface(estadoUI.consultar); }
            else { MensajesABM.MsjSinSeleccion("Designación", MensajesABM.Generos.Femenino, this.Text); }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0) { SetInterface(estadoUI.modificar); }
            else { MensajesABM.MsjSinSeleccion("Designación", MensajesABM.Generos.Femenino, this.Text); }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0) { dgvLista.SelectedRows[0].Selected = false; }
            SetInterface(estadoUI.inicio);
        }

        #endregion

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsDesignacion.DESIGNACIONES.Rows.Count == 0)
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
                    txtNombreBuscar.Text = string.Empty;
                    cbMarcaBuscar.SelectedIndex = -1;
                    tcDesignacion.SelectedTab = tpBuscar;
                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }
                    txtNombreBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Text=string.Empty;
                    cbMarcaDatos.Enabled = true;
                    cbMarcaDatos.SelectedIndex = -1;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcDesignacion.SelectedTab = tpDatos;
                    cbMarcaDatos.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Text = string.Empty;
                    cbMarcaDatos.Enabled = true;
                    cbMarcaDatos.SelectedIndex = -1;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcDesignacion.SelectedTab = tpDatos;
                    cbMarcaDatos.Focus();
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    cbMarcaDatos.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcDesignacion.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    cbMarcaDatos.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcDesignacion.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }

        //Metodos que seleccionan lo que esta en los textbox
        private void control_Enter(object sender, EventArgs e)
        {
            //if (sender.GetType().Equals(typeof(TextBox))) { (sender as TextBox).SelectAll(); }
        }

        private void dgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        } 

        #endregion

        #region Pestaña Datos

        //Metodo para cargar los datos desde la grilla a los controles
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoDesignacion = Convert.ToInt32(dvListaDesignacion[e.RowIndex]["desig_codigo"]);
            txtNombre.Text = dsDesignacion.DESIGNACIONES.FindByDESIG_CODIGO(codigoDesignacion).DESIG_NOMBRE;
            txtDescripcion.Text = dsDesignacion.DESIGNACIONES.FindByDESIG_CODIGO(codigoDesignacion).DESIG_DESCRIPCION;
            cbMarcaDatos.SelectedValue = dsDesignacion.DESIGNACIONES.FindByDESIG_CODIGO(codigoDesignacion).MCA_CODIGO;
        }

        //Metodo para eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                if (MensajesABM.MsjConfirmaEliminarDatos("Designación", MensajesABM.Generos.Femenino, this.Text) == DialogResult.Yes)
                {
                    try
                    {
                        //Lo eliminamos de la DB
                        int codigo = Convert.ToInt32(dvListaDesignacion[dgvLista.SelectedRows[0].Index]["desig_codigo"]);
                        BLL.DesignacionBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsDesignacion.DESIGNACIONES.FindByDESIG_CODIGO(codigo).Delete();
                        dsDesignacion.DESIGNACIONES.AcceptChanges();
                        btnVolver.PerformClick();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Eliminación);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Designación", MensajesABM.Generos.Femenino, this.Text);
            }
        }

        //Metodo que guarda los datos
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo y selecciono algo en el combo
           if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
            {
                Entidades.Designacion desig = new GyCAP.Entidades.Designacion();
                Entidades.Marca marca = new GyCAP.Entidades.Marca();                

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando una marca nueva
                    desig.Nombre = txtNombre.Text;
                    desig.Descripcion = txtDescripcion.Text;
                    
                    //Creo el objeto Marca y despues lo asigno
                    //Busco el codigo de la marca
                    int idMArca = Convert.ToInt32(cbMarcaDatos.SelectedValue);
                    marca.Codigo = Convert.ToInt32(dsDesignacion.MARCAS.FindByMCA_CODIGO(idMArca).MCA_CODIGO);                    

                    //Asigno la marca creada a la designacion correspondiente
                    desig.Marca = marca;

                    try
                    {
                        //Primero lo creamos en la db
                        desig.Codigo = BLL.DesignacionBLL.Insertar(desig);
                        //Ahora lo agregamos al dataset
                        Data.dsCocina.DESIGNACIONESRow rowDesig = dsDesignacion.DESIGNACIONES.NewDESIGNACIONESRow();
                        //Indicamos que comienza la edición de la fila
                        rowDesig.BeginEdit();
                        rowDesig.DESIG_CODIGO = desig.Codigo;
                        rowDesig.DESIG_NOMBRE = desig.Nombre;
                        rowDesig.MCA_CODIGO = desig.Marca.Codigo;
                        rowDesig.DESIG_DESCRIPCION = desig.Descripcion;
                        //Termina la edición de la fila
                        rowDesig.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsDesignacion.DESIGNACIONES.AddDESIGNACIONESRow(rowDesig);
                        dsDesignacion.DESIGNACIONES.AcceptChanges();
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
                    //Está modificando una designacion
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    desig.Codigo = Convert.ToInt32(dvListaDesignacion[dgvLista.SelectedRows[0].Index]["desig_codigo"]);
                    //Segundo obtenemos los nuevos datos que ingresó el usuario
                    desig.Nombre = txtNombre.Text;
                    desig.Descripcion = txtDescripcion.Text;

                    //Creo el objeto marca
                    int idMArca = Convert.ToInt32(cbMarcaDatos.SelectedValue);
                    marca.Codigo = Convert.ToInt32(dsDesignacion.MARCAS.FindByMCA_CODIGO(idMArca).MCA_CODIGO);                    

                    //Asigno el cliente creado a cliente de la marca
                    desig.Marca = marca;

                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.DesignacionBLL.Actualizar(desig);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsCocina.DESIGNACIONESRow rowDesig = dsDesignacion.DESIGNACIONES.FindByDESIG_CODIGO(desig.Codigo);
                        //Indicamos que comienza la edición de la fila
                        rowDesig.BeginEdit();
                        rowDesig.DESIG_DESCRIPCION = desig.Descripcion;
                        rowDesig.DESIG_NOMBRE = desig.Nombre;
                        rowDesig.MCA_CODIGO = desig.Marca.Codigo;
                        //Termina la edición de la fila
                        rowDesig.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsDesignacion.DESIGNACIONES.AcceptChanges();
                        //Avisamos que estuvo todo ok
                        MensajesABM.MsjConfirmaGuardar("Designación", this.Text, MensajesABM.Operaciones.Modificación);
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
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
               //recarga de la grilla
               dgvLista.Refresh();
            }
        }
        
        #endregion

    }
}