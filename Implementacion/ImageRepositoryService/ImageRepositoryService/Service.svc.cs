using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Drawing;

namespace ImageRepositoryService
{    
    public class Service : IImageRepositoryService
    {
        #region IImageRepositoryService Members

        public Image GetElementImage(int codigoElemento, ElementType elementType)
        {
            return Image.FromFile("e:\\Imagenes\\Varios\\calidad.jpg");
        }

        public bool SaveElementImage(int codigoElemento, ElementType elementType, Image imagen)
        {
            return true;
        }

        public bool DeleteElementImage(int codigoElemento, ElementType elementType)
        {
            return true;
        }

        #endregion
    }
}
