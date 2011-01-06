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
        public static readonly int EstadoEnCurso = 2;
        public static readonly int EstadoFinalizado = 5;
        
        public static void Insertar(Entidades.DetallePedido detalle, SqlTransaction transaccion)
        {
            string sqlInsert = @"INSERT INTO [DETALLE_PEDIDOS] 
                                        ([PED_CODIGO]
                                        ,[EDPED_CODIGO]
                                        ,[COC_CODIGO]
                                        ,[DPED_CANTIDAD])
                                        VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            object[] valorParametros = {detalle.Pedido.Codigo , detalle.Estado.Codigo
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
            object[] valorParametros = { detalle.Cantidad, detalle.Codigo };
            DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
        }

        public static void EliminarDetallePedido(long codigoPedido, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM DETALLE_PEDIDOS WHERE PED_CODIGO = @p0";
            object[] valorParametros = { codigoPedido };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }

        public static void ActualizarEstadoAEnCurso(int codigoDetalle, SqlTransaction transaccion)
        {
            ActualizarEstado(codigoDetalle, EstadoEnCurso, transaccion);
        }

        public static void ActualizarEstadoAFinalizado(int codigoDetalle, SqlTransaction transaccion)
        {
            ActualizarEstado(codigoDetalle, EstadoFinalizado, transaccion);
        }

        public static void ActualizarEstado(int codigoDetalle, int codigoEstado, SqlTransaction transaccion)
        {
            string sql = "UPDATE DETALLE_PEDIDOS SET edped_codigo = @p0 WHERE dped_codigo = @p1";
            object[] parametros = { codigoEstado, codigoDetalle };
            DB.executeNonQuery(sql, parametros, transaccion);
        }
        public static void CambiarEstado(int codigoDetallePedido, int estado)
        {
            SqlTransaction transaccion = null;

            try
            {
                //Inserto la demanda
                transaccion = DB.IniciarTransaccion();

                string sql = string.Empty;


                //Guardo las modificaciones
                sql = "UPDATE [DETALLE_PEDIDOS] SET edped_codigo=@p0 WHERE dped_codigo=@p1";
                object[] valorPar = { estado, codigoDetallePedido };
                DB.executeNonQuery(sql, valorPar, transaccion);

                transaccion.Commit();
                DB.FinalizarTransaccion();


            }
            catch (SqlException)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException();

            }
        }
        public static void ObtenerUnDetallePedido(DataTable dtDetallePedidos, int codigoDetalle)
        {
            string sql = @"SELECT dped_codigo, ped_codigo, edped_codigo, coc_codigo, dped_cantidad, dped_fecha_cancelacion
                           FROM DETALLE_PEDIDOS WHERE dped_codigo = @p0";

            object[] valorParametros = { codigoDetalle };
            try
            {
                //Se llena el Datatable
                DB.FillDataTable(dtDetallePedidos, sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }
        //Metodo que obtiene el detalle de pedido
        public static void ObtenerDetallePedido(DataTable dtDetallePedidos, int codigoPedido)
        {
            string sql = @"SELECT dped_codigo, ped_codigo, edped_codigo, coc_codigo, dped_cantidad, dped_fecha_cancelacion
                           FROM DETALLE_PEDIDOS WHERE ped_codigo = @p0";

            object[] valorParametros = { codigoPedido };
            try
            {
                //Se llena el Datatable
                DB.FillDataTable(dtDetallePedidos, sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }
    }
}
