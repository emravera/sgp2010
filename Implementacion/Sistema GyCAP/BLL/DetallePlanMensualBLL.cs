using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class DetallePlanMensualBLL
    {
        //metodo para traer el detalle de una cabecera
        public static void ObtenerDetalle(int idPlan, Data.dsPlanMensual ds)
        {
            DAL.DetallePlanMensualDAL.ObtenerDetalle(idPlan, ds);
        }


    }
}
