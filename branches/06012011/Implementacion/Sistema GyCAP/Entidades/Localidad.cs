using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Localidad
    {
        private int codigo;
        private string nombre;
        private Provincia provincia;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public Provincia Provincia
        {
            get { return provincia; }
            set { provincia = value; }
        }
    }
}
