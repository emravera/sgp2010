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
using GyCAP.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace GyCAP.UI.GestionStock
{
    public partial class frmGraficoVariacionStock : Form
    {
        private static frmGraficoVariacionStock _frmGraficoVariacionStock = null;
        private dsStock dsStock = new dsStock();
        private DataView dvStock, dvContenido;
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
                if (!dtpFechaDesde.EsFechaNull() && !dtpFechaHasta.EsFechaNull() && cboStock.GetSelectedIndex() != -1)
                {
                    listaMovimientos.Clear();

                    listaMovimientos = BLL.MovimientoStockBLL.ObtenerMovimientosUbicacionStock(dtpFechaDesde.GetFecha(), dtpFechaHasta.GetFecha(), cboStock.GetSelectedValueInt());
                    
                    if (listaMovimientos.Count > 0)
                    {
                        IList<decimal> valores = new List<decimal>();
                        IList<string> fechas = new List<string>();   
                        
                        //Armamos los movimientos con fecha inicio hasta hoy                                                                     
                        decimal actual = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(cboStock.GetSelectedValueInt()).USTCK_CANTIDADREAL;
                        valores.Add(actual);
                        fechas.Add(DateTime.Parse(dtpFechaHasta.GetFecha().ToString()).ToShortDateString());

                        foreach (MovimientoStock mvto in listaMovimientos.Where(p => p.FechaPrevista <= DateTime.Today).Reverse())
                        {
                            if (mvto.Destino.TipoEntidad.Codigo == (int)EntidadEnum.TipoEntidadEnum.UbicacionStock)
                            {
                                if (mvto.Estado.Codigo == (int)StockEnum.EstadoMovimientoStock.Finalizado)
                                {
                                    if ((mvto.Destino.EntidadExterna as UbicacionStock).Numero == cboStock.GetSelectedValueInt())
                                    {
                                        actual -= mvto.CantidadDestinoEstimada;
                                        valores.Add(actual);
                                        fechas.Add(mvto.FechaPrevista.Value.ToShortDateString());
                                    }
                                }
                            }

                            foreach (OrigenMovimiento origen in mvto.OrigenesMultiples)
                            {
                                if (mvto.Estado.Codigo == (int)StockEnum.EstadoMovimientoStock.EnProceso
                                    || mvto.Estado.Codigo == (int)StockEnum.EstadoMovimientoStock.Finalizado)
                                {
                                    if (origen.Entidad.TipoEntidad.Codigo == (int)EntidadEnum.TipoEntidadEnum.UbicacionStock)
                                    {
                                        if ((origen.Entidad.EntidadExterna as UbicacionStock).Numero == cboStock.GetSelectedValueInt())
                                        {
                                            actual += origen.CantidadEstimada;
                                            valores.Add(actual);
                                            fechas.Add(origen.FechaPrevista.ToShortDateString());
                                        }
                                    }
                                }
                            }
                        }

                        valores = valores.Reverse().ToList();
                        fechas = fechas.Reverse().ToList();
                        actual = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(cboStock.GetSelectedValueInt()).USTCK_CANTIDADREAL;

                        //Armamos los movimientos desde hoy hasta fecha final                        
                        foreach (MovimientoStock mvto in listaMovimientos.Where(p => p.FechaPrevista >= DateTime.Today))
                        {
                            if (mvto.Destino.TipoEntidad.Codigo == (int)EntidadEnum.TipoEntidadEnum.UbicacionStock)
                            {
                                if (mvto.Estado.Codigo == (int)StockEnum.EstadoMovimientoStock.Planificado
                                    || mvto.Estado.Codigo == (int)StockEnum.EstadoMovimientoStock.EnProceso)
                                {
                                    if ((mvto.Destino.EntidadExterna as UbicacionStock).Numero == cboStock.GetSelectedValueInt())
                                    {
                                        actual += mvto.CantidadDestinoEstimada;
                                        valores.Add(actual);
                                        fechas.Add(mvto.FechaPrevista.Value.ToShortDateString());
                                    }
                                }
                            }

                            foreach (OrigenMovimiento origen in mvto.OrigenesMultiples)
                            {
                                if (mvto.Estado.Codigo == (int)StockEnum.EstadoMovimientoStock.Planificado)
                                {
                                    if (origen.Entidad.TipoEntidad.Codigo == (int)EntidadEnum.TipoEntidadEnum.UbicacionStock)
                                    {
                                        if ((origen.Entidad.EntidadExterna as UbicacionStock).Numero == cboStock.GetSelectedValueInt())
                                        {
                                            actual -= origen.CantidadEstimada;
                                            valores.Add(actual);
                                            fechas.Add(origen.FechaPrevista.ToShortDateString());
                                        }
                                    }
                                }
                            }
                        }

                        GenerarGrafico(valores.ToArray(), fechas.ToArray(), dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(cboStock.GetSelectedValueInt()).USTCK_NOMBRE);
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
            dvContenido = new DataView(dsStock.CONTENIDO_UBICACION_STOCK);
            cboContenido.SetDatos(dvContenido, "con_codigo", "con_nombre", "--TODOS--", true);
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
