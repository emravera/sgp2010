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
            dgvHojasRuta.Columns.Add("HR_ACTIVO", "Activa");
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

            //Dataviews

        }

        #endregion
    }
}
