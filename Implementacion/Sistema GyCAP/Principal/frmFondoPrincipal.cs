﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Principal
{
    public partial class frmFondoPrincipal : Form
    {
        public frmFondoPrincipal()
        {
            InitializeComponent();
        }

        private void frmFondoPrincipal_SizeChanged(object sender, EventArgs e)
        {
            if (this.Visible) { this.WindowState = FormWindowState.Maximized; }
        }
    }
}
