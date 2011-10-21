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
        //Definimos las variables con los estados.
        public static readonly int EstadoEnCurso = DAL.EstadoPedidoDAL.ObtenerIDEstadosPedido("En Curso");
        public static readonly int EstadoFinalizado = DAL.EstadoPedidoDAL.ObtenerIDEstadosPedido("Finalizado");
        
        //Metodo para insertar el detalle de pedido
        public static int Insertar(Data.dsCliente.DETALLE_PEDIDOSRow row, SqlTransaction transaccion)
        {
            string sqlInsert = string.Empty;
            int codigoDetalle = 0;

            if (!row.IsDPED_FECHA_INICIONull())
            {
                sqlInsert = @"INSERT INTO [DETALLE_PEDIDOS] 
                                        ([PED_CODIGO]
                                        ,[EDPED_CODIGO]
                                        ,[COC_CODIGO]
                                        ,[DPED_CANTIDAD]
                                        ,[DPED_CODIGONEMONICO]
                                        ,[DPED_FECHA_ENTREGA_PREVISTA]
                                        ,[DPED_FECHA_INICIO])
                                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6) 
                                        SELECT @@Identity";
                object[] valorParametros = {row.PED_CODIGO, row.EDPED_CODIGO, row.COC_CODIGO, row.DPED_CANTIDAD,
                                        row.DPED_CODIGONEMONICO, row.DPED_FECHA_ENTREGA_PREVISTA, Convert.ToDateTime(row.DPED_FECHA_INICIO)};
                
                //Ejecutamos la consulta y obtenemos el codigo
                codigoDetalle = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));
            }
            else
            {
                sqlInsert = @"INSERT INTO [DETALLE_PEDIDOS] 
                                        ([PED_CODIGO]
                                        ,[EDPED_CODIGO]
                                        ,[COC_CODIGO]
                                        ,[DPED_CANTIDAD]
                                        ,[DPED_CODIGONEMONICO]
                                        ,[DPED_FECHA_ENTREGA_PREVISTA])
                                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5) 
                                        SELECT @@Identity";
                object[] valorParam = {row.PED_CODIGO, row.EDPED_CODIGO, row.COC_CODIGO, row.DPED_CANTIDAD,
                                        row.DPED_CODIGONEMONICO, row.DPED_FECHA_ENTREGA_PREVISTA};

                //Ejecutamos la consulta y obtenemos el codigo
                codigoDetalle = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParam, transaccion));
            }       

            return codigoDetalle;
        }

        public static void Eliminar(Entidades.DetallePedido detalle, SqlTransaction transaccion)
        {
            string sqlDelete = "DELETE FROM DETALLE_PEDIDOS WHERE DPED_CODIGO = @p0";
            object[] valorParametros = { detalle.Codigo };
            DB.executeNonQuery(sqlDelete, valorParametros, transaccion);
        }

        public static void Actualizar(Data.dsCliente.DETALLE_PEDIDOSRow row, SqlTransaction transaccion)
        {
            string sqlUpdate = @"UPDATE DETALLE_PEDIDOS SET 
                                        DPED_CANTIDAD = @p0, 
                                        DPED_FECHA_ENTREGA_PREVISTA = @p1,
                                        COC_CODIGO = @p2
                               WHERE DPED_CODIGO = @p3";
            object[] valorParametros = { row.DPED_CANTIDAD, row.DPED_FECHA_ENTREGA_PREVISTA, row.COC_CODIGO, row.DPED_CODIGO };
            DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
        }

        public static void CancelarPedido(Data.dsCliente.DETALLE_PEDIDOSRow row, SqlTransaction transaccion)
        {
            string sqlUpdate = @"UPDATE DETALLE_PEDIDOS SET 
                                        DPED_CANTIDAD = @p0, 
                                        DPED_FECHA_ENTREGA_PREVISTA = @p1,
                                        COC_CODIGO = @p2
                                        EDPED_CODIGO = @p3
                                        DPED_FECHA_CANCELACION = @p4
                               WHERE DPED_CODIGO = @p5";
            object[] valorParametros = { row.DPED_CANTIDAD, row.DPED_FECHA_ENTREGA_PREVISTA, row.COC_CODIGO, row.EDPED_CODIGO, row.DPED_FECHA_CANCELACION, row.DPED_CODIGO };
                        
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

        public static void CancelarDetallePedido(int codigoDetalle, DateTime fechaCancelacion)
        {
            int codigoCancelado = DAL.EstadoDetallePedidoDAL.ObtenerCodigoEstado("Cancelado");

            string sql = @"UPDATE DETALLE_PEDIDOS 
                           SET edped_codigo = @p0,
                           dped_fecha_cancelacion = @p1 
                           WHERE dped_codigo = @p2";

            object[] parametros = { codigoCancelado, fechaCancelacion, codigoDetalle };
            
            DB.executeNonQuery(sql, parametros, null);

        }

        public static void CambiarEstado(int codigoDetallePedido, int estado)
        {
            SqlTransaction transaccion = null;

            try
            {               
                transaccion = DB.IniciarTransaccion();

                string sql = @"UPDATE [DETALLE_PEDIDOS] 
                                SET edped_codigo=@p0 WHERE dped_codigo=@p1";

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
            string sql = @"SELECT dped_codigo, ped_codigo, edped_codigo, coc_codigo, dped_cantidad, 
                           dped_fecha_cancelacion, dped_fecha_inicio
                           FROM DETALLE_PEDIDOS WHERE dped_codigo = @p0";

            object[] valorParametros = { codigoDetalle };

            try
            {
                //Se llena el Datatable
                DB.FillDataTable(dtDetallePedidos, sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo para obtener el ID de pedido
        public static int ObtenerIDPedidoDetalle(int codigoDetalle)
        {
            string sql = @"SELECT ped_codigo
                           FROM DETALLE_PEDIDOS WHERE dped_codigo = @p0";

            object[] valorParametros = { codigoDetalle };
            int codigoPedido = 0;

            try
            {
                //Se llena el Datatable
                codigoPedido = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            return codigoPedido;
        }

        //Obtengo los detalles cuya fecha de inicio esta en ese mes
        public static int ValidarDetallesPedidos(DateTime fechaDesde, DateTime fechaHasta)
        {
            string sql = @"SELECT count(dped_codigo)
                           FROM DETALLE_PEDIDOS 
                           WHERE dped_fecha_inicio >= @p0 and 
                           dped_fecha_inicio <= @p1 and edped_codigo = @p2";

            int cantidad =0;
            object[] valorParametros = { fechaDesde, fechaHasta, DAL.EstadoDetallePedidoDAL.ObtenerCodigoEstado("Pendiente") };

            try
            {
                //Se llena el Datatable
               cantidad = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            return cantidad;
        }
        
        //Metodo que obtiene el detalle de pedido
        public static void ObtenerDetallePedido(DataTable dtDetallePedidos, int codigoPedido)
        {
            string sql = @"SELECT dped_codigo, ped_codigo, edped_codigo, coc_codigo, 
                                  dped_cantidad, dped_fecha_cancelacion, dped_codigonemonico,
                                  dped_fecha_entrega_prevista, dped_fecha_entrega_real, dped_fecha_inicio 
                           FROM DETALLE_PEDIDOS WHERE ped_codigo = @p0";

            object[] valorParametros = { codigoPedido };
            try
            {
                //Se llena el Datatable
                DB.FillDataTable(dtDetallePedidos, sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo que obtiene el detalle de pedido
        public static void ObtenerDetallePedidoEstado(DataTable dtDetallePedidos, int codigoPedido, int codigoEstado)
        {
            string sql = @"SELECT dped_codigo, ped_codigo, edped_codigo, coc_codigo, 
                                  dped_cantidad, dped_fecha_cancelacion, dped_codigonemonico,
                                  dped_fecha_entrega_prevista, dped_fecha_entrega_real, dped_fecha_inicio 
                           FROM DETALLE_PEDIDOS WHERE ped_codigo = @p0 and edped_codigo = @p1";

            object[] valorParametros = { codigoPedido, codigoEstado };
            try
            {
                //Se llena el Datatable
                DB.FillDataTable(dtDetallePedidos, sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            //Hacer un for para recorrer los detalles
            string sqlDENT = "SELECT count(DENT_CODIGO) FROM DETALLE_ENTREGA_PRODUCTO WHERE DPED_codigo = @p0";
            string sqlDPMES = "SELECT count(DPMES_CODIGO) FROM DETALLE_PLANES_MENSUALES WHERE DPED_codigo = @p0";
            string sqlDPSEM = "SELECT count(DPSEM_CODIGO) FROM DETALLE_PLANES_SEMANALES WHERE DPED_codigo = @p0";

            object[] valorParametros = { codigo };
            try
            {
                int resultadoDENT = Convert.ToInt32(DB.executeScalar(sqlDENT, valorParametros, null));
                int resultadoDPMES = Convert.ToInt32(DB.executeScalar(sqlDPMES, valorParametros, null));
                int resultadoDPSEM = Convert.ToInt32(DB.executeScalar(sqlDPSEM, valorParametros, null));

                if (resultadoDENT == 0 && resultadoDPMES == 0 && resultadoDPSEM == 0) { return true; }
                else { return false; }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
