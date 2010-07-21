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
        

        public Principal()
        {
            InitializeComponent();
            //Setea el directorio local de trabajo del sistema.
            GyCAP.BLL.SistemaBLL.WorkingPath = Application.StartupPath;
        }
         
        
        #region Menú Modulos
        
        private void menuItemEP_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmEstructuraProducto.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmEstructuraProducto.Instancia.Show();
        }

        private void menuItemSO_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Soporte.frmSoporte.Instancia.MdiParent = this;
            GyCAP.UI.Soporte.frmSoporte.Instancia.Show();
        }

        private void menuItemRF_Click(object sender, EventArgs e)
        {
            GyCAP.UI.RecursosFabricacion.frmRecursosFabricacion.Instancia.MdiParent = this;
            GyCAP.UI.RecursosFabricacion.frmRecursosFabricacion.Instancia.Show();
        }
        
        #endregion

        #region Menú Edición

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region Menú Ver

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        #endregion

        #region Menú Sistema

        #endregion

        #region Menú Ventana

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
        
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        #endregion

        #region Menú Ayuda

        #endregion

        #region Salir
        private void menuSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        


        //ELIMINAR LO QUE SIGUE
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmModeloCocina.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmModeloCocina.Instancia.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmTerminacion.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmTerminacion.Instancia.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Soporte.frmTipoUnidadMedida.Instancia.MdiParent = this;
            GyCAP.UI.Soporte.frmTipoUnidadMedida.Instancia.Show();
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

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmMateriaPrimaPrincipal.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmMateriaPrimaPrincipal.Instancia.Show();
        }

        private void t9_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Soporte.frmCapacidadEmpleado.Instancia.MdiParent = this;
            GyCAP.UI.Soporte.frmCapacidadEmpleado.Instancia.Show(); 
        }

        private void tLEmpleado_Click(object sender, EventArgs e)
        {
            GyCAP.UI.RecursosFabricacion.frmEmpleado.Instancia.MdiParent = this;
            GyCAP.UI.RecursosFabricacion.frmEmpleado.Instancia.Show();
        }

        

       

        

        
    }
}
