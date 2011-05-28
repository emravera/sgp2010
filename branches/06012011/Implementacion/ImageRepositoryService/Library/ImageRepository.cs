using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Library
{
    public class ImageRepository : IDisposable
    {
        private bool disposing;
        public enum ElementType { Cocina, Parte, Empleado, Repuesto };

        ~ImageRepository()
        {
            Dispose();
        }

        public Image GetElementImage(int codigoElemento, ElementType elementType)
        {
            string directorio = elementType.ToString() + "\\" + codigoElemento.ToString() + ".jpg" ;
            return Image.FromFile(directorio);
        }

        public bool SaveElementImage(int codigoElemento, ElementType elementType, Image imagen)
        {
            return true;
        }

        public bool DeleteElementImage(int codigoElemento, ElementType elementType)
        {
            return true;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool b)
        {
            if (!disposing)
            {
                disposing = true;
                GC.SuppressFinalize(this);
            }
        }

        #endregion
    }
}
