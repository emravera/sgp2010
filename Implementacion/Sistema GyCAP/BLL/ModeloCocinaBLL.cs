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

        public static void Eliminar(Entidades.ModeloCocina modeloCocina)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.ModeloCocinaDAL.PuedeEliminarse(modeloCocina))
            {
                //Puede eliminarse
                DAL.ModeloCocinaDAL.Eliminar(modeloCocina);
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

        public static Data.dsModeloCocina ObtenerTodos(string nombre)
        {
            Data.dsModeloCocina dsModeloCocina = new GyCAP.Data.dsModeloCocina();
            DAL.ModeloCocinaDAL.ObtenerModeloCocina(nombre, dsModeloCocina);
            return dsModeloCocina;
        } 
    }
}
