using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.PlanificacionProduccion
{
    public partial class frmArbolOrdenesTrabajo : Form
    {
        private static frmArbolOrdenesTrabajo _frmArbolOrdenesTrabajo = null;
        
        public frmArbolOrdenesTrabajo()
        {
            InitializeComponent();
        }

        public static frmArbolOrdenesTrabajo Instancia
        {
            get
            {
                if (_frmArbolOrdenesTrabajo == null || _frmArbolOrdenesTrabajo.IsDisposed)
                {
                    _frmArbolOrdenesTrabajo = new frmArbolOrdenesTrabajo();
                }
                else
                {
                    _frmArbolOrdenesTrabajo.BringToFront();
                }
                return _frmArbolOrdenesTrabajo;
            }
            set
            {
                _frmArbolOrdenesTrabajo = value;
            }
        }

        public void SetTextoVentana(string textoVentana)
        {
            this.Text = textoVentana;
        }

        public TreeView GetArbolDependenciaSimple()
        {
            return tvArbolDependenciaSimple;
        }  

        private void tvArbolDependenciaSimple_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //if (e.Node.Parent != null)
            //{
                //frmGenerarOrdenTrabajo.Instancia.SeleccionarOrdenTrabajo(Convert.ToInt32(e.Node.Name));
                //SeleccionarOrdenTrabajo(Convert.ToInt32(e.Node.Name));
            //}
        }

        public void SeleccionarOrdenTrabajo(int codigoOrdenT)
        {
            tvArbolDependenciaSimple.SelectedNode = tvArbolDependenciaSimple.Nodes[0].Nodes.Find(codigoOrdenT.ToString(), true)[0];
            //tvArbolOrdenesYEstructura.SelectedNode = tvArbolOrdenesYEstructura.Nodes[0].Nodes.Find(codigoOrdenT.ToString(), true)[0];
        }

        private void tcArbol_Selected(object sender, TabControlEventArgs e)
        {
            tvArbolDependenciaSimple.Focus();
        }
    }
}
