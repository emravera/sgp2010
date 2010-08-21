using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class PlanMensualBLL
    {
        //METODO DE BUSQUEDA
        public static void ObtenerTodos(int anio, string mes, Data.dsPlanMensual ds)
        {
            DAL.PlanMensualDAL.ObtenerTodos(anio, mes, ds);
        }
        //Metodo que valida que no exista un plan mensual para el mismo mes y año
        public static bool ExistePlanMensual(int anio, string mes)
        {
           return DAL.PlanMensualDAL.Validar(anio, mes);
        }





    }
}
