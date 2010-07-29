using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class ElementoActivoException : System.Exception
    {
        public ElementoActivoException() : base("El elemento se encuentra en estado activo.") { }
    }
}
