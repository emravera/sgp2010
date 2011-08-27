using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Data;
using GyCAP.Entidades;
using GyCAP.Entidades.Mensajes;

namespace GyCAP.UI.GestionStock
{
    public partial class frmMovimientoStock : Form
    {
        private static frmMovimientoStock _frmMovimientoStock = null;
        private dsStock dsStock = new dsStock();
        private DataView dvMovimientos, dvEstadoBuscar, dvOrigen, dvDestino;
        private IList<MovimientoStock> listaMovimientos = new List<MovimientoStock>();

        #region Inicio

        public frmMovimientoStock()
        {
            InitializeComponent();
            InicializarDatos();
        }

        public static frmMovimientoStock Instancia
        {
            get
            {
                if (_frmMovimientoStock == null || _frmMovimientoStock.IsDisposed)
                {
                    _frmMovimientoStock = new frmMovimientoStock();
                }
                else
                {
                    _frmMovimientoStock.BringToFront();
                }
                return _frmMovimientoStock;
            }
            set
            {
                _frmMovimientoStock = value;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose(true);
        }

        #endregion

        #region Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                listaMovimientos.Clear();
                
                object origen = GetEntityCode(cboOrigen.GetSelectedValueInt());

                object destino = GetEntityCode(cboDestino.GetSelectedValueInt());
                
                listaMovimientos = BLL.MovimientoStockBLL.ObtenerTodos(dtpFechaDesde.GetFecha(), dtpFechaHasta.GetFecha(), origen, destino, cboEstado.GetSelectedValueInt());
                dgvLista.DataSource = listaMovimientos;

                if (listaMovimientos.Count == 0)
                {
                    MensajesABM.MsjBuscarNoEncontrado("Movimientos de Stock", this.Text);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Búsqueda);
            }
        }

        #endregion

        #region Servicios

