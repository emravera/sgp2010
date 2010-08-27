
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class PlanSemanalDAL
    {
        //Metodo que obtiene las semanas del plan mensual
        public static void obtenerPS(DataTable dtPlanSemanal, int codigoPlanMensual)
        {
            string sql = @"SELECT psem_codigo, pmes_codigo, psem_semana, psem_fechacreacion
                        FROM PLANES_SEMANALES where pmes_codigo=@p0";

            object[] valorParametros = { codigoPlanMensual };
            try
            {
                DB.FillDataTable(dtPlanSemanal, sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
