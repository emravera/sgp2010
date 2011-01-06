using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class PiezaEstructuraDAL
    {
        public static void Insertar(Entidades.PiezaEstructura piezaEstructura, SqlTransaction transaccion)
        {
            string sqlInsert = @"INSERT INTO [PIEZASXESTRUCTURA] 
                                ([estr_codigo]
                                ,[pza_codigo]
                                ,[pxe_cantidad])
                                VALUES (@p0, @p1, @p2) SELECT @@Identity";

            object[] valorParametros = { piezaEstructura.CodigoEstructura, 
                                           piezaEstructura.CodigoPieza, 
                                           piezaEstructura.CantidadPieza };

            piezaEstructura.CodigoDetalle = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));
        }

        public static void Delete(Entidades.PiezaEstructura pE, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM PIEZASXESTRUCTURA WHERE pxe_codigo = @p0";
            object[] valorParametros = { pE.CodigoDetalle };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }

        public static void Actualizar(Entidades.PiezaEstructura pE, SqlTransaction transaccion)
        {
            string sql = "UPDATE PIEZASXESTRUCTURA SET pxe_cantidad = @p0 WHERE pxe_codigo = @p1";
            object[] valorParametros = { pE.CantidadPieza, pE.CodigoDetalle };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }
        
        public static void DeleteDetalleEstructura(int codigoEstructura, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM PIEZASXESTRUCTURA WHERE estr_codigo = @p0";
            object[] valorParametros = { codigoEstructura };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }

        public static void ObtenerPiezasEstructura(int[] codigosEstructura, Data.dsEstructura ds)
        {
            string sql = "SELECT pxe_codigo, estr_codigo, pza_codigo, pxe_cantidad FROM PIEZASXESTRUCTURA WHERE estr_codigo IN (@p0)";
            object[] valorParametros = { codigosEstructura };
            DB.FillDataSet(ds, "PIEZASXESTRUCTURA", sql, valorParametros);
        }
    }
}
