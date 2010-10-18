using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class MovimientoStockDAL
    {
        public static void Insertar(Entidades.MovimientoStock movimientoStock)
        {
            string sql = @"INSERT INTO [MOVIMIENTOS_STOCK] 
                        ([mvto_codigo]
                        ,[mvto_descripcion]
                        ,[mvto_fechaalta]
                        ,[mvto_fechaprevista]
                        ,[mvto_fechareal]
                        ,[ustck_origen]
                        ,[ustck_destino]
                        ,[mvto_cantidad]
                        ,[umed_codigo]
                        ,[emvto_codigo]) 
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9) SELECT @@Identity";

            object[] parametros = { movimientoStock.Codigo,
                                      movimientoStock.Descripcion,
                                      movimientoStock.FechaAlta,
                                      movimientoStock.FechaPrevista,
                                      movimientoStock.FechaReal,
                                      movimientoStock.Origen.Numero,
                                      movimientoStock.Destino.Numero,
                                      movimientoStock.Cantidad,
                                      movimientoStock.UnidadMedida.Codigo,
                                      movimientoStock.Estado.Codigo };

            try
            {
                movimientoStock.Numero = Convert.ToInt32(DB.executeScalar(sql, parametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Eliminar(int numeroMovimiento)
        {
            string sql = "DELETE FROM MOVIMIENTOS_STOCK WHERE mvto_numero = @p0";
            object[] parametros = { numeroMovimiento };

            try
            {
                DB.executeNonQuery(sql, parametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
