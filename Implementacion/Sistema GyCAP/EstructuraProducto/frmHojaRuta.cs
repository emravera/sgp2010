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
    public partial class frmHojaRuta : Form
    {
        private static frmHojaRuta _frmHojaRuta = null;
        private Data.dsProduccion dsHojaRuta = new GyCAP.Data.dsProduccion();
        private DataView dvHojasRuta, dvDetalleHoja, dvCentrosTrabajo;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        //Variable que simula el código autodecremental para el detalle, usa valores negativos para no tener problemas con valores existentes
        int codigoDetalle = -1;

        #region Inicio

        public frmHojaRuta()
        {
            InitializeComponent(); 
            InicializarDatos();
            SetSlide();
        }

        public static frmHojaRuta Instancia
        {
            get
            {
                if (_frmHojaRuta == null || _frmHojaRuta.IsDisposed)
                {
                    _frmHojaRuta = new frmHojaRuta();
                }
                else
                {
                    _frmHojaRuta.BringToFront();
                }
                return _frmHojaRuta;
            }
            set
            {
                _frmHojaRuta = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }
        #endregion

        #region Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsHojaRuta.HOJAS_RUTA.Clear();
                dsHojaRuta.CENTROSXHOJARUTA.Clear();
                dvHojasRuta.Table = null;
                dvDetalleHoja.Table = null;

                //Busquemos, no importa si ingresó algo o no, ya se encargarán las otras clases de verificarlo
                BLL.HojaRutaBLL.ObetenerHojasRuta(txtNombreBuscar.Text, cbActivaBuscar.GetSelectedValueInt(), dsHojaRuta);

                if (dsHojaRuta.HOJAS_RUTA.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Hojas de Ruta con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvHojasRuta.Table = dsHojaRuta.HOJAS_RUTA;
                dvDetalleHoja.Table = dsHojaRuta.CENTROSXHOJARUTA;

                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Hoja de Ruta - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dgvHojasRuta.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar la Hoja de Ruta seleccionada?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtenemos el codigo
                        int codigo = Convert.ToInt32(dvHojasRuta[dgvHojasRuta.SelectedRows[0].Index]["hr_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.HojaRutaBLL.Eliminar(codigo);
                        //Lo eliminamos de la tabla conjuntos del dataset
                        dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigo).Delete();
                        dsHojaRuta.HOJAS_RUTA.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: Hoja de Ruta - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: Hoja de Ruta - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Hoja de Ruta de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Datos


        #endregion

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos = true;

                    if (dsHojaRuta.HOJAS_RUTA.Rows.Count == 0)
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
                    tcHojaRuta.SelectedTab = tpBuscar;
                    txtNombreBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    dtpFechaAlta.SetFechaNull();
                    dtpFechaAlta.Enabled = true;
                    chkActivo.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    dvDetalleHoja.RowFilter = "HR_CODIGO = -1";
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    tcHojaRuta.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    dtpFechaAlta.SetFechaNull();
                    dtpFechaAlta.Enabled = true;
                    chkActivo.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    dvDetalleHoja.RowFilter = "HR_CODIGO = -1";
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcHojaRuta.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    dtpFechaAlta.Enabled = false;
                    chkActivo.Enabled = false;
                    txtDescripcion.ReadOnly = true;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    panelAcciones.Enabled = false;
                    slideControl.Selected = slideDatos;
                    estadoInterface = estadoUI.consultar;
                    tcHojaRuta.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    dtpFechaAlta.Enabled = true;
                    chkActivo.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.modificar;
                    tcHojaRuta.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                default:
                    break;
            }
        }

        private void InicializarDatos()
        {
            //Grillas
            dgvHojasRuta.AutoGenerateColumns = false;
            dgvHojasRuta.Columns.Add("HR_NOMBRE", "Nombre");
            dgvHojasRuta.Columns.Add("HR_FECHAALTA", "Fecha Creación");
            dgvHojasRuta.Columns.Add("HR_ACTIVO", "Estado");
            dgvHojasRuta.Columns.Add("HR_DESCRIPCION", "Descripción");
            dgvHojasRuta.Columns["HR_NOMBRE"].DataPropertyName = "HR_NOMBRE";
            dgvHojasRuta.Columns["HR_FECHAALTA"].DataPropertyName = "HR_FECHAALTA";
            dgvHojasRuta.Columns["HR_ACTIVO"].DataPropertyName = "HR_ACTIVO";
            dgvHojasRuta.Columns["HR_DESCRIPCION"].DataPropertyName = "HR_DESCRIPCION";
            dgvHojasRuta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvHojasRuta.Columns["HR_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvHojasRuta.Columns["HR_DESCRIPCION"].Resizable = DataGridViewTriState.True;

            dgvDetalleHoja.AutoGenerateColumns = false;
            dgvDetalleHoja.Columns.Add("CXHR_SECUENCIA", "Secuencia");
            dgvDetalleHoja.Columns.Add("CTO_CODIGO", "Centro Trabajo");
            dgvDetalleHoja.Columns["CXHR_SECUENCIA"].DataPropertyName = "CXHR_SECUENCIA";
            dgvDetalleHoja.Columns["CXHR_SECUENCIA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDetalleHoja.Columns["CTO_CODIGO"].DataPropertyName = "CTO_CODIGO";
            dgvDetalleHoja.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvCentrosTrabajo.Columns.Add("CTO_NOMBRE", "Nombre");
            dgvCentrosTrabajo.Columns.Add("CTO_TIPO", "Tipo");
            dgvCentrosTrabajo.Columns.Add("SEC_CODIGO", "Sector");
            dgvCentrosTrabajo.Columns.Add("CTO_DESCRIPCION", "Descripción");
            dgvCentrosTrabajo.Columns["CTO_NOMBRE"].DataPropertyName = "CTO_NOMBRE";
            dgvCentrosTrabajo.Columns["CTO_TIPO"].DataPropertyName = "CTO_TIPO";
            dgvCentrosTrabajo.Columns["SEC_CODIGO"].DataPropertyName = "SEC_CODIGO";
            dgvCentrosTrabajo.Columns["CTO_DESCRIPCION"].DataPropertyName = "CTO_DESCRIPCION";
            dgvCentrosTrabajo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvCentrosTrabajo.Columns["CTO_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCentrosTrabajo.Columns["CTO_DESCRIPCION"].Resizable = DataGridViewTriState.True;

            //Dataviews, combos y carga de datos iniciales
            dvHojasRuta = new DataView(dsHojaRuta.HOJAS_RUTA);
            dvHojasRuta.Sort = "HR_NOMBRE ASC";
            dgvHojasRuta.DataSource = dvHojasRuta;
            dvDetalleHoja = new DataView(dsHojaRuta.CENTROSXHOJARUTA);
            dgvDetalleHoja.DataSource = dvDetalleHoja;            
            string[] nombres = { "Activa", "Inactiva" };
            int[] valores = { BLL.HojaRutaBLL.hojaRutaActiva, BLL.HojaRutaBLL.hojaRutaInactiva };
            cbActivaBuscar.SetDatos(nombres, valores, "--TODOS--", true);

            try
            {
                BLL.CentroTrabajoBLL.ObetenerCentrosTrabajo(null, null, null, BLL.CentroTrabajoBLL.CentroActivo, dsHojaRuta.CENTROS_TRABAJOS);
                BLL.SectorBLL.ObtenerTodos(dsHojaRuta.SECTORES);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dvCentrosTrabajo = new DataView(dsHojaRuta.CENTROS_TRABAJOS);
            dvCentrosTrabajo.Sort = "CTO_NOMBRE ASC";
            dgvCentrosTrabajo.DataSource = dvCentrosTrabajo;
        }        

        private void dgvHojasRuta_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigo = Convert.ToInt32(dvHojasRuta[e.RowIndex]["hr_codigo"]);
        }

        private void dgvHojasRuta_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre = string.Empty;

                if (dgvHojasRuta.Columns[e.ColumnIndex].Name == "HR_ACTIVO")
                {
                    if (Convert.ToInt32(e.Value.ToString()) == BLL.HojaRutaBLL.hojaRutaActiva) { nombre = "Activa"; }
                    else if (Convert.ToInt32(e.Value.ToString()) == BLL.HojaRutaBLL.hojaRutaInactiva) { nombre = "Inactiva"; }
                    e.Value = nombre;
                }
            }
        }

        private void SetSlide()
        {
            gbDatos.Parent = slideDatos;
            gbCentrosTrabajo.Parent = slideAgregar;
            slideControl.AddSlide(slideAgregar);
            slideControl.AddSlide(slideDatos);
            slideControl.Selected = slideDatos;
        }

        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(txtNombre.GetType())) { (sender as TextBox).SelectAll(); }
            if (sender.GetType().Equals(txtDescripcion.GetType())) { (sender as RichTextBox).SelectAll(); }
            //if (sender.GetType().Equals(nudCantidad.GetType())) { (sender as NumericUpDown).Select(0, 20); }
        }

        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            Point punto = new Point((sender as Button).Location.X + 2, (sender as Button).Location.Y + 2);
            (sender as Button).Location = punto;
        }

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            Point punto = new Point((sender as Button).Location.X - 2, (sender as Button).Location.Y - 2);
            (sender as Button).Location = punto;
        }        

        private void dgvDetalleHoja_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre;

                switch (dgvDetalleHoja.Columns[e.ColumnIndex].Name)
                {
                    case "CTO_CODIGO":
                        nombre = dsHojaRuta.CENTROS_TRABAJOS.FindByCTO_CODIGO(Convert.ToInt32(e.Value.ToString())).CTO_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }        

        private void dgvCentrosTrabajo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre = string.Empty;

                switch (dgvCentrosTrabajo.Columns[e.ColumnIndex].Name)
                {
                    case "CTO_TIPO":
                        if (Convert.ToInt32(e.Value.ToString()) == BLL.CentroTrabajoBLL.TipoHombre) { nombre = "Hombre"; }
                        else if (Convert.ToInt32(e.Value.ToString()) == BLL.CentroTrabajoBLL.TipoMaquina) { nombre = "Máquina"; }
                        e.Value = nombre;
                        break;
                    case "SEC_CODIGO":
                        nombre = dsHojaRuta.SECTORES.FindBySEC_CODIGO(Convert.ToInt32(e.Value.ToString())).SEC_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion
    }
}
