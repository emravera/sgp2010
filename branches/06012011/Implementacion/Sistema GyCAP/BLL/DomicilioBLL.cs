using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class DomicilioBLL
    {

        public static void ObtenerDomicilios(int codigoProveedor,DataTable dtDomicilios )
        {
            DAL.DomicilioDAL.ObtenerDomicilios(codigoProveedor, dtDomicilios);
        }
    }
}
