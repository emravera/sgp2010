﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Seguridad
{
    public partial class frmLogin : Form
    {
        private static frmLogin _frm = null;
        //private GyCAP.Data.dsSeguridad dsSeguridad = new GyCAP.Data.dsSeguridad();
        //private DataView dvUsuario;

        public frmLogin()
        {
            InitializeComponent();


        }

        //Método para evitar la creación de más de una pantalla
        public static frmLogin Instancia
        {
            get
            {
                if (_frm == null || _frm.IsDisposed)
                {
                    _frm = new frmLogin();
                }
                else
                {
                    _frm.BringToFront();
                }
                return _frm;
            }
            set
            {
                _frm = value;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //this.Dispose(true);
            this.Close();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Revisamos que escribió algo
            if (Sistema.Validaciones.FormValidator.ValidarFormulario(this))
            {
                Entidades.Usuario u = new GyCAP.Entidades.Usuario();
                
                u.Login = txtLogin.Text.Trim();
                u.Password = txtPassword.Text.Trim();

                if (BLL.UsuarioBLL.validarUsuario(u))
                {
                    //Logueo CORRECTO
                    GyCAP.UI.Seguridad.frmUsuario frm = new frmUsuario();
                    frm.Show();
                    this.Close();
                }
                else 
                {
                    //Logueo INCORRECTO
                    MessageBox.Show("Logueo in correcto");
                    return;
                }

            }
        }
    }
}
