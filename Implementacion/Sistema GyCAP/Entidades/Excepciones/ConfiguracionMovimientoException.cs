using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class MovimientoMalConfiguradoException : System.Exception
    {
        public MovimientoMalConfiguradoException(string mensaje) : base(mensaje) { }
    }
}
