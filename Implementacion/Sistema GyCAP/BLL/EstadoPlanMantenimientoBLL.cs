using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class EstadoPlanMantenimientoBLL
    {
        public static long Insertar(Entidades.EstadoPlanMantenimiento estadoPlan)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsEstadoPlanMantenimiento(estadoPlan)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.EstadoPlanMantenimientoDAL.Insertar(estadoPlan);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.EstadoPlanMantenimientoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.EstadoPlanMantenimientoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.EstadoPlanMantenimiento estadoPlan)
        {
            DAL.EstadoPlanMantenimientoDAL.Actualizar(estadoPlan);
        }

        public static bool EsEstadoPlanMantenimiento(Entidades.EstadoPlanMantenimiento estadoPlan)
        {
            return DAL.EstadoPlanMantenimientoDAL.EsEstadoPlanMantenimiento(estadoPlan);
        }

        //public static void ObtenerTodos(Data.dsMantenimiento ds)
        //{
        //    DAL.EstadoPlanMantenimientoDAL.ObtenerEstadosPlanMantenimiento(ds);
        //}

        public static void ObtenerTodos(string nombre, Data.dsMantenimiento ds)
        {
            DAL.EstadoPlanMantenimientoDAL.ObtenerEstadosPlanMantenimiento (nombre, ds);
        }

        public static void ObtenerTodos(DataTable dtEstadoPlan)
        {
            DAL.EstadoPlanMantenimientoDAL.ObtenerEstadosPlanMantenimiento(dtEstadoPlan);
        }

        public static void ObtenerTodos(Data.dsMantenimiento ds)
        {
            DAL.EstadoPlanMantenimientoDAL.ObtenerEstadosPlanMantenimiento(ds);
        }
    }
}
