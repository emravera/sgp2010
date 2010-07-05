using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class UnidadMedida
    {
        private int codigo;
        private String nombre;
        private TipoUnidadMedida tipo;
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

        public TipoUnidadMedida Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public String Abreviatura
        {
            get { return abreviatura; }
            set { abreviatura = value; }
        }
    }
}
