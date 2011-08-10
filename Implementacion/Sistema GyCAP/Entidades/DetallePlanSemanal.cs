using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class DetallePlanSemanal
    {
        int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
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
        Entidades.DiasPlanSemanal diaPlanSemanal;

        public Entidades.DiasPlanSemanal DiaPlanSemanal
        {
            get { return diaPlanSemanal; }
            set { diaPlanSemanal = value; }
        }
        int estado;

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        Entidades.DetallePedido detallePedido;

        public Entidades.DetallePedido DetallePedido
        {
            get { return detallePedido; }
            set { detallePedido = value; }
        }
    }
}
