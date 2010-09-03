using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.GestionPedido
{
    public partial class frmCliente : Form
    {
        private static frmCliente _frmCliente = null;
        private Data.dsCliente dsCliente = new GyCAP.Data.dsCliente();
        private DataView dvCliente;
        private enum estadoUI { inicio, nuevo, consultar, modificar, nuevoExterno };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        public frmCliente()
        {
            InitializeComponent();

            //Setea el nombre de la Lista
            gpbLista.Text = "Listado de " + this.Text;

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;
            //Agregamos las columnas
            dgvLista.Columns.Add("CLI_CODIGO", "Código");
            dgvLista.Columns.Add("CLI_RAZONSOCIAL", "Razón Social");
            dgvLista.Columns.Add("CLI_TELEFONO", "Telefono");
            dgvLista.Columns.Add("CLI_MAIL", "Mail");
            dgvLista.Columns.Add("CLI_ESTADO", "Estado");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns["CLI_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["CLI_RAZONSOCIAL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["CLI_TELEFONO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLista.Columns["CLI_MAIL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;            
            dgvLista.Columns["CLI_ESTADO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["CLI_CODIGO"].DataPropertyName = "CLI_CODIGO";
            dgvLista.Columns["CLI_RAZONSOCIAL"].DataPropertyName = "CLI_RAZONSOCIAL";
            dgvLista.Columns["CLI_TELEFONO"].DataPropertyName = "CLI_TELEFONO";
            dgvLista.Columns["CLI_MAIL"].DataPropertyName = "CLI_MAIL";
            dgvLista.Columns["CLI_ESTADO"].DataPropertyName = "CLI_ESTADO";

            //Alineacion de los numeros y las fechas en la grilla
            dgvLista.Columns["CLI_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["CLI_CODIGO"].Visible = false;

            //Creamos el dataview y lo asignamos a la grilla
            dvCliente = new DataView(dsCliente.CLIENTES);
            dvCliente.Sort = "CLI_RAZONSOCIAL ASC";
            dgvLista.DataSource = dvCliente;

            //CARGA DE COMBOS
            cboBuscarEstado.Items.Add("Todos");
            cboBuscarEstado.Items.Add("Activo");
            cboBuscarEstado.Items.Add("Inactivo");
            cboBuscarEstado.SelectedIndex = 0;

            cboEstado.Items.Add("Activo");
            cboEstado.Items.Add("Inactivo");
            cboEstado.SelectedIndex = 0;

            //Seteo el maxlenght de los textbox para que no de error en la bd
            txtRazonSocial.MaxLength = 80;
            txtTelefono.MaxLength = 15;
            txtMotivoBaja.MaxLength = 254; 
            
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

                    if (dsCliente.CLIENTES.Rows.Count == 0)
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
                    txtRazonSocial.Text = string.Empty;
                    txtTelefono.Text = string.Empty;
                    txtMail.Text = string.Empty; 
                    txtMotivoBaja.Text = string.Empty; 
                    cboEstado.SelectedIndex = -1;                    

                    //gbGuardarCancelar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcABM.SelectedTab = tpDatos;
                    txtRazonSocial.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    setControles(false);
                    txtRazonSocial.Text = string.Empty;
                    txtTelefono.Text = string.Empty;
                    txtMotivoBaja.Text = string.Empty;
                    txtMail.Text = string.Empty;   
                    cboEstado.SelectedIndex = -1;        

                    //gbGuardarCancelar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcABM.SelectedTab = tpDatos;
                    txtRazonSocial.Focus();
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
                    txtRazonSocial.Focus();
                    break;
                default:
                    break;
            }
        }

        private void setControles(bool pValue)
        {
            txtRazonSocial.ReadOnly = pValue;
            txtTelefono.ReadOnly = pValue;
            txtMotivoBaja.ReadOnly = pValue;
            txtMail.ReadOnly = pValue; 
            cboEstado.Enabled = !pValue;
        }

        //Método para evitar la creación de más de una pantalla
        public static frmCliente Instancia
        {
            get
            {
                if (_frmCliente == null || _frmCliente.IsDisposed)
                {
                    _frmCliente = new frmCliente();
                }
                else
                {
                    _frmCliente.BringToFront();
                }
                return _frmCliente;
            }
            set
            {
                _frmCliente = value;
            }
        }

        #endregion

        private void frmCliente_Load(object sender, EventArgs e)
        {
            if (tcABM.SelectedTab == tpBuscar)
            {
                btnBuscar.Focus();
            }
            else
            {
                txtRazonSocial.Focus();
            }
        }

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
                string estado;
                estado = string.Empty;
                switch (cboBuscarEstado.Text.Substring(0, 1))
                {
                    case "A":
                        estado = "A";
                        break;
                    case "I":
                        estado = "I";
                        break;
                }

                dsCliente.CLIENTES.Clear();
                BLL.ClienteBLL.ObtenerTodos(txtRazonSocialBuscar.Text, estado, dsCliente);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvCliente.Table = dsCliente.CLIENTES;

                if (dsCliente.CLIENTES.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron Clientes con los datos ingresados.", "Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                SetInterface(estadoUI.inicio);
                
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Clientes - Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetInterface(estadoUI.inicio);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }
        
        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigoCliente = Convert.ToInt32(dvCliente[e.RowIndex]["cli_codigo"]);
            txtRazonSocial.Text = dsCliente.CLIENTES.FindByCLI_CODIGO(codigoCliente).CLI_RAZONSOCIAL;
            txtTelefono.Text = dsCliente.CLIENTES.FindByCLI_CODIGO(codigoCliente).CLI_TELEFONO;
            txtMail.Text = dsCliente.CLIENTES.FindByCLI_CODIGO(codigoCliente).CLI_MAIL;
            txtMotivoBaja.Text = dsCliente.CLIENTES.FindByCLI_CODIGO(codigoCliente).CLI_MOTIVOBAJA;
            if (dsCliente.CLIENTES.FindByCLI_CODIGO(codigoCliente).CLI_ESTADO == "A")
                cboEstado.SelectedIndex = 0;
            else
                cboEstado.SelectedIndex = 1;
        }

        private void txtRazonSocial_Enter(object sender, EventArgs e)
        {
            txtRazonSocial.SelectAll();
        }

        private void txtTelefono_Enter(object sender, EventArgs e)
        {
            txtTelefono.SelectAll();
        }

        private void txtMail_Enter(object sender, EventArgs e)
        {
            txtMail.SelectAll();
        }

        private void txtMotivoBaja_Enter(object sender, EventArgs e)
        {
            txtMotivoBaja.SelectAll();
        }

        private void txtRazonSocialBuscar_Enter(object sender, EventArgs e)
        {
            txtRazonSocialBuscar.SelectAll();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo y selecciono algo en el combo
            if (txtRazonSocial.Text != String.Empty && cboEstado.SelectedIndex != -1)
            {

                Entidades.Cliente cliente = new GyCAP.Entidades.Cliente();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Está cargando un nuevo Empleado
                    cliente.RazonSocial = txtRazonSocial.Text.Trim();
                    cliente.Mail = txtMail.Text.Trim();
                    cliente.Telefono = txtTelefono.Text.Trim();
                    cliente.MotivoBaja = txtMotivoBaja.Text.Trim();  
                    cliente.FechaAlta = BLL.DBBLL.GetFechaServidor();
                    if (cboEstado.SelectedItem.ToString().Substring(0, 1) == "A")
                    {
                        cliente.Estado = "A";
                        cliente.MotivoBaja = string.Empty;
                    }
                    else
                    {
                        cliente.Estado = "I";
                        cliente.MotivoBaja = txtMotivoBaja.Text.Trim();
                    }

                    try
                    {
                        //Primero lo creamos en la db
                        cliente.Codigo = BLL.ClienteBLL.Insertar(cliente);
                        //Ahora lo agregamos al dataset
                        Data.dsCliente.CLIENTESRow rowCliente = dsCliente.CLIENTES.NewCLIENTESRow();
                        //Indicamos que comienza la edición de la fila
                        rowCliente.BeginEdit();
                        rowCliente.CLI_CODIGO = cliente.Codigo;
                        rowCliente.CLI_RAZONSOCIAL = cliente.RazonSocial;
                        rowCliente.CLI_TELEFONO = cliente.Telefono;
                        rowCliente.CLI_MAIL = cliente.Mail;
                        rowCliente.CLI_MOTIVOBAJA = cliente.MotivoBaja;
                        rowCliente.CLI_ESTADO = cliente.Estado;

                        //Termina la edición de la fila
                        rowCliente.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsCliente.CLIENTES.AddCLIENTESRow(rowCliente);
                        dsCliente.CLIENTES.AcceptChanges();

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
                    cliente.Codigo = Convert.ToInt32(dvCliente[dgvLista.SelectedRows[0].Index]["cli_codigo"]);

                    //Segundo obtenemos los nuevos datos que ingresó el usuario
                    cliente.RazonSocial = txtRazonSocial.Text.Trim();
                    cliente.Mail = txtMail.Text.Trim();
                    cliente.Telefono = txtTelefono.Text.Trim();
                    cliente.MotivoBaja = txtMotivoBaja.Text.Trim();
                    if (cboEstado.SelectedItem.ToString().Substring(0, 1) == "A")
                        cliente.Estado = "A";
                    else
                        cliente.Estado = "I";

                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.ClienteBLL.Actualizar(cliente);
                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsCliente.CLIENTESRow rowCliente = dsCliente.CLIENTES.FindByCLI_CODIGO(cliente.Codigo);
                        //Indicamos que comienza la edición de la fila
                        rowCliente.BeginEdit();
                        //rowEmpleado.E_CODIGO = empleado.Codigo;
                        rowCliente.CLI_RAZONSOCIAL = cliente.RazonSocial;
                        rowCliente.CLI_TELEFONO = cliente.Telefono;
                        rowCliente.CLI_MAIL = cliente.Mail;
                        rowCliente.CLI_MOTIVOBAJA = cliente.MotivoBaja;
                        rowCliente.CLI_ESTADO = cliente.Estado;

                        //Termina la edición de la fila
                        rowCliente.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsCliente.CLIENTES.AcceptChanges();
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

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (e.Value.ToString() != String.Empty)
            //{
            string nombre;
            switch (dgvLista.Columns[e.ColumnIndex].Name)
            {
                case "CLI_ESTADO":
                    nombre = string.Empty;
                    if (e.Value.ToString() == "A")
                        nombre = "Activo";
                    else if (e.Value.ToString() == "I")
                        nombre = "Inactivo";

                    e.Value = nombre;
                    break;
                default:
                    break;
            }

            //}
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Controlamos que esté seleccionado algo
            if (dgvLista.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Preguntamos si está seguro
                DialogResult respuesta = MessageBox.Show("¿Ésta seguro que desea eliminar el Cliente seleccionado?", "Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Lo eliminamos de la DB
                        long codigo = Convert.ToInt64(dvCliente[dgvLista.SelectedRows[0].Index]["CLI_CODIGO"]);
                        BLL.ClienteBLL.Eliminar(codigo); 

                        //Lo eliminamos del dataset
                        dsCliente.CLIENTES.FindByCLI_CODIGO(codigo).Delete();
                        dsCliente.CLIENTES.AcceptChanges();
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
                MessageBox.Show("Debe seleccionar un Cliente de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }
}
