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

        //CLASE DE DETALLE DE PEDIDO
        public static void ObtenerDetallePedido(DataTable dtDetallePedido, int codigoPedido)
        {
            DAL.ClaseTemporalPedido.ObtenerDetallePedido(dtDetallePedido, codigoPedido);
        }

    }
}
