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

        public TreeView GetArbolDependenciaCompleta()
        {
            return tvArbolDependenciaCompleta;
        }

        public TreeView GetArbolOrdenesYEstructura()
        {
            return tvArbolOrdenesYEstructura;
        }       

        private void tvArbolDependenciaSimple_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (Convert.ToInt32(e.Node.Tag.ToString()) == 0)
            {
                frmGenerarOrdenTrabajo.InstanciaAutomatica.SeleccionarOrdenTrabajo(Convert.ToInt32(e.Node.Name));
                SeleccionarOrdenTrabajo(Convert.ToInt32(e.Node.Name));
            }
        }

        private void tvArbolDependenciaCompleta_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (Convert.ToInt32(e.Node.Tag.ToString()) == 0)
            {
                frmGenerarOrdenTrabajo.InstanciaAutomatica.SeleccionarOrdenTrabajo(Convert.ToInt32(e.Node.Name));
                SeleccionarOrdenTrabajo(Convert.ToInt32(e.Node.Name));
            }
        }

        private void tvArbolOrdenesYEstructura_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (Convert.ToInt32(e.Node.Tag.ToString()) == 0)
            {
                frmGenerarOrdenTrabajo.InstanciaAutomatica.SeleccionarOrdenTrabajo(Convert.ToInt32(e.Node.Name));
                SeleccionarOrdenTrabajo(Convert.ToInt32(e.Node.Name));
            }
        }

        public void SeleccionarOrdenTrabajo(int codigoOrdenT)
        {
            tvArbolDependenciaSimple.SelectedNode = tvArbolDependenciaSimple.Nodes[0].Nodes.Find(codigoOrdenT.ToString(), true)[0];
            tvArbolDependenciaCompleta.SelectedNode = tvArbolDependenciaCompleta.Nodes[0].Nodes.Find(codigoOrdenT.ToString(), true)[0];
            tvArbolOrdenesYEstructura.SelectedNode = tvArbolOrdenesYEstructura.Nodes[0].Nodes.Find(codigoOrdenT.ToString(), true)[0];
        }

        private void tcArbol_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tpArbol1) { tvArbolDependenciaSimple.Focus(); }
            else if (e.TabPage == tpArbol2) { tvArbolDependenciaCompleta.Focus(); }
            else if (e.TabPage == tpArbol3) { tvArbolOrdenesYEstructura.Focus(); }
        }
    }
}
