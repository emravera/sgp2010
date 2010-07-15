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
    public partial class frmConjunto : Form
    {
        private static frmConjunto _frmConjunto = null;
        private Data.dsEstructura dsEstructura = new GyCAP.Data.dsEstructura();
        private Data.dsTerminacion dsTerminacion = new GyCAP.Data.dsTerminacion();
        private DataView dvListaConjuntos, dvListaSubconjuntosActuales, dvListaSubconjuntosDisponibles;
        private DataView dvTerminacionBuscar, dvTerminaciones;
        private enum estadoUI { inicio, nuevo, consultar, modificar };
        private estadoUI estadoInterface;
        
        public frmConjunto()
        {
            InitializeComponent();

            //Setea todas las grillas y las vistas
            setGrillasVistasCombo();
            
            //Setea todos los controles necesarios para el efecto de slide
            SetSlide();
            
            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
        }

        //Método para evitar la creación de más de una pantalla
        public static frmConjunto Instancia
        {
            get
            {
                if (_frmConjunto == null || _frmConjunto.IsDisposed)
                {
                    _frmConjunto = new frmConjunto();
                }
                else
                {
                    _frmConjunto.BringToFront();
                }
                return _frmConjunto;
            }
            set
            {
                _frmConjunto = value;
            }
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
                dsEstructura.CONJUNTOS.Clear();
                dsEstructura.SUBCONJUNTOS.Clear();
                dsEstructura.SUBCONJUNTOSXCONJUNTOS.Clear();
                BLL.ConjuntoBLL.ObtenerTodos(txtNombreBuscar.Text, dsEstructura);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvListaConjuntos.Table = dsEstructura.CONJUNTOS;
                dvListaSubconjuntosActuales.Table = dsEstructura.SUBCONJUNTOSXCONJUNTOS;
                if (dsEstructura.CONJUNTOS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron conjuntos con el nombre ingresado.", "Aviso");
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
            if (dgvListaConjuntos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar el conjunto seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtenemos el codigo
                        int codigo = Convert.ToInt32(dvListaConjuntos[dgvListaConjuntos.SelectedRows[0].Index]["conj_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.ConjuntoBLL.Eliminar(codigo);
                        //Lo eliminamos de la tabla conjuntos del dataset
                        dsEstructura.CONJUNTOS.FindByCONJ_CODIGO(codigo).Delete();
                        dsEstructura.CONJUNTOS.AcceptChanges();
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
                MessageBox.Show("Debe seleccionar un conjunto de la lista.", "Aviso");
            }
        }

        #endregion

        #region Pestaña Datos

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODAVIA NO IMPLEMENTADO");
        }
        
        private void btnNew_Click(object sender, EventArgs e)
        {
            slideControl.ForwardTo("slideAgregar");
            panelAcciones.Enabled = false;
        }

        private void btnHecho_Click(object sender, EventArgs e)
        {
            slideControl.BackwardTo("slideDatos");
            panelAcciones.Enabled = true;
        }
        
        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        #endregion

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos = true;

                    if (dsEstructura.CONJUNTOS.Rows.Count == 0)
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
                    slideControl.Selected = slideDatos;
                    estadoInterface = estadoUI.inicio;
                    tcConjunto.SelectedTab = tpBuscar;
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
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    tcConjunto.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    panelAcciones.Enabled = false;
                    slideControl.Selected = slideDatos;
                    estadoInterface = estadoUI.consultar;
                    tcConjunto.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.modificar;
                    tcConjunto.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }

        //Método para evitar que se cierrre la pantalla con la X o con ALT+F4
        private void frmConjunto_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }

        //Evento RowEnter de la grilla, va cargando los datos en la pestaña Datos a medida que se
        //hace clic en alguna fila de la grilla
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            /*int codigoTipoUnidadMedida = Convert.ToInt32(dvTipoUnidadMedida[e.RowIndex]["tumed_codigo"]);
            txtCodigo.Text = codigoTipoUnidadMedida.ToString();
            txtNombre.Text = dsUnidadMedida.TIPOS_UNIDADES_MEDIDA.FindByTUMED_CODIGO(codigoTipoUnidadMedida).TUMED_NOMBRE;*/
        }
        
        //Evento doble clic en la grilla, es igual que si hiciera clic en Consultar
        private void dgvListaConjuntos_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        private void SetSlide()
        {
            panelDatos.Parent = slideDatos;
            panelAgregar.Parent = slideAgregar;
            
            slideControl.AddSlide(slideAgregar);
            slideControl.AddSlide(slideDatos);
            slideControl.Selected = slideDatos;
            panelDatos.Location = new Point(0, 10);
            panelAgregar.Location = new Point(7, 7);
        }

        private void setGrillasVistasCombo()
        {
            //Para que no genere las columnas automáticamente
            dgvListaConjuntos.AutoGenerateColumns = false;
            dgvSCActuales.AutoGenerateColumns = false;
            dgvSCDisponibles.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvListaConjuntos.Columns.Add("CONJ_CODIGO", "Código");
            dgvListaConjuntos.Columns.Add("CONJ_NOMBRE", "Nombre");
            dgvListaConjuntos.Columns.Add("TE_CODIGO", "Terminación");

            dgvSCActuales.Columns.Add("SCONJ_CODIGO", "Código");
            dgvSCActuales.Columns.Add("SCONJ_NOMBRE", "Nombre");
            dgvSCActuales.Columns.Add("SCONJ_CANTIDAD", "Cantidad");
            
            dgvSCDisponibles.Columns.Add("SCONJ_CODIGO","Código");
            dgvSCDisponibles.Columns.Add("SCONJ_NOMBRE", "Nombre");
            dgvSCDisponibles.Columns.Add("TE_CODIGO", "Terminación");

            //Indicamos de dónde van a sacar los datos cada columna
            dgvListaConjuntos.Columns["CONJ_CODIGO"].DataPropertyName = "CONJ_CODIGO";
            dgvListaConjuntos.Columns["CONJ_NOMBRE"].DataPropertyName = "CONJ_NOMBRE";
            dgvListaConjuntos.Columns["TE_CODIGO"].DataPropertyName = "TE_CODIGO";

            dgvSCActuales.Columns["SCONJ_CODIGO"].DataPropertyName = "SCONJ_CODIGO";
            dgvSCActuales.Columns["SCONJ_NOMBRE"].DataPropertyName = "SCONJ_CODIGO";
            dgvSCActuales.Columns["SCONJ_CANTIDAD"].DataPropertyName = "SCONJ_CANTIDAD";

            dgvSCDisponibles.Columns["SCONJ_CODIGO"].DataPropertyName = "SCONJ_CODIGO";
            dgvSCDisponibles.Columns["SCONJ_NOMBRE"].DataPropertyName = "SCONJ_NOMBRE";
            dgvSCDisponibles.Columns["TE_CODIGO"].DataPropertyName = "TE_CODIGO";
            
            //Creamos el dataview y lo asignamos a la grilla
            dvListaConjuntos = new DataView(dsEstructura.CONJUNTOS);
            dgvListaConjuntos.DataSource = dvListaConjuntos;
            dvListaSubconjuntosActuales = new DataView(dsEstructura.SUBCONJUNTOSXCONJUNTOS);
            dgvSCActuales.DataSource = dvListaSubconjuntosActuales;
            dvListaSubconjuntosDisponibles = new DataView(dsEstructura.SUBCONJUNTOS);
            dgvSCDisponibles.DataSource = dvListaSubconjuntosDisponibles;

            //Obtenemos las terminaciones
            //Codigo chancho por no pensar bien antes como trabajar
            try
            {
                BLL.TerminacionBLL.ObtenerTodos(string.Empty, dsTerminacion);
                
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message);
            }

            dvTerminaciones = new DataView(dsTerminacion.TERMINACIONES);
            cbTerminacion.DataSource = dvTerminaciones;
            cbTerminacion.DisplayMember = "te_nombre";
            cbTerminacion.ValueMember = "te_codigo";
            cbTerminacion.SelectedIndex = -1;
            dvTerminacionBuscar = new DataView(dsTerminacion.TERMINACIONES);
            cbTerminacionBuscar.DataSource = dvTerminacionBuscar;
            cbTerminacionBuscar.DisplayMember = "te_nombre";
            cbTerminacionBuscar.ValueMember = "te_codigo";
            cbTerminacionBuscar.SelectedIndex = -1;
        }

        private void rbNombre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNombre.Checked)
            { 
                txtNombreBuscar.ReadOnly = false;
                cbTerminacionBuscar.Enabled = false;
            }
        }

        private void rbTerminacion_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTerminacion.Checked)
            {
                txtNombreBuscar.ReadOnly = true;
                cbTerminacionBuscar.Enabled = true;
            }
        }
        
        #endregion        

        
    }
}
