using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public abstract class UsuarioBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda (se debe sobrecargar)
        public static void ObtenerTodos(Data.dsSeguridad ds)
        {
            DAL.UsuarioDAL.ObtenerTodos(ds);
        }

        public static void ObtenerTodos(System.Data.DataTable dt)
        {
            DAL.UsuarioDAL.ObtenerTodos(dt);
        }

        public static void ObtenerTodos(string nombre, string estado, Data.dsSeguridad ds)
        {
            DAL.UsuarioDAL.ObtenerTodos(nombre, estado, ds);
        }

        public static long Insertar(Entidades.Usuario usuario)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsUsuario(usuario)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.UsuarioDAL.Insertar(usuario);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.UsuarioDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.UsuarioDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.Usuario usuario)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsUsuario(usuario)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            else DAL.UsuarioDAL.Actualizar(usuario);
        }

        public static bool EsUsuario(Entidades.Usuario usuario)
        {
            return DAL.UsuarioDAL.esUsuario(usuario);
        }
        
    }
}
