using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class RegistrosMantenimientos
    {
        private long codigo;
        private TipoMantenimiento tipo;
        private Mantenimiento mantenimiento;
        private Maquina maquina;
        private DetallePlanMantenimiento detallePlanMantenimiento;
        private Empleado empleado;
        private DateTime fecha;
        private DateTime fechaRealizacion;
        private CausaFallo causaFallo;
        private string observacion;
        private Averia averia;

        public long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public TipoMantenimiento Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public Mantenimiento Mantenimiento
        {
            get { return mantenimiento; }
            set { mantenimiento = value; }
        }

        public Maquina Maquina
        {
            get { return maquina; }
            set { maquina = value; }
        }

        public DetallePlanMantenimiento DetallePlanMantenimiento
        {
            get { return detallePlanMantenimiento; }
            set { detallePlanMantenimiento = value; }
        }

        public Empleado Empleado
        {
            get { return empleado; }
            set { empleado = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public DateTime FechaRealizacion
        {
            get { return fechaRealizacion; }
            set { fechaRealizacion = value; }
        }

        public CausaFallo CausaFallo
        {
            get { return causaFallo; }
            set { causaFallo = value; }
        }

        public string Observacion
        {
            get { return observacion; }
            set { observacion = value; }
        }

        public Averia Averia
        {
            get { return averia; }
            set { averia = value; }
        }
    }
}
