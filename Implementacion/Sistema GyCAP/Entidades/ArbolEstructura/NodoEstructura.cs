using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.ArbolEstructura
{
    public class NodoEstructura
    {
        public enum tipoContenido { Parte, MateriaPrima, ProductoFinal };
        
        private int codigoNodo;
        private string text;
        private IList<NodoEstructura> nodosHijos;
        private NodoEstructura nodoPadre;
        private tipoContenido contenido;
        private object tag;
        private CompuestoParte compuesto;

        public int CodigoNodo
        {
            get { return codigoNodo; }
            set { codigoNodo = value; }
        }        

        public string Text
        {
            get { return text; }
            set { text = value; }
        }        

        public IList<NodoEstructura> NodosHijos
        {
            get { return nodosHijos; }
            set { nodosHijos = value; }
        }        

        public NodoEstructura NodoPadre
        {
            get { return nodoPadre; }
            set { nodoPadre = value; }
        }        

        public tipoContenido Contenido
        {
            get { return contenido; }
            set { contenido = value; }
        }        

        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public CompuestoParte Compuesto
        {
            get { return compuesto; }
            set { compuesto = value; }
        }

        

        public decimal GetCosto()
        {
            decimal costo = 0;
            
            foreach (NodoEstructura nodo in nodosHijos)
            {
                costo += nodo.GetCosto();
            }

            if (this.contenido == tipoContenido.MateriaPrima) { return this.compuesto.MateriaPrima.Costo; }

            return costo;
        }
    }
}
