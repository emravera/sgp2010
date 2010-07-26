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

            public ItemLista(int cod, string nom)
            {
                this.name = nom;
                this.value = cod;
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
            this.DisplayMember = "name";
            this.ValueMember = "value";

            if (cabeceraPersistente) { this.SelectedIndex = 0; }
            else { this.SelectedIndex = -1; }
        }
    }
}
