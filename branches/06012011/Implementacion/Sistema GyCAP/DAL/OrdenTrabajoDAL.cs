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
                         ,[par_codigo]
                         ,[ordt_origen]
                         ,[ordt_cantidadestimada]
                         ,[ordt_cantidadreal]
                         ,[ordt_fechainicioestimada]
                         ,[ordt_fechainicioreal]
                         ,[ordt_fechafinestimada]
                         ,[ordt_fechafinreal]
                         ,[ordt_observaciones]
                         ,[ordt_secuencia]
                         ,[dhr_codigo])
                         VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13) SELECT @@Identity";
            
            object fechainicioreal = DBNull.Value, fechafinreal = DBNull.Value, hr = DBNull.Value;
            if (orden.FechaInicioReal.HasValue) { fechainicioreal = orden.FechaInicioReal.Value.ToShortDateString(); }
            if (orden.FechaFinReal.HasValue) { fechafinreal = orden.FechaInicioReal.Value.ToShortDateString(); }
            if (orden.DetalleHojaRuta != null) { hr = orden.DetalleHojaRuta.Codigo; }

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
                                             hr
                                         };

            orden.Numero = Convert.ToInt32(DB.executeScalar(sql, valoresParametros, transaccion));
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
            string sql = @"SELECT ordt_numero, ordt_codigo, ordp_numero, eord_codigo, par_codigo, par_tipo, ordt_origen, ordt_cantidadestimada
                         , ordt_cantidadreal, ordt_fechainicioestimada, ordt_fechainicioreal, ordt_fechafinestimada, ordt_fechafinreal
                         , ordt_horainicioestimada, ordt_horainicioreal, ordt_horafinestimada, ordt_horafinreal, estr_codigo, cto_codigo, opr_numero
                         , ordt_observaciones, ordt_ordensiguiente, ordt_nivel,ordt_secuencia, ustck_origen, ustck_destino, hr_codigo  
                        FROM ORDENES_TRABAJO WHERE ordp_numero = @p0";

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
        
        public static void RegistrarCierreParcial(int numeroOrdenTrabajo, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsStock dsStock)
        {
            string sql = "UPDATE ORDENES_TRABAJO SET ordt_cantidadreal = ordt_cantidadreal + @p0 WHERE ordt_numero = @p1";
            Data.dsOrdenTrabajo.CIERRE_ORDEN_TRABAJORow rowCierre = dsOrdenTrabajo.CIERRE_ORDEN_TRABAJO.GetChanges(DataRowState.Added).Rows[0] as Data.dsOrdenTrabajo.CIERRE_ORDEN_TRABAJORow;

            object[] parametros = { rowCierre.CORD_CANTIDAD, numeroOrdenTrabajo };


            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                //Insertamos el cierre parcial
                Entidades.CierreParcialOrdenTrabajo cierre = new GyCAP.Entidades.CierreParcialOrdenTrabajo();
                cierre.OrdenTrabajo = new GyCAP.Entidades.OrdenTrabajo();
                cierre.Empleado = new GyCAP.Entidades.Empleado();
                cierre.Empleado.Codigo = long.Parse(rowCierre.E_CODIGO.ToString());
                if (!rowCierre.IsMAQ_CODIGONull()) 
                {
                    cierre.Maquina = new GyCAP.Entidades.Maquina();
                    cierre.Maquina.Codigo = Convert.ToInt32(rowCierre.MAQ_CODIGO); 
                }
                cierre.Cantidad = rowCierre.CORD_CANTIDAD;
                cierre.Fecha = rowCierre.CORD_FECHACIERRE;
                cierre.Hora = rowCierre.CORD_HORACIERRE;
                cierre.Observaciones = rowCierre.CORD_OBSERVACIONES;                
                CierreParcialOrdenTrabajoDAL.Insertar(cierre, transaccion);
                rowCierre.BeginEdit();
                rowCierre.CORD_CODIGO = cierre.Codigo;
                rowCierre.EndEdit();
                
                //Actualizamos las cantidades de la OT
                DB.executeNonQuery(sql, parametros, transaccion);
                dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).ORDT_CANTIDADREAL += rowCierre.CORD_CANTIDAD;

                //Obtenemos los movimientos de stock de la orden de trabajo, actualizamos cantidades y las ubicaciones de origen
                MovimientoStockDAL.ObtenerMovimientosStockOrdenTrabajo(Convert.ToInt32(numeroOrdenTrabajo), dsStock.MOVIMIENTOS_STOCK);
                decimal cantidadOrigen = 0, ubicacionDestino = 0;
                foreach (Data.dsStock.MOVIMIENTOS_STOCKRow rowMVTO in (Data.dsStock.MOVIMIENTOS_STOCKRow[])dsStock.MOVIMIENTOS_STOCK.Select("ORDT_NUMERO = " + rowCierre.ORDT_NUMERO))
                {
                    cantidadOrigen = (rowMVTO.MVTO_CANTIDAD_ORIGEN_ESTIMADA / rowMVTO.MVTO_CANTIDAD_DESTINO_ESTIMADA) * rowCierre.CORD_CANTIDAD;
                    MovimientoStockDAL.ActualizarCantidadesParciales(Convert.ToInt32(rowMVTO.MVTO_NUMERO), cantidadOrigen, rowCierre.CORD_CANTIDAD, transaccion);
                    //UbicacionStockDAL.ActualizarCantidadesStock(Convert.ToInt32(rowMVTO.USTCK_ORIGEN), (cantidadOrigen * -1), 0, transaccion);
                    //ubicacionDestino = rowMVTO.USTCK_DESTINO;
                    rowMVTO.MVTO_CANTIDAD_ORIGEN_REAL += cantidadOrigen;
                    rowMVTO.MVTO_CANTIDAD_DESTINO_REAL = rowCierre.CORD_CANTIDAD;
                    //dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(rowMVTO.USTCK_ORIGEN).USTCK_CANTIDADREAL -= cantidadOrigen;
                }

                //Actualizamos la ubicación de stock destino
                //UbicacionStockDAL.ActualizarCantidadesStock(Convert.ToInt32(ubicacionDestino), rowCierre.CORD_CANTIDAD, 0, transaccion);
                dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(ubicacionDestino).USTCK_CANTIDADREAL += rowCierre.CORD_CANTIDAD;

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
