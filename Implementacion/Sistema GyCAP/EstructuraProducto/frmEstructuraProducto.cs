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
            btnMPPrincipal.Tag = panelMPPrincipal;
            btnModeloCocina.Tag = panelModeloCocina;
            btnPieza.Tag = panelPieza;
            btnSubconjunto.Tag = panelSubconjunto;
            btnTerminacion.Tag = panelTerminacion;
            btnUnidadMedida.Tag = panelUnidadMedida;
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

        #region Cocina

        //frmCocina.Instancia.Location = PosicionarFormulario();

        #endregion

        #region Color

        private void btnConsultarColor_Click(object sender, EventArgs e)
        {
            frmColor.Instancia.TopLevel = false;
            frmColor.Instancia.Parent = areaTrabajo;
            frmColor.Instancia.Location = PosicionarFormulario();
            frmColor.Instancia.Show();
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
        }

        private void btnConsultarConjunto_Click(object sender, EventArgs e)
        {
            frmConjunto.Instancia.TopLevel = false;
            frmConjunto.Instancia.Parent = areaTrabajo;
            frmConjunto.Instancia.Location = PosicionarFormulario();
            frmConjunto.Instancia.SetEstadoInicial(frmConjunto.estadoInicialConsultar);
            frmConjunto.Instancia.Show();
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
        }

        private void btnConsultarDesignacion_Click(object sender, EventArgs e)
        {
            frmDesignacion.Instancia.TopLevel = false;
            frmDesignacion.Instancia.Parent = areaTrabajo;
            frmDesignacion.Instancia.Location = PosicionarFormulario();
            frmDesignacion.Instancia.SetEstadoInicial(frmDesignacion.estadoInicialConsultar);
            frmDesignacion.Instancia.Show();
        }

        #endregion

        #region Estructura Producto

        //frmEstructuraProducto.Instancia.Location = PosicionarFormulario();

        private void btnConsultarEstructuraProducto_Click(object sender, EventArgs e)
        {
            frmEstructuraCocina.Instancia.TopLevel = false;
            frmEstructuraCocina.Instancia.Parent = areaTrabajo;
            frmEstructuraCocina.Instancia.Show();
        }

        #endregion

        #region Materia Prima Principal

        private void button15_Click(object sender, EventArgs e)
        {
            frmMateriaPrimaPrincipal.Instancia.TopLevel = false;
            frmMateriaPrimaPrincipal.Instancia.Parent = areaTrabajo;
            frmMateriaPrimaPrincipal.Instancia.Location = PosicionarFormulario();
            frmMateriaPrimaPrincipal.Instancia.Show();
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
        }

        private void btnConsultarModeloCocina_Click(object sender, EventArgs e)
        {
            frmModeloCocina.Instancia.TopLevel = false;
            frmModeloCocina.Instancia.Parent = areaTrabajo;
            frmModeloCocina.Instancia.Location = PosicionarFormulario();
            frmModeloCocina.Instancia.SetEstadoInicial(frmModeloCocina.estadoInicialConsultar);
            frmModeloCocina.Instancia.Show();
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
        }

        private void btnConsultarPieza_Click(object sender, EventArgs e)
        {
            frmPieza.Instancia.TopLevel = false;
            frmPieza.Instancia.Parent = areaTrabajo;
            frmPieza.Instancia.Location = PosicionarFormulario();
            frmPieza.Instancia.SetEstadoInicial(frmPieza.estadoInicialConsultar);
            frmPieza.Instancia.Show();
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
        }

        private void btnConsultarSubconjunto_Click(object sender, EventArgs e)
        {
            frmSubconjunto.Instancia.TopLevel = false;
            frmSubconjunto.Instancia.Parent = areaTrabajo;
            frmSubconjunto.Instancia.Location = PosicionarFormulario();
            frmSubconjunto.Instancia.SetEstadoInicial(frmSubconjunto.estadoInicialConsultar);
            frmSubconjunto.Instancia.Show();
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
        }

        private void btnConsultarTerminacion_Click(object sender, EventArgs e)
        {
            frmTerminacion.Instancia.TopLevel = false;
            frmTerminacion.Instancia.Parent = areaTrabajo;
            frmTerminacion.Instancia.Location = PosicionarFormulario();
            frmTerminacion.Instancia.SetEstadoInicial(frmTerminacion.estadoInicialConsultar);
            frmTerminacion.Instancia.Show();
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
        }

        private void btnConsultarUnidadMedida_Click(object sender, EventArgs e)
        {
            frmUnidadMedida.Instancia.TopLevel = false;
            frmUnidadMedida.Instancia.Parent = areaTrabajo;
            frmUnidadMedida.Instancia.Location = PosicionarFormulario();
            frmUnidadMedida.Instancia.SetEstadoInicial(frmUnidadMedida.estadoInicialConsultar);
            frmUnidadMedida.Instancia.Show();
        }

        #endregion

        


    }
}
