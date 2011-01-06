using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class DetallePieza
    {
        private int codigoDetalle;

        public int CodigoDetalle
        {
            get { return codigoDetalle; }
            set { codigoDetalle = value; }
        }
        private int codigoPieza;

        public int CodigoPieza
        {
            get { return codigoPieza; }
            set { codigoPieza = value; }
        }
        private int codigoMateriaPrima;

        public int CodigoMateriaPrima
        {
            get { return codigoMateriaPrima; }
            set { codigoMateriaPrima = value; }
        }
        private decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
    }
}
