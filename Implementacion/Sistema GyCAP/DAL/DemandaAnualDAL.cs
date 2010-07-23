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
            string sql = @"SELECT deman_codigo, deman_anio, deman_fechacreacion, deman_nombre, deman_paramcrecimiento
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
            string sql = @"SELECT deman_codigo, deman_anio, deman_fechacreacion, deman_nombre, deman_paramcrecimiento
                        FROM DEMANDAS_ANUALES";
            try
            {
                DB.FillDataSet(ds, "DEMANDAS_ANUALES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //Metodo para insertar
        public static int Insertar(Entidades.DemandaAnual demanda)
        {

            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO [DEMANDAS_ANUALES] ([deman_anio], [deman_fechacreacion], [deman_paramcrecimiento], [deman_nombre]) VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";
            object[] valorParametros = { demanda.Anio, demanda.FechaCreacion, demanda.ParametroCrecimiento, demanda.Nombre };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }



    }
}
