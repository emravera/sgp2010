using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class EstadoEmpleadoBLL
    {
        public static readonly int EstadoDeBaja = 3;
        
        public static long Insertar(Entidades.EstadoEmpleado estadoEmpleado)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsEstadoEmpleado(estadoEmpleado)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.EstadoEmpleadoDAL.Insertar(estadoEmpleado);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.EstadoEmpleadoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.EstadoEmpleadoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.EstadoEmpleado estadoEmpleado)
        {
            DAL.EstadoEmpleadoDAL.Actualizar(estadoEmpleado);
        }

        public static bool EsEstadoEmpleado(Entidades.EstadoEmpleado estadoEmpleado)
        {
            return DAL.EstadoEmpleadoDAL.EsEstadoEmpleado(estadoEmpleado);
        }

        public static void ObtenerTodos(string nombre, Data.dsEstadoEmpleado dsEstadoEmpleado)
        {
            DAL.EstadoEmpleadoDAL.ObtenerEstadosEmpleado(nombre, dsEstadoEmpleado);
        }

        public static void ObtenerTodos(Data.dsEstadoEmpleado dsEstadoEmpleado)
        {
            DAL.EstadoEmpleadoDAL.ObtenerEstadosEmpleado(dsEstadoEmpleado);
        }

        public static void ObtenerTodos(Data.dsEmpleado dsEmpleado)
        {
            DAL.EstadoEmpleadoDAL.ObtenerEstadosEmpleado(dsEmpleado);
        }

    }
}
