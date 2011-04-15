using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class StockMateriaPrimaDAL
    {
        public static int ObtenerTotalAnual(int codigoPA)
        {
            int cantidadAnual = 0;

            string sql = @"SELECT sum(dpan_cantidadmes)
                        FROM DETALLE_PLAN_ANUAL where pan_codigo=@p0";

            object[] valorParametros = { codigoPA };
            try
            {
               cantidadAnual = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }

            return cantidadAnual;
        }
        public static object ObtenerTotalModelo(int codigoModelo, int codigoPA)
        {
            object cantidadAnual = 0;

            string sql = @"SELECT sum (dps.dpsem_cantidadreal) 
                           FROM PLANES_ANUALES as pa, PLANES_MENSUALES  as pm, PLANES_SEMANALES as ps,
                                DIAS_PLAN_SEMANAL as dia, DETALLE_PLANES_SEMANALES as dps, COCINAS as coc
                           WHERE pa.pan_codigo=pm.pan_codigo and pm.pmes_codigo=ps.pmes_codigo and 
                                ps.psem_codigo=dia.psem_codigo and dia.diapsem_codigo=dps.diapsem_codigo and coc.coc_codigo=dps.coc_codigo 
                                and dps.coc_codigo = @p0  and  pa.pan_codigo=@p1";

            object[] valorParametros = {codigoModelo, codigoPA };
            try
            {
                cantidadAnual = DB.executeScalar(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }

            return cantidadAnual;
        }



    }
}
