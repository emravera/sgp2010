using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class ConfiguracionInexistenteException : System.Exception
    {
        public ConfiguracionInexistenteException() : base("No existe una configuración con el nombre indicado.") {}

        public ConfiguracionInexistenteException(string mensaje) : base(mensaje) { }
    }
}
