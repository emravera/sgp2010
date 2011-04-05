using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class ElementoEnTransaccionException : System.Exception
    {
        public ElementoEnTransaccionException() : base(@"El elemento no se puede eliminar por estar asignado.") {}
    }
}
