using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.ServiceModel;

namespace ImageRepositoryService
{
    [MessageContract]
    public class ImageUploadMessage
    {
        [MessageHeader(MustUnderstand = true)]
        public int codigoElemento { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public Library.ImageRepository.ElementType ElementType { get; set; }

        [MessageBodyMember(Order = 1)]
        public Stream DataStream { get; set; }

    }    
}
