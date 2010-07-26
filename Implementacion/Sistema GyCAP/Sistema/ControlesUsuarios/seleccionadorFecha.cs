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
		}

        public void setNull()
        {
            this.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.CustomFormat = " ";
            this.isNull = true;
        }

        public bool isValueNull()
        {
            return isNull;
        }

        public void setFecha()
        {
            this.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.isNull = false;
        }

        protected override void OnCloseUp(EventArgs eventargs)
        {
            if (MouseButtons == System.Windows.Forms.MouseButtons.None)
            {
                this.setFecha();
            }
            base.OnCloseUp(eventargs);
        }
    }
}
