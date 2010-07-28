using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DetalleConjuntoDAL
    {
        public static void Insertar(Entidades.DetalleConjunto detalle, SqlTransaction transaccion)
        {
            string sqlInsert = @"INSERT INTO [DETALLE_CONJUNTO] 
                                ([conj_codigo]
                                ,[sconj_codigo]
                                ,[dcj_cantidad])
                                VALUES (@p0, @p1, @p2) SELECT @@Identity";

            object[] valorParametros = { detalle.CodigoConjunto, detalle.CodigoSubconjunto, detalle.Cantidad };
            detalle.CodigoDetalle = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));            
        }

        public static void Eliminar(Entidades.DetalleConjunto detalle, SqlTransaction transaccion)
        {
            string sqlDelete = "DELETE FROM DETALLE_CONJUNTO WHERE dcj_codigo = @p0";
            object[] valorParametros = { detalle.CodigoDetalle };
            DB.executeNonQuery(sqlDelete, valorParametros, transaccion);
        }

        public static void Actualizar(Entidades.DetalleConjunto detalle, SqlTransaction transaccion)
        {
            string sqlUpdate = @"UPDATE DETALLE_CONJUNTO SET dcj_cantidad = @p0 
                                 WHERE dcj_codigo = @p0";
            object[] valorParametros = { detalle.Cantidad, detalle.CodigoDetalle };
            DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
        }

        public static void EliminarDetalleDeConjunto(int codigoConjunto, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM DETALLE_CONJUNTO WHERE conj_codigo = @p0";
            object[] valorParametros = { codigoConjunto };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }
    }
}
