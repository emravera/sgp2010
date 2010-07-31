using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class PlanAnualBLL
    {
        //Metodos para la busqueda
        public static void ObtenerTodos(int anio, Data.dsPlanAnual ds)
        {
            DAL.PlanAnualDAL.ObtenerTodos(anio, ds);
        }
        public static void ObtenerTodos(Data.dsPlanAnual ds)
        {
            DAL.PlanAnualDAL.ObtenerTodos(ds);
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
