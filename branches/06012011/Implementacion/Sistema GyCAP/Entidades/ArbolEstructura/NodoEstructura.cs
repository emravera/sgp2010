using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.Entidades.ArbolEstructura
{
    public class NodoEstructura
    {
        public enum tipoContenido { Parte, MateriaPrima, ProductoFinal, Todos };
        
        private int codigoNodo;
        private string text;
        private List<NodoEstructura> nodosHijos;
        private NodoEstructura nodoPadre;
        private tipoContenido contenido;
        private object tag;
        private CompuestoParte compuesto;
        private decimal fixedCost;

        public NodoEstructura() 
        {
            this.nodosHijos = new List<NodoEstructura>();
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
            if (this.contenido == tipoContenido.MateriaPrima) 
            {
                this.fixedCost = this.compuesto.MateriaPrima.Costo * this.compuesto.Cantidad;
                return this.fixedCost;
            }
            if (this.contenido == tipoContenido.Parte && this.compuesto.Parte.Tipo.Adquirido == 1) 
            {
                this.fixedCost = this.compuesto.Parte.Costo * this.compuesto.Cantidad;
                return this.fixedCost;
            }

            decimal costo = 0;
            
            foreach (NodoEstructura nodo in nodosHijos)
            {
                costo += nodo.GetCosto() * this.compuesto.Cantidad;
            }

            this.fixedCost = costo;
            return this.fixedCost;
        }

        public IList<MPEstructura> GetMPQuantityForPart()
        {
            IList<MPEstructura> listaMP = new List<MPEstructura>();

            if (this.contenido == tipoContenido.MateriaPrima)
            {
                listaMP.Add(new MPEstructura() { MateriaPrima = this.compuesto.MateriaPrima, Cantidad = this.compuesto.Cantidad });
                return listaMP;
            }
            else
            {
                foreach (NodoEstructura hijo in this.nodosHijos)
                {
                     IList<MPEstructura> auxiliar = hijo.GetMPQuantityForPart();

                     foreach (MPEstructura item in auxiliar)
                     {
                         item.Cantidad *= this.compuesto.Cantidad;
                         listaMP.Add(item);
                     }
                }
            }

            return listaMP;
        }

        public void SumarCantidad(decimal cantidad)
        {
            if (this.compuesto != null) { this.compuesto.Cantidad += cantidad; }
        }

        public void RestarCantidad(decimal cantidad)
        {
            if (this.compuesto != null) { this.compuesto.Cantidad -= cantidad; }
        }

        public IList<string> AddChild(NodoEstructura nodo, bool allowDuplicated, bool AddQuantityIfExists)
        {
            NodoEstructura finded = this.FindInChilds(nodo.codigoNodo);

            if (finded != null && !allowDuplicated && AddQuantityIfExists)
            {
                finded.compuesto.Cantidad += nodo.compuesto.Cantidad;
                List<string> validaciones = new List<string>();
                validaciones.Add("Ya existe un nodo con los valores especificados.");
                return validaciones;
            }

            nodo.NodoPadre = this;
            this.nodosHijos.Add(nodo);

            return new List<string>();
        }

        public NodoEstructura FindInChilds(int codigo)
        {
            NodoEstructura nodo = null;
            
            if (this.nodosHijos.Count > 0)
            {
                nodo = this.nodosHijos.Find(p => p.codigoNodo == codigo);
            }

            return nodo;
        }

        public TreeNode AsNormalTreeNode()
        {
            TreeNode nodo = new TreeNode();
            nodo.Text = (compuesto.Parte != null) ? compuesto.Parte.Nombre : compuesto.MateriaPrima.Nombre;
            nodo.Name = this.codigoNodo.ToString();

            foreach (NodoEstructura item in this.nodosHijos)
            {
                nodo.Nodes.Add(item.AsNormalTreeNode());
            }

            return nodo;
        }

        public TreeNode AsExtendedTreeNode()
        {
            TreeNode nodo = new TreeNode();
            nodo.Text = (compuesto.Parte != null) ? compuesto.Parte.Nombre : compuesto.MateriaPrima.Nombre;
            nodo.Name = this.codigoNodo.ToString();
            nodo.Tag = new string[] { compuesto.Cantidad.ToString(), compuesto.UnidadMedida.Abreviatura };

            foreach (NodoEstructura item in this.nodosHijos)
            {
                nodo.Nodes.Add(item.AsExtendedTreeNode());
            }

            return nodo;
        }

        public IList<NodoEstructura> AsList(NodoEstructura.tipoContenido tipoContenido)
        {
            IList<NodoEstructura> lista = new List<NodoEstructura>();
            if (this.contenido == tipoContenido || tipoContenido == tipoContenido.Todos) { lista.Add(this); }

            foreach (NodoEstructura nodoHijo in this.nodosHijos)
            {
                lista = lista.Concat(nodoHijo.AsList(tipoContenido)).ToList();
            }            

            return lista;
        }

        private IList<string> ValidateAddChild(NodoEstructura nodo)
        {
            IList<string> validaciones = new List<string>();

            if (this.Equals(nodo)) { validaciones.Add("Una parte no puede ser padre e hijo al mismo tiempo."); }
            
            if (this.contenido == tipoContenido.MateriaPrima) { validaciones.Add("Una parte no puede ser hijo de una materia prima."); }
            NodoEstructura nodoTemp = nodo.nodoPadre;
            while (nodoTemp != null)
            {
                if (nodoTemp.nodoPadre.Equals(nodo)) { validaciones.Add("Una parte no puede ser padre e hijo al mismo tiempo."); }
            }
            if (nodo.Contenido == tipoContenido.ProductoFinal) { validaciones.Add("Un producto terminado no puede ser hijo."); }
            if (this.Contenido == tipoContenido.MateriaPrima) { validaciones.Add("Una materia prima no puede ser hijo de una materia prima."); }
            
            return validaciones;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) { return false; }
            if (obj.GetType() != typeof(NodoEstructura)) { return false; }
            NodoEstructura nodo = (NodoEstructura)obj;

            if (nodo.contenido != this.contenido) { return false; }
            if (nodo.compuesto.Codigo != this.compuesto.Codigo) { return false; }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        
    }
}
