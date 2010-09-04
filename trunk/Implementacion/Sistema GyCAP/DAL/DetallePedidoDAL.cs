using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DetallePedidoDAL
    {
        public static void Insertar(Entidades.DetallePedido detalle, SqlTransaction transaccion)
        {
            string sqlInsert = @"INSERT INTO [DETALLE_PEDIDOS] 
                                        ([DPED_CODIGO]
                                        ,[PED_CODIGO]
                                        ,[EDPED_CODIGO]
                                        ,[COC_CODIGO]
                                        ,[DPED_CANTIDAD])
                                        VALUES (@p0, @p1, @p2, @p4, @p4) SELECT @@Identity";

            object[] valorParametros = {detalle.Codigo, detalle.Pedido.Codigo , detalle.Estado.Codigo
                                       ,detalle.Cocina.CodigoCocina, detalle.Cantidad};
            detalle.Codigo = Convert.ToInt64(DB.executeScalar(sqlInsert, valorParametros, transaccion));
        }

        public static void Eliminar(Entidades.DetallePedido detalle, SqlTransaction transaccion)
        {
            string sqlDelete = "DELETE FROM DETALLE_PEDIDOS WHERE DPED_CODIGO = @p0";
            object[] valorParametros = { detalle.Codigo };
            DB.executeNonQuery(sqlDelete, valorParametros, transaccion);
        }

        public static void Actualizar(Entidades.DetallePedido detalle, SqlTransaction transaccion)
        {
            string sqlUpdate = @"UPDATE DETALLE_PEDIDOS SET DPED_CANTIDAD = @p0 
                               WHERE DPED_CODIGO = @p1";
            object[] valorParametros = { detalle.Cantidad, detalle.Codigo};
            DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
        }

        public static void EliminarDetallePedido(long codigoPedido, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM DETALLE_PEDIDOS WHERE PED_CODIGO = @p0";
            object[] valorParametros = { codigoPedido };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }
    }
}
