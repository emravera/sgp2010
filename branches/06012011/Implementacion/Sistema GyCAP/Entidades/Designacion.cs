using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Designacion
    {
        private int codigo;
        private String nombre;
        private String descripcion;
        private Marca marca;

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

        public Marca Marca
        {
            get { return marca; }
            set { marca = value; }
        }
    }
}
