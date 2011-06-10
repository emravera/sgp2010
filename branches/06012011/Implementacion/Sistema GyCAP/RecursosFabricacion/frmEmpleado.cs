using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Mensajes;

namespace GyCAP.UI.RecursosFabricacion
{
    public partial class frmEmpleado : Form
    {
        private static frmEmpleado _frmEmpleado = null;
        private Data.dsEmpleado dsEmpleado = new GyCAP.Data.dsEmpleado();
        private DataView dvEmpleado, dvEstadoEmpleado, dvEstadoEmpleadoBuscar, dvSectoresBuscar, dvSectores;
        private DataView dvCapacidadEmpleado, dvCapacidadAgregar;
        private enum estadoUI { inicio, nuevo, consultar, modificar, nuevoExterno};
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        private Sistema.ControlesUsuarios.AnimadorFormulario animador = new GyCAP.UI.Sistema.ControlesUsuarios.AnimadorFormulario();
        int codigoCxE = -1;

        #region Inicio

        public frmEmpleado()
        {
            InitializeComponent();
            SetSlide();
            InicializarDatos();
        }

        //Método para evitar la creación de más de una pantalla
        public static frmEmpleado Instancia
        {
            get
            {
                if (_frmEmpleado == null || _frmEmpleado.IsDisposed)
                {
                    _frmEmpleado = new frmEmpleado();
                }
                else
                {
                    _frmEmpleado.BringToFront();
                }
                return _frmEmpleado;
            }
            set
            {
                _frmEmpleado = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
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

                    if (dsEmpleado.EMPLEADOS.Rows.Count == 0)
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
                    estadoInterface = estadoUI.inicio;
                    slideControl.Selected = slideDatos;
                    btnZoomOut.PerformClick();
                    tcABM.SelectedTab = tpBuscar;
                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }
                    txtNombreBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    txtLegajo.Text = string.Empty;
                    txtNombre.Text = string.Empty;
                    txtApellido.Text = string.Empty;
                    sfFechaNac.SetFechaNull();
                    cboEstado.SetSelectedIndex(-1);
                    cboSector.SetSelectedIndex(-1);
                    txtLegajo.ReadOnly = false;
                    txtApellido.ReadOnly = false;
                    txtNombre.ReadOnly = false;
                    sfFechaNac.Enabled = true;                    
                    cboEstado.Enabled = true;
                    cboSector.Enabled = true;                    
                    dvCapacidadEmpleado.RowFilter = "E_CODIGO = " + -1;
                    pbImagen.Image = RecursosFabricacion.Properties.Resources.sinimagen;
                    btnZoomOut.PerformClick();
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    tcABM.SelectedTab = tpDatos;
                    txtLegajo.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtLegajo.Text = string.Empty;
                    txtNombre.Text = string.Empty;
                    txtApellido.Text = string.Empty;
                    sfFechaNac.SetFechaNull();
                    cboEstado.SetSelectedIndex(-1);
                    cboSector.SetSelectedIndex(-1);
                    txtLegajo.ReadOnly = false;
                    txtApellido.ReadOnly = false;
                    txtNombre.ReadOnly = false;
                    sfFechaNac.Enabled = true;
                    cboEstado.Enabled = true;
                    cboSector.Enabled = true;
                    dvCapacidadEmpleado.RowFilter = "E_CODIGO = " + -1;
                    pbImagen.Image = RecursosFabricacion.Properties.Resources.sinimagen;
                    btnZoomOut.PerformClick();
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.nuevo;
                    tcABM.SelectedTab = tpDatos;
                    txtLegajo.Focus();
                    break;
                case estadoUI.consultar:
                    txtLegajo.ReadOnly = true;
                    txtApellido.ReadOnly = true;
                    txtNombre.ReadOnly = true;
                    sfFechaNac.Enabled = false;
                    cboEstado.Enabled = false;
                    cboSector.Enabled = false;
                    btnNuevo.Enabled = true;
                    btnConsultar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    panelAcciones.Enabled = false;
                    estadoInterface = estadoUI.consultar;
                    btnZoomOut.PerformClick();
                    tcABM.SelectedTab = tpDatos;
                    btnVolver.Focus();
                    break;
                case estadoUI.modificar:
                    txtLegajo.ReadOnly = false;
                    txtApellido.ReadOnly = false;
                    txtNombre.ReadOnly = false;
                    sfFechaNac.Enabled = true;
                    cboEstado.Enabled = true;
                    cboSector.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    panelAcciones.Enabled = true;
                    estadoInterface = estadoUI.modificar;
                    btnZoomOut.PerformClick();
                    tcABM.SelectedTab = tpDatos;
                    txtLegajo.Focus();
                    break;
                default:
                    break;
            }
        }              

