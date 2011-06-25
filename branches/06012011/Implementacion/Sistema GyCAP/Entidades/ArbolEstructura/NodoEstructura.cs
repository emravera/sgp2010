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
        private List<NodoEstructura> nodosHijos;
        private NodoEstructura nodoPadre;
        private tipoContenido contenido;
        private object tag;
        private CompuestoParte compuesto;

        public NodoEstructura() 
        {
            this.nodosHijos = new List<NodoEstructura>();
        }

        public NodoEstructura(int codigoNode, string textoNode, NodoEstructura nodePadre, tipoContenido content, CompuestoParte comp)
        {
            this.codigoNodo = codigoNode;
            this.text = textoNode;
            this.NodosHijos = new List<NodoEstructura>();
            this.NodoPadre = nodePadre;
            this.contenido = content;
            this.compuesto = comp;
        }
        
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
        
        public List<NodoEstructura> NodosHijos
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
            if (this.contenido == tipoContenido.MateriaPrima) { return this.compuesto.MateriaPrima.Costo; }
            
            decimal costo = 0;
            
            foreach (NodoEstructura nodo in nodosHijos)
            {
                costo += nodo.GetCosto();
            }            

            return costo;
        }
    }
}
