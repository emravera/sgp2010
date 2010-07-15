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
    public partial class frmSectorTrabajo : Form
    {
        private static frmSectorTrabajo _frmSectorTrabajo = null;
        private Data.dsSectorTrabajo dsSectorTrabajo = new GyCAP.Data.dsSectorTrabajo();
        private DataView dvListaSector, dvComboSector;
        private enum estadoUI { inicio, nuevo, consultar, modificar, };
        private estadoUI estadoInterface;

        #region Inicio
        public frmSectorTrabajo()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("SEC_CODIGO", "Código");
            dgvLista.Columns.Add("SEC_NOMBRE", "Nombre");
            dgvLista.Columns.Add("SEC_DESCRIPCION", "Descripcion");
            dgvLista.Columns.Add("SEC_ABREVIATURA", "Abreviatura");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["SEC_CODIGO"].DataPropertyName = "SEC_CODIGO";
            dgvLista.Columns["SEC_ABREVIATURA"].DataPropertyName = "SEC_ABREVIATURA";
            dgvLista.Columns["SEC_NOMBRE"].DataPropertyName = "SEC_NOMBRE";
            dgvLista.Columns["SEC_DESCRIPCION"].DataPropertyName = "SEC_DESCRIPCION";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaSector = new DataView(dsSectorTrabajo.SECTORES);
            dgvLista.DataSource = dvListaSector;

            //Selecciono por defecto buscar por nombre
            rbNombre.Checked = true;

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

        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
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

                if (rbNombre.Checked == true && txtNombreBuscar.Text != string.Empty)
                {
                    BLL.SectorBLL.ObtenerTodos(txtNombreBuscar.Text, dsSectorTrabajo, true);

                }
                else if (rbAbreviatura.Checked == true && txtAbreviaturaBuscar.Text!= string.Empty)
                {
                    BLL.SectorBLL.ObtenerTodos(txtAbreviaturaBuscar.Text, dsSectorTrabajo, false);
                }
                else
                {
                    BLL.SectorBLL.ObtenerTodos(dsSectorTrabajo);
                }

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaSector.Table = dsSectorTrabajo.SECTORES;

                if (dsSectorTrabajo.SECTORES.Rows.Count == 0)
                {
                    if (rbNombre.Checked == true)
                    {
                        MessageBox.Show("No se encontraron Sectores con el nombre ingresado.", "Aviso");
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron Sectores para la Abreviatura ingresada.", "Aviso");
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
        //Configuracion de los Radiobuttons
        private void rbNombre_CheckedChanged(object sender, EventArgs e)
        {
            txtAbreviaturaBuscar.Enabled = false;
            txtNombreBuscar.Enabled = true;
        }

        private void rbAbreviatura_CheckedChanged(object sender, EventArgs e)
        {
            txtAbreviaturaBuscar.Enabled = true;
            txtNombreBuscar.Enabled = false;
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
                    break;
                case estadoUI.nuevo:
                    txtCodigo.Text = String.Empty;
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
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    txtAbreviatura.ReadOnly = true;
                    txtCodigo.ReadOnly = true;
                    btnGuardar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcSectorTrabajo.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtCodigo.ReadOnly = true;
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
                    break;
                default:
                    break;
            }
        }

        //Método para evitar que se cierrre la pantalla con la X o con ALT+F4
        private void frmSectorTrabajo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region Pestaña Datos

        //Metodo para cargar datos desde la grilla a los controles
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoSector = Convert.ToInt32(dvListaSector[e.RowIndex]["sec_codigo"]);
            txtCodigo.Text = codigoSector.ToString();
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
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar el Sector seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
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
                MessageBox.Show("Debe seleccionar un Sector de Trabajo de la lista.", "Aviso");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo y selecciono algo en el combo
            if (txtNombre.Text != String.Empty && txtAbreviatura.Text != String.Empty && txtDescripcion.Text != String.Empty)
            {
                Entidades.Sector sector = new GyCAP.Entidades.Sector();
               
                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo)
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
                        Data.dsSectorTrabajo.SECTORESRow rowSector = dsSectorTrabajo.SECTORES.NewSECTORESRow();
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
                        Data.dsSectorTrabajo.SECTORESRow rowSector = dsSectorTrabajo.SECTORES.FindBySEC_CODIGO(sector.Codigo);
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
