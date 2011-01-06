using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class DetallePlanMateriasPrimas
    {
        int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        PlanMateriaPrima plan;

        public PlanMateriaPrima Plan
        {
            get { return plan; }
            set { plan = value; }
        }
        MateriaPrima materiaPrima;

        public MateriaPrima MateriaPrima
        {
            get { return materiaPrima; }
            set { materiaPrima = value; }
        }


    }
}
