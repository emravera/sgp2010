using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class OrdenProduccion
    {
        private int numero;

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        private string codigo;

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private EstadoOrdenTrabajo estado;

        public EstadoOrdenTrabajo Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        private DateTime fechaAlta;

        public DateTime FechaAlta
        {
            get { return fechaAlta; }
            set { fechaAlta = value; }
        }
        private DetallePlanSemanal detallePlanSemanal;

        public DetallePlanSemanal DetallePlanSemanal
        {
            get { return detallePlanSemanal; }
            set { detallePlanSemanal = value; }
        }
        private string origen;

        public string Origen
        {
            get { return origen; }
            set { origen = value; }
        }
        private DateTime? fechaInicioEstimada;

        public DateTime? FechaInicioEstimada
        {
            get { return fechaInicioEstimada; }
            set { fechaInicioEstimada = value; }
        }
        private DateTime? fechaInicioReal;

        public DateTime? FechaInicioReal
        {
            get { return fechaInicioReal; }
            set { fechaInicioReal = value; }
        }
        private DateTime? fechaFinEstimada;

        public DateTime? FechaFinEstimada
        {
            get { return fechaFinEstimada; }
            set { fechaFinEstimada = value; }
        }
        private DateTime? fechaFinReal;

        public DateTime? FechaFinReal
        {
            get { return fechaFinReal; }
            set { fechaFinReal = value; }
        }
        private string observaciones;

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
        private int prioridad;

        public int Prioridad
        {
            get { return prioridad; }
            set { prioridad = value; }
        }
        private int estructura;

        public int Estructura
        {
            get { return estructura; }
            set { estructura = value; }
        }
        private int cantidadEstimada;

        public int CantidadEstimada
        {
            get { return cantidadEstimada; }
            set { cantidadEstimada = value; }
        }
        private int cantidadReal;

        public int CantidadReal
        {
            get { return cantidadReal; }
            set { cantidadReal = value; }
        }
        private Cocina cocina;

        public Cocina Cocina
        {
            get { return cocina; }
            set { cocina = value; }
        }
        private UbicacionStock ubicacionStock;

        public UbicacionStock UbicacionStock
        {
            get { return ubicacionStock; }
            set { ubicacionStock = value; }
        }
        private Lote lote;

        public Lote Lote
        {
            get { return lote; }
            set { lote = value; }
        }

        private BindingEntity.SortableBindingList<OrdenTrabajo> ordenesTrabajo;

        public BindingEntity.SortableBindingList<OrdenTrabajo> OrdenesTrabajo
        {
            get { return ordenesTrabajo; }
            set { ordenesTrabajo = value; }
        }
    }
}
