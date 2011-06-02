using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Drawing;
using System.IO;

namespace ImageRepositoryService
{    
    public class Service : IImageRepositoryService
    {
        #region IImageRepositoryService Members

        public Stream GetElementImage(int codigoElemento, Library.ImageRepository.ElementType elementType)
        {
            Stream imagen;
            using (Library.ImageRepository repository = new Library.ImageRepository())
            {            
                imagen = repository.GetElementImage(codigoElemento, elementType);
            }

            return imagen;
        }

        public void SaveElementImage(ImageUploadMessage msg)
        {            
            using (Library.ImageRepository repository = new Library.ImageRepository())
            {
                repository.SaveElementImage(msg.codigoElemento, msg.ElementType, msg.DataStream);
            }            
        }

        public bool DeleteElementImage(int codigoElemento, Library.ImageRepository.ElementType elementType)
        {
            bool result;
            using (Library.ImageRepository repository = new Library.ImageRepository())
            {
                result = repository.DeleteElementImage(codigoElemento, elementType);
            }

            return result;
        }

        #endregion
        
    }
}
