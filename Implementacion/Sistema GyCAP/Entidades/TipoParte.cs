using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class TipoParte
    {
        int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        int productoTerminado;

        public int ProductoTerminado
        {
            get { return productoTerminado; }
            set { productoTerminado = value; }
        }
    }
}
