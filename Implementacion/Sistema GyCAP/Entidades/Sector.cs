using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Sector
    {
        private int codigo;
        private String nombre;
        private String descripcion;
        private String abreviatura;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public String Abreviatura
        {
            get { return abreviatura; }
            set { abreviatura = value; }
        }
    }
}
