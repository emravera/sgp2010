using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class ElementoFinalizadoException : System.Exception
    {
        public ElementoFinalizadoException() : base("El elemento se encuentra en estado finalizado.") { }
    }
}
