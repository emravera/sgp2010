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

        #region Inicio

        public frmEmpleado()
        {
            InitializeComponent();

            InicializarDatos();            

            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
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
                    tcABM.SelectedTab = tpBuscar;
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
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
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
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
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
                    estadoInterface = estadoUI.consultar;
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
                    estadoInterface = estadoUI.modificar;
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

            dgvLista.Columns["E_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["E_LEGAJO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["E_APELLIDO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["E_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["SEC_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["EE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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
            dgvCapacidades.Columns["CEMP_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvCapacidades.Columns["CEMP_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvCapacidades.Columns["CEMP_DESCRIPCION"].DataPropertyName = "CEMP_DESCRIPCION";
            dgvCapacidades.Columns["CEMP_CODIGO"].DataPropertyName = "CEMP_CODIGO";
            
            //Grilla Capacidades a agregar
            dgvListaCapacidadesAgregar.AutoGenerateColumns = false;
            dgvListaCapacidadesAgregar.Columns.Add("CEMP_CODIGO", "Capacidad");
            dgvListaCapacidadesAgregar.Columns.Add("CEMP_DESCRIPCION", "Descripción");
            dgvListaCapacidadesAgregar.Columns["CEMP_CODIGO"].DataPropertyName = "CEMP_CODIGO";
            dgvListaCapacidadesAgregar.Columns["CEMP_DESCRIPCION"].DataPropertyName = "CEMP_DESCRIPCION";
            dgvListaCapacidadesAgregar.Columns["CEMP_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvListaCapacidadesAgregar.Columns["CEMP_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;            

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
        }

        #endregion

        #region Botones menu

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
            dvCapacidadEmpleado = new DataView();
            dgvCapacidades.DataSource = dvCapacidadEmpleado;            
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.consultar);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.modificar);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo y selecciono algo en el combo
            if (txtLegajo.Text != String.Empty && txtApellido.Text != String.Empty 
                && cboSector.SelectedIndex != -1 && cboEstado.SelectedIndex != -1)
            {

                Entidades.Empleado empleado = new GyCAP.Entidades.Empleado();
                Entidades.SectorTrabajo sector = new GyCAP.Entidades.SectorTrabajo();
                Entidades.EstadoEmpleado estadoEmpleado = new GyCAP.Entidades.EstadoEmpleado();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando un nuevo Empleado
                    empleado.Apellido = txtApellido.Text.Trim();
                    empleado.FechaNacimiento = DateTime.Parse(sfFechaNac.GetFecha().ToString()); //DateTime.Parse("03/11/1980");
                    empleado.Legajo = txtLegajo.Text.Trim();
                    empleado.Nombre = txtNombre.Text.Trim();
                    empleado.FechaAlta = BLL.DBBLL.GetFechaServidor();

                    //Creo el objeto Sector y despues lo asigno
                    int idSector = Convert.ToInt32(cboSector.SelectedValue);
                    sector.Codigo = Convert.ToInt32(dsEmpleado.SECTORES.FindBySEC_CODIGO(idSector).SEC_CODIGO);

                    //Asigno el Sector creado al Empleado 
                    empleado.Sector = sector;

                    //Creo el objeto Estado y despues lo asigno
                    int idEstado = Convert.ToInt32(cboEstado.SelectedValue);
                    estadoEmpleado.Codigo = Convert.ToInt32(dsEmpleado.ESTADO_EMPLEADOS.FindByEE_CODIGO(idEstado).EE_CODIGO);

                    //Asigno el Sector creado al Empleado 
                    empleado.Estado = estadoEmpleado;
                    
                    try
                    {
                        //Primero lo creamos en la db
                        empleado.Codigo = BLL.EmpleadoBLL.Insertar(empleado);
                        //Ahora lo agregamos al dataset
                        Data.dsEmpleado.EMPLEADOSRow rowEmpleado = dsEmpleado.EMPLEADOS.NewEMPLEADOSRow();
                        //Indicamos que comienza la edición de la fila
                        rowEmpleado.BeginEdit();
                        //rowEmpleado.E_CODIGO = empleado.Codigo;
                        rowEmpleado.E_NOMBRE = empleado.Nombre;
                        rowEmpleado.E_APELLIDO = empleado.Apellido;

                        if (empleado.FechaNacimiento == null)
                        {
                            rowEmpleado.SetE_FECHANACIMIENTONull();
                        }
                        else
                        {
                            rowEmpleado.E_FECHANACIMIENTO = DateTime.Parse(empleado.FechaNacimiento.ToString());
                        }
                        rowEmpleado.E_LEGAJO = empleado.Legajo;
                        rowEmpleado.SEC_CODIGO = empleado.Sector.Codigo;
                        rowEmpleado.EE_CODIGO = empleado.Estado.Codigo;
                        rowEmpleado.E_FECHA_ALTA = empleado.FechaAlta;

                        //Termina la edición de la fila
                        rowEmpleado.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsEmpleado.EMPLEADOS.AddEMPLEADOSRow(rowEmpleado);
                        dsEmpleado.EMPLEADOS.AcceptChanges();

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
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Está modificando una designacion
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada                
                    empleado.Codigo = Convert.ToInt32(dvEmpleado[dgvLista.SelectedRows[0].Index]["e_codigo"]);

                    //Segundo obtenemos los nuevos datos que ingresó el usuario
                    empleado.Apellido = txtApellido.Text.Trim();
                    empleado.FechaNacimiento = DateTime.Parse("03/11/1980");
                    empleado.Legajo = txtLegajo.Text.Trim();
                    empleado.Nombre = txtNombre.Text.Trim();
                    
                    //Creo el objeto Sector y despues lo asigno
                    int idSector = Convert.ToInt32(cboSector.SelectedValue);
                    sector.Codigo = Convert.ToInt32(dsEmpleado.SECTORES.FindBySEC_CODIGO(idSector).SEC_CODIGO);

                    //Asigno el Sector creado al Empleado 
                    empleado.Sector = sector;

                    //Creo el objeto Estado y despues lo asigno
                    int idEstado = Convert.ToInt32(cboEstado.SelectedValue);
                    estadoEmpleado.Codigo = Convert.ToInt32(dsEmpleado.ESTADO_EMPLEADOS.FindByEE_CODIGO(idEstado).EE_CODIGO);

                    //Asigno el Sector creado al Empleado 
                    empleado.Estado = estadoEmpleado;

                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.EmpleadoBLL.Actualizar(empleado);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsEmpleado.EMPLEADOSRow rowEmpleado = dsEmpleado.EMPLEADOS.FindByE_CODIGO(empleado.Codigo);
                        //Indicamos que comienza la edición de la fila
                        rowEmpleado.BeginEdit();
                        //rowEmpleado.E_CODIGO = empleado.Codigo;
                        rowEmpleado.E_NOMBRE = empleado.Nombre;
                        rowEmpleado.E_APELLIDO = empleado.Apellido;
                        if (empleado.FechaNacimiento == null)
                        {
                            rowEmpleado.SetE_FECHANACIMIENTONull();
                        }
                        else
                        {
                            rowEmpleado.E_FECHANACIMIENTO = DateTime.Parse(empleado.FechaNacimiento.ToString());
                        }
                        rowEmpleado.E_LEGAJO = empleado.Legajo;
                        rowEmpleado.SEC_CODIGO = empleado.Sector.Codigo;
                        rowEmpleado.EE_CODIGO = empleado.Estado.Codigo;
                        //Termina la edición de la fila
                        rowEmpleado.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsEmpleado.EMPLEADOS.AcceptChanges();
                        //Avisamos que estuvo todo ok
                        MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //recarga de la grilla
                dgvLista.Refresh();

            }
            else
            {
                MessageBox.Show("Debe completar los datos.", "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar el Empleado seleccionado?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Obtenemos el codigo
                        long codigo = Convert.ToInt64(dvEmpleado[dgvLista.SelectedRows[0].Index]["e_codigo"]);
                        //Lo eliminamos de la DB
                        BLL.EmpleadoBLL.Eliminar(codigo);
                        //Lo eliminamos de la tabla conjuntos del dataset
                        dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigo).Delete();
                        dsEmpleado.EMPLEADOS.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: Empleado - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: Empleado - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Empleado de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                BLL.CapacidadEmpleadoBLL.ObtenerCapacidadPorEmpleado(dsEmpleado);
                
                dvEmpleado.Table = dsEmpleado.EMPLEADOS;

                if (dsEmpleado.EMPLEADOS.Rows.Count == 0)
                {
                    MensajesABM.MsjBuscarNoEncontrado("Empleados", this.Text);
                }
                SetInterface(estadoUI.inicio);
                
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Búsqueda);
                SetInterface(estadoUI.inicio);
            }
        }

        #endregion

        #region Datos

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
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
            sfFechaNac.SetFecha(dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).E_FECHANACIMIENTO);
            dvCapacidadEmpleado.RowFilter = "E_CODIGO = " + codigoEmpleado;
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
                    default:
                        break;
                }
            }
        }        

        private void control_Enter(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(typeof(TextBox))) { (sender as TextBox).SelectAll(); }
        }

        private void dgvListaCapacidadesAgregar_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != String.Empty)
            {
                string nombre = string.Empty;
                switch (dgvListaCapacidadesAgregar.Columns[e.ColumnIndex].Name)
                {
                    case "CEMP_CODIGO":
                        nombre = dsEmpleado.CAPACIDAD_EMPLEADOS.FindByCEMP_CODIGO(Convert.ToInt32(e.Value)).CEMP_NOMBRE;
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
