using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class PlanoDAL
    {
        public static void ObtenerTodos(System.Data.DataTable dtPlano)
        {
            string sql = @"SELECT   pno_codigo, 
                                    pno_nombre, 
                                    pno_numero, 
                                    pno_observaciones, 
                                    pno_fechacreacion, 
                                    pno_habilitado, 
                                    pno_fechaHabilitado,
                                    pno_fechaDeshabilitado,
                                    pno_fechaModificacion,
                                    pno_letracambio 
                           FROM PLANOS";

            try
            {
                DB.FillDataTable(dtPlano, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
