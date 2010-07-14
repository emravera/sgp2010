using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{

    public class UnidadMedidaBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerTodos(string nombre, Data.dsUnidadMedida ds)
        {
            DAL.UnidadMedidaDAL.ObtenerUnidad(nombre,ds);
        }
        public static void ObtenerTodos(int tipoUnidad, Data.dsUnidadMedida ds)
        {
            DAL.UnidadMedidaDAL.ObtenerUnidad(tipoUnidad, ds);
        }
        public static void ObtenerTodos(Data.dsUnidadMedida ds)
        {
            DAL.UnidadMedidaDAL.ObtenerUnidad(ds);
        }

        //Eliminacion
        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.UnidadMedidaDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.UnidadMedidaDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }

    }
}
