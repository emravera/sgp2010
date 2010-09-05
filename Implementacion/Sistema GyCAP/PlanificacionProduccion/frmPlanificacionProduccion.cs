using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.PlanificacionProduccion
{
    public partial class frmPlanificacionProduccion : Form
    {
        private static frmPlanificacionProduccion _frmPlanificacionProduccion = null;
        private SplitterPanel areaTrabajo;
        private Panel activo = null;
        
        public frmPlanificacionProduccion()
        {
            InitializeComponent();

            areaTrabajo = scUp.Panel2;
            flpMenu.AutoScroll = false;
            btnDemanda.Tag = panelDemanda;
            btnPlanAnual.Tag = panelPlanAnual;
            btnMPPrincipal.Tag = panelMPPrincipal;
            Size size = new Size(panelDemanda.Size.Width, 0);
            panelDemanda.Size = size;
            panelMPPrincipal.Size = size;
            panelPlanAnual.Size = size;
        }

        private void frmPlanificacionProduccion_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        public static frmPlanificacionProduccion Instancia
        {
            get
            {
                if (_frmPlanificacionProduccion == null || _frmPlanificacionProduccion.IsDisposed)
                {
                    _frmPlanificacionProduccion = new frmPlanificacionProduccion();
                }
                else
                {
                    _frmPlanificacionProduccion.BringToFront();
                }
                return _frmPlanificacionProduccion;
            }
            set
            {
                _frmPlanificacionProduccion = value;
            }
        }

        private Point PosicionarFormulario()
        {
            int posicion = areaTrabajo.Controls.Count;
            Point location = new Point(posicion * 15, posicion * 15);
            return location;
        }

        private void scUp_Panel2_ControlRemoved(object sender, ControlEventArgs e)
        {
            areaTrabajo.Focus();
        }

        #region Menú Lateral

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (scDown.Panel1Collapsed == false)
            {
                scDown.Panel1Collapsed = true;
                btnMenu.Cursor = System.Windows.Forms.Cursors.PanEast;                
            }
            else
            {
                scDown.Panel1Collapsed = false;
                btnMenu.Cursor = System.Windows.Forms.Cursors.PanWest;
            }
            btnMenu.Parent.Focus();
        }

        private void ShowHide(Panel panelClic)
        {
            if (activo != null) //Se está mostrando uno
            {
                if (activo != panelClic) //Son distintos
                {
                    //Ocultamos el actual
                    //activo.Visible = false; Cambio en IT2
                    EfectoPanel(activo, 0);
                    //Mostramos el que viene y lo asignamos como activo
                    //panelClic.Visible = true; Cambio en IT2
                    EfectoPanel(panelClic, 1);
                    activo = panelClic;
                }
                else //Son iguales
                {
                    //Lo ocultamos y nadie queda activo
                    //activo.Visible = false; Cambio en IT2
                    EfectoPanel(activo, 0);
                    activo = null;
                }
            }
            else //No se está mostrando ninguno
            {
                //Lo mostramos y lo asignamos como activo                
                activo = panelClic;
                EfectoPanel(panelClic, 1);
                //activo.Visible = true; Cambio en IT2
            }

        }

        private void EfectoPanel(Panel panel, int efecto)
        {
            int finalSize;
            switch (panel.Controls.Count)
            {
                case 0:
                    finalSize = 80;
                    break;
                case 1:
                    finalSize = 80;
                    break;
                case 2:
                    finalSize = 145;
                    break;
                case 3:
                    finalSize = 210;
                    break;
                default:
                    finalSize = panel.Controls.Count * 70;
                    break;
            }
            Size sizePanel = new Size(panel.Size.Width, panel.Size.Height);
            if (efecto == 0)
            {
                //Ocultar
                while (panel.Size.Height > 0)
                {
                    sizePanel.Height -= 5;
                    panel.Size = sizePanel;
                }
            }
            else
            {
                //Mostrar
                while (panel.Size.Height < finalSize)
                {
                    sizePanel.Height += 5;
                    panel.Size = sizePanel;
                }
            }
        }

        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point punto = new Point((sender as Button).Location.X + 2, (sender as Button).Location.Y + 2);
                (sender as Button).Location = punto;
            }
        }

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point punto = new Point((sender as Button).Location.X - 2, (sender as Button).Location.Y - 2);
                (sender as Button).Location = punto;
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ShowHide(((Button)sender).Tag as Panel);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        #endregion    

        #region Estimar Demanda

        private void btnNuevoDemanda_Click(object sender, EventArgs e)
        {
            frmEstimarDemandaAnual.Instancia.TopLevel = false;
            frmEstimarDemandaAnual.Instancia.Parent = areaTrabajo;
            frmEstimarDemandaAnual.Instancia.Location = PosicionarFormulario();
            frmEstimarDemandaAnual.Instancia.btnNuevo.PerformClick();  
            frmEstimarDemandaAnual.Instancia.Show();             
        }

        private void btnConsultarDemanda_Click(object sender, EventArgs e)
        {
            frmEstimarDemandaAnual.Instancia.TopLevel = false;
            frmEstimarDemandaAnual.Instancia.Parent = areaTrabajo;
            frmEstimarDemandaAnual.Instancia.Location = PosicionarFormulario();
            frmEstimarDemandaAnual.Instancia.Show();
        }

        #endregion

        #region Plan Anual

        private void btnNuevoPlanAnual_Click(object sender, EventArgs e)
        {
            frmPlanAnual.Instancia.TopLevel = false;
            frmPlanAnual.Instancia.Parent = areaTrabajo;
            frmPlanAnual.Instancia.Location = PosicionarFormulario();
            frmPlanAnual.Instancia.btnNuevo.PerformClick(); 
            frmPlanAnual.Instancia.Show();                       
        }

        private void btnConsultarPlanAnual_Click(object sender, EventArgs e)
        {
            frmPlanAnual.Instancia.TopLevel = false;
            frmPlanAnual.Instancia.Parent = areaTrabajo;
            frmPlanAnual.Instancia.Location = PosicionarFormulario();
            frmPlanAnual.Instancia.Show();
        }

        #endregion

        #region Plan MP Principal

        private void btnNuevoMPPrincipal_Click(object sender, EventArgs e)
        {
            frmPlanificarMateriasPrimas.Instancia.TopLevel = false;
            frmPlanificarMateriasPrimas.Instancia.Parent = areaTrabajo;
            frmPlanificarMateriasPrimas.Instancia.Location = PosicionarFormulario();
            frmPlanificarMateriasPrimas.Instancia.btnNuevo.PerformClick();
            frmPlanificarMateriasPrimas.Instancia.Show();  
        }

        private void btnConsultarMPPrincipal_Click(object sender, EventArgs e)
        {
            frmPlanificarMateriasPrimas.Instancia.TopLevel = false;
            frmPlanificarMateriasPrimas.Instancia.Parent = areaTrabajo;
            frmPlanificarMateriasPrimas.Instancia.Location = PosicionarFormulario();
            frmPlanificarMateriasPrimas.Instancia.Show();
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            frmGenerarOrdenTrabajo.Instancia.TopLevel = false;
            frmGenerarOrdenTrabajo.Instancia.Parent = areaTrabajo;
            frmGenerarOrdenTrabajo.Instancia.Location = PosicionarFormulario();
            frmGenerarOrdenTrabajo.Instancia.Show();
        }

        

    }

}
