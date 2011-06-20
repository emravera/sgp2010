using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.ArbolEstructura
{
    public class ArbolEstructura
    {
        public ArbolEstructura() { }
        
        public ArbolEstructura(int codEstructura)
        {
            this.codigoEstructura = codEstructura;
            this.nodoRaiz = new NodoEstructura();
            this.nodoRaiz.NodosHijos = new List<NodoEstructura>();
        }
        
        private NodoEstructura nodoRaiz;

        public NodoEstructura NodoRaiz
        {
            get { return nodoRaiz; }
            set { nodoRaiz = value; }
        }
        private int codigoEstructura;

        public int CodigoEstructura
        {
            get { return codigoEstructura; }
            set { codigoEstructura = value; }
        }

        public void ClearAll()
        {
            if (nodoRaiz != null && nodoRaiz.NodosHijos != null) { nodoRaiz.NodosHijos.Clear(); }
        }

        public decimal GetCostoEstructura()
        {
            return nodoRaiz.GetCosto();
        }

        public NodoEstructura FindNodo(int codigoNodo, bool searchChilds)
        {
            foreach (NodoEstructura nodo in nodoRaiz.NodosHijos)
            {
                if (nodo.CodigoNodo == codigoNodo) { return nodo; }
                if (searchChilds) { return FindInChilds(nodo, codigoNodo); }
            }

            return null;
        }

        private NodoEstructura FindInChilds(NodoEstructura node, int codigoNodo)
        {
            foreach (NodoEstructura nodo in node.NodosHijos)
            {
                if (nodo.CodigoNodo == codigoNodo) { return nodo; }
                return FindInChilds(nodo, codigoNodo);
            }

            return null;
        }
    }
}
