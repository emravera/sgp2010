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
        
        public static void ObtenerEstructuras(object nombre, object codPlano, object fechaCreacion, object codCocina, object codResponsable, object activoSiNo, Data.dsEstructura ds)
        {
            object plano = null, cocina = null, responsable = null, activo = null;
            if (codPlano != null && Convert.ToInt32(codPlano.ToString()) > 0) { plano = codPlano; }
            if (codCocina != null && Convert.ToInt32(codCocina.ToString()) > 0) { cocina = codCocina; }
            if (codResponsable != null && Convert.ToInt32(codResponsable.ToString()) > 0) { responsable = codResponsable; }
            if (activoSiNo != null && Convert.ToInt32(activoSiNo) > -1) { activo = activoSiNo; }
            DAL.EstructuraDAL.ObtenerEstructuras(nombre, plano, fechaCreacion, cocina, responsable, activo, ds);
        }

        public static void ObtenerEstructura(int codigoEstructura, Data.dsEstructura ds, bool detalle)
        {
            DAL.EstructuraDAL.ObtenerEstructura(codigoEstructura, ds, detalle);
        }
    }
}
