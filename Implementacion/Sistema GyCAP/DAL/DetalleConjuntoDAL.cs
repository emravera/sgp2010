using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DetalleConjuntoDAL
    {
        #region SubconjuntoxConjunto IT2
        public static void Insertar(Entidades.SubconjuntosxConjunto detalle, SqlTransaction transaccion)
        {
            string sqlInsert = @"INSERT INTO [SUBCONJUNTOSXCONJUNTO] 
                                ([conj_codigo]
                                ,[sconj_codigo]
                                ,[scxcj_cantidad])
                                VALUES (@p0, @p1, @p2) SELECT @@Identity";

            object[] valorParametros = { detalle.CodigoConjunto, detalle.CodigoSubconjunto, detalle.Cantidad };
            detalle.CodigoDetalle = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));
        }

        public static void Eliminar(Entidades.SubconjuntosxConjunto detalle, SqlTransaction transaccion)
        {
            string sqlDelete = "DELETE FROM SUBCONJUNTOSXCONJUNTO WHERE scxcj_codigo = @p0";
            object[] valorParametros = { detalle.CodigoDetalle };
            DB.executeNonQuery(sqlDelete, valorParametros, transaccion);
        }

        public static void Actualizar(Entidades.SubconjuntosxConjunto detalle, SqlTransaction transaccion)
        {
            string sqlUpdate = @"UPDATE SUBCONJUNTOSXCONJUNTO SET scxcj_cantidad = @p0 
                                 WHERE scxcj_codigo = @p0";
            object[] valorParametros = { detalle.Cantidad, detalle.CodigoDetalle };
            DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
        }
        
        #endregion

        #region PiezasxConjunto IT2

        public static void Insertar(Entidades.PiezasxConjunto detalle, SqlTransaction transaccion)
        {
            string sqlInsert = @"INSERT INTO [PIEZASXCONJUNTO] 
                                ([conj_codigo]
                                ,[pza_codigo]
                                ,[pxcj_cantidad])
                                VALUES (@p0, @p1, @p2) SELECT @@Identity";

            object[] valorParametros = { detalle.CodigoConjunto, detalle.CodigoPieza, detalle.Cantidad };
            detalle.CodigoDetalle = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));
        }

        public static void Eliminar(Entidades.PiezasxConjunto detalle, SqlTransaction transaccion)
        {
            string sqlDelete = "DELETE FROM PIEZASXCONJUNTO WHERE pxcj_codigo = @p0";
            object[] valorParametros = { detalle.CodigoDetalle };
            DB.executeNonQuery(sqlDelete, valorParametros, transaccion);
        }

        public static void Actualizar(Entidades.PiezasxConjunto detalle, SqlTransaction transaccion)
        {
            string sqlUpdate = @"UPDATE PIEZASXCONJUNTO SET pxcj_cantidad = @p0 
                                 WHERE pxcj_codigo = @p0";
            object[] valorParametros = { detalle.Cantidad, detalle.CodigoDetalle };
            DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
        }

        #endregion

        public static void EliminarDetalleDeConjunto(int codigoConjunto, SqlTransaction transaccion)
        {
            string sql1 = "DELETE FROM SUBCONJUNTOSXCONJUNTO WHERE conj_codigo = @p0";
            string sql2 = "DELETE FROM PIEZASXCONJUNTO WHERE conj_codigo = @p0";
            object[] valorParametros = { codigoConjunto };
            DB.executeNonQuery(sql1, valorParametros, transaccion);
            DB.executeNonQuery(sql2, valorParametros, transaccion);
        }

        #region DetalleConjunto IT1
        /*public static void Insertar(Entidades.DetalleConjunto detalle, SqlTransaction transaccion)
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
        }*/
        #endregion
    }
}
