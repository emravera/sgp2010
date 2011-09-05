using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class CocinaBaseException : System.Exception
    {
        public CocinaBaseException(string mensaje) : base(mensaje) { }
    }
}
