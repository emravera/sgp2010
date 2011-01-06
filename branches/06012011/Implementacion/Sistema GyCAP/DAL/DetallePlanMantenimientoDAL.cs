using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class DetallePlanMantenimientoDAL
    {
        public static void Insertar(Entidades.DetallePlanMantenimiento detalle, SqlTransaction transaccion)
        {
            string sqlInsert = @"INSERT INTO [DETALLE_PLANES_MANTENIMIENTO] 
                                        ([PMAN_NUMERO]
                                        ,[EDMAN_CODIGO]
                                        ,[MAN_CODIGO]
                                        ,[UMED_CODIGO]
                                        ,[DPMAN_FRECUENCIA]
                                        ,[DPMAN_DESCRIPCION])
                                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5) SELECT @@Identity";

            object[] valorParametros = {detalle.Plan.Numero, detalle.Estado.Codigo, detalle.Mantenimiento.Codigo
                                        ,detalle.UnidadMedida.Codigo, detalle.Frecuencia, detalle.Descripcion };
            detalle.Codigo = Convert.ToInt64(DB.executeScalar(sqlInsert, valorParametros, transaccion));
        }

        public static void Eliminar(Entidades.DetallePlanMantenimiento detalle, SqlTransaction transaccion)
        {
            string sqlDelete = "DELETE FROM DETALLE_PLANES_MANTENIMIENTO WHERE DPMAN_CODIGO = @p0";
            object[] valorParametros = { detalle.Codigo };
            DB.executeNonQuery(sqlDelete, valorParametros, transaccion);
        }

        public static void Actualizar(Entidades.DetallePlanMantenimiento detalle, SqlTransaction transaccion)
        {
//            string sqlUpdate = @"UPDATE DETALLE_PLANES_MANTENIMIENTO SET DPED_CANTIDAD = @p0 
//                               WHERE DPED_CODIGO = @p1";
//            object[] valorParametros = { detalle.Cantidad, detalle.Codigo };
//            DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
        }

        public static void EliminarDetallePlanMantenimiento(long codigoPlan, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM DETALLE_PLANES_MANTENIMIENTO WHERE PMAN_NUMERO = @p0";
            object[] valorParametros = { codigoPlan };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }

        public static void ObtenerDetallePlanMantenimiento(Data.dsRegistrarMantenimiento ds)
        {
            string sql = @"SELECT DPMAN_CODIGO, PMAN_NUMERO, EDMAN_CODIGO, MAN_CODIGO, UMED_CODIGO
                          , DPMAN_FRECUENCIA, DPMAN_DESCRIPCION
                          FROM DETALLE_PLANES_MANTENIMIENTO WHERE PMAN_NUMERO = @p0";

            object[] valorParametros; ;

            foreach (Data.dsRegistrarMantenimiento.PLANES_MANTENIMIENTORow rowPlan in ds.PLANES_MANTENIMIENTO)
            {
                valorParametros = new object[] { rowPlan.PMAN_NUMERO };
                DB.FillDataTable(ds.DETALLE_PLANES_MANTENIMIENTO, sql, valorParametros);
            }
        }

        public static void ObtenerDetallePlanMantenimiento(DataTable dtDetallePlanMantenimiento ,long PMAN_NUMERO)
        {
            string sql = @"SELECT DPMAN_CODIGO, PMAN_NUMERO, EDMAN_CODIGO, MAN_CODIGO, UMED_CODIGO
                          , DPMAN_FRECUENCIA, DPMAN_DESCRIPCION
                          FROM DETALLE_PLANES_MANTENIMIENTO WHERE PMAN_NUMERO = @p0";

            object[] valorParametros; ;

            valorParametros = new object[] { PMAN_NUMERO };
            DB.FillDataTable(dtDetallePlanMantenimiento, sql, valorParametros);

        }
    }
}
