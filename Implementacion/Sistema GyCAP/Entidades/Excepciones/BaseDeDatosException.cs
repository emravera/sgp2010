using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class BaseDeDatosException : System.Exception
    {
        public BaseDeDatosException() : base("Ocurrió un error con la base de datos.") {}
    }
}
