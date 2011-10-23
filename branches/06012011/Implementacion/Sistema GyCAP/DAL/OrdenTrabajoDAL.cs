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
                         ,[ordt_fechafinestimada]
                         ,[ordt_observaciones]
                         ,[ordt_secuencia]
                         ,[dhr_codigo]
                         ,[ordt_numero_padre]
                         ,[ordt_tipo])
                         VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13) SELECT @@Identity";

            object hr = DBNull.Value, padre = DBNull.Value;
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
                                             orden.FechaInicioEstimada.Value.ToString("yyyyMMdd HH:mm"),
                                             orden.FechaFinEstimada.Value.ToString("yyyyMMdd HH:mm"),
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
        
        public static void IniciarOrdenTrabajo(OrdenTrabajo ordenT, SqlTransaction transaccion)
        {
            string sql = "UPDATE ORDENES_TRABAJO SET eord_codigo = @p0, ordt_fechainicioreal = @p1 WHERE ordt_numero = @p2";
            
            object[] parametros = { ordenT.Estado.Codigo, 
                                     ordenT.FechaInicioReal.Value.ToString("yyyyMMdd HH:mm"), 
                                     ordenT.Numero };

            DB.executeNonQuery(sql, parametros, transaccion);
        }

        public static void ActualizarEstado(OrdenTrabajo ordenT, SqlTransaction transaccion)
        {
            string sql = "UPDATE ORDENES_TRABAJO SET eord_codigo = @p0 WHERE ordt_numero = @p1";
            object[] parametros = { ordenT.Estado.Codigo, ordenT.Numero };
            DB.executeNonQuery(sql, parametros, transaccion);
        }
        
        public static void ObtenerOrdenesTrabajo(int numeroOrdenProduccion, Data.dsOrdenTrabajo dsOrdenTrabajo, bool obtenerCierresParciales)
        {
            string sql = @"SELECT ordt_numero, ordt_codigo, ordp_numero, eord_codigo, part_numero, ordt_origen, ordt_cantidadestimada,
                         ordt_cantidadreal, ordt_fechainicioestimada, ordt_fechainicioreal, ordt_fechafinestimada, ordt_fechafinreal,
                         ordt_observaciones, ordt_secuencia, dhr_codigo, ordt_numero_padre, ordt_tipo  
                        FROM ORDENES_TRABAJO WHERE ordp_numero = @p0 ORDER BY ordt_fechainicioestimada ASC, ordt_fechafinestimada ASC";

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
        
        public static void RegistrarCierreParcial(CierreParcialOrdenTrabajo cierre, SqlTransaction transaccion)
        {
            string sql = "UPDATE ORDENES_TRABAJO SET ordt_cantidadreal = ordt_cantidadreal + @p0 WHERE ordt_numero = @p1";

            object[] parametros = { cierre.Cantidad, cierre.OrdenTrabajo.Numero };

            DB.executeNonQuery(sql, parametros, transaccion);            
        }

        public static void FinalizarOrdenTrabajo(OrdenTrabajo ordenT, SqlTransaction transaccion)
        {
            string sql = @"UPDATE ORDENES_TRABAJO SET 
                         eord_codigo = @p0
                        ,ordt_fechainicioreal = @p1
                        ,ordt_cantidadreal = @p2
                        ,ordt_fechafinreal = @p3
                        WHERE ordt_numero = @p4";

            object[] parametros = { ordenT.Estado.Codigo, 
                                      ordenT.FechaInicioReal.Value.ToString("yyyMMdd HH:mm"), 
                                      ordenT.CantidadReal, 
                                      ordenT.FechaFinReal.Value.ToString("yyyyMMdd HH:mm"),
                                      ordenT.Numero };

            DB.executeNonQuery(sql, parametros, transaccion);
        }
    }
}
