using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.BLL
{
    public class DetallePedidoBLL
    {        
        public static void Insertar(Entidades.DetallePedido detalle, SqlTransaction transaccion)
        {
            DAL.DetallePedidoDAL.Insertar(detalle, transaccion); 
        }

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
    }
}
