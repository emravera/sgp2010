using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class DetallePlanMensualBLL
    {
        //metodo para traer el detalle de una cabecera
        public static void ObtenerDetalle(int idPlan, Data.dsPlanMensual ds)
        {
            DAL.DetallePlanMensualDAL.ObtenerDetalle(idPlan, ds);
        }

        //metodo que trae los detalles de un plan mensual
        public static void ObtenerDetallePM(int idPlanAnual, string mes, DataTable dtDetalle)
        {
            DAL.DetallePlanMensualDAL.ObtenerDetallePM(idPlanAnual, mes, dtDetalle);
        }

        //metodo para traer todos los detalles
        public static void ObtenerDetalle(DataTable dtDetalle)
        {
            DAL.DetallePlanMensualDAL.ObtenerDetalle(dtDetalle);
        }

    }
}
