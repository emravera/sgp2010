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

        public DateTime GetFechaFinalizacion(DateTime fechaInicio)
        {
            this.ordenProduccion.FechaInicioEstimada = fechaInicio;
            DateTime fechaReturn = fechaInicio;

            foreach (NodoOrdenTrabajo nodo in ordenesTrabajo)
            {
                DateTime fechaTemp = nodo.GetFechaFinalizacion(fechaInicio);
                if (fechaTemp > fechaReturn) { fechaReturn = fechaTemp; }
            }
            
            this.ordenProduccion.FechaFinEstimada = fechaReturn;
            return fechaReturn;
        }

        public DateTime GetFechaInicio(DateTime fechaFinalizacion)
        {
            this.ordenProduccion.FechaFinEstimada = fechaFinalizacion;
            DateTime fechaReturn = fechaFinalizacion;

            foreach (NodoOrdenTrabajo nodo in ordenesTrabajo)
            {
                DateTime fechaTemp = nodo.GetFechaInicio(fechaFinalizacion);
                if (fechaTemp < fechaReturn) { fechaReturn = fechaTemp; }
            }

            this.ordenProduccion.FechaInicioEstimada = fechaReturn;
            return fechaReturn;
        }
    }
}
