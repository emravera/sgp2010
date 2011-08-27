using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class PlanMateriaPrimaBLL
    {
        //Metodos de busqueda de datos
        public static void ObtenerDetalle(int codigo, Data.dsPlanMP ds)
        {
            DAL.PlanMateriasPrimasDAL.ObtenerDetalle(codigo,ds);
        }

        //Metodos para la busqueda
        public static void ObtenerTodos(int anio, Data.dsPlanMP ds)
        {
            DAL.PlanMateriasPrimasDAL.ObtenerTodos(anio, ds);
        }
        public static void ObtenerTodos(Data.dsPlanMP ds)
        {
            DAL.PlanMateriasPrimasDAL.ObtenerTodos(ds);
        }

        //Metodos de insercion de Datos
        public static int GuardarPlanAnual(Entidades.PlanMateriaPrima plan)
        {
           return DAL.PlanMateriasPrimasDAL.Insertar(plan);            
        }
        public static int GuardarDetalle(Entidades.DetallePlanMateriasPrimas detalle)
        {
            return DAL.PlanMateriasPrimasDAL.InsertarDetalle(detalle);
        }

        //Eliminacion de Datos
        public static void Eliminar(int codigo)
        {
            DAL.PlanMateriasPrimasDAL.Eliminar(codigo);
        }

        //Metodo que valida que no exista un plan ya generado
        public static void ValidarExistencia(int año)
        {
            DAL.PlanMateriasPrimasDAL.ValidarExistencia(año);
        }
    }
}
