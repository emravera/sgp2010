using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;


namespace GyCAP.UI.Sistema
{
    public class Item
    {
        private string name;
        private int value;

        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Item(string nombre, int valor)
        {
            Name = nombre;
            Value = valor;
        }

        public override string ToString()
        {
            return Name;
        }
    }

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

        public static void llenarCombos(DataView dv, ComboBox control, string ValueMember, string DisplayMember)
        {
            control.Items.Clear();

            dv.Sort = DisplayMember + " ASC";
            
            control.DataSource = dv;
            control.DropDownStyle = ComboBoxStyle.DropDownList;
            control.SelectedIndex = -1;
            if (dv.Count != 0)
            {
                control.DisplayMember = DisplayMember;
                control.ValueMember = ValueMember;
                //Para que el combo no quede selecionado cuando arranca y que sea una lista
                control.SelectedIndex = 0;
            }
        }

        public static void llenarCombosFiltros(DataView dv, ComboBox control, string ValueMember, string DisplayMember, string titulo)
        {
            control.Items.Clear();

            dv.Sort = DisplayMember + " ASC";

            control.DropDownStyle = ComboBoxStyle.DropDownList;

            //Agrego el primer item a mano
            Item filtro = new Item(titulo, 0);
            
            control.Items.Add(filtro);

            if (dv.Count != 0)
            {
                foreach (DataRowView dr in dv)
                {
                    Item item = new Item(dr[DisplayMember].ToString(), int.Parse(dr[ValueMember].ToString()));
                    control.Items.Add(item);
                }
            }
            control.SelectedIndex = 0;
        }
  
    }

}
