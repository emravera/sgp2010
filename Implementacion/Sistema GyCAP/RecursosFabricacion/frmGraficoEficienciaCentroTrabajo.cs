using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades.Mensajes;
using GyCAP.Entidades.ArbolOrdenesTrabajo;

namespace GyCAP.UI.RecursosFabricacion
{
    public partial class frmGraficoEficienciaCentroTrabajo : Form
    {
        private static frmGraficoEficienciaCentroTrabajo _frmGraficoEficienciaCentroTrabajo = null;
        private Data.dsHojaRuta dsHojaRuta = new GyCAP.Data.dsHojaRuta();
        private DataView dvCentro;

        #region Inicio

        public frmGraficoEficienciaCentroTrabajo()
        {
            InitializeComponent();
            Inicializar();
        }

        public static frmGraficoEficienciaCentroTrabajo Instancia
        {
            get
            {
                if (_frmGraficoEficienciaCentroTrabajo == null || _frmGraficoEficienciaCentroTrabajo.IsDisposed)
                {
                    _frmGraficoEficienciaCentroTrabajo = new frmGraficoEficienciaCentroTrabajo();
                }
                else
                {
                    _frmGraficoEficienciaCentroTrabajo.BringToFront();
                }
                return _frmGraficoEficienciaCentroTrabajo;
            }
            set
            {
                _frmGraficoEficienciaCentroTrabajo = value;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose(true);
        }

        #endregion

        #region Datos

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dtpFechaDesde.EsFechaNull() && !dtpFechaHasta.EsFechaNull() && cboCentro.GetSelectedIndex() != -1)
                {                    
                    IList<decimal> valores = new List<decimal>();
                    IList<string> fechas = new List<string>();

                    IList<HistoricoEficienciaCentro> datos = BLL.FabricaBLL.GetHistoricoEficienciaCentroTrabajo(cboCentro.GetSelectedValueInt(), DateTime.Parse(dtpFechaDesde.GetFecha().ToString()), DateTime.Parse(dtpFechaHasta.GetFecha().ToString()));

                    if (datos.Count > 0)
                    {
                        //realizar calculos - gonzalo
                    }
                    else
                    {
                        MensajesABM.MsjBuscarNoEncontrado("Centro de Trabajo", this.Text);
                    }

                    GenerarGrafico(valores.ToArray(), fechas.ToArray(), dsHojaRuta.CENTROS_TRABAJOS.FindByCTO_CODIGO(cboCentro.GetSelectedValueInt()).CTO_NOMBRE);
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

        #region Gráfico

        private void GenerarGrafico(decimal[] Valores, string[] fechas, string nombreSerie)
        {
            chartEficiencia.Series.Clear();

            chartEficiencia.Series.Add(nombreSerie);
            chartEficiencia.Series[nombreSerie].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            double plotY = 0;
            if (chartEficiencia.Series[nombreSerie].Points.Count > 0)
            {
                plotY = chartEficiencia.Series[nombreSerie].Points[chartEficiencia.Series["0"].Points.Count - 1].YValues[0];
            }

            for (int pointIndex = 0; pointIndex < Valores.Count(); pointIndex++)
            {
                chartEficiencia.Series[nombreSerie].Points.AddY(Convert.ToDouble(Valores[(pointIndex)]));
                chartEficiencia.Series[nombreSerie].Points[pointIndex].AxisLabel = fechas[pointIndex];
            }

            chartEficiencia.Visible = true;
        }

        #endregion

        #region Servicios

        private void Inicializar()
        {
            try
            {
                BLL.CentroTrabajoBLL.ObetenerCentrosTrabajo(null, null, null, null, dsHojaRuta.CENTROS_TRABAJOS);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }

            dvCentro = new DataView(dsHojaRuta.CENTROS_TRABAJOS);
            cboCentro.SetDatos(dvCentro, "cto_codigo", "cto_nombre", "cto_nombre", "Seleccione...", false);
        }

        #endregion
    }
}
