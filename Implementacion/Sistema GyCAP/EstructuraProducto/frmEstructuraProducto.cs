using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.EstructuraProducto
{
    public partial class frmEstructuraProducto : Form
    {
        private static frmEstructuraProducto _frmEstructuraProducto = null;
        private SplitterPanel areaTrabajo;
        private Panel activo = null;
        private Sistema.ControlesUsuarios.AnimadorFormulario animador = new GyCAP.UI.Sistema.ControlesUsuarios.AnimadorFormulario();
        
        public frmEstructuraProducto()
        {
            InitializeComponent();

            areaTrabajo = scUp.Panel2;
            flpMenu.AutoScroll = false;
            btnCocina.Tag = panelCocina;
            btnColor.Tag = panelColor;
            btnConjunto.Tag = panelConjunto;
            btnDesignacion.Tag = panelDesignacion;
            btnEstructuraProducto.Tag = panelEstructuraCocina;
            btnHojaRuta.Tag = panelHojaRuta;
            btnMPPrincipal.Tag = panelMPPrincipal;
            btnModeloCocina.Tag = panelModeloCocina;
            btnPieza.Tag = panelPieza;
            btnSubconjunto.Tag = panelSubconjunto;
            btnTerminacion.Tag = panelTerminacion;
            btnUnidadMedida.Tag = panelUnidadMedida;
            Size size = new Size(panelCocina.Size.Width, 0);
            panelCocina.Size = size;
            panelColor.Size = size;
            panelConjunto.Size = size;
            panelDesignacion.Size = size;
            panelEstructuraCocina.Size = size;
            panelHojaRuta.Size = size;
            panelModeloCocina.Size = size;
            panelMPPrincipal.Size = size;
            panelPieza.Size = size;
            panelSubconjunto.Size = size;
            panelTerminacion.Size = size;
            panelUnidadMedida.Size = size;
        }

        public static frmEstructuraProducto Instancia
        {
            get
            {
                if (_frmEstructuraProducto == null || _frmEstructuraProducto.IsDisposed)
                {
                    _frmEstructuraProducto = new frmEstructuraProducto();
                }
                else
                {
                    _frmEstructuraProducto.BringToFront();
                }
                return _frmEstructuraProducto;
            }
            set
            {
                _frmEstructuraProducto = value;
            }
        }

        private void frmEstructuraProducto_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private Point PosicionarFormulario()
        {
            int posicion = areaTrabajo.Controls.Count;
            Point location = new Point(posicion*15, posicion*15);
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

        private void scUp_Panel2_ControlRemoved(object sender, ControlEventArgs e)
        {
            areaTrabajo.Focus();
        }

        #endregion

        #region Cocina

        private void btnConsultarCocina_Click(object sender, EventArgs e)
        {
            frmCocina.Instancia.TopLevel = false;
            frmCocina.Instancia.Parent = areaTrabajo;
            frmCocina.Instancia.Location = PosicionarFormulario();
            frmCocina.Instancia.SetEstadoInicial(frmCocina.estadoInicialConsultar);
            frmCocina.Instancia.Show();
            frmCocina.Instancia.Focus();
        }

        private void btnNuevoCocina_Click(object sender, EventArgs e)
        {
            frmCocina.Instancia.TopLevel = false;
            frmCocina.Instancia.Parent = areaTrabajo;
            frmCocina.Instancia.Location = PosicionarFormulario();
            frmCocina.Instancia.SetEstadoInicial(frmCocina.estadoInicialNuevo);
            frmCocina.Instancia.Show();
            frmCocina.Instancia.Focus();
        }

        private void btnListadoCocina_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Color

        private void btnConsultarColor_Click(object sender, EventArgs e)
        {
            frmColor.Instancia.TopLevel = false;
            frmColor.Instancia.Parent = areaTrabajo;
            frmColor.Instancia.Location = PosicionarFormulario();
            frmColor.Instancia.Show();
            frmColor.Instancia.Focus();
        }

        #endregion
        
        #region Conjunto

        private void btnNuevoConjunto_Click(object sender, EventArgs e)
        {
            frmConjunto.Instancia.TopLevel = false;
            frmConjunto.Instancia.Parent = areaTrabajo;
            frmConjunto.Instancia.Location = PosicionarFormulario();
            frmConjunto.Instancia.SetEstadoInicial(frmConjunto.estadoInicialNuevo);
            frmConjunto.Instancia.Show();
            frmConjunto.Instancia.Focus();
        }

        private void btnConsultarConjunto_Click(object sender, EventArgs e)
        {
            frmConjunto.Instancia.TopLevel = false;
            frmConjunto.Instancia.Parent = areaTrabajo;
            frmConjunto.Instancia.Location = PosicionarFormulario();
            frmConjunto.Instancia.SetEstadoInicial(frmConjunto.estadoInicialConsultar);
            frmConjunto.Instancia.Show();
            frmConjunto.Instancia.Focus();
        }

        private void btnListadoConjunto_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Designación

        private void btnNuevoDesignacion_Click(object sender, EventArgs e)
        {
            frmDesignacion.Instancia.TopLevel = false;
            frmDesignacion.Instancia.Parent = areaTrabajo;
            frmDesignacion.Instancia.Location = PosicionarFormulario();
            frmDesignacion.Instancia.SetEstadoInicial(frmDesignacion.estadoInicialNuevo);
            frmDesignacion.Instancia.Show();
            frmDesignacion.Instancia.Focus();
        }

        private void btnConsultarDesignacion_Click(object sender, EventArgs e)
        {
            frmDesignacion.Instancia.TopLevel = false;
            frmDesignacion.Instancia.Parent = areaTrabajo;
            frmDesignacion.Instancia.Location = PosicionarFormulario();
            frmDesignacion.Instancia.SetEstadoInicial(frmDesignacion.estadoInicialConsultar);
            frmDesignacion.Instancia.Show();
            frmDesignacion.Instancia.Focus();
        }

        #endregion

        #region Estructura Cocina

        private void btnNuevoEstructuraProducto_Click(object sender, EventArgs e)
        {
            frmEstructuraCocina.Instancia.TopLevel = false;
            frmEstructuraCocina.Instancia.Parent = areaTrabajo;
            frmEstructuraCocina.Instancia.Location = PosicionarFormulario();
            frmEstructuraCocina.Instancia.SetEstadoInicial(frmEstructuraCocina.estadoInicialNuevo);
            frmEstructuraCocina.Instancia.Show();
            frmEstructuraCocina.Instancia.Focus();
        }

        private void btnConsultarEstructuraProducto_Click(object sender, EventArgs e)
        {
            frmEstructuraCocina.Instancia.TopLevel = false;
            frmEstructuraCocina.Instancia.Parent = areaTrabajo;
            frmEstructuraCocina.Instancia.Location = PosicionarFormulario();
            frmEstructuraCocina.Instancia.SetEstadoInicial(frmEstructuraCocina.estadoInicialConsultar);
            frmEstructuraCocina.Instancia.Show();
            frmEstructuraCocina.Instancia.Focus();
        }

        private void btnListadoEstructuraProducto_Click(object sender, EventArgs e)
        {
            frmListadoEstructura.Instancia.TopLevel = false;
            frmListadoEstructura.Instancia.Parent = areaTrabajo;
            frmListadoEstructura.Instancia.Location = PosicionarFormulario();
            frmListadoEstructura.Instancia.Show();
            frmListadoEstructura.Instancia.Focus();
        }

        #endregion

        #region Hoja Ruta

        private void btnNuevoHojaRuta_Click(object sender, EventArgs e)
        {
            frmHojaRuta.Instancia.TopLevel = false;
            frmHojaRuta.Instancia.Parent = areaTrabajo;
            frmHojaRuta.Instancia.Location = PosicionarFormulario();
            frmHojaRuta.Instancia.SetEstadoInicial(frmHojaRuta.estadoInicialNuevo);
            frmHojaRuta.Instancia.Show();
            frmHojaRuta.Instancia.Focus();
        }

        private void btnConsultarhojaRuta_Click(object sender, EventArgs e)
        {
            frmHojaRuta.Instancia.TopLevel = false;
            frmHojaRuta.Instancia.Parent = areaTrabajo;
            frmHojaRuta.Instancia.Location = PosicionarFormulario();
            frmHojaRuta.Instancia.SetEstadoInicial(frmHojaRuta.estadoInicialConsultar);
            frmHojaRuta.Instancia.Show();
            frmHojaRuta.Instancia.Focus();
        }

        #endregion

        #region Materia Prima Principal

        private void btnConsultarMPPrincipal_Click(object sender, EventArgs e)
        {
            frmMateriaPrimaPrincipal.Instancia.TopLevel = false;
            frmMateriaPrimaPrincipal.Instancia.Parent = areaTrabajo;
            frmMateriaPrimaPrincipal.Instancia.Location = PosicionarFormulario();
            frmMateriaPrimaPrincipal.Instancia.Show();
            frmMateriaPrimaPrincipal.Instancia.Focus();
        }

        #endregion

        #region Modelo Cocina

        private void btnNuevoModeloCocina_Click(object sender, EventArgs e)
        {
            frmModeloCocina.Instancia.TopLevel = false;
            frmModeloCocina.Instancia.Parent = areaTrabajo;
            frmModeloCocina.Instancia.Location = PosicionarFormulario();
            frmModeloCocina.Instancia.SetEstadoInicial(frmModeloCocina.estadoInicialNuevo);
            frmModeloCocina.Instancia.Show();
            frmModeloCocina.Instancia.Focus();
        }

        private void btnConsultarModeloCocina_Click(object sender, EventArgs e)
        {
            frmModeloCocina.Instancia.TopLevel = false;
            frmModeloCocina.Instancia.Parent = areaTrabajo;
            frmModeloCocina.Instancia.Location = PosicionarFormulario();
            frmModeloCocina.Instancia.SetEstadoInicial(frmModeloCocina.estadoInicialConsultar);
            frmModeloCocina.Instancia.Show();
            frmModeloCocina.Instancia.Focus();
        }

        #endregion

        #region Pieza

        private void btnNuevoPieza_Click(object sender, EventArgs e)
        {
            frmPieza.Instancia.TopLevel = false;
            frmPieza.Instancia.Parent = areaTrabajo;
            frmPieza.Instancia.Location = PosicionarFormulario();
            frmPieza.Instancia.SetEstadoInicial(frmPieza.estadoInicialNuevo);
            frmPieza.Instancia.Show();
            frmPieza.Instancia.Focus();
        }

        private void btnConsultarPieza_Click(object sender, EventArgs e)
        {
            frmPieza.Instancia.TopLevel = false;
            frmPieza.Instancia.Parent = areaTrabajo;
            frmPieza.Instancia.Location = PosicionarFormulario();
            frmPieza.Instancia.SetEstadoInicial(frmPieza.estadoInicialConsultar);
            frmPieza.Instancia.Show();
            frmPieza.Instancia.Focus();
        }

        private void btnListadoPieza_Click(object sender, EventArgs e)
        {
            //frmPieza.Instancia.Location = PosicionarFormulario();
        }

        #endregion

        #region Subconjunto

        private void btnNuevoSubconjunto_Click(object sender, EventArgs e)
        {
            frmSubconjunto.Instancia.TopLevel = false;
            frmSubconjunto.Instancia.Parent = areaTrabajo;
            frmSubconjunto.Instancia.Location = PosicionarFormulario();
            frmSubconjunto.Instancia.SetEstadoInicial(frmSubconjunto.estadoInicialNuevo);
            frmSubconjunto.Instancia.Show();
            frmSubconjunto.Instancia.Focus();
        }

        private void btnConsultarSubconjunto_Click(object sender, EventArgs e)
        {
            frmSubconjunto.Instancia.TopLevel = false;
            frmSubconjunto.Instancia.Parent = areaTrabajo;
            frmSubconjunto.Instancia.Location = PosicionarFormulario();
            frmSubconjunto.Instancia.SetEstadoInicial(frmSubconjunto.estadoInicialConsultar);
            frmSubconjunto.Instancia.Show();
            frmSubconjunto.Instancia.Focus();
        }

        private void btnListadoSubconjunto_Click(object sender, EventArgs e)
        {
            //frmSubconjunto.Instancia.Location = PosicionarFormulario();
        }

        #endregion

        #region Terminación

        private void btnNuevoTerminacion_Click(object sender, EventArgs e)
        {
            frmTerminacion.Instancia.TopLevel = false;
            frmTerminacion.Instancia.Parent = areaTrabajo;
            frmTerminacion.Instancia.Location = PosicionarFormulario();
            frmTerminacion.Instancia.SetEstadoInicial(frmTerminacion.estadoInicialNuevo);
            frmTerminacion.Instancia.Show();
            frmTerminacion.Instancia.Focus();
        }

        private void btnConsultarTerminacion_Click(object sender, EventArgs e)
        {
            frmTerminacion.Instancia.TopLevel = false;
            frmTerminacion.Instancia.Parent = areaTrabajo;
            frmTerminacion.Instancia.Location = PosicionarFormulario();
            frmTerminacion.Instancia.SetEstadoInicial(frmTerminacion.estadoInicialConsultar);
            frmTerminacion.Instancia.Show();
            frmTerminacion.Instancia.Focus();
        }

        #endregion

        #region Unidad Medida

        private void btnNuevoUnidadMedida_Click(object sender, EventArgs e)
        {
            frmUnidadMedida.Instancia.TopLevel = false;
            frmUnidadMedida.Instancia.Parent = areaTrabajo;
            frmUnidadMedida.Instancia.Location = PosicionarFormulario();
            frmUnidadMedida.Instancia.SetEstadoInicial(frmUnidadMedida.estadoInicialNuevo);
            frmUnidadMedida.Instancia.Show();
            frmUnidadMedida.Instancia.Focus();
        }

        private void btnConsultarUnidadMedida_Click(object sender, EventArgs e)
        {
            frmUnidadMedida.Instancia.TopLevel = false;
            frmUnidadMedida.Instancia.Parent = areaTrabajo;
            frmUnidadMedida.Instancia.Location = PosicionarFormulario();
            frmUnidadMedida.Instancia.SetEstadoInicial(frmUnidadMedida.estadoInicialConsultar);
            frmUnidadMedida.Instancia.Show();
            frmUnidadMedida.Instancia.Focus();
        }

        #endregion

        

        
    }
}
