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
        private DataView dvHojasRuta, dvDetalleHoja, dvCentrosTrabajo, dvOperaciones, dvStockOrigen, dvStockDestino, dvUbicacionStock;
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
                dsHojaRuta.DETALLE_HOJARUTA.Clear();
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
                dvDetalleHoja.Table = dsHojaRuta.DETALLE_HOJARUTA;

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
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar la Hoja de Ruta seleccionada?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        if (cboUbicacionStock.GetSelectedValueInt() != -1) { rowHoja.USTCK_NUMERO = cboUbicacionStock.GetSelectedValueInt(); }
                        else { rowHoja.SetUSTCK_NUMERONull(); }
                        rowHoja.EndEdit();
                        dsHojaRuta.HOJAS_RUTA.AddHOJAS_RUTARow(rowHoja);
                        //Todavia no aceptamos los cambios porque necesitamos que queden marcadas como nuevas las filas
                        //para que la entidad BLL y DAL sepan cuales insertar
                        BLL.HojaRutaBLL.Insertar(dsHojaRuta);
                        //Ahora si aceptamos los cambios
                        dsHojaRuta.HOJAS_RUTA.AcceptChanges();
                        dsHojaRuta.DETALLE_HOJARUTA.AcceptChanges();
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
                    if (cboUbicacionStock.GetSelectedValueInt() == -1) { dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigoHoja).SetUSTCK_NUMERONull(); }
                    else { dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigoHoja).USTCK_NUMERO = cboUbicacionStock.GetSelectedValueInt(); }
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.HojaRutaBLL.Actualizar(dsHojaRuta);
                        //El dataset ya se actualizó en las capas DAL y BLL, aceptamos los cambios
                        dsHojaRuta.HOJAS_RUTA.AcceptChanges();
                        dsHojaRuta.DETALLE_HOJARUTA.AcceptChanges();
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
                int codigo = Convert.ToInt32(dvDetalleHoja[dgvDetalleHoja.SelectedRows[0].Index]["dhr_codigo"]);
                //Lo borramos pero sólo del dataset
                dsHojaRuta.DETALLE_HOJARUTA.FindByDHR_CODIGO(codigo).Delete();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un detalle de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void btnSubir_Click(object sender, EventArgs e)
        {
            if (dgvDetalleHoja.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigo = Convert.ToInt32(dvDetalleHoja[dgvDetalleHoja.SelectedRows[0].Index]["dhr_codigo"]);
                //Aumentamos la cantidad                
                dsHojaRuta.DETALLE_HOJARUTA.FindByDHR_CODIGO(codigo).DHR_SECUENCIA += 1;
                dvDetalleHoja.Sort = "DHR_SECUENCIA ASC";
            }
            else
            {
                MessageBox.Show("Debe seleccionar un detalle de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBajar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleHoja.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigo = Convert.ToInt32(dvDetalleHoja[dgvDetalleHoja.SelectedRows[0].Index]["dhr_codigo"]);
                if (dsHojaRuta.DETALLE_HOJARUTA.FindByDHR_CODIGO(codigo).DHR_SECUENCIA > 0)
                {
                    //Aumentamos la cantidad                
                    dsHojaRuta.DETALLE_HOJARUTA.FindByDHR_CODIGO(codigo).DHR_SECUENCIA -= 1;
                    dvDetalleHoja.Sort = "DHR_SECUENCIA ASC";
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un detalle de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cbOperacion.GetSelectedIndex() != -1 && cbCentroTrabajo.GetSelectedIndex() != -1)
            {
                bool agregar; //variable que indica si se debe agregar al listado
                //Obtenemos el código según sea nueva o modificada, lo hacemos acá porque lo vamos a usar mucho
                int hojaCodigo;
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { hojaCodigo = -1; }
                else { hojaCodigo = Convert.ToInt32(dvHojasRuta[dgvHojasRuta.SelectedRows[0].Index]["hr_codigo"]); }
                //Obtenemos el código del centro trabajo, también lo vamos a usar mucho
                int centroCodigo = cbCentroTrabajo.GetSelectedValueInt();
                int operacionNumero = cbOperacion.GetSelectedValueInt();
                if (dvDetalleHoja.Count > 0)
                {
                    //Algo tiene, comprobemos que no intente agregar lo mismo haciendo una consulta al dataset,
                    //no usamos el dataview porque no queremos volver a filtrar los datos y perderlos
                    string filtro = "hr_codigo = " + hojaCodigo + " AND cto_codigo = " + centroCodigo + " AND opr_numero = " + operacionNumero;
                    Data.dsHojaRuta.DETALLE_HOJARUTARow[] rows =
                        (Data.dsHojaRuta.DETALLE_HOJARUTARow[])dsHojaRuta.DETALLE_HOJARUTA.Select(filtro);
                    if (rows.Length > 0)
                    {
                        //Ya lo ha agregado, avisemos
                        MessageBox.Show("La Hoja de Ruta ya posee la Operación en el Centro de Trabajo seleccionado.", "Información: elemento duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    Data.dsHojaRuta.DETALLE_HOJARUTARow row = dsHojaRuta.DETALLE_HOJARUTA.NewDETALLE_HOJARUTARow();
                    row.BeginEdit();
                    //Agregamos una fila nueva con nuestro código autodecremental, luego al guardar en la db se actualizará
                    row.DHR_CODIGO = codigoDetalle--; //-- para que se vaya autodecrementando en cada inserción
                    row.HR_CODIGO = hojaCodigo;
                    row.OPR_NUMERO = operacionNumero;
                    row.CTO_CODIGO = centroCodigo;
                    row.DHR_SECUENCIA = nudSecuencia.Value;
                    if (cboStockOrigen.GetSelectedValueInt() != -1) { row.USTCK_ORIGEN = cboStockOrigen.GetSelectedValueInt(); }
                    else { row.SetUSTCK_ORIGENNull(); }
                    if (cboStockDestino.GetSelectedValueInt() != -1) { row.USTCK_DESTINO = cboStockDestino.GetSelectedValueInt(); }
                    else { row.SetUSTCK_DESTINONull(); }
                    row.EndEdit();
                    //Agregamos la fila nueva al dataset sin aceptar cambios para que quede marcada como nueva ya que
                    //todavia no vamos a insertar en la db hasta que no haga Guardar
                    dsHojaRuta.DETALLE_HOJARUTA.AddDETALLE_HOJARUTARow(row);
                }
                cbOperacion.SetTexto("Seleccione");
                cbCentroTrabajo.SetTexto("Seleccione");
                cboStockOrigen.SetSelectedValue(-1);
                cboStockDestino.SetSelectedValue(-1);
                nudSecuencia.Value = 0;
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Operación y un Centro de Trabajo.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            dsHojaRuta.DETALLE_HOJARUTA.RejectChanges();
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
                    cboUbicacionStock.Enabled = true;
                    cboUbicacionStock.SetSelectedValue(-1);
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
                    cboStockOrigen.Enabled = true;
                    cboStockDestino.Enabled = true;
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
                    cboUbicacionStock.Enabled = true;
                    cboUbicacionStock.SetSelectedValue(-1);
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
                    cboStockOrigen.Enabled = true;
                    cboStockDestino.Enabled = true;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcHojaRuta.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    dtpFechaAlta.Enabled = false;
                    chkActivo.Enabled = false;
                    cboUbicacionStock.Enabled = false;
                    txtDescripcion.ReadOnly = true;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    panelAcciones.Enabled = false;
                    cboStockOrigen.Enabled = false;
                    cboStockDestino.Enabled = false;
                    slideControl.Selected = slideDatos;
                    estadoInterface = estadoUI.consultar;
                    tcHojaRuta.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    dtpFechaAlta.Enabled = true;
                    chkActivo.Enabled = true;
                    cboUbicacionStock.Enabled = true;
                    txtDescripcion.ReadOnly = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    cboStockOrigen.Enabled = true;
                    cboStockDestino.Enabled = true;
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
            dgvHojasRuta.Columns.Add("USTCK_NUMERO", "Ubicación Stock");
            dgvHojasRuta.Columns.Add("HR_DESCRIPCION", "Descripción");
            dgvHojasRuta.Columns["HR_NOMBRE"].DataPropertyName = "HR_NOMBRE";
            dgvHojasRuta.Columns["HR_FECHAALTA"].DataPropertyName = "HR_FECHAALTA";
            dgvHojasRuta.Columns["HR_ACTIVO"].DataPropertyName = "HR_ACTIVO";
            dgvHojasRuta.Columns["USTCK_NUMERO"].DataPropertyName = "USTCK_NUMERO";
            dgvHojasRuta.Columns["HR_DESCRIPCION"].DataPropertyName = "HR_DESCRIPCION";
            dgvHojasRuta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvHojasRuta.Columns["HR_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvHojasRuta.Columns["HR_DESCRIPCION"].Resizable = DataGridViewTriState.True;
            dgvHojasRuta.Columns["HR_FECHAALTA"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvHojasRuta.Columns["HR_FECHAALTA"].Width = 110;

            dgvDetalleHoja.AutoGenerateColumns = false;
            dgvDetalleHoja.Columns.Add("DHR_SECUENCIA", "Secuencia");
            dgvDetalleHoja.Columns.Add("OPR_NUMERO", "Operación");
            dgvDetalleHoja.Columns.Add("CTO_CODIGO", "Centro Trabajo");
            dgvDetalleHoja.Columns.Add("USTCK_ORIGEN", "Stock origen");
            dgvDetalleHoja.Columns.Add("USTCK_DESTINO", "Stock destino");
            dgvDetalleHoja.Columns["DHR_SECUENCIA"].DataPropertyName = "DHR_SECUENCIA";
            dgvDetalleHoja.Columns["DHR_SECUENCIA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDetalleHoja.Columns["CTO_CODIGO"].DataPropertyName = "CTO_CODIGO";
            dgvDetalleHoja.Columns["OPR_NUMERO"].DataPropertyName = "OPR_NUMERO";
            dgvDetalleHoja.Columns["USTCK_ORIGEN"].DataPropertyName = "USTCK_ORIGEN";
            dgvDetalleHoja.Columns["USTCK_DESTINO"].DataPropertyName = "USTCK_DESTINO";
            dgvDetalleHoja.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvDetalleHoja.Columns["CTO_CODIGO"].MinimumWidth = 110;

            //Dataviews, combos y carga de datos iniciales
            dvHojasRuta = new DataView(dsHojaRuta.HOJAS_RUTA);
            dvHojasRuta.Sort = "HR_NOMBRE ASC";
            dgvHojasRuta.DataSource = dvHojasRuta;
            dvDetalleHoja = new DataView(dsHojaRuta.DETALLE_HOJARUTA);
            dgvDetalleHoja.DataSource = dvDetalleHoja;
            dvDetalleHoja.Sort = "DHR_SECUENCIA ASC";
            string[] nombres = { "Activa", "Inactiva" };
            int[] valores = { BLL.HojaRutaBLL.hojaRutaActiva, BLL.HojaRutaBLL.hojaRutaInactiva };
            cbActivaBuscar.SetDatos(nombres, valores, "--TODOS--", true);

            try
            {
                BLL.CentroTrabajoBLL.ObetenerCentrosTrabajo(null, null, null, BLL.CentroTrabajoBLL.CentroActivo, dsHojaRuta.CENTROS_TRABAJOS);
                BLL.SectorBLL.ObtenerTodos(dsHojaRuta.SECTORES);
                BLL.OperacionBLL.ObetenerOperaciones(dsHojaRuta.OPERACIONES);
                BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsHojaRuta.UBICACIONES_STOCK);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dvCentrosTrabajo = new DataView(dsHojaRuta.CENTROS_TRABAJOS);
            dvOperaciones = new DataView(dsHojaRuta.OPERACIONES);
            string[] display = { "OPR_CODIGO", "OPR_NOMBRE" };
            cbOperacion.SetDatos(dvOperaciones, "OPR_NUMERO", display, " - ", "Seleccione", false);
            cbCentroTrabajo.SetDatos(dvCentrosTrabajo, "CTO_CODIGO", "CTO_NOMBRE", "Seleccione", false);

            dvStockOrigen = new DataView(dsHojaRuta.UBICACIONES_STOCK);
            dvStockDestino = new DataView(dsHojaRuta.UBICACIONES_STOCK);
            cboStockOrigen.SetDatos(dvStockOrigen, "USTCK_NUMERO", "USTCK_NOMBRE", "Sin especificar...", true);
            cboStockDestino.SetDatos(dvStockDestino, "USTCK_NUMERO", "USTCK_NOMBRE", "Sin especificar...", true);

            dvUbicacionStock = new DataView(dsHojaRuta.UBICACIONES_STOCK);
            cboUbicacionStock.SetDatos(dvUbicacionStock, "USTCK_NUMERO", "USTCK_NOMBRE", "Sin especificar...", true);
        }        

        private void dgvHojasRuta_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigo = Convert.ToInt32(dvHojasRuta[e.RowIndex]["hr_codigo"]);
            txtNombre.Text = dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigo).HR_NOMBRE;
            txtDescripcion.Text = dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigo).HR_DESCRIPCION;
            dtpFechaAlta.SetFecha(dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigo).HR_FECHAALTA);
            if (dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigo).HR_ACTIVO == BLL.HojaRutaBLL.hojaRutaActiva) { chkActivo.Checked = true; }
            else { chkActivo.Checked = false; }
            if (dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigo).IsUSTCK_NUMERONull()) { cboUbicacionStock.SetSelectedIndex(-1); }
            else { cboUbicacionStock.SetSelectedValue(Convert.ToInt32(dsHojaRuta.HOJAS_RUTA.FindByHR_CODIGO(codigo).USTCK_NUMERO)); }
            dvDetalleHoja.RowFilter = "hr_codigo = " + codigo;
        }

        private void dgvHojasRuta_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value.ToString()))
            {
                string nombre = string.Empty;

                switch (dgvHojasRuta.Columns[e.ColumnIndex].Name)
                {
                    case "HR_ACTIVO":
                        if (Convert.ToInt32(e.Value.ToString()) == BLL.HojaRutaBLL.hojaRutaActiva) { nombre = "Activa"; }
                    else if (Convert.ToInt32(e.Value.ToString()) == BLL.HojaRutaBLL.hojaRutaInactiva) { nombre = "Inactiva"; }
                    e.Value = nombre;
                        break;
                    case "HR_FECHAALTA":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "USTCK_NUMERO":
                        nombre = dsHojaRuta.UBICACIONES_STOCK.FindByUSTCK_NUMERO(Convert.ToInt32(e.Value)).USTCK_NOMBRE;
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
            gbAgregar.Parent = slideAgregar;
            gbBotonesAgregar.Parent = slideAgregar;
            slideControl.AddSlide(slideAgregar);
            slideControl.AddSlide(slideDatos);
            slideControl.Selected = slideDatos;
        }

        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(txtNombre.GetType())) { (sender as TextBox).SelectAll(); }
            if (sender.GetType().Equals(txtDescripcion.GetType())) { (sender as RichTextBox).SelectAll(); }
            if (sender.GetType().Equals(nudSecuencia.GetType())) { (sender as NumericUpDown).Select(0, 20); }
        }

        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point punto = new Point((sender as Button).Location.X + 2, (sender as Button).Location.Y + 2);
                (sender as Button).Location = punto;
            }
        }

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point punto = new Point((sender as Button).Location.X - 2, (sender as Button).Location.Y - 2);
                (sender as Button).Location = punto;
            }
        }      

        private void dgvDetalleHoja_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value.ToString()))
            {
                string nombre;

                switch (dgvDetalleHoja.Columns[e.ColumnIndex].Name)
                {
                    case "CTO_CODIGO":
                        nombre = dsHojaRuta.CENTROS_TRABAJOS.FindByCTO_CODIGO(Convert.ToInt32(e.Value.ToString())).CTO_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "OPR_NUMERO":
                        nombre = dsHojaRuta.OPERACIONES.FindByOPR_NUMERO(Convert.ToInt32(e.Value.ToString())).OPR_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "USTCK_ORIGEN":
                        nombre = dsHojaRuta.UBICACIONES_STOCK.FindByUSTCK_NUMERO(Convert.ToInt32(e.Value)).USTCK_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "USTCK_DESTINO":
                        nombre = dsHojaRuta.UBICACIONES_STOCK.FindByUSTCK_NUMERO(Convert.ToInt32(e.Value)).USTCK_NOMBRE;
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
