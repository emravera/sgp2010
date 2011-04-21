using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class TipoUbicacionStockBLL
    {
        public static readonly int TipoVista = 1;
        
        public static void ObtenerTiposUbicacionStock(DataTable dtTiposUbicacionStock)
        {
            DAL.TipoUbicacionStockDAL.ObtenerTiposUbicacionStock(dtTiposUbicacionStock);
        }
    }
}
