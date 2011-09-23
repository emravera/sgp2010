using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class PedidoBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerPedido(object nombre, object numero, int idEstadoPedido, object fechaDesde, object fechaHasta, Data.dsCliente ds , bool obtenerDetalle)
        {
            DAL.PedidoDAL.ObtenerPedido(nombre, numero, idEstadoPedido, fechaDesde, fechaHasta, ds, obtenerDetalle);
        }

        public static int Insertar(Entidades.Pedido pedido, DataTable dtDetallePedido)
        {    
            if (EsPedido(pedido)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.PedidoDAL.Insertar(pedido, dtDetallePedido);
        }

        //Comprueba si existe una pieza dado su nombre y terminación
        public static bool EsPedido(Entidades.Pedido pedido)
        {
            return DAL.PedidoDAL.EsPedido(pedido);
        }

        public static void Actualizar(Data.dsCliente dsCliente)
        {
            DAL.PedidoDAL.Actualizar(dsCliente);
        }

        public static void Eliminar(long codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.PedidoDAL.PuedeEliminarse(codigo) && DAL.DetallePedidoDAL.PuedeEliminarse(codigo) )
            {
                //Puede eliminarse
                DAL.PedidoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        //Metodo que obtiene los pedidos a partir de una fecha
        public static void ObtenerPedidoFecha(DateTime fecha, DataTable dtPedidos)
        {
            DAL.PedidoDAL.ObtenerPedidoFecha(fecha, dtPedidos);
        }

        //Metodo que cambia el estado del pedido
        public static void CambiarEstadoPedido(int codigoPedido, int estado)
        {
            DAL.PedidoDAL.CambiarEstadoPedido(codigoPedido, estado);
        }

        //Metodo que obtiene un pedido a partir de una fecha y para un cliente determinado
        public static void ObtenerPedidoCliente(int codigoCliente, int estadoPedido, DataTable dtPedido)
        {
            DAL.PedidoDAL.ObtenerPedidosCliente(codigoCliente,estadoPedido,dtPedido);
        }       
        
    }
}
