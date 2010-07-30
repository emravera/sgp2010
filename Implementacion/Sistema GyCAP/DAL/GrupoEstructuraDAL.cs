using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class GrupoEstructuraDAL
    {
        public static void Insertar(Entidades.GrupoEstructura grupo, SqlTransaction transaccion)
        {
            string sqlInsert = @"INSERT INTO [GRUPOS_ESTRUCTURA] 
                                    ([grp_numero]
                                    ,[estr_codigo]
                                    ,[grp_padre_codigo]
                                    ,[grp_nombre]
                                    ,[grp_descripcion]
                                    ,[grp_concreto])
                                    VALUES (@p0, @p1, @p2, @p3, @p4, @p5)";

            object codPadre = DBNull.Value;
            if (grupo.CodigoPadre != -1) { codPadre = grupo.CodigoPadre; }
            object[] valorParametros = { grupo.Numero, 
                                           grupo.CodigoEstructura, 
                                           codPadre, 
                                           grupo.NombreGrupo, 
                                           grupo.Descripcion, 
                                           grupo.Concreto };
            
            grupo.CodigoGrupo = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));
        }

        public static void Delete(Entidades.GrupoEstructura grupo, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM GRUPOS_ESTRUCTURA WHERE grp_codigo = @p0";
            object[] valorParametros = { grupo.CodigoGrupo };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }

        public static void Actualizar(Entidades.GrupoEstructura gE, SqlTransaction transaccion)
        {
            string sql = @"UPDATE GRUPOS_ESTRUCTURA SET 
                           grp_numero = @p0
                          ,grp_codigo_padre = @p1
                          ,grp_nombre = @p2
                          ,grp_descripcion = @p3
                          ,grp_concreto = @p4
                         WHERE grp_codigo = @p5";

            object[] valorParametros = { gE.Numero, gE.CodigoPadre, gE.NombreGrupo, gE.Descripcion, gE.Concreto };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }
        
        public static void DeleteGruposEstructura(int codigoEstructura, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM GRUPOS_ESTRUCTURA WHERE estr_codigo = @p0";
            object[] valorParametros = { codigoEstructura };
            DB.executeNonQuery(sql, valorParametros, transaccion);
        }

        public static void ObtenerGruposEstructura(int[] codigosEstructura, Data.dsEstructura ds)
        {
            string sql = @"SELECT grp_codigo, grp_numero, estr_codigo, grp_padre_codigo, grp_nombre, grp_descripcion, grp_concreto 
                         FROM GRUPOS_ESTRUCTURA WHERE estr_codigo IN (@p0)";

            object[] valorParametros = { codigosEstructura };
            DB.FillDataSet(ds, "GRUPOS_ESTRUCTURA", sql, valorParametros);
        }
    }
}
