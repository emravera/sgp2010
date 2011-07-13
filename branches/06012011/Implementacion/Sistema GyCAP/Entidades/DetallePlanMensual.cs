using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class DetallePlanMensual
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
        Entidades.Cocina cocina;

        public Entidades.Cocina Cocina
        {
            get { return cocina; }
            set { cocina = value; }
        }
        int cantidadEstimada;

        public int CantidadEstimada
        {
            get { return cantidadEstimada; }
            set { cantidadEstimada = value; }
        }
        int cantidadReal;

        public int CantidadReal
        {
            get { return cantidadReal; }
            set { cantidadReal = value; }
        }
        int detallePedido;

        public int DetallePedido
        {
            get { return detallePedido; }
            set { detallePedido = value; }
        }        
    }
}
