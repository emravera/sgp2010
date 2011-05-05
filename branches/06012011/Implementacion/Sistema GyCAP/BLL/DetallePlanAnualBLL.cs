using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class DetallePlanAnualBLL
    {
        //metodo para traer el detalle de una cabecera
        public static void ObtenerDetalle(int idPlan, DataTable dtDetallePlanAnual)
        {
            DAL.DetallePlanAnualDAL.ObtenerDetalle(idPlan, dtDetallePlanAnual);
        }
        //Metodo que obtiene todos los detalles de los planes anuales
        public static void ObtenerDetalle(DataTable dtDetallePlanAnual)
        {
            DAL.DetallePlanAnualDAL.ObtenerDetalle(dtDetallePlanAnual);
        }

        //Metodo para buscar el ID
        public static int ObtenerID(Entidades.DetallePlanAnual detalle)
        {
            return DAL.DetallePlanAnualDAL.ObtenerID(detalle);
        }

        //Metodo que devuelve una fila del plan anual
        public static void ObtenerFila(DataTable dtDetallePlanAnual, int codigo)
        {
            DAL.DetallePlanAnualDAL.ObtenerDetalle(dtDetallePlanAnual,codigo);
        }

        
    }
}
