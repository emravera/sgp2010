using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Principal
{
    public partial class frmOpciones : Form
    {
        private static frmOpciones _frmOpciones = null;
        
        public frmOpciones()
        {
            InitializeComponent();
        }

        public static frmOpciones Instancia
        {
            get
            {
                if (_frmOpciones == null || _frmOpciones.IsDisposed)
                {
                    _frmOpciones = new frmOpciones();
                }
                else
                {
                    _frmOpciones.BringToFront();
                }
                return _frmOpciones;
            }
            set
            {
                _frmOpciones = value;
            }
        }
        
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (rbLocal.Checked) { BLL.DBBLL.SetTipoConexion(BLL.DBBLL.tipoLocal); }
            else if (rbRemoto.Checked) { BLL.DBBLL.SetTipoConexion(BLL.DBBLL.tipoInterna); }
            else if (rbInterna.Checked) { BLL.DBBLL.SetTipoConexion(BLL.DBBLL.tipoRemota); }
            btnCerrar.PerformClick();
        }
    }
}
