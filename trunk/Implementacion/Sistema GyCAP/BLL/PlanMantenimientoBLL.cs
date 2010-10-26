using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class PlanMantenimientoBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerPlanMantenimiento(object descripcion, object numero, int idEstadoPlan, Data.dsPlanMantenimiento ds, bool obtenerDetalle)
        {
            DAL.PlanMantenimientoDAL.ObtenerPlanMantenimiento(descripcion, numero, idEstadoPlan, ds, obtenerDetalle);
        }

        public static void ObtenerPlanMantenimiento(DataTable dtPlanMantenimiento)
        {
            DAL.PlanMantenimientoDAL.ObtenerPlanMantenimiento(dtPlanMantenimiento);
        }

        public static void Insertar(Data.dsPlanMantenimiento dsPlanMantenimiento)
        {
            //Si existe lanzamos la excepción correspondiente
            Entidades.PlanMantenimiento plan = new GyCAP.Entidades.PlanMantenimiento();
            //Así obtenemos el pedido nuevo del dataset, indicamos la primer fila de la agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsPlanMantenimiento.PLANES_MANTENIMIENTORow rowPlan = dsPlanMantenimiento.PLANES_MANTENIMIENTO.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsPlanMantenimiento.PLANES_MANTENIMIENTORow;
            //Creamos el objeto plan para verificar si existe
            plan.Numero = Convert.ToInt64(rowPlan.PMAN_NUMERO);
            plan.Descripcion = rowPlan.PMAN_DESCRIPCION;

            if (EsPlanMantenimiento(plan)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            DAL.PlanMantenimientoDAL.Insertar(dsPlanMantenimiento);
        }

        //Comprueba si existe una pieza dado su nombre y terminación
        public static bool EsPlanMantenimiento(Entidades.PlanMantenimiento planMantenimiento)
        {
            return DAL.PlanMantenimientoDAL.EsPlanMantenimiento(planMantenimiento);
        }

        public static void Actualizar(Data.dsPlanMantenimiento dsPlanMantenimiento)
        {
            DAL.PlanMantenimientoDAL.Actualizar(dsPlanMantenimiento);
        }

        public static void Eliminar(long codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.PlanMantenimientoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.PlanMantenimientoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }

        }
    }
}
