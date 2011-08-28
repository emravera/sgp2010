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
using GyCAP.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace GyCAP.UI.GestionStock
{
    public partial class frmGraficoVariacionStock : Form
    {
        private static frmGraficoVariacionStock _frmGraficoVariacionStock = null;
        private dsStock dsStock = new dsStock();
        private DataView dvStock, dvEstado, dvContenido;
        private IList<MovimientoStock> listaMovimientos = new List<MovimientoStock>();

        #region Inicio

        public frmGraficoVariacionStock()
        {
            InitializeComponent();
            InicializarDatos();
        }

        public static frmGraficoVariacionStock Instancia
        {
            get
            {
                if (_frmGraficoVariacionStock == null || _frmGraficoVariacionStock.IsDisposed)
                {
                    _frmGraficoVariacionStock = new frmGraficoVariacionStock();
                }
                else
                {
                    _frmGraficoVariacionStock.BringToFront();
                }
                return _frmGraficoVariacionStock;
            }
            set
            {
                _frmGraficoVariacionStock = value;
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
                if (!dtpFechaDesde.EsFechaNull() && !dtpFechaHasta.EsFechaNull() && cboStock.GetSelectedIndex() != -1 && cboEstado.GetSelectedIndex() != -1)
                {
                    listaMovimientos.Clear();

                    listaMovimientos = BLL.MovimientoStockBLL.ObtenerMovimientosUbicacionStock(dtpFechaDesde.GetFecha(), dtpFechaHasta.GetFecha(), cboStock.GetSelectedValueInt(), cboEstado.GetSelectedValueInt());

                    if (listaMovimientos.Count > 0)
                    {
                        decimal[] valores = new decimal[listaMovimientos.Count + 1];
                        string[] fechas = new string[listaMovimientos.Count + 1];

                        if (listaMovimientos.Count > 0)
                        {
                            valores[0] = listaMovimientos[0].OldQuantity;
                            fechas[0] = string.Empty;
                        }

                        for (int i = 0; i < listaMovimientos.Count; i++)
                        {
                            
                            switch (listaMovimientos[i].Estado.Nombre)
                            {
                                case BLL.EstadoMovimientoStockBLL.Planificado:
                                    valores[i+1] = listaMovimientos[i].OldQuantity + listaMovimientos[i].CantidadOrigenEstimada;
                                    fechas[i+1] = listaMovimientos[i].FechaPrevista.Value.ToShortDateString();
                                    break;
                                case BLL.EstadoMovimientoStockBLL.Finalizado:
                                    if (listaMovimientos[i].Origen.TipoEntidad.Nombre == BLL.TipoEntidadBLL.UbicacionStockNombre)
                                    {
                                        valores[i+1] = listaMovimientos[i].OldQuantity + listaMovimientos[i].CantidadOrigenReal;
                                    }
                                    else if (listaMovimientos[i].Destino.TipoEntidad.Nombre == BLL.TipoEntidadBLL.UbicacionStockNombre)
                                    {
                                        valores[i+1] = listaMovimientos[i].OldQuantity + listaMovimientos[i].CantidadDestinoReal;
                                    }
                                    fechas[i+1] = listaMovimientos[i].FechaReal.Value.ToShortDateString();
                                    break;
                                default:
                                    break;
                            }
                        }

                        GenerarGrafico(valores, fechas, dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(cboStock.GetSelectedValueInt()).USTCK_NOMBRE);
                    }
                    else
                    {
                        MensajesABM.MsjBuscarNoEncontrado("Variación de Stock", this.Text);
                    }
                }
                else
                {                    
                    MensajesABM.MsjSinSeleccion("Filtro de búsqueda", MensajesABM.Generos.Masculino, this.Text);
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
            try
            {
                BLL.UbicacionStockBLL.ObtenerUbicacionesStock(dsStock.UBICACIONES_STOCK);
                BLL.EstadoMovimientoStockBLL.ObtenerEstadosMovimiento(dsStock.ESTADO_MOVIMIENTOS_STOCK);
                BLL.ContenidoUbicacionStockBLL.ObtenerContenidosUbicacionStock(dsStock.CONTENIDO_UBICACION_STOCK);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }

            dvStock = new DataView(dsStock.UBICACIONES_STOCK);
            dvStock.RowFilter = "TUS_CODIGO <> " + BLL.TipoUbicacionStockBLL.TipoVista;            
            cboStock.SetDatos(dvStock, "ustck_numero", "ustck_nombre", "ustck_nombre ASC", "Seleccione...", false);
            dvEstado = new DataView(dsStock.ESTADO_MOVIMIENTOS_STOCK);
            cboEstado.SetDatos(dvEstado, "emvto_codigo", "emvto_nombre", "Seleccione...", false);
            dvContenido = new DataView(dsStock.CONTENIDO_UBICACION_STOCK);
            cboContenido.SetDatos(dvContenido, "con_codigo", "con_nombre", "--TODOS--", true);
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

        #region Gráfico

        private void GenerarGrafico(decimal[] Valores, string[] fechas, string nombreSerie)
        {
            chartStock.Series.Clear();

            chartStock.Series.Add(nombreSerie);
            chartStock.Series[nombreSerie].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            double plotY = 0;
            if (chartStock.Series[nombreSerie].Points.Count > 0)
            {
                plotY = chartStock.Series[nombreSerie].Points[chartStock.Series["0"].Points.Count - 1].YValues[0];
            }

            for (int pointIndex = 0; pointIndex < Valores.Count(); pointIndex++)
            {
                chartStock.Series[nombreSerie].Points.AddY(Convert.ToDouble(Valores[(pointIndex)]));
                chartStock.Series[nombreSerie].Points[pointIndex].AxisLabel = fechas[pointIndex];
            }

            chartStock.Visible = true;
        }        

        private void cboContenido_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboContenido.GetSelectedValueInt() != -1)
            {
                dvStock.RowFilter = "TUS_CODIGO <> " + BLL.TipoUbicacionStockBLL.TipoVista + " AND con_codigo = " + cboContenido.GetSelectedValueInt();
                cboStock.SetDatos(dvStock, "ustck_numero", "ustck_nombre", "ustck_nombre ASC", "Seleccione...", false);
            }
            else
            {
                dvStock.RowFilter = string.Empty;
                cboStock.SetDatos(dvStock, "ustck_numero", "ustck_nombre", "ustck_nombre ASC", "Seleccione...", false);
            }
        }

        #endregion

    }
}
