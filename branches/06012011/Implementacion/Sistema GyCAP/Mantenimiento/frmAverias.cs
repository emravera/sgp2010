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
    public partial class frmAverias : Form
    {
        private static frmAverias _frmAverias = null;
        private Data.dsMantenimiento dsMantenimiento = new GyCAP.Data.dsMantenimiento();
        private DataView dvAverias, dvComboCriticidadBuscar,dvCboCriticidad, dvCboMaquina;
        private enum estadoUI { inicio, nuevo, consultar, modificar, nuevoExterno };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        #region Inicio

        public frmAverias()
        {
            InitializeComponent();

            //Setea el nombre de la Lista
            gpbLista.Text = "Listado de " + this.Text;

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("AVE_CODIGO", "Código");
            dgvLista.Columns.Add("MAQ_CODIGO", "Maquina");
            dgvLista.Columns.Add("NCRI_CODIGO", "Criticidad");
            dgvLista.Columns.Add("AVE_DESCRIPCION", "Descripción");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns["AVE_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["MAQ_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["NCRI_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["AVE_DESCRIPCION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["AVE_CODIGO"].DataPropertyName = "AVE_CODIGO";
            dgvLista.Columns["MAQ_CODIGO"].DataPropertyName = "MAQ_CODIGO";
            dgvLista.Columns["NCRI_CODIGO"].DataPropertyName = "NCRI_CODIGO";
            dgvLista.Columns["AVE_DESCRIPCION"].DataPropertyName = "AVE_DESCRIPCION";

            //Alineacion de los numeros y las fechas en la grilla
            dgvLista.Columns["AVE_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["AVE_CODIGO"].Visible = false;

            //Creamos el dataview y lo asignamos a la grilla
            dvAverias = new DataView(dsMantenimiento.AVERIAS);
            dvAverias.Sort = "AVE_DESCRIPCION ASC";
            dgvLista.DataSource = dvAverias;

            //Obtenemos los niveles
            try
            {
                BLL.NivelCriticidadBLL.ObtenerTodos(dsMantenimiento);
                BLL.MaquinaBLL.ObtenerMaquinas(dsMantenimiento);  
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: " + this.Text + " - Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //CARGA DE COMBOS
            //Combo de la Busqueda
            //Creamos el Dataview y se lo asignamos al combo
            dvComboCriticidadBuscar = new DataView(dsMantenimiento.NIVELES_CRITICIDAD);
            cboCriticidadBuscar.DataSource = dvComboCriticidadBuscar;
            cboCriticidadBuscar.DisplayMember = "NCRI_NOMBRE";
            cboCriticidadBuscar.ValueMember = "NCRI_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cboCriticidadBuscar.SelectedIndex = -1;
            cboCriticidadBuscar.DropDownStyle = ComboBoxStyle.DropDownList;

            //Combos de Datos
            dvCboMaquina  = new DataView(dsMantenimiento.MAQUINAS);
            cboMaquina.DataSource = dvCboMaquina;
            cboMaquina.DisplayMember = "MAQ_NOMBRE";
            cboMaquina.ValueMember = "MAQ_CODIGO";
            //Para que el combo no quede selecionado cuando arranca y que sea una lista
            cboMaquina.SelectedIndex = -1;
            cboMaquina.DropDownStyle = ComboBoxStyle.DropDownList;


            //Seteo el maxlenght de los textbox para que no de error en la bd
            txtDescripcion.MaxLength = 500;
            txtDescripcionBuscar.MaxLength = 500; 
            
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

                    if (dsMantenimiento.AVERIAS.Rows.Count == 0)
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
                    cboMaquina.SelectedIndex = -1;
                    cboCriticidadd.SelectedIndex = -1;
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
                    cboMaquina.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    setControles(false);
                    txtDescripcion.Text = string.Empty;
                    cboMaquina.SelectedIndex = -1;
                    cboCriticidadd.SelectedIndex = -1;    

                    //gbGuardarCancelar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcABM.SelectedTab = tpDatos;
                    cboMaquina.Focus();
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
                    cboMaquina.Focus();
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
            cboMaquina.Enabled = !pValue;
            cboCriticidadd.Enabled = !pValue;
        }

        //Método para evitar la creación de más de una pantalla
        public static frmAverias Instancia
        {
            get
            {
                if (_frmAverias == null || _frmAverias.IsDisposed )
                {
                    _frmAverias = new frmAverias();
                }
                else
                {
                    _frmAverias.BringToFront();
                }
                return _frmAverias;
            }
            set
            {
                _frmAverias = value;
            }
        }

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != String.Empty)
            {
                string nombre;
                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "MAQ_CODIGO":
                        nombre = dsMantenimiento.MAQUINAS.FindByMAQ_CODIGO(Convert.ToInt64(e.Value)).MAQ_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "NCRI_CODIGO":
                        nombre = dsMantenimiento.NIVELES_CRITICIDAD.FindByNCRI_CODIGO(Convert.ToInt32(e.Value)).NCRI_NOMBRE;
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
                int criticidad;
                criticidad = 0;
                if (cboCriticidadBuscar.SelectedIndex != -1)
                    criticidad = cboCriticidadBuscar.GetSelectedValueInt();

                dsMantenimiento.AVERIAS.Clear();

                BLL.AveriasBLL.ObtenerTodos(txtDescripcionBuscar.Text, criticidad, dsMantenimiento);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvAverias.Table = dsMantenimiento.AVERIAS;

                if (dsMantenimiento.AVERIAS.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Averias con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                SetInterface(estadoUI.inicio);

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Averias - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //if (txtRazonSocial.Text != String.Empty && cboEstado.SelectedIndex != -1)
            //{

            //    Entidades.Cliente cliente = new GyCAP.Entidades.Cliente();

            //    //Revisamos que está haciendo
            //    if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
            //    {
            //        //Está cargando un nuevo Empleado
            //        cliente.RazonSocial = txtRazonSocial.Text.Trim();
            //        cliente.Mail = txtMail.Text.Trim();
            //        cliente.Telefono = txtTelefono.Text.Trim();
            //        cliente.MotivoBaja = txtDescripcion.Text.Trim();
            //        cliente.FechaAlta = BLL.DBBLL.GetFechaServidor();
            //        if (cboEstado.SelectedItem.ToString().Substring(0, 1) == "A")
            //        {
            //            cliente.Estado = "A";
            //            cliente.MotivoBaja = string.Empty;
            //        }
            //        else
            //        {
            //            cliente.Estado = "I";
            //            cliente.MotivoBaja = txtDescripcion.Text.Trim();
            //        }

            //        try
            //        {
            //            //Primero lo creamos en la db
            //            cliente.Codigo = BLL.ClienteBLL.Insertar(cliente);
            //            //Ahora lo agregamos al dataset
            //            Data.dsCliente.CLIENTESRow rowCliente = dsMantenimiento.CLIENTES.NewCLIENTESRow();
            //            //Indicamos que comienza la edición de la fila
            //            rowCliente.BeginEdit();
            //            rowCliente.CLI_CODIGO = cliente.Codigo;
            //            rowCliente.CLI_RAZONSOCIAL = cliente.RazonSocial;
            //            rowCliente.CLI_TELEFONO = cliente.Telefono;
            //            rowCliente.CLI_MAIL = cliente.Mail;
            //            rowCliente.CLI_MOTIVOBAJA = cliente.MotivoBaja;
            //            rowCliente.CLI_ESTADO = cliente.Estado;

            //            //Termina la edición de la fila
            //            rowCliente.EndEdit();
            //            //Agregamos la fila al dataset y aceptamos los cambios
            //            dsMantenimiento.CLIENTES.AddCLIENTESRow(rowCliente);
            //            dsMantenimiento.CLIENTES.AcceptChanges();

            //            //Y por último seteamos el estado de la interfaz

            //            //Vemos cómo se inició el formulario para determinar la acción a seguir
            //            if (estadoInterface == estadoUI.nuevoExterno)
            //            {
            //                //Nuevo desde acceso directo, cerramos el formulario

            //                if (NuevoCliente != null)//preguntas si el evento esta manejado, si esta referenciado en algun otro form
            //                {
            //                    NuevoCliente(cliente); //llamas el evento
            //                }

            //                btnSalir.PerformClick();
            //            }
            //            else
            //            {
            //                //Nuevo desde el mismo formulario, volvemos a la pestaña buscar
            //                SetInterface(estadoUI.inicio);
            //            }

            //        }
            //        catch (Entidades.Excepciones.ElementoExistenteException ex)
            //        {
            //            MessageBox.Show(ex.Message, "Advertencia: Elemento existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //        catch (Entidades.Excepciones.BaseDeDatosException ex)
            //        {
            //            MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //    else
            //    {
            //        //Está modificando una designacion
            //        //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada                
            //        cliente.Codigo = Convert.ToInt32(dvAverias[dgvLista.SelectedRows[0].Index]["cli_codigo"]);

            //        //Segundo obtenemos los nuevos datos que ingresó el usuario
            //        cliente.RazonSocial = txtRazonSocial.Text.Trim();
            //        cliente.Mail = txtMail.Text.Trim();
            //        cliente.Telefono = txtTelefono.Text.Trim();
            //        cliente.MotivoBaja = txtDescripcion.Text.Trim();
            //        if (cboEstado.SelectedItem.ToString().Substring(0, 1) == "A")
            //            cliente.Estado = "A";
            //        else
            //            cliente.Estado = "I";

            //        try
            //        {
            //            //Lo actualizamos en la DB
            //            BLL.ClienteBLL.Actualizar(cliente);
            //            //Lo actualizamos en el dataset y aceptamos los cambios
            //            Data.dsCliente.CLIENTESRow rowCliente = dsMantenimiento.CLIENTES.FindByCLI_CODIGO(cliente.Codigo);
            //            //Indicamos que comienza la edición de la fila
            //            rowCliente.BeginEdit();
            //            //rowEmpleado.E_CODIGO = empleado.Codigo;
            //            rowCliente.CLI_RAZONSOCIAL = cliente.RazonSocial;
            //            rowCliente.CLI_TELEFONO = cliente.Telefono;
            //            rowCliente.CLI_MAIL = cliente.Mail;
            //            rowCliente.CLI_MOTIVOBAJA = cliente.MotivoBaja;
            //            rowCliente.CLI_ESTADO = cliente.Estado;

            //            //Termina la edición de la fila
            //            rowCliente.EndEdit();
            //            //Agregamos la fila al dataset y aceptamos los cambios
            //            dsMantenimiento.CLIENTES.AcceptChanges();
            //            //Avisamos que estuvo todo ok
            //            MessageBox.Show("Elemento actualizado correctamente.", "Información: Actualización ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            //Y por último seteamos el estado de la interfaz
            //            SetInterface(estadoUI.inicio);
            //        }
            //        catch (Entidades.Excepciones.BaseDeDatosException ex)
            //        {
            //            MessageBox.Show(ex.Message, "Error: " + this.Text + " - Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }

            //    //recarga de la grilla
            //    dgvLista.Refresh();

            //}
            //else
            //{
            //    MessageBox.Show("Debe completar los datos.", "Información: Completar los Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar La avería seleccionada?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Lo eliminamos de la DB
                        long codigo = Convert.ToInt64(dvAverias[dgvLista.SelectedRows[0].Index]["AVE_CODIGO"]);
                        BLL.AveriasBLL.Eliminar(codigo);

                        //Lo eliminamos del dataset
                        dsMantenimiento.AVERIAS.FindByAVE_CODIGO(codigo).Delete();
                        dsMantenimiento.AVERIAS.AcceptChanges();
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
                MessageBox.Show("Debe seleccionar una Avería de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                cboMaquina.Focus();
            }
        }
        
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //long codigo = Convert.ToInt64(dvAverias[e.RowIndex]["ave_codigo"]);
            //txtDescripcion.Text = dsMantenimiento.AVERIAS.FindByAVE_CODIGO(codigo).AVE_DESCRIPCION;
            //cboMaquina.SetSelectedValue(int.Parse(dsMantenimiento.MAQUINAS.FindByMAQ_CODIGO(codigo).MAQ_NOMBRE.ToString()));
            //cboCriticidad.SetSelectedValue(int.Parse(dsMantenimiento.NIVELES_CRITICIDAD.FindByNCRI_CODIGO(codigo).NCRI_OMBRE.ToString()));
        }

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            txtDescripcion.SelectAll();
        }

        private void txtDescripcionBuscar_Enter(object sender, EventArgs e)
        {
            txtDescripcionBuscar.SelectAll();
        }

        #endregion





    }
}
