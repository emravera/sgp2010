using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Provincia
    {
        private int codigo;
        private string nombre;

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
    }
}
