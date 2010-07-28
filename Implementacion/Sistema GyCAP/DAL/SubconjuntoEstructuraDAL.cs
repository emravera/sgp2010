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
    }
}
