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
        private static int codigoDomicilio = -1; int codigoProveedor = 0;

        #region Inicio

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
            dgvLista.Columns["PROVE_TELPRINCIPAL"].DataPropertyName = "PROVE_TELPRINCIPAL";
            dgvLista.Columns["PROVE_TELALTERNATIVO"].DataPropertyName = "PROVE_TELALTERNATIVO";
            
            //Creamos el dataview y lo asignamos a la grilla
            dvListaBusqueda = new DataView(dsProveedor.PROVEEDORES);
            dvListaBusqueda.Sort = "PROVE_CODIGO, PROVE_RAZONSOCIAL ASC";
            dgvLista.DataSource = dvListaBusqueda;

            //Ocultamos las columnas que no quiero que se vean
            dgvLista.Columns["PROVE_CODIGO"].Visible = false;

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
            
            //Seteamos el modo de tamaño de las columnas
            dgvDomicilios.Columns["DOM_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDomicilios.Columns["DOM_CALLE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDomicilios.Columns["DOM_NUMERO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDomicilios.Columns["DOM_PISO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDomicilios.Columns["DOM_DEPARTAMENTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDomicilios.Columns["LOC_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvDomicilios.Columns["DOM_CODIGO"].DataPropertyName = "DOM_CODIGO";
            dgvDomicilios.Columns["DOM_CALLE"].DataPropertyName = "DOM_CALLE";
            dgvDomicilios.Columns["DOM_NUMERO"].DataPropertyName = "DOM_NUMERO";
            dgvDomicilios.Columns["DOM_PISO"].DataPropertyName = "DOM_PISO";
            dgvDomicilios.Columns["DOM_DEPARTAMENTO"].DataPropertyName = "DOM_DEPARTAMENTO";
            dgvDomicilios.Columns["LOC_CODIGO"].DataPropertyName = "LOC_CODIGO";
            
            //Ocultamos la columna del Codigo de domicilio
            dgvDomicilios.Columns["DOM_CODIGO"].Visible = false;
            
            //Creamos el dataview y lo asignamos a la grilla
            dvListaDomicilios = new DataView(dsProveedor.DOMICILIOS);
            dgvDomicilios.DataSource = dvListaDomicilios;

            //Llenamos el dataset con sectores
            BLL.SectorBLL.ObtenerTodos(dsProveedor.SECTORES);

            //Llenamos el dataset con las provincias
            BLL.ProvinciaBLL.ObtenerProvincias(dsProveedor.PROVINCIAS);

            //Llenamos el dataset con las localidades
            BLL.LocalidadBLL.ObtenerLocalidades(dsProveedor.LOCALIDADES);

            //CARGA DE COMBOS
            //Creamos el Dataview y se lo asignamos al combo de Sectores Busqueda
            dvComboSectorBuscar = new DataView(dsProveedor.SECTORES);
            cbSectorBusqueda.SetDatos(dvComboSectorBuscar, "sec_codigo", "sec_nombre", "Seleccionar", false);

            //Creamos el Dataview y se lo asignamos al combo de Sectores en los datos
            dvComboSectorDatos = new DataView(dsProveedor.SECTORES);
            cbSectorDatos.SetDatos(dvComboSectorDatos, "sec_codigo", "sec_nombre", "Seleccionar", true);

            //Creamos el Dataview y se lo asignamos al combo de Provincias
            dvComboProvincia = new DataView(dsProveedor.PROVINCIAS);
            cbProvincia.SetDatos(dvComboProvincia, "pcia_codigo", "pcia_nombre", "Seleccionar", false);
            
            //Seteamos los maxlengh de los campos de texto
            txtRazonBuscar.MaxLength = 80;
            txtRazonSocial.MaxLength = 80;
            txtTelefonoPcipal.MaxLength = 30;
            txtTelefonoAlt.MaxLength = 30;
            txtCalle.MaxLength = 80;
            txtDepto.MaxLength = 5;
            txtNumero.MaxLength = 5;
            txtPiso.MaxLength = 2;

            //Seteamos la interface
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
                    if (dsProveedor.PROVEEDORES.Rows.Count == 0)
                    {
                        hayDatos = false;                        
                    }
                    else
                    {
                        hayDatos = true;
                        dgvLista.Focus();
                    }

                    //Linea para validacion
                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }

                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnConsultar.Enabled = hayDatos;
                    btnNuevo.Enabled = true;
                    
                    //Limpio la tabla de domicilios
                    dsProveedor.DOMICILIOS.Clear();

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
                    cbLocalidad.Enabled = false;
                    cbProvincia.SelectedIndex = -1;

                    //Ponemos readonly al resto de los textbox
                    txtRazonSocial.ReadOnly = false;
                    txtTelefonoAlt.ReadOnly = false;
                    txtTelefonoPcipal.ReadOnly = false;
                    cbSectorDatos.Enabled = true;

                    //Parte del domicilio
                    txtCalle.ReadOnly = false;
                    txtDepto.ReadOnly = false;
                    txtNumero.ReadOnly = false;
                    txtPiso.ReadOnly = false;
                    cbProvincia.Enabled = true;
                    btnNuevaLocalidad.Enabled = true;
                    btnAgregarDomicilio.Enabled = true;

                    //Ocultamos la columna del Codigo de domicilio
                    dgvDomicilios.Columns["DOM_CODIGO"].Visible = false;
                    
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    tcABM.SelectedTab = tpDatos;
                    estadoInterface = estadoUI.nuevo;
                    break;

                case estadoUI.modificar:
                    txtCalle.Text = string.Empty;
                    txtNumero.Text = string.Empty;
                    txtPiso.Text = string.Empty;
                    txtDepto.Text = string.Empty;
                    cbLocalidad.SelectedIndex = -1;
                    cbLocalidad.Enabled = false;
                    cbProvincia.SelectedIndex = -1;

                    //Ponemos readonly al resto de los textbox
                    txtRazonSocial.ReadOnly = false;
                    txtTelefonoAlt.ReadOnly = false;
                    txtTelefonoPcipal.ReadOnly = false;
                    cbSectorDatos.Enabled = true;

                    //Parte del domicilio
                    txtCalle.ReadOnly = false;
                    txtDepto.ReadOnly = false;
                    txtNumero.ReadOnly = false;
                    txtPiso.ReadOnly = false;
                    cbProvincia.Enabled = true;
                    btnAgregarDomicilio.Enabled = true;
                    btnNuevaLocalidad.Enabled = true;

                    //Ocultamos la columna del Codigo de domicilio
                    dgvDomicilios.Columns["DOM_CODIGO"].Visible = false;
                    
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    tcABM.SelectedTab = tpDatos;
                    estadoInterface = estadoUI.modificar;
                    break;

                case estadoUI.consultar:
                    txtCalle.Text = string.Empty;
                    txtNumero.Text = string.Empty;
                    txtPiso.Text = string.Empty;
                    txtDepto.Text = string.Empty;
                    cbLocalidad.SelectedIndex = -1;
                    cbLocalidad.Enabled = false;
                    cbProvincia.SelectedIndex = -1;
                    cbProvincia.Enabled = false;

                    //Ponemos readonly al resto de los textbox
                    txtRazonSocial.ReadOnly = true;
                    txtTelefonoAlt.ReadOnly = true;
                    txtTelefonoPcipal.ReadOnly = true;
                    cbSectorDatos.Enabled = false;
                    btnAgregarDomicilio.Enabled = false;
                    btnNuevaLocalidad.Enabled = false;

                    //Parte del domicilio
                    txtCalle.ReadOnly = true;
                    txtDepto.ReadOnly = true;
                    txtNumero.ReadOnly = true;
                    txtPiso.ReadOnly = true;
                    cbProvincia.Enabled = false;

                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    tcABM.SelectedTab = tpDatos;
                    estadoInterface = estadoUI.consultar;
                    break;
            }
        }

        //VALIDACIONES
        //Metodo para validar cuando se agrega domicilio
        private string ValidarAgregar()
        {
            string erroresValidacion = string.Empty;

            //Control de combos
            List<string> combos = new List<string>();
            if (cbLocalidad.GetSelectedIndex() == -1)
            {
                combos.Add("Localidad");               
            }
            if (cbProvincia.GetSelectedIndex() == -1)
            {
                combos.Add("Provincia");
            }
            //Mensaje de Combos
            if (combos.Count > 0)
            {
                erroresValidacion = erroresValidacion + Entidades.Mensajes.MensajesABM.EscribirValidacion(GyCAP.Entidades.Mensajes.MensajesABM.Validaciones.Seleccion, combos);
            }
                       
            //Control de los textbox 
            List<string> datos = new List<string>();
            if (txtCalle.Text.Trim().Length == 0)
            {
                datos.Add("Calle");
            }
            if (txtNumero.Text.Trim().Length == 0)
            {
                datos.Add("Numero");
            }
            //Mensaje de error de los textbox
            if (datos.Count > 0)
            {
                erroresValidacion = erroresValidacion + Entidades.Mensajes.MensajesABM.EscribirValidacion(GyCAP.Entidades.Mensajes.MensajesABM.Validaciones.CompletarDatos, datos);
            }            
            return erroresValidacion;
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

        //Formato de las grillas
        private void dgvDomicilios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                switch (dgvDomicilios.Columns[e.ColumnIndex].Name)
                {
                    case "LOC_CODIGO":
                        //Limpio y recargo completo el datatable de localidades
                        dsProveedor.LOCALIDADES.Clear();
                        BLL.LocalidadBLL.ObtenerLocalidades(dsProveedor.LOCALIDADES);
                        string nombre = dsProveedor.LOCALIDADES.FindByLOC_CODIGO(Convert.ToInt32(e.Value)).LOC_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }
        
        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "SEC_CODIGO":
                        string nombre = dsProveedor.SECTORES.FindBySEC_CODIGO(Convert.ToInt32(e.Value)).SEC_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
        
        #region Pestaña Busqueda

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }      

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsProveedor.PROVEEDORES.Clear();
                BLL.ProveedorBLL.ObtenerProveedor(txtRazonBuscar.Text, Convert.ToInt32(cbSectorBusqueda.GetSelectedValue()), dsProveedor.PROVEEDORES);
                dvListaBusqueda.Table = dsProveedor.PROVEEDORES;

                if (dsProveedor.PROVEEDORES.Rows.Count == 0)
                {
                    Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Proveedores", this.Text);
                }
                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
                SetInterface(estadoUI.inicio);
            }
        }
        #endregion
        
        #region Pestaña Datos

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDomicilios.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjConfirmaEliminarDatos("Proveedor", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Femenino, this.Text);
                if (respuesta == DialogResult.Yes)
                {
                    //Obtengo el código del domicilio
                    int codigo = Convert.ToInt32(dvListaDomicilios[dgvDomicilios.SelectedRows[0].Index]["dom_codigo"]);

                    //Elimino el Domicilio de la BD
                    BLL.DomicilioBLL.EliminarDomicilios(codigo);

                    //Elimino del dataset el domicilio
                    dsProveedor.DOMICILIOS.FindByDOM_CODIGO(codigo).Delete();
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Domicilio", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
            }
        }

        private void btnAgregarDomicilio_Click(object sender, EventArgs e)
        {
            try
            {
                string validar = ValidarAgregar();

                if( validar == string.Empty)
                {
                    Data.dsProveedor.DOMICILIOSRow row = dsProveedor.DOMICILIOS.NewDOMICILIOSRow();
                    row.BeginEdit();
                    row.DOM_CODIGO=codigoDomicilio--;
                    row.DOM_CALLE=txtCalle.Text;
                    row.DOM_NUMERO=txtNumero.Text;
                    row.PROVE_CODIGO = codigoProveedor;
                    row.LOC_CODIGO=Convert.ToInt32(cbLocalidad.GetSelectedValue());
                    if(txtDepto.Text != string.Empty)
                    {
                        row.DOM_DEPARTAMENTO= txtDepto.Text;
                    }
                    if(txtPiso.Text != string.Empty)
                    {
                        row.DOM_PISO= txtPiso.Text;
                    }
                    row.EndEdit();
                    dsProveedor.DOMICILIOS.AddDOMICILIOSRow(row);

                    //Limpiamos los controles una vez que se agrego el domicilio
                    txtCalle.Text = string.Empty;
                    txtNumero.Text = string.Empty;
                    txtDepto.Text = string.Empty;
                    txtPiso.Text = string.Empty;
                    cbLocalidad.Enabled = false;
                    cbLocalidad.SetSelectedIndex(-1);
                    cbLocalidad.Enabled = false;
                    dsProveedor.LOCALIDADES.Clear();
                    cbProvincia.SetSelectedIndex(-1);

                }
                else
                {
                    Entidades.Mensajes.MensajesABM.MsjValidacion(validar,this.Text);
                }
            }
            catch (Exception ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
            }
        }

      private void cbProvincia_DropDownClosed(object sender, EventArgs e)
       {
           if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.modificar)
           {
               //Limpiamos el datatable de localidades
               dsProveedor.LOCALIDADES.Clear();

               //Cargamos las localidades de la provincia
               BLL.LocalidadBLL.ObtenerLocalidades(Convert.ToInt32(cbProvincia.GetSelectedValue()), dsProveedor.LOCALIDADES);

               //Creamos el Dataview y se lo asignamos al combo de Localidades
               dvComboLocalidades = new DataView(dsProveedor.LOCALIDADES);
               cbLocalidad.SetDatos(dvComboLocalidades, "loc_codigo", "loc_nombre", "Seleccionar", true);
               cbLocalidad.Enabled = true;
           }
       }   

       private void btnGuardar_Click(object sender, EventArgs e)
       {
          try
           {
               if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
               {
                  
                   //Creamos los objetos a utilizar
                   Entidades.Proveedor proveedor = new GyCAP.Entidades.Proveedor();
                   Entidades.SectorTrabajo sector = new GyCAP.Entidades.SectorTrabajo();
                   List<Entidades.Domicilio> domicilios = new List<GyCAP.Entidades.Domicilio>();

                   //Creamos el objeto de proveedores
                   proveedor.RazonSocial = txtRazonSocial.Text;
                   proveedor.TelPrincipal = txtTelefonoPcipal.Text;
                   proveedor.TelSecundario = txtTelefonoAlt.Text;

                   //Creamos el objeto sector y se lo asignamos a proveedores
                   sector.Codigo = Convert.ToInt32(cbSectorDatos.GetSelectedValue());
                   proveedor.Sector = sector;

                   //Se define la fila para agregarlo o modificarlo en el dataset
                   Data.dsProveedor.PROVEEDORESRow row = dsProveedor.PROVEEDORES.NewPROVEEDORESRow();

                   //Vemos si esta guardando un nuevo Proveedor o modificando
                   if (estadoInterface == estadoUI.nuevo)
                   {
                       //Se guarda el proveedor nuevo en la base de datos
                       int codigoProv = BLL.ProveedorBLL.GuardarProveedor(proveedor, dsProveedor);
                                          
                       //Indicamos que comienza la edición de la fila del dataset
                       row.BeginEdit();
                       row.PROVE_CODIGO = codigoProv;
                       row.PROVE_RAZONSOCIAL = proveedor.RazonSocial;
                       row.PROVE_TELPRINCIPAL = proveedor.TelPrincipal;
                       row.PROVE_TELALTERNATIVO = proveedor.TelSecundario;
                       row.SEC_CODIGO = proveedor.Sector.Codigo;
                       //Termina la edición de la fila
                       row.EndEdit();
                       
                       //Agregamos la fila al dataset y aceptamos los cambios
                       dsProveedor.PROVEEDORES.AddPROVEEDORESRow(row);
                   }
                   else if (estadoInterface == estadoUI.modificar)
                   {
                       //Se modifica el proveedor
                       proveedor.Codigo= Convert.ToInt32(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["prove_codigo"]);
                       BLL.ProveedorBLL.ModificarProveedor(proveedor, dsProveedor);
                       
                       //Indicamos que comienza la edición de la fila del Dataset
                       row.BeginEdit();
                       row.PROVE_CODIGO = proveedor.Codigo;
                       row.PROVE_RAZONSOCIAL = proveedor.RazonSocial;
                       row.PROVE_TELPRINCIPAL = proveedor.TelPrincipal;
                       row.PROVE_TELALTERNATIVO = proveedor.TelSecundario;
                       row.SEC_CODIGO = proveedor.Sector.Codigo;

                       //Termina la edición de la fila
                       row.EndEdit();
                   }

                   //Mostramos el mensaje que se guardo correctamente
                   Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Proveedor", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);

                   //Si esta todo bien aceptamos los cambios que se le hacen al dataset
                   dsProveedor.PROVEEDORES.AcceptChanges();

                   //Limpio el dataset de Proveedores cargados
                   dsProveedor.PROVEEDORES.Clear();
                   
                   //Seteamos la interfaz al comienzo
                   SetInterface(estadoUI.inicio);                  
               }
           }
           catch (Entidades.Excepciones.ElementoExistenteException ex)
           {
              Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
           }
           catch(Entidades.Excepciones.BaseDeDatosException ex)
           {
               Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
           }
       }
      
       private void btnModificar_Click(object sender, EventArgs e)
       {
           try
           {
               //Datos de la cabecera
               int codigo = Convert.ToInt32(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["prove_codigo"]);
               txtRazonSocial.Text = dvListaBusqueda[dgvLista.SelectedRows[0].Index]["prove_razonsocial"].ToString();
               txtTelefonoPcipal.Text = dvListaBusqueda[dgvLista.SelectedRows[0].Index]["prove_telprincipal"].ToString();
               txtTelefonoAlt.Text = dvListaBusqueda[dgvLista.SelectedRows[0].Index]["prove_telalternativo"].ToString();
               int sector = Convert.ToInt32(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["sec_codigo"]);
               cbSectorDatos.SetSelectedValue(sector);

               //Selecciono los domicilios de ese proveedor
               dsProveedor.DOMICILIOS.Clear();
               BLL.DomicilioBLL.ObtenerDomicilios(codigo, dsProveedor.DOMICILIOS);

               if (sender.ToString() == "&Consultar")
               {
                   SetInterface(estadoUI.consultar);
               }
               else
               {
                   SetInterface(estadoUI.modificar);
               }
           }
           catch (Entidades.Excepciones.BaseDeDatosException ex)
           {
               Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
           }
       }

       private void btnConsultar_Click(object sender, EventArgs e)
       {
           this.btnModificar_Click(sender,e);
       }

       private void btnNuevaLocalidad_Click(object sender, EventArgs e)
       {
           //Abrimos el formulario de localidades
           GyCAP.UI.Soporte.frmLocalidad.Instancia.Show();
           
           //Recargamos el combo de localidades
           BLL.LocalidadBLL.ObtenerLocalidades(dsProveedor.LOCALIDADES);
       }

       private void btnEliminar_Click(object sender, EventArgs e)
       {
           //Controlamos que esté seleccionado algo
           if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
           {
               //Preguntamos si está seguro
               DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjConfirmaEliminarDatos("Proveedor", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
               if (respuesta == DialogResult.Yes)
               {
                   try
                   {
                       //Datos de la cabecera
                       int codigoProveedor = Convert.ToInt32(dvListaBusqueda[dgvLista.SelectedRows[0].Index]["prove_codigo"]);

                       //Elimino el Proveedor de la BD
                       BLL.ProveedorBLL.EliminarProveedor(codigoProveedor);

                       //Lo eliminamos del dataset
                       dsProveedor.PROVEEDORES.FindByPROVE_CODIGO(codigoProveedor).Delete();
                       dsProveedor.PROVEEDORES.AcceptChanges();

                       //Mensaje que fue eliminado correctamente
                       Entidades.Mensajes.MensajesABM.MsjConfirmaEliminar(this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                                              
                       //Seteo el estado de la interfaz
                       SetInterface(estadoUI.inicio);
                   }
                   catch (Entidades.Excepciones.BaseDeDatosException ex)
                   {
                       Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                   }
               }
           }
       }
        #endregion    

    }
}