        private void InicializarDatos()
        {
            dgvLista.AutoGenerateColumns = false;
            dgvLista.Columns.Add("mvto_codigo", "Código");
            dgvLista.Columns.Add("entd_origen", "Origen");
            dgvLista.Columns.Add("entd_destino", "Destino");            
            dgvLista.Columns.Add("mvto_cantidad_origen_estimada", "Cantidad origen estimada");
            dgvLista.Columns.Add("mvto_cantidad_origen_real", "Cantidad origen real");
            dgvLista.Columns.Add("mvto_cantidad_destino_estimada", "Cantidad destino estimada");
            dgvLista.Columns.Add("mvto_cantidad_destino_real", "Cantidad destino real");
            dgvLista.Columns.Add("mvto_fechaalta", "Fecha alta");
            dgvLista.Columns.Add("mvto_fechaprevista", "Fecha prevista");
            dgvLista.Columns.Add("mvto_fechareal", "Fecha real");
            dgvLista.Columns.Add("emvto_codigo", "Estado");
            dgvLista.Columns.Add("entd_duenio", "Evento");
            dgvLista.Columns["mvto_codigo"].DataPropertyName = "Codigo";
            dgvLista.Columns["entd_origen"].DataPropertyName = "Origen";
            dgvLista.Columns["entd_destino"].DataPropertyName = "Destino";
            dgvLista.Columns["mvto_cantidad_origen_estimada"].DataPropertyName = "CantidadOrigenEstimada";
            dgvLista.Columns["mvto_cantidad_origen_real"].DataPropertyName = "CantidadOrigenReal";
            dgvLista.Columns["mvto_cantidad_destino_estimada"].DataPropertyName = "CantidadDestinoEstimada";
            dgvLista.Columns["mvto_cantidad_destino_real"].DataPropertyName = "CantidadDestinoReal";
            dgvLista.Columns["mvto_fechaalta"].DataPropertyName = "FechaAlta";
            dgvLista.Columns["mvto_fechaprevista"].DataPropertyName = "FechaPrevista";
            dgvLista.Columns["mvto_fechareal"].DataPropertyName = "FechaReal";
            dgvLista.Columns["emvto_codigo"].DataPropertyName = "Estado";
            dgvLista.Columns["entd_duenio"].DataPropertyName = "Duenio";
            dgvLista.Columns["mvto_cantidad_origen_estimada"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["mvto_cantidad_origen_real"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["mvto_cantidad_destino_estimada"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["mvto_cantidad_destino_real"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["mvto_fechaalta"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLista.Columns["mvto_fechaprevista"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLista.Columns["mvto_fechareal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLista.Columns["mvto_cantidad_origen_estimada"].DefaultCellStyle.Format = "N3";
            dgvLista.Columns["mvto_cantidad_origen_real"].DefaultCellStyle.Format = "N3";
            dgvLista.Columns["mvto_cantidad_destino_estimada"].DefaultCellStyle.Format = "N3";
            dgvLista.Columns["mvto_cantidad_destino_real"].DefaultCellStyle.Format = "N3";

            try
            {
                BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsStock.UBICACIONES_STOCK);
                BLL.EstadoMovimientoStockBLL.ObtenerEstadosMovimiento(dsStock.ESTADO_MOVIMIENTOS_STOCK);
                BLL.EntidadBLL.ObtenerTodos(dsStock.ENTIDADES);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }

            dvMovimientos = new DataView(dsStock.MOVIMIENTOS_STOCK);
            dvEstadoBuscar = new DataView(dsStock.ESTADO_MOVIMIENTOS_STOCK);
            dvOrigen = new DataView(dsStock.UBICACIONES_STOCK);
            dvOrigen.RowFilter = "TUS_CODIGO <> " + BLL.TipoUbicacionStockBLL.TipoVista;
            dvDestino = new DataView(dsStock.UBICACIONES_STOCK);
            dvDestino.RowFilter = "TUS_CODIGO <> " + BLL.TipoUbicacionStockBLL.TipoVista;

            cboEstado.SetDatos(dvEstadoBuscar, "emvto_codigo", "emvto_nombre", "emvto_nombre ASC", "--TODOS--", true);
            cboOrigen.SetDatos(dvOrigen, "ustck_numero", "ustck_nombre", "ustck_nombre ASC", "--TODOS--", true);
            cboDestino.SetDatos(dvDestino, "ustck_numero", "ustck_nombre", "ustck_nombre ASC", "--TODOS--", true);
        }

        private void dgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Sistema.FuncionesAuxiliares.SetDataGridViewColumnsSize((sender as DataGridView));
        }        

        private void dgvLista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
            {
                string nombre = string.Empty;

                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "entd_origen":
                    case "entd_destino":
                    case "entd_duenio":
                        nombre = GetNombreExternalEntity((e.Value as Entidad));
                        e.Value = nombre;
                        break;
                    case "mvto_fechaalta":
                    case "mvto_fechaprevista":
                    case "mvto_fechareal":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "emvto_codigo":
                        nombre = (e.Value as EstadoMovimientoStock).Nombre;
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private int GetEntityCode(int codigoEntity)
        {
            if (codigoEntity < 0) { return codigoEntity; }

            int temp = BLL.TipoEntidadBLL.GetTipoEntidadEntity(BLL.TipoEntidadBLL.UbicacionStockNombre).Codigo;
            
            decimal entity = (from x in dsStock.ENTIDADES
                              where x.IsENTD_IDNull() == false
                              && x.ENTD_ID == codigoEntity
                              && x.TENTD_CODIGO == temp
                              select x.ENTD_CODIGO).FirstOrDefault();

            return Convert.ToInt32(entity);
        }

        private string GetNombreExternalEntity(Entidad entidad)
        {
            if (entidad.EntidadExterna == null && entidad.TipoEntidad.Nombre == BLL.TipoEntidadBLL.ManualNombre) { return BLL.TipoEntidadBLL.ManualNombre; }
            string nombre = string.Empty;

            switch (entidad.TipoEntidad.Nombre)
            {
                case BLL.TipoEntidadBLL.PedidoNombre:
                    nombre = string.Concat("Pedido ", (entidad.EntidadExterna as Pedido).Numero);
                    break;
                case BLL.TipoEntidadBLL.DetallePedidoNombre:
                    nombre = (entidad.EntidadExterna as DetallePedido).CodigoNemonico;
                    break;
                case BLL.TipoEntidadBLL.ManualNombre:
                    nombre = BLL.TipoEntidadBLL.ManualNombre;
                    break;
                case BLL.TipoEntidadBLL.OrdenProduccionNombre:
                    nombre = (entidad.EntidadExterna as OrdenProduccion).Codigo;
                    break;
                case BLL.TipoEntidadBLL.OrdenTrabajoNombre:
                    nombre = string.Concat("OrdT ", (entidad.EntidadExterna as OrdenTrabajo).Numero);
                    break;
                case BLL.TipoEntidadBLL.MantenimientoNombre:
                    nombre = string.Concat("ManT ", (entidad.EntidadExterna as Mantenimiento).Codigo);
                    break;
                case BLL.TipoEntidadBLL.UbicacionStockNombre:
                    nombre = (entidad.EntidadExterna as UbicacionStock).Nombre;
                    break;
                case BLL.TipoEntidadBLL.OrdenCompraNombre:
                    nombre = (entidad.EntidadExterna as OrdenCompra).Codigo;
                    break;
                default:
                    break;
            }

            return nombre;
        }

        #endregion
    }
}