        private void InicializarDatos()
        {
            //Setea el nombre de la Lista
            gpbLista.Text = "Listado de " + this.Text;

            //Grilla busqueda
            dgvLista.AutoGenerateColumns = false;
            dgvLista.Columns.Add("E_CODIGO", "Código");
            dgvLista.Columns.Add("E_LEGAJO", "Legajo");
            dgvLista.Columns.Add("E_APELLIDO", "Apellido");
            dgvLista.Columns.Add("E_NOMBRE", "Nombre");
            dgvLista.Columns.Add("SEC_CODIGO", "Sector");
            dgvLista.Columns.Add("EE_CODIGO", "Estado");            

            dgvLista.Columns["E_CODIGO"].DataPropertyName = "E_CODIGO";
            dgvLista.Columns["E_LEGAJO"].DataPropertyName = "E_LEGAJO";
            dgvLista.Columns["E_APELLIDO"].DataPropertyName = "E_APELLIDO";
            dgvLista.Columns["E_NOMBRE"].DataPropertyName = "E_NOMBRE";
            dgvLista.Columns["SEC_CODIGO"].DataPropertyName = "SEC_CODIGO";
            dgvLista.Columns["EE_CODIGO"].DataPropertyName = "EE_CODIGO";

            dgvLista.Columns["E_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["E_CODIGO"].Visible = false;

            //Grilla capacidades asignadas a empleado
            dgvCapacidades.AutoGenerateColumns = false;
            dgvCapacidades.Columns.Add("CEMP_CODIGO", "Capacidades del empleado");
            dgvCapacidades.Columns.Add("CEMP_DESCRIPCION", "Descripción");
            dgvCapacidades.Columns["CEMP_DESCRIPCION"].DataPropertyName = "CEMP_CODIGO";
            dgvCapacidades.Columns["CEMP_CODIGO"].DataPropertyName = "CEMP_CODIGO";
            
            //Grilla Capacidades a agregar
            dgvListaCapacidadesAgregar.AutoGenerateColumns = false;
            dgvListaCapacidadesAgregar.Columns.Add("CEMP_NOMBRE", "Nombre");
            dgvListaCapacidadesAgregar.Columns.Add("CEMP_DESCRIPCION", "Descripción");
            dgvListaCapacidadesAgregar.Columns["CEMP_NOMBRE"].DataPropertyName = "CEMP_NOMBRE";
            dgvListaCapacidadesAgregar.Columns["CEMP_DESCRIPCION"].DataPropertyName = "CEMP_DESCRIPCION";        

            try
            {
                //Llena el Dataset con los estados, sectores y capacidades
                BLL.EstadoEmpleadoBLL.ObtenerTodos(dsEmpleado);
                BLL.SectorBLL.ObtenerTodos(dsEmpleado.SECTORES);
                BLL.CapacidadEmpleadoBLL.ObtenerTodos(dsEmpleado.CAPACIDAD_EMPLEADOS);
                BLL.CapacidadEmpleadoBLL.ObtenerCapacidadPorEmpleado(dsEmpleado);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex) { MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio); }

            //Creamos los dataview y los asignamos a las grillas
            dvEmpleado = new DataView(dsEmpleado.EMPLEADOS);
            dvEmpleado.Sort = "E_APELLIDO ASC, E_NOMBRE ASC";            
            dvCapacidadEmpleado = new DataView(dsEmpleado.CAPACIDADESXEMPLEADO);
            dvCapacidadAgregar = new DataView(dsEmpleado.CAPACIDAD_EMPLEADOS);
            dvCapacidadAgregar.Sort = "CEMP_NOMBRE ASC";
            dgvLista.DataSource = dvEmpleado;
            dgvCapacidades.DataSource = dvCapacidadEmpleado;
            dgvListaCapacidadesAgregar.DataSource = dvCapacidadAgregar;
                        
            //CARGA DE COMBOS
            //Creamos el Dataview y se lo asignamos al combo
            dvEstadoEmpleado = new DataView(dsEmpleado.ESTADO_EMPLEADOS);
            dvEstadoEmpleadoBuscar = new DataView(dsEmpleado.ESTADO_EMPLEADOS);            
            dvSectores = new DataView(dsEmpleado.SECTORES);
            dvSectoresBuscar = new DataView(dsEmpleado.SECTORES);
            string[] nombres = { "Legajo", "Nombre", "Apellido" };
            int[] valores = { BLL.EmpleadoBLL.BuscarPorLegajo, BLL.EmpleadoBLL.BuscarPorNombre, BLL.EmpleadoBLL.BuscarPorApellido };
            cboEstado.SetDatos(dvEstadoEmpleado, "EE_CODIGO", "EE_NOMBRE", "Seleccione...", false);
            cboBuscarEstado.SetDatos(dvEstadoEmpleadoBuscar, "ee_codigo", "ee_nombre", "--TODOS--", true);
            cboBuscarPor.SetDatos(nombres, valores, "Seleccione...", false);
            cboBuscarPor.SetSelectedValue(BLL.EmpleadoBLL.BuscarPorLegajo);
            cboSector.SetDatos(dvSectores, "SEC_CODIGO", "SEC_NOMBRE", "Seleccione...", false);
            cboSectorBuscar.SetDatos(dvSectoresBuscar, "SEC_CODIGO", "SEC_NOMBRE", "--TODOS--", true);
            //Seteo el maxlenght de los textbox para que no de error en la bd
            txtApellido.MaxLength = 80;
            txtNombre.MaxLength = 80;
            txtLegajo.MaxLength = 20;

            //Seteos para los controles de la imagen
            pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            ofdImagen.Filter = "Archivos de imágenes (*.bmp, *.gif , *.jpeg, *.png)|*.bmp;*.gif;*.jpg;*.png|Todos los archivos (*.*)|*.*";

            SetInterface(estadoUI.inicio);
        }

