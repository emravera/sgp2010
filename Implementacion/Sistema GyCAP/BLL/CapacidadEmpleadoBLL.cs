using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class CapacidadEmpleadoBLL
    {
        public static int Insertar(Entidades.CapacidadEmpleado capacidadEmpleado)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsCapacidadEmpleado(capacidadEmpleado)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.CapacidadEmpleadoDAL.Insertar(capacidadEmpleado);
        }

        public static void Eliminar(int codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.CapacidadEmpleadoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.CapacidadEmpleadoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        public static void Actualizar(Entidades.CapacidadEmpleado capacidadEmpleado)
        {
            if (EsCapacidadEmpleado(capacidadEmpleado)) throw new Entidades.Excepciones.ElementoExistenteException();
            else DAL.CapacidadEmpleadoDAL.Actualizar(capacidadEmpleado);
        }

        public static bool EsCapacidadEmpleado(Entidades.CapacidadEmpleado capacidadEmpleado)
        {
            return DAL.CapacidadEmpleadoDAL.EsCapacidadEmpleado(capacidadEmpleado);
        }

        public static void ObtenerTodos(string nombre, Data.dsEmpleado dsCapacidadEmpleado)
        {
            DAL.CapacidadEmpleadoDAL.ObtenerCapacidadEmpleado(nombre, dsCapacidadEmpleado);
        }

        public static void ObtenerTodos(Data.dsEmpleado dsEmpleado)
        {
            DAL.CapacidadEmpleadoDAL.ObtenerCapacidadEmpleado(dsEmpleado);
        }

        public static void ObtenerTodos(DataTable dtCapacidadEmpleado)
        {
            DAL.CapacidadEmpleadoDAL.ObtenerCapacidadEmpleado(dtCapacidadEmpleado);
        }

        public static void ObtenerCapacidadPorEmpleado(int E_CODIGO, Data.dsEmpleado dsEmpleado)
        {
            DAL.CapacidadEmpleadoDAL.ObtenerCapacidadPorEmpleado(E_CODIGO, dsEmpleado);
        }

        public static void ObtenerCapacidadPorEmpleado(Data.dsEmpleado dsEmpleado)
        {
            DAL.CapacidadEmpleadoDAL.ObtenerCapacidadPorEmpleado(dsEmpleado);
        }
    }
}
