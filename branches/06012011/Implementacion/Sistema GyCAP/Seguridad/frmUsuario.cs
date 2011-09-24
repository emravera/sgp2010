using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Seguridad
{
    public partial class frmUsuario : Form
    {
        private static frmUsuario _frm = null;
        private GyCAP.Data.dsSeguridad dsSeguridad = new GyCAP.Data.dsSeguridad();
        private DataView dvUsuario, dvRoles, dvEstado;
        private enum estadoUI { inicio, nuevo, consultar, modificar, nuevoExterno };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        
        public frmUsuario()
        {
            InitializeComponent();

            //Para que no genere las columnas automáticamente
            dgvLista.AutoGenerateColumns = false;

            //Agregamos las columnas
            dgvLista.Columns.Add("U_CODIGO", "Código");
            dgvLista.Columns.Add("U_NOMBRE", "Nombre");
            dgvLista.Columns.Add("U_USUARIO", "Login");
            dgvLista.Columns.Add("ROL_CODIGO", "Rol");
            dgvLista.Columns.Add("U_MAIL", "Mail");
            dgvLista.Columns.Add("EU_CODIGO", "Estado");

            //Seteamos el modo de tamaño de las columnas
            dgvLista.Columns["U_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLista.Columns["U_NOMBRE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLista.Columns["U_USUARIO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLista.Columns["ROL_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLista.Columns["U_MAIL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvLista.Columns["EU_CODIGO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvLista.Columns["U_CODIGO"].DataPropertyName = "U_CODIGO";
            dgvLista.Columns["U_NOMBRE"].DataPropertyName = "U_NOMBRE";
            dgvLista.Columns["U_USUARIO"].DataPropertyName = "U_USUARIO";
            dgvLista.Columns["ROL_CODIGO"].DataPropertyName = "ROL_CODIGO";
            dgvLista.Columns["U_MAIL"].DataPropertyName = "U_MAIL";
            dgvLista.Columns["EU_CODIGO"].DataPropertyName = "EU_CODIGO";

            //Alineacion de los numeros y las fechas en la grilla
            dgvLista.Columns["U_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["U_CODIGO"].Visible = false;

            //Creamos el dataview y lo asignamos a la grilla
            dvUsuario = new DataView(dsSeguridad.USUARIOS);
            dvUsuario.Sort = "U_NOMBRE ASC";
            dgvLista.DataSource = dvUsuario;

            //CARGA DE COMBOS
            try
            {
                BLL.RolBLL.ObtenerTodos(dsSeguridad.ROLES);
                BLL.EstadoUsuarioBLL.ObtenerTodos(dsSeguridad.ESTADO_USUARIOS);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex) { Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Inicio); }

            //Dataviews
            dvRoles = new DataView(dsSeguridad.ROLES);
            dvRoles.Sort = "ROL_NOMBRE ASC";
            dvEstado = new DataView(dsSeguridad.ESTADO_USUARIOS);
            dvEstado.Sort = "EU_NOMBRE ASC"; 

            //Combos
            cboRol.SetDatos(dvRoles, "ROL_CODIGO", "ROL_NOMBRE", "Seleccione...", false);
            cboEstado.SetDatos(dvEstado, "EU_CODIGO", "EU_NOMBRE", "Seleccione...", false);
            cboBuscarEstado.SetDatos(dvEstado, "EU_CODIGO", "EU_NOMBRE", "Todos", true);

            //Seteo el maxlenght de los textbox para que no de error en la bd
            txtNombre.MaxLength = 80;
            txtLogin.MaxLength = 15;
            txtPassword.MaxLength = 12;
            txtPassword2.MaxLength = 12;
            txtMail.MaxLength = 50;

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

                    if (dsSeguridad.USUARIOS.Rows.Count == 0)
                    {
                        hayDatos = false;
                        btnBuscar.Focus();
                    }
                    else
                    {
                        hayDatos = true;
                        dgvLista.Focus();
                    }

                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }

                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnConsultar.Enabled = hayDatos;
                    btnNuevo.Enabled = true;
                    estadoInterface = estadoUI.inicio;
                    tcABM.SelectedTab = tpBuscar;
                    break;
                case estadoUI.nuevo:
                    setControles(false);
                    txtNombre.Text = string.Empty;
                    txtLogin.Text = string.Empty;
                    txtMail.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    txtPassword2.Text = string.Empty;
                    cboRol.SetSelectedIndex(-1);
                    try
                    {
                        cboEstado.SetSelectedValue(BLL.EstadoUsuarioBLL.EstadoActivo);
                        //cboRol.SetSelectedValue(BLL.RolBLL.RolAdministrador);
                    }
                    catch
                    {
                        cboEstado.SetSelectedIndex(-1);
                        cboRol.SetSelectedIndex(-1);
                    }        

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
                    setControles(false);
                    txtNombre.Text = string.Empty;
                    txtLogin.Text = string.Empty;
                    txtMail.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    txtPassword2.Text = string.Empty;
                    cboRol.SetSelectedIndex(-1);
                    try
                    {
                        cboEstado.SetSelectedValue(BLL.EstadoUsuarioBLL.EstadoActivo);
                        //cboRol.SetSelectedValue(BLL.RolBLL.RolAdministrador);
                    }
                    catch
                    {
                        cboEstado.SetSelectedIndex(-1);
                        cboRol.SetSelectedIndex(-1);
                    }          

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
                    setControles(true);
                    //gbGuardarCancelar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcABM.SelectedTab = tpDatos;
                    //btnVolver.Focus();
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
                    txtNombre.Focus();
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
            txtNombre.ReadOnly = pValue;
            txtLogin.ReadOnly = pValue;
            txtPassword.ReadOnly = pValue;
            txtPassword2.ReadOnly = pValue;
            txtMail.ReadOnly = pValue;
            cboEstado.Enabled = !pValue;
            cboRol.Enabled = !pValue;
        }

        //Método para evitar la creación de más de una pantalla
        public static frmUsuario Instancia
        {
            get
            {
                if (_frm == null || _frm.IsDisposed)
                {
                    _frm = new frmUsuario();
                }
                else
                {
                    _frm.BringToFront();
                }
                return _frm;
            }
            set
            {
                _frm = value;
            }
        }

        private void dgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != String.Empty)
            {
                string nombre;
                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "EU_CODIGO":
                        if (Convert.ToInt32(e.Value) != 0)
                        {
                            nombre = dsSeguridad.ESTADO_USUARIOS.FindByEU_CODIGO(Convert.ToInt32(e.Value)).EU_NOMBRE;
                        }
                        else nombre = "";
                        e.Value = nombre;
                        break;
                    case "ROL_CODIGO":
                        if (Convert.ToInt32(e.Value) != 0)
                        {
                            nombre = dsSeguridad.ROLES.FindByROL_CODIGO(Convert.ToInt32(e.Value)).ROL_NOMBRE;
                        }
                        else nombre = "";
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion Servicios

        #region Pestaña Buscar

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
                dsSeguridad.USUARIOS.Clear();
                GyCAP.BLL.UsuarioBLL.ObtenerTodos(txtNombre.Text, cboBuscarEstado.GetSelectedValueInt(), dsSeguridad);
                //Es necesario volver a asignar al dataview cada vez que cambien los datos de la tabla del dataset
                //por una consulta a la BD
                dvUsuario.Table = dsSeguridad.USUARIOS;

                if (dsSeguridad.USUARIOS.Rows.Count == 0)
                {
                    Entidades.Mensajes.MensajesABM.MsjBuscarNoEncontrado("Usuarios", this.Text);
                }
                SetInterface(estadoUI.inicio);

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Búsqueda);
                SetInterface(estadoUI.inicio);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        #endregion

        #region Pestaña Datos

        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int codigo = Convert.ToInt32(dvUsuario[e.RowIndex]["u_codigo"]);
            txtNombre.Text = dsSeguridad.USUARIOS.FindByU_CODIGO(codigo).U_NOMBRE;
            txtLogin.Text = dsSeguridad.USUARIOS.FindByU_CODIGO(codigo).U_USUARIO;
            txtMail.Text = dsSeguridad.USUARIOS.FindByU_CODIGO(codigo).U_MAIL;
            txtPassword.Text = dsSeguridad.USUARIOS.FindByU_CODIGO(codigo).U_PASSWORD;
            txtPassword2.Text = txtPassword.Text;
            try
            {
                cboEstado.SetSelectedValue(Convert.ToInt32(dsSeguridad.USUARIOS.FindByU_CODIGO(codigo).EU_CODIGO));
                cboRol.SetSelectedValue(Convert.ToInt32(dsSeguridad.USUARIOS.FindByU_CODIGO(codigo).ROL_CODIGO));
            }
            catch
            {
                cboEstado.SetSelectedIndex(-1);
                cboRol.SetSelectedIndex(-1);
            }        
        }
        
        private void txtNombre_Enter(object sender, EventArgs e)
        {
            txtNombre.SelectAll();
        }

        private void txtLogin_Enter(object sender, EventArgs e)
        {
            txtLogin.SelectAll();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
        }

        private void txtPassword2_Enter(object sender, EventArgs e)
        {
            txtPassword2.SelectAll();
        }

        private void txtMail_Enter(object sender, EventArgs e)
        {
            txtMail.SelectAll();
        }

        private void txtNombreBuscar_Enter(object sender, EventArgs e)
        {
            txtNombreBuscar.SelectAll();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo y selecciono algo en el combo
            if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
            {

                //Validar el password
                if (txtPassword.Text.Trim() != txtPassword2.Text.Trim())
                {
                    txtPassword.Text = string.Empty; 
                    txtPassword2.Text = string.Empty;
                    txtPassword.Focus();
                    Entidades.Mensajes.MensajesABM.MsjValidacion("Las contraseñas ingresadas no coinciden. Ingrese nuevamente las contraseñas.", this.Text);
                    return;
                }

                Entidades.Usuario usuario = new GyCAP.Entidades.Usuario(); 
                Entidades.EstadoUsuario estado = new GyCAP.Entidades.EstadoUsuario();
                Entidades.Rol rol = new GyCAP.Entidades.Rol();

                //Revisamos que está haciendo
                if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                {
                    //Validamos que no exista el login
                    Entidades.Usuario usuarioAux = new GyCAP.Entidades.Usuario();
                    usuarioAux.Login = txtLogin.Text.Trim();
                    if (BLL.UsuarioBLL.EsUsuario(usuarioAux) == true)
                    {
                        txtLogin.Focus();
                        Entidades.Mensajes.MensajesABM.MsjValidacion("El login ingresado ya existe en el sistema. Ingrese nuevamente un login.", this.Text);
                        return;
                    }

                    //Está cargando un nuevo Usuario
                    usuario.Nombre = txtNombre.Text.Trim();
                    usuario.Mail = txtMail.Text.Trim();
                    usuario.Login = txtLogin.Text.Trim();
                    usuario.Password = txtPassword.Text.Trim();
                    //Creo el objeto estado y despues lo asigno
                    estado.Codigo = cboEstado.GetSelectedValueInt();
                    estado.Nombre = cboEstado.SelectedText.ToString();
                    usuario.Estado = estado;

                    //Creo el objeto rol y despues lo asigno
                    rol.Codigo = cboRol.GetSelectedValueInt();
                    rol.Nombre = cboRol.SelectedText.ToString();
                    usuario.Rol  = rol;
                    
                    try
                    {
                        //Primero lo creamos en la db
                        usuario.Codigo = BLL.UsuarioBLL.Insertar(usuario);
                        //Ahora lo agregamos al dataset
                        Data.dsSeguridad.USUARIOSRow rowUsuario = dsSeguridad.USUARIOS.NewUSUARIOSRow();
                        //Indicamos que comienza la edición de la fila
                        rowUsuario.BeginEdit();
                        rowUsuario.U_CODIGO = usuario.Codigo;
                        rowUsuario.U_NOMBRE = usuario.Nombre;
                        rowUsuario.U_USUARIO = usuario.Login;
                        rowUsuario.U_MAIL = usuario.Mail;
                        rowUsuario.U_PASSWORD = usuario.Password;
                        rowUsuario.EU_CODIGO = usuario.Estado.Codigo;
                        rowUsuario.ROL_CODIGO = usuario.Rol.Codigo;

                        //Termina la edición de la fila
                        rowUsuario.EndEdit();
                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsSeguridad.USUARIOS.AddUSUARIOSRow(rowUsuario);
                        dsSeguridad.USUARIOS.AcceptChanges();

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
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                    }
                }
                else
                {
                    //Está modificando un Usuario
                    //Primero obtenemos su código del dataview que está realacionado a la fila seleccionada                
                    usuario.Codigo = Convert.ToInt32(dvUsuario[dgvLista.SelectedRows[0].Index]["u_codigo"]);

                    //Segundo obtenemos los nuevos datos que ingresó el usuario
                    usuario.Nombre = txtNombre.Text.Trim();
                    usuario.Mail = txtMail.Text.Trim();
                    usuario.Login = txtLogin.Text.Trim();
                    usuario.Password = txtPassword.Text.Trim();
                    //Creo el objeto estado y despues lo asigno
                    estado.Codigo = cboEstado.GetSelectedValueInt();
                    estado.Nombre = cboEstado.SelectedText.ToString();
                    usuario.Estado = estado;

                    //Creo el objeto rol y despues lo asigno
                    rol.Codigo = cboRol.GetSelectedValueInt();
                    rol.Nombre = cboRol.SelectedText.ToString();
                    usuario.Rol = rol;

                    try
                    {
                        //Lo actualizamos en la DB
                        BLL.UsuarioBLL.Actualizar(usuario);

                        //Lo actualizamos en el dataset y aceptamos los cambios
                        Data.dsSeguridad.USUARIOSRow rowUsuario = dsSeguridad.USUARIOS.FindByU_CODIGO(usuario.Codigo);

                        //Indicamos que comienza la edición de la fila
                        rowUsuario.BeginEdit();

                        rowUsuario.U_NOMBRE = usuario.Nombre;
                        rowUsuario.U_USUARIO = usuario.Login;
                        rowUsuario.U_MAIL = usuario.Mail;
                        rowUsuario.U_PASSWORD = usuario.Password;
                        rowUsuario.EU_CODIGO = usuario.Estado.Codigo;
                        rowUsuario.ROL_CODIGO = usuario.Rol.Codigo;

                        //Termina la edición de la fila
                        rowUsuario.EndEdit();

                        //Agregamos la fila al dataset y aceptamos los cambios
                        dsSeguridad.USUARIOS.AcceptChanges();

                        //Avisamos que estuvo todo ok
                        Entidades.Mensajes.MensajesABM.MsjConfirmaGuardar("Usuario", this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);

                        //Y por último seteamos el estado de la interfaz
                        SetInterface(estadoUI.inicio);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Guardado);
                    }
                    catch (Entidades.Excepciones.ElementoExistenteException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
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
                DialogResult respuesta = Entidades.Mensajes.MensajesABM.MsjConfirmaEliminarDatos("Usuario", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        //Lo eliminamos de la DB
                        int codigo = Convert.ToInt32(dvUsuario[dgvLista.SelectedRows[0].Index]["U_CODIGO"]);
                        BLL.UsuarioBLL.Eliminar(codigo);

                        //Lo eliminamos del dataset
                        dsSeguridad.USUARIOS.FindByU_CODIGO(codigo).Delete();
                        dsSeguridad.USUARIOS.AcceptChanges();
                        btnVolver.PerformClick();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                Entidades.Mensajes.MensajesABM.MsjSinSeleccion("Cliente", GyCAP.Entidades.Mensajes.MensajesABM.Generos.Masculino, this.Text);
            }
        }
        
        #endregion Pestaña Datos

        
    }
}
