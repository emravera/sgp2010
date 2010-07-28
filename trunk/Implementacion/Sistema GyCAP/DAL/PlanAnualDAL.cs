using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class PlanAnualDAL
    {
        //BUSQUEDA
        public static void ObtenerTodos(int anio, Data.dsPlanAnual ds)
        {
            string sql = @"SELECT pan_codigo, pan_anio, deman_codigo, pan_fechacreacion
                        FROM PLANES_ANUALES WHERE pan_anio=@p0";

            object[] parametros = { anio };

            try
            {
                DB.FillDataSet(ds, "PLANES_ANUALES", sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerTodos(Data.dsPlanAnual ds)
        {
            string sql = @"SELECT pan_codigo, pan_anio, deman_codigo, pan_fechacreacion
                        FROM PLANES_ANUALES";
            try
            {
                DB.FillDataSet(ds, "PLANES_ANUALES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
