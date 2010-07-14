using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Terminacion
    {
        private long codigo;
        private String nombre;
        private String descripcion;

        public long Codigo
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
    }
}
