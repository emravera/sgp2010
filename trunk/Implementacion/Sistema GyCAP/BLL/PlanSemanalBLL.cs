
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class PlanSemanalBLL
    {

        //METODOS DE BÚSQUEDA        
        //Metodo que obtiene los planes semanales de un plan mesual
        public static void obtenerPS(DataTable dtPlanSemanal, int codigoPlanMensual)
        {
            DAL.PlanSemanalDAL.obtenerPS(dtPlanSemanal, codigoPlanMensual);
        }
        //Metodo que busca la cantidad de cocinas planificadas
        public static int? obtenerCocinasPlanificadas(int codigoCocina, int codigoPA, int codigoPM)
        {
            return DAL.PlanSemanalDAL.obtenerCocinasPlanificadas(codigoCocina,codigoPA,codigoPM);
        }

        //Metodo que busca la cantidad de cocinas planificadas de un pedido
        public static int? obtenerCocinasPlanificadas(int codigoCocina, int codigoPA, int codigoPM, int codigoPedido)
        {
            return DAL.PlanSemanalDAL.obtenerCocinasPlanificadas(codigoCocina, codigoPA, codigoPM,codigoPedido);
        }

        //METODOS DE VALIDACION
        //Metodo que busca y verifica si existe un plan semanal para un anio y mes determinado
        public static bool validarDetalle(int codigoPlanMensual, int numeroSemana)
        {
           return DAL.PlanSemanalDAL.Validar(codigoPlanMensual, numeroSemana);
        }

        //Metodo que verifica que no exista en un plan semanal ese dia
        public static bool validarDia(DateTime dia)
        {
            return DAL.PlanSemanalDAL.ValidarDia(dia);
        }
        
        //METODO DE GUARDADO DE DATOS
        public static int InsertarPlanSemanal(Entidades.PlanSemanal planSemanal, Entidades.DiasPlanSemanal diaPlanSemanal, Data.dsPlanSemanal dsPlanSemanal, bool esPrimero)
        {
            return DAL.PlanSemanalDAL.GuardarPlanSemanal(planSemanal,diaPlanSemanal,dsPlanSemanal,esPrimero);
        }
        
        //Metodo para MODIFICAR el Plan Semanal
        public static void ModificarPlanSemanal(Entidades.PlanSemanal planSemanal, Entidades.DiasPlanSemanal diaPlanSemanal, Data.dsPlanSemanal dsPlanSemanal)
        {
            DAL.PlanSemanalDAL.GuardarPlanModificado(planSemanal, diaPlanSemanal, dsPlanSemanal);
        }

        //METODO PARA ELIMINAR EL PLAN SEMANAL
        //METODO DE ELIMINACION
        public static void EliminarPlan(int codigoPlan, Data.dsPlanSemanal dsPlanSemanal)
        {
            DAL.PlanSemanalDAL.EliminarPlan(codigoPlan, dsPlanSemanal);
        }



    }
}
