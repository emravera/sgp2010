using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.Entidades.ArbolEstructura
{
    public class ArbolEstructura
    {        
        public ArbolEstructura(int codEstructura)
        {
            this.codigosNodo = 0;
            this.codigoEstructura = codEstructura;
            this.nodoRaiz = new NodoEstructura();
            this.nodoRaiz.NodosHijos = new List<NodoEstructura>();
            this.SelectedNodeCode = 0;
        }

        private int codigosNodo;
        private NodoEstructura nodoRaiz;
        private int codigoEstructura;
        private int SelectedNodeCode;

        public int GetNextCodigoNodo()
        {
            this.codigosNodo++;
            return this.codigosNodo;
        }        

        public NodoEstructura NodoRaiz
        {
            get { return nodoRaiz; }
            set { nodoRaiz = value; }
        }        

        public int CodigoEstructura
        {
            get { return codigoEstructura; }
            set { codigoEstructura = value; }
        }        

        public NodoEstructura GetSelectedNode()
        {
            return Find(this.SelectedNodeCode, null);
        }

        public void SetSelectedNode(int codigoNodo)
        {
            this.SelectedNodeCode = codigoNodo;
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

        public IList<MPEstructura> GetMPQuantityForStructure()
        {
            IList<MPEstructura> allMP = nodoRaiz.GetMPQuantityForPart();
            IList<MPEstructura> distinctMP = allMP.GroupBy(x => x.MateriaPrima.CodigoMateriaPrima).Select(x => x.First()).ToList();

            foreach (MPEstructura item in distinctMP)
            {
                item.Cantidad = allMP.Where(x => x.MateriaPrima.CodigoMateriaPrima == item.MateriaPrima.CodigoMateriaPrima).Sum(x => x.Cantidad);                
            }            

            return distinctMP;
        }

        public NodoEstructura Find(int? codigoNodo, int? codigoCompuesto)
        {
            
            if (this.nodoRaiz != null && codigoNodo != null) { return BuscarNodoByCodigoNodo(codigoNodo); }
            if (this.nodoRaiz != null && codigoCompuesto != null) { return BuscarNodoByCodigoCompuesto(codigoCompuesto); }

            return null;
        }

        private NodoEstructura BuscarNodoByCodigoNodo(int? codigoNodo)
        {
            NodoEstructura nodo = null;

            List<NodoEstructura> abierta = new List<NodoEstructura>();
            abierta.Add(this.NodoRaiz);

            while (true)
            {
                if (abierta.Count == 0) { break; }
                nodo = abierta.First();
                abierta.RemoveAt(0);
                if (nodo.CodigoNodo == codigoNodo) { break; }
                else
                {
                    abierta.AddRange(nodo.NodosHijos);
                }
            }

            return nodo;
        }

        private NodoEstructura BuscarNodoByCodigoCompuesto(int? codigoCompuesto)
        {
            NodoEstructura nodo = null;

            List<NodoEstructura> abierta = new List<NodoEstructura>();
            abierta.Add(this.NodoRaiz);

            while (true)
            {
                if (abierta.Count == 0) { break; }
                nodo = abierta.First();
                abierta.RemoveAt(0);
                if (nodo.Compuesto.Codigo == codigoCompuesto) { break; }
                else
                {
                    abierta.AddRange(nodo.NodosHijos);
                }
            }

            return nodo;
        }

        public TreeView AsNormalTreeView()
        {
            TreeView treeReturn = new TreeView();
            treeReturn.BeginUpdate();
            TreeNode nodoInicio = new TreeNode();
            nodoInicio.Text = nodoRaiz.Compuesto.Parte.Nombre;
            nodoInicio.Name = nodoRaiz.CodigoNodo.ToString();

            foreach (NodoEstructura item in nodoRaiz.NodosHijos)
            {
                nodoInicio.Nodes.Add(item.AsNormalTreeNode());
            }

            treeReturn.Nodes.Add(nodoInicio);
            treeReturn.EndUpdate();
            return treeReturn;
        }

        public TreeView AsExtendedTreeView()
        {
            TreeView treeReturn = new TreeView();
            treeReturn.BeginUpdate();
            TreeNode nodoInicio = new TreeNode();
            nodoInicio.Text = nodoRaiz.Compuesto.Parte.Nombre;
            nodoInicio.Name = nodoRaiz.CodigoNodo.ToString();
            nodoInicio.Tag = new string[] { nodoRaiz.Compuesto.Cantidad.ToString(), nodoRaiz.Compuesto.UnidadMedida.Abreviatura };

            foreach (NodoEstructura item in nodoRaiz.NodosHijos)
            {
                nodoInicio.Nodes.Add(item.AsExtendedTreeNode());
            }

            treeReturn.Nodes.Add(nodoInicio);
            treeReturn.EndUpdate();
            return treeReturn;
        }

        public IList<NodoEstructura> AsList(bool IncluirRaiz)
        {
            IList<NodoEstructura> lista = new List<NodoEstructura>();
            if (IncluirRaiz) { lista.Add(nodoRaiz); }
            return lista;
        }

        public IList<string> AddRaiz(NodoEstructura nodo)
        {
            IList<string> validaciones = new List<string>();

            if (this.nodoRaiz.Compuesto != null) { validaciones.Add("La estructura ya contiene el producto raíz."); }
            if (nodo.Contenido != NodoEstructura.tipoContenido.ProductoFinal) { validaciones.Add("Unicamente un producto terminado puede ser raíz"); }

            this.nodoRaiz = nodo;

            return validaciones;
        }

        public IList<string> AddNodo(NodoEstructura nodoHijo, int? codigoCompuestoPadre)
        {
            if (codigoCompuestoPadre == null) { return AddRaiz(nodoHijo); }

            NodoEstructura finded = this.BuscarNodoByCodigoCompuesto(codigoCompuestoPadre);

            if (finded != null)
            {
                return finded.AddChild(nodoHijo, false, false);
            }

            IList<string> validaciones = new List<string>();
            validaciones.Add("El nodo padre especificado no existe en la estructura.");
            return validaciones;
        }

        public void DeleteNodo(int? codigoNodo, int? codigoCompuesto)
        {
            NodoEstructura nodo = Find(codigoNodo, codigoCompuesto);
            nodo.NodoPadre.NodosHijos.Remove(nodo);            
        }
    }
}
