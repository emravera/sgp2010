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
            DAL.OrdenProduccionDAL.Insertar(arbol);
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

        public static void IniciarOrdenProduccion(OrdenProduccion ordenP, DateTime fechaInicioReal)
        {            
            //Iniciamos la orden de producción
            ordenP.Estado = EstadoOrdenTrabajoBLL.GetEstado(OrdenesTrabajoEnum.EstadoOrdenEnum.EnProceso);
            EstadoOrdenTrabajo estadoEnEspera = EstadoOrdenTrabajoBLL.GetEstado(OrdenesTrabajoEnum.EstadoOrdenEnum.EnEspera);
            ArbolEstructura arbolEstructura = EstructuraBLL.GetArbolEstructura(ordenP.Cocina.CodigoCocina, false);

            //Iniciamos las órdenes de trabajo
            SqlTransaction transaccion = null;
            DateTime minDate = ordenP.OrdenesTrabajo.Min(p => p.FechaInicioEstimada).Value;

            try
            {
                transaccion = DAL.DB.IniciarTransaccion();
                foreach (OrdenTrabajo item in ordenP.OrdenesTrabajo.Where(p => p.FechaInicioEstimada == minDate).ToList())
                {
                    item.Estado = estadoEnEspera;
                    item.FechaInicioReal = fechaInicioReal;
                    if (item.DetalleHojaRuta.StockOrigen != null)
                    {
                        //actualizar stock
                    }
                    else
                    {
                        //si no tiene es porque el origen es una mp ??
                    }
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

        public static void FinalizarOrdenProduccion(int numeroOrdenProduccion, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsStock dsStock)
        {
            DAL.OrdenProduccionDAL.FinalizarOrdenProduccion(numeroOrdenProduccion, dsOrdenTrabajo, dsStock);
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
                    string mensaje = string.Empty;
                    if (codigoEstructura == 0)
                    {
                        listaExcepciones.Add(ExcepcionesPlanBLL.Add_ExcepcionCocinaSinEstructura(cocina.CodigoProducto));
                    }

                    ordenesProduccion.Add(new ArbolProduccion()
                    {
                        OrdenProduccion = new OrdenProduccion()
                                            {
                                                Numero = numeroOrdenProduccion,
                                                Codigo = string.Concat("OPA", numeroOrdenProduccion),
                                                Estado = estadoOrden,
                                                FechaAlta = DBBLL.GetFechaServidor(),
                                                DetallePlanSemanal = new DetallePlanSemanal() { 
                                                    Codigo = Convert.ToInt32(rowDetalle.DPSEM_CODIGO), 
                                                    DetallePedido = (rowDetalle.IsDPED_CODIGONull()) ? null : new DetallePedido() { Codigo = long.Parse(rowDetalle.DPED_CODIGO.ToString()) } 
                                                                                              },
                                                Origen = string.Concat("GA / ", (rowDetalle.IsDPED_CODIGONull()) ? "Planificación" : string.Concat("Pedido", rowDetalle.DPED_CODIGO)),
                                                FechaInicioReal = null,
                                                FechaFinReal = null,
                                                FechaInicioEstimada = null,
                                                FechaFinEstimada = null,
                                                Prioridad = 0,
                                                Observaciones = string.Empty,
                                                Cocina = cocina,
                                                CantidadEstimada = Convert.ToInt32(rowDetalle.DPSEM_CANTIDADESTIMADA),
                                                CantidadReal = 0,
                                                Estructura = codigoEstructura
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
