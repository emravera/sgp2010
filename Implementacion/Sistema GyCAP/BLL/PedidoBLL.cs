using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class PedidoBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerPedido(object nombre, object numero, int idEstadoPedido, DateTime fechaDesde, DateTime fechaHasta, Data.dsCliente ds , bool obtenerDetalle)
        {
            DAL.PedidoDAL.ObtenerPedido(nombre, numero, idEstadoPedido, fechaDesde, fechaHasta, ds, obtenerDetalle);
        }

    }
}
