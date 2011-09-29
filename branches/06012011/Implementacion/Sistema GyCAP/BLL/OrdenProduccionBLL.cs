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

namespace GyCAP.BLL
{
    public class OrdenProduccionBLL
    {
        public static void ObtenerOrdenesProduccion(object codigo, object estado, object modo, object fechaGeneracion, object fechaDesde, object fechaHasta, Data.dsOrdenTrabajo dsOrdenTrabajo)
        {
            if (estado != null && Convert.ToInt32(estado) <= 0) { estado = null; }
            if (modo != null && Convert.ToInt32(modo) <= 0) { modo = null; }
            DAL.OrdenProduccionDAL.ObtenerOrdenesProduccion(codigo, estado, modo, fechaGeneracion, fechaDesde, fechaHasta, dsOrdenTrabajo);
            foreach (Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow row in dsOrdenTrabajo.ORDENES_PRODUCCION)
            {
                OrdenTrabajoBLL.ObtenerOrdenesTrabajo(Convert.ToInt32(row.ORDP_NUMERO), dsOrdenTrabajo, true);
            }

            foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow row in dsOrdenTrabajo.ORDENES_TRABAJO)
            {
                CierreParcialOrdenTrabajoBLL.ObtenerCierresParcialesOrdenTrabajo(Convert.ToInt32(row.ORDT_NUMERO), dsOrdenTrabajo.CIERRE_ORDEN_TRABAJO);
            }
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

