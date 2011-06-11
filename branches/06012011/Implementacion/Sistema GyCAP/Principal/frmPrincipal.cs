using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Principal
{
    public partial class frmPrincipal : Form
    {
        

        public frmPrincipal()
        {
            InitializeComponent();
            
            //GyCAP.UI.Principal.frmFondoPrincipal fondo = new GyCAP.UI.Principal.frmFondoPrincipal();
            //fondo.MdiParent = this;
            //fondo.WindowState = FormWindowState.Maximized;
            //fondo.Show();
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

        private void menuItemPP_Click(object sender, EventArgs e)
        {
            GyCAP.UI.PlanificacionProduccion.frmPlanificacionProduccion.Instancia.MdiParent = this;
            GyCAP.UI.PlanificacionProduccion.frmPlanificacionProduccion.Instancia.Show();
        }

        private void menuItemGP_Click(object sender, EventArgs e)
        {
            GyCAP.UI.GestionPedido.frmGestionPedido.Instancia.MdiParent = this;
            GyCAP.UI.GestionPedido.frmGestionPedido.Instancia.Show();
        }

        private void menuItemGS_Click(object sender, EventArgs e)
        {
            GyCAP.UI.GestionStock.frmGestionStock.Instancia.MdiParent = this;
            GyCAP.UI.GestionStock.frmGestionStock.Instancia.Show();
        }

        private void menuItemPF_Click(object sender, EventArgs e)
        {
            GyCAP.UI.ProcesoFabricacion.frmProcesoFabricacion.Instancia.MdiParent = this;
            GyCAP.UI.ProcesoFabricacion.frmProcesoFabricacion.Instancia.Show();
        }

        private void menuItemMA_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Mantenimiento.frmModuloMantenimiento.Instancia.MdiParent = this;
            GyCAP.UI.Mantenimiento.frmModuloMantenimiento.Instancia.Show();
        }

        private void menuItemCP_Click(object sender, EventArgs e)
        {
            GyCAP.UI.ControlTrabajoEnProceso.frmControlTrabajoEnProceso.Instancia.MdiParent = this;
            GyCAP.UI.ControlTrabajoEnProceso.frmControlTrabajoEnProceso.Instancia.Show();
        }

        private void menuItemQA_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Calidad.frmModuloCalidad.Instancia.MdiParent = this;
            GyCAP.UI.Calidad.frmModuloCalidad.Instancia.Show();
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
            barraEstado.Visible = statusBarToolStripMenuItem.Checked;
        }

        #endregion

        #region Menú Sistema

        private void itemMenuOpciones_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Principal.frmOpciones.Instancia.MdiParent = this;
            GyCAP.UI.Principal.frmOpciones.Instancia.TopLevel = false;
            GyCAP.UI.Principal.frmOpciones.Instancia.Show();
        }

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

        
        //ELIMINAR LO QUE SIGUE - GONZALO

        private void t9_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Soporte.frmCapacidadEmpleado.Instancia.MdiParent = this;
            GyCAP.UI.Soporte.frmCapacidadEmpleado.Instancia.Show();
        }              

        private void btnControlPlan_Click(object sender, EventArgs e)
        {
            GyCAP.UI.ControlTrabajoEnProceso.frmControlPlanificacion.Instancia.MdiParent = this;
            GyCAP.UI.ControlTrabajoEnProceso.frmControlPlanificacion.Instancia.Show();
        }

        private void btnInventarioABC_Click(object sender, EventArgs e)
        {
            GyCAP.UI.GestionStock.frmInventarioABC.Instancia.MdiParent = this;
            GyCAP.UI.GestionStock.frmInventarioABC.Instancia.Show();
        }

        private void tsbPlanMantenimeinto_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Mantenimiento.frmPlanMantenimiento.Instancia.MdiParent = this;
            GyCAP.UI.Mantenimiento.frmPlanMantenimiento.Instancia.Show();
        }

        private void btnEntregaProducto_Click(object sender, EventArgs e)
        {
            GyCAP.UI.GestionStock.frmEntregaProducto.Instancia.MdiParent = this;
            GyCAP.UI.GestionStock.frmEntregaProducto.Instancia.Show();
        }

        private void tipoParte_Click(object sender, EventArgs e)
        {
            GyCAP.UI.GestionStock.frmActualizacionStock.Instancia.MdiParent = this;
            GyCAP.UI.GestionStock.frmActualizacionStock.Instancia.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmParte.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmParte.Instancia.Show();
        }

        private void tipoRepuesto_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Soporte.frmTipoRepuesto.Instancia.MdiParent = this;
            GyCAP.UI.Soporte.frmTipoRepuesto.Instancia.Show();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            GyCAP.UI.GestionStock.frmProveedor.Instancia.MdiParent = this;
            GyCAP.UI.GestionStock.frmProveedor.Instancia.Show();
        }

        private void toolAveria_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Mantenimiento.frmAverias.Instancia.MdiParent = this;
            GyCAP.UI.Mantenimiento.frmAverias.Instancia.Show();
        }

        private void toolStripRepuestos_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Mantenimiento.frmRepuestos.Instancia.MdiParent = this;
            GyCAP.UI.Mantenimiento.frmRepuestos.Instancia.Show();
        }

        private void toolStripTipoMantenimiento_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Mantenimiento.frmTipoMantenimiento.Instancia.MdiParent = this;
            GyCAP.UI.Mantenimiento.frmTipoMantenimiento.Instancia.Show();
        }

        private void toolStripCausaFallo_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Mantenimiento.frmCausasFallos.Instancia.MdiParent = this;
            GyCAP.UI.Mantenimiento.frmCausasFallos.Instancia.Show();
        }


    }
}
