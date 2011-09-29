﻿using System;
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
        /// <param name="arbolProduccion">La Orden de Producción</param>
        public static void GenerarOrdenesTrabajo(ArbolProduccion arbolProduccion, IList<ExcepcionesPlan> listaExcepciones)
        {
            //Si la orden de producción tiene órdenes de trabajo generadas las eliminamos
            //Y si ya estan en la DB ??? - gonzalo
            //Crear excepciones
            arbolProduccion.OrdenesTrabajo = new List<NodoOrdenTrabajo>();
                       
            int numeroOrdenT = -1;
            if (arbolProduccion.OrdenProduccion.Estructura > 0)
            {
                ArbolEstructura arbolEstructura = EstructuraBLL.GetArbolEstructura(arbolProduccion.OrdenProduccion.Cocina.CodigoCocina, true);
                arbolEstructura.SetProductQuantity(arbolProduccion.OrdenProduccion.CantidadEstimada);
                EstadoOrdenTrabajo estadoGenerado = EstadoOrdenTrabajoBLL.GetEstado(OrdenesTrabajoEnum.EstadoOrdenEnum.Generada);

                ProcessNodo(arbolEstructura.NodoRaiz, arbolProduccion, ref numeroOrdenT, estadoGenerado, null, listaExcepciones);
            }
            else
            {
                listaExcepciones.Add(ExcepcionesPlanBLL.Add_ExcepcionCocinaSinEstructura(arbolProduccion.OrdenProduccion.Cocina.CodigoProducto));
            }
        }

        private static void ProcessNodo(NodoEstructura nodoEstructura, ArbolProduccion arbolProduccion, ref int numeroOrdenT, EstadoOrdenTrabajo estadoGenerado, NodoOrdenTrabajo lastNodoOT, IList<ExcepcionesPlan> listaExcepciones)
        {
            if (nodoEstructura.Contenido != NodoEstructura.tipoContenido.MateriaPrima)
            {
                if (nodoEstructura.Compuesto.Parte.HojaRuta != null)
                {
                    foreach (DetalleHojaRuta detalleHR in nodoEstructura.Compuesto.Parte.HojaRuta.Detalle)
                    {
                        NodoOrdenTrabajo nodoOT = new NodoOrdenTrabajo()
                        {
                            CodigoNodo = arbolProduccion.GetNextCodigoOrden(),
                            NodoPadre = lastNodoOT,
                            NodosHijos = new List<NodoOrdenTrabajo>(),
                            Text = string.Concat("OTA", (numeroOrdenT + 1) * -1),
                            OrdenTrabajo = new OrdenTrabajo()
                            {
                                Numero = numeroOrdenT--,
                                Codigo = string.Concat("OTA", (numeroOrdenT + 1) * -1),
                                Origen = string.Concat(arbolProduccion.OrdenProduccion.Origen, " / ", nodoEstructura.Compuesto.Parte.Codigo),
                                Observaciones = "",
                                Estado = estadoGenerado,
                                CantidadEstimada = Convert.ToInt32(nodoEstructura.Compuesto.Cantidad),
                                CantidadReal = 0,
                                DetalleHojaRuta = detalleHR,
                                FechaInicioEstimada = null,
                                FechaInicioReal = null,
                                FechaFinEstimada = null,
                                FechaFinReal = null,
                                OrdenProduccion = arbolProduccion.OrdenProduccion.Numero,
                                Parte = nodoEstructura.Compuesto.Parte,
                                Secuencia = detalleHR.Secuencia,
                                OrdenTrabajoPadre = (lastNodoOT != null) ? lastNodoOT.OrdenTrabajo.Numero : 0
                            }
                        };

                        if (lastNodoOT == null)
                        {
                            arbolProduccion.OrdenesTrabajo.Add(nodoOT);
                        }
                        else
                        {
                            lastNodoOT.NodosHijos.Add(nodoOT);
                        }
                        lastNodoOT = nodoOT;

                        if (detalleHR.CentroTrabajo.Activo == (int)RecursosFabricacionEnum.EstadoCentroTrabajo.Inactivo)
                        {
                            listaExcepciones.Add(ExcepcionesPlanBLL.Add_ExcepcionCentroInactivo(detalleHR.CentroTrabajo.Nombre));
                        }
                    }

                    foreach (NodoEstructura item in nodoEstructura.NodosHijos)
                    {
                        ProcessNodo(item, arbolProduccion, ref numeroOrdenT, estadoGenerado, lastNodoOT, listaExcepciones);
                    }
                }
                else
                {
                    listaExcepciones.Add(ExcepcionesPlanBLL.Add_ExcepcionParteSinHojaRuta(nodoEstructura.Compuesto.Parte.Codigo));
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
