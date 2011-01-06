
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class EstadoParteDAL
    {
        public static void ObtenerTodos(System.Data.DataTable dtEstadoParte)
        {
            string sql = "SELECT par_codigo, par_nombre, par_descripcion FROM ESTADO_PARTES";

            try
            {
                DB.FillDataTable(dtEstadoParte, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
