using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class LocalidadBLL
    {
        public static void Insertar(Entidades.Localidad localidad)
        {
            if (!EsLocalidad(localidad)) { DAL.LocalidadDAL.Insertar(localidad); }
            else { throw new Entidades.Excepciones.ElementoExistenteException(); }
        }

        public static void Actualizar(Entidades.Localidad localidad)
        {
            if (!EsLocalidad(localidad)) { DAL.LocalidadDAL.Actualizar(localidad);}
            else { throw new Entidades.Excepciones.ElementoExistenteException(); }
        }

        public static void Eliminar(int codigo)
        {
            if (DAL.LocalidadDAL.PuedeEliminarse(codigo)) { DAL.LocalidadDAL.Eliminar(codigo); }
            else { throw new Entidades.Excepciones.ElementoEnTransaccionException(); }
        }       

        /// <summary>
        /// Obtiene todas las localidades que coincidan con el nombre y/o el código de provincia, las
        /// carga en un DataTable del tipo Localidades. Si se pasan ambos filtros con valor null
        /// devuelve todas las localidades.
        /// </summary>
        /// <param name="nombre">El nombre de las localidades a buscar.</param>
        /// <param name="codProvincia">El código de la provincia de las localidades a buscar.</param>
        /// <param name="dtLocalidades">El DataTable del tipo Localidades.</param>
        public static void ObtenerLocalidades(object nombre, object codProvincia, DataTable dtLocalidades)
        {
            object codigo = null;
            if (codProvincia != null && Convert.ToInt32(codProvincia.ToString()) > 0) { codigo = codProvincia; }
            DAL.LocalidadDAL.ObtenerLocalidades(nombre, codigo, dtLocalidades);
        }

        public static void ObtenerLocalidades(int codigoProvincia, DataTable dtLocalidades)
        {
            DAL.LocalidadDAL.ObtenerLocalidades(codigoProvincia, dtLocalidades);
        }

        public static void ObtenerLocalidades(DataTable dtLocalidades)
        {
            DAL.LocalidadDAL.ObtenerLocalidades(dtLocalidades);
        }

        /// <summary>
        /// Determina si una localidad existe en base a su nombre y provincia.
        /// </summary>
        /// <param name="localidad">El objeto localidad con el nombre y código de la provincia.</param>
        /// <returns>true si existe, false en caso contrario.</returns>        
        public static bool EsLocalidad(Entidades.Localidad localidad)
        {
            return DAL.LocalidadDAL.EsLocalidad(localidad);
        }
    }
}
