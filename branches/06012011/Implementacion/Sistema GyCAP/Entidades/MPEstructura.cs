using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class MPEstructura
    {
        Entidades.MateriaPrima materiaPrima;

        public Entidades.MateriaPrima MateriaPrima
        {
            get { return materiaPrima; }
            set { materiaPrima = value; }
        }
        decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
    }
}
