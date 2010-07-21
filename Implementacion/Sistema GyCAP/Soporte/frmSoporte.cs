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

        #region Capacidad Empleado

        private void btnNuevoCapacidadEmpleado_Click(object sender, EventArgs e)
        {
            frmCapacidadEmpleado.Instancia.TopLevel = false;
            frmCapacidadEmpleado.Instancia.Parent = areaTrabajo;
            frmCapacidadEmpleado.Instancia.Location = PosicionarFormulario();
            frmCapacidadEmpleado.Instancia.SetEstadoInicial(frmCapacidadEmpleado.estadoInicialNuevo);
            frmCapacidadEmpleado.Instancia.Show();
        }

        private void btnConsultarCapacidadEmpleado_Click(object sender, EventArgs e)
        {
            frmCapacidadEmpleado.Instancia.TopLevel = false;
            frmCapacidadEmpleado.Instancia.Parent = areaTrabajo;
            frmCapacidadEmpleado.Instancia.Location = PosicionarFormulario();
            frmCapacidadEmpleado.Instancia.SetEstadoInicial(frmCapacidadEmpleado.estadoInicialConsultar);
            frmCapacidadEmpleado.Instancia.Show();
        }

        #endregion

        #region Localidad
        #endregion

        #region Marca

        private void btnNuevoMarca_Click(object sender, EventArgs e)
        {
            frmMarca.Instancia.TopLevel = false;
            frmMarca.Instancia.Parent = areaTrabajo;
            frmMarca.Instancia.Location = PosicionarFormulario();
            frmMarca.Instancia.SetEstadoInicial(frmMarca.estadoInicialNuevo);
            frmMarca.Instancia.Show();
        }

        private void btnConsultarMarca_Click(object sender, EventArgs e)
        {
            frmMarca.Instancia.TopLevel = false;
            frmMarca.Instancia.Parent = areaTrabajo;
            frmMarca.Instancia.Location = PosicionarFormulario();
            frmMarca.Instancia.SetEstadoInicial(frmMarca.estadoInicialConsultar);
            frmMarca.Instancia.Show();
        }

        #endregion

        #region Proceso Fabricación
        #endregion

        #region Provincia
        #endregion

        #region Sector Trabajo

        private void btnNuevoSectorTrabajo_Click(object sender, EventArgs e)
        {
            frmSectorTrabajo.Instancia.TopLevel = false;
            frmSectorTrabajo.Instancia.Parent = areaTrabajo;
            frmSectorTrabajo.Instancia.Location = PosicionarFormulario();
            frmSectorTrabajo.Instancia.SetEstadoInicial(frmSectorTrabajo.estadoInicialNuevo);
            frmSectorTrabajo.Instancia.Show();
        }

        private void btnConsultarSectorTrabajo_Click(object sender, EventArgs e)
        {
            frmSectorTrabajo.Instancia.TopLevel = false;
            frmSectorTrabajo.Instancia.Parent = areaTrabajo;
            frmSectorTrabajo.Instancia.Location = PosicionarFormulario();
            frmSectorTrabajo.Instancia.SetEstadoInicial(frmSectorTrabajo.estadoInicialConsultar);
            frmSectorTrabajo.Instancia.Show();
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
        }

        #endregion
    }
}
