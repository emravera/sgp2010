using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class TipoUnidadMedidaBLL
    {
        public static int Insertar(Entidades.TipoUnidadMedida tipoUnidadMedida)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsTipoUnidadMedida(tipoUnidadMedida)) throw new Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.TipoUnidadMedidaDAL.Insertar(tipoUnidadMedida);
        }

        public static void Eliminar(Entidades.TipoUnidadMedida tipoUnidadMedida)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.TipoUnidadMedidaDAL.PuedeEliminarse(tipoUnidadMedida))
            {
                //Puede eliminarse
                DAL.TipoUnidadMedidaDAL.Eliminar(tipoUnidadMedida);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new BLL.Excepciones.ElementoEnTransaccionException();
            }

        }

        public static void Actualizar(Entidades.TipoUnidadMedida tipoUnidadMedida)
        {
            DAL.TipoUnidadMedidaDAL.Actualizar(tipoUnidadMedida);
        }

        public static bool EsTipoUnidadMedida(Entidades.TipoUnidadMedida tipoUnidadMedida)
        {
            return DAL.TipoUnidadMedidaDAL.EsTipoUnidadMedida(tipoUnidadMedida);
        }

        public static void ObtenerTodos(string nombre, Data.dsUnidadMedida ds)
        {
            DAL.TipoUnidadMedidaDAL.ObtenerTipoUnidadMedida(nombre, ds);
        }
    }
}
