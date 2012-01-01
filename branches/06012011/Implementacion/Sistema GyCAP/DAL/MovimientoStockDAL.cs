using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades;
using GyCAP.Entidades.Excepciones;

namespace GyCAP.DAL
{
    public class MovimientoStockDAL
    {
        public static void InsertarPlanificado(MovimientoStock movimientoStock, SqlTransaction transaccion)
        {
            Insertar(movimientoStock, transaccion);
        }

        public static void InsertarFinalizado(MovimientoStock movimientoStock, SqlTransaction transaccion, bool modificarCantidadesStock)
        {
            Insertar(movimientoStock, transaccion);
            IniciarMovimiento(movimientoStock, transaccion);
            FinalizarMovimiento(movimientoStock, transaccion, modificarCantidadesStock);
        }

        private static void Insertar(MovimientoStock movimientoStock, SqlTransaction transaccion)
        {
            string sql = @"INSERT INTO [MOVIMIENTOS_STOCK] 
                        ([mvto_codigo]
                        ,[mvto_descripcion]
                        ,[mvto_fechaalta]
                        ,[mvto_fechaprevista]
                        ,[mvto_fechareal]
                        ,[mvto_cantidad_destino_estimada]
                        ,[mvto_cantidad_destino_real]
                        ,[emvto_codigo]
                        ,[entd_destino]
                        ,[entd_duenio]) 
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9) SELECT @@Identity";

            object fechaPrevista = DBNull.Value, fechaReal = DBNull.Value, destino = DBNull.Value, duenio = DBNull.Value;
            if (movimientoStock.FechaPrevista.HasValue) { fechaPrevista = movimientoStock.FechaPrevista.Value.ToString("yyyyMMdd"); }
            if (movimientoStock.FechaReal.HasValue) { fechaReal = movimientoStock.FechaReal.Value.ToString("yyyyMMdd"); }
            if (movimientoStock.Destino != null) { destino = movimientoStock.Destino.Codigo; }
            if (movimientoStock.Duenio != null) { duenio = movimientoStock.Duenio.Codigo; }

            object[] parametros = { 
                                      movimientoStock.Codigo,
                                      movimientoStock.Descripcion,
                                      movimientoStock.FechaAlta.ToString("yyyyMMdd"),
                                      fechaPrevista,
                                      fechaReal,
                                      movimientoStock.CantidadDestinoEstimada,
                                      movimientoStock.CantidadDestinoReal,
                                      movimientoStock.Estado.Codigo,
                                      destino,
                                      duenio
                                  };

            movimientoStock.Numero = Convert.ToInt32(DB.executeScalar(sql, parametros, transaccion));

            foreach (OrigenMovimiento item in movimientoStock.OrigenesMultiples)
            {
                item.MovimientoStock = movimientoStock.Numero;
                InsertarOrigenes(item, transaccion);
            }
        }

        private static void InsertarOrigenes(OrigenMovimiento origen, SqlTransaction transaccion)
        {
            string sql = @"INSERT INTO [ORIGENES_MOVIMIENTO_STOCK]
                            ([entd_codigo]
                            ,[om_cantidad_estimada]
                            ,[om_cantidad_real]
                            ,[mvto_numero]
                            ,[om_fechaprevista]
                            ,[om_fechareal])
                            VALUES (@p0, @p1, @p2, @p3, @p4, @p5) SELECT @@Identity";

            object fechaReal = DBNull.Value;
            if (origen.FechaReal.HasValue) { fechaReal = origen.FechaReal.Value.ToString("yyyyMMdd"); }

            object[] parametros = { origen.Entidad.Codigo, 
                                      origen.CantidadEstimada, 
                                      origen.CantidadReal, 
                                      origen.MovimientoStock, 
                                      origen.FechaPrevista.ToString("yyyyMMdd"),
                                      fechaReal };

            origen.Codigo = Convert.ToInt32(DB.executeScalar(sql, parametros, transaccion));
        }

        public static void Eliminar(int numeroMovimiento, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM MOVIMIENTOS_STOCK WHERE mvto_numero = @p0";
            string sqlOrigenes = "DELETE FROM ORIGENES_MOVIMIENTO_STOCK WHERE mvto_numero = @p0";
            object[] parametros = { numeroMovimiento };
                            
            DB.executeNonQuery(sqlOrigenes, parametros, transaccion);
            DB.executeNonQuery(sql, parametros, transaccion);
        }

