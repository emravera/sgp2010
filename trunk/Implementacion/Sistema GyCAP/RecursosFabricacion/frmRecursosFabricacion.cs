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

        
    }
}