        /*public static void IniciarOrdenProduccion(int numeroOrdenProduccion, DateTime fechaInicioReal, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsStock dsStock, Data.dsHojaRuta dsHojaRuta, Data.dsEstructura dsEstructura)
        {
            int codigoMovimiento = -1;
            
            //Iniciamos la orden de producción
            dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_FECHAINICIOREAL = fechaInicioReal;
            dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).EORD_CODIGO = EstadoEnProceso;

            //Obtenemos la estructura entera de la cocina a fabricar
            BLL.EstructuraBLL.ObtenerEstructura(Convert.ToInt32(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ESTR_CODIGO), dsEstructura, true);
            //Iniciamos las órdenes de trabajo
            string filtro = "ORDP_NUMERO = " + numeroOrdenProduccion + " AND ORDT_FECHAINICIOESTIMADA = MIN (ORDT_FECHAINICIOESTIMADA)";
            decimal ultimaParte = 0, stockDestino = 0, cantidadDestino = 0;
            foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOT in (Data.dsOrdenTrabajo.ORDENES_TRABAJORow[])dsOrdenTrabajo.ORDENES_TRABAJO.Select(filtro))
	        {
                //Cambiamos el estado de la orden de trabajo a EnProceso y anotamos la fecha real de inicio
                rowOT.EORD_CODIGO = EstadoEnProceso;
                rowOT.ORDT_FECHAINICIOREAL = fechaInicioReal;
                
                //Realizamos los movimientos de stock correspondientes y actualizamos las cantidades
                //Como son las órdenes de la/s primera/s operación/es de la parte tenemos que descontar el stock de cada
                //materia prima que usa cada parte, que sería si o si una pieza en este punto de ejecución, pero sólo de la primer
                //operación de la hoja de ruta
                if (rowOT.PAR_CODIGO != ultimaParte)
                {                    
                    foreach (Data.dsEstructura.MATERIASPRIMASXPIEZARow rowMPxP in dsEstructura.PIEZAS.FindByPZA_CODIGO(rowOT.PAR_CODIGO).GetMATERIASPRIMASXPIEZARows())
                    {
                        //Creamos un movimiento de stock para cada materia prima según su ubicación de stock
                        if (!rowMPxP.MATERIAS_PRIMASRow.IsUSTCK_NUMERONull())
                        {
                            Data.dsStock.MOVIMIENTOS_STOCKRow rowMovimiento = dsStock.MOVIMIENTOS_STOCK.NewMOVIMIENTOS_STOCKRow();
                            rowMovimiento.BeginEdit();
                            rowMovimiento.MVTO_NUMERO = codigoMovimiento--;
                            //rowMovimiento.EMVTO_CODIGO = EstadoMovimientoStockBLL.EstadoPlanificado;
                            rowMovimiento.MVTO_CODIGO = "MSA/OT- " + rowOT.ORDT_CODIGO;
                            rowMovimiento.MVTO_DESCRIPCION = "Automático. Origen: Orden de Trabajo.";
                            rowMovimiento.MVTO_FECHAALTA = DBBLL.GetFechaServidor();
                            rowMovimiento.MVTO_FECHAPREVISTA = rowOT.ORDT_FECHAFINESTIMADA;
                            rowMovimiento.SetMVTO_FECHAREALNull();
                            //rowMovimiento.USTCK_ORIGEN = rowMPxP.MATERIAS_PRIMASRow.USTCK_NUMERO;
                            //if (!rowOT.IsUSTCK_DESTINONull()) { rowMovimiento.USTCK_DESTINO = rowOT.USTCK_DESTINO; }
                            //else 
                            //{
                                //Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOTTemp = rowOT;
                                //while (rowOTTemp.PAR_CODIGO == rowOTTemp.ORDENES_TRABAJORowParent.PAR_CODIGO && rowOTTemp.PAR_TIPO == rowOTTemp.ORDENES_TRABAJORowParent.PAR_TIPO && rowOTTemp.ORDENES_TRABAJORowParent.IsUSTCK_DESTINONull())
                                //{
                                //    rowOTTemp = rowOTTemp.ORDENES_TRABAJORowParent;
                                //}
                                //rowMovimiento.USTCK_DESTINO = rowOTTemp.ORDENES_TRABAJORowParent.USTCK_DESTINO;
                            //}
                            rowMovimiento.MVTO_CANTIDAD_ORIGEN_ESTIMADA = rowMPxP.MPXP_CANTIDAD * rowOT.ORDT_CANTIDADESTIMADA;
                            rowMovimiento.MVTO_CANTIDAD_DESTINO_ESTIMADA = rowOT.ORDT_CANTIDADESTIMADA;
                            rowMovimiento.MVTO_CANTIDAD_ORIGEN_REAL = 0;
                            rowMovimiento.MVTO_CANTIDAD_DESTINO_REAL = 0;
                            //rowMovimiento.ORDT_NUMERO = rowOT.ORDT_NUMERO;
                            rowMovimiento.EndEdit();
                            dsStock.MOVIMIENTOS_STOCK.AddMOVIMIENTOS_STOCKRow(rowMovimiento);

                            //Actualizamos los datos de la ubicación de stock origen
                            //dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(rowMovimiento.USTCK_ORIGEN).USTCK_CANTIDADVIRTUAL -= rowMovimiento.MVTO_CANTIDAD_ORIGEN_ESTIMADA;
                            //if (!rowMovimiento.IsUSTCK_DESTINONull())
                            ///{
                            //    stockDestino = rowMovimiento.USTCK_DESTINO;
                           //     cantidadDestino = rowMovimiento.MVTO_CANTIDAD_DESTINO_ESTIMADA;
                            //}
                        }
                    }
                    //Actualizamos los datos de la ubicación de stock destino, que para todos los casos anteriores es el mismo
                    //dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(stockDestino).USTCK_CANTIDADVIRTUAL += cantidadDestino;
                    ultimaParte = rowOT.PAR_CODIGO;
                }
	        }

            //Actualizamos el estado de las demás órdenes de trabajo de la orden de producción a EnEspera
            foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow row in (Data.dsOrdenTrabajo.ORDENES_TRABAJORow[])dsOrdenTrabajo.ORDENES_TRABAJO.Select("ORDP_NUMERO = " + numeroOrdenProduccion))
            {
                if (row.EORD_CODIGO == EstadoGenerado) { row.EORD_CODIGO = EstadoEnEspera; }
            }
            
            //Ahora mandamos todo a la BD
            DAL.OrdenProduccionDAL.IniciarOrdenProduccion(numeroOrdenProduccion, dsOrdenTrabajo, dsStock);
        }*/

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
        
        public static ArbolProduccion GetArbolProduccion()
        {
            return new ArbolProduccion();
        }
    }
}
