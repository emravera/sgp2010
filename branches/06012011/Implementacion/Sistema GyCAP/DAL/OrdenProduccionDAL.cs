using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades;
using GyCAP.Entidades.ArbolOrdenesTrabajo;

namespace GyCAP.DAL
{
    public class OrdenProduccionDAL
    {
        public static readonly int OrdenAutomatica = 1;
        public static readonly int OrdenManual = 2;
        
        public static void Insertar(ArbolProduccion arbol, SqlTransaction transaccion)
        {
            OrdenProduccion orden = arbol.OrdenProduccion;

            string sql = @"INSERT INTO ORDENES_PRODUCCION 
                        ([ordp_codigo]
                        ,[eord_codigo]
                        ,[ordp_fechaalta]
                        ,[dpsem_codigo]
                        ,[ordp_origen]
                        ,[ordp_fechainicioestimada]
                        ,[ordp_fechainicioreal]
                        ,[ordp_fechafinestimada]
                        ,[ordp_fechafinreal]
                        ,[ordp_observaciones]
                        ,[ordp_prioridad]
                        ,[estr_codigo]
                        ,[ordp_cantidadestimada]
                        ,[ordp_cantidadreal]
                        ,[coc_codigo]
                        ,[ustck_destino]
                        ,[lot_codigo])
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16) SELECT @@Identity";

            object fechainicio = DBNull.Value, fechafinreal = DBNull.Value, lote = DBNull.Value;
            if (orden.FechaInicioReal.HasValue) { fechainicio = orden.FechaInicioReal.Value.ToShortDateString(); }
            if (orden.FechaFinReal.HasValue) { fechafinreal = orden.FechaFinReal.Value.ToShortDateString(); }
            if (orden.Lote != null) { lote = orden.Lote.Codigo; }
            object[] valoresParametros = {   
                                             orden.Codigo,
                                             orden.Estado.Codigo,
                                             orden.FechaAlta.ToShortDateString(),
                                             orden.DetallePlanSemanal.Codigo,
                                             orden.Origen,
                                             orden.FechaInicioEstimada.ToString(),
                                             fechainicio,
                                             orden.FechaFinEstimada.Value.ToShortDateString(),
                                             fechafinreal,
                                             orden.Observaciones,
                                             orden.Prioridad,
                                             orden.Estructura,
                                             orden.CantidadEstimada,
                                             orden.CantidadReal,
                                             orden.Cocina.CodigoCocina,
                                             orden.UbicacionStock.Numero,
                                             lote
                                         };
                                   
            
            orden.Numero = Convert.ToInt32(DB.executeScalar(sql, valoresParametros, transaccion));
            orden.Codigo = string.Concat("GA", orden.Numero);
            ActualizarCodigo(orden.Codigo, orden.Numero, transaccion);
            
            IList<OrdenTrabajo> ordenesTrabajo = arbol.AsOrdenesTrabajoList().OrderBy(p => p.FechaInicioEstimada).ToList();
            
            foreach (OrdenTrabajo item in ordenesTrabajo)
            {
                int oldNumber = item.Numero;
                item.OrdenProduccion = orden.Numero;
                
                OrdenTrabajoDAL.Insertar(item, transaccion);
                foreach (OrdenTrabajo ordt in ordenesTrabajo.Where(p => p.OrdenTrabajoPadre == oldNumber))
                {
                    ordt.OrdenTrabajoPadre = item.Numero;
                }
            }
        }

        private static void ActualizarCodigo(string codigo, int numeroOrden, SqlTransaction transaccion)
        {
            string sql = "UPDATE ORDENES_PRODUCCION SET ordp_codigo = @p0 WHERE ordp_numero = @p1";
            object[] parametros = { codigo, numeroOrden };

            DB.executeNonQuery(sql, parametros, transaccion);
        }
        
        public static void Eliminar(int numeroOrdenProduccion)
        {
            string sqlOT = "DELETE FROM ORDENES_TRABAJO WHERE ordp_numero = @p0";
            string sqlOP = "DELETE FROM ORDENES_PRODUCCION WHERE ordp_numero = @p0";

            object[] parametros = { numeroOrdenProduccion };
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                DB.executeNonQuery(sqlOT, parametros, transaccion);
                DB.executeNonQuery(sqlOP, parametros, transaccion);
                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                //Error en alguna consulta, descartamos los cambios
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
            finally
            {
                //En cualquier caso finalizamos la transaccion para que se cierre la conexion
                DB.FinalizarTransaccion();
            }
        }

