using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class SubconjuntosxConjunto
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
        private int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
    }

    public class PiezasxConjunto
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
        private int codigoPieza;

        public int CodigoPieza
        {
            get { return codigoPieza; }
            set { codigoPieza = value; }
        }
        private int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
    }
    
    public class DetalleConjunto
    {
        private int codigoDetalle;
        private int codigoConjunto;
        private int codigoSubconjunto;
        private int codigoPieza;
        private int cantidad;

        public int CodigoDetalle
        {
            get { return codigoDetalle; }
            set { codigoDetalle = value; }
        }
        
        public int CodigoConjunto
        {
            get { return codigoConjunto; }
            set { codigoConjunto = value; }
        }
        
        public int CodigoSubconjunto
        {
            get { return codigoSubconjunto; }
            set { codigoSubconjunto = value; }
        }

        public int CodigoPieza
        {
            get { return codigoPieza; }
            set { codigoPieza = value; }
        }
        
        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
    }
}
