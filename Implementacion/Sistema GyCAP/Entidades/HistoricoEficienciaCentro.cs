using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class HistoricoEficienciaCentro
    {
        private int centroTrabajo;

        public int CentroTrabajo
        {
            get { return centroTrabajo; }
            set { centroTrabajo = value; }
        }

        private DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private decimal eficiencia;

        public decimal Eficiencia
        {
            get { return eficiencia; }
            set { eficiencia = value; }
        }
    }
}
