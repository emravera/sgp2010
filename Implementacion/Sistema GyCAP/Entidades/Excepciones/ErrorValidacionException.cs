using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.Entidades.Excepciones
{
    public class ErrorValidacionException: System.Exception
    {
        private string mensajeExtendido;
                
        public ErrorValidacionException() : base("Ocurrió un error de validación.") {}

        public ErrorValidacionException(string mensaje) : base("Error validación: " + mensaje) { }
        
        public void SetMensajeExtendido(string mensaje)
        {
             mensajeExtendido = mensaje;
        }

        public string GetMensajeExtendido()
        {
            return mensajeExtendido + "\n\n" + base.Message;
        }


    }
}
