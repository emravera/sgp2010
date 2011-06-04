using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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

    public class DataGridViewState
    {
        public DataGridViewState()
        {
            this.wasProcessedForData = false;
            this.columnsVisibleCount = 0;
        }
        
        private bool wasProcessedForData;

        public bool WasProcessedForData
        {
            get { return wasProcessedForData; }
            set { wasProcessedForData = value; }
        }
        private int columnsVisibleCount;

        public int ColumnsVisibleCount
        {
            get { return columnsVisibleCount; }
            set { columnsVisibleCount = value; }
        }
    }

    public class FuncionesAuxiliares
    {
        #region Propiedades para el método que quita los checkbox de un TreeNode
        public const int TVIF_STATE = 0x8;
        public const int TVIS_STATEIMAGEMASK = 0xF000;
        public const int TV_FIRST = 0x1100;
        public const int TVM_SETITEM = TV_FIRST + 63;

        public struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public String lpszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage;
            public int cChildren;
            public IntPtr lParam;

        }

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        #endregion

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

        /// <summary>
        /// Remueve el checkbox de un nodo de un TreeView.
        /// </summary>
        /// <param name="nodo">El Treenode (nodo) al cual se debe quitar el checkbox.</param>
        /// <param name="treeview">El TreeView al que pertenece el nodo.</param>
        public static void QuitarCheckBox(TreeNode nodo, TreeView treeview)
        {
            TVITEM tvi = new TVITEM();
            tvi.hItem = nodo.Handle;
            tvi.mask = TVIF_STATE;
            tvi.stateMask = TVIS_STATEIMAGEMASK;
            tvi.state = 0;
            IntPtr lparam = Marshal.AllocHGlobal(Marshal.SizeOf(tvi));
            Marshal.StructureToPtr(tvi, lparam, false);
            SendMessage(treeview.Handle, TVM_SETITEM, IntPtr.Zero, lparam);
        }

        public static void SetDataGridViewColumnsSize(DataGridView grilla)
        {
            grilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grilla.AllowUserToResizeColumns = true;
            if (grilla.Tag == null) { grilla.Tag = new DataGridViewState(); }
            if (grilla.RowCount == 0 || grilla.ColumnCount == 0) 
            { 
                grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                (grilla.Tag as DataGridViewState).WasProcessedForData = false;
            }
            else
            {
                int divisor = 0;
                int visibleColumns = 0;
                for (int i = 0; i < grilla.ColumnCount; i++) { if (grilla.Columns[i].Visible) { visibleColumns++; } }

                if (!(grilla.Tag as DataGridViewState).WasProcessedForData && (grilla.Tag as DataGridViewState).ColumnsVisibleCount != visibleColumns)
                {
                    grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                    for (int i = 0; i < grilla.ColumnCount; i++)
                    {
                        if (grilla.Columns[i].Visible) { divisor++; }
                    }

                    int size = (grilla.Width / divisor) + 1;

                    for (int i = 0; i < grilla.ColumnCount; i++)
                    {
                        grilla.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        grilla.Columns[i].Width = size;
                    }

                    (grilla.Tag as DataGridViewState).WasProcessedForData = true;
                    (grilla.Tag as DataGridViewState).ColumnsVisibleCount = visibleColumns;
                }
            }
        }

    }

}
