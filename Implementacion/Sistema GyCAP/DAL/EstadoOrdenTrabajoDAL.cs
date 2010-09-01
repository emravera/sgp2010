using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class EstadoOrdenTrabajoDAL
    {
        public static void ObtenerEstados(DataTable dtEstadosOrden)
        {
            string sql = "SELECT eord_codigo, eord_nombre, eord_descripcion FROM ESTADO_ORDENES_TRABAJO";

            try
            {
                DB.FillDataTable(dtEstadosOrden, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
