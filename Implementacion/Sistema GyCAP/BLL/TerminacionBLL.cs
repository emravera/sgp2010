using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class TerminacionBLL
    {
        public static int Insertar(Entidades.Terminacion terminacion)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsTerminacion(terminacion)) throw new Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.TerminacionDAL.Insertar(terminacion);
        }

        public static void Eliminar(Entidades.Terminacion terminacion)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.TerminacionDAL.PuedeEliminarse(terminacion))
            {
                //Puede eliminarse
                DAL.TerminacionDAL.Eliminar(terminacion);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new BLL.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.Terminacion terminacion)
        {
            DAL.TerminacionDAL.Actualizar(terminacion);
        }

        public static bool EsTerminacion(Entidades.Terminacion terminacion)
        {
            return DAL.TerminacionDAL.EsTeminacion(terminacion);
        }

        public static Data.dsTerminacion ObtenerTodos(string nombre)
        {
            Data.dsTerminacion dsTerminacion = new GyCAP.Data.dsTerminacion();
            DAL.TerminacionDAL.ObtenerTerminacion(nombre, dsTerminacion);
            return dsTerminacion;
        }
    }
}
