using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class TipoRepuestoBLL
    {
        public static long Insertar(Entidades.TipoRepuesto tipoRepuesto)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsTipoRepuesto(tipoRepuesto)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.TipoRepuestoDAL.Insertar(tipoRepuesto);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.TipoRepuestoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.TipoRepuestoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.TipoRepuesto tipoRepuesto)
        {
            DAL.TipoRepuestoDAL.Actualizar(tipoRepuesto);
        }

        public static bool EsTipoRepuesto(Entidades.TipoRepuesto tipoRepuesto)
        {
            return DAL.TipoRepuestoDAL.EsTipoRepuesto(tipoRepuesto);
        }

        public static void ObtenerTodos(string nombre, Data.dsMantenimiento dsMantenimiento)
        {
            DAL.TipoRepuestoDAL.ObtenerTipoRepuesto(nombre, dsMantenimiento);
        }

        public static void ObtenerTodos(Data.dsMantenimiento dsMantenimiento)
        {
            DAL.TipoRepuestoDAL.ObtenerTipoRepuesto(dsMantenimiento);
        }

        //Repuestos
        //public static void ObtenerTodos(Data.dsMaquina dsMaquina)
        //{
        //    DAL.EstadoMaquinaDAL.ObtenerEstadosMaquina(dsMaquina);
        //}

        public static void ObtenerTodos(DataTable dtTipoRepuesto)
        {
            DAL.TipoRepuestoDAL.ObtenerTipoRepuesto(dtTipoRepuesto);
        }
    }
}
