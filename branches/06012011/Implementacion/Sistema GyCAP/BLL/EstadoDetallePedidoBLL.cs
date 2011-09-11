using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class EstadoDetallePedidoBLL
    {
        public static readonly int EstadoEnCurso = 2;
        public static readonly int EstadoFinalizado = 5;
        
        public static long Insertar(Entidades.EstadoDetallePedido estadoDetallePedido)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsEstadoDetallePedido(estadoDetallePedido)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.EstadoDetallePedidoDAL.Insertar(estadoDetallePedido);
        }

        public static void Eliminar(long codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.EstadoDetallePedidoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.EstadoDetallePedidoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }

        public static void Actualizar(Entidades.EstadoDetallePedido estadoDetallePedido)
        {
            DAL.EstadoDetallePedidoDAL.Actualizar(estadoDetallePedido);
        }

        public static bool EsEstadoDetallePedido(Entidades.EstadoDetallePedido estadoDetallePedido)
        {
            return DAL.EstadoDetallePedidoDAL.EsEstadoDetallePedido(estadoDetallePedido);
        }       

        public static void ObtenerTodos(string nombre, DataTable dtEstadoDetallePedido)
        {
            DAL.EstadoDetallePedidoDAL.ObtenerEstadosDetallePedido(nombre, dtEstadoDetallePedido);
        }

        public static void ObtenerTodos(DataTable dtEstadoDetallePedido)
        {
            DAL.EstadoDetallePedidoDAL.ObtenerEstadosDetallePedido(dtEstadoDetallePedido);
        }
    }
}
