using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class CierreParcialOrdenTrabajoDAL
    {
        public static void Insertar(Entidades.CierreParcialOrdenTrabajo cierreOrdenTrabajo, SqlTransaction transaccion)
        {
            string sql = @"INSERT INTO [CIERRE_ORDEN_TRABAJO] 
                        ([ordt_numero]
                        ,[e_codigo]
                        ,[maq_codigo]
                        ,[cord_cantidad]
                        ,[cord_fechacierre]
                        ,[cord_horacierre]
                        ,[cord_observaciones]) 
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6) SELECT @@Identity";

            object maquina = DBNull.Value;
            if (cierreOrdenTrabajo.Maquina != null) { maquina = cierreOrdenTrabajo.Maquina.Codigo; }
            object[] parametros = { cierreOrdenTrabajo.OrdenTrabajo.Numero,
                                      cierreOrdenTrabajo.Empleado.Codigo,
                                      maquina,
                                      cierreOrdenTrabajo.Cantidad,
                                      cierreOrdenTrabajo.Fecha,
                                      cierreOrdenTrabajo.Hora,
                                      cierreOrdenTrabajo.Observaciones };

            cierreOrdenTrabajo.Codigo = Convert.ToInt32(DB.executeScalar(sql, parametros, transaccion));
        }

        public static void Eliminar(int codigoCierre)
        {
            string sql = "DELETE FROM CIERRE_ORDEN_TRABAJO WHERE cord_codigo = @p0";
            object[] parametros = { codigoCierre };

            try
            {
                DB.executeNonQuery(sql, parametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Actualizar(Entidades.CierreParcialOrdenTrabajo cierreOrdenTrabajo)
        {
            string sql = @"UPDATE CIERRE_ORDEN_TRABAJO SET 
                         e_codigo = @p0
                        ,maq_codigo = @p1
                        ,cord_cantidad = @p2
                        ,cord_fechacierre = @p3
                        ,cord_horacierre = @p4
                        ,cord_observaciones = @p5 
                        WHERE cord_codigo = @p6";

            object[] parametros = { cierreOrdenTrabajo.Empleado.Codigo,
                                      cierreOrdenTrabajo.Maquina.Codigo,
                                      cierreOrdenTrabajo.Cantidad,
                                      cierreOrdenTrabajo.Fecha,
                                      cierreOrdenTrabajo.Hora,
                                      cierreOrdenTrabajo.Observaciones };

            try
            {
                DB.executeNonQuery(sql, parametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
        public static void ObtenerCierresParcialesOrdenTrabajo(int numeroOrdenTrabajo, DataTable dtCierreParcialOrdenTrabajo)
        {
            string sql = @"SELECT cord_codigo, ordt_numero, e_codigo, maq_codigo, cord_cantidad, cord_fechacierre, cord_horacierre, cord_observaciones 
                        FROM CIERRE_ORDEN_TRABAJO WHERE ordt_numero = @p0";

            object[] parametros = { numeroOrdenTrabajo };

            try
            {
                DB.FillDataTable(dtCierreParcialOrdenTrabajo, sql, parametros);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
