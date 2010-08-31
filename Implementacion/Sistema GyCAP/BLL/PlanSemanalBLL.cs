
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




    }
}
