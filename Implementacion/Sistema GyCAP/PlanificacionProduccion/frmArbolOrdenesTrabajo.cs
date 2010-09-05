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

        public void SetArbol(TreeView tvOrdenesTrabajo, string textoVentana)
        {
            tvOrdenes = tvOrdenesTrabajo;
            tvOrdenes.ExpandAll();
            this.Text = textoVentana;
        }

        public TreeView GetArbol()
        {
            return tvOrdenes;
        }
    }
}
