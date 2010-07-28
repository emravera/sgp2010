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
    }
}
