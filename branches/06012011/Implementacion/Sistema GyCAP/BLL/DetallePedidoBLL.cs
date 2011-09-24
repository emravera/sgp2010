using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.BLL
{
    public class DetallePedidoBLL
    {        
        public static void Eliminar(Entidades.DetallePedido detalle, SqlTransaction transaccion)
        {
            DAL.DetallePedidoDAL.Eliminar(detalle, transaccion); 
        }

        public static void Actualizar(Entidades.DetallePedido detalle, SqlTransaction transaccion)
        {
            DAL.DetallePedidoDAL.Actualizar(detalle, transaccion); 
        }

        public static void EliminarDetallePedido(long codigoPedido, SqlTransaction transaccion)
        {
            DAL.DetallePedidoDAL.EliminarDetallePedido(codigoPedido, transaccion);    
        }

        public static void ObtenerDetallePedido(DataTable dtDetallePedido, int codigoPedido)
        {
            DAL.DetallePedidoDAL.ObtenerDetallePedido(dtDetallePedido, codigoPedido);
        }

        public static void CambiarEstado(int codigoDetallePedido, int estado)
        {
            DAL.DetallePedidoDAL.CambiarEstado(codigoDetallePedido, estado);
        }

        public static bool EsCliente(Entidades.Cliente cliente)
        {
            return DAL.ClienteDAL.esCliente(cliente);
        }
    }
}
