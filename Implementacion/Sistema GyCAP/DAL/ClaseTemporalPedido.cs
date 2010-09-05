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

    }
}
