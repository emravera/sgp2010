using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.GestionStock
{
    public partial class frmProveedor : Form
    {

        private static frmProveedor _frmProveedor = null;
        private Data.dsProveedor dsProveedor = new GyCAP.Data.dsProveedor();
        private enum estadoUI { inicio, nuevo, consultar, modificar, nuevoExterno };
        private estadoUI estadoInterface;
        private DataView dvListaBusqueda, dvListaDomicilios, dvComboSectorBuscar, dvComboSectorDatos, 
                         dvComboProvincia, dvComboLocalidades;
        
        public frmProveedor()
        {
            InitializeComponent();

            //LISTA DE BUSQUEDA
            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("PROVE_CODIGO", "Código");
            dgvLista.Columns.Add("PROVE_RAZONSOCIAL", "Razon Social");
            dgvLista.Columns.Add("SEC_CODIGO", "Sector");
            dgvLista.Columns.Add("PROVE_TELPRINCIPAL", "Telefono Principal");
            dgvLista.Columns.Add("PROVE_TELALTERNATIVO", "Telefono Alternativo");
            
            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns["PROVE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PROVE_RAZONSOCIAL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["SEC_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PROVE_TELPRINCIPAL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["PROVE_TELALTERNATIVO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["PROVE_CODIGO"].DataPropertyName = "PROVE_CODIGO";
            dgvLista.Columns["PROVE_RAZONSOCIAL"].DataPropertyName = "PROVE_RAZONSOCIAL";
            dgvLista.Columns["SEC_CODIGO"].DataPropertyName = "SEC_CODIGO";
            dgvLista.Columns["PROVE_TELPRINCIPAL"].DataPropertyName = "DOM_CALLE";
            dgvLista.Columns["PROVE_TELALTERNATIVO"].DataPropertyName = "DOM_NUMERO";
            
            //Creamos el dataview y lo asignamos a la grilla
            dvListaBusqueda = new DataView(dsProveedor.PROVEEDORES);
            dvListaBusqueda.Sort = "PROVE_CODIGO, PROVE_RAZONSOCIAL ASC";
            dgvLista.DataSource = dvListaBusqueda;


            //LISTA DE DATOS
            //Para que no genere las columnas automáticamente
            dgvDomicilios.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvDomicilios.Columns.Add("DOM_CODIGO", "Código");
            dgvDomicilios.Columns.Add("DOM_CALLE", "Calle");
            dgvDomicilios.Columns.Add("DOM_NUMERO", "Nro.");
            dgvDomicilios.Columns.Add("DOM_PISO", "Piso");
            dgvDomicilios.Columns.Add("DOM_DEPARTAMENTO", "Depto.");
            dgvDomicilios.Columns.Add("LOC_CODIGO", "Localidad");
            dgvDomicilios.Columns.Add("PCIA_CODIGO", "Provincia");

            //Seteamos el modo de tamaño de las columnas
            dgvDomicilios.Columns["DOM_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDomicilios.Columns["DOM_CALLE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDomicilios.Columns["DOM_NUMERO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDomicilios.Columns["DOM_PISO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDomicilios.Columns["DOM_DEPARTAMENTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDomicilios.Columns["LOC_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDomicilios.Columns["PCIA_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDomicilios.Columns["DOM_CODIGO"].DataPropertyName = "DOM_CODIGO";
            dgvDomicilios.Columns["DOM_CALLE"].DataPropertyName = "DOM_CALLE";
            dgvDomicilios.Columns["DOM_NUMERO"].DataPropertyName = "DOM_NUMERO";
            dgvDomicilios.Columns["DOM_PISO"].DataPropertyName = "DOM_PISO";
            dgvDomicilios.Columns["DOM_DEPARTAMENTO"].DataPropertyName = "DOM_DEPARTAMENTO";
            dgvDomicilios.Columns["LOC_CODIGO"].DataPropertyName = "LOC_CODIGO";
            dgvDomicilios.Columns["PCIA_CODIGO"].DataPropertyName = "PCIA_CODIGO";

            //Creamos el dataview y lo asignamos a la grilla
            dvListaDomicilios = new DataView(dsProveedor.DOMICILIOS);
            dgvDomicilios.DataSource = dvListaDomicilios;

            //Llenamos el dataset con sectores
            BLL.SectorBLL.ObtenerTodos(dsProveedor.SECTORES);

            //Llenamos el dataset con las provincias
            BLL.ProvinciaBLL.ObtenerProvincias(dsProveedor.PROVINCIAS);

            //CARGA DE COMBOS
            //Creamos el Dataview y se lo asignamos al combo de Sectores Busqueda
            dvComboSectorBuscar = new DataView(dsProveedor.SECTORES);
            cbSectorBusqueda.SetDatos(dvComboSectorBuscar, "sec_codigo", "sec_nombre", "Seleccionar", true);

            //Creamos el Dataview y se lo asignamos al combo de Sectores en los datos
            dvComboSectorDatos = new DataView(dsProveedor.SECTORES);
            cbSectorDatos.SetDatos(dvComboSectorDatos, "sec_codigo", "sec_nombre", "Seleccionar", true);

            //Creamos el Dataview y se lo asignamos al combo de Provincias
            dvComboProvincia = new DataView(dsProveedor.PROVINCIAS);
            cbProvincia.SetDatos(dvComboProvincia, "pcia_codigo", "pcia_nombre", "Seleccionar", true);
            
            //Seteamos los maxlengh de los campos de texto
            txtRazonBuscar.MaxLength = 50;
            txtRazonSocial.MaxLength = 50;
            txtTelefonoPcipal.MaxLength = 30;
            txtTelefonoAlt.MaxLength = 30;
            txtCalle.MaxLength = 30;
            txtDepto.MaxLength = 10;
            txtNumero.MaxLength = 6;
            txtPiso.MaxLength = 2;

            //Seteamos la interface
            SetInterface(estadoUI.inicio);

        }

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;
                    if (dsProveedor.PROVEEDORES.Rows.Count == 0)
                    {
                        hayDatos = false;
                        btnBuscar.Focus();
                    }
                    else
                    {
                        hayDatos = true;
                        dgvLista.Focus();
                    }
                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnConsultar.Enabled = hayDatos;
                    btnNuevo.Enabled = true;
                    tcABM.SelectedTab = tpBuscar;
                    estadoInterface = estadoUI.inicio;
                    break;
                case estadoUI.nuevo:
                    txtRazonSocial.Text = string.Empty;
                    txtTelefonoPcipal.Text = string.Empty;
                    txtTelefonoAlt.Text = string.Empty;
                    txtCalle.Text = string.Empty;
                    txtNumero.Text = string.Empty;
                    txtPiso.Text = string.Empty;
                    txtDepto.Text = string.Empty;
                    cbSectorDatos.SelectedIndex = -1;
                    cbLocalidad.SelectedIndex = -1;
                    cbProvincia.SelectedIndex = -1;

                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    tcABM.SelectedTab = tpDatos;
                    estadoInterface = estadoUI.nuevo;
                    break;
            }
        }


        //Método para evitar la creación de más de una pantalla
        public static frmProveedor Instancia
        {
            get
            {
                if (_frmProveedor == null || _frmProveedor.IsDisposed)
                {
                    _frmProveedor = new frmProveedor();
                }
                else
                {
                    _frmProveedor.BringToFront();
                }
                return _frmProveedor;
            }
            set
            {
                _frmProveedor = value;
            }
        }

        
        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsProveedor.PROVEEDORES.Clear();
                BLL.ProveedorBLL.ObtenerProveedor(txtRazonBuscar.Text, cbSectorBusqueda.GetSelectedValueInt(), dsProveedor.PROVEEDORES);
                dvListaBusqueda.Table = dsProveedor.PROVEEDORES;

                if (dsProveedor.PROVEEDORES.Rows.Count == 0)
                {
                    Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Localidades", this.Text);
                }
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
                SetInterface(estadoUI.inicio);
            }
        }

       

        
        

        
    }
}
