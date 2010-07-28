using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class PlanAnualBLL
    {
        //Metodos para la busqueda
        public static void ObtenerTodos(int anio, Data.dsPlanAnual ds)
        {
            DAL.PlanAnualDAL.ObtenerTodos(anio, ds);
        }
        public static void ObtenerTodos(Data.dsPlanAnual ds)
        {
            DAL.PlanAnualDAL.ObtenerTodos(ds);
        }

    }
}
