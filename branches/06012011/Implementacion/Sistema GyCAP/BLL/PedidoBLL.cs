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

        public static void Insertar(Data.dsCliente dsCliente)
        {
            //Si existe lanzamos la excepción correspondiente
            Entidades.Pedido pedido = new GyCAP.Entidades.Pedido();
            //Así obtenemos el pedido nuevo del dataset, indicamos la primer fila de la agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsCliente.PEDIDOSRow rowPedido = dsCliente.PEDIDOS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsCliente.PEDIDOSRow;
            //Creamos el objeto pedido para verificar si existe
            pedido.Codigo = Convert.ToInt64(rowPedido.PED_CODIGO);
            pedido.Numero = rowPedido.PED_NUMERO;
            
            if (EsPedido(pedido)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            DAL.PedidoDAL.Insertar(dsCliente);
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
        public static void ObtenerPedido(DateTime fecha, Data.dsPlanMensual dsPlanMensual)
        {
            DAL.PedidoDAL.ObtenerPedido(fecha, dsPlanMensual);
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
