using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.RecursosFabricacion
{
    public partial class frmRecursosFabricacion : Form
    {
        private static frmRecursosFabricacion _frmRecursosFabricacion = null;
        private SplitterPanel areaTrabajo;
        private Panel activo = null;
        
        public frmRecursosFabricacion()
        {
            InitializeComponent();

            areaTrabajo = scUp.Panel2;
            flpMenu.AutoScroll = false;
            btnEmpleado.Tag = panelEmpleado;
            btnMaquina.Tag = panelMaquina;            
            btnProductividad.Tag = panelProductividad;
            btnSectorTrabajo.Tag = panelSectorTrabajo;
            btnCentroTrabajo.Tag = panelCentroTrabajo;
            Size size = new Size(panelEmpleado.Size.Width, 0);
            panelEmpleado.Size = size;
            panelMaquina.Size = size;
            panelProductividad.Size = size;
            panelSectorTrabajo.Size = size;
            panelCentroTrabajo.Size = size;
        }

        private void frmRecursosFabricacion_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        public static frmRecursosFabricacion Instancia
        {
            get
            {
                if (_frmRecursosFabricacion == null || _frmRecursosFabricacion.IsDisposed)
                {
                    _frmRecursosFabricacion = new frmRecursosFabricacion();
                }
                else
                {
                    _frmRecursosFabricacion.BringToFront();
                }
                return _frmRecursosFabricacion;
            }
            set
            {
                _frmRecursosFabricacion = value;
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
            Point punto = new Point((sender as Button).Location.X + 2, (sender as Button).Location.Y + 2);
            (sender as Button).Location = punto;
        }

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            Point punto = new Point((sender as Button).Location.X - 2, (sender as Button).Location.Y - 2);
            (sender as Button).Location = punto;
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

        #region Centro de Trabajo

        private void btnListadoCentroTrabajo_Click(object sender, EventArgs e)
        {
            frmCentroTrabajo.Instancia.TopLevel = false;
            frmCentroTrabajo.Instancia.Parent = areaTrabajo;
            frmCentroTrabajo.Instancia.Location = PosicionarFormulario();
            frmCentroTrabajo.Instancia.SetEstadoInicial(frmCentroTrabajo.estadoInicialNuevo);
            frmCentroTrabajo.Instancia.Show();
            frmCentroTrabajo.Instancia.Focus();
        }

        private void btnConsultarCentroTrabajo_Click(object sender, EventArgs e)
        {
            frmCentroTrabajo.Instancia.TopLevel = false;
            frmCentroTrabajo.Instancia.Parent = areaTrabajo;
            frmCentroTrabajo.Instancia.Location = PosicionarFormulario();
            frmCentroTrabajo.Instancia.SetEstadoInicial(frmCentroTrabajo.estadoInicialConsultar);
            frmCentroTrabajo.Instancia.Show();
            frmCentroTrabajo.Instancia.Focus();
        }

        private void btnNuevoCentroTrabajo_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Empleado

        private void btnNuevoEmpleado_Click(object sender, EventArgs e)
        {
            frmEmpleado.Instancia.TopLevel = false;
            frmEmpleado.Instancia.Parent = areaTrabajo;
            frmEmpleado.Instancia.Location = PosicionarFormulario();
            frmEmpleado.Instancia.SetEstadoInicial(frmEmpleado.estadoInicialNuevo);
            frmEmpleado.Instancia.Show();
            frmEmpleado.Instancia.Focus();
        }

        private void btnConsultarEmpleado_Click(object sender, EventArgs e)
        {
            frmEmpleado.Instancia.TopLevel = false;
            frmEmpleado.Instancia.Parent = areaTrabajo;
            frmEmpleado.Instancia.Location = PosicionarFormulario();
            frmEmpleado.Instancia.SetEstadoInicial(frmEmpleado.estadoInicialConsultar);
            frmEmpleado.Instancia.Show();
            frmEmpleado.Instancia.Focus();
        }

        private void btnListadoEmpleado_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Máquina

        private void btnNuevoMaquina_Click(object sender, EventArgs e)
        {
            frmRFMaquina.Instancia.TopLevel = false;
            frmRFMaquina.Instancia.Parent = areaTrabajo;
            frmRFMaquina.Instancia.Location = PosicionarFormulario();
            frmRFMaquina.Instancia.SetEstadoInicial(frmRFMaquina.estadoInicialNuevo);
            frmRFMaquina.Instancia.Show();
            frmRFMaquina.Instancia.Focus();
        }

        private void btnConsultarMaquina_Click(object sender, EventArgs e)
        {
            frmRFMaquina.Instancia.TopLevel = false;
            frmRFMaquina.Instancia.Parent = areaTrabajo;
            frmRFMaquina.Instancia.Location = PosicionarFormulario();
            frmRFMaquina.Instancia.SetEstadoInicial(frmRFMaquina.estadoInicialConsultar);
            frmRFMaquina.Instancia.Show();
            frmRFMaquina.Instancia.Focus();
        }

        private void btnListadoMaquina_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Productividad

        private void btnIndiceProductividad_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Sector Trabajo

        private void btnNuevoSectorTrabajo_Click(object sender, EventArgs e)
        {
            frmSectorTrabajo.Instancia.TopLevel = false;
            frmSectorTrabajo.Instancia.Parent = areaTrabajo;
            frmSectorTrabajo.Instancia.Location = PosicionarFormulario();
            frmSectorTrabajo.Instancia.SetEstadoInicial(frmSectorTrabajo.estadoInicialNuevo);
            frmSectorTrabajo.Instancia.Show();
            frmSectorTrabajo.Instancia.Focus();
        }

        private void btnConsultarSectorTrabajo_Click(object sender, EventArgs e)
        {
            frmSectorTrabajo.Instancia.TopLevel = false;
            frmSectorTrabajo.Instancia.Parent = areaTrabajo;
            frmSectorTrabajo.Instancia.Location = PosicionarFormulario();
            frmSectorTrabajo.Instancia.SetEstadoInicial(frmSectorTrabajo.estadoInicialConsultar);
            frmSectorTrabajo.Instancia.Show();
            frmSectorTrabajo.Instancia.Focus();
        }

        #endregion


    }
}
