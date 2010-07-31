using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class DetallePlanAnual
    {
        int codigo, cantidadMes;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public int CantidadMes
        {
            get { return cantidadMes; }
            set { cantidadMes = value; }
        }
        string mes;

        public string Mes
        {
            get { return mes; }
            set { mes = value; }
        }
        Entidades.PlanAnual planAnual;

        public Entidades.PlanAnual PlanAnual
        {
            get { return planAnual; }
            set { planAnual = value; }
        }


    }
}
