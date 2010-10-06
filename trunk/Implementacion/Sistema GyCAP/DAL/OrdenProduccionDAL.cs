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
                        ,[coc_codigo])
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15) SELECT @@Identity";

            Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow rowOrdenP = dsOrdenTrabajo.ORDENES_PRODUCCION.FindByORDP_NUMERO(numeroOrdenProduccion);
            object ordpm = DBNull.Value, dpsem = DBNull.Value, cocina = DBNull.Value;
            if (rowOrdenP.IsORDPM_NUMERONull()) { dpsem = rowOrdenP.DPSEM_CODIGO; }
            else { ordpm = rowOrdenP.ORDPM_NUMERO; }
            if (!rowOrdenP.IsORDPM_NUMERONull()) { cocina = rowOrdenP.COC_CODIGO; }
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
                                             cocina };

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
    }
}
