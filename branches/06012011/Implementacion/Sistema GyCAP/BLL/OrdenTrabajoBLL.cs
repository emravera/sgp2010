using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GyCAP.Entidades;
using GyCAP.Entidades.ArbolOrdenesTrabajo;
using GyCAP.Entidades.ArbolEstructura;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades.Excepciones;

namespace GyCAP.BLL
{
    public class OrdenTrabajoBLL
    {   
        /// <summary>
        /// Genera las órdenes de trabajo para la orden de producción dada.
        /// </summary>
        /// <param name="codigoOrdenProduccion"></param>
        /// <param name="dsPlanSemanal"></param>
        /// <param name="dsOrdenTrabajo"></param>
        /// <param name="dsEstructura"></param>
        /// <param name="dsHojaRuta"></param>
        public static void GenerarOrdenesTrabajo(ArbolProduccion arbolProduccion)
        {
            //Si la orden de producción tiene órdenes de trabajo generadas las eliminamos
            //Y si ya estan en la DB ??? - gonzalo
            arbolProduccion.OrdenesTrabajo = new List<NodoOrdenTrabajo>();
            
            //Generamos los códigos de las órdenes de trabajo            
            int numeroOrdenT = 0;
            //Obtenemos la estructura completa de la cocina            
            ArbolEstructura arbolEstructura = EstructuraBLL.GetArbolEstructura(arbolProduccion.OrdenProduccion.Cocina.CodigoCocina, true);

            //Creamos las órdenes de trabajo, generamos una para cada par operación-centro de la hoja de ruta de la parte
            //De forma recursiva empezando por el nivel más alto de la estructura
            EstadoOrdenTrabajo estadoGenerado = EstadoOrdenTrabajoBLL.GetEstado(OrdenesTrabajoEnum.EstadoOrdenEnum.Generada);

            if (arbolEstructura.NodoRaiz.Compuesto.Parte.HojaRuta != null)
            {
                foreach (DetalleHojaRuta detalleHR in arbolEstructura.NodoRaiz.Compuesto.Parte.HojaRuta.Detalle)
	            {
                    arbolProduccion.OrdenesTrabajo.Add(new NodoOrdenTrabajo()
                    {
                        CodigoNodo = arbolProduccion.GetNextCodigoOrden(),
                        NodoPadre = null,
                        NodosHijos = new List<NodoOrdenTrabajo>(),
                        Text = "texto",
                        OrdenTrabajo = new OrdenTrabajo()
                        {
                            Numero = numeroOrdenT--,
                            Codigo = "codigo",
                            Origen = "origen",
                            Observaciones = "",
                            Estado = estadoGenerado,
                            CantidadEstimada = 0,
                            CantidadReal = 0,
                            DetalleHojaRuta = detalleHR,
                            FechaInicioEstimada = DateTime.Now,
                            FechaInicioReal = null,
                            FechaFinEstimada = DateTime.Now,
                            FechaFinReal = null,
                            OrdenProduccion = arbolProduccion.OrdenProduccion.Numero,
                            Parte = arbolEstructura.NodoRaiz.Compuesto.Parte,
                            Secuencia = 0
                        }
                    });
            	}
            }
        }

        public static void ObtenerOrdenesTrabajo(int numeroOrdenProduccion, Data.dsOrdenTrabajo dsOrdenTrabajo, bool obtenerCierresParciales)
        {
            DAL.OrdenTrabajoDAL.ObtenerOrdenesTrabajo(numeroOrdenProduccion, dsOrdenTrabajo, obtenerCierresParciales);
        }

        public static void RegistrarCierreParcial(int numeroOrdenTrabajo, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsStock dsStock)
        {
            DAL.OrdenTrabajoDAL.RegistrarCierreParcial(numeroOrdenTrabajo, dsOrdenTrabajo, dsStock);
        }
    }
}
