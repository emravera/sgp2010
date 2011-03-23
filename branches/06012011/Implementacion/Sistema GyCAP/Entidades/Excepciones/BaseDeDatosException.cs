using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.Entidades.Excepciones
{
    public class BaseDeDatosException : System.Exception
    {
        private string mensajeExtendido;
                
        public BaseDeDatosException() : base("Ocurrió un error con la base de datos.") {}

        public BaseDeDatosException(string mensaje) : base("Ocurrió un error con la base de datos.\n\nError: " + mensaje) { }
        
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
