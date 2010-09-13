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
        public static void Insertar(Data.dsOrdenTrabajo dsOrdenTrabajo)
        {
            string sql = @"INSERT INTO ORDENES_PRODUCCION 
                        ([ordp_numero]
                        ,[ordp_codigo]
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
                        ,[ordp_prioridad])
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @12) SELECT @@Identity";

            Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow rowOrden = dsOrdenTrabajo.ORDENES_PRODUCCION.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsOrdenTrabajo.ORDENES_PRODUCCIONRow;
            object ordpm = DBNull.Value, dpsem = DBNull.Value;
            if (rowOrden.IsORDPM_NUMERONull()) { dpsem = rowOrden.DPSEM_CODIGO; }
            else { ordpm = rowOrden.ORDPM_NUMERO; }
            object[] valoresParametros = { rowOrden.ORDP_NUMERO,
                                             rowOrden.ORDP_CODIGO,
                                             rowOrden.EORD_CODIGO,
                                             rowOrden.ORDP_FECHAALTA,
                                             dpsem,
                                             ordpm,
                                             rowOrden.ORDP_ORIGEN,
                                             rowOrden.ORDP_FECHAINICIOESTIMADA,
                                             DBNull.Value,
                                             rowOrden.ORDP_FECHAFINESTIMADA,
                                             DBNull.Value,
                                             rowOrden.ORDP_OBSERVACIONES,
                                             rowOrden.ORDP_PRIORIDAD };

            SqlTransaction transaccion = null;
            
            try
            {
                transaccion = DB.IniciarTransaccion();
                rowOrden.BeginEdit();
                rowOrden.ORDP_NUMERO = Convert.ToInt32(DB.executeScalar(sql, valoresParametros, transaccion));
                rowOrden.EndEdit();

                foreach (Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOrdenTrabajo in (Data.dsOrdenTrabajo.ORDENES_TRABAJORow[])dsOrdenTrabajo.ORDENES_TRABAJO.Select("ordp_numero = " + rowOrden.ORDP_NUMERO))
                {
                    rowOrdenTrabajo.BeginEdit();
                    rowOrdenTrabajo.ORDP_NUMERO = rowOrden.ORDP_NUMERO;
                    rowOrdenTrabajo.ORDT_NUMERO = OrdenTrabajoDAL.Insertar(rowOrdenTrabajo, transaccion);
                    rowOrdenTrabajo.EndEdit();
                }

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
