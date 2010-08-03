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

        #region Menú Lateral

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (scDown.Panel1Collapsed == false)
            {
                scDown.Panel1Collapsed = true;
                btnMenu.Cursor = System.Windows.Forms.Cursors.PanEast;
                btnMenu.Refresh();
            }
            else
            {
                scDown.Panel1Collapsed = false;
                btnMenu.Cursor = System.Windows.Forms.Cursors.PanWest;
                btnMenu.Refresh();
            }
        }

        private void ShowHide(Panel panelClic)
        {
            if (activo != null) //Se está mostrando uno
            {
                if (activo != panelClic) //Son distintos
                {
                    //Ocultamos el actual
                    activo.Visible = false;
                    //Mostramos el que viene y lo asignamos como activo
                    panelClic.Visible = true;
                    activo = panelClic;
                }
                else //Son iguales
                {
                    //Lo ocultamos y nadie queda activo
                    activo.Visible = false;
                    activo = null;
                }
            }
            else //No se está mostrando ninguno
            {
                //Lo mostramos y lo asignamos como activo
                activo = panelClic;
                activo.Visible = true;
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
    }

}
