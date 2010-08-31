using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class DetallePlanSemanalBLL
    {
        //metodo para traer el detalle de una cabecera
        public static void ObtenerDetalle(int idPlan, DataTable dtDetalle)
        {
            DAL.DetallePlanSemanalDAL.ObtenerDetalle(dtDetalle,idPlan);
        }
    }
}
