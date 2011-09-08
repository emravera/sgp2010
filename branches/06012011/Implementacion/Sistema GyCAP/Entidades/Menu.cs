using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Menu
    {
        private int codigo;
        private String nombre;
        private String padre;
        private String descripcion;
        private String formulario;
        private String tag;
        private String orden;

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

        public String Padre
        {
            get { return padre; }
            set { padre = value; }
        }

        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public String Formulario
        {
            get { return formulario; }
            set { formulario = value; }
        }

        public String Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public String Orden
        {
            get { return orden; }
            set { orden = value; }
        }

    }
}
