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
    [ServiceContract]
    public interface IImageRepositoryService
    {
        [OperationContract]
        Stream GetElementImage(int codigoElemento, Library.ImageRepository.ElementType elementType);
        [OperationContract]
        void SaveElementImage(ImageUploadMessage msg);
        [OperationContract]
        bool DeleteElementImage(int codigoElemento, Library.ImageRepository.ElementType elementType);
    }

    
    [DataContract]
    public class GetElementImage
    {
        [DataMember]
        public int codigoElemento { get; set; }
        [DataMember]
        public Library.ImageRepository.ElementType elementType { get; set; }
    }

    [DataContract]
    public class SaveElementImage
    {        
        [DataMember]
        public ImageUploadMessage msg { get; set; }
    }

    [DataContract]
    public class DeleteElementImage
    {
        [DataMember]
        public int codigoElemento { get; set; }
        [DataMember]
        public Library.ImageRepository.ElementType elementType { get; set; }
    }
}
