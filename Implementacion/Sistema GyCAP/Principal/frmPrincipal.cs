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
            //Setea el directorio local de trabajo del sistema.
            GyCAP.BLL.SistemaBLL.WorkingPath = Application.StartupPath;
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

        private void itemMenuOpciones_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Principal.frmOpciones.Instancia.MdiParent = this;
            GyCAP.UI.Principal.frmOpciones.Instancia.TopLevel = false;
            GyCAP.UI.Principal.frmOpciones.Instancia.Show();
        }


        //ELIMINAR LO QUE SIGUE
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            GyCAP.UI.PlanificacionProduccion.frmGenerarOrdenTrabajo.InstanciaAutomatica.MdiParent = this;
            GyCAP.UI.PlanificacionProduccion.frmGenerarOrdenTrabajo.InstanciaAutomatica.Show();
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

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            GyCAP.UI.PlanificacionProduccion.frmEstimarDemandaAnual.Instancia.MdiParent = this;
            GyCAP.UI.PlanificacionProduccion.frmEstimarDemandaAnual.Instancia.Show();
        }

        private void btnPlanAnual_Click(object sender, EventArgs e)
        {
            GyCAP.UI.PlanificacionProduccion.frmPlanAnual.Instancia.MdiParent = this;
            GyCAP.UI.PlanificacionProduccion.frmPlanAnual.Instancia.Show();
        }

        private void tsMaquinas_Click(object sender, EventArgs e)
        {
            GyCAP.UI.RecursosFabricacion.frmRFMaquina.Instancia.MdiParent = this;
            GyCAP.UI.RecursosFabricacion.frmRFMaquina.Instancia.Show();
        }

        private void menuItemPP_Click(object sender, EventArgs e)
        {
            GyCAP.UI.PlanificacionProduccion.frmPlanificacionProduccion.Instancia.MdiParent = this;
            GyCAP.UI.PlanificacionProduccion.frmPlanificacionProduccion.Instancia.Show();
        }

        private void btnPlanMensual_Click(object sender, EventArgs e)
        {
            GyCAP.UI.PlanificacionProduccion.frmPlanMensual.Instancia.MdiParent = this;
            GyCAP.UI.PlanificacionProduccion.frmPlanMensual.Instancia.Show();
        }

        private void btnPlanSemanal_Click(object sender, EventArgs e)
        {
            GyCAP.UI.PlanificacionProduccion.frmPlanSemanal.Instancia.MdiParent = this;
            GyCAP.UI.PlanificacionProduccion.frmPlanSemanal.Instancia.Show();
        }

        private void toolCliente_Click(object sender, EventArgs e)
        {
            GyCAP.UI.GestionPedido.frmCliente.Instancia.MdiParent = this;
            GyCAP.UI.GestionPedido.frmCliente.Instancia.Show();
        }

        private void btnOperaciones_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmOperacionesFabricacion.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmOperacionesFabricacion.Instancia.Show();
        }

        private void toolPedidos_Click(object sender, EventArgs e)
        {
            GyCAP.UI.GestionPedido.frmPedidos.Instancia.MdiParent = this;
            GyCAP.UI.GestionPedido.frmPedidos.Instancia.Show();
        }

        private void menuItemGP_Click(object sender, EventArgs e)
        {
            GyCAP.UI.GestionPedido.frmGestionPedido.Instancia.MdiParent = this;
            GyCAP.UI.GestionPedido.frmGestionPedido.Instancia.Show();
        }

        private void btnControlPlan_Click(object sender, EventArgs e)
        {
            GyCAP.UI.ControlTrabajoEnProceso.frmControlPlanificacion.Instancia.MdiParent = this;
            GyCAP.UI.ControlTrabajoEnProceso.frmControlPlanificacion.Instancia.Show();
        }
        
    }
}
