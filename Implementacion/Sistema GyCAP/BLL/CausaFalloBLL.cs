using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class CausaFalloBLL
    {
        public static int Insertar(Entidades.CausaFallo causaFallo)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsCausaFallo(causaFallo)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.CausaFalloDAL.Insertar(causaFallo);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.CausaFalloDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.CausaFalloDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.CausaFallo causaFallo)
        {
            DAL.CausaFalloDAL.Actualizar(causaFallo);
        }

        public static bool EsCausaFallo(Entidades.CausaFallo causaFallo)
        {
            return DAL.CausaFalloDAL.EsCausaFallo(causaFallo);
        }

        public static void ObtenerTodos(string nombre, string codigo, Data.dsMantenimiento dsMantenimiento)
        {
            DAL.CausaFalloDAL.ObtenerCausaFallo(nombre, codigo, dsMantenimiento);
        }

        public static void ObtenerTodos(Data.dsMantenimiento dsMantenimiento)
        {
            DAL.CausaFalloDAL.ObtenerCausaFallo(dsMantenimiento);
        }

        public static void ObtenerTodos(DataTable dtCausaFallo)
        {
            DAL.CausaFalloDAL.ObtenerCausaFallo(dtCausaFallo);
        }
    }
}
