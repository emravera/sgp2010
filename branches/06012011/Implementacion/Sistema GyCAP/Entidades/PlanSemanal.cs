
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class PlanSemanal
    {
        int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        Entidades.PlanMensual planMensual;

        public Entidades.PlanMensual PlanMensual
        {
            get { return planMensual; }
            set { planMensual = value; }
        }
        int semana;

        public int Semana
        {
            get { return semana; }
            set { semana = value; }
        }
        DateTime fechaCreacion;

        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }
        
    }
}
