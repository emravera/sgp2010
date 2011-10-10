using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class OrigenMovimiento
    {
        private int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        private Entidad entidad;

        public Entidad Entidad
        {
            get { return entidad; }
            set { entidad = value; }
        }
        private decimal cantidadEstimada;

        public decimal CantidadEstimada
        {
            get { return cantidadEstimada; }
            set { cantidadEstimada = value; }
        }
        private decimal cantidadReal;

        public decimal CantidadReal
        {
            get { return cantidadReal; }
            set { cantidadReal = value; }
        }

        private int movimientoStock;

        public int MovimientoStock
        {
            get { return movimientoStock; }
            set { movimientoStock = value; }
        }

        private DateTime fechaPrevista;

        public DateTime FechaPrevista
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
    }
}
