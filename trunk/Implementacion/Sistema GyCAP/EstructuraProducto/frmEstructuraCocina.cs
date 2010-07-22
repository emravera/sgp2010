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
    public partial class frmEstructuraCocina : Form
    {
        private static frmEstructuraCocina _frmEstructuraCocina = null;
        private enum estadoUI { inicio, nuevo, nuevoExterno, consultar, modificar, };
        private estadoUI estadoInterface;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar

        #region Inicio

        public frmEstructuraCocina()
        {
            InitializeComponent();
        }

        public static frmEstructuraCocina Instancia
        {
            get
            {
                if (_frmEstructuraCocina == null || _frmEstructuraCocina.IsDisposed)
                {
                    _frmEstructuraCocina = new frmEstructuraCocina();
                }
                else
                {
                    _frmEstructuraCocina.BringToFront();
                }
                return _frmEstructuraCocina;
            }
            set
            {
                _frmEstructuraCocina = value;
            }
        }

        public void SetEstadoInicial(int estado)
        {
            if (estado == estadoInicialNuevo) { SetInterface(estadoUI.nuevoExterno); }
            if (estado == estadoInicialConsultar) { SetInterface(estadoUI.inicio); }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

        }

        #endregion




        #region Servicios

        //Setea la pantalla de acuerdo al estado en que se encuentre
        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    /*bool hayDatos;

                    if (dsModeloCocina.MODELOS_COCINAS.Rows.Count == 0)
                    {
                        hayDatos = false;
                    }
                    else
                    {
                        hayDatos = true;
                    }

                    btnModificar.Enabled = hayDatos;
                    btnEliminar.Enabled = hayDatos;
                    btnConsultar.Enabled = hayDatos;
                    btnNuevo.Enabled = true;
                    estadoInterface = estadoUI.inicio;
                    tcModeloCocina.SelectedTab = tpBuscar;*/
                    break;
                case estadoUI.nuevo:
                    /* txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    txtDescripcion.Text = String.Empty;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevo;
                    tcModeloCocina.SelectedTab = tpDatos;*/
                    break;
                case estadoUI.nuevoExterno:
                    /* txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    txtNombre.Text = String.Empty;
                    txtDescripcion.Text = String.Empty;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.nuevoExterno;
                    tcModeloCocina.SelectedTab = tpDatos;*/
                    break;
                case estadoUI.consultar:
                    /* txtNombre.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    btnGuardar.Enabled = false;
                    btnVolver.Enabled = true;
                    estadoInterface = estadoUI.consultar;
                    tcModeloCocina.SelectedTab = tpDatos;*/
                    break;
                case estadoUI.modificar:
                    /* txtNombre.ReadOnly = false;
                    txtDescripcion.ReadOnly = false;
                    btnGuardar.Enabled = true;
                    btnVolver.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnConsultar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    estadoInterface = estadoUI.modificar;
                    tcModeloCocina.SelectedTab = tpDatos;*/
                    break;
                default:
                    break;
            }
        }

        #endregion

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            tcEstructuraCocina.SelectedTab = tpDatos;
        }
    }
}
