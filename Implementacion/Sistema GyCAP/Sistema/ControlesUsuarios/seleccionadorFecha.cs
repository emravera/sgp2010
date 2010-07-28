using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Data;

namespace GyCAP.UI.Sistema.ControlesUsuarios
{
    public class seleccionadorFecha : System.Windows.Forms.DateTimePicker
    {
        private bool isNull = false;
        private bool firstFocusHappend = false;
        private bool keyPressed = false;
        
        public seleccionadorFecha() : base()
		{
            this.Format = DateTimePickerFormat.Short;
            this.ShowCheckBox = false;
            SetFechaNull();
		}

        public void SetFechaNull()
        {
            this.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.CustomFormat = " ";
            this.isNull = true;
        }

        public bool IsValueNull()
        {
            return isNull;
        }

        private void SetFecha()
        {
            this.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.isNull = false;
        }

        public void SetFecha(DateTime fecha)
        {
            if (fecha != null)
            {
                this.Value = fecha;
                this.isNull = false;
            }
            else
            {
                SetFechaNull();
            }
        }

        public void SetFocus()
        {
            this.Focus();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (firstFocusHappend == false && keyPressed == false)
            {
                firstFocusHappend = true;
                SetFecha();
            }
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (!keyPressed) { firstFocusHappend = false; }
            base.OnLostFocus(e);
        }
        
        protected override void OnCloseUp(EventArgs eventargs)
        {
            if (MouseButtons == System.Windows.Forms.MouseButtons.None)
            {
                this.SetFecha();
            }
            base.OnCloseUp(eventargs);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                keyPressed = true;
                SetFechaNull();
            }
            base.OnKeyUp(e);
        }
    }
}
