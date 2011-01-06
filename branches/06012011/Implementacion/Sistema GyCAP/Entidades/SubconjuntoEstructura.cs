using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class SubconjuntoEstructura
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
        private int codigoSubconjunto;

        public int CodigoSubconjunto
        {
            get { return codigoSubconjunto; }
            set { codigoSubconjunto = value; }
        }
        private int cantidadSubconjunto;

        public int CantidadSubconjunto
        {
            get { return cantidadSubconjunto; }
            set { cantidadSubconjunto = value; }
        }
        private int codigoGrupo;

        public int CodigoGrupo
        {
            get { return codigoGrupo; }
            set { codigoGrupo = value; }
        }
    }
}
