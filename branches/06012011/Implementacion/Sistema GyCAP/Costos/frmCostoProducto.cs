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
                tvcCostos.TreeView.Nodes.Clear();
                arbol = BLL.EstructuraBLL.GetArbolEstructura(cboCocina.GetSelectedValueInt());

                if (arbol != null && arbol.CodigoEstructura > 0)
                {                    
                    TreeView tv = arbol.AsExtendedTreeView();
                    TreeNode nodo = (TreeNode)tv.Nodes[0].Clone();
                    tv.Dispose();
                    tvcCostos.TreeView.Nodes.Add(nodo);
                    tvcCostos.TreeView.ExpandAll();
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

            tvcCostos.TreeView.Nodes.Clear();
            tvcCostos.Columns.Clear();
            tvcCostos.TreeView.CheckBoxes = false;
            tvcCostos.Columns.Add("Partes", 355, HorizontalAlignment.Center);
            tvcCostos.Columns.Add("Cantidad", 80, HorizontalAlignment.Right);
            tvcCostos.Columns.Add("Unidad Medida", 100, HorizontalAlignment.Center);
            tvcCostos.Columns.Add("Costo unitario $", 110, HorizontalAlignment.Right);
            tvcCostos.Columns.Add("Costo total $", 110, HorizontalAlignment.Right);
        }

        #endregion        
    }
}
