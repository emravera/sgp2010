using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class ClaseTemporalPedido
    {
        //CLASE DE PEDIDOS BLL
        //Metodo que obtiene los pedidos a partir de una fecha
        public static void ObtenerPedido(DateTime fecha, Data.dsPlanMensual dsPlanMensual)
        {
            DAL.ClaseTemporalPedido.ObtenerPedido(fecha, dsPlanMensual);
        }
        public static void CambiarEstadoPedido(int codigoPedido, int estado)
        {
            DAL.ClaseTemporalPedido.CambiarEstadoPedido(codigoPedido, estado);
        }

        //CLASE DE DETALLE DE PEDIDO
        public static void ObtenerDetallePedido(DataTable dtDetallePedido, int codigoPedido)
        {
            DAL.ClaseTemporalPedido.ObtenerDetallePedido(dtDetallePedido, codigoPedido);
        }
        public static void CambiarEstado(int codigoDetallePedido, int estado)
        {
            DAL.ClaseTemporalPedido.CambiarEstado(codigoDetallePedido, estado);
        }

    }
}
