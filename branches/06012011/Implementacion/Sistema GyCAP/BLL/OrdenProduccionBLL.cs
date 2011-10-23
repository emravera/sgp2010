using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using GyCAP.Entidades;
using GyCAP.Entidades.ArbolEstructura;
using GyCAP.Entidades.ArbolOrdenesTrabajo;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades.BindingEntity;
using System.Data.SqlClient;
using GyCAP.Entidades.Excepciones;

namespace GyCAP.BLL
{
    public class OrdenProduccionBLL
    {
        public static SortableBindingList<OrdenProduccion> ObtenerOrdenesProduccion(object codigo, object estado, object modo, object fechaGeneracion, object fechaDesde, object fechaHasta, 
                                                SortableBindingList<Cocina> cocinas, SortableBindingList<EstadoOrdenTrabajo> estadosOrden, SortableBindingList<UbicacionStock> ubicaciones)
        {
            if (estado != null && Convert.ToInt32(estado) <= 0) { estado = null; }
            if (modo != null && Convert.ToInt32(modo) <= 0) { modo = null; }
            Data.dsOrdenTrabajo ds = new GyCAP.Data.dsOrdenTrabajo();
            DAL.OrdenProduccionDAL.ObtenerOrdenesProduccion(codigo, estado, modo, fechaGeneracion, fechaDesde, fechaHasta, ds);
            SortableBindingList<OrdenProduccion> lista = new SortableBindingList<OrdenProduccion>();

            foreach (Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow row in ds.ORDENES_PRODUCCION)
            {
                OrdenProduccion orden = new OrdenProduccion();
                orden.Numero = Convert.ToInt32(row.ORDP_NUMERO);
                orden.Codigo = row.ORDP_CODIGO;
                orden.Origen = row.ORDP_ORIGEN;
                orden.Prioridad = Convert.ToInt32(row.ORDP_PRIORIDAD);
                orden.CantidadEstimada = Convert.ToInt32(row.ORDP_CANTIDADESTIMADA);
                orden.CantidadReal = Convert.ToInt32(row.ORDP_CANTIDADREAL);
                orden.Cocina = cocinas.Where(p => p.CodigoCocina == Convert.ToInt32(row.COC_CODIGO)).Single();
                orden.DetallePlanSemanal = new DetallePlanSemanal() { Codigo = Convert.ToInt32(row.DPSEM_CODIGO) };
                object pedido = DAL.DetallePlanSemanalDAL.ObtenerPedidoClienteDeDetalle(orden.DetallePlanSemanal.Codigo, null);
                if(pedido != null) { orden.DetallePlanSemanal.DetallePedido = new DetallePedido() { Codigo = long.Parse(pedido.ToString()) }; }
                orden.Estado = estadosOrden.Where(p => p.Codigo == Convert.ToInt32(row.EORD_CODIGO)).Single();
                orden.Estructura = Convert.ToInt32(row.ESTR_CODIGO);
                orden.FechaAlta = row.ORDP_FECHAALTA;
                orden.FechaInicioEstimada = row.ORDP_FECHAINICIOESTIMADA;
                orden.FechaFinEstimada = row.ORDP_FECHAFINESTIMADA;
                if (row.IsORDP_FECHAINICIOREALNull()) { orden.FechaInicioReal = null; }
                else { orden.FechaInicioReal = row.ORDP_FECHAINICIOREAL; }
                if (row.IsORDP_FECHAFINREALNull()) { orden.FechaFinReal = null; }
                else { orden.FechaFinReal = row.ORDP_FECHAFINREAL; }
                orden.Lote = (row.IsLOT_CODIGONull()) ? null : new Lote() { Codigo = Convert.ToInt32(row.LOT_CODIGO) };
                orden.Observaciones = row.ORDP_OBSERVACIONES;
                orden.UbicacionStock = ubicaciones.Where(p => p.Numero == Convert.ToInt32(row.USTCK_DESTINO)).Single();

                lista.Add(orden);
            }

            return lista;
        }

