using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Mensajes;
using GyCAP.Entidades;
using GyCAP.Entidades.Enumeraciones;


namespace GyCAP.UI.GestionStock
{
    public partial class frmActualizacionStock : Form
    {
        private static frmActualizacionStock _frmActualizacionStock = null;
        private enum estadoUI { inicio, actualizar };
        private Data.dsStock dsStock = new GyCAP.Data.dsStock();
        DataView dvUbicaciones, dvContenidoBuscar;
        private estadoUI estadoInterface;

        #region Inicio
        
        public frmActualizacionStock()
        {
            InitializeComponent();
            Inicializar();
        }

        public static frmActualizacionStock Instancia
        {
            get
            {
                if (_frmActualizacionStock == null || _frmActualizacionStock.IsDisposed)
                {
                    _frmActualizacionStock = new frmActualizacionStock();
                }
                else
                {
                    _frmActualizacionStock.BringToFront();
                }
                return _frmActualizacionStock;
            }
            set
            {
                _frmActualizacionStock = value;
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
            
            if (string.IsNullOrEmpty(filtro)) { filtro = "TUS_CODIGO <> " + BLL.TipoUbicacionStockBLL.TipoVista; }
            else { filtro += " AND TUS_CODIGO <> " + BLL.TipoUbicacionStockBLL.TipoVista; }
            
            if (cboContenidoBuscar.GetSelectedValueInt() != -1)
            {
                if (string.IsNullOrEmpty(filtro)) { filtro = "CON_CODIGO = " + cboContenidoBuscar.GetSelectedValueInt(); }
                else { filtro += " AND CON_CODIGO = " + cboContenidoBuscar.GetSelectedValueInt(); }
            }
            
            dvUbicaciones.RowFilter = filtro;

            if (dvUbicaciones.Count == 0) { MensajesABM.MsjBuscarNoEncontrado("Actualización de Stock", this.Text); }

            CompletarDatos();

            SetInterface(estadoUI.inicio);
        }                

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0) { SetInterface(estadoUI.actualizar); }
            else { MensajesABM.MsjSinSeleccion("Actualización de Stock", MensajesABM.Generos.Femenino, this.Text); }
        }       

        private void btnVolver_Click(object sender, EventArgs e)
        {
            SetInterface(estadoUI.inicio);
        }

