using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class DomicilioBLL
    {
        //Selecciona los domicilios de un proveedor
        public static void ObtenerDomicilios(int codigoProveedor,DataTable dtDomicilios )
        {
            DAL.DomicilioDAL.ObtenerDomicilios(codigoProveedor, dtDomicilios);
        }

        //Elimina una instancia de domicilio de un proveedor
        public static void EliminarDomicilios(int codigoDomicilio)
        {
            DAL.DomicilioDAL.EliminarDomicilio(codigoDomicilio);
        }
   }
}
