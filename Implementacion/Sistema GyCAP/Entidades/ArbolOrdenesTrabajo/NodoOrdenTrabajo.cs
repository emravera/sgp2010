using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.Entidades.ArbolOrdenesTrabajo
{
    public class NodoOrdenTrabajo
    {
        private int codigoNodo;

        public int CodigoNodo
        {
            get { return codigoNodo; }
            set { codigoNodo = value; }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        private OrdenTrabajo ordenTrabajo;

        public OrdenTrabajo OrdenTrabajo
        {
            get { return ordenTrabajo; }
            set { ordenTrabajo = value; }
        }

        private NodoOrdenTrabajo nodoPadre;

        public NodoOrdenTrabajo NodoPadre
        {
            get { return nodoPadre; }
            set { nodoPadre = value; }
        }

        private IList<NodoOrdenTrabajo> nodosHijos;

        public IList<NodoOrdenTrabajo> NodosHijos
        {
            get { return nodosHijos; }
            set { nodosHijos = value; }
        }

        public void AsOrdenesTrabajoList(IList<OrdenTrabajo> lista)
        {
            lista.Add(this.ordenTrabajo);

            foreach (NodoOrdenTrabajo nodo in this.NodosHijos)
            {
                nodo.AsOrdenesTrabajoList(lista);
            }
        }

        public TreeNode AsTreeNode()
        {
            TreeNode nodo = new TreeNode();
            nodo.Text = this.ordenTrabajo.Origen + ((this.NodoPadre != null) ? " - " + this.NodoPadre.ordenTrabajo.Origen : string.Empty);
            nodo.Name = this.ordenTrabajo.Numero.ToString();

            foreach (NodoOrdenTrabajo item in this.NodosHijos)
            {
                nodo.Nodes.Add(item.AsTreeNode());
            }

            return nodo;
        }
    }
}
