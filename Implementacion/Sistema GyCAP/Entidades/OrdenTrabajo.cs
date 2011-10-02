using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.Entidades
{
    public class OrdenTrabajo
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
        private int ordenProduccion;

        public int OrdenProduccion
        {
            get { return ordenProduccion; }
            set { ordenProduccion = value; }
        }
        private EstadoOrdenTrabajo estado;

        public EstadoOrdenTrabajo Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        private Parte parte;

        public Parte Parte
        {
            get { return parte; }
            set { parte = value; }
        }
        private string origen;

        public string Origen
        {
            get { return origen; }
            set { origen = value; }
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
        private int secuencia;

        public int Secuencia
        {
            get { return secuencia; }
            set { secuencia = value; }
        }
        private DetalleHojaRuta detalleHojaRuta;

        public DetalleHojaRuta DetalleHojaRuta
        {
            get { return detalleHojaRuta; }
            set { detalleHojaRuta = value; }
        }

        private int? ordenTrabajoPadre;

        public int? OrdenTrabajoPadre
        {
            get { return ordenTrabajoPadre; }
            set { ordenTrabajoPadre = value; }
        }

        private BindingEntity.SortableBindingList<CierreParcialOrdenTrabajo> cierresParciales;

        public BindingEntity.SortableBindingList<CierreParcialOrdenTrabajo> CierresParciales
        {
            get { return cierresParciales; }
            set { cierresParciales = value; }
        }

        private int tipo;

        public int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
    }
}
