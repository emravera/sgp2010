using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class ProveedorBLL
    {

        public static void ObtenerProveedor(object razonSocial, object codigoSector, DataTable dtProveedor)
        {
            object codigo = null;
            if (codigoSector != null && Convert.ToInt32(codigoSector.ToString()) > 0) { codigo = codigoSector; }
            DAL.ProveedorDAL.ObtenerProveedores(razonSocial, codigo, dtProveedor);
        }

    }
}
