using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class UbicacionStockBLL
    {
        public static void ObtenerUbicacionesStock(DataTable dtUbicacionStock)
        {
            DAL.UbicacionStockDAL.ObtenerUbicacionesStock(dtUbicacionStock);
        }
    }
}
