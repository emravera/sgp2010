using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class ElementoInexistenteException : System.Exception
    {
        public ElementoInexistenteException() : base("No existe el elemento especificado.") { }
    }
}
