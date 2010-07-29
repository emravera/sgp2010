using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class SubconjuntoEstructuraDAL
    {
        public static void Insertar(Entidades.SubconjuntoEstructura subconjuntoEstructura, SqlTransaction transaccion)
        {
            string sqlInsert = @"INSERT INTO [SUBCONJUNTOSXESTRUCTURA] 
                               ([estr_codigo]
                               ,[sconj_codigo]
                               ,[scxe_cantidad]
                               ,[grp_codigo])
                               VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            object[] valorParametros = { subconjuntoEstructura.CodigoEstructura, 
                                         subconjuntoEstructura.CantidadSubconjunto, 
                                         subconjuntoEstructura.CantidadSubconjunto, 
                                         subconjuntoEstructura.CodigoGrupo };

            subconjuntoEstructura.CodigoDetalle = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));
        }

        public static void Delete(Entidades.SubconjuntoEstructura scE, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM SUBCONJUNTOSXESTRUCTURA WHERE scxe_codigo = @p0";
            object[] valorParametros = { scE.CodigoDetalle };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }

        public static void Actualizar(Entidades.SubconjuntoEstructura scE, SqlTransaction transaccion)
        {
            string sql = "UPDATE SUBCONJUNTOSXESTRUCTURA SET scxe_cantidad = @p0 WHERE scxe_codigo = @p1";
            object[] valorParametros = { scE.CantidadSubconjunto, scE.CodigoDetalle };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }
        
        public static void DeleteDetalleEstructura(int codigoEstructura, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM SUBCONJUNTOSXESTRUCTURA WHERE estr_codigo = @p0";
            object[] valorParametros = { codigoEstructura };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }

        public static void ObtenerSubconjuntosEstructura(int[] codigosEstructura, Data.dsEstructura ds)
        {
            string sql = "SELECT scxe_codigo, estr_codigo, sconj_codigo, scxe_cantidad FROM SUBCONJUNTOSXESTRUCTURA WHERE estr_codigo IN (@p0)";
            object[] valorParametros = { codigosEstructura };
            DB.FillDataSet(ds, "SUBCONJUNTOSXESTRUCTURA", sql, valorParametros);
        }
    }
}
