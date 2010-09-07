using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.GestionPedido
{
    public partial class frmGestionPedido : Form
    {
        private static frmGestionPedido _frmGestionPedido = null;
        private SplitterPanel areaTrabajo;
        private Panel activo = null;
        
        public frmGestionPedido()
        {
            InitializeComponent();

            areaTrabajo = scUp.Panel2;
            flpMenu.AutoScroll = false;
            btnCliente.Tag = panelCliente;
            btnPedido.Tag = panelPedido;
            Size size = new Size(panelCliente.Size.Width, 0);
            panelCliente.Size = size;
            panelPedido.Size = size;
        }

        public static frmGestionPedido Instancia
        {
            get
            {
                if (_frmGestionPedido == null || _frmGestionPedido.IsDisposed)
                {
                    _frmGestionPedido = new frmGestionPedido();
                }
                else
                {
                    _frmGestionPedido.BringToFront();
                }
                return _frmGestionPedido;
            }
            set
            {
                _frmGestionPedido = value;
            }
        }

        private void frmGestionPedido_Load(object sender, EventArgs e)
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

        #region Cliente

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            frmCliente.Instancia.TopLevel = false;
            frmCliente.Instancia.Parent = areaTrabajo;
            frmCliente.Instancia.Location = PosicionarFormulario();
            frmCliente.Instancia.SetEstadoInicial(frmCliente.estadoInicialNuevo);
            frmCliente.Instancia.Show();
            frmCliente.Instancia.Focus();
        }

        private void btnConsultarCliente_Click(object sender, EventArgs e)
        {
            frmCliente.Instancia.TopLevel = false;
            frmCliente.Instancia.Parent = areaTrabajo;
            frmCliente.Instancia.Location = PosicionarFormulario();
            frmCliente.Instancia.SetEstadoInicial(frmCliente.estadoInicialConsultar);
            frmCliente.Instancia.Show();
            frmCliente.Instancia.Focus();
        }

        #endregion

        #region Pedido

        private void btnNuevoPedido_Click(object sender, EventArgs e)
        {
            frmPedidos.Instancia.TopLevel = false;
            frmPedidos.Instancia.Parent = areaTrabajo;
            frmPedidos.Instancia.Location = PosicionarFormulario();
            frmPedidos.Instancia.SetEstadoInicial(frmPedidos.estadoInicialNuevo);
            frmPedidos.Instancia.Show();
            frmPedidos.Instancia.Focus();
        }

        private void btnConsultarPedido_Click(object sender, EventArgs e)
        {
            frmPedidos.Instancia.TopLevel = false;
            frmPedidos.Instancia.Parent = areaTrabajo;
            frmPedidos.Instancia.Location = PosicionarFormulario();
            frmPedidos.Instancia.SetEstadoInicial(frmPedidos.estadoInicialConsultar);
            frmPedidos.Instancia.Show();
            frmPedidos.Instancia.Focus();
        }

        #endregion
    }
}