        private void dgvLista_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            CompletarDatos();
        }

        private void CompletarDatos()
        {
            if (dvUbicaciones.Count > 0 && dgvLista.SelectedRows.Count > 0)
            {
                int numero = Convert.ToInt32(dvUbicaciones[dgvLista.SelectedRows[0].Index]["ustck_numero"]);
                txtCodigo.Text = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_CODIGO;
                txtNombre.Text = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_NOMBRE;
                nudRealActual.Value = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_CANTIDADREAL;
                txtUnidadMedida.Text = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).UNIDADES_MEDIDARow.UMED_NOMBRE;
                txtTipo.Text = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).TIPOS_UBICACIONES_STOCKRow.TUS_NOMBRE;
                txtEstado.Text = (dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_ACTIVO == BLL.UbicacionStockBLL.Activo) ? "Activo" : "Inactivo";
                txtContenido.Text = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).CONTENIDO_UBICACION_STOCKRow.CON_NOMBRE;            
            }
        }

        #endregion

        #region Datos

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
            {
                try
                {
                    int numero = Convert.ToInt32(dvUbicaciones[dgvLista.SelectedRows[0].Index]["ustck_numero"]);

                    MovimientoStock movimiento = new MovimientoStock();
                    movimiento.Numero = 0;
                    movimiento.Codigo = StockEnum.CodigoMovimiento.Manual.ToString();
                    movimiento.Descripcion = txtDescripcion.Text;
                    movimiento.FechaAlta = BLL.DBBLL.GetFechaServidor();
                    movimiento.FechaPrevista = DateTime.Parse(dtpFecha.GetFecha().ToString());
                    movimiento.FechaReal = movimiento.FechaPrevista;
                    movimiento.Estado = BLL.EstadoMovimientoStockBLL.GetEstadoEntity(StockEnum.EstadoMovimientoStock.Finalizado);

                    decimal cantidadMovimiento = nudCantidadNueva.Value - dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numero).USTCK_CANTIDADREAL;
                    OrigenMovimiento origenMvto = new OrigenMovimiento();

                    if (cantidadMovimiento < 0)
                    {
                        cantidadMovimiento *= -1;
                        //origen es la ubicacion stock, se le está restando cantidad                        
                        origenMvto.Entidad = BLL.EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.UbicacionStock, numero, null);
                        origenMvto.Entidad.EntidadExterna = BLL.UbicacionStockBLL.GetUbicacionStock(numero);
                        //destino es la entidad manual
                        movimiento.Destino = BLL.EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.Manual, -1, null);
                        //el dueño es la entidad manual, es el evento que generó el movimiento
                        movimiento.Duenio = movimiento.Destino;
                        origenMvto.CantidadReal = cantidadMovimiento;
                        origenMvto.CantidadEstimada = cantidadMovimiento;
                        origenMvto.FechaPrevista = movimiento.FechaPrevista.Value;
                        origenMvto.FechaReal = movimiento.FechaPrevista;
                        movimiento.OrigenesMultiples.Add(origenMvto);
                        movimiento.CantidadDestinoReal = cantidadMovimiento;
                        movimiento.CantidadDestinoEstimada = cantidadMovimiento;
                    }
                    else
                    {
                        //es el caso contrario, se le está sumando cantidad a la ubicación de stock
                        origenMvto.Entidad = BLL.EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.Manual, -1, null);
                        movimiento.Destino = BLL.EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.UbicacionStock, numero, null);
                        movimiento.Destino.EntidadExterna = BLL.UbicacionStockBLL.GetUbicacionStock(numero);                        
                        origenMvto.CantidadEstimada = cantidadMovimiento;
                        origenMvto.CantidadReal = cantidadMovimiento;
                        origenMvto.FechaPrevista = movimiento.FechaPrevista.Value;
                        origenMvto.FechaReal = movimiento.FechaPrevista;
                        movimiento.OrigenesMultiples.Add(origenMvto);
                        movimiento.CantidadDestinoReal = cantidadMovimiento;
                        movimiento.CantidadDestinoEstimada = cantidadMovimiento;
                        movimiento.Duenio = movimiento.OrigenesMultiples[0].Entidad;
                    }

                    BLL.MovimientoStockBLL.InsertarFinalizado(movimiento, null);                    

                    MensajesABM.MsjConfirmaGuardar("Stock", this.Text, MensajesABM.Operaciones.Guardado);

                    if (movimiento.OrigenesMultiples[0].Entidad.TipoEntidad.Codigo == (int)EntidadEnum.TipoEntidadEnum.UbicacionStock)
                    {
                        ActualizarDataset((movimiento.OrigenesMultiples[0].Entidad.EntidadExterna as Entidades.UbicacionStock).Numero, movimiento.OrigenesMultiples[0].CantidadReal);
                    }
                    if (movimiento.Destino.TipoEntidad.Codigo == (int)EntidadEnum.TipoEntidadEnum.UbicacionStock)
                    {
                        ActualizarDataset((movimiento.Destino.EntidadExterna as Entidades.UbicacionStock).Numero, movimiento.CantidadDestinoReal);
                    }

                    CompletarDatos();

                    SetInterface(estadoUI.inicio);

                }
                catch (Entidades.Excepciones.BaseDeDatosException ex)
                {
                    MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                }
                catch (Exception ex)
                {
                    MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Guardado);
                }
            }
        }

        private void ActualizarDataset(int codigoUbicacion, decimal cantidad)
        {
            int codigo = codigoUbicacion;

            while (codigo > 0)
            {
                dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(codigo).USTCK_CANTIDADREAL += cantidad;
                codigo = (dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(codigo).IsUSTCK_PADRENull()) ? -1 : Convert.ToInt32(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(codigo).USTCK_PADRE);
            }

            dsStock.UBICACIONES_STOCK.AcceptChanges();
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

                    btnActualizar.Enabled = hayDatos;
                    tcUbicacionStock.SelectedTab = tpBuscar;
                    estadoInterface = estadoUI.inicio;
                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }
                    if (dgvLista.SelectedRows.Count > 0) { dgvLista.SelectedRows[0].Selected = false; }
                    txtNombreBuscar.Focus();
                    break;
                case estadoUI.actualizar:
                    dtpFecha.SetFechaNull();
                    nudCantidadNueva.Value = 0;
                    txtDescripcion.Clear();
                    tcUbicacionStock.SelectedTab = tpDatos;
                    estadoInterface = estadoUI.actualizar;
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
                BLL.TipoUnidadMedidaBLL.ObtenerTodos(dsStock.TIPOS_UNIDADES_MEDIDA);
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
            
            string[] nombres = { "Activo", "Inactivo" };
            int[] valores = { BLL.UbicacionStockBLL.Activo, BLL.UbicacionStockBLL.Inactivo };
            cboEstadoBuscar.SetDatos(nombres, valores, "--TODOS--", true);            
            
            dvContenidoBuscar = new DataView(dsStock.CONTENIDO_UBICACION_STOCK);            
            cboContenidoBuscar.SetDatos(dvContenidoBuscar, "CON_CODIGO", "CON_NOMBRE", "--TODOS--", true);            

            SetInterface(estadoUI.inicio);
        }        

        private void ActualizarCantidadPadre(decimal numeroStockPadre, decimal cantidadReal, decimal cantidadVirtual)
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

        private void dgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }

        #endregion

                
    }
}
