using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class DetalleSubconjunto
    {
        private int codigoDetalle;

        public int CodigoDetalle
        {
            get { return codigoDetalle; }
            set { codigoDetalle = value; }
        }
        private int codigoSubconjunto;

        public int CodigoSubconjunto
        {
            get { return codigoSubconjunto; }
            set { codigoSubconjunto = value; }
        }
        private int codigoPieza;

        public int CodigoPieza
        {
            get { return codigoPieza; }
            set { codigoPieza = value; }
        }
        private decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
    }
}
