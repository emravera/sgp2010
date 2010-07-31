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
    public partial class frmCocina : Form
    {
        private static frmCocina _frmCocina = null;
        Data.dsCocina dsCocina = new GyCAP.Data.dsCocina();
        DataView dvCocinas, dvMarcaBuscar, dvEstadoBuscar, dvTerminacionBuscar;
        DataView dvModelo, dvMarca, dvDesignacion, dvColor, dvTerminacion, dvEstado;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        #region Inicio

        public frmCocina()
        {
            InitializeComponent();

            SetGrillaCombosDatos();
            SetInterface(estadoUI.inicio);
        }

        public static frmCocina Instancia
        {
            get
            {
                if (_frmCocina == null || _frmCocina.IsDisposed)
                {
                    _frmCocina = new frmCocina();
                }
                else
                {
                    _frmCocina.BringToFront();
                }
                return _frmCocina;
            }
            set
            {
                _frmCocina = value;
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

        #endregion Inicio

        #region Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void dgvListaCocina_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void dgvListaCocina_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

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
                    bool hayDatos;

                    if (dsCocina.COCINAS.Rows.Count == 0)
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
                    tcCocina.SelectedTab = tpBuscar;
                    txtCodigoBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Text = String.Empty;
                    cbModelo.Enabled = true;
                    cbMarca.Enabled = true;
                    cbDesignacion.Enabled = true;
                    cbColor.Enabled = true;
                    cbTerminacion.Enabled = true;
                    cbEstado.Enabled = true;
                    nudPrecio.Enabled = true;
                    gbImagen.Enabled = true;                    
                    cbModelo.SetSelectedIndex(-1);
                    cbMarca.SetSelectedIndex(-1);
                    cbDesignacion.SetSelectedIndex(-1);
                    cbColor.SetSelectedIndex(-1);
                    cbTerminacion.SetSelectedIndex(-1);
                    cbEstado.SetSelectedIndex(-1);
                    nudPrecio.Value = 0;
                    pbImagen.Image = null;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcCocina.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Text = String.Empty;
                    cbModelo.Enabled = true;
                    cbMarca.Enabled = true;
                    cbDesignacion.Enabled = true;
                    cbColor.Enabled = true;
                    cbTerminacion.Enabled = true;
                    cbEstado.Enabled = true;
                    nudPrecio.Enabled = true;
                    gbImagen.Enabled = true;
                    cbModelo.SetSelectedIndex(-1);
                    cbMarca.SetSelectedIndex(-1);
                    cbDesignacion.SetSelectedIndex(-1);
                    cbColor.SetSelectedIndex(-1);
                    cbTerminacion.SetSelectedIndex(-1);
                    cbEstado.SetSelectedIndex(-1);
                    nudPrecio.Value = 0;
                    pbImagen.Image = null;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcCocina.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                case estadoUI.consultar:
                    txtCodigo.ReadOnly = true;
                    cbModelo.Enabled = false;
                    cbMarca.Enabled = false;
                    cbDesignacion.Enabled = false;
                    cbColor.Enabled = false;
                    cbTerminacion.Enabled = false;
                    cbEstado.Enabled = false;
                    nudPrecio.Enabled = false;
                    gbImagen.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcCocina.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:
                    txtCodigo.ReadOnly = false;
                    cbModelo.Enabled = true;
                    cbMarca.Enabled = true;
                    cbDesignacion.Enabled = true;
                    cbColor.Enabled = true;
                    cbTerminacion.Enabled = true;
                    cbEstado.Enabled = true;
                    nudPrecio.Enabled = true;
                    gbImagen.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcCocina.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                default:
                    break;
            }
        }

        private void SetGrillaCombosDatos()
        {
            //Cargo los datos
            try
            {
                BLL.TerminacionBLL.ObtenerTodos(string.Empty, dsCocina.TERMINACIONES);
                BLL.MarcaBLL.ObtenerTodos(dsCocina.MARCAS);
                BLL.EstadoCocinaBLL.ObtenerEstados(dsCocina.ESTADO_COCINAS);
                
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
            //Grilla
            dgvListaCocina.Columns.Add("COC_CODIGO_PRODUCTO","Código");
            dgvListaCocina.Columns.Add("MOD_CODIGO", "Modelo");
            dgvListaCocina.Columns.Add("MCA_CODIGO", "Marca");
            dgvListaCocina.Columns.Add("ECOC_CODIGO", "Estado");
            dgvListaCocina.Columns["COC_CODIGO_PRODUCTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvListaCocina.Columns["MOD_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvListaCocina.Columns["MCA_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvListaCocina.Columns["ECOC_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvListaCocina.Columns["COC_CODIGO_PRODUCTO"].DataPropertyName = "COC_CODIGO_PRODUCTO";
            dgvListaCocina.Columns["MOD_CODIGO"].DataPropertyName = "MOD_CODIGO";
            dgvListaCocina.Columns["MCA_CODIGO"].DataPropertyName = "MCA_CODIGO";
            dgvListaCocina.Columns["ECOC_CODIGO"].DataPropertyName = "ECOC_CODIGO";
            
            //Dataviews
            dvMarcaBuscar = new DataView(dsCocina.MARCAS);
            dvEstadoBuscar = new DataView(dsCocina.ESTADO_COCINAS);
            dvTerminacionBuscar = new DataView(dsCocina.TERMINACIONES);
            dvCocinas = new DataView(dsCocina.COCINAS);
            dvCocinas.Sort = "COC_CODIGO_PRODUCTO";
            dgvListaCocina.DataSource = dvCocinas;
            dvModelo = new DataView(dsCocina.MODELOS_COCINAS);
            dvMarca = new DataView(dsCocina.MARCAS);
            dvDesignacion = new DataView(dsCocina.DESIGNACIONES);
            dvColor = new DataView(dsCocina.COLORES);
            dvTerminacion = new DataView(dsCocina.TERMINACIONES);
            dvEstado = new DataView(dsCocina.ESTADO_COCINAS);

            //Combos
            cbMarcaBuscar.SetDatos(dvMarcaBuscar, "MCA_CODIGO", "MCA_NOMBRE", "--TODOS--", true);
            cbEstadoBuscar.SetDatos(dvEstadoBuscar, "ECOC_CODIGO", "ECOC_NOMBRE", "--TODOS--", true);
            cbTerminacionBuscar.SetDatos(dvTerminacionBuscar, "TE_CODIGO", "TE_NOMBRE", "--TODOS--", true);
            cbMarca.SetDatos(dvMarca, "MCA_CODIGO", "MCA_NOMBRE", "Seleccione", false);
            cbEstado.SetDatos(dvEstado, "ECOC_CODIGO", "ECOC_NOMBRE", "Seleccione", false);
            cbTerminacion.SetDatos(dvTerminacion, "TE_CODIGO", "TE_NOMBRE", "Seleccione", false);            
            cbModelo.SetDatos(dvModelo, "MOD_CODIGO", "MOD_NOMBRE", "Seleccione", false);
            cbDesignacion.SetDatos(dvDesignacion, "DESIG_CODIGO", "DESIG_NOMBRE", "Seleccione", false);
            cbColor.SetDatos(dvColor, "COL_CODIGO", "COL_NOMBRE", "Seleccione", false);
        }

        #endregion Servicios

    }
}
