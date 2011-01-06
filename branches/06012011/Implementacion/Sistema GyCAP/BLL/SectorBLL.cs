using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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

        public static void ObtenerTodos(DataTable dtSectores)
        {
            DAL.SectorDAL.ObtenerSector(dtSectores);
        }

        //Eliminacion
        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.SectorDAL.PuedeEliminarse(codigo))
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
        public static int Insertar(Entidades.SectorTrabajo sector)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsSector(sector)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.SectorDAL.Insertar(sector);
        }

        //Metodo que valida que no se este guardando algo que ya existe
        public static bool EsSector(Entidades.SectorTrabajo sector)
        {
            return DAL.SectorDAL.esSector(sector);
        }
        //Actualización de los datos
        public static void Actualizar(Entidades.SectorTrabajo sector)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsSector(sector)) throw new Entidades.Excepciones.ElementoExistenteException();
            else DAL.SectorDAL.Actualizar(sector);
        }


    }
}
