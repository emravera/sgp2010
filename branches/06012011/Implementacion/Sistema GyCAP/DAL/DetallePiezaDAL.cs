using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DetallePiezaDAL
    {
        public static void Insertar(Entidades.DetallePieza detalle, SqlTransaction transaccion)
        {
            string sqlInsert = @"INSERT INTO [MATERIASPRIMASXPIEZA] 
                                        ([pza_codigo]
                                        ,[mp_codigo]
                                        ,[mpxp_cantidad])
                                        VALUES (@p0, @p1, @p2) SELECT @@Identity";

            object[] valorParametros = { detalle.CodigoPieza, detalle.CodigoMateriaPrima, detalle.Cantidad };
            detalle.CodigoDetalle = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));
        }

        public static void Eliminar(Entidades.DetallePieza detalle, SqlTransaction transaccion)
        {
            string sqlDelete = "DELETE FROM MATERIASPRIMASXPIEZA WHERE mpxp_codigo = @p0";
            object[] valorParametros = { detalle.CodigoDetalle };
            DB.executeNonQuery(sqlDelete, valorParametros, transaccion);
        }

        public static void Actualizar(Entidades.DetallePieza detalle, SqlTransaction transaccion)
        {
            string sqlUpdate = @"UPDATE MATERIASPRIMASXPIEZA SET mpxp_cantidad = @p0 
                               WHERE mpxp_codigo = @p0";
            object[] valorParametros = { detalle.Cantidad, detalle.CodigoDetalle };
            DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
        }

        public static void EliminarDetalleDePieza(int codigoPieza, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM MATERIASPRIMASXPIEZA WHERE pza_codigo = @p0";
            object[] valorParametros = { codigoPieza };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }
    }
}
