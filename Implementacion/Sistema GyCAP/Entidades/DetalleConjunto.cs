using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class DetalleConjunto
    {
        private int codigoDetalle;

        public int CodigoDetalle
        {
            get { return codigoDetalle; }
            set { codigoDetalle = value; }
        }
        private int codigoConjunto;

        public int CodigoConjunto
        {
            get { return codigoConjunto; }
            set { codigoConjunto = value; }
        }
        private int codigoSubconjunto;

        public int CodigoSubconjunto
        {
            get { return codigoSubconjunto; }
            set { codigoSubconjunto = value; }
        }
        private decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
    }
}
