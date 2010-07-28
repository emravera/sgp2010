using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            object[] valorParametros = { grupo.Numero, 
                                           grupo.CodigoEstructura, 
                                           grupo.CodigoPadre, 
                                           grupo.NombreGrupo, 
                                           grupo.Descripcion, 
                                           grupo.Concreto };
            
            grupo.CodigoGrupo = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));
        }
    }
}
