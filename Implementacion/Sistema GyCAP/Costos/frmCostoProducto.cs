using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Mensajes;
using GyCAP.Entidades.Excepciones;
using GyCAP.Entidades.ArbolEstructura;

namespace GyCAP.UI.Costos
{
    public partial class frmCostoProducto : Form
    {
        private static frmCostoProducto _frmCostoProducto = null;
        private Data.dsCocina dsCocina = new GyCAP.Data.dsCocina();
        private DataView dvCocina;
        private ArbolEstructura arbol;

        #region Inicio

        public frmCostoProducto()
        {
            InitializeComponent();
            Inicializar();
        }

        public static frmCostoProducto Instancia
        {
            get
            {
                if (_frmCostoProducto == null || _frmCostoProducto.IsDisposed)
                {
                    _frmCostoProducto = new frmCostoProducto();
                }
                else
                {
                    _frmCostoProducto.BringToFront();
                }
                return _frmCostoProducto;
            }
            set
            {
                _frmCostoProducto = value;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        #endregion

        #region Datos

        private void btnBuscar_Click(object sender, EventArgs e)
        {            
            if (cboCocina.GetSelectedIndex() != -1)
            {
                tvcCostosMateriales.TreeView.Nodes.Clear();
                tvcCostosCentros.TreeView.Nodes.Clear();
                txtCostoTotal.Text = string.Empty;
                arbol = BLL.EstructuraBLL.GetArbolEstructura(cboCocina.GetSelectedValueInt(), true);

                if (arbol != null && arbol.CodigoEstructura > 0)
                {                    
                    TreeView tv = arbol.AsExtendedTreeViewWithMaterials();
                    TreeNode nodo = (TreeNode)tv.Nodes[0].Clone();
                    tvcCostosMateriales.TreeView.Nodes.Add(nodo);
                    tvcCostosMateriales.TreeView.ExpandAll();

                    decimal costoProceso = 0;
                    tv = arbol.AsExtendedTreeViewWithCentros(out costoProceso);
                    foreach (TreeNode node in tv.Nodes)
                    {
                        tvcCostosCentros.TreeView.Nodes.Add((TreeNode)node.Clone());
                    }
                    tvcCostosCentros.TreeView.ExpandAll();
                    tv.Dispose();

                    txtCostoTotal.Text = string.Format("{0:0.00}",Convert.ToDouble(arbol.GetCostoEstructura() + costoProceso));
                }
                else
                {
                    MensajesABM.MsjValidacion("La cocina seleccionada no posee una estructura definida.", this.Text);
                }
            }
            else
            {
                MensajesABM.MsjSinSeleccion("Cocina", MensajesABM.Generos.Femenino, this.Text);
            }
        }

        #endregion

        #region Servicios

        private void Inicializar()
        {
            try
            {
                BLL.CocinaBLL.ObtenerCocinas(dsCocina.COCINAS);
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MensajesABM.MsjExcepcion(ex.Message, this.Text, MensajesABM.Operaciones.Inicio);
            }

            dvCocina = new DataView(dsCocina.COCINAS);
            cboCocina.SetDatos(dvCocina, "coc_codigo", "coc_codigo_producto", "Seleccione...", false);

            tvcCostosMateriales.TreeView.Nodes.Clear();
            tvcCostosMateriales.Columns.Clear();
            tvcCostosMateriales.TreeView.CheckBoxes = false;
            tvcCostosMateriales.Columns.Add("Partes", 355, HorizontalAlignment.Left);
            tvcCostosMateriales.Columns.Add("Cantidad", 80, HorizontalAlignment.Right);
            tvcCostosMateriales.Columns.Add("Unidad Medida", 100, HorizontalAlignment.Center);
            tvcCostosMateriales.Columns.Add("Costo unitario $", 110, HorizontalAlignment.Right);
            tvcCostosMateriales.Columns.Add("Costo total $", 110, HorizontalAlignment.Right);

            tvcCostosCentros.TreeView.Nodes.Clear();
            tvcCostosCentros.Columns.Clear();
            tvcCostosCentros.TreeView.CheckBoxes = false;
            tvcCostosCentros.Columns.Add("Partes", 250, HorizontalAlignment.Left);
            tvcCostosCentros.Columns.Add("Centro", 150, HorizontalAlignment.Left);
            tvcCostosCentros.Columns.Add("Operación", 150, HorizontalAlignment.Left);
            tvcCostosCentros.Columns.Add("Costo $", 100, HorizontalAlignment.Right);
            tvcCostosCentros.Columns.Add("Costo total $", 100, HorizontalAlignment.Right);
        }

        #endregion        
    }
}
