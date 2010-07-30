using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.RecursosFabricacion
{
    public partial class frmRFAsignarCapacidad : Form
    {
        private static frmRFAsignarCapacidad _frmRFAsignarCapacidad = null;
        private Data.dsEmpleado dsEmpleado = new GyCAP.Data.dsEmpleado();
        private DataView dvCapacidadEmpleado, dvEmpleado;

        public frmRFAsignarCapacidad()
        {
            InitializeComponent();



            //Carga de la Lista de Sectores
            dvCapacidadEmpleado = new DataView(dsEmpleado.CAPACIDAD_EMPLEADOS);
            lvCapacidades.View = View.Details;
            lvCapacidades.FullRowSelect = true;
            lvCapacidades.MultiSelect = false;
            lvCapacidades.CheckBoxes = true;
            lvCapacidades.GridLines = true;
            lvCapacidades.Columns.Add("Capacidades", 220);
            lvCapacidades.Columns.Add("Codigo", 0);
            if (dvCapacidadEmpleado.Count != 0)
            {
                foreach (DataRowView dr in dvCapacidadEmpleado)
                {
                    ListViewItem li = new ListViewItem(dr["CEMP_NOMBRE"].ToString());
                    li.SubItems.Add(dr["CEMP_CODIGO"].ToString());
                    li.Checked = true;
                    lvCapacidades.Items.Add(li);
                }
            }

            //CARGA DE COMBOS
            //Creamos el Dataview y se lo asignamos al combo
            dvEmpleado = new DataView(dsEmpleado.EMPLEADOS);
            cboEmpleado.SetDatos(dvEmpleado, "e_codigo", "e_nombre", "Seleccione un Empleado...", true);
        }
    }
}
