
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class PlanSemanalBLL
    {

        //METODOS DE BÚSQUEDA        
        //Metodo que obtiene los planes semanales de un plan mesual
        public static void obtenerPS(DataTable dtPlanSemanal, int codigoPlanMensual)
        {
            DAL.PlanSemanalDAL.obtenerPS(dtPlanSemanal, codigoPlanMensual);
        }
        //Metodo que busca la cantidad de cocinas planificadas
        public static int? obtenerCocinasPlanificadas(int codigoCocina, int codigoPA, int codigoPM)
        {
            return DAL.PlanSemanalDAL.obtenerCocinasPlanificadas(codigoCocina,codigoPA,codigoPM);
        }

        //Metodo que busca la cantidad de cocinas planificadas de un pedido
        public static int? obtenerCocinasPlanificadas(int codigoCocina, int codigoPA, int codigoPM, int codigoPedido)
        {
            return DAL.PlanSemanalDAL.obtenerCocinasPlanificadas(codigoCocina, codigoPA, codigoPM,codigoPedido);
        }

        //METODOS DE VALIDACION
        //Metodo que busca y verifica si existe un plan semanal para un anio y mes determinado
        public static bool validarDetalle(int codigoPlanMensual, int numeroSemana)
        {
           return DAL.PlanSemanalDAL.Validar(codigoPlanMensual, numeroSemana);
        }

        //Metodo que verifica que no exista en un plan semanal ese dia
        public static bool validarDia(DateTime dia)
        {
            return DAL.PlanSemanalDAL.ValidarDia(dia);
        }
        
        //METODO DE GUARDADO DE DATOS
        public static int InsertarPlanSemanal(Entidades.PlanSemanal planSemanal, Entidades.DiasPlanSemanal diaPlanSemanal, Data.dsPlanSemanal dsPlanSemanal, bool esPrimero)
        {
            return DAL.PlanSemanalDAL.GuardarPlanSemanal(planSemanal,diaPlanSemanal,dsPlanSemanal,esPrimero);
        }
        
        //Metodo para MODIFICAR el Plan Semanal
        public static void ModificarPlanSemanal(Entidades.PlanSemanal planSemanal, Entidades.DiasPlanSemanal diaPlanSemanal, Data.dsPlanSemanal dsPlanSemanal)
        {
            DAL.PlanSemanalDAL.GuardarPlanModificado(planSemanal, diaPlanSemanal, dsPlanSemanal);
        }

        //METODO PARA ELIMINAR EL PLAN SEMANAL
        //METODO DE ELIMINACION
        public static void EliminarPlan(int codigoPlan, Data.dsPlanSemanal dsPlanSemanal)
        {
            DAL.PlanSemanalDAL.EliminarPlan(codigoPlan, dsPlanSemanal);
        }

        //**************************************** Control Planificación **************************************
        //METODOS DE SEGUIMIENTO DE LA PLANIFICACIÓN
        public static int ObtenerEstado(int codigoPlan)
        {
           return DAL.PlanSemanalDAL.obtenerEstadoOrden(codigoPlan);
        }
        //METODOS DE VALIDACION DE EXISTENCIA DE ORDEN DE PRODUCCION
        public static int VerificarOrden(int codigoPlan)
        {
            return DAL.PlanSemanalDAL.VerificarOrdenProduccion(codigoPlan);
        }
        //METODO QUE ME DEVUELVE LOS PLANES MENSUALES DE UN DETERMINADO AÑO
        public static void ObtenerPlanesMensuales(int codigoPlanAnual, Data.dsPlanSemanal dsPlanSemanal)
        {
            DAL.PlanSemanalDAL.obtenerPlanesMensuales(codigoPlanAnual,dsPlanSemanal);
        }

        //*************************************************************************
        //                         Chequeo excepciones Plan Semanal
        //**************************************************************************       
        public static List<Entidades.ExcepcionesPlan> CheckeoExcepciones(DataTable dtDetallePlanSemanal)
        {
            //Defino las listas genericas a utilizar
            List<Entidades.ExcepcionesPlan> excepciones = new List<GyCAP.Entidades.ExcepcionesPlan>();
            List<Entidades.DetallePlanSemanal> detallePlanSemanal = new List<GyCAP.Entidades.DetallePlanSemanal>();

            //Metodo que pasa el datatatable de detalle a objetos
            detallePlanSemanal = GenerarDetalle(dtDetallePlanSemanal);

            //Metodo que checkea los faltantes de MP
            excepciones = Checkeo_MP(detallePlanSemanal);

            //Para otros checkeos se van concatenando las listas genericas en una sola
            //CHEQUEO2 (Ejemplo de como concatenar las listas)
            //excepciones.Concat(Checkeo_2(detallePlanMes)); 

            return excepciones;
        }

        private static List<Entidades.DetallePlanSemanal> GenerarDetalle(DataTable dtDetallePlanSemanal)
        {
            //Creo la lista generica
            List<Entidades.DetallePlanSemanal> detallePlan = new List<GyCAP.Entidades.DetallePlanSemanal>();

            //Creo el objeto generico para cargar e la lista
            Entidades.DetallePlanSemanal detalle = new GyCAP.Entidades.DetallePlanSemanal();
            Entidades.Cocina cocina = new GyCAP.Entidades.Cocina();
            Entidades.DiasPlanSemanal diaPlanSemanal = new GyCAP.Entidades.DiasPlanSemanal();
            Entidades.DetallePedido detallePedido = new GyCAP.Entidades.DetallePedido();

            foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row in dtDetallePlanSemanal.Rows)
            {
                detalle.Codigo = Convert.ToInt32(row.DPSEM_CODIGO);
                cocina.CodigoCocina = Convert.ToInt32(row.COC_CODIGO);
                detalle.Cocina = cocina;
                diaPlanSemanal.Codigo = Convert.ToInt32(row.DIAPSEM_CODIGO);
                detalle.DiaPlanSemanal = diaPlanSemanal;
                detalle.CantidadEstimada = Convert.ToInt32(row.DPSEM_CANTIDADESTIMADA);
                if (row.DPED_CODIGO != null)
                {
                    detallePedido.Codigo = Convert.ToInt32(row.DPED_CODIGO);
                    detalle.DetallePedido = detallePedido;
                }
                
                //Agregamos el objeto a la lista generica
                detallePlan.Add(detalle);
            }

            return detallePlan;
        }

        private static List<Entidades.ExcepcionesPlan> Checkeo_MP(List<Entidades.DetallePlanSemanal> detallePlanSemanal)
        {
            //Defino la lista que va a devolver las excepciones
            List<Entidades.ExcepcionesPlan> listaExcepciones = new List<GyCAP.Entidades.ExcepcionesPlan>();

            //Defino la lista que va a contener las materias primas y cantidades
            List<Entidades.MPEstructura> listaMPEstructura = new List<GyCAP.Entidades.MPEstructura>();

            //Metodo que checkea que se tenga en cuenta las cantidades de materia prima
            foreach (Entidades.DetallePlanSemanal psem in detallePlanSemanal)
            {
                List<Entidades.MPEstructura> listaAuxiliar = new List<GyCAP.Entidades.MPEstructura>();
                listaAuxiliar = EstructuraBLL.MateriasPrimasCocina(psem.Cocina.CodigoCocina, psem.CantidadEstimada);

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
