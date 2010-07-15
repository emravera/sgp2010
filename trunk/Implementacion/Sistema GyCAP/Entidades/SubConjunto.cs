using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class SubConjunto
    {
        private int codigoSubconjunto;
        private string nombre;
        private int codigoTerminacion;
        private int cantidadStock;

        public int CodigoSubconjunto
        {
            get { return codigoSubconjunto; }
            set { codigoSubconjunto = value; }
        }
        
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        
        public int CodigoTerminacion
        {
            get { return codigoTerminacion; }
            set { codigoTerminacion = value; }
        }
        
        public int CantidadStock
        {
            get { return cantidadStock; }
            set { cantidadStock = value; }
        }
    }
}
