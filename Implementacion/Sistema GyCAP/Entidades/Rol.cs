using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Rol
    {
        private int codigo;
        private String nombre;
        private String descripcion;
        private String permiso;

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

        public String Permiso
        {
            get { return permiso; }
            set { permiso = value; }
        }


    }
}
