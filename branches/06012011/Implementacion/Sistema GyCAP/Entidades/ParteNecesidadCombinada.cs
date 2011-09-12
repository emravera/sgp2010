using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class ParteNecesidadCombinada
    {
        private Parte parte;

        public Parte Parte
        {
            get { return parte; }
            set { parte = value; }
        }
        private decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
    }
}
