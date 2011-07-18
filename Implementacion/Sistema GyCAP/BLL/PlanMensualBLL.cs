using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class PlanMensualBLL
    {
        //METODO DE BUSQUEDA
        public static void ObtenerTodos(int anio, string mes, Data.dsPlanMensual ds)
        {
            DAL.PlanMensualDAL.ObtenerTodos(anio, mes, ds);
        }
        public static void ObtenerTodos(DataTable dtPlanMensual)
        {
            DAL.PlanMensualDAL.ObtenerTodos(dtPlanMensual);
        }
        //Metodo que obtiene todos los planes mensuales de un plan anual
        public static void ObtenerPMAnio(DataTable dtPlanMensual, int codigoPA)
        {
            DAL.PlanMensualDAL.ObtenerPMAnio(dtPlanMensual, codigoPA);
        }

        //Metodo que valida que no exista un plan mensual para el mismo mes y año
        public static bool ExistePlanMensual(int anio, string mes)
        {
           return DAL.PlanMensualDAL.Validar(anio, mes);
        }

        //Metodo que valida que no exista un detalle de plan semanal para ese pedido
        public static bool ExistePlanSemanalPedido(int codigoDetallePedido)
        {
            return DAL.PlanMensualDAL.ExistePlanSemanalPedido(codigoDetallePedido);
        }

        //Metodo que valida que no exista un plan semanal con ese plan mensual para poder modificarse
        public static bool ExistePlanSemanal(int codigoPlan)
        {
            return DAL.PlanMensualDAL.ValidarActualizar(codigoPlan);
        }

        //METODO DE GUARDADO
        public static void GuardarPlan(Entidades.PlanMensual plan, Data.dsPlanMensual detalle)
        {
            DAL.PlanMensualDAL.GuardarPlan(plan, detalle);
        }

        //METODO DE MODIFICACION
        public static void GuardarPlanModificado(Entidades.PlanMensual plan, Data.dsPlanMensual detalle)
        {
            DAL.PlanMensualDAL.GuardarPlanModificado(plan, detalle);
        }

        //METODO DE ELIMINACION
        public static void EliminarPlan(int codigoPlan)
        {
            DAL.PlanMensualDAL.EliminarPlan(codigoPlan);
        }

        //OBTENCION DE LA SUMATORIA 
        public static int CalcularTotal(int anio, string mes)
        {
            return DAL.PlanMensualDAL.ObtenerCantidad(anio, mes);
        }
        //*************************************************************************
        //                         Chequeo excepciones Plan Mensual
        //**************************************************************************       
        public static List<Entidades.ExcepcionesPlan> CheckeoExcepciones(DataTable dtDetallePlanMensual)
        {
            //Defino las listas genericas a utilizar
            List<Entidades.ExcepcionesPlan> excepciones = new List<GyCAP.Entidades.ExcepcionesPlan>();
            List<Entidades.DetallePlanMensual> detallePlanMes = new List<GyCAP.Entidades.DetallePlanMensual>();
            
            //Metodo que pasa el datatatable de detalle a objetos
            detallePlanMes = GenerarDetalle(dtDetallePlanMensual);
            
            //Metodo que checkea los faltantes de MP
            excepciones = Checkeo_MP(detallePlanMes);

            //Para otros checkeos se van concatenando las listas genericas en una sola
            //CHEQUEO2 (Ejemplo de como concatenar las listas)
            //excepciones.Concat(Checkeo_2(detallePlanMes)); 
                        
            return excepciones;
        }

        private static List<Entidades.DetallePlanMensual> GenerarDetalle (DataTable dtDetallePM)
        {
            //Creo la lista generica
            List<Entidades.DetallePlanMensual> detallePlan = new List<GyCAP.Entidades.DetallePlanMensual>();
            
            //Creo el objeto generico para cargar e la lista
            Entidades.DetallePlanMensual detalle = new GyCAP.Entidades.DetallePlanMensual();
            Entidades.Cocina cocina = new GyCAP.Entidades.Cocina();
            Entidades.PlanMensual planMes = new GyCAP.Entidades.PlanMensual();

            foreach (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row in dtDetallePM.Rows)
            {
                detalle.Codigo =Convert.ToInt32(row.DPMES_CODIGO);
                cocina.CodigoCocina =Convert.ToInt32(row.COC_CODIGO);
                detalle.Cocina = cocina;
                planMes.Codigo = Convert.ToInt32(row.PMES_CODIGO);
                detalle.PlanMensual = planMes;
                detalle.CantidadEstimada = Convert.ToInt32(row.DPMES_CANTIDADESTIMADA);
                detalle.DetallePedido = Convert.ToInt32(row.DPED_CODIGO);

                //Agregamos el objeto a la lista generica
                detallePlan.Add(detalle);
            }

            return detallePlan;
        }

        private static List<Entidades.ExcepcionesPlan> Checkeo_MP(List<Entidades.DetallePlanMensual> detallePlanMes)
        {
            //Defino la lista que va a devolver las excepciones
            List<Entidades.ExcepcionesPlan> listaExcepciones = new List<GyCAP.Entidades.ExcepcionesPlan>();

            //Defino la lista que va a contener las materias primas y cantidades
            List<Entidades.MPEstructura> listaMPEstructura = new List<GyCAP.Entidades.MPEstructura>();

            //Metodo que checkea que se tenga en cuenta las cantidades de materia prima
            foreach (Entidades.DetallePlanMensual pm in detallePlanMes)
            {
                List<Entidades.MPEstructura> listaAuxiliar = new List<GyCAP.Entidades.MPEstructura>();
                listaAuxiliar = EstructuraBLL.MateriasPrimasCocina(pm.Cocina.CodigoCocina, pm.CantidadEstimada);

                listaMPEstructura = listaMPEstructura.Concat(listaAuxiliar).ToList();                
            }
            
            //Genero una lista unica de objetos que sume las cantidades de MP
            List<Entidades.MPEstructura> listaAux = new List<GyCAP.Entidades.MPEstructura>();

            foreach (Entidades.MPEstructura mp in listaMPEstructura)
            {
                if (listaAux.Exists(x => x.MateriaPrima.CodigoMateriaPrima == mp.MateriaPrima.CodigoMateriaPrima))
                {
                    listaAux.Find(x => x.MateriaPrima.CodigoMateriaPrima == mp.MateriaPrima.CodigoMateriaPrima).Cantidad += mp.Cantidad;
                }
                else
                {
                    listaAux.Add(mp);
                }
            }

            //Checkeo las cantidades de materia prima en stock y voy generando las excepciones
            foreach (Entidades.MPEstructura mp in listaAux)
            {
                //Obtengo la cantidad disponible
                decimal cantidadDisponible = BLL.UbicacionStockBLL.CantidadMateriaPrima(mp.MateriaPrima);

                //Genero excepciones si la cantidad es mayor a la cantidad disponible
                if (mp.Cantidad > cantidadDisponible)
                {
                    listaExcepciones.Add(BLL.ExcepcionesPlanBLL.Add_ExcepcionMP(Convert.ToDecimal(cantidadDisponible - mp.Cantidad), mp.MateriaPrima));
                }
            }

            return listaExcepciones;
        }
     
    }
}
