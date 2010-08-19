using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class TurnoTrabajo
    {
        public TurnoTrabajo() { }

        public TurnoTrabajo(int codigoTurno, string nombreTurno, decimal inicioHora, decimal finFin)
        {
            this.Codigo = codigoTurno;
            this.Nombre = nombreTurno;
            this.HoraInicio = inicioHora;
            this.HoraFin = finFin;
        }
        
        private int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private decimal horaInicio;

        public decimal HoraInicio
        {
            get { return horaInicio; }
            set { horaInicio = value; }
        }
        private decimal horaFin;

        public decimal HoraFin
        {
            get { return horaFin; }
            set { horaFin = value; }
        }
    }
}
