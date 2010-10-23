using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.ControlTrabajoEnProceso
{
    public partial class frmControlTrabajoEnProceso : Form
    {
        private static frmControlTrabajoEnProceso _frmControlTrabajoEnProceso = null;
        private SplitterPanel areaTrabajo;
        private Panel activo = null;

        #region Inicio

        public frmControlTrabajoEnProceso()
        {
            InitializeComponent();

            areaTrabajo = scUp.Panel2;
            flpMenu.AutoScroll = false;
            btnOrdenProduccion.Tag = panelOrdenProduccion;
            btnPlanificacion.Tag = panelPlanificacion;
            Size size = new Size(panelPlanificacion.Size.Width, 0);
            panelOrdenProduccion.Size = size;
            panelPlanificacion.Size = size;
        }

        public static frmControlTrabajoEnProceso Instancia
        {
            get
            {
                if (_frmControlTrabajoEnProceso == null || _frmControlTrabajoEnProceso.IsDisposed)
                {
                    _frmControlTrabajoEnProceso = new frmControlTrabajoEnProceso();
                }
                else
                {
                    _frmControlTrabajoEnProceso.BringToFront();
                }
                return _frmControlTrabajoEnProceso;
            }
            set
            {
                _frmControlTrabajoEnProceso = value;
            }
        }

        private void frmControlTrabajoEnProceso_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
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

        #endregion

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
                foreach (Control item in panel.Controls)
                {
                    item.Hide();
                }
                while (panel.Size.Height < finalSize)
                {
                    sizePanel.Height += 5;
                    panel.Size = sizePanel;
                }
                foreach (Control item in panel.Controls)
                {
                    item.Show();
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

        #region Orden Producción
        private void btnConsultarOrdenProduccion_Click(object sender, EventArgs e)
        {
            frmControlProduccion.Instancia.TopLevel = false;
            frmControlProduccion.Instancia.Parent = areaTrabajo;
            frmControlProduccion.Instancia.Location = PosicionarFormulario();
            frmControlProduccion.Instancia.Show();
            frmControlProduccion.Instancia.Focus();
        }

        private void btnConsultarOrdenTrabajo_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Planificacion
        private void btnConsultarPlanificacion_Click(object sender, EventArgs e)
        {
            frmControlPlanificacion.Instancia.TopLevel = false;
            frmControlPlanificacion.Instancia.Parent = areaTrabajo;
            frmControlPlanificacion.Instancia.Location = PosicionarFormulario();
            frmControlPlanificacion.Instancia.Show();
            frmControlPlanificacion.Instancia.Focus();
        }
        #endregion
    }
}
