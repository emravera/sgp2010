using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GyCAP.UI.Sistema
{
    public partial class frmImagenZoom : Form
    {
        private static frmImagenZoom _frmImagenZoom = null;
        
        public frmImagenZoom()
        {
            InitializeComponent();
        }

        public static frmImagenZoom Instancia
        {
            get
            {
                if (_frmImagenZoom == null || _frmImagenZoom.IsDisposed)
                {
                    _frmImagenZoom = new frmImagenZoom();
                }
                else
                {
                    _frmImagenZoom.BringToFront();
                }
                return _frmImagenZoom;
            }
            set
            {
                _frmImagenZoom = value;
            }
        }

        public void SetImagen(Image imagen, string textoVentana)
        {
            pbImagen.Image = imagen;
            this.Text = textoVentana;
        }
    }
}
