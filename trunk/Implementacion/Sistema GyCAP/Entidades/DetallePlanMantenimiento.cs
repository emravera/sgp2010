using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class DetallePlanMantenimiento
    {
        private long codigo;
        private Maquina maquina;
        private PlanMantenimiento plan;
        private EstadoDetalleMantenimiento estado;
        private CausaFallo causaFallo;
        private Empleado empleado;
        private Repuesto repuesto;
        private Mantenimiento mantenimiento;
        private DateTime fechaRealizacionPrevista;
        private DateTime fechaRealizacionReal;
        private string observaciones;


        public long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        public Maquina Maquina
        {
            get { return maquina; }
            set { maquina = value; }
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

        public CausaFallo CausaFallo
        {
            get { return causaFallo; }
            set { causaFallo = value; }
        }

        public Empleado Empleado
        {
            get { return empleado; }
            set { empleado = value; }
        }

        public Repuesto Repuesto
        {
            get { return repuesto; }
            set { repuesto = value; }
        }

        public Mantenimiento Mantenimiento
        {
            get { return mantenimiento; }
            set { mantenimiento = value; }
        }

        public DateTime FechaRealizacionPrevista
        {
            get { return fechaRealizacionPrevista; }
            set { fechaRealizacionPrevista = value; }
        }

        public DateTime FechaRealizacionReal
        {
            get { return fechaRealizacionReal; }
            set { fechaRealizacionReal = value; }
        }

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }


    }
}
