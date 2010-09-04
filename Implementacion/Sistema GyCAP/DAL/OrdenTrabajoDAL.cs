using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class OrdenTrabajoDAL
    {
        public static void Insertar(Data.dsOrdenTrabajo dsOrdenTrabajo)
        {
            string sql = @"INSERT INTO ORDENES_TRABAJO 
                        ([ord_codigo]
                        ,[eord_codigo]
                        ,[ord_fechaalta]
                        ,[dpsem_codigo]
                        ,[ordm_numero]
                        ,[ord_origen]
                        ,[ord_fechainicioestimada]
                        ,[ord_fechainicioreal]
                        ,[ord_fechafinestimada]
                        ,[ord_fechafinreal]
                        ,[ord_observaciones]
                        ,[ord_prioridad])
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11) SELECT @@Identity";

            Data.dsOrdenTrabajo.ORDENES_TRABAJORow rowOrden = dsOrdenTrabajo.ORDENES_TRABAJO.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsOrdenTrabajo.ORDENES_TRABAJORow;
            object ord = DBNull.Value, ordm = DBNull.Value, dpsem = DBNull.Value;
            object[] valoresParametros = { rowOrden.ORD_CODIGO,
                                             rowOrden.EORD_CODIGO,
                                             rowOrden.ORD_FECHAALTA,
                                             dpsem,
                                             ordm,
                                             rowOrden.ORD_ORIGEN,
                                             rowOrden.ORD_FECHAINICIOESTIMADA,
                                             DBNull.Value,
                                             rowOrden.ORD_FECHAFINESTIMADA,
                                             DBNull.Value,
                                             rowOrden.ORD_OBSERVACIONES,
                                             rowOrden.ORD_PRIORIDAD };

            SqlTransaction transaccion = null;
            try
            {
                transaccion = DB.IniciarTransaccion();
                rowOrden.BeginEdit();
                rowOrden.ORD_NUMERO = Convert.ToInt32(DB.executeScalar(sql, valoresParametros, transaccion));
                rowOrden.EndEdit();

                foreach (Data.dsOrdenTrabajo.DETALLE_ORDENES_TRABAJORow rowDetalle in (Data.dsOrdenTrabajo.DETALLE_ORDENES_TRABAJORow[])dsOrdenTrabajo.DETALLE_ORDENES_TRABAJO.Select("ord_numero = " + rowOrden.ORD_NUMERO))
                {
                    rowDetalle.BeginEdit();
                    rowDetalle.ORD_NUMERO = rowOrden.ORD_NUMERO;
                    rowDetalle.DORD_NUMERO = DetalleOrdenTrabajoDAL.Insertar(rowDetalle, transaccion);
                    rowDetalle.EndEdit();
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
