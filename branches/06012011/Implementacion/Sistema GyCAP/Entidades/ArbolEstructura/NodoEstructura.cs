using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            if (this.contenido == tipoContenido.MateriaPrima) { return this.compuesto.MateriaPrima.Costo * this.compuesto.Cantidad; }
            
            decimal costo = 0;
            
            foreach (NodoEstructura nodo in nodosHijos)
            {
                costo += nodo.GetCosto() * this.compuesto.Cantidad;
            }            

            return costo;
        }

        public void SumarCantidad(decimal cantidad)
        {
            if (this.compuesto != null) { this.compuesto.Cantidad += cantidad; }
        }

        public void RestarCantidad(decimal cantidad)
        {
            if (this.compuesto != null) { this.compuesto.Cantidad -= cantidad; }
        }

        public bool AddChild(NodoEstructura nodo, bool allowDuplicated, bool AddQuantityIfExists)
        {
            NodoEstructura finded = this.FindInChilds(nodo.codigoNodo);
            
            if (finded != null && !allowDuplicated) 
            {
                if (!AddQuantityIfExists) { return false; }

                finded.compuesto.Cantidad += nodo.compuesto.Cantidad;

                return true;
            }

            this.nodosHijos.Add(nodo);

            return true;
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

        public TreeNode AsTreeNode()
        {
            TreeNode nodo = new TreeNode();
            nodo.Text = (compuesto.ParteHijo != null) ? compuesto.ParteHijo.Nombre : compuesto.MateriaPrima.Nombre;
            nodo.Name = this.codigoNodo.ToString();
            nodo.Tag = new string[] { compuesto.Cantidad.ToString(), compuesto.UnidadMedida.Abreviatura };

            foreach (NodoEstructura item in this.nodosHijos)
            {
                nodo.Nodes.Add(item.AsTreeNode());
            }

            return nodo;
        }

        private IList<string> ValidateAddChild(NodoEstructura nodo)
        {
            IList<string> validaciones = new List<string>();

            //if (nodo.nodoPadre.compuesto.PartePadre != null && this.Equals(nodo)) { validaciones.Add(""); }

            if (this.contenido == tipoContenido.MateriaPrima) { validaciones.Add(""); }

            //agrego la parte
                    //Condiciones:
                    //              - que el padre no sea ella misma
                    //              - que el padre no sea una MP
                    //              - si ya está dentro del mismo padre, avisar y preguntar si quiere aumentar la cantidad ingresada
                    //              - si ya está en otro padre, que no arme una estructura diferente para la misma parte
                    //              - que ella misma no sea ancestro
                    //              - solo una parte del tipo producto terminado puede ser raíz
                    //              - un producto terminado no puede ser hijo de nadie
                    
                
            
                
                    //agrego la MP
                    //Condiciones:
                    //              - que no sea raíz
                    //              - que no sea hija de otra MP
                    //              - si ya está en el mismo padre, avisar y preguntar si quiere aumentar la cantidad ingresada

            return validaciones;
        }
    }
}
