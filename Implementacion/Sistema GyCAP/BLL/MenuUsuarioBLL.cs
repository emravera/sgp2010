using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class MenuUsuarioBLL
    {
        public static void ObtenerTodos(Data.dsSeguridad ds)
        {
            DAL.MenuUsuarioDAL.ObtenerTodos(ds);
        }

        //public static void ObtenerTodos(System.Data.DataTable dt)
        //{
        //    DAL.MenuUsuarioDAL.ObtenerTodos(dt);
        //}

        //public static void ObtenerTodos(int usuario, Data.dsSeguridad ds)
        //{
        //    DAL.MenuUsuarioDAL.ObtenerTodos(usuario, ds);
        //}

        public static int Insertar(Entidades.MenuUsuario mu)
        {
            //Como no existe lo creamos
            return DAL.MenuUsuarioDAL.Insertar(mu);
        }

        public static void Eliminar(int codigoUsuario)
        {
            //try
            //{
            DAL.MenuUsuarioDAL.Eliminar(codigoUsuario);
            //}
            //catch (Exception)
            //{
            //    //No puede eliminarse, lanzamos nuestra excepción
            //    throw new Entidades.Excepciones.ElementoEnTransaccionException();
            //}

        }
    }
}
