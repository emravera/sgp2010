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
    public partial class frmPrincipal2 : Form
    {
        private GyCAP.Data.dsSeguridad dsSeguridad = new GyCAP.Data.dsSeguridad();
        private DataView dvUsuario, dvMenuUsuario, dvMenu;
        frmLogin frm;

        private int childFormNumber = 0;

        public frmPrincipal2()
        {
            InitializeComponent();
        }

        public frmPrincipal2(int codUsuario)
        {
            InitializeComponent();

            //BLL.MenuUsuarioBLL.ObtenerTodos(codUsuario,  dsSeguridad);
            //dvMenuUsuario = new DataView(dsSeguridad.MENU_USUARIOS);

            BLL.MenuBLL.ObtenerTodos(codUsuario, dsSeguridad);
            dvMenu = new DataView(dsSeguridad.MENU ); 
        }

        public frmPrincipal2(frmLogin frmPadre, int codUsuario)
        {
            InitializeComponent();

            frm = frmPadre;

            //BLL.MenuBLL.ObtenerTodos(codUsuario, dsSeguridad);
            //dvMenu = new DataView(dsSeguridad.MENU);

            BLL.MenuUsuarioBLL.ObtenerTodos(codUsuario, dsSeguridad);
            dvMenuUsuario = new DataView(dsSeguridad.MENU_USUARIOS);

            setPermisos();

        }
        
        public frmPrincipal2(Entidades.Usuario usuario)
        {
            InitializeComponent();
        }

        private void setPermisos() 
        {
            foreach (ToolStripMenuItem menu in this.menuStrip.Items)
            {
                //if (menu.Tag.ToString() != "0") 
                //{
                //    menu.Enabled = false;
                //}
                if (menu.Name != "helpMenu" && menu.Name != "salirToolStripMenuItem")
                {
                    if (menu.DropDownItems.Count > 0)
                    {
                        recorrerItemsMenu(menu.DropDownItems, false);
                    }
                }
            }

            try
            {
                foreach (DataRowView row in dvMenuUsuario)
                {
                    //Recorrer el menu
                    foreach (ToolStripMenuItem menu in this.menuStrip.Items )
                    {
                        if (menu.Name != "helpMenu" && menu.Name != "salirToolStripMenuItem")
                        {
                            if (menu.DropDownItems.Count > 0)
                            {
                                recorrerItemsMenu(menu.DropDownItems, true, int.Parse(row["MNU_CODIGO"].ToString()));
                            }
                        }
                        if (menu.Tag.ToString()== row["MNU_CODIGO"].ToString())
                        {
                            //menu.Enabled = true;

                        }
                    }

                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex) { Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Inicio); }

        }

        private void recorrerItemsMenu(ToolStripItemCollection colOpcionesMenu, Boolean pActivo)
        {
            // recorrer el submenú
            foreach (ToolStripItem itmOpcion in colOpcionesMenu)
            {
                
                itmOpcion.Enabled = pActivo;
                
                // si esta opción a su vez despliega un nuevo submenú
                // llamar recursivamente a este método para cambiar sus opciones
                if (((ToolStripMenuItem)itmOpcion).DropDownItems.Count > 0)
                {
                    this.recorrerItemsMenu(((ToolStripMenuItem)itmOpcion).DropDownItems, pActivo);
                }
            }
        }

        private void recorrerItemsMenu(ToolStripItemCollection colOpcionesMenu, Boolean pActivo, int codMenuUsuario)
        {
            // recorrer el submenú
            foreach (ToolStripItem itmOpcion in colOpcionesMenu)
            {

                if (itmOpcion.Tag.ToString() == codMenuUsuario.ToString())
                {
                    itmOpcion.Enabled = pActivo;
                }

                // si esta opción a su vez despliega un nuevo submenú
                // llamar recursivamente a este método para cambiar sus opciones
                //if (((ToolStripMenuItem)itmOpcion).DropDownItems.Count > 0)
                //{
                //    this.recorrerItemsMenu(((ToolStripMenuItem)itmOpcion).DropDownItems, pActivo, codMenuUsuario);
                //}
            }
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
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

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrincipal2_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackgroundImage = new Bitmap("C:\\LogoFlorencia.jpg");
                //this.BackgroundImage = new Bitmap(Application.StartupPath. +"\\LogoFlorencia.jpg");
                
            }
            catch
            {
                this.BackgroundImage = null;
            }
        }

        private void frmPrincipal2_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm.Close();
        }

        #region Menú EstructuraProducto

        private void cocinasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmCocina.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmCocina.Instancia.Show();
        }

        private void designaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmDesignacion.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmDesignacion.Instancia.Show();
        }

        private void estructuraDeLaCocinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmEstructuraCocina.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmEstructuraCocina.Instancia.Show();
        }

        private void listadoDeEstructuraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmListadoEstructura.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmListadoEstructura.Instancia.Show();
        }

        private void materiaPrimaPrincipalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmMateriaPrimaPrincipal.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmMateriaPrimaPrincipal.Instancia.Show();
        }

        private void modeloDeCocinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmModeloCocina.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmModeloCocina.Instancia.Show();
        }

        private void parteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmParte.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmParte.Instancia.Show();
        }

        private void terminaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmTerminacion.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmTerminacion.Instancia.Show();
        }

        private void tipoDeParteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.EstructuraProducto.frmTipoParte.Instancia.MdiParent = this;
            GyCAP.UI.EstructuraProducto.frmTipoParte.Instancia.Show();
        }

        #endregion

        #region Menú Seguridad

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Seguridad.frmUsuario.Instancia.MdiParent = this;
            GyCAP.UI.Seguridad.frmUsuario.Instancia.Show();
        }

        private void permisosDeAccesoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Seguridad.frmPermisosAcceso.Instancia.MdiParent = this;
            GyCAP.UI.Seguridad.frmPermisosAcceso.Instancia.Show();
        }

        #endregion

        #region Menú ControlTrabajoProceso
        private void controlDeLaPlanificaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.ControlTrabajoEnProceso.frmControlPlanificacion.Instancia.MdiParent = this;
            GyCAP.UI.ControlTrabajoEnProceso.frmControlPlanificacion.Instancia.Show();
        }

        private void controlDeLaPloduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.ControlTrabajoEnProceso.frmControlProduccion.Instancia.MdiParent = this;
            GyCAP.UI.ControlTrabajoEnProceso.frmControlProduccion.Instancia.Show();
        }
        #endregion

        #region Menú Costos

        private void costoDelProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.Costos.frmCostoProducto.Instancia.MdiParent = this;
            GyCAP.UI.Costos.frmCostoProducto.Instancia.Show();
        }

        #endregion

        #region Menú Planificacion

        private void estimarDemandaAnualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.PlanificacionProduccion.frmEstimarDemandaAnual.Instancia.MdiParent = this;
            GyCAP.UI.PlanificacionProduccion.frmEstimarDemandaAnual.Instancia.Show();
        }

        private void planAnualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.PlanificacionProduccion.frmPlanAnual.Instancia.MdiParent = this;
            GyCAP.UI.PlanificacionProduccion.frmPlanAnual.Instancia.Show();
        }

        private void planMensualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.PlanificacionProduccion.frmPlanMensual.Instancia.MdiParent = this;
            GyCAP.UI.PlanificacionProduccion.frmPlanMensual.Instancia.Show();
        }

        private void planSemanalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.PlanificacionProduccion.frmPlanSemanal.Instancia.MdiParent = this;
            GyCAP.UI.PlanificacionProduccion.frmPlanSemanal.Instancia.Show();
        }

        private void planificarMateriasPrimasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.PlanificacionProduccion.frmPlanificarMateriasPrimas.Instancia.MdiParent = this;
            GyCAP.UI.PlanificacionProduccion.frmPlanificarMateriasPrimas.Instancia.Show();
        }

        private void generarOrdenesTrabajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.PlanificacionProduccion.frmGenerarOrdenTrabajo.InstanciaManual.MdiParent = this;
            GyCAP.UI.PlanificacionProduccion.frmGenerarOrdenTrabajo.InstanciaManual.Show();
        }

        #endregion

        #region ProcesosFabricacion

        private void holaDeRutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.ProcesoFabricacion.frmHojaRuta.Instancia.MdiParent = this;
            GyCAP.UI.ProcesoFabricacion.frmHojaRuta.Instancia.Show();
        }

        private void operacionesDeFabricacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GyCAP.UI.ProcesoFabricacion.frmOperacionesFabricacion.Instancia.MdiParent = this;
            GyCAP.UI.ProcesoFabricacion.frmOperacionesFabricacion.Instancia.Show();
        }

        #endregion

        

    }
}
