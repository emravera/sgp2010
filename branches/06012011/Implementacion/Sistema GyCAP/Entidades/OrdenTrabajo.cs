using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.Entidades
{
    public class OrdenTrabajo
    {
        private int numero;

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        public OrdenTrabajo() { }

        public OrdenTrabajo(int numeroOrden)
        {
            this.numero = numeroOrden;
        }
    }
}
