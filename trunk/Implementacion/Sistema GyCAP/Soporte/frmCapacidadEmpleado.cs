using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Soporte
{
    public partial class frmCapacidadEmpleado : Form
    {
        private static frmCapacidadEmpleado _frmABM = null;
        private Data.dsCapacidadEmpleado dsCapacidadEmpleado = new GyCAP.Data.dsCapacidadEmpleado();
        private DataView dvCapacidadEmpleado;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        public frmCapacidadEmpleado()
        {
            InitializeComponent();

            //Setea el nombre de la Lista
            gpbLista.Text = "Listado de " + this.Text;

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;

            //Agregamos las columnas
            dgvLista.Columns.Add("CEMP_CODIGO", "Código");
            dgvLista.Columns.Add("CEMP_NOMBRE", "Nombre");
            dgvLista.Columns.Add("CEMP_DESCRIPCION", "Descripción");

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["CEMP_CODIGO"].DataPropertyName = "CEMP_CODIGO";
            dgvLista.Columns["CEMP_NOMBRE"].DataPropertyName = "CEMP_NOMBRE";
            dgvLista.Columns["CEMP_DESCRIPCION"].DataPropertyName = "CEMP_DESCRIPCION";

            //Oculta la columna que contiene los encabezados
            dgvLista.RowHeadersVisible = false;

            //Setemaos las columnas
            dgvLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvLista.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Alineacion de los numeros y las fechas en la grilla
            dgvLista.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns[0].Visible = false;

            //Creamos el dataview y lo asignamos a la grilla
            dvCapacidadEmpleado = new DataView(dsCapacidadEmpleado.CAPACIDAD_EMPLEADOS);
            dvCapacidadEmpleado.Sort = "CEMP_NOMBRE ASC";
            dgvLista.DataSource = dvCapacidadEmpleado;

            //Seteo el maxlenght de los textbox para que no de error en la bd
            txtDescripcion.MaxLength = 250;
            txtNombre.MaxLength = 80;

            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);

        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
        }

        #region Servicios

        //Setea la pantalla de acuerdo al estado en que se encuentre
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsCapacidadEmpleado.CAPACIDAD_EMPLEADOS.Rows.Count == 0)
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
                    break;
                case estadoUI.nuevo:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    txtDescripcion.Text = string.Empty;
                    //gbGuardarCancelar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcABM.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    txtDescripcion.Text = string.Empty;
                    //gbGuardarCancelar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcABM.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                case estadoUI.consultar:
                    txtNombre.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    //gbGuardarCancelar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcABM.SelectedTab = tpDatos;
                    btnVolver.Focus();
                    break;
                case estadoUI.modificar:
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    //gbGuardarCancelar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcABM.SelectedTab = tpDatos;
                    txtNombre.Focus();
                    break;
                default:
                    break;
            }
        }

        //Método para evitar la creación de más de una pantalla
        public static frmCapacidadEmpleado Instancia
        {
            get
            {
                if (_frmABM == null || _frmABM.IsDisposed)
                {
                    _frmABM = new frmCapacidadEmpleado();
                }
                else
                {
                    _frmABM.BringToFront();
                }
                return _frmABM;
            }
            set
            {
                _frmABM = value;
            }
        }

        //Evita que el formulario se cierre desde la cruz
        //private void frmTerminacion_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (e.CloseReason == CloseReason.UserClosing)
        //    {
        //        e.Cancel = true;
        //    }
        //}

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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar la capacidad seleccionada?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Creamos el objeto 
                        int codigo = Convert.ToInt32(dvCapacidadEmpleado[dgvLista.SelectedRows[0].Index]["CEMP_CODIGO"]);
                        //Lo eliminamos de la DB
                        BLL.CapacidadEmpleadoBLL.Eliminar(codigo);
                        //Lo eliminamos del dataset
                        dsCapacidadEmpleado.CAPACIDAD_EMPLEADOS.FindByCEMP_CODIGO(codigo).Delete();
                        dsCapacidadEmpleado.CAPACIDAD_EMPLEADOS.AcceptChanges();
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        //MessageBox.Show(ex.Message);
                        MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        //MessageBox.Show(ex.Message);
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una " + this.Text + " de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dsCapacidadEmpleado.CAPACIDAD_EMPLEADOS.Clear();
                BLL.CapacidadEmpleadoBLL.ObtenerTodos(txtNombreBuscar.Text, dsCapacidadEmpleado);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvCapacidadEmpleado.Table = dsCapacidadEmpleado.CAPACIDAD_EMPLEADOS;
                if (dsCapacidadEmpleado.CAPACIDAD_EMPLEADOS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Capacidades de Empleado con el nombre ingresado.","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Information );
                }

                SetInterface(estadoUI.inicio);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message);
                SetInterface(estadoUI.inicio);
            }   
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo
            if (txtNombre.Text != String.Empty)
            {
                Entidades.CapacidadEmpleado capacidadEmpleado = new GyCAP.Entidades.CapacidadEmpleado();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando una terminacion nuevo
                    capacidadEmpleado.Nombre = txtNombre.Text;
                    capacidadEmpleado.Descripcion = txtDescripcion.Text;
                    try
                    {
                        //Primero lo creamos en la db
                        capacidadEmpleado.Codigo = BLL.CapacidadEmpleadoBLL.Insertar(capacidadEmpleado);
                        //Ahora lo agregamos al dataset
                        Data.dsCapacidadEmpleado.CAPACIDAD_EMPLEADOSRow rowCapacidadEmpleado = dsCapacidadEmpleado.CAPACIDAD_EMPLEADOS.NewCAPACIDAD_EMPLEADOSRow();
                        //Indicamos que comienza la edición de la fila
                        rowCapacidadEmpleado.BeginEdit();
                        rowCapacidadEmpleado.CEMP_CODIGO = capacidadEmpleado.Codigo;
                        rowCapacidadEmpleado.CEMP_NOMBRE = capacidadEmpleado.Nombre;
                        rowCapacidadEmpleado.CEMP_DESCRIPCION = capacidadEmpleado.Descripcion;
                        //Termina la edición de la fila
                        rowCapacidadEmpleado.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsCapacidadEmpleado.CAPACIDAD_EMPLEADOS.AddCAPACIDAD_EMPLEADOSRow(rowCapacidadEmpleado);
                        dsCapacidadEmpleado.CAPACIDAD_EMPLEADOS.AcceptChanges();
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
                    //Está modificando una terminacion
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada
                    capacidadEmpleado.Codigo = Convert.ToInt32(dvCapacidadEmpleado[dgvLista.SelectedRows[0].Index]["cemp_codigo"]);
                    //Segundo obtenemos el nuevo nombre que ingresó el usuario
                    capacidadEmpleado.Nombre = txtNombre.Text;
                    capacidadEmpleado.Descripcion = txtDescripcion.Text;
                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.CapacidadEmpleadoBLL.Actualizar(capacidadEmpleado);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsCapacidadEmpleado.CAPACIDAD_EMPLEADOSRow rowCapacidadEmpleado = dsCapacidadEmpleado.CAPACIDAD_EMPLEADOS.FindByCEMP_CODIGO(capacidadEmpleado.Codigo);
                        rowCapacidadEmpleado.BeginEdit();
                        rowCapacidadEmpleado.CEMP_NOMBRE = txtNombre.Text;
                        rowCapacidadEmpleado.CEMP_DESCRIPCION = txtDescripcion.Text;
                        rowCapacidadEmpleado.EndEdit();
                        dsCapacidadEmpleado.CAPACIDAD_EMPLEADOS.AcceptChanges();
                        //Avisamos que estuvo todo ok
                        MessageBox.Show("Elemento actualizado correctamente.", "Aviso");
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

        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            if (dsCapacidadEmpleado.CAPACIDAD_EMPLEADOS.Rows.Count != 0)
            {
                btnConsultar.PerformClick();
            }   
        }

        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoCapacidadEmpleado = Convert.ToInt32(dvCapacidadEmpleado[e.RowIndex]["CEMP_CODIGO"]);
            txtNombre.Text = dsCapacidadEmpleado.CAPACIDAD_EMPLEADOS.FindByCEMP_CODIGO(codigoCapacidadEmpleado).CEMP_NOMBRE ;
            txtDescripcion.Text = dsCapacidadEmpleado.CAPACIDAD_EMPLEADOS.FindByCEMP_CODIGO(codigoCapacidadEmpleado).CEMP_DESCRIPCION;
        }

        private void txtNombreBuscar_Enter(object sender, EventArgs e)
        {
            txtNombreBuscar.SelectAll();
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtNombre.SelectAll();
        }

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            txtDescripcion.SelectAll();
        }

        private void frmCapacidadEmpleado_Load(object sender, EventArgs e)
        {

        }
    }
}
