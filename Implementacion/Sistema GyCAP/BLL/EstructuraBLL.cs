using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class EstructuraBLL
    {
        public static readonly int EstructuraActiva = 1;
        public static readonly int EstructuraInactiva = 0;
        
        public static void Insertar(Data.dsEstructura ds)
        {
            DAL.EstructuraDAL.Insertar(ds);
        }
        
        public static void Eliminar(int codigoEstructura)
        {
            //De momento solo chequeamos si está activa o no, luego agregaremos las demás condiciones - gonzalo
            if(!DAL.EstructuraDAL.EsEstructuraActiva(codigoEstructura))
            {
                DAL.EstructuraDAL.Eliminar(codigoEstructura);
            }
            else
            {
                throw new Entidades.Excepciones.ElementoActivoException();
            }
            
            
            /*if(DAL.EstructuraDAL.PuedeEliminarse(codigoEstructura))
            {
                DAL.EstructuraDAL.Eliminar(codigoEstructura);
            }
            else
            {
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }*/
        }

        public static void Actualizar(Data.dsEstructura ds)
        {
            DAL.EstructuraDAL.Actualizar(ds);
        }
        
        public static void ObtenerEstructuras(object nombre, object codPlano, object fechaCreacion, object codCocina, object legResponsable, object activoSiNo, Data.dsEstructura ds)
        {
            DAL.EstructuraDAL.ObtenerEstructuras(nombre, codPlano, fechaCreacion, codCocina, legResponsable, activoSiNo, ds);
        }
        
        
    }
}
