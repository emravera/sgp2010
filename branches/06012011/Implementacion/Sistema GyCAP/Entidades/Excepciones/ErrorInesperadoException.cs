using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class ErrorInesperadoException : System.Exception
    {
        public ErrorInesperadoException() : base("Ocurrió un error inesperado.") { }
    }
}
