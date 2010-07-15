﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.EstructuraProducto
{
    public partial class frmDesignacion : Form
    {
        private static frmDesignacion _frmDesignacion = null;
        private Data.dsDesignacion dsDesignacion = new GyCAP.Data.dsDesignacion();
        private DataView dvListaDesignacion, dvComboDesignacion;
        private enum estadoUI { inicio, nuevo, consultar, modificar, };
        private estadoUI estadoInterface;

        #region Inicio
        public frmDesignacion()
        {
            InitializeComponent();


            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("DESIG_CODIGO", "Código");
            dgvLista.Columns.Add("MCA_CODIGO", "Marca");
            dgvLista.Columns.Add("DESIG_NOMBRE", "Nombre");
            dgvLista.Columns.Add("DESIG_DESCRIPCION", "Descripcion");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["DESIG_CODIGO"].DataPropertyName = "DESIG_CODIGO";
            dgvLista.Columns["MCA_CODIGO"].DataPropertyName = "MCA_CODIGO";
            dgvLista.Columns["DESIG_NOMBRE"].DataPropertyName = "DESIG_NOMBRE";
            dgvLista.Columns["DESIG_DESCRIPCION"].DataPropertyName = "DESIG_DESCRIPCION";

            //Llena el Dataset con las marcas
            BLL.MarcaBLL.ObtenerTodos(dsDesignacion);
            //Creamos el dataview y lo asignamos a la grilla
            dvListaDesignacion = new DataView(dsDesignacion.DESIGNACIONES);
            dgvLista.DataSource = dvListaDesignacion;
            
            //CARGA DE COMBOS
            //Creamos el Dataview y se lo asignamos al combo
            dvComboDesignacion = new DataView(dsDesignacion.MARCAS);
            cbMarcaBuscar.DataSource = dvComboDesignacion;
            cbMarcaBuscar.DisplayMember = "MCA_NOMBRE";
            cbMarcaBuscar.ValueMember = "MCA_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbMarcaBuscar.SelectedIndex = -1;
            cbMarcaBuscar.DropDownStyle = ComboBoxStyle.DropDownList;

            //Combo de Datos
            cbMarcaDatos.DataSource = dvComboDesignacion;
            cbMarcaDatos.DisplayMember = "MCA_NOMBRE";
            cbMarcaDatos.ValueMember = "MCA_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbMarcaDatos.SelectedIndex = -1;
            cbMarcaDatos.DropDownStyle = ComboBoxStyle.DropDownList;

            //Selecciono por defecto buscar por nombre
            rbNombre.Checked = true;

            //Seteo el maxlenght de los textbox para que no de error en la bd
            txtDescripcion.MaxLength = 50;
            txtNombre.MaxLength = 30;

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
        #endregion

        #region Pestaña Buscar

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            try
            {
                //Limpiamos el Dataset
                dsDesignacion.DESIGNACIONES.Clear();

                if (rbNombre.Checked == true && txtNombreBuscar.Text != string.Empty)
                {
                    BLL.DesignacionBLL.ObtenerTodos(txtNombreBuscar.Text, dsDesignacion);

                }
                else if (rbMarca.Checked == true && cbMarcaBuscar.SelectedIndex != -1)
                {
                    BLL.DesignacionBLL.ObtenerTodos(Convert.ToInt32(cbMarcaBuscar.SelectedValue), dsDesignacion);
                }
                else
                {
                    BLL.DesignacionBLL.ObtenerTodos(dsDesignacion);
                }

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaDesignacion.Table = dsDesignacion.DESIGNACIONES;

                if (dsDesignacion.DESIGNACIONES.Rows.Count == 0)
                {
                    if (rbNombre.Checked == true)
                    {
                        MessageBox.Show("No se encontraron Designaciones con el nombre ingresado.", "Aviso");
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron Designaciones para la Marca seleccionada.", "Aviso");
                    }

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
            if (e.Value.ToString() != String.Empty)
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
        //Configuracion de los radio Buttons
        private void rbNombre_CheckedChanged(object sender, EventArgs e)
        {
            cbMarcaDatos.SelectedIndex = -1;
            cbMarcaBuscar.Enabled = false;
            txtNombreBuscar.Enabled = true;
        }

        private void rbMarca_CheckedChanged(object sender, EventArgs e)
        {
            txtNombreBuscar.Enabled = false;
            txtNombreBuscar.Text = String.Empty;
            cbMarcaBuscar.Enabled = true;
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
        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
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
                    tcDesignacion.SelectedTab = tpBuscar;
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtCodigo.Text = String.Empty;
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

        //Método para evitar que se cierrre la pantalla con la X o con ALT+F4
        private void frmDesignacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }

        #endregion


        #region Pestaña Datos
        //Metodo para cargar los datos desde la grilla a los controles
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoDesignacion = Convert.ToInt32(dvListaDesignacion[e.RowIndex]["desig_codigo"]);
            txtCodigo.Text = codigoDesignacion.ToString();
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
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar la Designación seleccionada?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
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
                MessageBox.Show("Debe seleccionar una Designacion de la lista.", "Aviso");
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo y selecciono algo en el combo
           if (txtNombre.Text != String.Empty && cbMarcaDatos.SelectedIndex != -1 && txtDescripcion.Text != String.Empty)
            {
                Entidades.Designacion desig = new GyCAP.Entidades.Designacion();
                Entidades.Marca marca = new GyCAP.Entidades.Marca();
                

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo)
                {
                    //Está cargando una marca nueva
                    desig.Nombre = txtNombre.Text;
                    desig.Descripcion = txtDescripcion.Text;
                    
                    //Creo el objeto Marca y despues lo asigno
                    //Busco el codigo de la marca
                    int idMArca = Convert.ToInt32(cbMarcaDatos.SelectedValue);
                    marca.Codigo = Convert.ToInt32(dsDesignacion.MARCAS.FindByMCA_CODIGO(idMArca).MCA_CODIGO);
                    

                    //Asigno la marca creada a la designacion correspondiente
                    desig.Marca=marca;

                    try
                    {
                        //Primero lo creamos en la db
                        desig.Codigo = BLL.DesignacionBLL.Insertar(desig);
                        //Ahora lo agregamos al dataset
                        Data.dsDesignacion.DESIGNACIONESRow rowDesig = dsDesignacion.DESIGNACIONES.NewDESIGNACIONESRow();
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
                        Data.dsDesignacion.DESIGNACIONESRow rowDesig = dsDesignacion.DESIGNACIONES.FindByDESIG_CODIGO(desig.Codigo);
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
        
        #endregion

        

       



















    }
}
