using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class EstructuraSinMateriaPrimaException : System.Exception
    {
        public EstructuraSinMateriaPrimaException() : base("La estructura de la cocina seleccionada no posee materias primas.") {}

        public EstructuraSinMateriaPrimaException(string mensaje) : base(mensaje) { }
    }
}
