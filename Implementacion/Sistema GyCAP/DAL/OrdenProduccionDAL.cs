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
                        ,[ordp_fechafinestimada]
                        ,[ordp_observaciones]
                        ,[ordp_prioridad]
                        ,[estr_codigo]
                        ,[ordp_cantidadestimada]
                        ,[ordp_cantidadreal]
                        ,[coc_codigo]
                        ,[ustck_destino]
                        ,[lot_codigo])
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14) SELECT @@Identity";

            object lote = DBNull.Value;            
            if (orden.Lote != null) { lote = orden.Lote.Codigo; }
            object[] valoresParametros = {   
                                             orden.Codigo,
                                             orden.Estado.Codigo,
                                             orden.FechaAlta,
                                             orden.DetallePlanSemanal.Codigo,
                                             orden.Origen,
                                             orden.FechaInicioEstimada.Value.ToString("yyyyMMdd HH:mm"),
                                             orden.FechaFinEstimada.Value.ToString("yyyyMMdd HH:mm"),
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
            
            IList<OrdenTrabajo> ordenesTrabajo = arbol.AsOrdenesTrabajoList();
            
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

        public static void IniciarOrdenProduccion(OrdenProduccion ordenP, SqlTransaction transaccion)
        {
            string sql = "UPDATE ORDENES_PRODUCCION SET eord_codigo = @p0, ordp_fechainicioreal = @p1 WHERE ordp_numero = @p2";
            
            object[] parametros = { ordenP.Estado.Codigo,
                                    ordenP.FechaInicioReal.Value.ToString("yyyyMMdd"),
                                    ordenP.Numero };
            
             DB.executeNonQuery(sql, parametros, transaccion);            
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

        public static void FinalizarOrdenProduccion(OrdenProduccion ordenP, SqlTransaction transaccion)
        {
            string sql = @"UPDATE ORDENES_PRODUCCION SET 
                        eord_codigo = @p0, 
                        ordp_fechafinreal = @p1, 
                        ordp_cantidadreal = @p2
                        WHERE ordp_numero = @p3;";

            object[] parametros = { ordenP.Estado.Codigo, ordenP.FechaFinReal.Value.ToString("yyyyMMdd"), ordenP.CantidadReal, ordenP.Numero };

            DB.executeNonQuery(sql, parametros, transaccion);
         }
    }
}
