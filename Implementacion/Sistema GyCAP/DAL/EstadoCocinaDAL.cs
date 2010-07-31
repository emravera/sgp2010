using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class EstadoCocinaDAL
    {
        public static void ObtenerEstados(DataTable dtEstados)
        {
            string sql = "SELECT ecoc_codigo, ecoc_nombre, ecoc_descripcion FROM ESTADO_COCINAS";

            try
            {
                DB.FillDataTable(dtEstados, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
