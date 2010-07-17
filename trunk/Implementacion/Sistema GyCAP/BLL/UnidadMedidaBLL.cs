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

        //Guardado de Datos
        public static int Insertar(Entidades.UnidadMedida unidadMedida)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsUnidadMedida(unidadMedida)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.UnidadMedidaDAL.Insertar(unidadMedida);
        }

        //Metodo que valida que no se este guardando algo que ya existe
        public static bool EsUnidadMedida(Entidades.UnidadMedida unidadMedida)
        {
            return DAL.UnidadMedidaDAL.esUnidadMedida(unidadMedida);
        }

        //Actualización de los datos
        public static void Actualizar(Entidades.UnidadMedida unidadMedida)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsUnidadMedida(unidadMedida)) throw new Entidades.Excepciones.ElementoExistenteException();
            else DAL.UnidadMedidaDAL.Actualizar(unidadMedida);
        }

    }
}
