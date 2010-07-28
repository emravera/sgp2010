using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DetallePlanAnualDAL
    {
        public static void ObtenerDetalle(int idCodigo, Data.dsPlanAnual ds)
        {
            string sql = @"SELECT dpan_codigo, dpan_mes, dpan_cantidadmes
                        FROM DETALLE_PLAN_ANUAL WHERE pan_codigo=@p0";

            object[] parametros = { idCodigo };

            try
            {
                DB.FillDataSet(ds, "DETALLE_PLAN_ANUAL", sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }


    }
}
