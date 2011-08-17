using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class CocinaSinEstructuraActivaException : System.Exception
    {                
        public CocinaSinEstructuraActivaException() : base("La cocina seleccionada no posee una estructura activa.") {}

        public CocinaSinEstructuraActivaException(string mensaje) : base(mensaje) { }
    }
}
