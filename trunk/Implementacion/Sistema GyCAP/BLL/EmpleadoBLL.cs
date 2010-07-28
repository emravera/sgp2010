using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class EmpleadoBLL : UsuarioBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerTodos(string buscarPor, object nombre, int idEstadoEmpleado,string cadSectores, Data.dsEmpleado ds)
        {
            DAL.EmpleadoDAL.ObtenerEmpleado(buscarPor, nombre, idEstadoEmpleado, cadSectores, ds);
        }

        //Eliminacion
        public static void Eliminar(long codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.EmpleadoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.EmpleadoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }

        //Guardado de Datos
        public static int Insertar(Entidades.Empleado empleado)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsEmpleado(empleado)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.EmpleadoDAL.Insertar(empleado);
        }

        //Metodo que valida que no se este guardando algo que ya existe
        public static bool EsEmpleado(Entidades.Empleado empleado)
        {
            return DAL.EmpleadoDAL.esEmpleado(empleado);
        }
        //Actualización de los datos
        public static void Actualizar(Entidades.Empleado empleado)
        {
            DAL.EmpleadoDAL.Actualizar(empleado);
        }
    }
}
