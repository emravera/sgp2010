using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class EstadoPedidoBLL
    {
        public static long Insertar(Entidades.EstadoPedido estadoPedido)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsEstadoPedido(estadoPedido)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.EstadoPedidoDAL.Insertar(estadoPedido);
        }

        public static void Eliminar(long codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.EstadoPedidoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.EstadoPedidoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }

        public static void Actualizar(Entidades.EstadoPedido estadoPedido)
        {
            DAL.EstadoPedidoDAL.Actualizar(estadoPedido);
        }

        public static bool EsEstadoPedido(Entidades.EstadoPedido estadoPedido)
        {
            return DAL.EstadoPedidoDAL.EsEstadoPedido(estadoPedido);
        }

        public static void ObtenerTodos(Data.dsEstadoPedidos ds)
        {
            DAL.EstadoPedidoDAL.ObtenerEstadosPedido(ds);
        }

        public static void ObtenerTodos(string nombre, Data.dsEstadoPedidos ds)
        {
            DAL.EstadoPedidoDAL.ObtenerEstadosPedido(nombre, ds);
        }

        public static void ObtenerTodos(DataTable dtEstadoPedido)
        {
            //DAL.EstadoPedidoDAL.ObtenerEstadosDetallePedido(dtEstadoPedido);
        }

        public static void ObtenerTodos(Data.dsCliente ds)
        {
            DAL.EstadoPedidoDAL.ObtenerEstadosPedido(ds);
        }
    }
}
