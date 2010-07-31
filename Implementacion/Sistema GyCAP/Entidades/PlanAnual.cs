using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class PlanAnual
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
        Entidades.DemandaAnual demanda;

        public Entidades.DemandaAnual Demanda
        {
            get { return demanda; }
            set { demanda = value; }
        }
        DateTime fechaCreacion;

        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }

    }
}
