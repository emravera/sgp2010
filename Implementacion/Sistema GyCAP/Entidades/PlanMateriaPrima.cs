using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class PlanMateriaPrima
    {
        int codigo, anio;

        public int Anio
        {
            get { return anio; }
            set { anio = value; }
        }

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        DateTime fechaCreacion;

        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }
        string mes;

        public string Mes
        {
            get { return mes; }
            set { mes = value; }
        }


    }
}
