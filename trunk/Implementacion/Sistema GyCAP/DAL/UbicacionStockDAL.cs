using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class UbicacionStockDAL
    {
        public static void ObtenerUbicacionesStock(DataTable dtUbicacionesStock)
        {
            string sql = @"SELECT ustck_numero, ustck_codigo, ustck_nombre, ustck_descripcion, ustck_ubicacionfisica, 
                            ustck_cantidadreal, ustck_cantidadvirtual, umed_codigo, ustck_padre, ustck_activo 
                            FROM UBICACIONES_STOCK";

            try
            {
                DB.FillDataTable(dtUbicacionesStock, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