        #endregion

        #region Botones menu

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);           
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0) { SetInterface(estadoUI.consultar); }
            else { MensajesABM.MsjSinSeleccion("Empleado", MensajesABM.Generos.Masculino, this.Text); }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0) { SetInterface(estadoUI.modificar); }
            else { MensajesABM.MsjSinSeleccion("Empleado", MensajesABM.Generos.Masculino, this.Text); }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }        

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                if (MensajesABM.MsjConfirmaEliminarDatos("Empleado", MensajesABM.Generos.Masculino, this.Text) == DialogResult.Yes)
                {
                    try
                    {
                        //Obtenemos el codigo
                        long codigo = Convert.ToInt64(dvEmpleado[dgvLista.SelectedRows[0].Index]["e_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.EmpleadoBLL.Eliminar(codigo);
                        //Lo eliminamos de la tabla conjuntos del dataset
                        dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigo).Delete();
                        foreach (Data.dsEmpleado.CAPACIDADESXEMPLEADORow rowCE in (Data.dsEmpleado.CAPACIDADESXEMPLEADORow[])dsEmpleado.CAPACIDADESXEMPLEADO.Select("e_codigo = " + codigo))
                        {
                            rowCE.Delete();
                        }
                        dsEmpleado.EMPLEADOS.AcceptChanges();
                        dsEmpleado.CAPACIDADESXEMPLEADO.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        dsEmpleado.EMPLEADOS.RejectChanges();
                        dsEmpleado.CAPACIDADESXEMPLEADO.RejectChanges();
                        MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsEmpleado.EMPLEADOS.RejectChanges();
                        dsEmpleado.CAPACIDADESXEMPLEADO.RejectChanges();
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Empleado", MensajesABM.Generos.Masculino, this.Text);
            }
        }

        #endregion

        #region Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsEmpleado.EMPLEADOS.Clear();
                dsEmpleado.CAPACIDADESXEMPLEADO.Clear();
                BLL.EmpleadoBLL.ObtenerTodos(cboBuscarPor.GetSelectedValue(), txtNombreBuscar.Text, cboBuscarEstado.GetSelectedValue(), cboSectorBuscar.GetSelectedValue(), dsEmpleado);

                dvEmpleado.Table = dsEmpleado.EMPLEADOS;

                if (dsEmpleado.EMPLEADOS.Rows.Count == 0)
                {
                    MensajesABM.MsjBuscarNoEncontrado("Empleados", this.Text);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Búsqueda);                
            }
            finally { SetInterface(estadoUI.inicio); }
        }

        #endregion

        #region Datos

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo y selecciono algo en el combo
            if (Sistema.Validaciones.FormValidator.ValidarFormulario(this, GyCAP.UI.Sistema.Validaciones.FormValidator.GrillaOptions.FilaAgregada))
            {
                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    try
                    {                        
                        Data.dsEmpleado.EMPLEADOSRow rowEmpleado = dsEmpleado.EMPLEADOS.NewEMPLEADOSRow();
                        rowEmpleado.BeginEdit();
                        rowEmpleado.E_CODIGO = -1;
                        rowEmpleado.E_NOMBRE = txtNombre.Text;
                        rowEmpleado.E_APELLIDO = txtApellido.Text;
                        if (sfFechaNac.EsFechaNull()) { rowEmpleado.SetE_FECHANACIMIENTONull(); }
                        else { rowEmpleado.E_FECHANACIMIENTO = DateTime.Parse(sfFechaNac.GetFecha().ToString()); }
                        rowEmpleado.E_LEGAJO = txtLegajo.Text;
                        rowEmpleado.SEC_CODIGO = cboSector.GetSelectedValueInt();
                        rowEmpleado.EE_CODIGO = cboEstado.GetSelectedValueInt();
                        if (cboEstado.GetSelectedValueInt() == BLL.EstadoEmpleadoBLL.EstadoDeBaja && rowEmpleado.IsE_FECHA_BAJANull()) { rowEmpleado.E_FECHA_BAJA = BLL.DBBLL.GetFechaServidor(); }
                        else { rowEmpleado.SetE_FECHA_BAJANull(); }
                        rowEmpleado.E_FECHA_ALTA = BLL.DBBLL.GetFechaServidor();
                        rowEmpleado.SetE_FECHA_BAJANull();
                        //determinar si tiene foto - gonzalo
                        rowEmpleado.E_HAS_IMAGE = BLL.ImageRepository.WithImage;
                        rowEmpleado.EndEdit();
                        Image imagen = pbImagen.Image;
                        dsEmpleado.EMPLEADOS.AddEMPLEADOSRow(rowEmpleado);
                        int codigo = BLL.EmpleadoBLL.Insertar(dsEmpleado);
                        BLL.EmpleadoBLL.GuardarFoto(codigo, imagen);
                        rowEmpleado.BeginEdit();
                        rowEmpleado.E_CODIGO = codigo;
                        rowEmpleado.EndEdit();
                        dsEmpleado.EMPLEADOS.AcceptChanges();
                        dsEmpleado.CAPACIDADESXEMPLEADO.AcceptChanges();
                        imagen.Dispose();

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
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        dsEmpleado.EMPLEADOS.RejectChanges();
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsEmpleado.EMPLEADOS.RejectChanges();
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                    }
                }
                else
                {
                    try
                    {
                        //Está modificando una designacion
                        //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada                
                        int codigo = Convert.ToInt32(dvEmpleado[dgvLista.SelectedRows[0].Index]["e_codigo"]);
                        
                        //Segundo obtenemos los nuevos datos que ingresó el usuario
                        dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigo).BeginEdit();
                        dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigo).E_APELLIDO = txtApellido.Text.Trim();
                        if (!sfFechaNac.EsFechaNull()) { dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigo).E_FECHANACIMIENTO = DateTime.Parse(sfFechaNac.GetFecha().ToString()); }
                        else { dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigo).SetE_FECHANACIMIENTONull(); }
                        dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigo).E_LEGAJO = txtLegajo.Text.Trim();
                        dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigo).E_NOMBRE = txtNombre.Text.Trim();
                        dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigo).SEC_CODIGO = cboSector.GetSelectedValueInt();
                        dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigo).EE_CODIGO = cboEstado.GetSelectedValueInt();
                        if (cboEstado.GetSelectedValueInt() != BLL.EstadoEmpleadoBLL.EstadoDeBaja && dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigo).IsE_FECHA_BAJANull()) { dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigo).SetE_FECHA_BAJANull(); }
                        else { dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigo).E_FECHA_BAJA = BLL.DBBLL.GetFechaServidor(); }
                        dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigo).EndEdit();

                        //Lo actualizamos en la DB
                        BLL.EmpleadoBLL.Actualizar(dsEmpleado);
                        MensajesABM.MsjConfirmaGuardar("Empleado", this.Text, MensajesABM.Operaciones.Modificación);
                        dsEmpleado.EMPLEADOS.AcceptChanges();
                        dsEmpleado.CAPACIDADESXEMPLEADO.AcceptChanges();

                        BLL.EmpleadoBLL.GuardarFoto(codigo, pbImagen.Image);
                        
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);                        
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        dsEmpleado.EMPLEADOS.RejectChanges();
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        dsEmpleado.EMPLEADOS.RejectChanges();
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                    }
                }

                //recarga de la grilla
                dgvLista.Refresh();
            }
        }
        
        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0) { dgvLista.SelectedRows[0].Selected = false; }
            dsEmpleado.EMPLEADOS.RejectChanges();
            dsEmpleado.CAPACIDADESXEMPLEADO.RejectChanges();
            SetInterface(estadoUI.inicio);
        }

        #endregion

        #region Agregar capacidad

        private void btnAgregarCapacidad_Click(object sender, EventArgs e)
        {
            if (dgvListaCapacidadesAgregar.SelectedRows.Count > 0)
            {
                int codigoCapacidad = Convert.ToInt32(dvCapacidadAgregar[dgvListaCapacidadesAgregar.SelectedRows[0].Index]["cemp_codigo"]);
                int codigoEmpleado = 0;
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno) { codigoEmpleado = -1; }
                else { codigoEmpleado = Convert.ToInt32(dvEmpleado[dgvLista.SelectedRows[0].Index]["e_codigo"]); }
                if (dsEmpleado.CAPACIDADESXEMPLEADO.Select("e_codigo = " + codigoEmpleado + " AND cemp_codigo = " + codigoCapacidad).Count() == 0)
                {
                    Data.dsEmpleado.CAPACIDADESXEMPLEADORow row = dsEmpleado.CAPACIDADESXEMPLEADO.NewCAPACIDADESXEMPLEADORow();
                    row.BeginEdit();
                    row.CXE_CODIGO = codigoCxE--;
                    row.E_CODIGO = codigoEmpleado;
                    row.CEMP_CODIGO = codigoCapacidad;
                    row.EndEdit();
                    dsEmpleado.CAPACIDADESXEMPLEADO.AddCAPACIDADESXEMPLEADORow(row);
                }
                else { MensajesABM.MsjValidacion("El empleado ya posee la capacidad seleccionada.", this.Text); }
            }
            else { MensajesABM.MsjSinSeleccion("Capacidad", MensajesABM.Generos.Femenino, this.Text); }
        }
        
        private void btnHecho_Click(object sender, EventArgs e)
        {
            slideControl.BackwardTo("slideDatos");            
            panelAcciones.Enabled = true;
        }

        #endregion

        #region CellFormatting, RowEnter y control_enter

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() != String.Empty)
            {
                string nombre;
                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "SEC_CODIGO":
                        nombre = dsEmpleado.SECTORES.FindBySEC_CODIGO(Convert.ToInt32(e.Value)).SEC_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "EE_CODIGO":
                        nombre = dsEmpleado.ESTADO_EMPLEADOS.FindByEE_CODIGO(Convert.ToInt32(e.Value)).EE_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoEmpleado = Convert.ToInt32(dvEmpleado[e.RowIndex]["e_codigo"]);
            txtApellido.Text = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).E_APELLIDO;
            txtLegajo.Text = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).E_LEGAJO;
            txtNombre.Text = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).E_NOMBRE;
            cboSector.SetSelectedValue(int.Parse(dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).SEC_CODIGO.ToString()));
            cboEstado.SetSelectedValue(int.Parse(dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).EE_CODIGO.ToString()));
            if (dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).IsE_FECHANACIMIENTONull()) { sfFechaNac.SetFechaNull(); }
            else { sfFechaNac.SetFecha(dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).E_FECHANACIMIENTO); }
            dvCapacidadEmpleado.RowFilter = "E_CODIGO = " + codigoEmpleado;
            pbImagen.Image = BLL.EmpleadoBLL.ObtenerFoto(codigoEmpleado);
        }

        private void dgvCapacidades_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != String.Empty)
            {
                string nombre = string.Empty;
                switch (dgvCapacidades.Columns[e.ColumnIndex].Name)
                {
                    case "CEMP_CODIGO":
                        nombre = dsEmpleado.CAPACIDAD_EMPLEADOS.FindByCEMP_CODIGO(Convert.ToInt32(e.Value)).CEMP_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "CEMP_DESCRIPCION":
                        nombre = dsEmpleado.CAPACIDAD_EMPLEADOS.FindByCEMP_CODIGO(Convert.ToInt32(e.Value)).CEMP_DESCRIPCION;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }        

        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(typeof(TextBox))) { (sender as TextBox).SelectAll(); }
        }

        #endregion

        #region Foto empleado y Slide

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            Sistema.frmImagenZoom.Instancia.SetImagen(pbImagen.Image, "Foto del empleado");
            animador.SetFormulario(Sistema.frmImagenZoom.Instancia, this, Sistema.ControlesUsuarios.AnimadorFormulario.animacionDerecha, 300, true);
            animador.MostrarFormulario();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            animador.CerrarFormulario();
        }

        private void ActualizarImagen()
        {
            if (animador.EsVisible())
            {
                (animador.GetForm() as Sistema.frmImagenZoom).SetImagen(pbImagen.Image, "Foto del empleado");
            }
        }

        private void pbImagen_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            ActualizarImagen();
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

        private void btnImagen_Click(object sender, EventArgs e)
        {
            ofdImagen.ShowDialog();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            pbImagen.Image = RecursosFabricacion.Properties.Resources.sinimagen;
            ActualizarImagen();
        }

        private void ofdImagen_FileOk(object sender, CancelEventArgs e)
        {
            pbImagen.ImageLocation = ofdImagen.FileName;
        }

        private void btnNewCapacidad_Click(object sender, EventArgs e)
        {
            slideControl.ForwardTo("slideAgregar");
            panelAcciones.Enabled = false;
        }

        private void btnDeleteCapacidad_Click(object sender, EventArgs e)
        {
            if (dgvCapacidades.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Obtenemos el código
                int codigo = Convert.ToInt32(dvCapacidadEmpleado[dgvCapacidades.SelectedRows[0].Index]["cxe_codigo"]);
                //Lo borramos pero sólo del dataset
                dsEmpleado.CAPACIDADESXEMPLEADO.FindByCXE_CODIGO(codigo).Delete();
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Capacidad", MensajesABM.Generos.Femenino, this.Text);
            }
        }

        private void SetSlide()
        {
            slideDatos.Parent = slideControl;
            gbDatos.Parent = slideDatos;
            gbAgregar.Parent = slideAgregar;
            slideControl.AddSlide(slideAgregar);
            slideControl.AddSlide(slideDatos);
            slideControl.Selected = slideDatos;
        }

        private void dgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        #endregion

        
    }


}
