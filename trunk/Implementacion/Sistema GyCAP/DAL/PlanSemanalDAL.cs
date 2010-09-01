
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

        //Metodo que valida que no exista un plan semanal para ese año, mes y semana ya creado
        public static bool Validar(int codigoPlanMensual, int semana)
        {
            string sql = @"SELECT count(ps.psem_codigo)
                           FROM PLANES_MENSUALES as pm, PLANES_SEMANALES as ps
                           WHERE pm.pmes_codigo=ps.pmes_codigo and ps.psem_semana=@p0 and pm.pmes_codigo=@p1";

            object[] valorParametros = { codigoPlanMensual, semana };
            try
            {
                int cantidad = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
                if (cantidad == 0) return true;
                else return false;
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
