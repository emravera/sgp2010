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

        public static Data.dsStock.TIPOS_UBICACIONES_STOCKDataTable GetTipoUbicacion(int codigo)
        {
            string sql = "SELECT tus_codigo, tus_nombre, tus_descripcion FROM TIPOS_UBICACIONES_STOCK WHERE tus_codigo = @p0";
            object[] parametros = { codigo };

            try
            {
                Data.dsStock.TIPOS_UBICACIONES_STOCKDataTable dt = new GyCAP.Data.dsStock.TIPOS_UBICACIONES_STOCKDataTable();
                DB.FillDataTable(dt, sql, parametros);
                return dt;
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
