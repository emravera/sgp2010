﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Principal
{
    public partial class Principal : Form
    {
        private int childFormNumber = 0;

        public Principal()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
    
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Soporte.frmTipoUnidadMedida.Instancia.MdiParent = this;
            GyCAP.UI.Soporte.frmTipoUnidadMedida.Instancia.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmTerminacion.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmTerminacion.Instancia.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmConjunto.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmConjunto.Instancia.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmUnidadMedida.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmUnidadMedida.Instancia.Show();

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Soporte.frmMarca.Instancia.MdiParent = this;
            GyCAP.UI.Soporte.frmMarca.Instancia.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmDesignacion.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmDesignacion.Instancia.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Soporte.frmSectorTrabajo.Instancia.MdiParent = this;
            GyCAP.UI.Soporte.frmSectorTrabajo.Instancia.Show();
        }
    }
}
