using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DemandaAnualDAL
    {
        public static void ObtenerTodos(int anio, Data.dsEstimarDemanda ds)
        {
            string sql = @"SELECT deman_codigo, deman_anio, deman_fechainicio
                        FROM DEMANDAS_ANUALES WHERE deman_anio=@p0";
            
            object[] parametros = { anio };
            
            try
            {
                DB.FillDataSet(ds, "DEMANDAS_ANUALES", sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerTodos(Data.dsEstimarDemanda ds)
        {
            string sql = @"SELECT deman_codigo, deman_anio, deman_fechainicio
                        FROM DEMANDAS_ANUALES";
            try
            {
                DB.FillDataSet(ds, "DEMANDAS_ANUALES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
