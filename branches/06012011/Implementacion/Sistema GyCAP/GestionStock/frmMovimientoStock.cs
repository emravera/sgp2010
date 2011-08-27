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
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar };
        private estadoUI estadoInterface;
        private static frmMovimientoStock _frmMovimientoStock = null;
        private dsStock dsStock = new dsStock();
        private DataView dvMovimientos, dvEstadoBuscar, dvOrigen, dvDestino;

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
                dsStock.MOVIMIENTOS_STOCK.Clear();
                
                object origen = GetEntityCode(cboOrigen.GetSelectedValueInt());

                object destino = GetEntityCode(cboDestino.GetSelectedValueInt());

                BLL.MovimientoStockBLL.ObtenerTodos(dtpFechaDesde.GetFecha(), dtpFechaHasta.GetFecha(), origen, destino, cboEstado.GetSelectedValueInt(),dsStock.MOVIMIENTOS_STOCK);

                if (dsStock.MOVIMIENTOS_STOCK.Rows.Count == 0)
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

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    bool hayDatos;

                    if (dsStock.MOVIMIENTOS_STOCK.Rows.Count == 0)
                    {
                        hayDatos = false;
                    }
                    else
                    {
                        hayDatos = true;
                    }

                    if (this.Tag != null) { (this.Tag as ErrorProvider).Dispose(); }

                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnConsultar.Enabled = hayDatos;
                    btnNuevo.Enabled = true;
                    estadoInterface = estadoUI.inicio;
                    tcMovimiento.SelectedTab = tpBuscar;
                    dtpFechaDesde.Focus();
                    break;
                case estadoUI.nuevo:                    
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcMovimiento.SelectedTab = tpDatos;
                    break;
                case estadoUI.nuevoExterno:                    
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcMovimiento.SelectedTab = tpDatos;
                    break;
                case estadoUI.consultar:                    
                    btnGuardar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcMovimiento.SelectedTab = tpDatos;
                    break;
                case estadoUI.modificar:                   
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcMovimiento.SelectedTab = tpDatos;
                    break;
                default:
                    break;
            }
        }

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
            dgvLista.Columns["mvto_codigo"].DataPropertyName = "mvto_codigo";
            dgvLista.Columns["entd_origen"].DataPropertyName = "entd_origen";
            dgvLista.Columns["entd_destino"].DataPropertyName = "entd_destino";
            dgvLista.Columns["mvto_cantidad_origen_estimada"].DataPropertyName = "mvto_cantidad_origen_estimada";
            dgvLista.Columns["mvto_cantidad_origen_real"].DataPropertyName = "mvto_cantidad_origen_real";
            dgvLista.Columns["mvto_cantidad_destino_estimada"].DataPropertyName = "mvto_cantidad_destino_estimada";
            dgvLista.Columns["mvto_cantidad_destino_real"].DataPropertyName = "mvto_cantidad_destino_real";
            dgvLista.Columns["mvto_fechaalta"].DataPropertyName = "mvto_fechaalta";
            dgvLista.Columns["mvto_fechaprevista"].DataPropertyName = "mvto_fechaprevista";
            dgvLista.Columns["mvto_fechareal"].DataPropertyName = "mvto_fechareal";
            dgvLista.Columns["emvto_codigo"].DataPropertyName = "emvto_codigo";
            dgvLista.Columns["entd_duenio"].DataPropertyName = "entd_duenio";
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
            dgvLista.DataSource = dvMovimientos;
            dvEstadoBuscar = new DataView(dsStock.ESTADO_MOVIMIENTOS_STOCK);
            dvOrigen = new DataView(dsStock.UBICACIONES_STOCK);
            dvDestino = new DataView(dsStock.UBICACIONES_STOCK);

            cboEstado.SetDatos(dvEstadoBuscar, "emvto_codigo", "emvto_nombre", "emvto_nombre ASC","--TODOS--", true);
            cboOrigen.SetDatos(dvOrigen, "ustck_numero", "ustck_nombre", "ustck_nombre ASC","--TODOS--", true);
            cboDestino.SetDatos(dvDestino, "ustck_numero", "ustck_nombre", "ustck_nombre ASC", "--TODOS--", true);

            SetInterface(estadoUI.inicio);
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
                Entidad entity;

                switch (dgvLista.Columns[e.ColumnIndex].Name)
                {
                    case "entd_origen":
                        entity = GetEntity(Convert.ToInt32(e.Value.ToString()));
                        if (entity != null) { nombre = entity.Nombre; }
                        e.Value = nombre;
                        break;
                    case "entd_destino":
                        entity = GetEntity(Convert.ToInt32(e.Value.ToString()));
                        if (entity != null) { nombre = entity.Nombre; }
                        e.Value = nombre;
                        break;
                    case "mvto_fechaalta":
                    case "mvto_fechaprevista":
                    case "mvto_fechareal":
                        nombre = DateTime.Parse(e.Value.ToString()).ToShortDateString();
                        e.Value = nombre;
                        break;
                    case "emvto_codigo":
                        nombre = dsStock.ESTADO_MOVIMIENTOS_STOCK.FindByEMVTO_CODIGO(Convert.ToInt32(e.Value)).EMVTO_NOMBRE;
                        e.Value = nombre;
                        break;
                    case "entd_duenio":
                        entity = GetEntity(Convert.ToInt32(e.Value.ToString()));
                        if (entity != null) { nombre = entity.Nombre; }
                        e.Value = nombre;
                        break;
                    default:
                        break;
                }
            }
        }

        private object GetEntityCode(int codigoEntity)
        {
            object entity = (from x in dsStock.ENTIDADES
                             where x.IsENTD_IDNull() == false
                             && x.ENTD_ID == codigoEntity
                             && x.TENTD_CODIGO == BLL.TipoEntidadBLL.GetTipoEntidadEntity(BLL.TipoEntidadBLL.UbicacionStockNombre).Codigo
                             select x.ENTD_CODIGO).FirstOrDefault();

            return entity;
        }

        private Entidad GetEntity(int codigoEntity)
        {
            Data.dsStock.ENTIDADESRow entity = (from x in dsStock.ENTIDADES
                                                 where x.IsENTD_IDNull() == false
                                                 && x.ENTD_ID == codigoEntity
                                                 && x.TENTD_CODIGO == BLL.TipoEntidadBLL.GetTipoEntidadEntity(BLL.TipoEntidadBLL.UbicacionStockNombre).Codigo
                                                 select x).FirstOrDefault();

            Entidad entidad = new Entidad();

            if (entity != null)
            {
                entidad = BLL.EntidadBLL.GetEntidad(BLL.TipoEntidadBLL.GetNombreTipoEntidad(Convert.ToInt32(entity.TENTD_CODIGO)), Convert.ToInt32(entity.ENTD_ID));
            }

            return entidad;
        }

        #endregion
    }
}
