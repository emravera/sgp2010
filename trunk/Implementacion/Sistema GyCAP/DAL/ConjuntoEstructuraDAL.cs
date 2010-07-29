using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class ConjuntoEstructuraDAL
    {
        public static void Insertar(Entidades.ConjuntoEstructura conjuntoEstructura, SqlTransaction transaccion)
        {
            string sqlInsert = @"INSERT INTO [CONJUNTOSXESTRUCTURA] 
                               ([estr_codigo]
                               ,[conj_codigo]
                               ,[cxe_cantidad]
                               ,[grp_codigo])
                               VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            object[] valorParametros = { conjuntoEstructura.CodigoEstructura, 
                                           conjuntoEstructura.CodigoConjunto, 
                                           conjuntoEstructura.CantidadConjunto, 
                                           conjuntoEstructura.CodigoGrupo };
            
            conjuntoEstructura.CodigoDetalle = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));
        }

        public static void Delete(Entidades.ConjuntoEstructura cE, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM CONJUNTOSXESTRUCTURA WHERE cxe_codigo = @p0";
            object[] valorParametros = { cE.CodigoDetalle };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }

        public static void Actualizar(Entidades.ConjuntoEstructura cE, SqlTransaction transaccion)
        {
            string sql = "UPDATE CONJUNTOSXESTRUCTURA SET cxe_cantidad = @p0 WHERE cxe_codigo = @p1";
            object[] valorParametros = { cE.CantidadConjunto, cE.CodigoDetalle };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }
        
        public static void DeleteDetalleEstructura(int codigoEstructura, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM CONJUNTOSXESTRUCTURA WHERE estr_codigo = @p0";
            object[] valorParametros = { codigoEstructura };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }

        public static void ObtenerConjuntosEstructura(int[] codigosEstructura, Data.dsEstructura ds)
        {
            string sql = "SELECT cxe_codigo, estr_codigo, conj_codigo, cxe_cantidad FROM CONJUNTOSXESTRUCTURA WHERE estr_codigo IN (@p0)";
            object[] valorParametros = { codigosEstructura };
            DB.FillDataSet(ds, "CONJUNTOSXESTRUCTURA", sql, valorParametros);
        }
    }
}
