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
                    Item item = new Item(dr[DisplayMember].ToString(), Convert.ToInt32(dr[ValueMember].ToString()));
                    control.Items.Add(item);
                }
            }
            control.SelectedIndex = 0;
        }

        /// <summary>
        /// Convierte una hora en valor decimal al formato horas:minutos, ej. 1.25 a 1:15, 1.5 a 1:30
        /// </summary>
        /// <param name="hours">Valor decimal de la hora</param>
        /// <param name="format">Cadena con el formato deseado, opcional, donde {0} es horas y {1} es minutos.</param>
        /// <returns></returns>
        public static string DecimalHourToString(decimal hours, string format)
        {
            if (string.IsNullOrEmpty(format)) format = "{0}:{1}";
            TimeSpan tspan = TimeSpan.FromHours((double)hours);
            string minutos = tspan.Minutes.ToString();
            if (tspan.Minutes.ToString().Length == 1 && tspan.Minutes > 0) minutos = "0" + minutos;
            if (tspan.Minutes.ToString().Length == 1 && tspan.Minutes == 0) minutos += "0";
            return string.Format(format, tspan.Hours, minutos);
        }

        /// <summary>
        /// Convierte una hora en valor decimal al formato horas:minutos, ej. 1.25 a 1:15, 1.5 a 1:30
        /// </summary>
        /// <param name="hours">Valor decimal de la hora</param>
        public static string DecimalHourToString(decimal hours)
        {
            return DecimalHourToString(hours, null);
        }

        /// <summary>
        /// Convierte una hora de un string con el formato HH:mm en un decimal.
        /// </summary>
        /// <param name="hora">El string con la hora en formato HH:mm</param>
        /// <returns></returns>
        public static decimal StringHourToDecimal(string hora)
        {
            if (string.IsNullOrEmpty(hora)) return 0;
            string[] temp = hora.Split(':');
            decimal horas = decimal.Parse(temp[0]);
            decimal minutos = decimal.Parse(temp[1]) / 60;
            return horas + minutos;
        }

        /// <summary>
        /// Convierte un decimal en un datetime con la hora proporcionada.
        /// </summary>
        /// <param name="hora">El valor decimal de la hora.</param>
        /// <returns></returns>
        public static DateTime DecimalToDateTime(decimal hora)
        {
            TimeSpan tspan = TimeSpan.FromHours((double)hora);
            return new DateTime(2000, 1, 1, tspan.Hours, tspan.Minutes, 0);
        }

    }

}