        //Metodo para eliminar el/los movimientos de stock de un pedido
        public static void EliminarMovimientosPedido(int numeroEntidadPedido, SqlTransaction transaccion)
        {
            string sql = @"DELETE FROM MOVIMIENTOS_STOCK 
                                  WHERE mvto_codigo = 'Pedido' AND entd_duenio = @p0";

            object[] parametros = { numeroEntidadPedido };

            try
            {
                DB.executeNonQuery(sql, parametros, transaccion);
            }
            catch (SqlException ex) { throw new BaseDeDatosException(ex.Message); }
        }

        public static void IniciarMovimiento(MovimientoStock movimiento, SqlTransaction transaccion)
        {
            string sql = "UPDATE MOVIMIENTOS_STOCK SET emvto_codigo = @p0 WHERE mvto_numero = @p1";

            object[] parametros = { movimiento.Estado.Codigo, movimiento.Numero };

            DB.executeNonQuery(sql, parametros, transaccion);

            foreach (OrigenMovimiento origen in movimiento.OrigenesMultiples)
            {
                FinalizarOrigen(origen, transaccion);
            }
        }

        public static void FinalizarMovimiento(MovimientoStock movimiento, SqlTransaction transaccion, bool modificarCantidadesStock)
        {
            string sql = @"UPDATE MOVIMIENTOS_STOCK SET 
                         mvto_fechareal = @p0
                        ,emvto_codigo = @p1 
                         WHERE mvto_numero = @p2";

            if (modificarCantidadesStock)
            {
                sql = @"UPDATE MOVIMIENTOS_STOCK SET 
                         mvto_fechareal = @p0
                        ,emvto_codigo = @p1
                        ,mvto_cantidad_destino_real = @p2 
                         WHERE mvto_numero = @p3";
            }

            object[] parametros = { 
                                      movimiento.FechaReal.Value.ToString("yyyyMMdd"), 
                                      movimiento.Estado.Codigo,
                                      movimiento.Numero 
                                  };

            if (modificarCantidadesStock)
            {
                parametros = new object[] { 
                                            movimiento.FechaReal.Value.ToString("yyyyMMdd"), 
                                            movimiento.Estado.Codigo,
                                            movimiento.CantidadDestinoReal,
                                            movimiento.Numero 
                                          };
            }
            
            SqlTransaction _transaccion = null;

            try
            {
                _transaccion = (transaccion == null) ? DB.IniciarTransaccion() : transaccion;

                DB.executeNonQuery(sql, parametros, _transaccion);

                if (modificarCantidadesStock)
                {
                    if (movimiento.Destino.EntidadExterna != null && movimiento.Destino.TipoEntidad.Codigo == (int)EntidadEnum.TipoEntidadEnum.UbicacionStock)
                    {
                        UbicacionStockDAL.ActualizarCantidadesStockAndParents((movimiento.Destino.EntidadExterna as Entidades.UbicacionStock).Numero, movimiento.CantidadDestinoReal, _transaccion);
                    }
                }

                if (transaccion == null) { _transaccion.Commit(); }
            }
            catch (SqlException ex)
            {
                if (transaccion != null) { throw ex; }

                _transaccion.Rollback();
                throw new BaseDeDatosException(ex.Message);
            }
            finally
            {
                if (transaccion == null) { DB.FinalizarTransaccion(); }
            }
        }

        public static void RegistrarAvance(MovimientoStock movimiento, decimal incremento, SqlTransaction transaccion)
        {
            string sql = @"UPDATE MOVIMIENTOS_STOCK SET 
                         mvto_cantidad_destino_real = @p0
                         WHERE mvto_numero = @p1";

            object[] parametros = { 
                                      movimiento.CantidadDestinoReal,
                                      movimiento.Numero 
                                  };

            DB.executeNonQuery(sql, parametros, transaccion);

            if (movimiento.Destino.EntidadExterna != null && movimiento.Destino.TipoEntidad.Codigo == (int)EntidadEnum.TipoEntidadEnum.UbicacionStock)
            {
                UbicacionStockDAL.ActualizarCantidadesStockAndParents((movimiento.Destino.EntidadExterna as Entidades.UbicacionStock).Numero, incremento, transaccion);
            }
        }

