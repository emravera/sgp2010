using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DetalleDemandaAnualDAL
    {
        public static void ObtenerDetalle(int idCodigo, Data.dsEstimarDemanda ds)
        {
            string sql = @"SELECT ddeman_codigo, deman_codigo, ddeman_mes, ddeman_cantidadmes
                        FROM DETALLE_DEMANDAS_ANUALES WHERE deman_codigo=@p0";

            object[] parametros = { idCodigo };

            try
            {
                DB.FillDataSet(ds, "DETALLE_DEMANDAS_ANUALES", sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        public static int ObtenerTotal(int idCodigo)
        {
            string sql = @"SELECT sum(ddeman_cantidadmes)
                        FROM DETALLE_DEMANDAS_ANUALES WHERE deman_codigo=@p0";

            object[] valorParametros = { idCodigo };

            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        public static int ObtenerCantidadAñoMes(int año, string nombre, string mes)
        {
            string sql = @"SELECT det.ddeman_cantidadmes
                        FROM DETALLE_DEMANDAS_ANUALES as det , DEMANDAS_ANUALES as dem
                        WHERE det.deman_codigo=dem.deman_codigo and dem.deman_anio=@p0 and det.ddeman_mes=@p1 and dem.deman_nombre=@p2";

            object[] valorParametros = { año, mes, nombre };

            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }


        public static int Insertar(Entidades.DetalleDemandaAnual detalle)
        {
                      
                //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
                string sql = "INSERT INTO [DETALLE_DEMANDAS_ANUALES] ([deman_codigo], [ddeman_mes], [ddeman_cantidadmes]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
                object[] valorParametros = { detalle.Demanda.Codigo, detalle.Mes, detalle.Cantidadmes };

                try
                {
                    return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
                       
        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.DetalleDemandaAnual detalle)
        {
            string sql = @"UPDATE DETALLE_DEMANDAS_ANUALES SET ddeman_cantidadmes = @p0
                         WHERE deman_codigo = @p1 and ddeman_mes= @p2";
            object[] valorParametros = { detalle.Cantidadmes , detalle.Demanda.Codigo, detalle.Mes };
            
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //MODIFICAR 
        //Metodo que devuelve el valor de la identidad
        public static int ObtenerID(Entidades.DetalleDemandaAnual detalle)
        {
            string sql =@"SELECT ddeman_codigo FROM DETALLE_DEMANDAS_ANUALES
                            WHERE deman_codigo=@p0 and ddeman_mes=@p1";
            object[] valorParametros = {  detalle.Demanda.Codigo, detalle.Mes };

            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