        public static void Insertar(ArbolProduccion arbol)
        {
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DAL.DB.IniciarTransaccion();

                ArbolEstructura arbolEstructura = EstructuraBLL.GetArbolEstructuraByCocina(arbol.OrdenProduccion.Cocina.CodigoCocina, true);
                arbolEstructura.SetProductQuantity(arbol.OrdenProduccion.CantidadEstimada);
                
                //Guardamos la orden de producción y de trabajo
                DAL.OrdenProduccionDAL.Insertar(arbol, transaccion);

                //Creamos los movimientos de stock                
                IList<MovimientoStock> listaMovimientos = new List<MovimientoStock>();

                //Actualizamos los estados del detalle pedido y agregamos las cantidad al plan
                DAL.DetallePlanSemanalDAL.ActualizarEstado(new int[] { arbol.OrdenProduccion.DetallePlanSemanal.Codigo }, (int)PlanificacionEnum.EstadoDetallePlanSemanal.ConOrden, transaccion);
               
                if (arbol.OrdenProduccion.DetallePlanSemanal.DetallePedido != null)
                {
                    DAL.DetallePedidoDAL.ActualizarEstadoAEnCurso((int)(arbol.OrdenProduccion.DetallePlanSemanal.DetallePedido.Codigo), transaccion);

                    MovimientoStock movimiento = MovimientoStockBLL.GetMovimientoConfigurado(StockEnum.CodigoMovimiento.DetallePedido, StockEnum.EstadoMovimientoStock.Planificado);
                    Entidad entidad = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.DetallePedido, arbol.OrdenProduccion.DetallePlanSemanal.Codigo, transaccion);

                    movimiento.OrigenesMultiples.Add(new OrigenMovimiento()
                    {
                        Codigo = 0,
                        CantidadEstimada = arbol.OrdenProduccion.DetallePlanSemanal.DetallePedido.Cantidad,
                        CantidadReal = 0,
                        Entidad = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.UbicacionStock, arbol.OrdenProduccion.UbicacionStock.Numero, transaccion),
                        MovimientoStock = 0,
                        FechaPrevista = arbol.OrdenProduccion.DetallePlanSemanal.DetallePedido.FechaEntregaPrevista
                    });

                    movimiento.CantidadDestinoEstimada = arbol.OrdenProduccion.CantidadEstimada;
                    movimiento.Destino = entidad;
                    movimiento.Duenio = entidad;
                    movimiento.CantidadDestinoEstimada = arbol.OrdenProduccion.DetallePlanSemanal.DetallePedido.Cantidad;
                    movimiento.FechaPrevista = arbol.OrdenProduccion.DetallePlanSemanal.DetallePedido.FechaEntregaPrevista;

                    listaMovimientos.Add(movimiento);
                }

                foreach (OrdenTrabajo orden in arbol.AsOrdenesTrabajoList())
                {                    
                    MovimientoStock movimiento = MovimientoStockBLL.GetMovimientoConfigurado(StockEnum.CodigoMovimiento.OrdenTrabajo, StockEnum.EstadoMovimientoStock.Planificado);
                    Entidad entidadOrden = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.OrdenTrabajo, orden.Numero, transaccion);
                    
                    if (orden.Secuencia == 0)
                    {                        
                        NodoEstructura currentParte = arbolEstructura.FindByPartNumber(orden.Parte.Numero);
                        //es la primer operación de la hoja de ruta de la parte, usamos los stock de los hijos de la estructura
                        foreach (NodoEstructura nodoEstr in currentParte.NodosHijos)
                        {                            
                            OrigenMovimiento origen = new OrigenMovimiento()
                            {
                                Codigo = 0,
                                CantidadEstimada = nodoEstr.Compuesto.Cantidad,
                                CantidadReal = 0,
                                Entidad = null,
                                MovimientoStock = 0,
                                FechaPrevista = orden.FechaInicioEstimada.Value
                            };

                            if (nodoEstr.Contenido == NodoEstructura.tipoContenido.MateriaPrima)
                            {
                                if (nodoEstr.Compuesto.MateriaPrima.UbicacionStock != null)
                                {
                                    origen.Entidad = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.UbicacionStock, nodoEstr.Compuesto.MateriaPrima.UbicacionStock.Numero, transaccion);
                                }
                            }
                            else
                            {
                                if (nodoEstr.Compuesto.Parte.HojaRuta.UbicacionStock != null)
                                {
                                    origen.Entidad = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.UbicacionStock, nodoEstr.Compuesto.Parte.HojaRuta.UbicacionStock.Numero, transaccion);                                    
                                }
                            }