        public static void EliminarAvance(MovimientoStock movimiento, decimal decremento, SqlTransaction transaccion)
        {
            string sql = @"UPDATE MOVIMIENTOS_STOCK SET 
                         mvto_cantidad_destino_real = @p0
                         WHERE mvto_numero = @p1";

            object[] parametros = { 
                                      movimiento.CantidadDestinoReal,
                                      movimiento.Numero 
                                  };

            DB.executeNonQuery(sql, parametros, transaccion);

            if (movimiento.Destino.EntidadExterna != null && movimiento.Destino.TipoEntidad.Codigo == (int)EntidadEnum.TipoEntidadEnum.UbicacionStock)
            {
                UbicacionStockDAL.ActualizarCantidadesStockAndParents((movimiento.Destino.EntidadExterna as Entidades.UbicacionStock).Numero, (decremento * -1), transaccion);
            }
        }

        private static void FinalizarOrigen(OrigenMovimiento origen, SqlTransaction transaccion)
        {
            string sql = @"UPDATE ORIGENES_MOVIMIENTO_STOCK SET 
                            om_cantidad_real = @p0,
                            om_fechareal = @p1
                            WHERE om_codigo = @p2";

            object[] parametros = { origen.CantidadReal, origen.FechaReal.Value.ToString("yyyyMMdd"), origen.Codigo };

            if (origen.Entidad.EntidadExterna != null && origen.Entidad.TipoEntidad.Codigo == (int)EntidadEnum.TipoEntidadEnum.UbicacionStock)
            {
                UbicacionStockDAL.ActualizarCantidadesStockAndParents((origen.Entidad.EntidadExterna as Entidades.UbicacionStock).Numero, origen.CantidadReal * -1, transaccion);
            }

            DB.executeNonQuery(sql, parametros, transaccion);
        }

        public static void Cancelar(MovimientoStock movimiento, SqlTransaction transaccion)
        {
            string sql = @"UPDATE MOVIMIENTOS_STOCK SET                          
                        ,mvto_fechareal = @p0
                        ,emvto_codigo = @p1
                         WHERE mvto_numero = @p2";
                        
            object[] parametros = { DB.GetFechaServidor().ToString("yyyyMMdd"), movimiento.Estado.Codigo, movimiento.Numero };

            DB.executeNonQuery(sql, parametros, transaccion);
        }

