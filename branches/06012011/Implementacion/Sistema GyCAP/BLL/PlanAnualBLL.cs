using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class PlanAnualBLL
    {
        //Metodos para la busqueda
        public static void ObtenerTodos(int anio, Data.dsPlanAnual ds)
        {
            DAL.PlanAnualDAL.ObtenerTodos(anio, ds);
        }        
        public static void ObtenerTodos(DataTable dtPlanAnual)
        {
            DAL.PlanAnualDAL.ObtenerTodos(dtPlanAnual);
        }
        //Metodo que se llama desde el formulario de plan mensual
        public static int ObtenerTodos(int anio, string mes)
        {
           return DAL.PlanAnualDAL.ObtenerCantidad(anio,mes);
        }

        //Insertar
        public static IList<Entidades.DetallePlanAnual> Insertar(Entidades.PlanAnual planAnual, IList<Entidades.DetallePlanAnual> detalle)
        {
            return DAL.PlanAnualDAL.Insertar(planAnual, detalle);
        }

        //Modificar
        public static void Modificar(Entidades.PlanAnual planAnual, IList<Entidades.DetallePlanAnual> detalle)
        {
            DAL.PlanAnualDAL.Actualizar(planAnual, detalle);
        }

        public static bool ValidarModificacion(Entidades.PlanAnual planAnual)
        {
            return DAL.PlanAnualDAL.Validar(planAnual);
        }

        //Eliminacion
        public static void Eliminar(int codigo)
        {
            DAL.PlanAnualDAL.Eliminar(codigo);
        }

        public static bool PuedeEliminarse(int codigo)
        {
            return DAL.PlanAnualDAL.PuedeEliminarse(codigo);
        }     



    }
}
