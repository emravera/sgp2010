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
        private const double tiempoFijo = 1;
        
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
            nodo.Text = string.Concat(this.ordenTrabajo.Codigo, " / ",this.OrdenTrabajo.Parte.Codigo);
            nodo.Name = this.ordenTrabajo.Numero.ToString();

            foreach (NodoOrdenTrabajo item in this.NodosHijos)
            {
                nodo.Nodes.Add(item.AsTreeNode());
            }

            return nodo;
        }

        public DateTime GetFechaFinalizacion(DateTime fechaInicio, bool forPedido)
        {
            while (!this.IsWorkableDay(fechaInicio)) { fechaInicio = fechaInicio.AddDays(Convert.ToDouble(1)); }            

            DateTime fechaMayor = fechaInicio;

            foreach (NodoOrdenTrabajo nodo in nodosHijos)
            {
                DateTime fechaTemp = nodo.GetFechaFinalizacion(fechaInicio, forPedido);
                if (fechaTemp > fechaMayor) { fechaMayor = fechaTemp; }
            }

            this.ordenTrabajo.FechaInicioEstimada = fechaMayor;

            if (forPedido)
            {
                this.ordenTrabajo.FechaFinEstimada = fechaMayor.AddMinutes(GetOperationTime() * this.ordenTrabajo.CantidadEstimada);
            }
            else
            {
                this.ordenTrabajo.FechaFinEstimada = fechaMayor.AddMinutes(tiempoFijo);
            }

            while(!this.IsWorkableDay(this.ordenTrabajo.FechaFinEstimada.Value)) { this.ordenTrabajo.FechaFinEstimada = this.ordenTrabajo.FechaFinEstimada.Value.AddDays(Convert.ToDouble(1)); }            

            return this.ordenTrabajo.FechaFinEstimada.Value;
        }

        private double GetOperationTime()
        {
            if (this.ordenTrabajo.DetalleHojaRuta.CentroTrabajo.Tipo.Codigo == (int)RecursosFabricacionEnum.TipoCentroTrabajo.Hombre
                || this.ordenTrabajo.DetalleHojaRuta.CentroTrabajo.Tipo.Codigo == (int)RecursosFabricacionEnum.TipoCentroTrabajo.Proveedor)
            {
                double temp = (double)(60 / (this.ordenTrabajo.DetalleHojaRuta.CentroTrabajo.CapacidadUnidadHora * this.ordenTrabajo.DetalleHojaRuta.CentroTrabajo.Eficiencia));
                return temp;
            }
            else
            {
                double temp = (double)((1 / (this.ordenTrabajo.DetalleHojaRuta.CentroTrabajo.CapacidadCiclo * this.ordenTrabajo.DetalleHojaRuta.CentroTrabajo.Eficiencia)) * this.ordenTrabajo.DetalleHojaRuta.CentroTrabajo.HorasCiclo);
                return temp;
            }
        }

        public DateTime GetFechaInicio(DateTime fechaFinalizacion, bool forPedido)
        {
            while (!this.IsWorkableDay(fechaFinalizacion)) { fechaFinalizacion = fechaFinalizacion.Subtract(new TimeSpan(24, 0, 0)); }
                        
            this.ordenTrabajo.FechaFinEstimada = fechaFinalizacion;
            double tiempo = 0;
            
            if (forPedido)
            {
                tiempo = GetOperationTime() * this.ordenTrabajo.CantidadEstimada;
            }
            else
            {
                tiempo = tiempoFijo;
            }

            this.ordenTrabajo.FechaInicioEstimada = fechaFinalizacion.Subtract(TimeSpan.FromMinutes(tiempo));

            while (!this.IsWorkableDay(this.ordenTrabajo.FechaInicioEstimada.Value)) { this.ordenTrabajo.FechaInicioEstimada = this.ordenTrabajo.FechaInicioEstimada.Value.Subtract(new TimeSpan(24, 0, 0)); }
                        
            DateTime fechaMenor = this.ordenTrabajo.FechaInicioEstimada.Value;

            foreach (NodoOrdenTrabajo nodo in nodosHijos)
            {
                DateTime fechaTemp = nodo.GetFechaInicio(this.ordenTrabajo.FechaInicioEstimada.Value, forPedido);
                if (fechaTemp < fechaMenor) { fechaMenor = fechaTemp; }
            }
            
            return fechaMenor;
        }

        private bool IsWorkableDay(DateTime fecha)
        {
            bool isWorkable = true;
            if (fecha.DayOfWeek.ToString().Equals(OrdenesTrabajoEnum.NonWorkingDays.Sábado.ToString(), StringComparison.InvariantCultureIgnoreCase)) { isWorkable = false; }
            if (fecha.DayOfWeek.ToString().Equals(OrdenesTrabajoEnum.NonWorkingDays.Domingo.ToString(), StringComparison.InvariantCultureIgnoreCase)) { isWorkable = false; }
            if (fecha.DayOfWeek.ToString().Equals(OrdenesTrabajoEnum.NonWorkingDays.Saturday.ToString(), StringComparison.InvariantCultureIgnoreCase)) { isWorkable = false; }
            if (fecha.DayOfWeek.ToString().Equals(OrdenesTrabajoEnum.NonWorkingDays.Sunday.ToString(), StringComparison.InvariantCultureIgnoreCase)) { isWorkable = false; }
            return isWorkable;
        }
    }
}
