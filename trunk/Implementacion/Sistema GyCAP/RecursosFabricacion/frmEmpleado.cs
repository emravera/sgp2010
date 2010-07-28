using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.RecursosFabricacion
{
    public partial class frmEmpleado : Form
    {
        private static frmEmpleado _frmEmpleado = null;
        private Data.dsEmpleado dsEmpleado = new GyCAP.Data.dsEmpleado(); 
        private DataView dvEmpleado, dvEstadoEmpleado,dvEstadoEmpleadoBuscar, 
                         dvListaSectores, dvSectores, dvCapacidadEmpleado;
        private enum estadoUI { inicio, nuevo, consultar, modificar, };
        private estadoUI estadoInterface;

        public frmEmpleado()
        {
            InitializeComponent();

            //Setea el nombre de la Lista
            gpbLista.Text = "Listado de " + this.Text;

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("E_CODIGO", "Código");
            dgvLista.Columns.Add("E_LEGAJO", "Legajo");
            dgvLista.Columns.Add("E_APELLIDO", "Apellido");
            dgvLista.Columns.Add("E_NOMBRE", "Nombre");
            dgvLista.Columns.Add("SEC_CODIGO", "Sector");
            dgvLista.Columns.Add("EE_CODIGO", "Estado");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns["E_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["E_LEGAJO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["E_APELLIDO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["E_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["SEC_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["EE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["E_CODIGO"].DataPropertyName = "E_CODIGO";
            dgvLista.Columns["E_LEGAJO"].DataPropertyName = "E_LEGAJO";
            dgvLista.Columns["E_APELLIDO"].DataPropertyName = "E_APELLIDO";
            dgvLista.Columns["E_NOMBRE"].DataPropertyName = "E_NOMBRE";
            dgvLista.Columns["SEC_CODIGO"].DataPropertyName = "SEC_CODIGO";
            dgvLista.Columns["EE_CODIGO"].DataPropertyName = "EE_CODIGO";

            //Alineacion de los numeros y las fechas en la grilla
            dgvLista.Columns["E_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["E_CODIGO"].Visible = false;

            ////Agregamos las columnas
            //ColumnasGrillas columnas = new ColumnasGrillas();

            //columnas.Add("E_CODIGO", "Código",true);
            //columnas.Add("E_LEGAJO", "Legajo");
            //columnas.Add("E_APELLIDO", "Apellido");
            //columnas.Add("E_NOMBRE", "Nombre");
            //columnas.Add("SEC_CODIGO", "Sector");
            //columnas.Add("EE_CODIGO", "Estado");

            //dgvLista.Columnas = columnas;

            //Creamos el dataview y lo asignamos a la grilla
            dvEmpleado = new DataView(dsEmpleado.EMPLEADOS);
            dvEmpleado.Sort = "E_APELLIDO, E_NOMBRE ASC";
            dgvLista.DataSource = dvEmpleado;

            //Llena el Dataset con los estados
            BLL.EstadoEmpleadoBLL.ObtenerTodos(dsEmpleado);

            //Llena el Dataset con los Sectores
            BLL.SectorBLL.ObtenerTodos(dsEmpleado);

            //Carga de la Lista de Sectores
            dvListaSectores = new DataView(dsEmpleado.SECTORES);
      
            lvSectores.View = View.Details;
            lvSectores.FullRowSelect = true;
            lvSectores.MultiSelect = false;
            lvSectores.CheckBoxes = true;
            lvSectores.GridLines = true;
            lvSectores.Columns.Add("Sectores", 120);
            lvSectores.Columns.Add("Codigo", 0);
            if (dvListaSectores.Count != 0)
            {
                foreach (DataRowView dr in dvListaSectores)
                {
                    ListViewItem li = new ListViewItem(dr["SEC_NOMBRE"].ToString());
                    li.SubItems.Add(dr["SEC_CODIGO"].ToString());
                    li.Checked = true;
                    lvSectores.Items.Add(li);
                }
            }

            //Carga de la Lista de Sectores
            dvCapacidadEmpleado = new DataView(dsEmpleado.CAPACIDAD_EMPLEADO);

            lvCapacidadEmpleado.View = View.Details;
            lvCapacidadEmpleado.FullRowSelect = true;
            lvCapacidadEmpleado.MultiSelect = false;
            lvCapacidadEmpleado.CheckBoxes = true;
            lvCapacidadEmpleado.GridLines = true;
            lvCapacidadEmpleado.Columns.Add("Capacidad", 280);
            lvCapacidadEmpleado.Columns.Add("Codigo", 0);
            if (dvCapacidadEmpleado.Count != 0)
            {
                foreach (DataRowView dr in dvCapacidadEmpleado)
                {
                    ListViewItem li = new ListViewItem(dr["CEMP_NOMBRE"].ToString());
                    li.SubItems.Add(dr["CEMP_CODIGO"].ToString());
                    li.Checked = true;
                    lvCapacidadEmpleado.Items.Add(li);
                }
            }


            //CARGA DE COMBOS
            //Creamos el Dataview y se lo asignamos al combo
            dvEstadoEmpleadoBuscar = new DataView(dsEmpleado.ESTADO_EMPLEADOS);
            cboBuscarEstado.SetDatos(dvEstadoEmpleadoBuscar, "ee_codigo", "ee_nombre", "-- TODOS --",true);

            cboBuscarPor.Items.Add("Legajo");
            cboBuscarPor.Items.Add("Nombre");
            cboBuscarPor.Items.Add("Apellido");
            cboBuscarPor.SelectedIndex = 0;

            //Combo de Datos
            dvEstadoEmpleado = new DataView(dsEmpleado.ESTADO_EMPLEADOS);
            Sistema.FuncionesAuxiliares.llenarCombos(dvEstadoEmpleado, cboEstado, "EE_CODIGO", "EE_NOMBRE");

            dvSectores = new DataView(dsEmpleado.SECTORES);
            Sistema.FuncionesAuxiliares.llenarCombos(dvSectores, cboSector, "SEC_CODIGO", "SEC_NOMBRE");

            //Llenar listView
            dvListaSectores = new DataView(dsEmpleado.SECTORES);

            //Seteo el maxlenght de los textbox para que no de error en la bd
            txtApellido.MaxLength = 80;
            txtNombre.MaxLength = 80;
            txtLegajo.MaxLength = 20;
            txtTelefono.MaxLength = 15;            

            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
        }

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
                        txtNombreBuscar.Focus();
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
                    setBotones(false);
                    txtNombre.Text = string.Empty;
                    txtApellido.Text = string.Empty;
                    txtLegajo.Text = string.Empty;
                    txtTelefono.Text = string.Empty;
                    cboEstado.SelectedIndex = 0;
                    cboSector.SelectedIndex = 0;
                    

                    //gbGuardarCancelar.Enabled = true;
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
                    setBotones(true);
                    //gbGuardarCancelar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcABM.SelectedTab = tpDatos;
                    btnVolver.Focus();
                    break;
                case estadoUI.modificar:
                    setBotones(false);
                    //gbGuardarCancelar.Enabled = true;
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

        private void setBotones(bool pValue) 
        {
            txtApellido.ReadOnly = pValue;
            txtNombre.ReadOnly = pValue;
            //sfFechaNac.Enabled != pValue;
            //txtFechaNac.ReadOnly = pValue;
            txtLegajo.ReadOnly = pValue;
            txtTelefono.ReadOnly = pValue;
            cboEstado.Enabled = ! pValue;
            cboSector.Enabled = ! pValue;
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

        #endregion

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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            Entidades.Empleado empleado = new GyCAP.Entidades.Empleado();
            Entidades.Sector sector = new GyCAP.Entidades.Sector();
            Entidades.EstadoEmpleado estadoEmpleado = new GyCAP.Entidades.EstadoEmpleado();


            //Revisamos que está haciendo
            if (estadoInterface == estadoUI.nuevo ) //|| estadoInterface == estadoUI.nuevoExterno)
            {
                //Está cargando un nuevo Empleado
                empleado.Apellido = txtApellido.Text.Trim();
                empleado.FechaNacimiento = DateTime.Parse("03/11/1980");
                empleado.Legajo = txtLegajo.Text.Trim();
                empleado.Nombre = txtNombre.Text.Trim();
                empleado.Telefono = txtTelefono.Text.Trim();
                empleado.FechaAlta = BLL.DBBLL.GetFechaServidor();

                //Creo el objeto Sector y despues lo asigno
                int idSector = Convert.ToInt32(cboSector.SelectedValue);
                sector.Codigo = Convert.ToInt32(dsEmpleado.SECTORES.FindBySEC_CODIGO(idSector).SEC_CODIGO);

                //Asigno el Sector creado al Empleado 
                empleado.Sector = sector;

                //Creo el objeto Estado y despues lo asigno
                int idEstado= Convert.ToInt32(cboEstado.SelectedValue);
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
                    rowEmpleado.E_CODIGO = empleado.Codigo;
                    rowEmpleado.E_NOMBRE = empleado.Nombre;
                    rowEmpleado.E_APELLIDO = empleado.Apellido;

                    if (empleado.FechaNacimiento == null)
                    {
                        rowEmpleado.SetE_FECHANACIMIENTONull();
                    }
                    else 
                    {
                        rowEmpleado.E_FECHANACIMIENTO = DateTime.Parse( empleado.FechaNacimiento.ToString())  ;
                    }
                    rowEmpleado.E_LEGAJO = empleado.Legajo;
                    rowEmpleado.E_TELEFONO = empleado.Telefono;
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
                    /*if (estadoInterface == estadoUI.nuevoExterno)
                    {
                        //Nuevo desde acceso directo, cerramos el formulario
                        btnSalir.PerformClick();
                    }
                    else
                    {
                        //Nuevo desde el mismo formulario, volvemos a la pestaña buscar
                        SetInterface(estadoUI.inicio);
                    }*/

                    SetInterface(estadoUI.inicio);

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
                empleado.Telefono = txtTelefono.Text.Trim();

                //Creo el objeto Sector y despues lo asigno
                int idSector = Convert.ToInt32(cboSector.SelectedValue);
                sector.Codigo = Convert.ToInt32(dsEmpleado.SECTORES.FindBySEC_CODIGO(idSector).SEC_CODIGO);

                //Asigno el Sector creado al Empleado 
                empleado.Sector = sector;

                //Creo el objeto Estado y despues lo asigno
                int idEstado= Convert.ToInt32(cboEstado.SelectedValue);
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
                    rowEmpleado.E_CODIGO = empleado.Codigo;
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
                    rowEmpleado.E_TELEFONO = empleado.Telefono;
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ListView.CheckedListViewItemCollection chequeados = lvSectores.CheckedItems;
                string cadSectores = string.Empty;

                if (chequeados.Count == 0)
                {
                    MessageBox.Show("Seleccione al menos un Sector.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    foreach (ListViewItem item in chequeados)
                    {
                        if (cadSectores == string.Empty)
                        {
                            cadSectores = item.SubItems[1].Text;
                        }
                        else
                        {
                            cadSectores += ", " + item.SubItems[1].Text;
                        }
                    }

                    dsEmpleado.EMPLEADOS.Clear();
                    BLL.EmpleadoBLL.ObtenerTodos(cboBuscarPor.Text, txtNombreBuscar.Text, cboBuscarEstado.GetSelectedValueInt(), cadSectores, dsEmpleado);
                    //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                    //por una consulta a la BD
                    dvEmpleado.Table = dsEmpleado.EMPLEADOS;
                    if (dsEmpleado.EMPLEADOS.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron Empleados con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    SetInterface(estadoUI.inicio);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Empleados - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

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

        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            btnConsultar.PerformClick();
        }

        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoEmpleado = Convert.ToInt32(dvEmpleado[e.RowIndex]["e_codigo"]);
            txtApellido.Text = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).E_APELLIDO;
            txtLegajo.Text = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).E_LEGAJO;
            txtNombre.Text = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).E_NOMBRE;
            txtTelefono.Text = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).E_TELEFONO;
            cboSector.SelectedValue = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).SEC_CODIGO;
            cboEstado.SelectedValue = dsEmpleado.EMPLEADOS.FindByE_CODIGO(codigoEmpleado).EE_CODIGO;  

        }
        
    }


}
