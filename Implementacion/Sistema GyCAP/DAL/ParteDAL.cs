using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class ParteDAL
    {
        public static void Insertar(Data.dsEstructuraProducto dsEstructura)
        {
            string sql = @"INSERT INTO [PARTES] 
                       ([part_nombre],
                        [part_descripcion],
                        [part_codigo],
                        [pno_codigo],
                        [part_costo],
                        [part_costofijo],
                        [par_codigo],
                        [tpar_codigo],
                        [te_codigo],
                        [hr_codigo]) 
                        VALUES
                       (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9) SELECT @@Identity";

            object plano = DBNull.Value, terminacion = DBNull.Value;
            Data.dsEstructuraProducto.PARTESRow rowParte = dsEstructura.PARTES.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructuraProducto.PARTESRow;
            if (!rowParte.IsPNO_CODIGONull()) { plano = rowParte.PNO_CODIGO; }
            if (!rowParte.IsTE_CODIGONull()) { terminacion = rowParte.TE_CODIGO; }

            object[] parametros = { rowParte.PART_NOMBRE,
                                    rowParte.PART_DESCRIPCION,
                                    rowParte.PART_CODIGO,
                                    plano,
                                    rowParte.PART_COSTO,
                                    rowParte.PART_COSTOFIJO,
                                    rowParte.PAR_CODIGO,
                                    rowParte.TPAR_CODIGO,
                                    terminacion,
                                    rowParte.HR_CODIGO };

            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                //Insertamos la cabecera
                rowParte.BeginEdit();
                rowParte.PART_NUMERO = Convert.ToInt32(DB.executeScalar(sql, parametros, transaccion));
                rowParte.EndEdit();
                //Insertamos los hijos
                Entidades.CompuestoParte compuesto = new GyCAP.Entidades.CompuestoParte();
                foreach (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow row in (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])dsEstructura.COMPUESTOS_PARTES.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    //compuesto.PartePadre = Convert.ToInt32(rowParte.PART_NUMERO);
                    //compuesto.
                    
                    //conjuntoE.CodigoEstructura = Convert.ToInt32(rowEstructura.ESTR_CODIGO);
                    //conjuntoE.CodigoConjunto = Convert.ToInt32(row.CONJ_CODIGO);
                    //conjuntoE.CantidadConjunto = Convert.ToInt32(row.CXE_CANTIDAD);
                    //DAL.ConjuntoEstructuraDAL.Insertar(conjuntoE, transaccion);
                    //row.BeginEdit();
                    //row.CXE_CODIGO = conjuntoE.CodigoDetalle;
                    //row.EndEdit();
                }
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
