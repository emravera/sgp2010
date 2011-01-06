using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class DetallePlanMantenimiento
    {
        private long codigo;
        private PlanMantenimiento plan;
        private EstadoDetalleMantenimiento estado;
        private Mantenimiento mantenimiento;
        private UnidadMedida unidadMedida;
        private string frecuencia;
        private string descripcion;

        public long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public PlanMantenimiento Plan
        {
            get { return plan; }
            set { plan = value; }
        }

        public EstadoDetalleMantenimiento Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public Mantenimiento Mantenimiento
        {
            get { return mantenimiento; }
            set { mantenimiento = value; }
        }

        public UnidadMedida UnidadMedida
        {
            get { return unidadMedida; }
            set { unidadMedida = value; }
        }

        public string Frecuencia
        {
            get { return frecuencia; }
            set { frecuencia = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

    }
}