        public static void Eliminar(IList<Entidades.OrdenProduccion> ordenesProduccion)
        {
            string sqlOT = "DELETE FROM ORDENES_TRABAJO WHERE ordp_numero IN (@p0)";
            string sqlOP = "DELETE FROM ORDENES_PRODUCCION WHERE ordp_numero IN (@p0)";

            int[] ordenes = new int[ordenesProduccion.Count];
            int[] detalles = new int[ordenesProduccion.Count];
            for (int i = 0; i < ordenesProduccion.Count; i++) 
            { 
                ordenes[i] = ordenesProduccion[i].Numero;
                detalles[i] = ordenesProduccion[i].DetallePlanSemanal.Codigo;
            }

            object[] parametros = { ordenes };
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                DB.executeNonQuery(sqlOT, parametros, transaccion);
                DB.executeNonQuery(sqlOP, parametros, transaccion);
                DetallePlanSemanalDAL.ActualizarEstado(detalles, (int)PlanificacionEnum.EstadoDetallePlanSemanal.Generado, transaccion);
                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                //Error en alguna consulta, descartamos los cambios
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
            finally
            {
                //En cualquier caso finalizamos la transaccion para que se cierre la conexion
                DB.FinalizarTransaccion();
            }
        }

        public static void IniciarOrdenProduccion(int numeroOrdenProduccion, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsStock dsStock)
        {
            string sql = "UPDATE ORDENES_PRODUCCION SET eord_codigo = @p0, ordp_fechainicioreal = @p1 WHERE ordp_numero = @p2";
            
            object[] parametros = { dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).EORD_CODIGO,
                                    dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_FECHAINICIOREAL,
                                    numeroOrdenProduccion };

            SqlTransaction transaccion = null;
            try
            {
                transaccion = DB.IniciarTransaccion();
                //Actualizamos la orden de produccion
                DB.executeNonQuery(sql, parametros, transaccion);
                //Iniciamos las órdenes de trabajo que corresponden
                string filtro = "ORDP_NUMERO = " + numeroOrdenProduccion + " AND EORD_CODIGO = " + (int)OrdenesTrabajoEnum.EstadoOrdenEnum.EnProceso;
                foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOT in (Data.dsOrdenTrabajo.ORDENES_TRABAJORow[])dsOrdenTrabajo.ORDENES_TRABAJO.Select(filtro))
                {
                    OrdenTrabajoDAL.IniciarOrdenTrabajo(Convert.ToInt32(rowOT.ORDT_NUMERO), dsOrdenTrabajo, dsStock, transaccion);
                }

