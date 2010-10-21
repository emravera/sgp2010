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
               cantidadAnual =Convert.ToInt32(DB.executeScalar(sql, valorParametros,null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            return cantidadAnual;
        }



    }
}
