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
            if (DAL.UbicacionStockDAL.EsUbicacionStock(ubicacion.Codigo, ubicacion.Nombre)) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            DAL.UbicacionStockDAL.Insertar(ubicacion);
        }
        
        public static void Eliminar(int numeroUbicacionStock)
        {
            if (DAL.UbicacionStockDAL.PuedeEliminarse(numeroUbicacionStock)) { DAL.UbicacionStockDAL.Eliminar(numeroUbicacionStock); }
            else { throw new Entidades.Excepciones.ElementoEnTransaccionException(); }
        }

        public static void ActualizarCantidadesStock(int numeroUbicacion, decimal cantidadReal, decimal cantidadVirtual)
        {
            DAL.UbicacionStockDAL.ActualizarCantidadesStock(numeroUbicacion, cantidadReal, cantidadVirtual, null);
        }
    }
}
