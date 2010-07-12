using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class ColorBLL
    {
        public static int Insertar(Entidades.Color color)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsColor(color)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.ColorDAL.Insertar(color);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.ColorDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.ColorDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }

        public static void Actualizar(Entidades.Color color)
        {
            DAL.ColorDAL.Actualizar(color);
        }

        public static bool EsColor(Entidades.Color color)
        {
            return DAL.ColorDAL.esColor(color);
        }

        public static void ObtenerTodos(string nombre, Data.dsColor ds)
        {
            DAL.ColorDAL.ObtenerColor(nombre, ds);
        }

        
    }
}
