using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.Entidades.ArbolOrdenesTrabajo
{
    public class ArbolProduccion
    {
        private OrdenProduccion ordenProduccion;
        private int codigoOrden = 0;

        public OrdenProduccion OrdenProduccion
        {
            get { return ordenProduccion; }
            set { ordenProduccion = value; }
        }
        private IList<NodoOrdenTrabajo> ordenesTrabajo;

        public IList<NodoOrdenTrabajo> OrdenesTrabajo
        {
            get { return ordenesTrabajo; }
            set { ordenesTrabajo = value; }
        }

        public int GetNextCodigoOrden()
        {
            codigoOrden--;
            return codigoOrden;
        }

        public IList<OrdenTrabajo> AsOrdenesTrabajoList()
        {
            IList<OrdenTrabajo> lista = new List<OrdenTrabajo>();

            foreach (NodoOrdenTrabajo nodo in ordenesTrabajo)
            {
                nodo.AsOrdenesTrabajoList(lista);
            }

            return lista;
        }

        public TreeView AsTreeView()
        {
            TreeView treeReturn = new TreeView();
            treeReturn.BeginUpdate();
            TreeNode nodoInicio = new TreeNode();
            nodoInicio.Text = this.ordenProduccion.Codigo;
            nodoInicio.Name = this.ordenProduccion.Numero.ToString();

            foreach (NodoOrdenTrabajo item in this.OrdenesTrabajo)
            {
                nodoInicio.Nodes.Add(item.AsTreeNode());
            }

            treeReturn.Nodes.Add(nodoInicio);
            treeReturn.EndUpdate();
            return treeReturn;
        }
    }
}
