using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class PlanMantenimiento
    {
        private long numero;
        private DateTime fecha;
        private string observaciones;
        private EstadoPlanMantenimiento estado;
        private string descripcion;

        public long Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public EstadoPlanMantenimiento Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
    }
}
