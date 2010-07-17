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
        private DataView dvListaUnidad, dvComboUnidad, dvComboBuscarUnidad;
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

            //Setemaos las columnas
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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
            dvComboBuscarUnidad = new DataView(dsUnidadMedida.TIPOS_UNIDADES_MEDIDA);
            cbTipo.DataSource = dvComboBuscarUnidad;
            cbTipoUnidadDatos.DisplayMember = "TUMED_NOMBRE";
            cbTipoUnidadDatos.ValueMember = "TUMED_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cbTipoUnidadDatos.SelectedIndex = -1;
            cbTipoUnidadDatos.DropDownStyle = ComboBoxStyle.DropDownList;

            //Se setean las caracteristicas de los textbox
            txtNombre.MaxLength = 80;
            txtAbreviatura.MaxLength = 10;


            //Seteo busqueda de nombre por defecto
            rbNombre.Checked = true;

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
        private void rbNombre_CheckedChanged_1(object sender, EventArgs e)
        {
            txtNombreBuscar.Enabled = true;
            cbTipo.Enabled = false;
            cbTipo.SelectedIndex = -1;
        }

        private void rbTipo_CheckedChanged_1(object sender, EventArgs e)
        {
            txtNombreBuscar.Text = string.Empty;
            txtNombreBuscar.Enabled = false;
            cbTipo.Enabled = true;
        }

        #endregion

        #region Pestaña Datos
        //Programacion de Cada uno de los botones
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar la Unidad de Medida seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Lo eliminamos de la DB
                        int codigo = Convert.ToInt32(dvListaUnidad[dgvLista.SelectedRows[0].Index]["umed_codigo"]);
                        BLL.UnidadMedidaBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsUnidadMedida.UNIDADES_MEDIDA.FindByUMED_CODIGO(codigo).Delete();
                        dsUnidadMedida.UNIDADES_MEDIDA.AcceptChanges();
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
                MessageBox.Show("Debe seleccionar una Unidad de Medida de la lista.", "Aviso");
            }
        }
        //Metodo que carga los datos desde la grilla hacia a los controles 
        private void dgvLista_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            int codigoUnidad = Convert.ToInt32(dvListaUnidad[e.RowIndex]["umed_codigo"]);
            txtCodigo.Text = codigoUnidad.ToString();
            txtNombre.Text = dsUnidadMedida.UNIDADES_MEDIDA.FindByUMED_CODIGO(codigoUnidad).UMED_NOMBRE;
            txtAbreviatura.Text = dsUnidadMedida.UNIDADES_MEDIDA.FindByUMED_CODIGO(codigoUnidad).UMED_ABREVIATURA;
            cbTipoUnidadDatos.SelectedValue = dsUnidadMedida.UNIDADES_MEDIDA.FindByUMED_CODIGO(codigoUnidad).TUMED_CODIGO;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo y selecciono algo en el combo
            if (txtNombre.Text != String.Empty && txtAbreviatura.Text !=String.Empty && cbTipoUnidadDatos.SelectedIndex != -1)
            {
                Entidades.UnidadMedida unidadMedida = new GyCAP.Entidades.UnidadMedida();
                Entidades.TipoUnidadMedida tipoUnidad = new GyCAP.Entidades.TipoUnidadMedida();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo)
                {
                    //Está cargando una unidad de medida nueva
                    unidadMedida.Nombre = txtNombre.Text;
                    unidadMedida.Abreviatura = txtAbreviatura.Text;
                    //Creo el objeto tipo unidad de medida y despues lo asigno
                    tipoUnidad.Codigo = Convert.ToInt32(cbTipoUnidadDatos.SelectedValue);
                    tipoUnidad.Nombre = cbTipoUnidadDatos.SelectedText.ToString();
                    unidadMedida.Tipo = tipoUnidad;
                    try
                    {
                        //Primero lo creamos en la db
                        unidadMedida.Codigo = BLL.UnidadMedidaBLL.Insertar(unidadMedida);
                        //Ahora lo agregamos al dataset
                        Data.dsUnidadMedida.UNIDADES_MEDIDARow rowUnidadMedida = dsUnidadMedida.UNIDADES_MEDIDA.NewUNIDADES_MEDIDARow();
                        //Indicamos que comienza la edición de la fila
                        rowUnidadMedida.BeginEdit();
                        rowUnidadMedida.UMED_CODIGO = unidadMedida.Codigo;
                        rowUnidadMedida.UMED_NOMBRE = unidadMedida.Nombre;
                        rowUnidadMedida.UMED_ABREVIATURA = unidadMedida.Abreviatura;
                        rowUnidadMedida.TUMED_CODIGO = unidadMedida.Tipo.Codigo;
                        //Termina la edición de la fila
                        rowUnidadMedida.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsUnidadMedida.UNIDADES_MEDIDA.AddUNIDADES_MEDIDARow(rowUnidadMedida);
                        dsUnidadMedida.UNIDADES_MEDIDA.AcceptChanges();
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
                    //Está modificando una unidad de medida
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    unidadMedida.Codigo = Convert.ToInt32(dvListaUnidad[dgvLista.SelectedRows[0].Index]["umed_codigo"]);
                    //Segundo obtenemos los nuevos datos que ingresó el usuario
                    unidadMedida.Nombre = txtNombre.Text;
                    unidadMedida.Abreviatura = txtAbreviatura.Text;
                    tipoUnidad.Codigo = Convert.ToInt32(cbTipoUnidadDatos.SelectedValue);
                    tipoUnidad.Nombre = cbTipoUnidadDatos.SelectedText.ToString();
                    unidadMedida.Tipo = tipoUnidad;

                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.UnidadMedidaBLL.Actualizar(unidadMedida);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsUnidadMedida.UNIDADES_MEDIDARow rowUnidadMedida = dsUnidadMedida.UNIDADES_MEDIDA.FindByUMED_CODIGO(unidadMedida.Codigo);
                        //Indicamos que comienza la edición de la fila
                        rowUnidadMedida.BeginEdit();
                        rowUnidadMedida.UMED_NOMBRE = unidadMedida.Nombre;
                        rowUnidadMedida.UMED_ABREVIATURA = unidadMedida.Abreviatura;
                        rowUnidadMedida.TUMED_CODIGO = unidadMedida.Tipo.Codigo;
                        //Termina la edición de la fila
                        rowUnidadMedida.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsUnidadMedida.UNIDADES_MEDIDA.AcceptChanges();
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

