using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades.Excepciones;
using GyCAP.Entidades;

namespace GyCAP.DAL
{
    public class OrdenTrabajoDAL
    {        
        public static void Insertar(OrdenTrabajo orden, SqlTransaction transaccion)
        {
            string sql = @"INSERT INTO ORDENES_TRABAJO 
                         ([ordt_codigo]
                         ,[ordp_numero]
                         ,[eord_codigo]
                         ,[part_numero]
                         ,[ordt_origen]
                         ,[ordt_cantidadestimada]
                         ,[ordt_cantidadreal]
                         ,[ordt_fechainicioestimada]
                         ,[ordt_fechainicioreal]
                         ,[ordt_fechafinestimada]
                         ,[ordt_fechafinreal]
                         ,[ordt_observaciones]
                         ,[ordt_secuencia]
                         ,[dhr_codigo]
                         ,[ordt_numero_padre]
                         ,[ordt_tipo])
                         VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15) SELECT @@Identity";

            object fechainicioreal = DBNull.Value, fechafinreal = DBNull.Value, hr = DBNull.Value, padre = DBNull.Value;
            if (orden.FechaInicioReal.HasValue) { fechainicioreal = orden.FechaInicioReal.Value.ToShortDateString(); }
            if (orden.FechaFinReal.HasValue) { fechafinreal = orden.FechaInicioReal.Value.ToShortDateString(); }
            if (orden.DetalleHojaRuta != null) { hr = orden.DetalleHojaRuta.Codigo; }
            if (orden.OrdenTrabajoPadre > 0) { padre = orden.OrdenTrabajoPadre; }
            orden.Origen = orden.Origen.Replace("OPA", string.Concat("OPA", orden.OrdenProduccion));

            object[] valoresParametros = { 
                                             orden.Codigo,
                                             orden.OrdenProduccion,
                                             orden.Estado.Codigo,
                                             orden.Parte.Numero,
                                             orden.Origen,
                                             orden.CantidadEstimada,
                                             orden.CantidadReal,
                                             orden.FechaInicioEstimada.Value.ToShortDateString(),
                                             fechainicioreal,
                                             orden.FechaFinEstimada.Value.ToShortDateString(),
                                             fechafinreal,
                                             orden.Observaciones,
                                             orden.Secuencia,
                                             hr,
                                             padre,
                                             orden.Tipo
                                         };

            orden.Numero = Convert.ToInt32(DB.executeScalar(sql, valoresParametros, transaccion));
            orden.Codigo = string.Concat("OTA", orden.Numero);
            ActualizarCodigo(orden.Codigo, orden.Numero, transaccion);
        }

        private static void ActualizarCodigo(string codigo, int numeroOrden, SqlTransaction transaccion)
        {
            string sql = "UPDATE ORDENES_TRABAJO SET ordt_codigo = @p0 WHERE ordt_numero = @p1";
            object[] parametros = { codigo, numeroOrden };

            DB.executeNonQuery(sql, parametros, transaccion);
        }
        