                //Actualizamos el estado del resto de las órdenes de trabajo
                filtro = "ORDP_NUMERO = " + numeroOrdenProduccion + " AND EORD_CODIGO = " + (int)OrdenesTrabajoEnum.EstadoOrdenEnum.EnEspera;
                foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOT in (Data.dsOrdenTrabajo.ORDENES_TRABAJORow[])dsOrdenTrabajo.ORDENES_TRABAJO.Select(filtro))
                {
                    OrdenTrabajoDAL.ActualizarEstado(Convert.ToInt32(rowOT.ORDT_NUMERO), (int)OrdenesTrabajoEnum.EstadoOrdenEnum.EnEspera, transaccion);
                }

                //Si la orden de trabajo es por un pedido actualizamos su estado
                if (!dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).IsDPSEM_CODIGONull())
                {
                    object pedido = DetallePlanSemanalDAL.ObtenerPedidoClienteDeDetalle(Convert.ToInt32(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).DPSEM_CODIGO), transaccion);
                    if (pedido != null && pedido != DBNull.Value)
                    {
                        PedidoDAL.ActualizarDetallePedidoAEnCurso(Convert.ToInt32(pedido), transaccion);
                    }
                }

                //Confirmamos los cambios
                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                //Error en alguna consulta, descartamos los cambios
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
            finally
            {
                //En cualquier caso finalizamos la transaccion para que se cierre la conexion
                DB.FinalizarTransaccion();
            }
        }
        
        public static void ObtenerOrdenesProduccion(object codigo, object estado, object modo, object fechaGeneracion, object fechaDesde, object fechaHasta, Data.dsOrdenTrabajo ds)
        {
            string sql = @"SELECT ordp_numero, ordp_codigo, eord_codigo, ordp_fechaalta, dpsem_codigo, ordp_origen, ordp_fechainicioestimada, 
                        ordp_fechainicioreal, ordp_fechafinestimada, ordp_fechafinreal, ordp_observaciones, ordp_prioridad, estr_codigo, 
                        ordp_cantidadestimada, ordp_cantidadreal, coc_codigo, ustck_destino, lot_codigo  
                        FROM ORDENES_PRODUCCION WHERE 1 = 1";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[6];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (codigo != null && codigo.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND ordp_codigo LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                codigo = "%" + codigo + "%";
                valoresFiltros[cantidadParametros] = codigo;
                cantidadParametros++;
            }

            //Revisamos si pasó algun valor y si es un integer
            if (estado != null && estado.GetType() == cantidadParametros.GetType())
            {
                sql += " AND eord_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(estado);
                cantidadParametros++;
            }

            //Revisamos si pasó algun valor y si es un integer
            DateTime generacion;
            if (fechaGeneracion != null && DateTime.TryParse(fechaGeneracion.ToString(), out generacion))
            {                
                sql += " AND ordp_fechaalta >= @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = generacion.ToString("yyyyMMdd");
                cantidadParametros++;
                sql += " AND ordp_fechaalta < dateadd(dd, 1, @p" + cantidadParametros + ")";
                valoresFiltros[cantidadParametros] = generacion.ToString("yyyyMMdd");
                cantidadParametros++;
            }

            //Revisamos si pasó algun valor y si es un integer
            DateTime desde, hasta;
            if (fechaDesde != null && DateTime.TryParse(fechaDesde.ToString(), out desde) && fechaHasta != null && DateTime.TryParse(fechaHasta.ToString(), out hasta))
            {
                sql += " AND ordp_fechainicioestimada BETWEEN @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = desde.ToString("yyyyMMdd");
                cantidadParametros++;
                sql += " AND @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = hasta.ToString("yyyyMMdd");
                cantidadParametros++;
            }

            //Revisamos si pasó algun valor y si es un integer
            /*if (modo != null && modo.GetType() == cantidadParametros.GetType())
            {
                if (Convert.ToInt32(modo) == OrdenAutomatica)
                {
                    sql += " AND ordpm_numero IS NULL";
                }
                else if (Convert.ToInt32(modo) == OrdenManual)
                {
                    sql += " AND ordpm_numero IS NOT NULL";
                }                
            }*/

            if (cantidadParametros > 0)
            {
                //Buscamos con filtro, armemos el array de los valores de los parametros
                object[] valorParametros = new object[cantidadParametros];
                for (int i = 0; i < cantidadParametros; i++)
                {
                    valorParametros[i] = valoresFiltros[i];
                }
                try
                {
                    DB.FillDataSet(ds, "ORDENES_PRODUCCION", sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
        }

        public static void FinalizarOrdenProduccion(int numeroOrdenProduccion, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsStock dsStock)
        {
            string sql = @"UPDATE ORDENES_PRODUCCION SET 
                        eord_codigo = @p0, 
                        ordp_fechafinreal = ordp_fechafinestimada, 
                        ordp_cantidadreal = ordp_cantidadestimada 
                        WHERE ordp_numero = @p1;";

            object[] parametros = { (int)OrdenesTrabajoEnum.EstadoOrdenEnum.Finalizada, numeroOrdenProduccion };

            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                //Finalizo la orden de producción
                DB.executeNonQuery(sql, parametros, transaccion);
                dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).EORD_CODIGO = (int)OrdenesTrabajoEnum.EstadoOrdenEnum.Finalizada;
                dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_CANTIDADREAL = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_CANTIDADESTIMADA;
                dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_FECHAFINREAL = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_FECHAFINESTIMADA;

                //Finalizo las ordenes de trabajo
                foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOT in dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).GetORDENES_TRABAJORows())
                {
                    OrdenTrabajoDAL.FinalizarOrdenTrabajo(Convert.ToInt32(rowOT.ORDT_NUMERO), dsOrdenTrabajo, dsStock, transaccion);
                }

                //Actualizar stock de la cocina de la orden de producción
                if (!dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).IsUSTCK_DESTINONull())
                {
                    int ubicacion = Convert.ToInt32(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).USTCK_DESTINO);
                    decimal cantidadReal = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_CANTIDADREAL;
                    //UbicacionStockDAL.ActualizarCantidadesStock(ubicacion, cantidadReal, 0, transaccion);
                }

                //Actualizamos la planificación en que estaba basada la orden de producción
                //También si la orden de producción es por un pedido actualizamos su estado
                if (!dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).IsDPSEM_CODIGONull())
                {
                    int codigoPlan = Convert.ToInt32(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).DPSEM_CODIGO);
                    int cantidad = Convert.ToInt32(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).ORDP_CANTIDADREAL);
                    int cocina = Convert.ToInt32(dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).COC_CODIGO);

                    //Planificación
                    DetallePlanSemanalDAL.SumarCantidadFinalizada(codigoPlan, cocina, cantidad, transaccion);
                    
                    //Pedido
                    object pedido = DetallePlanSemanalDAL.ObtenerPedidoClienteDeDetalle(codigoPlan, transaccion);
                    if (pedido != null && pedido != DBNull.Value)
                    {
                        //PedidoDAL.ActualizarDetallePedidoAFinalizado(Convert.ToInt32(pedido), transaccion);
                        PedidoDAL.ActualizarEstado(Convert.ToInt32(pedido), 5, transaccion);
                    }
                }

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
    }
}
