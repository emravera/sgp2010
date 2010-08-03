using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class DetallePlanAnualBLL
    {
        //metodo para traer el detalle de una cabecera
        public static void ObtenerDetalle(int idPlan, Data.dsPlanAnual ds)
        {
            DAL.DetallePlanAnualDAL.ObtenerDetalle(idPlan, ds);
        }
        //Metodo para buscar el ID
        public static int ObtenerID(Entidades.DetallePlanAnual detalle)
        {
            return DAL.DetallePlanAnualDAL.ObtenerID(detalle);
        }

        //Metodo que devuelve una fila del plan anual
        public static void ObtenerFila(Data.dsPlanMateriasPrimas ds, int codigo)
        {
            DAL.DetallePlanAnualDAL.ObtenerDetalle(ds,codigo);
        }

        
    }
}
