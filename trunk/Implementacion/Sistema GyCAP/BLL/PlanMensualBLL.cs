using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class PlanMensualBLL
    {
        //METODO DE BUSQUEDA
        public static void ObtenerTodos(int anio, string mes, Data.dsPlanMensual ds)
        {
            DAL.PlanMensualDAL.ObtenerTodos(anio, mes, ds);
        }
        public static void ObtenerTodos(DataTable dtPlanMensual)
        {
            DAL.PlanMensualDAL.ObtenerTodos(dtPlanMensual);
        }
        //Metodo que obtiene todos los planes mensuales de un plan anual
        public static void ObtenerPMAnio(DataTable dtPlanMensual, int codigoPA)
        {
            DAL.PlanMensualDAL.ObtenerPMAnio(dtPlanMensual, codigoPA);
        }

        //Metodo que valida que no exista un plan mensual para el mismo mes y año
        public static bool ExistePlanMensual(int anio, string mes)
        {
           return DAL.PlanMensualDAL.Validar(anio, mes);
        }

        //Metodo que valida que no exista un detalle de plan semanal para ese pedido
        public static bool ExistePlanSemanalPedido(int codigoDetallePedido)
        {
            return DAL.PlanMensualDAL.ExistePlanSemanalPedido(codigoDetallePedido);
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
