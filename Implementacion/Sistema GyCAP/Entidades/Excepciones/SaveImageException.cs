using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class SaveImageException : System.Exception
    {
        private string mensajeExtendido;
                
        public SaveImageException() : base("La imagen no se pudo guardar.") {}        
        
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
