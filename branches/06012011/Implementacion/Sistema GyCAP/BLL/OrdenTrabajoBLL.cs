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
using GyCAP.Entidades.BindingEntity;
using System.Data.SqlClient;

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
                                OrdenTrabajoPadre = (lastNodoOT != null) ? lastNodoOT.OrdenTrabajo.Numero : 0,
                                Tipo = (detalleHR.CentroTrabajo.Tipo.Codigo == (int)RecursosFabricacionEnum.TipoCentroTrabajo.Proveedor) ? (int)OrdenesTrabajoEnum.TipoOrden.Adquisición : (int)OrdenesTrabajoEnum.TipoOrden.Fabricación
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

        public static SortableBindingList<OrdenTrabajo> ObtenerOrdenesTrabajo(OrdenProduccion ordenProduccion, 
                                                                              SortableBindingList<EstadoOrdenTrabajo> estados, 
                                                                              SortableBindingList<Parte> partes, 
                                                                              SortableBindingList<HojaRuta> hojasRutas,
                                                                              bool obtenerCierresParciales)
        {
            Data.dsOrdenTrabajo ds = new GyCAP.Data.dsOrdenTrabajo();
            DAL.OrdenTrabajoDAL.ObtenerOrdenesTrabajo(ordenProduccion.Numero, ds, obtenerCierresParciales);
            SortableBindingList<OrdenTrabajo> lista = new SortableBindingList<OrdenTrabajo>();

            foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow row in ds.ORDENES_TRABAJO.Rows)
            {
                OrdenTrabajo orden = new OrdenTrabajo();
                orden.Numero = Convert.ToInt32(row.ORDT_NUMERO);
                orden.Codigo = row.ORDT_CODIGO;
                orden.Origen = row.ORDT_ORIGEN;
                orden.Observaciones = row.ORDT_OBSERVACIONES;
                orden.OrdenProduccion = ordenProduccion.Numero;
                orden.CantidadEstimada = Convert.ToInt32(row.ORDT_CANTIDADESTIMADA);
                orden.CantidadReal = Convert.ToInt32(row.ORDT_CANTIDADREAL);
                foreach (HojaRuta item in hojasRutas)
                {
                    orden.DetalleHojaRuta = item.Detalle.FirstOrDefault(p => p.Codigo == Convert.ToInt32(row.DHR_CODIGO));
                    if (orden.DetalleHojaRuta != null) { break; }
                }
                orden.Estado = estados.Where(p => p.Codigo == Convert.ToInt32(row.EORD_CODIGO)).Single();
                orden.FechaFinEstimada = row.ORDT_FECHAFINESTIMADA;
                orden.FechaInicioEstimada = row.ORDT_FECHAINICIOESTIMADA;
                if (row.IsORDT_FECHAINICIOREALNull()) { orden.FechaInicioReal = null; }
                else { orden.FechaInicioReal = row.ORDT_FECHAINICIOREAL; }
                if (row.IsORDT_FECHAFINREALNull()) { orden.FechaFinReal = null; }
                else { orden.FechaFinReal = row.ORDT_FECHAFINREAL; }
                if (row.IsORDT_NUMERO_PADRENull()) { orden.OrdenTrabajoPadre = null; }
                else { orden.OrdenTrabajoPadre = Convert.ToInt32(row.ORDT_NUMERO_PADRE); }
                orden.Parte = partes.Where(p => p.Numero == Convert.ToInt32(row.PART_NUMERO)).Single();
                orden.Secuencia = Convert.ToInt32(row.ORDT_SECUENCIA);
                orden.Tipo = Convert.ToInt32(row.ORDT_TIPO);

                lista.Add(orden);
            }

            return ordenProduccion.OrdenesTrabajo = lista;
        }

        public static void IniciarOrdenTrabajo(OrdenTrabajo ordenT, SqlTransaction transaccion)
        {
            DAL.OrdenTrabajoDAL.IniciarOrdenTrabajo(ordenT, transaccion);
        }

        public static void ActualizarEstado(OrdenTrabajo ordenT, SqlTransaction transaccion)
        {
            DAL.OrdenTrabajoDAL.ActualizarEstado(ordenT, transaccion);
        }

        public static void RegistrarCierreParcial(CierreParcialOrdenTrabajo cierre, SqlTransaction transaccion)
        {
            SqlTransaction _transaccion = null;

            try
            {
                _transaccion = (transaccion == null) ? DAL.DB.IniciarTransaccion() : transaccion;

                CierreParcialOrdenTrabajoBLL.Insertar(cierre, _transaccion);
                cierre.OrdenTrabajo.CierresParciales.Add(cierre);
                cierre.OrdenTrabajo.CantidadReal += cierre.Cantidad;
                DAL.OrdenTrabajoDAL.RegistrarCierreParcial(cierre, _transaccion);

                if (transaccion == null) { _transaccion.Commit(); }
            }
            catch (SqlException ex)
            {
                if (transaccion == null)
                {
                    _transaccion.Rollback();
                    throw new BaseDeDatosException(ex.Message);
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                if (transaccion == null) { DAL.DB.FinalizarTransaccion(); }
            }            
        }

        public static void Finalizar(OrdenTrabajo ordenT, SqlTransaction transaccion)
        {            
            DAL.OrdenTrabajoDAL.FinalizarOrdenTrabajo(ordenT, transaccion);

            foreach (MovimientoStock mvto in ordenT.MovimientosStock)
            {
                MovimientoStockBLL.Finalizar(mvto, transaccion);
            }
        }
    }
}
