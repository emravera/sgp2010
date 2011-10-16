using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.GestionPedido
{
    public partial class frmDialogResult : Form
    {
        public frmDialogResult(string message, string windowsTitle)
        {
            InitializeComponent();

            lblMensaje.Text = message;
            this.Text = windowsTitle;
        }
    }
}
