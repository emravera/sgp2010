using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GyCAP.Entidades.Enumeraciones;

namespace GyCAP.Entidades.ArbolOrdenesTrabajo
{
    public class NodoOrdenTrabajo
    {
        private int codigoNodo;

        public int CodigoNodo
        {
            get { return codigoNodo; }
            set { codigoNodo = value; }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        private OrdenTrabajo ordenTrabajo;

        public OrdenTrabajo OrdenTrabajo
        {
            get { return ordenTrabajo; }
            set { ordenTrabajo = value; }
        }

        private NodoOrdenTrabajo nodoPadre;

        public NodoOrdenTrabajo NodoPadre
        {
            get { return nodoPadre; }
            set { nodoPadre = value; }
        }

        private IList<NodoOrdenTrabajo> nodosHijos;

        public IList<NodoOrdenTrabajo> NodosHijos
        {
            get { return nodosHijos; }
            set { nodosHijos = value; }
        }

        public void AsOrdenesTrabajoList(IList<OrdenTrabajo> lista)
        {
            lista.Add(this.ordenTrabajo);

            foreach (NodoOrdenTrabajo nodo in this.NodosHijos)
            {
                nodo.AsOrdenesTrabajoList(lista);
            }
        }

        public void AsNodoOrdenTrabajoList(IList<NodoOrdenTrabajo> lista)
        {
            lista.Add(this);

            foreach (NodoOrdenTrabajo nodo in this.NodosHijos)
            {
                nodo.AsNodoOrdenTrabajoList(lista);
            }
        }

        public TreeNode AsTreeNode()
        {
            TreeNode nodo = new TreeNode();
            nodo.Text = this.ordenTrabajo.Origen + ((this.NodoPadre != null) ? " - " + this.NodoPadre.ordenTrabajo.Origen : string.Empty);
            nodo.Name = this.ordenTrabajo.Numero.ToString();

            foreach (NodoOrdenTrabajo item in this.NodosHijos)
            {
                nodo.Nodes.Add(item.AsTreeNode());
            }

            return nodo;
        }

        public DateTime GetFechaFinalizacion(DateTime fechaInicio)
        {
            DateTime fechaMayor = fechaInicio;
            foreach (NodoOrdenTrabajo nodo in nodosHijos)
            {
                DateTime fechaTemp = nodo.GetFechaFinalizacion(fechaInicio);
                if (fechaTemp > fechaMayor) { fechaMayor = fechaTemp; }
            }

            this.ordenTrabajo.FechaInicioEstimada = fechaMayor;
            this.ordenTrabajo.FechaFinEstimada = fechaMayor.AddMinutes(GetOperationTime() * this.ordenTrabajo.CantidadEstimada);
            return this.ordenTrabajo.FechaFinEstimada.Value;
        }

        private double GetOperationTime()
        {
            if (this.ordenTrabajo.DetalleHojaRuta.CentroTrabajo.Tipo == (int)RecursosFabricacionEnum.TipoCentroTrabajo.TipoHombre)
            {
                double temp = (double)(60 / (this.ordenTrabajo.DetalleHojaRuta.CentroTrabajo.CapacidadUnidadHora * this.ordenTrabajo.DetalleHojaRuta.CentroTrabajo.Eficiencia));
                return temp;
            }
            else
            {
                double temp = (double)(1 / (this.ordenTrabajo.DetalleHojaRuta.CentroTrabajo.CapacidadCiclo * this.ordenTrabajo.DetalleHojaRuta.CentroTrabajo.Eficiencia)) * 60;
                return temp;
            }
        }

        public DateTime GetFechaInicio(DateTime fechaFinalizacion)
        {
            this.ordenTrabajo.FechaFinEstimada = fechaFinalizacion;
            double tiempo = GetOperationTime() * this.ordenTrabajo.CantidadEstimada;
            this.ordenTrabajo.FechaInicioEstimada = fechaFinalizacion.Subtract(TimeSpan.FromMinutes(tiempo));
            DateTime fechaMenor = this.ordenTrabajo.FechaInicioEstimada.Value;

            foreach (NodoOrdenTrabajo nodo in nodosHijos)
            {
                DateTime fechaTemp = nodo.GetFechaInicio(this.ordenTrabajo.FechaInicioEstimada.Value);
                if (fechaTemp < fechaMenor) { fechaMenor = fechaTemp; }
            }

            return fechaMenor;
        }
    }
}
