using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class RepuestoBLL
    {
        public static long Insertar(Entidades.Repuesto repuesto)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsRepuesto(repuesto)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.RepuestoDAL.Insertar(repuesto);
        }

        public static void Eliminar(long codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.RepuestoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.RepuestoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.Repuesto repuesto)
        {
            DAL.RepuestoDAL.Actualizar(repuesto);
        }

        public static bool EsRepuesto(Entidades.Repuesto repuesto)
        {
            return DAL.RepuestoDAL.EsRepuesto(repuesto);
        }

        public static void ObtenerTodos(string nombre, Data.dsMantenimiento dsMantenimiento)
        {
            DAL.RepuestoDAL.ObtenerRepuesto(nombre, dsMantenimiento);
        }

        public static void ObtenerTodos(Data.dsMantenimiento dsMantenimiento)
        {
            DAL.RepuestoDAL.ObtenerRepuesto(dsMantenimiento);
        }

        //Repuestos
        //public static void ObtenerTodos(Data.dsMaquina dsMaquina)
        //{
        //    DAL.EstadoMaquinaDAL.ObtenerEstadosMaquina(dsMaquina);
        //}

        public static void ObtenerTodos(DataTable dtRepuesto)
        {
            DAL.RepuestoDAL.ObtenerRepuesto(dtRepuesto);
        }
    }
}
