using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class EstadoPlanMantenimiento
    {
        private int codigo;
        private string nombre;
        private string descripcion;

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

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

    }
}
