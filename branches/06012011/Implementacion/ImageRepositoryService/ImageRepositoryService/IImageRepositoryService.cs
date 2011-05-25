using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Drawing;

namespace ImageRepositoryService
{
    public enum ElementType { Cocina, Parte, Empleado, Repuesto };

    [ServiceContract]
    public interface IImageRepositoryService
    {
        [OperationContract]
        Image GetElementImage(int codigoElemento, ElementType elementType);
        [OperationContract]
        bool SaveElementImage(int codigoElemento, ElementType elementType, Image imagen);
        [OperationContract]
        bool DeleteElementImage(int codigoElemento, ElementType elementType);
    }

    
    [DataContract]
    public class GetElementImage
    {
        [DataMember]
        public int codigoElemento { get; set; }
        [DataMember]
        public ElementType elementType { get; set; }
    }

    [DataContract]
    public class SaveElementImage
    {
        [DataMember]
        public int codigoElemento { get; set; }
        [DataMember]
        public ElementType elementType { get; set; }
        [DataMember]
        public Image imagen { get; set; }
    }

    [DataContract]
    public class DeleteElementImage
    {
        [DataMember]
        public int codigoElemento { get; set; }
        [DataMember]
        public ElementType elementType { get; set; }
    }
}
