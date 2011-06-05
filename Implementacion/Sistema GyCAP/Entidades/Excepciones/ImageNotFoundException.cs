using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.Entidades.Excepciones
{
    public class ImageNotFoundException : System.Exception
    {
        private string mensajeExtendido;
                
        public ImageNotFoundException() : base("Imagen no encontrada.") {}        
        
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
