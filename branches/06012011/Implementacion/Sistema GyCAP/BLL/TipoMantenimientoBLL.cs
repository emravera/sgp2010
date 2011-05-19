using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class TipoMantenimientoBLL
    {
        public static int Insertar(Entidades.TipoMantenimiento tipoMantenimiento)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsTipoMantenimiento(tipoMantenimiento)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.TipoMantenimientoDAL.Insertar(tipoMantenimiento);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.TipoMantenimientoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.TipoMantenimientoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.TipoMantenimiento tipoMantenimiento)
        {
            DAL.TipoMantenimientoDAL.Actualizar(tipoMantenimiento);
        }

        public static bool EsTipoMantenimiento(Entidades.TipoMantenimiento tipoMantenimiento)
        {
            return DAL.TipoMantenimientoDAL.EsTipoMantenimiento(tipoMantenimiento);
        }

        public static void ObtenerTodos(string nombre, Data.dsMantenimiento dsMantenimiento)
        {
            DAL.TipoMantenimientoDAL.ObtenerTipoMantenimiento(nombre, dsMantenimiento);
        }

        public static void ObtenerTodos(Data.dsMantenimiento dsMantenimiento)
        {
            DAL.TipoMantenimientoDAL.ObtenerTipoMantenimiento(dsMantenimiento);
        }

        public static void ObtenerTodos(Data.dsMaquina dsMaquina)
        {
            DAL.EstadoMaquinaDAL.ObtenerEstadosMaquina(dsMaquina);
        }

        public static void ObtenerTodos(DataTable dtTipoMantenimiento)
        {
            DAL.TipoMantenimientoDAL.ObtenerTipoMantenimiento(dtTipoMantenimiento);
        }
    }
}