        public static bool EsFinalizado(int numeroMovimiento)
        {
            string sql = "SELECT emvto_codigo FROM MOVIMIENTOS_STOCK WHERE mvto_numero = @p0";

            object[] parametros = { numeroMovimiento };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, parametros, null)) == (int)StockEnum.EstadoMovimientoStock.Finalizado) { return true; }
                else { return false; }
            }
            catch (SqlException ex) { throw new BaseDeDatosException(ex.Message); }
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
            string sql = @"SELECT M.mvto_numero, M.mvto_codigo, M.mvto_descripcion, M.mvto_fechaalta, M.mvto_fechaprevista, 
                            M.mvto_fechareal, M.mvto_cantidad_destino_estimada, M.mvto_cantidad_destino_real, M.emvto_codigo, 
                            M.entd_destino, M.entd_duenio  
                            FROM MOVIMIENTOS_STOCK M ";

            if (origen != null) { sql += ", ORIGENES_MOVIMIENTO_STOCK O WHERE 1=1 "; }
            else { sql += " WHERE 1=1 "; }

            int cantidadParametros = 0;
            object[] valoresFiltros = new object[5];

            if (origen != null)
            {
                sql += " AND M.mvto_numero = O.mvto_numero AND O.entd_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(origen);
                cantidadParametros++;
            }

            if (destino != null)
            {
                sql += " AND M.entd_destino = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(destino);
                cantidadParametros++;
            }

            if (estado != null && estado.GetType() == cantidadParametros.GetType())
            {
                sql += " AND M.emvto_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(estado);
                cantidadParametros++;
            }

            if (fechaDesde != null && !string.IsNullOrEmpty(fechaDesde.ToString()))
            {
                sql += " AND M.mvto_fechareal >= @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = fechaDesde;
                cantidadParametros++;
            }

            if (fechaHasta != null && !string.IsNullOrEmpty(fechaHasta.ToString()))
            {
                sql += " AND M.mvto_fechareal <= @p" + cantidadParametros;
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
                catch (SqlException ex) { throw new BaseDeDatosException(ex.Message); }
            }
            else
            {
                try
                {
                    DB.FillDataTable(dt, sql, null);
                }
                catch (SqlException ex) { throw new BaseDeDatosException(ex.Message); }
            }
        }

        public static Data.dsStock.ORIGENES_MOVIMIENTO_STOCKDataTable ObtenerOrigenes(int numeroMovimiento)
        {
            string sql = @"SELECT om_codigo, om_cantidad_estimada, om_cantidad_real, entd_codigo, mvto_numero, om_fechaprevista, om_fechareal  
                            FROM ORIGENES_MOVIMIENTO_STOCK WHERE mvto_numero = @p0";

            object[] parametros = { numeroMovimiento };
            Data.dsStock.ORIGENES_MOVIMIENTO_STOCKDataTable dt = new GyCAP.Data.dsStock.ORIGENES_MOVIMIENTO_STOCKDataTable();

            DB.FillDataTable(dt, sql, parametros);

            return dt;
        }

        public static Data.dsStock.MOVIMIENTOS_STOCKDataTable ObtenerMovimientosUbicacionStock(object fechaDesde, object fechaHasta, object codigoEntidad)
        {
            string sql = @"SELECT M.mvto_numero, M.mvto_codigo, M.mvto_descripcion, M.mvto_fechaalta, M.mvto_fechaprevista, 
                            M.mvto_fechareal, M.mvto_cantidad_destino_estimada, M.mvto_cantidad_destino_real, M.emvto_codigo, 
                            M.entd_destino, M.entd_duenio   
                            FROM MOVIMIENTOS_STOCK M, ORIGENES_MOVIMIENTO_STOCK O WHERE 1=1 ";

            int cantidadParametros = 0;
            object[] valoresFiltros = new object[3];

            Data.dsStock.MOVIMIENTOS_STOCKDataTable dt = new GyCAP.Data.dsStock.MOVIMIENTOS_STOCKDataTable();

            if (fechaDesde != null && !string.IsNullOrEmpty(fechaDesde.ToString()))
            {
                sql += " AND M.mvto_fechaprevista >= @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = fechaDesde;
                cantidadParametros++;
            }

            if (fechaHasta != null && !string.IsNullOrEmpty(fechaHasta.ToString()))
            {
                sql += " AND (M.mvto_fechaprevista <= @p" + cantidadParametros + " OR O.om_fechaprevista <= @p" + cantidadParametros + ")";
                valoresFiltros[cantidadParametros] = fechaHasta;
                cantidadParametros++;
            }

            if (codigoEntidad != null)
            {
                sql += " AND M.entd_destino = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(codigoEntidad);
                sql += " OR (M.mvto_numero = O.mvto_numero AND O.entd_codigo = @p" + cantidadParametros + ")";
                cantidadParametros++;
            }

            sql += @" GROUP BY M.mvto_numero, M.mvto_codigo, M.mvto_descripcion, M.mvto_fechaalta, M.mvto_fechaprevista, 
                    M.mvto_fechareal, M.mvto_cantidad_destino_estimada, M.mvto_cantidad_destino_real, M.emvto_codigo, 
                    M.entd_destino, M.entd_duenio ORDER BY M.mvto_fechaprevista ASC";

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
                catch (SqlException ex) { throw new BaseDeDatosException(ex.Message); }
            }

            return dt;
        }

        public static void GetMovimientosByOwner(Entidad owner, DataTable dt)
        {
            string sql = @"SELECT mvto_numero, mvto_codigo, mvto_descripcion, mvto_fechaalta, mvto_fechaprevista, 
                            mvto_fechareal, mvto_cantidad_destino_estimada, mvto_cantidad_destino_real, emvto_codigo, 
                            entd_destino, entd_duenio   
                            FROM MOVIMIENTOS_STOCK WHERE entd_duenio = @p0 ";

            object[] parametros = { owner.Codigo };

            DB.FillDataTable(dt, sql, parametros);
        }
    }
}
