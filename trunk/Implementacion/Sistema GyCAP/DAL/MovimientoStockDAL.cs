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
        public static void Insertar(Entidades.MovimientoStock movimientoStock, SqlTransaction transaccion)
        {
            string sql = @"INSERT INTO [MOVIMIENTOS_STOCK] 
                        ([mvto_codigo]
                        ,[mvto_descripcion]
                        ,[mvto_fechaalta]
                        ,[mvto_fechaprevista]
                        ,[mvto_fechareal]
                        ,[ustck_origen]
                        ,[ustck_destino]
                        ,[mvto_cantidad_origen]
                        ,[mvto_cantidad_destino]
                        ,[emvto_codigo]
                        ,[ordt_numero]) 
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10) SELECT @@Identity";

            
            object[] parametros = { movimientoStock.Codigo,
                                      movimientoStock.Descripcion,
                                      movimientoStock.FechaAlta,
                                      movimientoStock.FechaPrevista,
                                      DBNull.Value,
                                      movimientoStock.Origen.Numero,
                                      movimientoStock.Destino.Numero,
                                      movimientoStock.CantidadOrigen,
                                      movimientoStock.CantidadDestino,
                                      movimientoStock.Estado.Codigo,
                                      movimientoStock.OrdenTrabajo.Numero };

            movimientoStock.Numero = Convert.ToInt32(DB.executeScalar(sql, parametros, transaccion));
            //Actualizamos la ubicación de stock origen involucrada en el movimiento
            UbicacionStockDAL.ActualizarCantidadesStock(movimientoStock.Origen.Numero, 0, (movimientoStock.CantidadOrigen * -1), transaccion);
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
