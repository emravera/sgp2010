using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.ArbolEstructura
{
    public class ArbolEstructura
    {
        public ArbolEstructura() { codigosNodo = 0; }
        
        public ArbolEstructura(int codEstructura)
        {
            this.codigosNodo = 0;
            this.codigoEstructura = codEstructura;
            this.nodoRaiz = new NodoEstructura();
            this.nodoRaiz.NodosHijos = new List<NodoEstructura>();
        }

        private int codigosNodo;

        public int GetNextCodigoNodo()
        {
            this.codigosNodo++;
            return this.codigosNodo;
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
            this.codigosNodo = 0;
        }

        public decimal GetCostoEstructura()
        {
            return nodoRaiz.GetCosto();
        }        

        public NodoEstructura Find(int codigo)
        {
            if (this.nodoRaiz != null) { return BuscarNodo(codigo); }

            return null;
        }

        private NodoEstructura BuscarNodo(int codigo)
        {
            NodoEstructura nodo = null;

            List<NodoEstructura> abierta = new List<NodoEstructura>();
            abierta.Add(this.NodoRaiz);

            while (true)
            {
                if (abierta.Count == 0) { break; }
                nodo = abierta.First();
                abierta.RemoveAt(0);
                if (nodo.CodigoNodo == codigo) { break; }
                else
                {
                    abierta.AddRange(nodo.NodosHijos);
                }
            }

            return nodo;
        }
    }
}
