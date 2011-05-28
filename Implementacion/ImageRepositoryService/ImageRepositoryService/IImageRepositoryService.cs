using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Drawing;

namespace ImageRepositoryService
{
    
    [ServiceContract]
    public interface IImageRepositoryService
    {
        [OperationContract]
        Image GetElementImage(int codigoElemento, Library.ImageRepository.ElementType elementType);
        [OperationContract]
        bool SaveElementImage(int codigoElemento, Library.ImageRepository.ElementType elementType, Image imagen);
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
        public int codigoElemento { get; set; }
        [DataMember]
        public Library.ImageRepository.ElementType elementType { get; set; }
        [DataMember]
        public Image imagen { get; set; }
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
