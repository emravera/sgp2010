using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class OperacionBLL
    {
        public static void ObetenerOperaciones(DataTable dtOperaciones)
        {
            DAL.OperacionDAL.ObtenerOperaciones(dtOperaciones);
        }
    }
}
