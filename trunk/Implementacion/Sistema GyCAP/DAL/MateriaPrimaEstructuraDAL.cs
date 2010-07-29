using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class MateriaPrimaEstructuraDAL
    {
        public static void Insertar(Entidades.MateriaPrimaEstructura materiaPrimaEstructura, SqlTransaction transaccion)
        {
            string sqlInsert = @"INSERT INTO [MATERIASPRIMASXESTRUCTURA] 
                               ([estr_codigo]
                               ,[mp_codigo]
                               ,[mpxe_cantidad]
                               ,[grp_codigo])
                               VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";
            
            object[] valorParametros = { materiaPrimaEstructura.CodigoEstructura,
                                         materiaPrimaEstructura.CodigoMateriaPrima,
                                         materiaPrimaEstructura.CantidadMateriaPrima,
                                         materiaPrimaEstructura.CodigoGrupo };

            materiaPrimaEstructura.CodigoDetalle = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));
        }

        public static void Delete(Entidades.MateriaPrimaEstructura mpE, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM MATERIASPRIMASXESTRUCTURA WHERE mpxe_codigo = @p0";
            object[] valorParametros = { mpE.CodigoDetalle };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }

        public static void Actualizar(Entidades.MateriaPrimaEstructura mpE, SqlTransaction transaccion)
        {
            string sql = "UPDATE MATERIASPRIMASXESTRUCTURA SET mpxe_cantidad = @p0 WHERE mpxe_codigo = @p1";
            object[] valorParametros = { mpE.CantidadMateriaPrima, mpE.CodigoDetalle };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }
        
        public static void DeleteDetalleEstructura(int codigoEstructura, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM MATERIASPRIMASXESTRUCTURA WHERE estr_codigo = @p0";
            object[] valorParametros = { codigoEstructura };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }
    }
}
