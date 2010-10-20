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
                            ustck_cantidadreal, ustck_cantidadvirtual, umed_codigo, ustck_padre, ustck_activo, tus_codigo 
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
                         [ustck_activo],
                         [tus_codigo])
                         VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9) SELECT @@Identity";

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
                                      ubicacion.Activo,
                                      ubicacion.TipoUbicacion.Codigo };

            try
            {
                ubicacion.Numero = Convert.ToInt32(DB.executeScalar(sql, parametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
        public static void Eliminar(int numeroUbicacionStock)
        {
            string sql = "DELETE FROM UBICACIONES_STOCK WHERE ustck_numero = @p0";
            object[] parametros = { numeroUbicacionStock };
            try
            {
                DB.executeNonQuery(sql, parametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
        public static bool PuedeEliminarse(int numeroUbicacionStock)
        {
            return true;
        }

        public static void ActualizarCantidadesStock(int numeroUbicacion, decimal cantidadReal, decimal cantidadVirtual, SqlTransaction transaccion)
        {
            string sql = @"UPDATE UBICACIONES_STOCK SET 
                         ustck_cantidadreal = ustck_cantidadreal + @p0 
                        ,ustck_cantidadvirtual = ustck_cantidadvirtual + @p1 
                        WHERE ustck_numero = @p2";
            
            object[] parametros = { cantidadReal, cantidadVirtual, numeroUbicacion };

            DB.executeNonQuery(sql, parametros, transaccion);
        }
    }
}
