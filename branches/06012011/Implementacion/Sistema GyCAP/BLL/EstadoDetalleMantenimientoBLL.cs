using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.BLL
{
    public class EstadoDetalleMantenimientoBLL
    {
        public static long Insertar(Entidades.EstadoDetalleMantenimiento estadoDetalleMantenimiento)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsEstadoDetalleMantenimiento(estadoDetalleMantenimiento)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.EstadoDetalleMantenimientoDAL.Insertar(estadoDetalleMantenimiento);
        }

        public static void Eliminar(long codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.EstadoDetalleMantenimientoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.EstadoDetalleMantenimientoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }

        public static void Actualizar(Entidades.EstadoDetalleMantenimiento estadoDetalleMantenimiento)
        {
            DAL.EstadoDetalleMantenimientoDAL.Actualizar(estadoDetalleMantenimiento);
        }

        public static bool EsEstadoDetalleMantenimiento(Entidades.EstadoDetalleMantenimiento estadoDetalleMantenimiento)
        {
            return DAL.EstadoDetalleMantenimientoDAL.EsEstadoDetalleMantenimiento(estadoDetalleMantenimiento);
        }

        public static void ObtenerTodos(Data.dsMantenimiento ds)
        {
            DAL.EstadoDetalleMantenimientoDAL.ObtenerEstadosDetalleMantenimiento(ds);
        }

        public static void ObtenerTodos(string nombre, Data.dsMantenimiento ds)
        {
            DAL.EstadoDetalleMantenimientoDAL.ObtenerEstadosDetalleMantenimiento(nombre, ds);
        }

        public static void ObtenerTodos(DataTable dtEstadoDetalleMantenimiento)
        {
            DAL.EstadoDetalleMantenimientoDAL.ObtenerEstadosDetalleMantenimiento(dtEstadoDetalleMantenimiento);
        }

        //public static void ObtenerTodos(Data.dsCliente ds)
        //{
        //    DAL.EstadoDetallePedidoDAL.ObtenerEstadosDetallePedido(ds);
        //}

    }
}
