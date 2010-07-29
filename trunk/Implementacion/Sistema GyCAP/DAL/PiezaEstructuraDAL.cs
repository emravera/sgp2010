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
                                ,[pxe_cantidad]
                                ,[grp_codigo])
                                VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            object[] valorParametros = { piezaEstructura.CodigoEstructura, 
                                           piezaEstructura.CantidadPieza, 
                                           piezaEstructura.CantidadPieza, 
                                           piezaEstructura.CodigoGrupo };

            piezaEstructura.CodigoDetalle = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));
        }

        public static void DeleteDetalleEstructura(int codigoEstructura, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM PIEZASXESTRUCTURA WHERE estr_codigo = @p0";
            object[] valorParametros = { codigoEstructura };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }
    }
}
