using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades
{
    public class SimulacionProduccion
    {
        private bool esPosible;

        public bool EsPosible
        {
            get { return esPosible; }
            set { esPosible = value; }
        }
        private DateTime fechaNecesidad;

        public DateTime FechaNecesidad
        {
            get { return fechaNecesidad; }
            set { fechaNecesidad = value; }
        }
        private DateTime fechaSugerida;

        public DateTime FechaSugerida
        {
            get { return fechaSugerida; }
            set { fechaSugerida = value; }
        }
        private DateTime fechaInicio;

        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }

        private int codigoCocina;

        public int CodigoCocina
        {
            get { return codigoCocina; }
            set { codigoCocina = value; }
        }
        private int cantidadNecesidad;

        public int CantidadNecesidad
        {
            get { return cantidadNecesidad; }
            set { cantidadNecesidad = value; }
        }
        private int cantidadSugerida;

        public int CantidadSugerida
        {
            get { return cantidadSugerida; }
            set { cantidadSugerida = value; }
        }

        private bool isValid;

        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }
    }
}