                            if (origen.Entidad != null) { movimiento.OrigenesMultiples.Add(origen); }

                            if (orden.DetalleHojaRuta.StockDestino != null)
                            {
                                movimiento.Destino = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.UbicacionStock, orden.DetalleHojaRuta.StockDestino.Numero, transaccion);
                            }
                            else
                            {
                                if (currentParte.Compuesto.Parte.HojaRuta.Detalle.Max(p => p.Codigo) == orden.DetalleHojaRuta.Codigo)
                                {
                                    movimiento.Destino = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.UbicacionStock, currentParte.Compuesto.Parte.HojaRuta.UbicacionStock.Numero, transaccion);
                                }
                                else
                                {
                                    movimiento.Destino = entidadOrden;
                                }
                            }                            
                        }                        
                    }
                    else
                    {
                        //como no es la primer operación usamos el stock origen si está definido                        
                        OrigenMovimiento origen = new OrigenMovimiento()
                        {
                            CantidadEstimada = orden.CantidadEstimada,
                            CantidadReal = 0,
                            Codigo = 0,
                            Entidad = null,
                            MovimientoStock = 0,
                            FechaPrevista = orden.FechaInicioEstimada.Value
                        };

                        if (orden.DetalleHojaRuta.StockOrigen != null)
                        {
                            origen.Entidad = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.UbicacionStock, orden.DetalleHojaRuta.StockOrigen.Numero, transaccion);
                        }
                        else
                        {
                            origen.Entidad = entidadOrden;
                        }

                        movimiento.OrigenesMultiples.Add(origen);
                        
                        if (orden.DetalleHojaRuta.StockDestino != null)
                        {
                            movimiento.Destino = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.UbicacionStock, orden.DetalleHojaRuta.StockDestino.Numero, transaccion);
                        }
                        else
                        {
                            if (orden.Parte.HojaRuta.Detalle.Max(p => p.Codigo) == orden.DetalleHojaRuta.Codigo)
                            {
                                movimiento.Destino = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.UbicacionStock, orden.Parte.HojaRuta.UbicacionStock.Numero, transaccion);
                            }
                            else
                            {
                                movimiento.Destino = entidadOrden;
                            }
                        }
                    }

                    movimiento.FechaPrevista = orden.FechaInicioEstimada.Value;
                    movimiento.CantidadDestinoEstimada = orden.CantidadEstimada;
                    movimiento.Duenio = entidadOrden;

                    if (movimiento.OrigenesMultiples.Count > 0 && movimiento.Destino != null) { listaMovimientos.Add(movimiento); }
                }

                foreach (var item in listaMovimientos)
                {
                    if (item.OrigenesMultiples.Count > 0 && item.Destino != null) { MovimientoStockBLL.InsertarPlanificado(item, transaccion); }
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

        public static void Actualizar(ArbolProduccion arbol)
        {

        }

        public static void Guardar(ArbolProduccion arbol)
        {
            if (arbol.OrdenProduccion.Numero > 0) { Insertar(arbol); }
            else { Actualizar(arbol); }
        }

        public static void Eliminar(int numeroOrdenProduccion)
        {
            //revisar que condiciones hacen faltan para poder elimiarse - gonzalo
            DAL.OrdenProduccionDAL.Eliminar(numeroOrdenProduccion);
        }

        public static void Eliminar(IList<Entidades.OrdenProduccion> ordenesProduccion)
        {
            //revisar que condiciones hacen faltan para poder elimiarse - gonzalo
            DAL.OrdenProduccionDAL.Eliminar(ordenesProduccion);
        }

        public static IList<ExcepcionesPlan> IniciarOrdenProduccion(OrdenProduccion ordenP, DateTime fechaInicioReal)
        {
            SqlTransaction transaccion = null;
            IList<ExcepcionesPlan> excepciones = CheckExceptions(ordenP);

            if (excepciones.Count == 0)
            {
                try
                {
                    EstadoOrdenTrabajo estadoEnProceso = EstadoOrdenTrabajoBLL.GetEstado(OrdenesTrabajoEnum.EstadoOrdenEnum.EnProceso);
                    if (ordenP.Estado.Codigo == (int)OrdenesTrabajoEnum.EstadoOrdenEnum.Generada)
                    {
                        ordenP.Estado = estadoEnProceso;
                        ordenP.FechaInicioReal = fechaInicioReal;
                    }

                    EstadoOrdenTrabajo estadoEnEspera = EstadoOrdenTrabajoBLL.GetEstado(OrdenesTrabajoEnum.EstadoOrdenEnum.EnEspera);
                    EstadoMovimientoStock estadoMvto = EstadoMovimientoStockBLL.GetEstadoEntity(StockEnum.EstadoMovimientoStock.EnProceso);
                    ArbolEstructura arbolEstructura = EstructuraBLL.GetArbolEstructuraByCocina(ordenP.Cocina.CodigoCocina, false);

                    DateTime minDate = ordenP.OrdenesTrabajo.Min(p => p.FechaInicioEstimada).Value;

                    foreach (OrdenTrabajo ordenT in ordenP.OrdenesTrabajo.Where(p => p.FechaInicioEstimada == minDate).ToList())
                    {
                        ordenT.MovimientosStock = MovimientoStockBLL.GetMovimientosByOwner(EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.OrdenTrabajo, ordenT.Numero, transaccion));
                    }

                    transaccion = DAL.DB.IniciarTransaccion();

                    DAL.OrdenProduccionDAL.IniciarOrdenProduccion(ordenP, transaccion);
                    
                    if (ordenP.DetallePlanSemanal.DetallePedido != null)
                    {
                        DAL.PedidoDAL.ActualizarDetallePedidoAEnCurso(Convert.ToInt32(ordenP.DetallePlanSemanal.DetallePedido.Codigo), transaccion);
                    }

                    foreach (OrdenTrabajo ordenT in ordenP.OrdenesTrabajo.Where(p => p.FechaInicioEstimada == minDate).ToList())
                    {
                        ordenT.Estado = estadoEnProceso;
                        ordenT.FechaInicioReal = fechaInicioReal;

                        OrdenTrabajoBLL.IniciarOrdenTrabajo(ordenT, transaccion);

                        foreach (MovimientoStock mvto in ordenT.MovimientosStock)
                        {
                            mvto.Estado = estadoMvto;

                            foreach (OrigenMovimiento origenMvto in mvto.OrigenesMultiples)
                            {
                                origenMvto.CantidadReal = origenMvto.CantidadEstimada;
                                origenMvto.FechaReal = origenMvto.FechaPrevista;
                            }

                            MovimientoStockBLL.Iniciar(mvto, transaccion);
                        }
                    }

                    foreach (OrdenTrabajo ordenT in ordenP.OrdenesTrabajo.Where(p => p.FechaInicioEstimada != minDate).ToList())
                    {
                        ordenT.Estado = estadoEnEspera;
                        OrdenTrabajoBLL.ActualizarEstado(ordenT, transaccion);
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

            return excepciones;
        }

        private static IList<ExcepcionesPlan> CheckExceptions(OrdenProduccion ordenP)
        {
            IList<ExcepcionesPlan> excepciones = new List<ExcepcionesPlan>();
            
            DateTime minDate = ordenP.OrdenesTrabajo.Min(p => p.FechaInicioEstimada).Value;

            foreach (OrdenTrabajo ordenT in ordenP.OrdenesTrabajo.Where(p => p.FechaInicioEstimada == minDate).ToList())
            {
                ordenT.MovimientosStock = MovimientoStockBLL.GetMovimientosByOwner(EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.OrdenTrabajo, ordenT.Numero, null));
            }

            foreach (OrdenTrabajo ordenT in ordenP.OrdenesTrabajo.Where(p => p.FechaInicioEstimada == minDate).ToList())
            {
                foreach (MovimientoStock mvto in ordenT.MovimientosStock)
                {
                    foreach (OrigenMovimiento origenMvto in mvto.OrigenesMultiples)
                    {
                        if (origenMvto.Entidad.TipoEntidad.Codigo == (int)EntidadEnum.TipoEntidadEnum.UbicacionStock)
                        {
                            if ((origenMvto.Entidad.EntidadExterna as UbicacionStock).CantidadReal < origenMvto.CantidadEstimada)
                            {
                                excepciones.Add(ExcepcionesPlanBLL.Add_ExcepcionStockInsuficiente((origenMvto.Entidad.EntidadExterna as UbicacionStock).Nombre));
                            }
                        }
                    }
                }
            }

            return excepciones;
        }
        
        public static void FinalizarOrdenProduccion(OrdenProduccion ordenP)
        {
            SqlTransaction transaccion = null;            

            try
            {
                foreach (OrdenTrabajo ordenT in ordenP.OrdenesTrabajo)
                {
                    ordenT.MovimientosStock = MovimientoStockBLL.GetMovimientosByOwner(EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.OrdenTrabajo, ordenT.Numero, transaccion));
                }

                EstadoOrdenTrabajo estadoEnProceso = EstadoOrdenTrabajoBLL.GetEstado(OrdenesTrabajoEnum.EstadoOrdenEnum.EnProceso);
                EstadoOrdenTrabajo estadoFinalizada = EstadoOrdenTrabajoBLL.GetEstado(OrdenesTrabajoEnum.EstadoOrdenEnum.Finalizada);
                EstadoMovimientoStock estadoMvtoProceso = EstadoMovimientoStockBLL.GetEstadoEntity(StockEnum.EstadoMovimientoStock.EnProceso);
                EstadoMovimientoStock estadoMvtoFinalizado = EstadoMovimientoStockBLL.GetEstadoEntity(StockEnum.EstadoMovimientoStock.Finalizado);

                transaccion = DAL.DB.IniciarTransaccion();

                ordenP.CantidadReal = ordenP.CantidadEstimada;
                ordenP.Estado = estadoFinalizada;
                ordenP.FechaFinReal = ordenP.FechaFinEstimada;

                DAL.OrdenProduccionDAL.FinalizarOrdenProduccion(ordenP, transaccion);

                DAL.DetallePlanSemanalDAL.SumarCantidadFinalizada(ordenP.DetallePlanSemanal.Codigo, ordenP.Cocina.CodigoCocina, ordenP.CantidadReal, transaccion);

                if (ordenP.DetallePlanSemanal.DetallePedido != null)
                {
                    DAL.PedidoDAL.ActualizarDetallePedidoAFinalizado(Convert.ToInt32(ordenP.DetallePlanSemanal.DetallePedido.Codigo), transaccion);
                }
                
                //finalizamos las iniciadas
                foreach (OrdenTrabajo ordenT in ordenP.OrdenesTrabajo.Where(p => p.Estado.Codigo == (int)OrdenesTrabajoEnum.EstadoOrdenEnum.EnProceso))
                {
                    ordenT.CantidadReal = ordenT.CantidadEstimada;
                    ordenT.Estado = estadoFinalizada;
                    ordenT.FechaFinReal = ordenT.FechaFinEstimada;

                    if (ordenT.CierresParciales == null || (ordenT.CierresParciales != null && ordenT.CierresParciales.Count == 0))
                    {
                        ordenT.CierresParciales = new SortableBindingList<CierreParcialOrdenTrabajo>();
                        CierreParcialOrdenTrabajo cierre = new CierreParcialOrdenTrabajo();
                        cierre.Cantidad = ordenT.CantidadEstimada;
                        cierre.Codigo = 0;
                        cierre.Empleado = new Empleado() { Codigo = 2 };
                        cierre.Fecha = ordenT.FechaInicioEstimada.Value;
                        cierre.Maquina = new Maquina() { Codigo = 2 };
                        cierre.Observaciones = "";
                        cierre.OperacionesFallidas = 10;
                        cierre.OrdenTrabajo = ordenT;

                        OrdenTrabajoBLL.RegistrarCierreParcial(cierre, transaccion);
                    }

                    foreach (MovimientoStock mvto in ordenT.MovimientosStock)
                    {
                        mvto.CantidadDestinoReal = mvto.CantidadDestinoEstimada;
                        mvto.FechaReal = mvto.FechaPrevista;
                        mvto.Estado = estadoMvtoFinalizado;
                    }

                    OrdenTrabajoBLL.Finalizar(ordenT, transaccion);
                }

                //Iniciamos las en espera
                foreach (OrdenTrabajo ordenT in ordenP.OrdenesTrabajo.Where(p => p.Estado.Codigo == (int)OrdenesTrabajoEnum.EstadoOrdenEnum.EnEspera))
                {
                    ordenT.Estado = estadoEnProceso;
                    ordenT.FechaInicioReal = ordenT.FechaInicioEstimada.Value;

                    OrdenTrabajoBLL.IniciarOrdenTrabajo(ordenT, transaccion);
                    
                    foreach (MovimientoStock mvto in ordenT.MovimientosStock)
                    {
                        mvto.Estado = estadoMvtoProceso;

                        foreach (OrigenMovimiento origenMvto in mvto.OrigenesMultiples)
                        {
                            origenMvto.CantidadReal = origenMvto.CantidadEstimada;
                            origenMvto.FechaReal = origenMvto.FechaPrevista;
                        }

                        MovimientoStockBLL.Iniciar(mvto, transaccion);
                    }
                }

                //Y finalizamos todas
                foreach (OrdenTrabajo ordenT in ordenP.OrdenesTrabajo.Where(p => p.Estado.Codigo == (int)OrdenesTrabajoEnum.EstadoOrdenEnum.EnProceso))
                {
                    //ordenT.CantidadReal = ordenT.CantidadEstimada;
                    ordenT.Estado = estadoFinalizada;
                    ordenT.FechaFinReal = ordenT.FechaFinEstimada;

                    if (ordenT.CierresParciales == null || (ordenT.CierresParciales != null && ordenT.CierresParciales.Count == 0))
                    {
                        ordenT.CierresParciales = new SortableBindingList<CierreParcialOrdenTrabajo>();
                        CierreParcialOrdenTrabajo cierre = new CierreParcialOrdenTrabajo();
                        cierre.Cantidad = ordenT.CantidadEstimada;
                        cierre.Codigo = 0;
                        cierre.Empleado = new Empleado() { Codigo = 2 };
                        cierre.Fecha = ordenT.FechaInicioEstimada.Value;
                        cierre.Maquina = new Maquina() { Codigo = 2 };
                        cierre.Observaciones = "";
                        cierre.OperacionesFallidas = 10;
                        cierre.OrdenTrabajo = ordenT;

                        OrdenTrabajoBLL.RegistrarCierreParcial(cierre, transaccion);
                    }

                    foreach (MovimientoStock mvto in ordenT.MovimientosStock)
                    {
                        mvto.CantidadDestinoReal = mvto.CantidadDestinoEstimada;
                        mvto.FechaReal = mvto.FechaPrevista;
                        mvto.Estado = estadoMvtoFinalizado;
                    }
                    
                    OrdenTrabajoBLL.Finalizar(ordenT, transaccion);
                }

                //Actualizamos la eficiencia de los centros involucrados
                foreach (OrdenTrabajo ordenT in ordenP.OrdenesTrabajo)
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

        public static SortableBindingList<ArbolProduccion> GenerarOrdenesProduccion(int codigoDia, Data.dsPlanSemanal dsPlanSemanal, IList<Cocina> listaCocinas, IList<ExcepcionesPlan> listaExcepciones)
        {
            int numeroOrdenProduccion = 1;
            SortableBindingList<ArbolProduccion> ordenesProduccion = new SortableBindingList<ArbolProduccion>();
            EstadoOrdenTrabajo estadoOrden = EstadoOrdenTrabajoBLL.GetEstado(OrdenesTrabajoEnum.EstadoOrdenEnum.Generada);

            foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow rowDetalle in (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow[])dsPlanSemanal.DETALLE_PLANES_SEMANALES.Select("diapsem_codigo = " + codigoDia))
            {
                //Primero controlamos si no tiene órdenes
                if (rowDetalle.DPSEM_ESTADO == BLL.DetallePlanSemanalBLL.estadoGenerado)
                {
                    //No tiene órdenes, controlamos si la cocina tiene una estructura activa
                    Cocina cocina = listaCocinas.First(p => p.CodigoCocina == Convert.ToInt32(rowDetalle.COC_CODIGO));
                    int codigoEstructura = CocinaBLL.ObtenerCodigoEstructuraActiva(cocina.CodigoCocina);
                    ArbolEstructura arbolEstructura = EstructuraBLL.GetArbolEstructuraByEstructura(codigoEstructura, true);
                    string mensaje = string.Empty;
                    if (codigoEstructura == 0)
                    {
                        listaExcepciones.Add(ExcepcionesPlanBLL.Add_ExcepcionCocinaSinEstructura(cocina.CodigoProducto));
                    }

                    DateTime fecha = dsPlanSemanal.DIAS_PLAN_SEMANAL.FindByDIAPSEM_CODIGO(codigoDia).DIAPSEM_FECHA;
                    fecha = DateTime.Parse(string.Concat(fecha.ToShortDateString(), " 08:00:00"));

                    ordenesProduccion.Add(new ArbolProduccion()
                    {
                        OrdenProduccion = new OrdenProduccion()
                                            {
                                                Numero = numeroOrdenProduccion,
                                                Codigo = string.Concat("OPA-", rowDetalle.DPSEM_COD_NEMONICO),
                                                Estado = estadoOrden,
                                                FechaAlta = DBBLL.GetFechaServidor(),
                                                DetallePlanSemanal = new DetallePlanSemanal() { 
                                                    Codigo = Convert.ToInt32(rowDetalle.DPSEM_CODIGO), 
                                                    DetallePedido = (rowDetalle.IsDPED_CODIGONull()) ? null : new DetallePedido() { Codigo = long.Parse(rowDetalle.DPED_CODIGO.ToString()), FechaEntregaPrevista = rowDetalle.DETALLE_PEDIDOSRow.DPED_FECHA_ENTREGA_PREVISTA } 
                                                                                              },
                                                Origen = string.Concat("GA / ", (rowDetalle.IsDPED_CODIGONull()) ? "Planificación" : string.Concat("Pedido ", rowDetalle.DPED_CODIGO)),
                                                FechaInicioReal = null,
                                                FechaFinReal = null,
                                                FechaInicioEstimada = fecha,
                                                FechaFinEstimada = fecha,
                                                Prioridad = 0,
                                                Observaciones = string.Empty,
                                                Cocina = cocina,
                                                CantidadEstimada = Convert.ToInt32(rowDetalle.DPSEM_CANTIDADESTIMADA),
                                                CantidadReal = 0,
                                                Estructura = codigoEstructura, 
                                                UbicacionStock = arbolEstructura.NodoRaiz.Compuesto.Parte.HojaRuta.UbicacionStock
                                            },
                        OrdenesTrabajo = new List<NodoOrdenTrabajo>()
                    });
                    rowDetalle.BeginEdit();
                    rowDetalle.DPSEM_ESTADO = BLL.DetallePlanSemanalBLL.estadoConOrden;
                    rowDetalle.EndEdit();                    
                }
                numeroOrdenProduccion++;
            }

            return ordenesProduccion;
        }
        
        public static ArbolProduccion GetArbolProduccion(OrdenProduccion ordenProduccion)
        {
            return new ArbolProduccion();
        }
    }
}
