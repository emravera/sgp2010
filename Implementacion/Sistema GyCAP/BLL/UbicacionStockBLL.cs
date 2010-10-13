using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class UbicacionStockBLL
    {
        public static readonly int Activo = 1;
        public static readonly int Inactivo = 0;
        
        public static void ObtenerUbicacionesStock(DataTable dtUbicacionStock)
        {
            DAL.UbicacionStockDAL.ObtenerUbicacionesStock(dtUbicacionStock);
        }

        public static void Insertar(Entidades.UbicacionStock ubicacion)
        {
            //Controlar si ya existe ???? - gonzalo
            DAL.UbicacionStockDAL.Insertar(ubicacion);
        }
        
        public static void Eliminar(int numeroUbicacionStock)
        {
            if (DAL.UbicacionStockDAL.PuedeEliminarse(numeroUbicacionStock))
            {
                DAL.UbicacionStockDAL.Eliminar(numeroUbicacionStock);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }
    }
}
