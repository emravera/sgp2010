using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;

namespace GyCAP.UI.Sistema
{
    public class Utilidades
    {
        public enum elemento { conjunto, subconjunto, pieza, materiaPrima, cocina, repuesto};

        public static void GuardarImagenElemento(elemento tipoElemento, string archivo)
        {
            System.IO.FileInfo copiador = new System.IO.FileInfo(archivo);
            string destino;
            switch (tipoElemento)
            {
                case elemento.conjunto:
                    break;
                case elemento.subconjunto:
                    break;
                case elemento.pieza:
                    break;
                case elemento.materiaPrima:
                    break;
                case elemento.cocina:
                    break;
                case elemento.repuesto:
                    break;
                default:
                    break;
            }            
        }

        public static void ObtenerImagenElemento(elemento tipoElemento)
        {
        }
    }
}
