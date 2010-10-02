using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class Mantenimiento
    {
        private long codigo;
        private string nombre;
        private string descripcion;
        private TipoMantenimiento tipo;

        public long Codigo
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

        public TipoMantenimiento Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }


    }
}
