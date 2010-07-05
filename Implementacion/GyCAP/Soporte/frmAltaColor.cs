using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Soporte
{
    public partial class frmAltaColor : Form
    {
        private static frmAltaColor _frmAltaColor = null;
        
        public frmAltaColor()
        {
            InitializeComponent();
        }

        public static frmAltaColor Instancia
        {
            get
            {
                if (_frmAltaColor == null || _frmAltaColor.IsDisposed)
                {
                    _frmAltaColor = new frmAltaColor();
                }
                else
                {
                    _frmAltaColor.BringToFront();
                }
                return _frmAltaColor;
            }
            set
            {
                _frmAltaColor = value;
            }
        }
        
        private void frmAltaColor_Load(object sender, EventArgs e)
        {

        }
        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Length != 0 && !BLL.ColorBLL.esColor(txtNombre.Text))
            {
                BLL.ColorBLL.guardar(txtNombre.Text, txtDescripcion.Text);
                MessageBox.Show("lleno");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
