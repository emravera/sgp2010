using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class OrdenProduccionDAL
    {
        public static readonly int OrdenAutomatica = 1;
        public static readonly int OrdenManual = 2;

        public static readonly int EstadoGenerado = 1;
        public static readonly int EstadoEnEspera = 2;
        public static readonly int EstadoEnProceso = 3;
        public static readonly int EstadoFinalizado = 4;
        public static readonly int EstadoCancelado = 5;
        
        public static void Insertar(int numeroOrdenProduccion, Data.dsOrdenTrabajo dsOrdenTrabajo)
        {
            string sql = @"INSERT INTO ORDENES_PRODUCCION 
                        ([ordp_codigo]
                        ,[eord_codigo]
                        ,[ordp_fechaalta]
                        ,[dpsem_codigo]
                        ,[ordpm_numero]
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
                        ,[ustck_destino])
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16) SELECT @@Identity";

            Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow rowOrdenP = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion);
            object ordpm = DBNull.Value, dpsem = DBNull.Value, cocina = DBNull.Value, stock = DBNull.Value;
            if (!rowOrdenP.IsDPSEM_CODIGONull()) { dpsem = rowOrdenP.DPSEM_CODIGO; }
            else { ordpm = rowOrdenP.ORDPM_NUMERO; }
            if (!rowOrdenP.IsCOC_CODIGONull()) { cocina = rowOrdenP.COC_CODIGO; }
            if (!rowOrdenP.IsUSTCK_DESTINONull()) { stock = rowOrdenP.USTCK_DESTINO; }
            object[] valoresParametros = {   rowOrdenP.ORDP_CODIGO,
                                             rowOrdenP.EORD_CODIGO,
                                             rowOrdenP.ORDP_FECHAALTA,
                                             dpsem,
                                             ordpm,
                                             rowOrdenP.ORDP_ORIGEN,
                                             rowOrdenP.ORDP_FECHAINICIOESTIMADA,
                                             DBNull.Value,
                                             rowOrdenP.ORDP_FECHAFINESTIMADA,
                                             DBNull.Value,
                                             rowOrdenP.ORDP_OBSERVACIONES,
                                             rowOrdenP.ORDP_PRIORIDAD,
                                             rowOrdenP.ESTR_CODIGO,
                                             rowOrdenP.ORDP_CANTIDADESTIMADA,
                                             rowOrdenP.ORDP_CANTIDADREAL, 
                                             cocina,
                                             stock };

            string sqlUOP = "UPDATE ORDENES_PRODUCCION SET ordp_codigo = @p0 WHERE ordp_numero = @p1";
            string sqlUOT = "UPDATE ORDENES_TRABAJO SET ordt_codigo = @p0 WHERE ordt_numero = @p1";
            SqlTransaction transaccion = null;
            
            try
            {
                transaccion = DB.IniciarTransaccion();
                rowOrdenP.BeginEdit();
                numeroOrdenProduccion = Convert.ToInt32(DB.executeScalar(sql, valoresParametros, transaccion));
                if (rowOrdenP.IsORDPM_NUMERONull()) { rowOrdenP.ORDP_CODIGO = "OPA-" + numeroOrdenProduccion; }
                else { rowOrdenP.ORDP_CODIGO = "OPM-" + numeroOrdenProduccion; }
                rowOrdenP.EndEdit();

                valoresParametros = new object[] { rowOrdenP.ORDP_CODIGO, numeroOrdenProduccion };
                DB.executeNonQuery(sqlUOP, valoresParametros, transaccion);

                foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOrdenTrabajo in rowOrdenP.GetORDENES_TRABAJORows())
                {
                    rowOrdenTrabajo.BeginEdit();
                    rowOrdenTrabajo.ORDP_NUMERO = numeroOrdenProduccion;
                    int numero = OrdenTrabajoDAL.Insertar(rowOrdenTrabajo, transaccion);
                    
                    foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow row in (Data.dsOrdenTrabajo.ORDENES_TRABAJORow[])dsOrdenTrabajo.ORDENES_TRABAJO.Select("ordt_ordensiguiente = " + rowOrdenTrabajo.ORDT_NUMERO))
                    {
                        row.ORDT_ORDENSIGUIENTE = numero;
                    }
                    
                    rowOrdenTrabajo.ORDT_NUMERO = numero;
                    if (rowOrdenP.IsORDPM_NUMERONull()) { rowOrdenTrabajo.ORDT_CODIGO = "OTA-" + rowOrdenTrabajo.ORDT_NUMERO.ToString(); }
                    else { rowOrdenTrabajo.ORDT_CODIGO = "OTM-" + rowOrdenTrabajo.ORDT_NUMERO.ToString(); }
                    valoresParametros = new object[] { rowOrdenTrabajo.ORDT_CODIGO, rowOrdenTrabajo.ORDT_NUMERO };
                    DB.executeNonQuery(sqlUOT, valoresParametros, transaccion);
                    rowOrdenTrabajo.EndEdit();
                }
                rowOrdenP.BeginEdit();
                rowOrdenP.ORDP_NUMERO = numeroOrdenProduccion;
                rowOrdenP.EndEdit();
                
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
                string filtro = "ORDP_NUMERO = " + numeroOrdenProduccion + " AND EORD_CODIGO = " + EstadoEnProceso;
                foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOT in (Data.dsOrdenTrabajo.ORDENES_TRABAJORow[])dsOrdenTrabajo.ORDENES_TRABAJO.Select(filtro))
                {
                    OrdenTrabajoDAL.IniciarOrdenTrabajo(Convert.ToInt32(rowOT.ORDT_NUMERO), dsOrdenTrabajo, dsStock, transaccion);
                }

                //Actualizamos el estado del resto de las órdenes de trabajo
                filtro = "ORDP_NUMERO = " + numeroOrdenProduccion + " AND EORD_CODIGO = " + EstadoEnEspera;
                foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOT in (Data.dsOrdenTrabajo.ORDENES_TRABAJORow[])dsOrdenTrabajo.ORDENES_TRABAJO.Select(filtro))
                {
                    OrdenTrabajoDAL.ActualizarEstado(Convert.ToInt32(rowOT.ORDT_NUMERO), EstadoEnEspera, transaccion);
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
        
        public static void ObtenerOrdenesProduccion(object codigo, object estado, object modo, object fechaGeneracion, object fechaDesde, object fechaHasta, Data.dsOrdenTrabajo dsOrdenTrabajo)
        {
            string sql = @"SELECT ordp_numero, ordp_codigo, eord_codigo, ordp_fechaalta, dpsem_codigo, ordpm_numero, ordp_origen, ordp_fechainicioestimada, 
                        ordp_fechainicioreal, ordp_fechafinestimada, ordp_fechafinreal, ordp_observaciones, ordp_prioridad, estr_codigo, 
                        ordp_cantidadestimada, ordp_cantidadreal, coc_codigo, ustck_destino 
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
            if (modo != null && modo.GetType() == cantidadParametros.GetType())
            {
                if (Convert.ToInt32(modo) == OrdenAutomatica)
                {
                    sql += " AND ordpm_numero IS NULL";
                }
                else if (Convert.ToInt32(modo) == OrdenManual)
                {
                    sql += " AND ordpm_numero IS NOT NULL";
                }                
            }

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
                    DB.FillDataSet(dsOrdenTrabajo, "ORDENES_PRODUCCION", sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
            else if (modo != null)
            {
                try
                {
                    DB.FillDataSet(dsOrdenTrabajo, "ORDENES_PRODUCCION", sql, null);
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

            object[] parametros = { EstadoFinalizado, numeroOrdenProduccion };

            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                //Finalizo la orden de producción
                DB.executeNonQuery(sql, parametros, transaccion);
                dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion).EORD_CODIGO = EstadoFinalizado;
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
                    UbicacionStockDAL.ActualizarCantidadesStock(ubicacion, cantidadReal, 0, transaccion);
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
