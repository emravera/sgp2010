using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Mantenimiento
{
    public partial class frmRepuestos : Form
    {
        private static frmRepuestos _frmRepuestos = null;
        private Data.dsMantenimiento dsMantenimiento = new GyCAP.Data.dsMantenimiento();
        private DataView dvRepuestos, dvComboTipoRepBuscar,dvCboTipoRep;
        private enum estadoUI { inicio, nuevo, consultar, modificar, nuevoExterno };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        #region Inicio

        public frmRepuestos()
        {
            InitializeComponent();

            //Setea el nombre de la Lista
            gpbLista.Text = "Listado de " + this.Text;

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("REP_CODIGO", "Código");
            dgvLista.Columns.Add("TREP_CODIGO", "Tipo");
            dgvLista.Columns.Add("REP_NOMBRE", "Repuesto");
            dgvLista.Columns.Add("REP_CANTIDADSTOCK", "Stock");
            dgvLista.Columns.Add("REP_COSTO", "Costo");
            dgvLista.Columns.Add("REP_DESCRIPCION", "Descripción");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns["REP_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["TREP_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["REP_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["REP_CANTIDADSTOCK"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["REP_COSTO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["REP_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["REP_CODIGO"].DataPropertyName = "REP_CODIGO";
            dgvLista.Columns["TREP_CODIGO"].DataPropertyName = "TREP_CODIGO";
            dgvLista.Columns["REP_NOMBRE"].DataPropertyName = "REP_NOMBRE";
            dgvLista.Columns["REP_CANTIDADSTOCK"].DataPropertyName = "REP_CANTIDADSTOCK";
            dgvLista.Columns["REP_COSTO"].DataPropertyName = "REP_COSTO";
            dgvLista.Columns["REP_DESCRIPCION"].DataPropertyName = "REP_DESCRIPCION";

            //Alineacion de los numeros y las fechas en la grilla
            dgvLista.Columns["REP_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["REP_CODIGO"].Visible = false;

            //Creamos el dataview y lo asignamos a la grilla
            dvRepuestos = new DataView(dsMantenimiento.REPUESTOS);
            dvRepuestos.Sort = "REP_NOMBRE ASC";
            dgvLista.DataSource = dvRepuestos;

            //Obtenemos los niveles
            try
            {
                BLL.TipoRepuestoBLL.ObtenerTodos(dsMantenimiento);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //CARGA DE COMBOS
            //Combo de la Busqueda
            //Creamos el Dataview y se lo asignamos al combo
            dvComboTipoRepBuscar = new DataView(dsMantenimiento.TIPOS_REPUESTOS);

            //Combos de Datos
            dvCboTipoRep = new DataView(dsMantenimiento.TIPOS_REPUESTOS);

            cboTipoRepBuscar.SetDatos(dvComboTipoRepBuscar, "TREP_CODIGO", "TREP_NOMBRE", "--TODOS--", true);
            cboTipoRep.SetDatos(dvCboTipoRep, "TREP_CODIGO", "TREP_NOMBRE", "Seleccione el nivel...", false);

            //Seteo el maxlenght de los textbox para que no de error en la bd
            txtDescripcion.MaxLength = 200;
            txtDescripcionBuscar.MaxLength = 200;
            txtNombre.MaxLength = 80;
            txtCosto.MaxLength = 10;

            //Seteamos el estado de la interfaz
            SetInterface(estadoUI.inicio);
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

                    if (dsMantenimiento.REPUESTOS.Rows.Count == 0)
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
                    break;
                case estadoUI.nuevo:
                    setControles(false);
                    cboTipoRep.SelectedIndex = -1;
                    txtDescripcion.Text = string.Empty;
                    txtNombre.Text = string.Empty;
                    txtCosto.Text = string.Empty;
                    //gbGuardarCancelar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcABM.SelectedTab = tpDatos;
                    cboTipoRep.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    setControles(false);
                    txtDescripcion.Text = string.Empty;
                    cboTipoRep.SelectedIndex = -1;
                    txtNombre.Text = string.Empty;
                    txtCosto.Text = string.Empty;
                    //gbGuardarCancelar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcABM.SelectedTab = tpDatos;
                    cboTipoRep.Focus();
                    break;
                case estadoUI.consultar:
                    setControles(true);
                    //gbGuardarCancelar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcABM.SelectedTab = tpDatos;
                    btnVolver.Focus();
                    break;
                case estadoUI.modificar:
                    setControles(false);
                    //gbGuardarCancelar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcABM.SelectedTab = tpDatos;
                    cboTipoRep.Focus();
                    break;
                default:
                    break;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
        }

        private void setControles(bool pValue)
        {
            txtDescripcion.ReadOnly = pValue;
            cboTipoRep.Enabled = !pValue;
            txtNombre.ReadOnly = pValue;
            txtCosto.ReadOnly = pValue;
        }

        //Método para evitar la creación de más de una pantalla
        public static frmRepuestos Instancia
        {
            get
            {
                if (_frmRepuestos == null || _frmRepuestos.IsDisposed )
                {
                    _frmRepuestos = new frmRepuestos();
                }
                else
                {
                    _frmRepuestos.BringToFront();
                }
                return _frmRepuestos;
            }
            set
            {
                _frmRepuestos = value;
            }
        }

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != String.Empty)
            {
                string nombre;
                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "TREP_CODIGO":
                        nombre = dsMantenimiento.TIPOS_REPUESTOS.FindByTREP_CODIGO(Convert.ToInt32(e.Value)).TREP_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.consultar);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.modificar);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int tipoRepuesto;
                tipoRepuesto = 0;
                if (cboTipoRepBuscar.SelectedIndex != -1)
                    tipoRepuesto = cboTipoRepBuscar.GetSelectedValueInt();

                dsMantenimiento.REPUESTOS.Clear();

                BLL.RepuestoBLL.ObtenerTodos(txtDescripcionBuscar.Text, tipoRepuesto, dsMantenimiento);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvRepuestos.Table = dsMantenimiento.REPUESTOS;

                if (dsMantenimiento.REPUESTOS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron repuestos con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                SetInterface(estadoUI.inicio);

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: repuestos - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo y selecciono algo en el combo
            if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
            {
                Entidades.Repuesto repuesto = new GyCAP.Entidades.Repuesto();
                repuesto.Tipo = new GyCAP.Entidades.TipoRepuesto();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando un nuevo Empleado
                    repuesto.Descripcion = txtDescripcion.Text.Trim();
                    repuesto.Nombre = txtNombre.Text.Trim();
                    repuesto.Costo = Convert.ToDecimal ( txtCosto.Text.Trim());
                    repuesto.Tipo.Codigo = cboTipoRep.GetSelectedValueInt();
                    
                    try
                    {
                        //Primero lo creamos en la db
                        repuesto.Codigo = BLL.RepuestoBLL.Insertar(repuesto);
                        //Ahora lo agregamos al dataset 
                        Data.dsMantenimiento.REPUESTOSRow rowRepuesto = dsMantenimiento.REPUESTOS.NewREPUESTOSRow();
                        //Indicamos que comienza la edición de la fila
                        rowRepuesto.BeginEdit();
                        rowRepuesto.REP_CODIGO = repuesto.Codigo;
                        rowRepuesto.REP_NOMBRE = repuesto.Nombre;
                        rowRepuesto.REP_DESCRIPCION = repuesto.Descripcion;
                        rowRepuesto.REP_COSTO = repuesto.Costo;
                        rowRepuesto.TREP_CODIGO = repuesto.Tipo.Codigo;

                        //Termina la edición de la fila
                        rowRepuesto.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsMantenimiento.REPUESTOS.AddREPUESTOSRow(rowRepuesto);
                        dsMantenimiento.REPUESTOS.AcceptChanges();

                        //Y por último seteamos el estado de la interfaz

                        //Vemos cómo se inició el formulario para determinar la acción a seguir
                        if (estadoInterface == estadoUI.nuevoExterno)
                        {
                            //Nuevo desde acceso directo, cerramos el formulario
                            btnSalir.PerformClick();
                        }
                        else
                        {
                            dgvLista.Refresh();

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
                    repuesto.Codigo = Convert.ToInt32(dvRepuestos[dgvLista.SelectedRows[0].Index]["rep_codigo"]);

                    //Segundo obtenemos los nuevos datos que ingresó el usuario
                    repuesto.Descripcion = txtDescripcion.Text.Trim();
                    repuesto.Nombre = txtNombre.Text.Trim();
                    repuesto.Costo = Convert.ToDecimal(txtCosto.Text.Trim());
                    repuesto.Tipo.Codigo = cboTipoRep.GetSelectedValueInt();

                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.RepuestoBLL.Actualizar(repuesto);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsMantenimiento.REPUESTOSRow rowRepuesto = dsMantenimiento.REPUESTOS.FindByREP_CODIGO(repuesto.Codigo);
                        //Indicamos que comienza la edición de la fila
                        rowRepuesto.BeginEdit();
                        rowRepuesto.REP_NOMBRE = repuesto.Nombre;
                        rowRepuesto.REP_DESCRIPCION = repuesto.Descripcion;
                        rowRepuesto.REP_COSTO = repuesto.Costo;
                        rowRepuesto.TREP_CODIGO = repuesto.Tipo.Codigo;

                        //Termina la edición de la fila
                        rowRepuesto.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsMantenimiento.REPUESTOS.AcceptChanges();
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
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar el repuesto seleccionado?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Lo eliminamos de la DB
                        int codigo = Convert.ToInt32(dvRepuestos[dgvLista.SelectedRows[0].Index]["REP_CODIGO"]);
                        BLL.RepuestoBLL.Eliminar(codigo);

                        //Lo eliminamos del dataset
                        dsMantenimiento.REPUESTOS.FindByREP_CODIGO(codigo).Delete();
                        dsMantenimiento.REPUESTOS.AcceptChanges();
                        btnVolver.PerformClick();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        MessageBox.Show(ex.Message, "Advertencia: Elemento en transacción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MessageBox.Show(ex.Message, "Error: " + this.Text + " - Eliminacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un repusto de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Eventos

        private void frmAverias_Load(object sender, EventArgs e)
        {
            if (tcABM.SelectedTab == tpBuscar)
            {
                btnBuscar.Focus();
            }
            else
            {
                cboTipoRep.Focus();
            }
        }
        
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            long codigo = Convert.ToInt64(dvRepuestos[e.RowIndex]["rep_codigo"]);
            txtNombre.Text = dsMantenimiento.REPUESTOS.FindByREP_CODIGO(codigo).REP_NOMBRE;
            cboTipoRep.SetSelectedValue(Convert.ToInt32(dsMantenimiento.REPUESTOS.FindByREP_CODIGO(codigo).TREP_CODIGO));
            txtCosto.Text = dsMantenimiento.REPUESTOS.FindByREP_CODIGO(codigo).REP_COSTO.ToString();
            txtDescripcion.Text = dsMantenimiento.REPUESTOS.FindByREP_CODIGO(codigo).REP_DESCRIPCION;

        }

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            txtDescripcion.SelectAll();
        }

        private void txtDescripcionBuscar_Enter(object sender, EventArgs e)
        {
            txtDescripcionBuscar.SelectAll();
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtNombre.SelectAll();
        }

        private void txtCosto_Enter(object sender, EventArgs e)
        {
            txtCosto.SelectAll();
        }

        #endregion

    }
}
