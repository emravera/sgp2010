using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades;
using GyCAP.Entidades.BindingEntity;
using GyCAP.Entidades.Excepciones;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades.Mensajes;
using GyCAP.Entidades.ArbolEstructura;

namespace GyCAP.UI.Calidad
{
    public partial class frmEficienciaProceso : Form
    {
        private static frmEficienciaProceso _frmEficienciaProceso = null;
        private Data.dsHojaRuta dsHojaRuta = new GyCAP.Data.dsHojaRuta();
        private Data.dsCocina dsCocina = new GyCAP.Data.dsCocina();
        private Data.dsEstructuraProducto dsEstructura = new GyCAP.Data.dsEstructuraProducto();
        private DataView dvCocina, dvEstructura;
        private ArbolEstructura arbol;
        private SortableBindingList<CentroTrabajo> listaCentros;

        #region Inicio

        public frmEficienciaProceso()
        {
            InitializeComponent();
            Inicializar();
        }

        public static frmEficienciaProceso Instancia
        {
            get
            {
                if (_frmEficienciaProceso == null || _frmEficienciaProceso.IsDisposed)
                {
                    _frmEficienciaProceso = new frmEficienciaProceso();
                }
                else
                {
                    _frmEficienciaProceso.BringToFront();
                }
                return _frmEficienciaProceso;
            }
            set
            {
                _frmEficienciaProceso = value;
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
            if (cboEstructura.GetSelectedIndex() != -1)
            {
                tvcCostosMateriales.TreeView.Nodes.Clear();
                tvcCostosCentros.TreeView.Nodes.Clear();
                arbol = BLL.EstructuraBLL.GetArbolEstructuraByEstructura(cboEstructura.GetSelectedValueInt(), true);

                if (arbol != null)
                {
                    TreeView tv = arbol.AsExtendedTreeViewWithMaterialsWithoutCost();
                    TreeNode nodo = (TreeNode)tv.Nodes[0].Clone();
                    tvcCostosMateriales.TreeView.Nodes.Add(nodo);
                    tvcCostosMateriales.TreeView.ExpandAll();
                    tv = arbol.AsExtendedTreeViewWithCentrosWithoutCost();
                    foreach (TreeNode node in tv.Nodes)
                    {
                        tvcCostosCentros.TreeView.Nodes.Add((TreeNode)node.Clone());
                    }
                    
                    tvcCostosCentros.TreeView.ExpandAll();
                    tv.Dispose();
                }
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Estructura", MensajesABM.Generos.Femenino, this.Text);
            }
        }

        #endregion

        #region Servicios

        private void Inicializar()
        {
            try
            {
                BLL.CocinaBLL.ObtenerCocinas(dsCocina.COCINAS);
                listaCentros = BLL.CentroTrabajoBLL.GetAll();
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }

            dvCocina = new DataView(dsCocina.COCINAS);
            cboCocina.SetDatos(dvCocina, "coc_codigo", "coc_codigo_producto", "coc_codigo_producto", "Seleccione...", false);

            dvEstructura = new DataView(dsEstructura.ESTRUCTURAS);
            cboEstructura.SetDatos(dvEstructura, "estr_codigo", "estr_nombre", "estr_nombre", "Seleccione...", false);

            tvcCostosMateriales.TreeView.Nodes.Clear();
            tvcCostosMateriales.Columns.Clear();
            tvcCostosMateriales.TreeView.CheckBoxes = false;
            tvcCostosMateriales.Columns.Add("Partes", 380, HorizontalAlignment.Left);
            tvcCostosMateriales.Columns.Add("Cantidad", 100, HorizontalAlignment.Right);
            tvcCostosMateriales.Columns.Add("Unidad Medida", 250, HorizontalAlignment.Center);

            tvcCostosCentros.TreeView.Nodes.Clear();
            tvcCostosCentros.Columns.Clear();
            tvcCostosCentros.TreeView.CheckBoxes = false;
            tvcCostosCentros.Columns.Add("Partes", 300, HorizontalAlignment.Left);
            tvcCostosCentros.Columns.Add("Centro", 225, HorizontalAlignment.Left);
            tvcCostosCentros.Columns.Add("Operación", 225, HorizontalAlignment.Left);
        }        

        private void cboCocina_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCocina.GetSelectedIndex() != -1)
            {
                try
                {
                    dsEstructura.Clear();
                    BLL.EstructuraBLL.ObtenerEstructuras(null, null, null, cboCocina.GetSelectedValueInt(), null, null, dsEstructura);
                    cboEstructura.SetDatos(dvEstructura, "estr_codigo", "estr_nombre", "estr_nombre", "Seleccione...", false);
                }
                catch (BaseDeDatosException ex)
                {
                    MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Búsqueda);
                }
                catch (Exception) { }
            }
        }

        #endregion

        #region Generar gráfico

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode nodo = tvcCostosCentros.TreeView.SelectedNode;
                int codigoCentro = 0;
                string nombreCentro = string.Empty;

                if (nodo != null)
                {
                    string[] datos = (string[])nodo.Tag;
                    nombreCentro = datos[0];
                    codigoCentro = listaCentros.First(p => p.Nombre.Equals(nombreCentro)).Codigo;                    
                }                
                
                if (!dtpFechaDesde.EsFechaNull() && !dtpFechaHasta.EsFechaNull() && codigoCentro > 0)
                {
                    IList<decimal> valores = new List<decimal>();
                    IList<string> fechas = new List<string>();
                    valores.Add(1);
                    fechas.Add(" ");

                    IList<HistoricoEficienciaCentro> datos = BLL.FabricaBLL.GetHistoricoEficienciaCentroTrabajo(codigoCentro, DateTime.Parse(dtpFechaDesde.GetFecha().ToString()), DateTime.Parse(dtpFechaHasta.GetFecha().ToString()));

                    if (datos.Count > 0)
                    {
                        foreach (HistoricoEficienciaCentro dato in datos)
                        {
                            valores.Add(dato.Eficiencia);
                            fechas.Add(dato.Fecha.ToShortDateString());
                        }
                    }
                    else
                    {
                        MensajesABM.MsjBuscarNoEncontrado("Centro de Trabajo", this.Text);
                    }

                    GenerarGrafico(valores.ToArray(), fechas.ToArray(), nombreCentro);
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
    }
}
