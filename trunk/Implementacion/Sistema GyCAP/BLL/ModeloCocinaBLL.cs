using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class ModeloCocinaBLL
    {
        public static int Insertar(Entidades.ModeloCocina modeloCocina)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsModeloCocina(modeloCocina)) throw new Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.ModeloCocinaDAL.Insertar(modeloCocina);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.ModeloCocinaDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.ModeloCocinaDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new BLL.Excepciones.ElementoEnTransaccionException();
            }

        }

        public static void Actualizar(Entidades.ModeloCocina modeloCocina)
        {
            DAL.ModeloCocinaDAL.Actualizar(modeloCocina);
        }

        public static bool EsModeloCocina(Entidades.ModeloCocina modeloCocina)
        {
            return DAL.ModeloCocinaDAL.EsModeloCocina(modeloCocina);
        }

        public static void ObtenerTodos(string nombre, Data.dsModeloCocina ds)
        {
            DAL.ModeloCocinaDAL.ObtenerModeloCocina(nombre, ds);
        } 
    }
}
