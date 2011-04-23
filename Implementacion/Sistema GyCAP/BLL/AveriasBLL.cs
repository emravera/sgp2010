using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class AveriasBLL
    {
        public static long Insertar(Entidades.Averia averia)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsAveria(averia)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.AveriasDAL.Insertar(averia);
        }

        public static void Eliminar(long codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.AveriasDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.AveriasDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.Averia averia)
        {
            DAL.AveriasDAL.Actualizar(averia);
        }

        public static bool EsAveria(Entidades.Averia averia)
        {
            return DAL.AveriasDAL.EsAveria(averia);
        }

        public static void ObtenerTodos(string nombre, int codNivelCriticidad, Data.dsMantenimiento dsMantenimiento)
        {
            DAL.AveriasDAL.ObtenerAverias(nombre, codNivelCriticidad, dsMantenimiento);
        }

        public static void ObtenerTodos(Data.dsMantenimiento dsMantenimiento)
        {
            DAL.AveriasDAL.ObtenerAverias(dsMantenimiento);
        }

        public static void ObtenerTodos(DataTable dtAveria)
        {
            DAL.AveriasDAL.ObtenerAverias(dtAveria);
        }
    }
}
