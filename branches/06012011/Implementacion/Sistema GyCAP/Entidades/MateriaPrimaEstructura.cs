using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class MateriaPrimaEstructura
    {
        private int codigoDetalle;

        public int CodigoDetalle
        {
            get { return codigoDetalle; }
            set { codigoDetalle = value; }
        }
        private int codigoEstructura;

        public int CodigoEstructura
        {
            get { return codigoEstructura; }
            set { codigoEstructura = value; }
        }
        private int codigoMateriaPrima;

        public int CodigoMateriaPrima
        {
            get { return codigoMateriaPrima; }
            set { codigoMateriaPrima = value; }
        }
        private decimal cantidadMateriaPrima;

        public decimal CantidadMateriaPrima
        {
            get { return cantidadMateriaPrima; }
            set { cantidadMateriaPrima = value; }
        }
        private int codigoGrupo;

        public int CodigoGrupo
        {
            get { return codigoGrupo; }
            set { codigoGrupo = value; }
        }
    }
}
