using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class TerminacionBLL
    {
        public static long Insertar(Entidades.Terminacion terminacion)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsTerminacion(terminacion)) throw new Entidades.Excepciones.ElementoExistenteException ();
            //Como no existe lo creamos
            return DAL.TerminacionDAL.Insertar(terminacion);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.TerminacionDAL.PuedeEliminarse(codigo ))
            {
                //Puede eliminarse
                DAL.TerminacionDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.Terminacion terminacion)
        {
            if (EsTerminacion(terminacion)) throw new Entidades.Excepciones.ElementoExistenteException();
            DAL.TerminacionDAL.Actualizar(terminacion);
        }

        public static bool EsTerminacion(Entidades.Terminacion terminacion)
        {
            return DAL.TerminacionDAL.EsTeminacion(terminacion);
        }

        public static void ObtenerTodos(string nombre, System.Data.DataTable dt)
        {
            DAL.TerminacionDAL.ObtenerTerminacion(nombre, dt);
        }
        
        public static void ObtenerTodos(string nombre, Data.dsCocina dsTerminacion)
        {
            DAL.TerminacionDAL.ObtenerTerminacion(nombre, dsTerminacion);            
        }
    }
}
