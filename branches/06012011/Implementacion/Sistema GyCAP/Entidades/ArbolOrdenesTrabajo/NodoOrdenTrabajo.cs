using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
