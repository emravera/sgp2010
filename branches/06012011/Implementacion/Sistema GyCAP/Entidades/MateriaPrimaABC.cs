using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class MateriaPrimaABC
    {
        int codigoMP;

        public int CodigoMP
        {
            get { return codigoMP; }
            set { codigoMP = value; }
        }
        decimal cantidadMP;

        public decimal CantidadMP
        {
            get { return cantidadMP; }
            set { cantidadMP = value; }
        }
        decimal inversion;

        public decimal Inversion
        {
            get { return inversion; }
            set { inversion = value; }
        }

        decimal precioMP;

        public decimal PrecioMP
        {
            get { return precioMP; }
            set { precioMP = value; }
        }

        string claseMP;

        public string ClaseMP
        {
            get { return claseMP; }
            set { claseMP = value; }
        }

        decimal porcentajeMP;

        public decimal PorcentajeMP
        {
            get { return porcentajeMP; }
            set { porcentajeMP = value; }
        }



    }
}
