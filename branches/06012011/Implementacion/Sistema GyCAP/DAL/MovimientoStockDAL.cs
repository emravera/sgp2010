using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class MovimientoStockDAL
    {
        public static readonly int EstadoPlanificado = 1;
        public static readonly int EstadoFinalizado = 2;
        public static readonly int EstadoCancelado = 3;
        
        public static void InsertarPlanificado(Entidades.MovimientoStock movimientoStock)
        {
            try
            {
                Insertar(movimientoStock, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void InsertarFinalizado(Entidades.MovimientoStock movimientoStock)
        {
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                Insertar(movimientoStock, transaccion);
                Finalizar(movimientoStock, transaccion);
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

        private static void Insertar(Entidades.MovimientoStock movimientoStock, SqlTransaction transaccion)
        {
            string sql = @"INSERT INTO [MOVIMIENTOS_STOCK] 
                        ([mvto_codigo]
                        ,[mvto_descripcion]
                        ,[mvto_fechaalta]
                        ,[mvto_fechaprevista]
                        ,[mvto_fechareal]
                        ,[mvto_cantidad_origen_estimada]
                        ,[mvto_cantidad_destino_estimada]
                        ,[mvto_cantidad_origen_real]
                        ,[mvto_cantidad_destino_real]
                        ,[emvto_codigo]
                        ,[entd_origen]
                        ,[entd_destino]
                        ,[entd_duenio]
                        ,[mvto_oldquantity]) 
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13) SELECT @@Identity";

            object fechaPrevista = DBNull.Value, fechaReal = DBNull.Value, origen = DBNull.Value, destino = DBNull.Value, duenio = DBNull.Value;
            if (movimientoStock.FechaPrevista.HasValue) { fechaPrevista = movimientoStock.FechaPrevista.Value.ToShortDateString(); }
            if (movimientoStock.FechaReal.HasValue) { fechaReal = movimientoStock.FechaReal.Value.ToShortDateString(); }
            if (movimientoStock.Origen != null) { origen = movimientoStock.Origen.Codigo; }
            if (movimientoStock.Destino != null) { destino = movimientoStock.Destino.Codigo; }
            if (movimientoStock.Duenio != null) { duenio = movimientoStock.Duenio.Codigo; }

            object[] parametros = { 
                                      movimientoStock.Codigo,
                                      movimientoStock.Descripcion,
                                      movimientoStock.FechaAlta.ToShortDateString(),
                                      fechaPrevista,
                                      fechaReal,
                                      movimientoStock.CantidadOrigenEstimada,
                                      movimientoStock.CantidadDestinoEstimada,
                                      movimientoStock.CantidadOrigenReal,
                                      movimientoStock.CantidadDestinoReal,
                                      movimientoStock.Estado.Codigo,
                                      origen,
                                      destino,
                                      duenio,
                                      movimientoStock.OldQuantity
                                  };

            movimientoStock.Numero = Convert.ToInt32(DB.executeScalar(sql, parametros, transaccion));
        }

        public static void Eliminar(int numeroMovimiento)
        {
            string sql = "DELETE FROM MOVIMIENTOS_STOCK WHERE mvto_numero = @p0";
            object[] parametros = { numeroMovimiento };

            try
            {
                DB.executeNonQuery(sql, parametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void ObtenerMovimientosStockOrdenTrabajo(int numeroOrdenTrabajo, DataTable dtMovimientos)
        {
            string sql = @"SELECT mvto_numero, mvto_codigo, mvto_descripcion, mvto_fechaalta, mvto_fechaprevista, mvto_fechareal
                        ,ustck_origen, ustck_destino, mvto_cantidad_origen_estimada, mvto_cantidad_destino_estimada
                        ,mvto_cantidad_origen_real, mvto_cantidad_destino_real, emvto_codigo  
                        FROM MOVIMIENTOS_STOCK WHERE ordt_numero = @p0";

            object[] parametros = { numeroOrdenTrabajo };

            DB.FillDataTable(dtMovimientos, sql, parametros);
        }
        
        public static void ActualizarCantidadesParciales(int numeroMovimiento, decimal cantidadOrigenReal, decimal cantidadDestinoReal, SqlTransaction transaccion)
        {
            string sql = @"UPDATE MOVIMIENTOS_STOCK SET 
                         mvto_cantidad_origen_real  = mvto_cantidad_origen_real + @p0 
                        ,mvto_cantidad_destino_real = mvto_cantidad_destino_real + @p1 
                         WHERE mvto_numero = @p2";

            object[] parametros = { cantidadOrigenReal, cantidadDestinoReal, numeroMovimiento };
            DB.executeNonQuery(sql, parametros, transaccion);
        }

        public static void Finalizar(Entidades.MovimientoStock movimiento, SqlTransaction transaccion)
        {
            string sql = @"UPDATE MOVIMIENTOS_STOCK SET 
                         mvto_cantidad_origen_real  = @p0
                        ,mvto_cantidad_destino_real = @p1
                        ,mvto_fechareal = @p2
                        ,emvto_codigo = @p3
                        ,mvto_oldquantity = @p4 
                         WHERE mvto_numero = @p5";

            if (movimiento.Origen.EntidadExterna != null && movimiento.Origen.EntidadExterna.GetType() == typeof(Entidades.UbicacionStock))
            {
                movimiento.OldQuantity = (movimiento.Origen.EntidadExterna as Entidades.UbicacionStock).CantidadReal;
            }
            else if (movimiento.Destino.EntidadExterna != null && movimiento.Destino.EntidadExterna.GetType() == typeof(Entidades.UbicacionStock))
            {
                movimiento.OldQuantity = (movimiento.Destino.EntidadExterna as Entidades.UbicacionStock).CantidadReal;
            }
            
            object[] parametros = {   
                                      movimiento.CantidadOrigenReal, 
                                      movimiento.CantidadDestinoReal, 
                                      movimiento.FechaReal.Value.ToShortDateString(), 
                                      movimiento.Estado.Codigo,
                                      movimiento.OldQuantity,
                                      movimiento.Numero 
                                  };

            SqlTransaction _transaccion = null;

            try
            {
                _transaccion = (transaccion == null) ? DB.IniciarTransaccion() : transaccion;

                DB.executeNonQuery(sql, parametros, _transaccion);

                if (movimiento.Origen.EntidadExterna != null && movimiento.Origen.EntidadExterna.GetType() == typeof(Entidades.UbicacionStock))
                {
                    UbicacionStockDAL.ActualizarCantidadesStockAndParents((movimiento.Origen.EntidadExterna as Entidades.UbicacionStock).Numero, movimiento.CantidadOrigenReal, _transaccion);
                }
                if (movimiento.Destino.EntidadExterna != null && movimiento.Destino.EntidadExterna.GetType() == typeof(Entidades.UbicacionStock))
                {
                    UbicacionStockDAL.ActualizarCantidadesStockAndParents((movimiento.Destino.EntidadExterna as Entidades.UbicacionStock).Numero, movimiento.CantidadDestinoReal, _transaccion);
                }

                if (transaccion == null) { _transaccion.Commit(); }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) { throw ex; }
                
                _transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
            finally
            {
                if (transaccion == null) { DB.FinalizarTransaccion(); }
            }            
        }

        public static void Cancelar(int numeroMovimiento)
        {
            string sql = @"UPDATE MOVIMIENTOS_STOCK SET                          
                        ,mvto_fechareal = @p0
                        ,emvto_codigo = @p1
                         WHERE mvto_numero = @p2";

            try
            {
                object[] parametros = { DB.GetFechaServidor().ToShortDateString(), EstadoCancelado, numeroMovimiento };

                DB.executeNonQuery(sql, parametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool EsFinalizado(int numeroMovimiento)
        {
            string sql = "SELECT emvto_codigo FROM MOVIMIENTOS_STOCK WHERE mvto_numero = @p0";

            object[] parametros = { numeroMovimiento };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, parametros, null)) == EstadoFinalizado) { return true; }
                else { return false; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool PuedeEliminarse(int numeroMovimiento)
        {
            bool result = EsFinalizado(numeroMovimiento);
            if (result) { return false; }

            //Ver otras condiciones - gonzalo

            return true;
        }

        public static void ObtenerTodos(object fechaDesde, object fechaHasta, object origen, object destino, object estado, Data.dsStock.MOVIMIENTOS_STOCKDataTable dt)
        {
            string sql = @"SELECT mvto_numero, mvto_codigo, mvto_descripcion, mvto_fechaalta, mvto_fechaprevista, 
                            mvto_fechareal, mvto_cantidad_origen_estimada, mvto_cantidad_origen_real, 
                            mvto_cantidad_destino_estimada, mvto_cantidad_destino_real, emvto_codigo, 
                            entd_origen, entd_destino, entd_duenio, mvto_oldquantity  
                            FROM MOVIMIENTOS_STOCK WHERE 1=1";

            int cantidadParametros = 0;
            object[] valoresFiltros = new object[5];

            if (origen != null)
            {
                sql += " AND entd_origen = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(origen);
                cantidadParametros++;
            }

            if (destino != null)
            {
                sql += " AND entd_destino = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(destino);
                cantidadParametros++;
            }
            
            if (estado != null && estado.GetType() == cantidadParametros.GetType())
            {
                sql += " AND emvto_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(estado);
                cantidadParametros++;
            }

            if (fechaDesde != null && !string.IsNullOrEmpty(fechaDesde.ToString()))
            {
                sql += " AND mvto_fechareal >= @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = fechaDesde;
                cantidadParametros++;
            }

            if (fechaHasta != null && !string.IsNullOrEmpty(fechaHasta.ToString()))
            {
                sql += " AND mvto_fechareal <= @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = fechaHasta;
                cantidadParametros++;
            }

            if (cantidadParametros > 0)
            {
                object[] valorParametros = new object[cantidadParametros];
                for (int i = 0; i < cantidadParametros; i++)
                {
                    valorParametros[i] = valoresFiltros[i];
                }
                try
                {
                    DB.FillDataTable(dt, sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
            else
            {
                try
                {
                    DB.FillDataTable(dt, sql, null);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
        }

        public static Data.dsStock.MOVIMIENTOS_STOCKDataTable ObtenerMovimientosUbicacionStock(object fechaDesde, object fechaHasta, object codigoEntidad, object estado)
        {
            string sql = @"SELECT mvto_numero, mvto_codigo, mvto_descripcion, mvto_fechaalta, mvto_fechaprevista, 
                            mvto_fechareal, mvto_cantidad_origen_estimada, mvto_cantidad_origen_real, 
                            mvto_cantidad_destino_estimada, mvto_cantidad_destino_real, emvto_codigo, 
                            entd_origen, entd_destino, entd_duenio, mvto_oldquantity 
                            FROM MOVIMIENTOS_STOCK WHERE 1=1 ";

            int cantidadParametros = 0;
            object[] valoresFiltros = new object[5];

            Data.dsStock.MOVIMIENTOS_STOCKDataTable dt = new GyCAP.Data.dsStock.MOVIMIENTOS_STOCKDataTable();

            if (fechaDesde != null && !string.IsNullOrEmpty(fechaDesde.ToString()))
            {
                sql += " AND mvto_fechareal >= @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = fechaDesde;
                cantidadParametros++;
            }

            if (fechaHasta != null && !string.IsNullOrEmpty(fechaHasta.ToString()))
            {
                sql += " AND mvto_fechareal <= @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = fechaHasta;
                cantidadParametros++;
            }

            if (codigoEntidad != null)
            {
                sql += " AND (entd_origen = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(codigoEntidad);
                cantidadParametros++;
                sql += " OR entd_destino = @p" + cantidadParametros + ")";
                valoresFiltros[cantidadParametros] = Convert.ToInt32(codigoEntidad);
                cantidadParametros++;
            }

            if (estado != null)
            {
                sql += " AND emvto_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(estado);
                cantidadParametros++;
            }

            Entidades.EstadoMovimientoStock estadoEntity = EstadoMovimientoStockDAL.GetEstadoEntity(Convert.ToInt32(estado));
            if (estadoEntity.Nombre == EstadoMovimientoStockDAL.Finalizado) { sql += " ORDER BY mvto_fechareal ASC"; }
            else if (estadoEntity.Nombre == EstadoMovimientoStockDAL.Planificado) { sql += " ORDER BY mvto_fechaprevista ASC"; }

            if (cantidadParametros > 0)
            {
                object[] valorParametros = new object[cantidadParametros];
                for (int i = 0; i < cantidadParametros; i++)
                {
                    valorParametros[i] = valoresFiltros[i];
                }
                try
                {
                    DB.FillDataTable(dt, sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }

            return dt;
        }
    }
}
