using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class TipoUbicacionStockDAL
    {
        public static void ObtenerTiposUbicacionStock(DataTable dtTiposUbicacion)
        {
            string sql = "SELECT tus_codigo, tus_nombre, tus_descripcion FROM TIPOS_UBICACIONES_STOCK";

            try
            {
                DB.FillDataTable(dtTiposUbicacion, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
