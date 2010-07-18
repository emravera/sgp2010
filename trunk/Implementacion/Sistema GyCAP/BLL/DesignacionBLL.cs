using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class DesignacionBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerTodos(string nombre, int idMarca, Data.dsDesignacion ds)
        {
            DAL.DesignacionDAL.ObtenerDesignacion(nombre, idMarca, ds);
        }

        //Eliminacion
        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.DesignacionDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.DesignacionDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }

        //Guardado de Datos
        public static int Insertar(Entidades.Designacion desig)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsDesignacion(desig)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.DesignacionDAL.Insertar(desig);
        }

        //Metodo que valida que no se este guardando algo que ya existe
        public static bool EsDesignacion(Entidades.Designacion desig)
        {
            return DAL.DesignacionDAL.esDesignacion(desig);
        }
        //Actualización de los datos
        public static void Actualizar(Entidades.Designacion desig)
        {
           DAL.DesignacionDAL.Actualizar(desig);
        }

    }
}
