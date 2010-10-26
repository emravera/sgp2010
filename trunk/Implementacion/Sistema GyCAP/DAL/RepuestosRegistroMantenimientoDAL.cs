using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class RepuestosRegistroMantenimientoDAL
    {
        public static void Insertar(Entidades.RepuestosRegistroMantenimiento detalle, SqlTransaction transaccion)
        {
            string sqlInsert = @"INSERT INTO [REPUESTOS_REGISTRO_MANTENIMIENTO] 
                                        ([RMAN_CODIGO]
                                        ,[REP_CODIGO]
                                        ,[RRMAN_CANTIDAD])
                                        VALUES (@p0, @p1, @p2) SELECT @@Identity";

            object[] valorParametros = {detalle.RegistroMantenimiento.Codigo , detalle.Repuesto.Codigo
                                       ,detalle.Cantidad};
            detalle.Codigo = Convert.ToInt64(DB.executeScalar(sqlInsert, valorParametros, transaccion));
        }

        public static void Eliminar(Entidades.RepuestosRegistroMantenimiento detalle, SqlTransaction transaccion)
        {
            string sqlDelete = "DELETE FROM REPUESTOS_REGISTRO_MANTENIMIENTO WHERE RRMAN_CODIGO = @p0";
            object[] valorParametros = { detalle.Codigo };
            DB.executeNonQuery(sqlDelete, valorParametros, transaccion);
        }

        public static void Actualizar(Entidades.RepuestosRegistroMantenimiento detalle, SqlTransaction transaccion)
        {
            string sqlUpdate = @"UPDATE REPUESTOS_REGISTRO_MANTENIMIENTO SET RRMAN_CANTIDAD = @p0 
                               WHERE RRMAN_CODIGO = @p1";
            object[] valorParametros = { detalle.Cantidad, detalle.Codigo };
            DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
        }

        public static void EliminarRepuestosRegistroMantenimiento(long codigoRegistroMantenimiento, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM REPUESTOS_REGISTRO_MANTENIMIENTO WHERE RMAN_CODIGO = @p0";
            object[] valorParametros = { codigoRegistroMantenimiento };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }
    }
}
