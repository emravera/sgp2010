using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Mensajes;


namespace GyCAP.UI.GestionStock
{
    public partial class frmUbicacionStock : Form
    {
        private static frmUbicacionStock _frmUbicacionStock = null;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, eliminar};
        private estadoUI estadoInterface;
        private Data.dsStock dsStock = new GyCAP.Data.dsStock();
        DataView dvUbicaciones, dvUnidadMedida, dvUbicacionPadre, dvTipoUbicacion, dvTipoBuscar, dvContenidoBuscar, dvContenido;

        #region Inicio
        public frmUbicacionStock()
        {
            InitializeComponent();
            Inicializar();
        }

        public static frmUbicacionStock Instancia
        {
            get
            {
                if (_frmUbicacionStock == null || _frmUbicacionStock.IsDisposed)
                {
                    _frmUbicacionStock = new frmUbicacionStock();
                }
                else
                {
                    _frmUbicacionStock.BringToFront();
                }
                return _frmUbicacionStock;
            }
            set
            {
                _frmUbicacionStock = value;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }
        #endregion

        #region Botones menu y buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {            
            string filtro = string.Empty;
            if (!string.IsNullOrEmpty(txtNombreBuscar.Text))
            {
                filtro = "USTCK_NOMBRE LIKE '%" + txtNombreBuscar.Text + "%'";
            }

            if (cboEstadoBuscar.GetSelectedValueInt() != -1)
            {
                if (string.IsNullOrEmpty(filtro)) { filtro = "USTCK_ACTIVO = " + cboEstadoBuscar.GetSelectedValueInt(); }
                else { filtro += " AND USTCK_ACTIVO = " + cboEstadoBuscar.GetSelectedValueInt(); }
            }

            if(!string.IsNullOrEmpty(txtCodigoBuscar.Text))
            {
                if (string.IsNullOrEmpty(filtro)) { filtro = "USTCK_NOMBRE LIKE '%" + txtCodigoBuscar.Text + "%'"; }
                else { filtro += " AND USTCK_NOMBRE LIKE '%" + txtCodigoBuscar.Text + "%'"; }
            }

            if (cboTipoBuscar.GetSelectedValueInt() != -1)
            {
                if (string.IsNullOrEmpty(filtro)) { filtro = "TUS_CODIGO = " + cboTipoBuscar.GetSelectedValueInt(); }
                else { filtro += " AND TUS_CODIGO = " + cboTipoBuscar.GetSelectedValueInt(); }
            }

            if (cboContenidoBuscar.GetSelectedValueInt() != -1)
            {
                if (string.IsNullOrEmpty(filtro)) { filtro = "CON_CODIGO = " + cboContenidoBuscar.GetSelectedValueInt(); }
                else { filtro += " AND CON_CODIGO = " + cboContenidoBuscar.GetSelectedValueInt(); }
            }
            
            dvUbicaciones.RowFilter = filtro;

            if (dvUbicaciones.Count == 0) { MensajesABM.MsjBuscarNoEncontrado("Ubicaciones de Stock", this.Text); }

            CompletarDatos();

            SetInterface(estadoUI.inicio);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0)
            {
                //Preguntamos si está seguro
                if (MensajesABM.MsjConfirmaEliminarDatos("Ubicación de Stock", MensajesABM.Generos.Femenino, this.Text) == DialogResult.Yes)
                {
                    try
                    {
                        estadoInterface = estadoUI.eliminar;
                        //Obtenemos el codigo
                        int numero = Convert.ToInt32(dvUbicaciones[dgvLista.SelectedRows[0].Index]["ustck_numero"]);
                        //Lo eliminamos de la DB
                        BLL.UbicacionStockBLL.Eliminar(numero);
                        //Lo eliminamos de la tabla conjuntos del dataset pero antes actualizamos las cantidades del padre
                        if(!dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).IsUSTCK_PADRENull())
                          { ActualizarCantidadPadre(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_PADRE, dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_CANTIDADREAL * -1); }
                        dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).Delete();
                        dsStock.UBICACIONES_STOCK.AcceptChanges();
                        RecargarUbicacionesPadres();
                        SetInterface(estadoUI.inicio);
                        CompletarDatos();
                    }
                    catch (Entidades.Excepciones.ElementoEnTransaccionException ex)
                    {
                        MensajesABM.MsjElementoTransaccion(ex.Message, this.Text);
                    }
                    catch (Entidades.Excepciones.BaseDeDatosException ex)
                    {
                        MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Eliminación);
                    }
                }
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Ubicación de Stock", MensajesABM.Generos.Femenino, this.Text);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.nuevo);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0) { SetInterface(estadoUI.consultar); }
            else { MensajesABM.MsjSinSeleccion("Ubicación stock", MensajesABM.Generos.Femenino, this.Text); }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0) { dgvLista.SelectedRows[0].Selected = false; }
            SetInterface(estadoUI.inicio);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0) { SetInterface(estadoUI.modificar); }
            else { MensajesABM.MsjSinSeleccion("Ubicación stock", MensajesABM.Generos.Femenino, this.Text); }
        }        

        #endregion

        #region Boton Guardar

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
            {
                //Antes controlamos que si asigno un padre, el contenido y la unidad de medida coincidan
                List<ItemValidacion> validacion = new List<ItemValidacion>();
                if(cboPadre.GetSelectedValueInt() != -1)
                {
                    if (dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(cboPadre.GetSelectedValueInt()).TUS_CODIGO != cboContenidoStock.GetSelectedValueInt())
                    {
                        validacion.Add(new ItemValidacion(MensajesABM.Validaciones.Logica, "El contenido de las ubicaciones padre e hijo no coinciden."));
                    }
                    if (dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(cboPadre.GetSelectedValueInt()).UMED_CODIGO != cboUnidadMedida.GetSelectedValueInt())
                    {
                        validacion.Add(new ItemValidacion(MensajesABM.Validaciones.Logica, "La unidad de medida de las ubicaciones padre e hijo no coinciden."));
                    }
                }

                if (validacion.Count == 0)
                {
                    if (estadoInterface == estadoUI.nuevo || estadoInterface == estadoUI.nuevoExterno)
                    {
                        //Está cargando uno nuevo
                        try
                        {
                            Entidades.UbicacionStock ubicacion = new GyCAP.Entidades.UbicacionStock();
                            ubicacion.Numero = -1;
                            ubicacion.Codigo = txtCodigo.Text;
                            ubicacion.Nombre = txtNombre.Text;
                            ubicacion.Descripcion = txtDescripcion.Text;
                            ubicacion.UbicacionFisica = txtUbicacionFisica.Text;
                            ubicacion.CantidadReal = nudCantidadReal.Value;
                            if (cboPadre.GetSelectedValueInt() != -1) { ubicacion.UbicacionPadre = new GyCAP.Entidades.UbicacionStock(cboPadre.GetSelectedValueInt()); }
                            ubicacion.UnidadMedida = new GyCAP.Entidades.UnidadMedida(cboUnidadMedida.GetSelectedValueInt());
                            ubicacion.Activo = cboEstado.GetSelectedValueInt();
                            ubicacion.TipoUbicacion = BLL.TipoUbicacionStockBLL.AsTipoUbicacionStockEntity(dsStock.TIPOS_UBICACIONES_STOCK.FindByTUS_CODIGO(cboTipoUbicacion.GetSelectedValueInt()));
                            ubicacion.Contenido = new GyCAP.Entidades.ContenidoUbicacionStock(cboContenidoStock.GetSelectedValueInt());
                            BLL.UbicacionStockBLL.Insertar(ubicacion);
                            Data.dsStock.UBICACIONES_STOCKRow row = dsStock.UBICACIONES_STOCK.NewUBICACIONES_STOCKRow();
                            row.BeginEdit();
                            row.USTCK_NUMERO = ubicacion.Numero;
                            row.USTCK_CODIGO = ubicacion.Codigo;
                            row.USTCK_NOMBRE = ubicacion.Nombre;
                            row.USTCK_DESCRIPCION = ubicacion.Descripcion;
                            row.USTCK_UBICACIONFISICA = ubicacion.UbicacionFisica;
                            if (ubicacion.UbicacionPadre == null) { row.SetUSTCK_PADRENull(); }
                            else
                            {
                                row.USTCK_PADRE = ubicacion.UbicacionPadre.Numero;
                                ActualizarCantidadPadre(ubicacion.UbicacionPadre.Numero, ubicacion.CantidadReal);
                            }
                            row.UMED_CODIGO = ubicacion.UnidadMedida.Codigo;
                            row.USTCK_CANTIDADREAL = ubicacion.CantidadReal;
                            row.USTCK_ACTIVO = ubicacion.Activo;
                            row.TUS_CODIGO = ubicacion.TipoUbicacion.Codigo;
                            row.CON_CODIGO = ubicacion.Contenido.Codigo;
                            row.EndEdit();
                            dsStock.UBICACIONES_STOCK.AddUBICACIONES_STOCKRow(row);
                            dsStock.UBICACIONES_STOCK.AcceptChanges();
                            dvUbicaciones.Table = dsStock.UBICACIONES_STOCK;
                            dgvLista.Refresh();
                            RecargarUbicacionesPadres();

                            SetInterface(estadoUI.inicio);
                        }
                        catch (Entidades.Excepciones.ElementoExistenteException ex)
                        {
                            dsStock.UBICACIONES_STOCK.RejectChanges();
                            MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                        }
                        catch (Entidades.Excepciones.BaseDeDatosException ex)
                        {
                            dsStock.UBICACIONES_STOCK.RejectChanges();
                            MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                        }
                    }
                    else
                    {
                        //Está modificando
                        try
                        {
                            Entidades.UbicacionStock ubicacion = new GyCAP.Entidades.UbicacionStock();
                            ubicacion.Numero = Convert.ToInt32(dvUbicaciones[dgvLista.SelectedRows[0].Index]["ustck_numero"]);
                            string mensaje = string.Empty;
                            //Validamos que no cambie un tipo vista a otro
                            //Validamos que no cambie un tipo != vista a vista
                            int ubicacionAnterior = Convert.ToInt32(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(ubicacion.Numero).TUS_CODIGO);
                            int ubicacionNueva = cboTipoUbicacion.GetSelectedValueInt();
                            if (ubicacionAnterior == BLL.TipoUbicacionStockBLL.TipoVista && ubicacionNueva != BLL.TipoUbicacionStockBLL.TipoVista) { mensaje = "Una ubicación tipo vista no puede convertirse en otro tipo."; }
                            if (ubicacionAnterior != BLL.TipoUbicacionStockBLL.TipoVista && ubicacionNueva == BLL.TipoUbicacionStockBLL.TipoVista) { mensaje = "Una ubicación distinta de tipo vista no puede convertirse en vista."; }
                            if (mensaje == string.Empty)
                            {
                                ubicacion.Codigo = txtCodigo.Text;
                                ubicacion.Nombre = txtNombre.Text;
                                ubicacion.Descripcion = txtDescripcion.Text;
                                ubicacion.UbicacionFisica = txtUbicacionFisica.Text;
                                ubicacion.CantidadReal = nudCantidadReal.Value;
                                if (cboPadre.GetSelectedValueInt() != -1) { ubicacion.UbicacionPadre = new GyCAP.Entidades.UbicacionStock(cboPadre.GetSelectedValueInt()); }
                                ubicacion.UnidadMedida = new GyCAP.Entidades.UnidadMedida(cboUnidadMedida.GetSelectedValueInt());
                                ubicacion.Activo = cboEstado.GetSelectedValueInt();
                                ubicacion.TipoUbicacion = BLL.TipoUbicacionStockBLL.AsTipoUbicacionStockEntity(dsStock.TIPOS_UBICACIONES_STOCK.FindByTUS_CODIGO(cboTipoUbicacion.GetSelectedValueInt()));
                                ubicacion.Contenido = new GyCAP.Entidades.ContenidoUbicacionStock(cboContenidoStock.GetSelectedValueInt());
                                BLL.UbicacionStockBLL.Actualizar(ubicacion);
                                Data.dsStock.UBICACIONES_STOCKRow row = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(ubicacion.Numero);
                                row.BeginEdit();
                                row.USTCK_CODIGO = ubicacion.Codigo;
                                row.USTCK_NOMBRE = ubicacion.Nombre;
                                row.USTCK_DESCRIPCION = ubicacion.Descripcion;
                                row.USTCK_UBICACIONFISICA = ubicacion.UbicacionFisica;
                                if (ubicacion.UbicacionPadre == null && !row.IsUSTCK_PADRENull())
                                {
                                    ActualizarCantidadPadre(row.USTCK_PADRE, row.USTCK_CANTIDADREAL * -1);
                                    row.SetUSTCK_PADRENull();
                                }
                                if (ubicacion.UbicacionPadre != null && !row.IsUSTCK_PADRENull() && ubicacion.UbicacionPadre.Numero != row.USTCK_PADRE)
                                {
                                    ActualizarCantidadPadre(row.USTCK_PADRE, row.USTCK_CANTIDADREAL * -1);
                                    row.USTCK_PADRE = ubicacion.UbicacionPadre.Numero;
                                    ActualizarCantidadPadre(row.USTCK_PADRE, row.USTCK_CANTIDADREAL);
                                }
                                row.UMED_CODIGO = ubicacion.UnidadMedida.Codigo;
                                row.USTCK_CANTIDADREAL = ubicacion.CantidadReal;
                                row.USTCK_ACTIVO = ubicacion.Activo;
                                row.TUS_CODIGO = ubicacion.TipoUbicacion.Codigo;
                                row.CON_CODIGO = ubicacion.Contenido.Codigo;
                                row.EndEdit();
                                MensajesABM.MsjConfirmaGuardar("Ubicación de Stock", this.Text, MensajesABM.Operaciones.Modificación);
                                dsStock.UBICACIONES_STOCK.AcceptChanges();
                                dvUbicaciones.Table = dsStock.UBICACIONES_STOCK;
                                dgvLista.Refresh();

                                RecargarUbicacionesPadres();
                                SetInterface(estadoUI.inicio);
                            }
                            else
                            {
                                MensajesABM.MsjValidacion(mensaje, this.Text);
                            }
                        }
                        catch (Entidades.Excepciones.ElementoExistenteException ex)
                        {
                            dsStock.UBICACIONES_STOCK.RejectChanges();
                            MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                        }
                        catch (Entidades.Excepciones.BaseDeDatosException ex)
                        {
                            dsStock.UBICACIONES_STOCK.RejectChanges();
                            MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                        }
                    }
                }
                else { MensajesABM.MsjValidacion(MensajesABM.EscribirValidacion(validacion), this.Text); }
            }
        }

        #endregion

        #region Servicios

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dvUbicaciones.Count == 0)
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
                    estadoInterface = estadoUI.inicio;
                    tcUbicacionStock.SelectedTab = tpBuscar;
                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }
                    txtNombreBuscar.Focus();
                    break;
                case estadoUI.nuevo:
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Clear();
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    txtUbicacionFisica.ReadOnly = false;
                    txtUbicacionFisica.Clear();
                    cboPadre.SetSelectedValue(-1);
                    cboPadre.Enabled = true;
                    cboUnidadMedida.SetSelectedIndex(-1);
                    cboUnidadMedida.Enabled = true;
                    cboEstado.SetSelectedIndex(-1);
                    cboEstado.Enabled = true;
                    cboTipoUbicacion.SetSelectedIndex(-1);
                    cboTipoUbicacion.Enabled = true;
                    cboContenidoStock.SetSelectedIndex(-1);
                    cboContenidoStock.Enabled = true;
                    nudCantidadReal.Value = 0;
                    nudCantidadReal.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcUbicacionStock.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                case estadoUI.nuevoExterno:
                    txtCodigo.ReadOnly = false;
                    txtCodigo.Clear();
                    txtNombre.ReadOnly = false;
                    txtNombre.Clear();
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Clear();
                    txtUbicacionFisica.ReadOnly = false;
                    txtUbicacionFisica.Clear();
                    cboPadre.Enabled = true;
                    cboPadre.SetSelectedValue(-1);
                    cboUnidadMedida.SetSelectedIndex(-1);
                    cboUnidadMedida.Enabled = true;
                    cboEstado.SetSelectedIndex(-1);
                    cboEstado.Enabled = true;
                    cboTipoUbicacion.SetSelectedIndex(-1);
                    cboTipoUbicacion.Enabled = true;
                    cboContenidoStock.SetSelectedIndex(-1);
                    cboContenidoStock.Enabled = true;
                    nudCantidadReal.Value = 0;
                    nudCantidadReal.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcUbicacionStock.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                case estadoUI.consultar:
                    txtCodigo.ReadOnly = true;
                    txtNombre.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    txtUbicacionFisica.ReadOnly = true;
                    cboUnidadMedida.Enabled = false;
                    cboPadre.Enabled = false;
                    cboEstado.Enabled = false;
                    cboUnidadMedida.Enabled = false;
                    cboTipoUbicacion.Enabled = false;
                    cboContenidoStock.Enabled = false;
                    nudCantidadReal.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcUbicacionStock.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                case estadoUI.modificar:
                    txtCodigo.ReadOnly = false;
                    txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    txtUbicacionFisica.ReadOnly = false;
                    cboUnidadMedida.Enabled = true;
                    cboPadre.Enabled = true;
                    cboEstado.Enabled = true;
                    cboUnidadMedida.Enabled = true;
                    cboTipoUbicacion.Enabled = true;
                    cboContenidoStock.Enabled = true;
                    nudCantidadReal.Enabled = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcUbicacionStock.SelectedTab = tpDatos;
                    txtCodigo.Focus();
                    break;
                default:
                    break;
            }
        }

        private void Inicializar()
        {
            dgvLista.AutoGenerateColumns = false;
            dgvLista.Columns.Add("USTCK_CODIGO", "Código");
            dgvLista.Columns.Add("USTCK_NOMBRE", "Nombre");
            dgvLista.Columns.Add("TUS_CODIGO", "Tipo");            
            dgvLista.Columns.Add("USTCK_CANTIDADREAL", "Cantidad real");
            dgvLista.Columns["USTCK_CANTIDADREAL"].DefaultCellStyle.Format = "N3";
            dgvLista.Columns.Add("UMED_CODIGO", "Unidad medida");
            dgvLista.Columns.Add("USTCK_ACTIVO", "Estado");
            dgvLista.Columns.Add("CON_CODIGO", "Contenido");            
            dgvLista.Columns["USTCK_CANTIDADREAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;           
            dgvLista.Columns["USTCK_CODIGO"].DataPropertyName = "USTCK_CODIGO";
            dgvLista.Columns["USTCK_NOMBRE"].DataPropertyName = "USTCK_NOMBRE";
            dgvLista.Columns["TUS_CODIGO"].DataPropertyName = "TUS_CODIGO";
            dgvLista.Columns["USTCK_CANTIDADREAL"].DataPropertyName = "USTCK_CANTIDADREAL";
            dgvLista.Columns["UMED_CODIGO"].DataPropertyName = "UMED_CODIGO";
            dgvLista.Columns["USTCK_ACTIVO"].DataPropertyName = "USTCK_ACTIVO";
            dgvLista.Columns["CON_CODIGO"].DataPropertyName = "CON_CODIGO";

            try
            {
                BLL.UnidadMedidaBLL.ObtenerTodos(dsStock.UNIDADES_MEDIDA);
                BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsStock.UBICACIONES_STOCK);
                BLL.TipoUbicacionStockBLL.ObtenerTiposUbicacionStock(dsStock.TIPOS_UBICACIONES_STOCK);
                BLL.ContenidoUbicacionStockBLL.ObtenerContenidosUbicacionStock(dsStock.CONTENIDO_UBICACION_STOCK);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }

            dvUbicaciones = new DataView(dsStock.UBICACIONES_STOCK);
            dvUbicaciones.Sort = "USTCK_NOMBRE ASC";
            dvUbicaciones.RowFilter = "USTCK_NUMERO < 0";
            dgvLista.DataSource = dvUbicaciones;
            dvUbicacionPadre = new DataView(dsStock.UBICACIONES_STOCK);
            dvUbicacionPadre.Sort = "USTCK_NOMBRE ASC";

            dvUnidadMedida = new DataView(dsStock.UNIDADES_MEDIDA);
            cboUnidadMedida.SetDatos(dvUnidadMedida, "UMED_CODIGO", "UMED_NOMBRE", "Seleccione", false);
            string[] nombres = { "Activo", "Inactivo" };
            int[] valores = { BLL.UbicacionStockBLL.Activo, BLL.UbicacionStockBLL.Inactivo };
            cboEstadoBuscar.SetDatos(nombres, valores, "--TODOS--", true);
            cboEstado.SetDatos(nombres, valores, "Seleccione", false);
            //dvUbicacionPadre = new DataView(dsStock.UBICACIONES_STOCK);
            //dvUbicacionPadre.RowFilter = "TUS_CODIGO = " + BLL.TipoUbicacionStockBLL.TipoVista.ToString();
            //cboPadre.SetDatos(dvUbicacionPadre, "USTCK_NUMERO", "USTCK_NOMBRE", "--Sin especificar--", true);
            RecargarUbicacionesPadres();
            dvTipoUbicacion = new DataView(dsStock.TIPOS_UBICACIONES_STOCK);
            cboTipoUbicacion.SetDatos(dvTipoUbicacion, "TUS_CODIGO", "TUS_NOMBRE", "Seleccione", false);
            dvTipoBuscar = new DataView(dsStock.TIPOS_UBICACIONES_STOCK);
            cboTipoBuscar.SetDatos(dvTipoBuscar, "TUS_CODIGO", "TUS_NOMBRE", "--TODOS--", true);
            dvContenidoBuscar = new DataView(dsStock.CONTENIDO_UBICACION_STOCK);
            dvContenido = new DataView(dsStock.CONTENIDO_UBICACION_STOCK);
            cboContenidoBuscar.SetDatos(dvContenidoBuscar, "CON_CODIGO", "CON_NOMBRE", "--TODOS--", true);
            cboContenidoStock.SetDatos(dvContenido, "CON_CODIGO", "CON_NOMBRE", "Seleccione", false);
        }

        private void RecargarUbicacionesPadres()
        {
            dvUbicacionPadre.RowFilter = "TUS_CODIGO = " + BLL.TipoUbicacionStockBLL.TipoVista.ToString();
            cboPadre.SetDatos(dvUbicacionPadre, "USTCK_NUMERO", "USTCK_NOMBRE", "--Sin especificar--", true);
        }

        private void ActualizarCantidadPadre(decimal numeroStockPadre, decimal cantidadReal)
        {
            while (numeroStockPadre != -1)
            {
                dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroStockPadre).USTCK_CANTIDADREAL += cantidadReal;
                try
                {
                    BLL.UbicacionStockBLL.ActualizarCantidadesStock(Convert.ToInt32(numeroStockPadre), cantidadReal);
                }
                catch (Entidades.Excepciones.BaseDeDatosException ex) { MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado); }
                                
                //Acomodamos los valores para que no queden negativos
                if (dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroStockPadre).USTCK_CANTIDADREAL < 0)
                { dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroStockPadre).USTCK_CANTIDADREAL = 0; }
               
                if(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroStockPadre).IsUSTCK_PADRENull()) { numeroStockPadre = -1; }
                else { numeroStockPadre = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroStockPadre).USTCK_PADRE; }
            }
        }

        private void control_Enter(object sender, EventArgs e)
        {
            //if (sender.GetType().Equals(txtNombre.GetType())) { (sender as TextBox).SelectAll(); }
            //if (sender.GetType().Equals(txtDescripcion.GetType())) { (sender as RichTextBox).SelectAll(); }
            //if (sender.GetType().Equals(nudCantidadReal.GetType())) { (sender as NumericUpDown).Select(0, 20); }
        }        

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(!string.IsNullOrEmpty(e.Value.ToString()))
            {
                string nombre = string.Empty;

                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "USTCK_PADRE":
                        nombre = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(Convert.ToInt32(e.Value)).USTCK_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "UMED_CODIGO":
                        nombre = dsStock.UNIDADES_MEDIDA.FindByUMED_CODIGO(Convert.ToInt32(e.Value)).UMED_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "USTCK_ACTIVO":
                        if (Convert.ToInt32(e.Value) == BLL.UbicacionStockBLL.Activo) { nombre = "Activo"; }
                        if (Convert.ToInt32(e.Value) == BLL.UbicacionStockBLL.Inactivo) { nombre = "Inactivo"; }
                        e.Value = nombre;
                        break;
                    case "TUS_CODIGO":
                        nombre = dsStock.TIPOS_UBICACIONES_STOCK.FindByTUS_CODIGO(Convert.ToInt32(e.Value)).TUS_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "CON_CODIGO":
                        nombre = dsStock.CONTENIDO_UBICACION_STOCK.FindByCON_CODIGO(Convert.ToInt32(e.Value)).CON_NOMBRE;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }        

        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0)
            {
                CompletarDatos();
            }
        }

        private void CompletarDatos()
        {
            if (dvUbicaciones.Count > 0 && dgvLista.SelectedRows.Count > 0)
            {
                if (estadoInterface != estadoUI.eliminar)
                {
                    int numero = Convert.ToInt32(dvUbicaciones[dgvLista.SelectedRows[0].Index]["ustck_numero"]);
                    txtCodigo.Text = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_CODIGO;
                    txtNombre.Text = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_NOMBRE;
                    txtDescripcion.Text = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_DESCRIPCION;
                    txtUbicacionFisica.Text = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_UBICACIONFISICA;
                    if (!dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).IsUSTCK_PADRENull())
                    { cboPadre.SetSelectedValue(Convert.ToInt32(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_PADRE)); }
                    else { cboPadre.SetSelectedValue(-1); }
                    nudCantidadReal.Value = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_CANTIDADREAL;
                    cboUnidadMedida.SetSelectedValue(Convert.ToInt32(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).UMED_CODIGO));
                    cboTipoUbicacion.SetSelectedValue(Convert.ToInt32(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).TUS_CODIGO));
                    cboEstado.SetSelectedValue(Convert.ToInt32(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_ACTIVO));
                    cboContenidoStock.SetSelectedValue(Convert.ToInt32(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).CON_CODIGO));
                }
            }
        }

        private void dgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }        

        private void cboTipoUbicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (estadoInterface == estadoUI.nuevo)
                {
                    if (cboTipoUbicacion.GetSelectedValueInt() == BLL.TipoUbicacionStockBLL.TipoVista)
                    {
                        nudCantidadReal.Enabled = false;
                        nudCantidadReal.Value = 0;
                    }
                    else
                    {
                        nudCantidadReal.Enabled = true;
                    }
                }
            }
            catch (Exception) { }
        }
        #endregion
    }
}
