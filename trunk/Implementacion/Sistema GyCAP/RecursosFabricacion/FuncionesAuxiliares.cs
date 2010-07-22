using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;


namespace GyCAP.UI.RecursosFabricacion
{
    public class FuncionesAuxiliares
    {
        public static void llenarListas(DataView dv,CheckedListBox control)
        {
            control.Items.Clear();

            foreach (DataRowView dr in dv ) 
            {   
                control.Items.Add(dr["SEC_NOMBRE"],true );
            }

            

        }
    }
}
