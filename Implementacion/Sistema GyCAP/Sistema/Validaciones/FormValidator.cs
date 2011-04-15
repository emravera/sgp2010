﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Sistema.Validaciones
{
    public class FormValidator
    {
        /// <summary>
        /// Valida los siguientes controles que tengan su propiedad CausesValidation = true
        ///     - Textbox no vacíos ni sólo espacios en blanco
        ///     - DropDownList (GyCAP) con GetSelectedIndex o GetSelectedValue -1
        ///     - Combobox (.NET) con SelectedIndex -1
        ///     - seleccionadorFecha (GyCAP) sin fecha seleccionada
        ///     - DataGridView (.NET) con al menos una fila seleccionada
        /// </summary>
        /// <param name="formulario">El formulario a validar</param>
        /// <returns>true si es válido, false en caso contrario</returns>
        public static bool ValidarFormulario(Form formulario)
        {
            ErrorProvider errProvider;
            if (formulario.Tag != null) { errProvider = (ErrorProvider)formulario.Tag; }
            else
            {
                errProvider = new ErrorProvider();
                errProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                errProvider.ContainerControl = formulario;
                formulario.Tag = errProvider;
            }
            errProvider.Tag = true;
            foreach (Control ctrl in formulario.Controls)
            {                
                ProcessChildControls(ctrl, errProvider);
                ValidarControl(ctrl, errProvider);
            }

            return (bool)errProvider.Tag;
        }

        private static void ProcessChildControls(Control control, ErrorProvider provider)
        {
            foreach (Control ctrl in control.Controls)
            {
                ProcessChildControls(ctrl, provider);
                ValidarControl(ctrl, provider);
            }
        }

        private static void ValidarControl(Control ctrl, ErrorProvider provider)
        {            
            if (ctrl.GetType().Equals(typeof(TextBox)))
            {
                TextBox txt = (TextBox)ctrl;
                if (txt.CausesValidation && string.IsNullOrEmpty(txt.Text.Trim()))
                {
                    provider.SetError(ctrl, "El elemento no puede estar vacío.");
                    provider.Tag = false;
                }
                else { provider.SetError(ctrl, string.Empty); }
            }
            else if (ctrl.GetType().Equals(typeof(ControlesUsuarios.DropDownList)))
            {
                ControlesUsuarios.DropDownList ddl = (ControlesUsuarios.DropDownList)ctrl;
                if (ddl.CausesValidation && (ddl.GetSelectedIndex() == -1 || ddl.GetSelectedValueInt() == -1))
                {
                    provider.SetError(ctrl, "Debe seleccionar un elemento de la lista.");
                    provider.Tag = false;
                }
                else { provider.SetError(ctrl, string.Empty); }
            }
            else if (ctrl.GetType().Equals(typeof(ControlesUsuarios.seleccionadorFecha)))
            {
                ControlesUsuarios.seleccionadorFecha dtp = (ControlesUsuarios.seleccionadorFecha)ctrl;
                if (dtp.CausesValidation && dtp.GetFecha() == null)
                {
                    provider.SetError(ctrl, "Debe seleccionar una fecha.");
                    provider.Tag = false;
                }
                else { provider.SetError(ctrl, string.Empty); }
            }
            else if (ctrl.GetType().Equals(typeof(ComboBox)))
            {
                ComboBox cbo = (ComboBox)ctrl;
                if (cbo.CausesValidation && cbo.SelectedIndex == -1)
                {
                    provider.SetError(ctrl, "Debe seleccionar un elemento de la lista.");
                    provider.Tag = false;
                }
                else { provider.SetError(ctrl, string.Empty); }
            }
            else if (ctrl.GetType().Equals(typeof(DataGridView)))
            {
                DataGridView dgv = (DataGridView)ctrl;
                if (dgv.CausesValidation && dgv.SelectedRows.Count == 0)
                {
                    provider.SetError(ctrl, "Debe seleccionar un elemento de la grilla.");
                    provider.Tag = false;
                }
                else { provider.SetError(ctrl, string.Empty); }
            }
        }
    }
}