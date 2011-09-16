using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class RolBLL
    {
        public static readonly int EstadoDeBaja = 2;

        public static long Insertar(Entidades.Rol rol)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsRol(rol)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.RolDAL.Insertar(rol);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.RolDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.RolDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.Rol rol)
        {
            DAL.RolDAL.Actualizar(rol);
        }

        public static bool EsRol(Entidades.Rol rol)
        {
            return DAL.RolDAL.EsRol(rol);
        }

        public static void ObtenerTodos(string nombre, Data.dsSeguridad dsSeguridad)
        {
            DAL.RolDAL.ObtenerRoles(nombre, dsSeguridad);
        }

        public static void ObtenerTodos(Data.dsSeguridad dsSeguridad)
        {
            DAL.RolDAL.ObtenerRoles(dsSeguridad);
        }

        public static void ObtenerTodos(DataTable dtRol)
        {
            DAL.RolDAL.ObtenerRoles(dtRol);
        }
    }
}
