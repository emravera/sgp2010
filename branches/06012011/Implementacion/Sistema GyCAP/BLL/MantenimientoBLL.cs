using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class MantenimientoBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerTodos(object nombre, int idEstado, string cadFabricantes, string cadModelo, Data.dsMaquina ds)
        {
            //DAL.MaquinaDAL.ObtenerMaquinas(nombre, idEstado, cadFabricantes, cadModelo, ds);
        }

        //Eliminacion
        public static void Eliminar(long codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.MantenimientoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.MantenimientoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }

        //Guardado de Datos
        public static int Insertar(Entidades.Mantenimiento mantenimiento)
        {
            //Si existe lanzamos la excepción correspondiente
            if (EsMantenimiento(mantenimiento)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.MantenimientoDAL.Insertar(mantenimiento);
        }

        //Metodo que valida que no se este guardando algo que ya existe
        public static bool EsMantenimiento(Entidades.Mantenimiento mantenimiento)
        {
            return DAL.MantenimientoDAL.esMantenimiento(mantenimiento);
        }
        //Actualización de los datos
        public static void Actualizar(Entidades.Mantenimiento mantenimiento)
        {
            DAL.MantenimientoDAL.Actualizar(mantenimiento);
        }

        /// <summary>
        /// Obtiene todas las máquinas sin filtrar, los carga en una DataTable del tipo máquina.
        /// </summary>
        /// <param name="dtMaquina">La tabla donde cargar los datos.</param>
        public static void ObtenerMantenimientos(DataTable dtMantenimiento)
        {
            DAL.MantenimientoDAL.ObtenerMantenimientos(dtMantenimiento);
        }

        public static void ObtenerMantenimientos(Data.dsPlanMantenimiento dsPlanMantenimiento)
        {
            DAL.MantenimientoDAL.ObtenerMantenimientos(dsPlanMantenimiento);
        }
    }
}
