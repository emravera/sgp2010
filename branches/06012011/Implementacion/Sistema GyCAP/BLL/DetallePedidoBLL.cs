﻿using System;
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

        public static void Actualizar(Data.dsCliente.DETALLE_PEDIDOSRow row, SqlTransaction transaccion)
        {
            DAL.DetallePedidoDAL.Actualizar(row, transaccion); 
        }

        public static void EliminarDetallePedido(long codigoPedido, SqlTransaction transaccion)
        {
            DAL.DetallePedidoDAL.EliminarDetallePedido(codigoPedido, transaccion);    
        }

        public static void ObtenerDetallePedido(DataTable dtDetallePedido, int codigoPedido)
        {
            DAL.DetallePedidoDAL.ObtenerDetallePedido(dtDetallePedido, codigoPedido);
        }

        //Metodo para cancelar un detalle de pedido
        public static void CancelarDetallePedido(int codigoDetallePedido, DateTime fechaCancelacion)
        {
            DAL.DetallePedidoDAL.CancelarDetallePedido(codigoDetallePedido, fechaCancelacion);
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