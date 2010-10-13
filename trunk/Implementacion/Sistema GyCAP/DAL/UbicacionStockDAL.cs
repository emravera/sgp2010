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

        public static void Insertar(Entidades.UbicacionStock ubicacion)
        {
            string sql = @"INSERT INTO [UBICACIONES_STOCK] 
                        ([ustck_codigo], 
                         [ustck_nombre], 
                         [ustck_descripcion], 
                         [ustck_ubicacionfisica], 
                         [ustck_cantidadreal], 
                         [ustck_cantidadvirtual], 
                         [umed_codigo], 
                         [ustck_padre], 
                         [ustck_activo])
                         VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8)";

            object padre = DBNull.Value;
            if (ubicacion.UbicacionPadre != null) { padre = ubicacion.UbicacionPadre.Numero; };
            object[] parametros = { ubicacion.Codigo,
                                      ubicacion.Nombre,
                                      ubicacion.Descripcion,
                                      ubicacion.UbicacionFisica,
                                      ubicacion.CantidadReal,
                                      ubicacion.CantidadVirtual,
                                      ubicacion.UnidadMedida.Codigo,
                                      padre,
                                      ubicacion.Activo };

            try
            {
                ubicacion.Numero = Convert.ToInt32(DB.executeScalar(sql, parametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
        public static void Eliminar(int numeroUbicacionStock)
        {
        }
        
        public static bool PuedeEliminarse(int numeroUbicacionStock)
        {
            return true;
        }
    }
}
