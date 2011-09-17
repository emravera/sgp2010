using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class EstadoUsuarioBLL
    {
        public static readonly int EstadoActivo = 1;
        public static readonly int EstadoInactivo = 2;

        public static long Insertar(Entidades.EstadoUsuario estadoUsuario)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsEstadoUsuario(estadoUsuario)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.EstadoUsuarioDAL.Insertar(estadoUsuario);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.EstadoUsuarioDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.EstadoUsuarioDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.EstadoUsuario estadoUsuario)
        {
            DAL.EstadoUsuarioDAL.Actualizar(estadoUsuario);
        }

        public static bool EsEstadoUsuario(Entidades.EstadoUsuario estadoUsuario)
        {
            return DAL.EstadoUsuarioDAL.EsEstadoUsuario(estadoUsuario);
        }

        public static void ObtenerTodos(string nombre, Data.dsSeguridad dsSeguridad)
        {
            DAL.EstadoUsuarioDAL.ObtenerEstadosUsuario(nombre, dsSeguridad);
        }

        public static void ObtenerTodos(Data.dsSeguridad dsSeguridad)
        {
            DAL.EstadoUsuarioDAL.ObtenerEstadosUsuario(dsSeguridad);
        }

        public static void ObtenerTodos(DataTable dtEstadoUsuario)
        {
            DAL.EstadoUsuarioDAL.ObtenerEstadosUsuario(dtEstadoUsuario);
        }
    }
}
