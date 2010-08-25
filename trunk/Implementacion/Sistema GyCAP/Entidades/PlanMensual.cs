using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class PlanMensual
    {
        int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        PlanAnual planAnual;

        public PlanAnual PlanAnual
        {
            get { return planAnual; }
            set { planAnual = value; }
        }

        string mes;

        public string Mes
        {
            get { return mes; }
            set { mes = value; }
        }

        DateTime fechaCreacion;

        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }


    }
}
