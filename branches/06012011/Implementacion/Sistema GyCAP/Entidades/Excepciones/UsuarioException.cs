using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class UsuarioException: System.Exception 
    {
        public UsuarioException(string mensaje) : base(mensaje) { }
    }
}
