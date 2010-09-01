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
        private Data.dsHojaRuta dsHojaRuta = new GyCAP.Data.dsHojaRuta();
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

        #region Buscar y botones menú

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsHojaRuta.HOJAS_RUTA.Clear();
                dsHojaRuta.CENTROSXHOJARUTA.Clear();
                dvHojasRuta.Table = null;
                dvDetalleHoja.Table = null;

                //Busquemos, no importa si ingresó algo o no, ya se encargarán las otras clases de verificarlo
                BLL.HojaRutaBLL.ObtenerHojasRuta(txtNombreBuscar.Text, cbActivaBuscar.GetSelectedValueInt(), dsHojaRuta, true);

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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Datos opcionales = descripcion, 
            //Revisamos que completó los datos obligatorios
            string datosFaltantes = string.Empty;
            if (txtNombre.Text == string.Empty) { datosFaltantes += "* Nombre\n"; }
            if (dtpFechaAlta.IsValueNull()) { datosFaltantes += "* Fecha de creación\n"; }            
            if (dgvDetalleHoja.Rows.Count == 0) { datosFaltantes += "* El detalle de la Hoja de Ruta\n"; }
            if (datosFaltantes == string.Empty)
            {
                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando uno nuevo
                    try
                    {
                        //Como ahora tenemos más de una tabla y relacionadas vamos a trabajar diferente
                        //Primero lo agregamos a la tabla del dataset con código -1, luego la entidad 
                        //DAL se va a encargar de insertarle el código que corresponda
                        Data.dsHojaRuta.HOJAS_RUTARow rowHoja = dsHojaRuta.HOJAS_RUTA.NewHOJAS_RUTARow();
                        rowHoja.BeginEdit();
                        rowHoja.HR_CODIGO = -1;
                        rowHoja.HR_NOMBRE = txtNombre.Text;
                        rowHoja.HR_DESCRIPCION = txtDescripcion.Text;
                        rowHoja.HR_FECHAALTA = DateTime.Parse(dtpFechaAlta.GetFecha().ToString());
                        if (chkActivo.Checked) { rowHoja.HR_ACTIVO = BLL.HojaRutaBLL.hojaRutaActiva; }
                        else { rowHoja.HR_ACTIVO = BLL.HojaRutaBLL.hojaRutaInactiva; }
                        rowHoja.EndEdit();
                        dsHojaRuta.HOJAS_RUTA.AddHOJAS_RUTARow(rowHoja);
                        //Todavia no aceptamos los cambios porque necesitamos que queden marcadas como nuevas las filas
                        //para que la entidad BLL y DAL sepan cuales insertar
                        BLL.HojaRutaBLL.Insertar(dsHojaRuta);
                        //Ahora si aceptamos los cambios
                        dsHojaRuta.HOJAS_RUTA.AcceptChanges();
                        dsHojaRuta.CENTROSXHOJARUTA.AcceptChanges();
                        //Y por último seteamos el estado de la interfaz

                        //Vemos cómo se inició el formulario para determinar la acción a seguir
                        if (estadoInterface == estadoUI.nuevoExterno)
                        {
                            //Nuevo desde acceso directo, cerramos el formulario
                            btnSalir.PerformClick();
                        }
                        else
                        {
                            //Nuevo desde el mismo formulario, volvemos a la pestaña buscar
                            SetInterface(estadoUI.inicio);
                        }
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de hoja ruta ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsHojaRuta.HOJAS_RUTA.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Está modificando
                    //Primero obtenemos su código del dataview que está relacionado a la fila seleccionada
                    int codigoHoja = Convert.ToInt32(dvHojasRuta[dgvHojasRuta.SelectedRows[0].Index]["hr_codigo"]);
                    //Segundo obtenemos el resto de los datos que puede cambiar el usuario, la estructura se fué
                    //actualizando en el dataset a medida que el usuario ejecutaba una acción
                    dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigoHoja).HR_NOMBRE = txtNombre.Text;
                    dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigoHoja).HR_DESCRIPCION = txtDescripcion.Text;
                    dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigoHoja).HR_FECHAALTA = DateTime.Parse(dtpFechaAlta.GetFecha().ToString());
                    if (chkActivo.Checked) { dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigoHoja).HR_ACTIVO = BLL.HojaRutaBLL.hojaRutaActiva; }
                    else { dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigoHoja).HR_ACTIVO = BLL.HojaRutaBLL.hojaRutaInactiva; }
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.HojaRutaBLL.Actualizar(dsHojaRuta);
                        //El dataset ya se actualizó en las capas DAL y BLL, aceptamos los cambios
                        dsHojaRuta.HOJAS_RUTA.AcceptChanges();
                        dsHojaRuta.CENTROSXHOJARUTA.AcceptChanges();
                        //Avisamos que estuvo todo ok
                        MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //Hubo problemas con la BD, descartamos los cambios de hoja ruta ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsHojaRuta.HOJAS_RUTA.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Entidades.Excepciones.ErrorInesperadoException ex)
                    {
                        //Hubo problemas no esperados, descartamos los cambios de hoja ruta ya que puede intentar
                        //de nuevo y funcionar, en caso contrario el botón volver se encargará de descartar todo
                        dsHojaRuta.HOJAS_RUTA.RejectChanges();
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                dgvHojasRuta.Refresh();
            }
            else
            {
                MessageBox.Show("Debe completar los datos:\n\n" + datosFaltantes, "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void btnNew_Click(object sender, EventArgs e)
        {
            slideControl.ForwardTo("slideAgregar");
            panelAcciones.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDetalleHoja.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigo = Convert.ToInt32(dvDetalleHoja[dgvDetalleHoja.SelectedRows[0].Index]["cxhr_codigo"]);
                //Lo borramos pero sólo del dataset
                dsHojaRuta.CENTROSXHOJARUTA.FindByCXHR_CODIGO(codigo).Delete();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Centro de Trabajo de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void btnSubir_Click(object sender, EventArgs e)
        {
            if (dgvDetalleHoja.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigo = Convert.ToInt32(dvDetalleHoja[dgvDetalleHoja.SelectedRows[0].Index]["cxhr_codigo"]);
                //Aumentamos la cantidad                
                dsHojaRuta.CENTROSXHOJARUTA.FindByCXHR_CODIGO(codigo).CXHR_SECUENCIA += 1;
                dvDetalleHoja.Sort = "CXHR_SECUENCIA ASC";
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Centro de Trabajo de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBajar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleHoja.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigo = Convert.ToInt32(dvDetalleHoja[dgvDetalleHoja.SelectedRows[0].Index]["cxhr_codigo"]);
                if (dsHojaRuta.CENTROSXHOJARUTA.FindByCXHR_CODIGO(codigo).CXHR_SECUENCIA > 0)
                {
                    //Aumentamos la cantidad                
                    dsHojaRuta.CENTROSXHOJARUTA.FindByCXHR_CODIGO(codigo).CXHR_SECUENCIA -= 1;
                    dvDetalleHoja.Sort = "CXHR_SECUENCIA ASC";
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Centro de Trabajo de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvCentrosTrabajo.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                bool agregar; //variable que indica si se debe agregar al listado
                //Obtenemos el código según sea nueva o modificada, lo hacemos acá porque lo vamos a usar mucho
                int hojaCodigo;
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { hojaCodigo = -1; }
                else { hojaCodigo = Convert.ToInt32(dvHojasRuta[dgvHojasRuta.SelectedRows[0].Index]["hr_codigo"]); }
                //Obtenemos el código del centro trabajo, también lo vamos a usar mucho
                int centroCodigo = Convert.ToInt32(dvCentrosTrabajo[dgvCentrosTrabajo.SelectedRows[0].Index]["cto_codigo"]);

                if (dvDetalleHoja.Count > 0)
                {
                    //Algo tiene, comprobemos que no intente agregar lo mismo haciendo una consulta al dataset,
                    //no usamos el dataview porque no queremos volver a filtrar los datos y perderlos
                    string filtro = "hr_codigo = " + hojaCodigo + " AND cto_codigo = " + centroCodigo;
                    Data.dsHojaRuta.CENTROSXHOJARUTARow[] rows =
                        (Data.dsHojaRuta.CENTROSXHOJARUTARow[])dsHojaRuta.CENTROSXHOJARUTA.Select(filtro);
                    if (rows.Length > 0)
                    {
                        //Ya lo ha agregado, avisemos
                        MessageBox.Show("La Hoja de Ruta ya posee el Centro de Tarbajo seleccionado.", "Información: elemento duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Como ya existe marcamos que no debe agregarse
                        agregar = false;
                    }
                    else
                    {
                        //No lo ha agregado, marcamos que debe agregarse
                        agregar = true;
                    }
                }
                else
                {
                    //No tiene ningún centro agregado, marcamos que debe agregarse
                    agregar = true;
                }

                //Ahora comprobamos si debe agregarse la materia prima o no
                if (agregar)
                {
                    Data.dsHojaRuta.CENTROSXHOJARUTARow row = dsHojaRuta.CENTROSXHOJARUTA.NewCENTROSXHOJARUTARow();
                    row.BeginEdit();
                    //Agregamos una fila nueva con nuestro código autodecremental, luego al guardar en la db se actualizará
                    row.CXHR_CODIGO = codigoDetalle--; //-- para que se vaya autodecrementando en cada inserción
                    row.HR_CODIGO = hojaCodigo;
                    row.CTO_CODIGO = centroCodigo;
                    row.CXHR_SECUENCIA = nudSecuencia.Value;
                    row.EndEdit();
                    //Agregamos la fila nueva al dataset sin aceptar cambios para que quede marcada como nueva ya que
                    //todavia no vamos a insertar en la db hasta que no haga Guardar
                    dsHojaRuta.CENTROSXHOJARUTA.AddCENTROSXHOJARUTARow(row);
                }
                nudSecuencia.Value = 0;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Centro de Trabajo de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void btnHecho_Click(object sender, EventArgs e)
        {
            slideControl.BackwardTo("slideDatos");
            nudSecuencia.Value = 0;
            panelAcciones.Enabled = true;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            //Descartamos los cambios realizamos hasta el momento sin guardar
            dsHojaRuta.HOJAS_RUTA.RejectChanges();
            dsHojaRuta.CENTROSXHOJARUTA.RejectChanges();
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

            dgvCentrosTrabajo.AutoGenerateColumns = false;
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
            dvDetalleHoja.Sort = "CXHR_SECUENCIA ASC";
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
            txtNombre.Text = dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigo).HR_NOMBRE;
            txtDescripcion.Text = dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigo).HR_DESCRIPCION;
            dtpFechaAlta.SetFecha(dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigo).HR_FECHAALTA);
            if (dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigo).HR_ACTIVO == BLL.HojaRutaBLL.hojaRutaActiva) { chkActivo.Checked = true; }
            else { chkActivo.Checked = false; }
            dvDetalleHoja.RowFilter = "hr_codigo = " + codigo;
        }

        private void dgvHojasRuta_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
            {
                string nombre = string.Empty;

                switch (dgvHojasRuta.Columns[e.ColumnIndex].Name)
                {
                    case "HR_ACTIVO":
                        if (Convert.ToInt32(e.Value.ToString()) == BLL.HojaRutaBLL.hojaRutaActiva) { nombre = "Activa"; }
                    else if (Convert.ToInt32(e.Value.ToString()) == BLL.HojaRutaBLL.hojaRutaInactiva) { nombre = "Inactiva"; }
                    e.Value = nombre;
                        break;
                    case "HR+FECHAALTA":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    default:
                        break;
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

        private void frmHojaRuta_Activated(object sender, EventArgs e)
        {
            txtNombre.Focus();
        }

        #endregion
    }
}
