using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class ProvinciaBLL
    {
        public static int Insertar(Entidades.Provincia provincia)
        {
            if (DAL.ProvinciaDAL.EsProvinciaNuevo(provincia)) { return DAL.ProvinciaDAL.Insertar(provincia); }
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
            if (DAL.ProvinciaDAL.EsProvinciaActualizar(provincia) == false ) { DAL.ProvinciaDAL.Actualizar(provincia); }
            else { throw new Entidades.Excepciones.ElementoExistenteException(); }            
        }

        public static void ObtenerProvincias(DataTable dtProvincia)
        {
            DAL.ProvinciaDAL.ObtenerProvincias(dtProvincia);
        }       
    }
}
