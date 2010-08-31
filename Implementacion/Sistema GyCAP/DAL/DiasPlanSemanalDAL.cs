using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DiasPlanSemanalDAL
    {
        //Metodo que busca los dias que pertenecen a un plan semanal
        public static void ObtenerPS(DataTable dtDiasPS, int codigoPS)
        {
            string sql = @"SELECT diapsem_codigo, psem_codigo, diapsem_dia, diapsem_fecha
                        FROM DIAS_PLAN_SEMANAL where psem_codigo=@p0";

            object[] valorParametros = { codigoPS };
            try
            {
                DB.FillDataTable(dtDiasPS, sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
