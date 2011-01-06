using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class DiasPlanSemanal
    {
        int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        string dia;

        public string Dia
        {
            get { return dia; }
            set { dia = value; }
        }
        DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        Entidades.PlanSemanal planSemanal;

        public Entidades.PlanSemanal PlanSemanal
        {
            get { return planSemanal; }
            set { planSemanal = value; }
        }

    }
}
