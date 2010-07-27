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

        protected override void OnCloseUp(EventArgs eventargs)
        {
            if (MouseButtons == System.Windows.Forms.MouseButtons.None)
            {
                this.SetFecha();
            }
            base.OnCloseUp(eventargs);
        }
    }
}
