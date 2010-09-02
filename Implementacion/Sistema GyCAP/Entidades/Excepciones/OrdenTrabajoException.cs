using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class OrdenTrabajoException : System.Exception
    {
        private string mensajeExtendido;
                
        public OrdenTrabajoException() : base("Ocurrió un error al generar la Orden de Trabajo.") {}

        public OrdenTrabajoException(string mensaje) : base("Ocurrió un error al generar la Orden de Trabajo.\n\nError: " + mensaje) { }

        public void SetMensajeExtendido(string mensaje)
        {
             mensajeExtendido = mensaje;
        }

        public string GetMensajeExtendido()
        {
            return mensajeExtendido + "\\n\\n" + base.Message;
        }
    }
}
