using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class MovimientoStock
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
        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        private DateTime fechaAlta;

        public DateTime FechaAlta
        {
            get { return fechaAlta; }
            set { fechaAlta = value; }
        }
        private DateTime? fechaPrevista;

        public DateTime? FechaPrevista
        {
            get { return fechaPrevista; }
            set { fechaPrevista = value; }
        }
        private DateTime? fechaReal;

        public DateTime? FechaReal
        {
            get { return fechaReal; }
            set { fechaReal = value; }
        }
        private Entidad origen;

        public Entidad Origen
        {
            get { return origen; }
            set { origen = value; }
        }
        private Entidad destino;

        public Entidad Destino
        {
            get { return destino; }
            set { destino = value; }
        }
        private decimal cantidadOrigenEstimada;

        public decimal CantidadOrigenEstimada
        {
            get { return cantidadOrigenEstimada; }
            set { cantidadOrigenEstimada = value; }
        }
        private decimal cantidadDestinoEstimada;

        public decimal CantidadDestinoEstimada
        {
            get { return cantidadDestinoEstimada; }
            set { cantidadDestinoEstimada = value; }
        }

        private decimal cantidadOrigenReal;

        public decimal CantidadOrigenReal
        {
            get { return cantidadOrigenReal; }
            set { cantidadOrigenReal = value; }
        }
        private decimal cantidadDestinoReal;

        public decimal CantidadDestinoReal
        {
            get { return cantidadDestinoReal; }
            set { cantidadDestinoReal = value; }
        }

        private EstadoMovimientoStock estado;

        public EstadoMovimientoStock Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private Entidad duenio;

        public Entidad Duenio
        {
            get { return duenio; }
            set { duenio = value; }
        }
    }
}
