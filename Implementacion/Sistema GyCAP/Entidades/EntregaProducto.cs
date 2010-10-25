using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class EntregaProducto
    {
        int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        Entidades.Cliente cliente;

        public Entidades.Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
        Entidades.Empleado empleado;

        public Entidades.Empleado Empleado
        {
            get { return empleado; }
            set { empleado = value; }
        }


    }
}
