using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace GyCAP.UI.Sistema.ControlesUsuarios
{
    public class DropDownList : ComboBox
    {
        private class ItemLista
        {
            private int value;
            private string name;
            private object valueAlternativo;
          
            public ItemLista(int val, string nom)
            {
                this.name = nom;
                this.value = val;
            }

            public ItemLista(object val, string nom)
            {
                this.name = nom;
                this.valueAlternativo = val;
            }

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

            public object ValueAlternativo
            {
                get { return this.valueAlternativo; }
                set { this.valueAlternativo = value; }
            }
            
            public override string ToString()
            {
                return name;
            }
        }
        
        private Label texto = new Label();
        private bool persistente = false;
        
        public DropDownList() : base()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            texto.AutoSize = true;
            texto.Font = new System.Drawing.Font("Tahoma", 8);
            this.Font = new System.Drawing.Font("Tahoma", 8);
            this.Controls.Add(texto);
            texto.Location = new System.Drawing.Point(2, 4);
            texto.Visible = false;
            texto.Click += new EventHandler(texto_Click);
        }

        void texto_Click(object sender, EventArgs e)
        {
            this.DroppedDown = true;
        }

        //Oculta el texto al desplegar la lista si se marco como no persistente.
        protected override void OnDropDown(EventArgs e)
        {
            if (!persistente) { texto.Visible = false; }
            base.OnDropDown(e);
        }

        //Vuelve a mostrar el texto si se no se seleccionó un elemento.
        protected override void OnDropDownClosed(EventArgs e)
        {
            //if (!persistente && this.SelectedIndex == -1) { texto.Visible = true; }
            base.OnDropDownClosed(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (!persistente) { texto.Visible = false; }
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (!persistente && this.SelectedIndex == -1) { texto.Visible = true; }
            base.OnLostFocus(e);
        }

        /// <summary>
        /// Define el texto a mostrar en el combobox. Si se especifica un string.Empty se oculta.
        /// El texto mostrado desaperece al desplegar la lista del combobox u obtener el foco.
        /// </summary>
        /// <param name="txt">El texto a mostrar en el combobox.</param>
        public void SetTexto(string txt)
        {
            if (txt != string.Empty)
            {
                texto.Text = txt;
                texto.Visible = true;
                persistente = false;
                this.SelectedIndex = -1;
            }
            else { texto.Visible = false; }
        }

        /// <summary>
        /// Define el SelectedValue del combobox. Es decir que se seleccionará el item que posea el valor que
        /// se pase por parámetro.
        /// </summary>
        /// <param name="valor">El valor a asignar a SelectedValue.</param>
        public void SetSelectedValue(int valor)
        {
            this.SelectedValue = valor;
            if (!persistente) { texto.Visible = false; }
        }

        /// <summary>
        /// Define el SelectedIndex del combo.
        /// Si la cabecera es no persistente y el índice es -1 se muestra el texto. 
        /// Si la cabecera es no persistente y el índice es mayor a 0, se selecciona ese índice y oculta el texto.
        /// Si la cabecera es persistente y el índice es -1 o 0, se selecciona el primer elemento y oculta el texto.
        /// Si la cabecera es persistente y el índice es mayor a 0, se selecciona ese índice y oculta el texto.
        /// </summary>
        /// <param name="index">El índice a seleccionar.</param>
        public void SetSelectedIndex(int index)
        {
            this.SelectedIndex = index;
            if (index > 0) { texto.Visible = false; }
            if (index == -1 && persistente == false) { SetTexto(this.texto.Text); }
            if (index <= 0 && persistente == true) { this.SelectedIndex = 0; }
        }

        public object GetSelectedValue()
        {
            return this.SelectedValue;
        }

        public int GetSelectedValueInt()
        {
            return Convert.ToInt32(this.SelectedValue.ToString());
        }

        public string GetSelectedValueString()
        {
            return this.SelectedValue.ToString();
        }

        public decimal GetSelectedValueDecimal()
        {
            return Convert.ToDecimal(this.SelectedValue.ToString());
        }

        public DateTime GetSelectedValueDate()
        {
            return DateTime.Parse(this.SelectedValue.ToString());
        }

        public string GetSelectedText()
        {
            return (this.SelectedItem as ItemLista).Name;
        }

        public int GetSelectedIndex()
        {
            return this.SelectedIndex;
        }

        /// <summary>
        /// Carga los datos al combobox desde un dataview según el displayMember y valueMemeber especificado.
        /// Contiene una cabecera que puede formar parte de los items del combobox o como un simple texto. 
        /// La cabecera persistente que forma parte de los items se carga con valor -1 y se selecciona por defecto.
        /// La cabecera no persistente (como texto) desaparece al desplegar la lista del combobox u obtener el foco,
        /// se selecciona por defecto el índice -1.
        /// </summary>
        /// <param name="dataview">El dataview con los datos a cargar.</param>
        /// <param name="valueMember">El campo de dónde se tomará el valor.</param>
        /// <param name="displayMember">El campo de dónde se tomará el string a mostrar.</param>
        /// <param name="textoCabecera">El texto de la cabecera.</param>
        /// <param name="cabeceraPersistente">True para que forme parte de los items, false para simple texto.</param>
        public void SetDatos(DataView dataview, string valueMember, string displayMember, string textoCabecera, bool cabeceraPersistente)
        {
            persistente = cabeceraPersistente;
            this.DataSource = null;
            this.Items.Clear();
            IList<ItemLista> lista = new List<ItemLista>();
            if (cabeceraPersistente) { lista.Add(new ItemLista(-1, textoCabecera)); } 
            else { SetTexto(textoCabecera); }
           
            if (dataview.Count > 0)
            {
                dataview.Sort = displayMember + " ASC";
                foreach (DataRowView dr in dataview)
                {
                    lista.Add(new ItemLista(Convert.ToInt32(dr[valueMember].ToString()), dr[displayMember].ToString()));
                }
            }
            this.DataSource = lista;
            this.DisplayMember = "Name";
            this.ValueMember = "Value";

            if (cabeceraPersistente) { this.SelectedIndex = 0; }
            else { this.SelectedIndex = -1; }
        }

        /// <summary>
        /// Ofrece la posibilidad de concatenar varios displayMember.
        /// Carga los datos al combobox desde un dataview según el displayMember y valueMemeber especificado.
        /// Contiene una cabecera que puede formar parte de los items del combobox o como un simple texto. 
        /// La cabecera persistente que forma parte de los items se carga con valor -1 y se selecciona por defecto.
        /// La cabecera no persistente (como texto) desaparece al desplegar la lista del combobox u obtener el foco,
        /// se selecciona por defecto el índice -1.
        /// </summary>
        /// <param name="dataview">El dataview con los datos a cargar.</param>
        /// <param name="valueMember">El campo de dónde se tomará el valor.</param>
        /// <param name="displayMember">Un array de string con los campos de dónde se concatenará el string a mostrar.</param>
        /// <param name="textoCabecera">El texto de la cabecera.</param>
        /// <param name="cabeceraPersistente">True para que forme parte de los items, false para simple texto.</param>
        public void SetDatos(DataView dataview, string valueMember, string[] displayMember, string separadorConcatenado, string textoCabecera, bool cabeceraPersistente)
        {
            persistente = cabeceraPersistente;
            this.DataSource = null;
            this.Items.Clear();
            IList<ItemLista> lista = new List<ItemLista>();
            if (cabeceraPersistente) { lista.Add(new ItemLista(-1, textoCabecera)); }
            else { SetTexto(textoCabecera); }

            if (dataview.Count > 0)
            {
                string cadenaAuxiliar = string.Empty;
                for (int i = 0; i < displayMember.Length; i++)
                {
                    cadenaAuxiliar += displayMember[i] + " ASC ";
                    if (i + 1 < displayMember.Length) { cadenaAuxiliar += ","; }
                }
                dataview.Sort = cadenaAuxiliar;
                foreach (DataRowView dr in dataview)
                {
                    cadenaAuxiliar = string.Empty;
                    for (int i = 0; i < displayMember.Length; i++)
                    {
                        cadenaAuxiliar += dr[displayMember[i]].ToString();
                        if (i + 1 < displayMember.Length) { cadenaAuxiliar += separadorConcatenado; }
                    }
                    lista.Add(new ItemLista(Convert.ToInt32(dr[valueMember].ToString()), cadenaAuxiliar));
                }
            }
            this.DataSource = lista;
            this.DisplayMember = "Name";
            this.ValueMember = "Value";

            if (cabeceraPersistente) { this.SelectedIndex = 0; }
            else { this.SelectedIndex = -1; }
        }

        /// <summary>
        /// Carga los datos al combobox desde un array.
        /// Contiene una cabecera que puede formar parte de los items del combobox o como un simple texto. 
        /// La cabecera persistente que forma parte de los items se carga con valor -1 y se selecciona por defecto.
        /// La cabecera no persistente (como texto) desaparece al desplegar la lista del combobox u obtener el foco,
        /// se selecciona por defecto el índice -1.
        /// </summary>
        /// <param name="valores">El array de dónde se tomará el valor.</param>
        /// <param name="nombres">El array de dónde se tomará el string a mostrar.</param>
        /// <param name="textoCabecera">El texto de la cabecera.</param>
        /// <param name="cabeceraPersistente">True para que forme parte de los items, false para simple texto.</param>
        public void SetDatos(string[] nombres, int[] valores, string textoCabecera, bool cabeceraPersistente)
        {
            persistente = cabeceraPersistente;
            this.DataSource = null;
            this.Items.Clear();
            IList<ItemLista> lista = new List<ItemLista>();
            if (cabeceraPersistente) { lista.Add(new ItemLista(-1, textoCabecera)); }
            else { SetTexto(textoCabecera); }

            if (nombres.Length > 0 && valores.Length > 0 && nombres.Length == valores.Length)
            {
                for (int i = 0; i < valores.Length; i++)
                {
                    lista.Add(new ItemLista(valores[i], nombres[i]));
                }
            }
            this.DataSource = lista;
            this.DisplayMember = "Name";
            this.ValueMember = "Value";

            if (cabeceraPersistente) { this.SelectedIndex = 0; }
            else { this.SelectedIndex = -1; }
        }

        /// <summary>
        /// Carga los datos al combobox desde un array, recibe cualquier tipo de dato en valueMemeber.
        /// Contiene una cabecera que puede formar parte de los items del combobox o como un simple texto. 
        /// La cabecera persistente que forma parte de los items se carga con valor -1 y se selecciona por defecto.
        /// La cabecera no persistente (como texto) desaparece al desplegar la lista del combobox u obtener el foco,
        /// se selecciona por defecto el índice -1.
        /// </summary>
        /// <param name="valores">El array de object con los valores.</param>
        /// <param name="nombres">El array de dónde se tomará el string a mostrar.</param>
        /// <param name="textoCabecera">El texto de la cabecera.</param>
        /// <param name="cabeceraPersistente">True para que forme parte de los items, false para simple texto.</param>
        public void SetDatos(string[] nombres, object[] valores, string textoCabecera, bool cabeceraPersistente)
        {
            persistente = cabeceraPersistente;
            this.DataSource = null;
            this.Items.Clear();
            IList<ItemLista> lista = new List<ItemLista>();
            if (cabeceraPersistente) { lista.Add(new ItemLista(-1, textoCabecera)); }
            else { SetTexto(textoCabecera); }

            if (nombres.Length > 0 && valores.Length > 0 && nombres.Length == valores.Length)
            {
                for (int i = 0; i < valores.Length; i++)
                {
                    lista.Add(new ItemLista(valores[i], nombres[i]));
                }
            }
            this.DataSource = lista;
            this.DisplayMember = "Name";
            this.ValueMember = "Value";

            if (cabeceraPersistente) { this.SelectedIndex = 0; }
            else { this.SelectedIndex = -1; }
        }
    }
}
