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
            arbolProduccion.OrdenesTrabajo = new List<NodoOrdenTrabajo>();
                       
            int numeroOrdenT = -1;
            if (arbolProduccion.OrdenProduccion.Estructura > 0)
            {
                ArbolEstructura arbolEstructura = EstructuraBLL.GetArbolEstructuraByCocina(arbolProduccion.OrdenProduccion.Cocina.CodigoCocina, true);
                arbolEstructura.SetProductQuantity(arbolProduccion.OrdenProduccion.CantidadEstimada);
                EstadoOrdenTrabajo estadoGenerado = EstadoOrdenTrabajoBLL.GetEstado(OrdenesTrabajoEnum.EstadoOrdenEnum.Generada);

                ProcessNodo(arbolEstructura.NodoRaiz, arbolProduccion, ref numeroOrdenT, estadoGenerado, null, listaExcepciones);

                arbolProduccion.GetFechaInicio(arbolProduccion.GetFechaFinalizacion(arbolProduccion.OrdenProduccion.FechaInicioEstimada.Value, false), false);
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
                    foreach (DetalleHojaRuta detalleHR in nodoEstructura.Compuesto.Parte.HojaRuta.Detalle.Reverse())
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
                                FechaInicioEstimada = arbolProduccion.OrdenProduccion.FechaInicioEstimada.Value,
                                FechaInicioReal = null,
                                FechaFinEstimada = arbolProduccion.OrdenProduccion.FechaFinEstimada.Value,
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

            foreach (OrdenTrabajo ot in lista)
            {
                if (lista.Count(p => p.OrdenTrabajoPadre.HasValue && p.OrdenTrabajoPadre.Value == ot.Numero) > 0)
                {
                    ot.HasChildren = true;
                }
            }

            return ordenProduccion.OrdenesTrabajo = lista;
        }

        public static void IniciarOrdenTrabajo(OrdenTrabajo ordenT)
        {
            SqlTransaction transaccion = null;

            try
            {
                EstadoMovimientoStock estadoMvto = EstadoMovimientoStockBLL.GetEstadoEntity(StockEnum.EstadoMovimientoStock.EnProceso);
                EstadoOrdenTrabajo estadoEnProceso = EstadoOrdenTrabajoBLL.GetEstado(OrdenesTrabajoEnum.EstadoOrdenEnum.EnProceso);
                DateTime fechaInicioReal = DBBLL.GetFechaServidor();

                ordenT.Estado = estadoEnProceso;
                ordenT.FechaInicioReal = fechaInicioReal;

                transaccion = DAL.DB.IniciarTransaccion();

                OrdenTrabajoBLL.RegistrarInicioOrdenTrabajo(ordenT, transaccion);

                ordenT.MovimientosStock = MovimientoStockBLL.GetMovimientosByOwner(EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.OrdenTrabajo, ordenT.Numero, transaccion));

                foreach (MovimientoStock mvto in ordenT.MovimientosStock)
                {
                    mvto.Estado = estadoMvto;

                    foreach (OrigenMovimiento origenMvto in mvto.OrigenesMultiples)
                    {
                        origenMvto.CantidadReal = origenMvto.CantidadEstimada;
                        origenMvto.FechaReal = fechaInicioReal;
                    }

                    MovimientoStockBLL.Iniciar(mvto, transaccion);
                }

                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new BaseDeDatosException(ex.Message);
            }
            finally
            {
                DAL.DB.FinalizarTransaccion();
            }
        }
        
        public static void RegistrarInicioOrdenTrabajo(OrdenTrabajo ordenT, SqlTransaction transaccion)
        {
            DAL.OrdenTrabajoDAL.RegistrarInicioOrdenTrabajo(ordenT, transaccion);
        }

        public static void ActualizarEstado(OrdenTrabajo ordenT, SqlTransaction transaccion)
        {
            DAL.OrdenTrabajoDAL.ActualizarEstado(ordenT, transaccion);
        }

        public static void RegistrarCierreParcial(CierreParcialOrdenTrabajo cierre)
        {
            SqlTransaction transaccion = null;

            try
            {
                cierre.OrdenTrabajo.MovimientosStock = MovimientoStockBLL.GetMovimientosByOwner(EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.OrdenTrabajo, cierre.OrdenTrabajo.Numero, transaccion));
                
                transaccion = DAL.DB.IniciarTransaccion();

                CierreParcialOrdenTrabajoBLL.Insertar(cierre, transaccion);
                cierre.OrdenTrabajo.CierresParciales.Add(cierre);
                cierre.OrdenTrabajo.CantidadReal += cierre.Cantidad;
                DAL.OrdenTrabajoDAL.RegistrarCierreParcial(cierre, transaccion);                

                foreach (MovimientoStock mvto in cierre.OrdenTrabajo.MovimientosStock)
                {
                    decimal incremento = (decimal)cierre.Cantidad;
                    mvto.CantidadDestinoReal += incremento;
                    MovimientoStockBLL.RegistrarAvance(mvto, incremento, transaccion);
                }

                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new BaseDeDatosException(ex.Message);
            }
            finally
            {
                DAL.DB.FinalizarTransaccion();
            }
        }        

        public static void Finalizar(OrdenTrabajo ordenT)
        {
            SqlTransaction transaccion = null;
            
            try
            {
                EstadoMovimientoStock estadoMvtoFinalizado = EstadoMovimientoStockBLL.GetEstadoEntity(StockEnum.EstadoMovimientoStock.Finalizado);
                EstadoOrdenTrabajo estadoFinalizada = EstadoOrdenTrabajoBLL.GetEstado(OrdenesTrabajoEnum.EstadoOrdenEnum.Finalizada);
                DateTime fechaServidor = DBBLL.GetFechaServidor();

                transaccion = DAL.DB.IniciarTransaccion();

                ordenT.Estado = estadoFinalizada;
                ordenT.FechaFinReal = fechaServidor;

                DAL.OrdenTrabajoDAL.FinalizarOrdenTrabajo(ordenT, transaccion);

                foreach (MovimientoStock mvto in ordenT.MovimientosStock)
                {
                    mvto.Estado = estadoMvtoFinalizado;
                    mvto.FechaReal = fechaServidor;
                    MovimientoStockBLL.Finalizar(mvto, transaccion, false);
                }

                //Actualizamos la eficiencia de los centros involucrados
                if (ordenT.CierresParciales != null)
                {
                    int operacionesFallidas = 0;
                    foreach (CierreParcialOrdenTrabajo cierre in ordenT.CierresParciales)
                    {
                        operacionesFallidas += cierre.OperacionesFallidas;
                    }

                    BLL.CentroTrabajoBLL.ActualizarEficiencia(ordenT.DetalleHojaRuta.CentroTrabajo, ordenT.CantidadReal, operacionesFallidas, transaccion);
                }

                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new BaseDeDatosException(ex.Message);
            }
            finally
            {
                DAL.DB.FinalizarTransaccion();
            }
        }

        public static void Cancelar(OrdenTrabajo ordenTrabajo, EstadoOrdenTrabajo estadoCancelado, SqlTransaction transaccion)
        {
            ordenTrabajo.MovimientosStock = MovimientoStockBLL.GetMovimientosByOwner(EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.OrdenTrabajo, ordenTrabajo.Numero, transaccion));

            foreach (MovimientoStock mvto in ordenTrabajo.MovimientosStock)
            {
                mvto.Estado = EstadoMovimientoStockBLL.GetEstadoEntity(StockEnum.EstadoMovimientoStock.Cancelado);
                MovimientoStockBLL.Cancelar(mvto, transaccion);
            }

            ordenTrabajo.Estado = estadoCancelado;

            DAL.OrdenTrabajoDAL.Cancelar(ordenTrabajo, transaccion);            
        }

        public static void EliminarCierreParcial(CierreParcialOrdenTrabajo cierre)
        {
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DAL.DB.IniciarTransaccion();

                cierre.OrdenTrabajo.MovimientosStock = MovimientoStockBLL.GetMovimientosByOwner(EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.OrdenTrabajo, cierre.OrdenTrabajo.Numero, transaccion));

                CierreParcialOrdenTrabajo cierreToRemove = cierre.OrdenTrabajo.CierresParciales.First(p => p.Codigo == cierre.Codigo);
                cierre.OrdenTrabajo.CierresParciales.Remove(cierreToRemove);                
                cierre.OrdenTrabajo.CantidadReal -= cierre.Cantidad;
                DAL.OrdenTrabajoDAL.EliminarCierreParcial(cierre, transaccion);

                foreach (MovimientoStock mvto in cierre.OrdenTrabajo.MovimientosStock)
                {
                    decimal decremento = (decimal)cierre.Cantidad;
                    mvto.CantidadDestinoReal -= decremento;
                    MovimientoStockBLL.EliminarAvance(mvto, decremento, transaccion);
                }

                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new BaseDeDatosException(ex.Message);
            }
            finally
            {
                DAL.DB.FinalizarTransaccion();
            }
        }
    }
}
