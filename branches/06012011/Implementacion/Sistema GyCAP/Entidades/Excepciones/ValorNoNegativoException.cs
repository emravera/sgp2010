using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class ValorNoNegativoException : System.Exception
    {
        public ValorNoNegativoException() : base("No se admiten valores negativos.") {}
    }
}