        public static void IniciarOrdenTrabajo(int numeroOrdenTrabajo, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsStock dsStock, SqlTransaction transaccion)
        {
            string sql = "UPDATE ORDENES_TRABAJO SET eord_codigo = @p0, ordt_fechainicioreal = @p1 WHERE ordt_numero = @p2";
            object[] parametros = { dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).EORD_CODIGO, 
                                     dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).ORDT_FECHAINICIOREAL, 
                                     numeroOrdenTrabajo };

            DB.executeNonQuery(sql, parametros, transaccion);

            //Insertamos los movimientos de stock que corresponden a la OT
            Entidades.MovimientoStock movimiento;
            foreach (Data.dsStock.MOVIMIENTOS_STOCKRow rowMVTO in (Data.dsStock.MOVIMIENTOS_STOCKRow[])dsStock.MOVIMIENTOS_STOCK.Select("ORDT_NUMERO = " + numeroOrdenTrabajo))
            {
                movimiento = new GyCAP.Entidades.MovimientoStock();
                movimiento.Codigo = rowMVTO.MVTO_CODIGO;
                movimiento.Descripcion = rowMVTO.MVTO_DESCRIPCION;
                movimiento.FechaAlta = rowMVTO.MVTO_FECHAALTA;
                movimiento.FechaPrevista = rowMVTO.MVTO_FECHAPREVISTA;
                //movimiento.Origen = new GyCAP.Entidades.UbicacionStock(Convert.ToInt32(rowMVTO.USTCK_ORIGEN));
                //movimiento.Destino = new GyCAP.Entidades.UbicacionStock(Convert.ToInt32(rowMVTO.USTCK_DESTINO));
                movimiento.CantidadOrigenEstimada = rowMVTO.MVTO_CANTIDAD_ORIGEN_ESTIMADA;
                movimiento.CantidadOrigenReal = 0;
                movimiento.CantidadDestinoEstimada = rowMVTO.MVTO_CANTIDAD_DESTINO_ESTIMADA;
                movimiento.CantidadDestinoReal = 0;
                movimiento.Estado = new GyCAP.Entidades.EstadoMovimientoStock(Convert.ToInt32(rowMVTO.EMVTO_CODIGO));
                //movimiento.OrdenTrabajo = new GyCAP.Entidades.OrdenTrabajo(Convert.ToInt32(rowMVTO.ORDT_NUMERO));
                //MovimientoStockDAL.Crear(movimiento, transaccion);
                rowMVTO.MVTO_NUMERO = movimiento.Numero;
            }
            //Actualizamos la ubicación destino de la orden de trabajo
            //UbicacionStockDAL.ActualizarCantidadesStock(Convert.ToInt32(dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).USTCK_DESTINO),
                                               //         0,
                                               //         dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).ORDT_CANTIDADESTIMADA,
                                               //         transaccion);
        }

        public static void ActualizarEstado(int numeroOrdenTrabajo, int codigoEstado, SqlTransaction transaccion)
        {
            string sql = "UPDATE ORDENES_TRABAJO SET eord_codigo = @p0 WHERE ordt_numero = @p1";
            object[] parametros = { codigoEstado, numeroOrdenTrabajo };
            DB.executeNonQuery(sql, parametros, transaccion);
        }
        
        public static void ObtenerOrdenesTrabajo(int numeroOrdenProduccion, Data.dsOrdenTrabajo dsOrdenTrabajo, bool obtenerCierresParciales)
        {
            string sql = @"SELECT ordt_numero, ordt_codigo, ordp_numero, eord_codigo, part_numero, ordt_origen, ordt_cantidadestimada,
                         ordt_cantidadreal, ordt_fechainicioestimada, ordt_fechainicioreal, ordt_fechafinestimada, ordt_fechafinreal,
                         ordt_observaciones, ordt_secuencia, dhr_codigo, ordt_numero_padre, ordt_tipo  
                        FROM ORDENES_TRABAJO WHERE ordp_numero = @p0 ORDER BY ordt_fechainicioestimada ASC";

            object[] parametros = { numeroOrdenProduccion };

            try
            {
                DB.FillDataTable(dsOrdenTrabajo.ORDENES_TRABAJO, sql, parametros);
                foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOT in dsOrdenTrabajo.ORDENES_TRABAJO.Rows)
                {
                    if (obtenerCierresParciales) { CierreParcialOrdenTrabajoDAL.ObtenerCierresParcialesOrdenTrabajo(Convert.ToInt32(rowOT.ORDT_NUMERO), dsOrdenTrabajo.CIERRE_ORDEN_TRABAJO); }
                }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
        public static void RegistrarCierreParcial(CierreParcialOrdenTrabajo cierre)
        {
            string sql = "UPDATE ORDENES_TRABAJO SET ordt_cantidadreal = ordt_cantidadreal + @p0 WHERE ordt_numero = @p1";

            object[] parametros = { cierre.Cantidad, cierre.OrdenTrabajo.Numero };

            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                //Insertamos el cierre parcial                
                //CierreParcialOrdenTrabajoDAL.Insertar(cierre, transaccion);                
                
                //Actualizamos las cantidades de la OT
                //DB.executeNonQuery(sql, parametros, transaccion);
                cierre.OrdenTrabajo.CantidadReal += (int)cierre.Cantidad;

                //Obtenemos los movimientos de stock de la orden de trabajo, actualizamos cantidades y las ubicaciones                

                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
            finally
            {
                DB.FinalizarTransaccion();
            }       
        }

        public static void FinalizarOrdenTrabajo(int numeroOrdenTrabajo, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsStock dsStock, SqlTransaction transaccion)
        {
            string sql = @"UPDATE ORDENES_TRABAJO SET 
                         eord_codigo = @p0
                        ,ordt_fechainicioreal = ordt_fechainicioestimada
                        ,ordt_cantidadreal = ordt_cantidadestimada
                        ,ordt_fechafinreal = ordt_fechafinestimada 
                        WHERE ordt_numero = @p1";

            object[] parametros = { (int)OrdenesTrabajoEnum.EstadoOrdenEnum.Finalizada, numeroOrdenTrabajo };

            //Finalizo la orden de trabajo
            DB.executeNonQuery(sql, parametros, transaccion);

            if (dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).EORD_CODIGO == (int)OrdenesTrabajoEnum.EstadoOrdenEnum.EnProceso)
            {
                //*************FINALIZACIÓN ÓRDENES INICIADAS*************************************************

                dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).EORD_CODIGO = (int)OrdenesTrabajoEnum.EstadoOrdenEnum.Finalizada;
                dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).ORDT_CANTIDADREAL = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).ORDT_CANTIDADESTIMADA;
                dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).ORDT_FECHAFINREAL = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).ORDT_FECHAFINESTIMADA;
                                
                //Actualizo los movimientos generados para cada orden de trabajo
                MovimientoStockDAL.ObtenerMovimientosStockOrdenTrabajo(Convert.ToInt32(numeroOrdenTrabajo), dsStock.MOVIMIENTOS_STOCK);
                decimal cantidadOrigen = 0, cantidadDestino = 0, ubicacionDestino = 0;
                foreach (Data.dsStock.MOVIMIENTOS_STOCKRow rowMVTO in (Data.dsStock.MOVIMIENTOS_STOCKRow[])dsStock.MOVIMIENTOS_STOCK.Select("ordt_numero = " + numeroOrdenTrabajo))
                {
                    cantidadDestino = rowMVTO.MVTO_CANTIDAD_DESTINO_ESTIMADA - rowMVTO.MVTO_CANTIDAD_DESTINO_REAL;
                    cantidadOrigen = rowMVTO.MVTO_CANTIDAD_ORIGEN_ESTIMADA - rowMVTO.MVTO_CANTIDAD_ORIGEN_REAL;
                    //MovimientoStockDAL.FinalizarMovimiento(Convert.ToInt32(rowMVTO.MVTO_NUMERO), transaccion);
                    rowMVTO.MVTO_CANTIDAD_DESTINO_REAL = rowMVTO.MVTO_CANTIDAD_DESTINO_ESTIMADA;
                    rowMVTO.MVTO_CANTIDAD_ORIGEN_REAL = rowMVTO.MVTO_CANTIDAD_ORIGEN_ESTIMADA;
                    rowMVTO.MVTO_FECHAREAL = rowMVTO.MVTO_FECHAPREVISTA;
                    rowMVTO.EMVTO_CODIGO = MovimientoStockDAL.EstadoFinalizado;
                    //ubicacionDestino = rowMVTO.USTCK_DESTINO;
                    //UbicacionStockDAL.ActualizarCantidadesStock(Convert.ToInt32(rowMVTO.USTCK_ORIGEN), (cantidadOrigen * -1), 0, transaccion);
                    //dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(rowMVTO.USTCK_ORIGEN).USTCK_CANTIDADREAL -= cantidadOrigen;
                }

                //Actualizo las ubicaciones de stock afectadas por la orden de trabajo
                cantidadDestino = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(ubicacionDestino).USTCK_CANTIDADREAL * -1;
                dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(ubicacionDestino).USTCK_CANTIDADREAL = 0;
                //decimal cantidadVirtual = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(ubicacionDestino).USTCK_CANTIDADVIRTUAL * -1;
                //UbicacionStockDAL.ActualizarCantidadesStock(Convert.ToInt32(ubicacionDestino), cantidadDestino, cantidadVirtual, transaccion);
            }
            else
            {
                //*************FINALIZACIÓN ÓRDENES EN ESPERA****************************************************
                dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).EORD_CODIGO = (int)OrdenesTrabajoEnum.EstadoOrdenEnum.Finalizada;
                dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).ORDT_CANTIDADREAL = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).ORDT_CANTIDADESTIMADA;
                dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).ORDT_FECHAFINREAL = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).ORDT_FECHAFINESTIMADA;
                dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).ORDT_FECHAINICIOREAL = dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).ORDT_FECHAINICIOESTIMADA;
            }
        }
    }
}
