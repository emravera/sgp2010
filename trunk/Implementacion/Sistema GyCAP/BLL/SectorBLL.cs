using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class SectorBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerTodos(string nombre, string abrev, Data.dsSectorTrabajo ds)
        {
            DAL.SectorDAL.ObtenerSector(nombre, abrev,ds);
        }

        public static void ObtenerTodos(Data.dsEmpleado ds)
        {
            DAL.SectorDAL.ObtenerSector(ds);
        }

        //Eliminacion
        public static void Eliminar(int codigo)
        {
            //Se hacen las valiaciones de que no este referenciada en ninguna tabla
            bool val1 = DAL.SectorDAL.PuedeEliminarseValidacion1(codigo);
            bool val2 = DAL.SectorDAL.PuedeEliminarseValidacion2(codigo);
            bool val3 = DAL.SectorDAL.PuedeEliminarseValidacion3(codigo);
            //Revisamos que no esté en alguna transacción
            if (val1==true && val2==true && val3 == true)
            {
                //Puede eliminarse
                DAL.SectorDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }
        //Guardado de Datos
        public static int Insertar(Entidades.Sector sector)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsSector(sector)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.SectorDAL.Insertar(sector);
        }

        //Metodo que valida que no se este guardando algo que ya existe
        public static bool EsSector(Entidades.Sector sector)
        {
            return DAL.SectorDAL.esSector(sector);
        }
        //Actualización de los datos
        public static void Actualizar(Entidades.Sector sector)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsSector(sector)) throw new Entidades.Excepciones.ElementoExistenteException();
            else DAL.SectorDAL.Actualizar(sector);
        }


    }
}
