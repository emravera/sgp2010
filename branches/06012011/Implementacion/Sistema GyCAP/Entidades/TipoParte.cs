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

        int fantasma;

        public int Fantasma
        {
            get { return fantasma; }
            set { fantasma = value; }
        }

        int ordentrabajo;

        public int Ordentrabajo
        {
            get { return ordentrabajo; }
            set { ordentrabajo = value; }
        }

        int ensamblado;

        public int Ensamblado
        {
            get { return ensamblado; }
            set { ensamblado = value; }
        }

        int adquirido;

        public int Adquirido
        {
            get { return adquirido; }
            set { adquirido = value; }
        }
    }
}
