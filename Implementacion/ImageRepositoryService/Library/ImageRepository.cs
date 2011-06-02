using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Library
{
    public class ImageRepository : IDisposable
    {
        private bool disposing;
        private string directorio = string.Empty;
        public enum ElementType { Cocina, Parte, Empleado, Repuesto };

        ~ImageRepository()
        {
            Dispose();
        }

        public Stream GetElementImage(int codigoElemento, ElementType elementType)
        {
            directorio = "E:\\Repositorio\\Implementacion\\ImageRepositoryService\\Library\\" + elementType.ToString() + "\\coc" + codigoElemento.ToString() + ".jpg" ;

            //if (!File.Exists(directorio)) { throw new  }

            return new FileStream(directorio, FileMode.Open, FileAccess.Read);
        }

        public void SaveElementImage(int codigoElemento, ElementType elementType, Stream imagen)
        {
            
            directorio = "E:\\Repositorio\\Implementacion\\ImageRepositoryService\\Library\\" + elementType.ToString() + "\\coc" + codigoElemento.ToString() + ".jpg";

            if (!Directory.Exists(Path.GetDirectoryName(directorio))) { Directory.CreateDirectory(Path.GetDirectoryName(directorio)); }
                        
            using (FileStream outputStream = new FileStream(directorio, FileMode.Create))
            {
                imagen.CopyTo(outputStream);
            }
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
