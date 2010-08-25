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

        //Metodo que valida que no exista un plan semanal con ese plan mensual para poder modificarse
        public static bool ExistePlanSemanal(int codigoPlan)
        {
            return DAL.PlanMensualDAL.ValidarActualizar(codigoPlan);
        }

        //METODO DE GUARDADO
        public static void GuardarPlan(Entidades.PlanMensual plan, Data.dsPlanMensual detalle)
        {
            DAL.PlanMensualDAL.GuardarPlan(plan, detalle);
        }

        //METODO DE MODIFICACION
        public static void GuardarPlanModificado(Entidades.PlanMensual plan, Data.dsPlanMensual detalle)
        {
            DAL.PlanMensualDAL.GuardarPlanModificado(plan, detalle);
        }

        //METODO DE ELIMINACION
        public static void EliminarPlan(int codigoPlan)
        {
            DAL.PlanMensualDAL.EliminarPlan(codigoPlan);
        }

        //OBTENCION DE LA SUMATORIA 
        public static int CalcularTotal(int anio, string mes)
        {
            return DAL.PlanMensualDAL.ObtenerCantidad(anio, mes);
        }


        


    }
}
