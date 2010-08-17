using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Soporte
{
    public partial class frmSoporte : Form
    {
        private static frmSoporte _frmSoporte = null;
        private SplitterPanel areaTrabajo;
        private Panel activo = null;
        
        public frmSoporte()
        {
            InitializeComponent();

            areaTrabajo = scUp.Panel2;
            flpMenu.AutoScroll = false;
            btnCapacidadEmpleado.Tag = panelCapacidadEmpleado;
            btnLocalidad.Tag = panelLocalidad;
            btnMarca.Tag = panelMarca;           
            btnProvincia.Tag = panelProvincia;            
            btnTipoRepuesto.Tag = panelTipoRepuesto;
            btnTipoUnidadMedida.Tag = panelTipoUnidadMedida;
            Size size = new Size(panelLocalidad.Size.Width, 0);
            panelCapacidadEmpleado.Size = size;
            panelLocalidad.Size = size;
            panelMarca.Size = size;
            panelProvincia.Size = size;
            panelTipoRepuesto.Size = size;
            panelTipoUnidadMedida.Size = size;
        }

        public static frmSoporte Instancia
        {
            get
            {
                if (_frmSoporte == null || _frmSoporte.IsDisposed)
                {
                    _frmSoporte = new frmSoporte();
                }
                else
                {
                    _frmSoporte.BringToFront();
                }
                return _frmSoporte;
            }
            set
            {
                _frmSoporte = value;
            }
        }

        private void frmSoporte_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
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

        #region Capacidad Empleado

        private void btnNuevoCapacidadEmpleado_Click(object sender, EventArgs e)
        {
            frmCapacidadEmpleado.Instancia.TopLevel = false;
            frmCapacidadEmpleado.Instancia.Parent = areaTrabajo;
            frmCapacidadEmpleado.Instancia.Location = PosicionarFormulario();
            frmCapacidadEmpleado.Instancia.SetEstadoInicial(frmCapacidadEmpleado.estadoInicialNuevo);
            frmCapacidadEmpleado.Instancia.Show();
            frmCapacidadEmpleado.Instancia.Focus();
        }

        private void btnConsultarCapacidadEmpleado_Click(object sender, EventArgs e)
        {
            frmCapacidadEmpleado.Instancia.TopLevel = false;
            frmCapacidadEmpleado.Instancia.Parent = areaTrabajo;
            frmCapacidadEmpleado.Instancia.Location = PosicionarFormulario();
            frmCapacidadEmpleado.Instancia.SetEstadoInicial(frmCapacidadEmpleado.estadoInicialConsultar);
            frmCapacidadEmpleado.Instancia.Show();
            frmCapacidadEmpleado.Instancia.Focus();
        }

        #endregion

        #region Localidad

        private void btnNuevoLocalidad_Click(object sender, EventArgs e)
        {
            frmLocalidad.Instancia.TopLevel = false;
            frmLocalidad.Instancia.Parent = areaTrabajo;
            frmLocalidad.Instancia.Location = PosicionarFormulario();
            frmLocalidad.Instancia.SetEstadoInicial(frmLocalidad.estadoInicialNuevo);
            frmLocalidad.Instancia.Show();
            frmLocalidad.Instancia.Focus();
        }

        private void btnConsultarLocalidad_Click(object sender, EventArgs e)
        {
            frmLocalidad.Instancia.TopLevel = false;
            frmLocalidad.Instancia.Parent = areaTrabajo;
            frmLocalidad.Instancia.Location = PosicionarFormulario();
            frmLocalidad.Instancia.SetEstadoInicial(frmLocalidad.estadoInicialConsultar);
            frmLocalidad.Instancia.Show();
            frmLocalidad.Instancia.Focus();
        }

        #endregion

        #region Marca

        private void btnNuevoMarca_Click(object sender, EventArgs e)
        {
            frmMarca.Instancia.TopLevel = false;
            frmMarca.Instancia.Parent = areaTrabajo;
            frmMarca.Instancia.Location = PosicionarFormulario();
            frmMarca.Instancia.SetEstadoInicial(frmMarca.estadoInicialNuevo);
            frmMarca.Instancia.Show();
            frmMarca.Instancia.Focus();
        }

        private void btnConsultarMarca_Click(object sender, EventArgs e)
        {
            frmMarca.Instancia.TopLevel = false;
            frmMarca.Instancia.Parent = areaTrabajo;
            frmMarca.Instancia.Location = PosicionarFormulario();
            frmMarca.Instancia.SetEstadoInicial(frmMarca.estadoInicialConsultar);
            frmMarca.Instancia.Show();
            frmMarca.Instancia.Focus();
        }

        #endregion

        #region Provincia

        private void btnConsultarProvincia_Click(object sender, EventArgs e)
        {
            frmProvincia.Instancia.TopLevel = false;
            frmProvincia.Instancia.Parent = areaTrabajo;
            frmProvincia.Instancia.Location = PosicionarFormulario();
            frmProvincia.Instancia.Show();
            frmProvincia.Instancia.Focus();
        }

        #endregion

        #region Tipo Repuesto
        #endregion

        #region TUMED

        private void btnConsultarTUMED_Click(object sender, EventArgs e)
        {
            frmTipoUnidadMedida.Instancia.TopLevel = false;
            frmTipoUnidadMedida.Instancia.Parent = areaTrabajo;
            frmTipoUnidadMedida.Instancia.Location = PosicionarFormulario();
            frmTipoUnidadMedida.Instancia.Show();
            frmTipoUnidadMedida.Instancia.Focus();
        }

        #endregion
 
    }
}
