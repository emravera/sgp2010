using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.ArbolEstructura
{
    public class ArbolEstructura
    {
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
