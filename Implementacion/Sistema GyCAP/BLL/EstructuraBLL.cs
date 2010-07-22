using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class EstructuraBLL
    {
        public static void Eliminar(int codigoEstructura)
        {
            if(DAL.EstructuraDAL.PuedeEliminarse(codigoEstructura))
            {
                DAL.EstructuraDAL.Eliminar(codigoEstructura);
            }
            else
            {
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void ObtenerEstructuras(object nombre, object codPlano, object fechaCreacion, object codCocina, object legResponsable, object activoSiNo, Data.dsEstructura ds)
        {
            DAL.EstructuraDAL.ObtenerEstructuras(nombre, codPlano, fechaCreacion, codCocina, legResponsable, activoSiNo, ds);
        }
        
        
    }
}
