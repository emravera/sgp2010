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
    public partial class frmColor : Form
    {
        private static frmColor _frmColor = null;
        private Data.dsColor dsColor = new GyCAP.Data.dsColor();
        private DataView dvColor;
        private enum estadoUI {inicio, nuevo, consultar, modificar, };
        private estadoUI estadoInterface;
        
        public frmColor()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("COL_CODIGO", "Código");
            dgvLista.Columns.Add("COL_NOMBRE", "Nombre");
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["COL_CODIGO"].DataPropertyName = "COL_CODIGO";
            dgvLista.Columns["COL_NOMBRE"].DataPropertyName = "COL_NOMBRE";
            //Creamos el dataview y lo asignamos a la grilla
            dvColor = new DataView(dsColor.COLORES);
            dgvLista.DataSource = dvColor;
            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
        }

        //Método para evitar la creación de más de una pantalla
        public static frmColor Instancia
        {
            get
            {
                if (_frmColor == null || _frmColor.IsDisposed)
                {
                    _frmColor = new frmColor();
                }
                else
                {
                    _frmColor.BringToFront();
                }
                return _frmColor;
            }
            set
            {
                _frmColor = value;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        //Usando el comando #region NOMBRE y #endregion, se puede agrupar código relacionado para mostrarlo
        //u ocultarlo y hacer más fácil la lectura

        #region Pestaña Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsColor.COLORES.Clear();
                BLL.ColorBLL.ObtenerTodos(txtNombreBuscar.Text, dsColor);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvColor.Table = dsColor.COLORES;
                if (dsColor.COLORES.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron colores con el nombre ingresado.", "Aviso");
                }
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message);
                SetInterface(estadoUI.inicio);
            }
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar el color seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {                         
                        //Lo eliminamos de la DB
                        int codigo = Convert.ToInt32(dvColor[dgvLista.SelectedRows[0].Index]["col_codigo"]);
                        BLL.ColorBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsColor.COLORES.FindByCOL_CODIGO(codigo).Delete();
                        dsColor.COLORES.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
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
                MessageBox.Show("Debe seleccionar un color de la lista.", "Aviso");
            }
        }

        #endregion

        #region Pestaña Datos

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo
            if (txtNombre.Text != String.Empty)
            {
                Entidades.Color color = new GyCAP.Entidades.Color();
                
                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo)
                {
                    //Está cargando un color nuevo
                    color.Nombre = txtNombre.Text;
                    try
                    {
                        //Primero lo creamos en la db
                        color.Codigo = BLL.ColorBLL.Insertar(color);
                        //Ahora lo agregamos al dataset
                        Data.dsColor.COLORESRow rowColor = dsColor.COLORES.NewCOLORESRow();
                        //Indicamos que comienza la edición de la fila
                        rowColor.BeginEdit();
                        rowColor.COL_CODIGO = color.Codigo;
                        rowColor.COL_NOMBRE = color.Nombre;
                        //Termina la edición de la fila
                        rowColor.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsColor.COLORES.AddCOLORESRow(rowColor);
                        dsColor.COLORES.AcceptChanges();
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
                    //Está modificando un color
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    color.Codigo = Convert.ToInt32(dvColor[dgvLista.SelectedRows[0].Index]["col_codigo"]);
                    //Segundo obtenemos el nuevo nombre que ingresó el usuario
                    color.Nombre = txtNombre.Text;
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.ColorBLL.Actualizar(color);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsColor.COLORESRow rowColor = dsColor.COLORES.FindByCOL_CODIGO(color.Codigo);
                        rowColor.BeginEdit();
                        rowColor.COL_NOMBRE = txtNombre.Text;
                        rowColor.EndEdit();
                        dsColor.COLORES.AcceptChanges();
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        #endregion

        #region Servicios

        //Setea la pantalla de acuerdo al estado en que se encuentre
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsColor.COLORES.Rows.Count == 0)
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
                    estadoInterface = estadoUI.inicio;
                    tcColor.SelectedTab = tpBuscar;                    
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtCodigo.Text = String.Empty;
                    txtNombre.Text = String.Empty;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcColor.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcColor.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcColor.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }
          

        //Método para evitar que se cierrre la pantalla con la X o con ALT+F4
        private void frmColor_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        //Evento RowEnter de la grilla, va cargando los datos en la pestaña Datos a medida que se
        //hace clic en alguna fila de la grilla
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoColor = Convert.ToInt32(dvColor[e.RowIndex]["col_codigo"]);
            txtCodigo.Text = codigoColor.ToString();
            txtNombre.Text = dsColor.COLORES.FindByCOL_CODIGO(codigoColor).COL_NOMBRE;
        }

        //Evento doble clic en la grilla, es igual que si hiciera clic en Consultar
        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        private void txtNombreBuscar_Enter(object sender, EventArgs e)
        {
            txtNombreBuscar.SelectAll();
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtNombre.SelectAll();
        }

        #endregion


    }
}
