using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class ContenidoUbicacionStockDAL
    {        
        public static void ObtenerContenidosUbicacionStock(DataTable dtContenidoUbicaiconStock)
        {
            string sql = "SELECT con_codigo, con_nombre, con_descripcion FROM CONTENIDO_UBICACION_STOCK";

            try
            {
                DB.FillDataTable(dtContenidoUbicaiconStock, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static int ObtenerCodigoUbicacion(string nombreUbicacionStock)
        {
            string sql = "SELECT con_codigo FROM CONTENIDO_UBICACION_STOCK where con_nombre = @p0";
            object[] parametros = { nombreUbicacionStock };
            int codigo = 0;

            try
            {
                codigo =Convert.ToInt32(DB.executeScalar(sql, parametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }

            return codigo;
        }

        public static Data.dsStock.CONTENIDO_UBICACION_STOCKDataTable GetContenidoUbicacionStock(int codigo)
        {
            string sql = "SELECT con_codigo, con_nombre, con_descripcion FROM CONTENIDO_UBICACION_STOCK WHERE con_codigo = @p0";
            object[] parametros = { codigo };

            try
            {
                Data.dsStock.CONTENIDO_UBICACION_STOCKDataTable dt = new GyCAP.Data.dsStock.CONTENIDO_UBICACION_STOCKDataTable();
                DB.FillDataTable(dt, sql, parametros);
                return dt;
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
