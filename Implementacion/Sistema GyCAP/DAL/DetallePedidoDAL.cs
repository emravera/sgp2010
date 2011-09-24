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
        public static void Insertar(Data.dsCliente.DETALLE_PEDIDOSRow row, SqlTransaction transaccion)
        {
            string sqlInsert = @"INSERT INTO [DETALLE_PEDIDOS] 
                                        ([PED_CODIGO]
                                        ,[EDPED_CODIGO]
                                        ,[COC_CODIGO]
                                        ,[DPED_CANTIDAD]
                                        ,[DPED_CODIGONEMONICO]
                                        ,[DPED_FECHA_ENTREGA_PREVISTA])
                                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5) SELECT @@Identity";

            object[] valorParametros = {row.PED_CODIGO, row.EDPED_CODIGO, row.COC_CODIGO, row.DPED_CANTIDAD,
                                        row.DPED_CODIGONEMONICO, row.DPED_FECHA_ENTREGA_PREVISTA};

            //Ejecutamos la consulta y obtenemos el codigo
            DB.executeNonQuery(sqlInsert, valorParametros, transaccion);          
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
            string sql = @"SELECT dped_codigo, ped_codigo, edped_codigo, coc_codigo, 
                                  dped_cantidad, dped_fecha_cancelacion, dped_codigonemonico,
                                  dped_fecha_entrega_prevista, dped_fecha_entrega_real 
                           FROM DETALLE_PEDIDOS WHERE ped_codigo = @p0";

            object[] valorParametros = { codigoPedido };
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
