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

            areaTrabajo = scUp.Panel1;
            flpMenu.AutoScroll = false;
            btnCocina.Tag = panelCocina;
            btnColor.Tag = panelColor;
            btnConjunto.Tag = panelConjunto;
            btnDesignacion.Tag = panelDesignacion;
            btnEstructuraProducto.Tag = panelEstructuraProducto;
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
        
    }
}
