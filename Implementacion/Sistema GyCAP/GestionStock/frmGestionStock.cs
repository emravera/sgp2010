using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.GestionStock
{
    public partial class frmGestionStock : Form
    {
        private static frmGestionStock _frmGestionStock = null;
        private SplitterPanel areaTrabajo;
        private Panel activo = null;

        #region Inicio

        public frmGestionStock()
        {
            InitializeComponent();

            areaTrabajo = scUp.Panel2;
            flpMenu.AutoScroll = false;
            btnInventarioAbc.Tag = panelInventarioABC;
            btnUbicacionStock.Tag = panelUbicacionStock;
            btnEntregaProducto.Tag = panelEntregaProducto;
            btnActualizacionStock.Tag = panelActualizacionStock;
            Size size = new Size(panelUbicacionStock.Size.Width, 0);
            panelInventarioABC.Size = size;
            panelUbicacionStock.Size = size;
            panelEntregaProducto.Size = size;
            panelActualizacionStock.Size = size;
        }

        public static frmGestionStock Instancia
        {
            get
            {
                if (_frmGestionStock == null || _frmGestionStock.IsDisposed)
                {
                    _frmGestionStock = new frmGestionStock();
                }
                else
                {
                    _frmGestionStock.BringToFront();
                }
                return _frmGestionStock;
            }
            set
            {
                _frmGestionStock = value;
            }
        }

        private void frmGestionStock_Load(object sender, EventArgs e)
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

        #region Ubicacion stock
        private void btnNuevoUbicacionStock_Click(object sender, EventArgs e)
        {
            frmUbicacionStock.Instancia.TopLevel = false;
            frmUbicacionStock.Instancia.Parent = areaTrabajo;
            frmUbicacionStock.Instancia.Location = PosicionarFormulario();
            frmUbicacionStock.Instancia.btnNuevo.PerformClick();
            frmUbicacionStock.Instancia.Show();
            frmUbicacionStock.Instancia.Focus();
        }

        private void btnConsultarUbicacionStock_Click(object sender, EventArgs e)
        {
            frmUbicacionStock.Instancia.TopLevel = false;
            frmUbicacionStock.Instancia.Parent = areaTrabajo;
            frmUbicacionStock.Instancia.Location = PosicionarFormulario();
            frmUbicacionStock.Instancia.Show();
            frmUbicacionStock.Instancia.Focus();
        }
        #endregion

        #region Inventario ABC
        private void btnConsultarInventarioAbc_Click(object sender, EventArgs e)
        {
            frmInventarioABC.Instancia.TopLevel = false;
            frmInventarioABC.Instancia.Parent = areaTrabajo;
            frmInventarioABC.Instancia.Location = PosicionarFormulario();
            frmInventarioABC.Instancia.Show();
            frmInventarioABC.Instancia.Focus();
        }
        #endregion

        #region Entrega Producto
        private void btnNuevoEntregaProducto_Click(object sender, EventArgs e)
        {
            frmEntregaProducto.Instancia.TopLevel = false;
            frmEntregaProducto.Instancia.Location = PosicionarFormulario();
            frmEntregaProducto.Instancia.Parent = areaTrabajo;
            frmEntregaProducto.Instancia.Show();
            frmEntregaProducto.Instancia.btnNuevo.PerformClick();
            frmEntregaProducto.Instancia.Focus();
        }

        private void btnConsultarEntregaProducto_Click(object sender, EventArgs e)
        {
            frmEntregaProducto.Instancia.TopLevel = false;
            frmEntregaProducto.Instancia.Location = PosicionarFormulario();
            frmEntregaProducto.Instancia.Parent = areaTrabajo;
            frmEntregaProducto.Instancia.Show();
            frmEntregaProducto.Instancia.Focus();
        }
        #endregion

        #region Actualizacion Stock
        private void btnConsultarActualizacionStock_Click(object sender, EventArgs e)
        {
            frmActualizacionStock.Instancia.TopLevel = false;
            frmActualizacionStock.Instancia.Location = PosicionarFormulario();
            frmActualizacionStock.Instancia.Parent = areaTrabajo;
            frmActualizacionStock.Instancia.Show();
            frmActualizacionStock.Instancia.Focus();
        }
        #endregion
    }
}
