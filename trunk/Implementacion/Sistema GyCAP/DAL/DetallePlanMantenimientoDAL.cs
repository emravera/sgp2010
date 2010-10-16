using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DetallePlanMantenimientoDAL
    {
//        public static void Insertar(Entidades.DetallePlanMantenimiento detalle, SqlTransaction transaccion)
//        {
//            string sqlInsert = @"INSERT INTO [DETALLE_PLANES_MANTENIMIENTO] 
//                                        ([PMAN_NUMERO]
//                                        ,[MAQ_CODIGO]
//                                        ,[EDMAN_CODIGO]
//                                        ,[CF_NUMERO]
//                                        ,[E_CODIGO]
//                                        ,[REP_CODIGO]
//                                        ,[MAN_CODIGO]
//                                        ,[DPMAN_FECHAREALIZACIONPREVISTA]
//                                        ,[DPMAN_FECHAREALIZACIONREAL]
//                                        ,[DPMAN_OBSERVACIONES])
//                                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9) SELECT @@Identity";

//            object[] valorParametros = {detalle.Plan.Numero , detalle.Maquina.Codigo, detalle.Estado.Codigo 
//                                       ,detalle.CausaFallo.Codigo, detalle.Empleado.Codigo,detalle.Repuesto 
//                                       };
//            detalle.Codigo = Convert.ToInt64(DB.executeScalar(sqlInsert, valorParametros, transaccion));
//        }

//        public static void Eliminar(Entidades.DetallePedido detalle, SqlTransaction transaccion)
//        {
//            string sqlDelete = "DELETE FROM DETALLE_PLANES_MANTENIMIENTO WHERE DPED_CODIGO = @p0";
//            object[] valorParametros = { detalle.Codigo };
//            DB.executeNonQuery(sqlDelete, valorParametros, transaccion);
//        }

//        public static void Actualizar(Entidades.DetallePedido detalle, SqlTransaction transaccion)
//        {
//            string sqlUpdate = @"UPDATE DETALLE_PLANES_MANTENIMIENTO SET DPED_CANTIDAD = @p0 
//                               WHERE DPED_CODIGO = @p1";
//            object[] valorParametros = { detalle.Cantidad, detalle.Codigo };
//            DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
//        }

//        public static void EliminarDetallePedido(long codigoPlan, SqlTransaction transaccion)
//        {
//            string sql = "DELETE FROM DETALLE_PLANES_MANTENIMIENTO WHERE PMAN_NUMERO = @p0";
//            object[] valorParametros = { codigoPlan };
//            DB.executeNonQuery(sql, valorParametros, transaccion);
//        }
    }
}
