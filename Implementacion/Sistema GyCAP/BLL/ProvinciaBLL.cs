using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class ProvinciaBLL
    {
        public static int Insertar(string nombre)
        {
            if (!EsProvincia(nombre)) { return DAL.ProvinciaDAL.Insertar(nombre); }
            else { throw new Entidades.Excepciones.ElementoExistenteException(); }
        }

        public static void Eliminar(int codigo)
        {
            if (DAL.ProvinciaDAL.PuedeEliminarse(codigo))
            {
                DAL.ProvinciaDAL.Eliminar(codigo);
            }
            else { throw new Entidades.Excepciones.ElementoEnTransaccionException(); }
        }

        public static void Actualizar(Entidades.Provincia provincia)
        {
            if (!EsProvincia(provincia.Nombre)) { DAL.ProvinciaDAL.Actualizar(provincia); }
            else { throw new Entidades.Excepciones.ElementoExistenteException(); }            
        }

        public static void ObtenerProvincias(DataTable dtProvincia)
        {
            DAL.ProvinciaDAL.ObtenerProvincias(dtProvincia);
        }

        public static bool EsProvincia(string nombre)
        {
            return DAL.ProvinciaDAL.EsProvincia(nombre);
        }
    }
}
