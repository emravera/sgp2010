using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class NivelCriticidadBLL
    {
        public static long Insertar(Entidades.NivelCriticidad nivelCriticidad)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsNivelCriticidad(nivelCriticidad)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.NivelCriticidadDAL.Insertar(nivelCriticidad);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.NivelCriticidadDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.NivelCriticidadDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.NivelCriticidad nivelCriticidad)
        {
            DAL.NivelCriticidadDAL.Actualizar(nivelCriticidad);
        }

        public static bool EsNivelCriticidad(Entidades.NivelCriticidad nivelCriticidad)
        {
            return DAL.NivelCriticidadDAL.EsNivelCriticidad(nivelCriticidad);
        }

        public static void ObtenerTodos(string nombre, Data.dsMantenimiento dsMantenimiento)
        {
            DAL.NivelCriticidadDAL.ObtenerNivelCriticidad(nombre, dsMantenimiento);
        }

        public static void ObtenerTodos(Data.dsMantenimiento dsMantenimiento)
        {
            DAL.NivelCriticidadDAL.ObtenerNivelCriticidad(dsMantenimiento);
        }
    }
}
