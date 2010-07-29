using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class PiezaEstructura
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
        private int codigoPieza;

        public int CodigoPieza
        {
            get { return codigoPieza; }
            set { codigoPieza = value; }
        }
        private int cantidadPieza;

        public int CantidadPieza
        {
            get { return cantidadPieza; }
            set { cantidadPieza = value; }
        }
        private int codigoGrupo;

        public int CodigoGrupo
        {
            get { return codigoGrupo; }
            set { codigoGrupo = value; }
        }
    }
}
