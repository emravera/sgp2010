using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class TipoUnidadMedida
    {
        private int codigo;
        private String nombre;

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

    }
}
