using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class ElementoExistenteException : System.Exception
    {
        public ElementoExistenteException() : base("Ya existe un elemento con los datos ingresados.") {}
    }
}
