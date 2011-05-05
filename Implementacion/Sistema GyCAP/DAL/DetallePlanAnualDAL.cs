using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DetallePlanAnualDAL
    {
        //Metodo de Busqueda de un detalle
        public static void ObtenerDetalle(int idCodigo, DataTable dtDetallePlanAnual)
        {
            string sql = @"SELECT dpan_codigo, dpan_mes, dpan_cantidadmes, pan_codigo
                        FROM DETALLE_PLAN_ANUAL WHERE pan_codigo=@p0";

            object[] parametros = { idCodigo };

            try
            {
                DB.FillDataTable(dtDetallePlanAnual, sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerDetalle(DataTable dtDetallePlanAnual)
        {
            string sql = @"SELECT dpan_codigo, dpan_mes, dpan_cantidadmes, pan_codigo
                        FROM DETALLE_PLAN_ANUAL";

            try
            {
                DB.FillDataTable(dtDetallePlanAnual, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }


        public static void ObtenerDetalle(DataTable dtDetallePlanAnual, int idCodigo)
        {
            string sql = @"SELECT dpan_codigo, dpan_mes, dpan_cantidadmes, pan_codigo
                        FROM DETALLE_PLAN_ANUAL WHERE pan_codigo=@p0";

            object[] parametros = { idCodigo };

            try
            {
                DB.FillDataTable(dtDetallePlanAnual, sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //INSERTAR
        public static int Insertar(Entidades.DetallePlanAnual detalle, SqlTransaction transaccion)
        {

            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO [DETALLE_PLAN_ANUAL] ([pan_codigo], [dpan_mes], [dpan_cantidadmes]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
            object[] valorParametros = { detalle.PlanAnual.Codigo, detalle.Mes, detalle.CantidadMes };

            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, transaccion));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.DetallePlanAnual detalle)
        {
            string sql = @"UPDATE DETALLE_PLAN_ANUAL SET dpan_cantidadmes = @p0
                         WHERE pan_codigo = @p1 and dpan_mes= @p2";
            object[] valorParametros = { detalle.CantidadMes , detalle.PlanAnual.Codigo , detalle.Mes };

            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //MODIFICAR 
        //Metodo que devuelve el valor de la identidad
        public static int ObtenerID(Entidades.DetallePlanAnual detalle)
        {
            string sql = @"SELECT dpan_codigo FROM DETALLE_PLAN_ANUAL
                            WHERE pan_codigo=@p0 and dpan_mes=@p1";
            object[] valorParametros = { detalle.PlanAnual.Codigo, detalle.Mes };

            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }


    }
}
