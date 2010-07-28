using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DetalleSubconjuntoDAL
    {
        public static void Insertar(Entidades.DetalleSubconjunto detalle, SqlTransaction transaccion)
        {
            string sqlInsert = @"INSERT INTO [DETALLE_SUBCONJUNTO] 
                               ([sconj_codigo]
                               ,[pza_codigo]
                               ,[dsc_cantidad])
                               VALUES (@p0, @p1, @p2) SELECT @@Identity";

            object[] valorParametros = { detalle.CodigoSubconjunto, detalle.CodigoPieza, detalle.Cantidad };
            detalle.CodigoDetalle = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));
        }

        public static void Eliminar(Entidades.DetalleSubconjunto detalle, SqlTransaction transaccion)
        {
            string sqlDelete = "DELETE FROM DETALLE_SUBCONJUNTO WHERE dsc_codigo = @p0";
            object[] valorParametros = { detalle.CodigoDetalle };
            DB.executeNonQuery(sqlDelete, valorParametros, transaccion);
        }

        public static void Actualizar(Entidades.DetalleSubconjunto detalle, SqlTransaction transaccion)
        {
            string sqlUpdate = @"UPDATE DETALLE_SUBCONJUNTO SET dsc_cantidad = @p0 
                               WHERE dsc_codigo = @p0";

            object[] valorParametros = { detalle.Cantidad, detalle.CodigoDetalle };
            DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
        }

        public static void EliminarDetalleDeSubconjunto(int codigoSubconjunto, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM DETALLE_SUBCONJUNTO WHERE sconj_codigo = @p0";
            object[] valorParametros = { codigoSubconjunto };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }
    }
}
