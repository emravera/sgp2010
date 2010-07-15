using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Conjunto
    {
        private int codigoConjunto;
        private int cantidadStock;
        private int codigoTerminacion;
        private string nombre;

        public int CodigoConjunto
        {
            get { return codigoConjunto; }
            set { codigoConjunto = value; }
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
