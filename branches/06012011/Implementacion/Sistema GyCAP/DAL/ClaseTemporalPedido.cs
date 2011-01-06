using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class ClaseTemporalPedido
    {
        //Metodo que obtiene el pedido
        public static void ObtenerPedido(DateTime fecha, Data.dsPlanMensual dsPlanMensual)
        {
            string sql = @"SELECT ped_codigo, cli_codigo, eped_codigo, ped_fechaentregaprevista, ped_fechaentregareal, ped_fecha_alta, ped_numero
                           FROM PEDIDOS WHERE ped_fechaentregaprevista >= @p0";
            string dia = "'" + fecha.ToString() + "'";
            object[] valorParametros = { fecha };
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(dsPlanMensual, "PEDIDOS", sql, valorParametros);
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

        public static void CambiarEstadoPedido(int codigoPedido, int estado)
        {
            SqlTransaction transaccion = null;

            try
            {
                //Inserto la demanda
                transaccion = DB.IniciarTransaccion();

                string sql = string.Empty;


                //Guardo las modificaciones
                sql = "UPDATE [PEDIDOS] SET eped_codigo=@p0 WHERE ped_codigo=@p1";
                object[] valorPar = { estado, codigoPedido };
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

    }
}
