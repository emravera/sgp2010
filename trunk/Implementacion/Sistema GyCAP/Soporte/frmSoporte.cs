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

            areaTrabajo = scUp.Panel1;
            flpMenu.AutoScroll = false;
            btnCapacidadEmpleado.Tag = panelCapacidadEmpleado;
            btnLocalidad.Tag = panelLocalidad;
            btnMarca.Tag = panelMarca;
            btnProcesoFabricacion.Tag = panelProcesoFabricacion;
            btnProvincia.Tag = panelProvincia;
            btnSectorTrabajo.Tag = panelSectorTrabajo;
            btnTipoRepuesto.Tag = panelTipoRepuesto;
            btnTipoUnidadMedida.Tag = panelTipoUnidadMedida;
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

        private void btnMenu_Click(object sender, EventArgs e)
        {

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
