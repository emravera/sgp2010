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
                            ustck_cantidadreal, ustck_cantidadvirtual, umed_codigo, ustck_padre, ustck_activo, tus_codigo, con_codigo  
                            FROM UBICACIONES_STOCK";

            try
            {
                DB.FillDataTable(dtUbicacionesStock, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void ObtenerUbicacionesStock(DataTable dtUbicacionesStock, int contenidoUbicacionStock)
        {
            string sql = @"SELECT ustck_numero, ustck_codigo, ustck_nombre, ustck_descripcion, ustck_ubicacionfisica, 
                            ustck_cantidadreal, ustck_cantidadvirtual, umed_codigo, ustck_padre, ustck_activo, tus_codigo, con_codigo 
                            FROM UBICACIONES_STOCK WHERE con_codigo = @p0";

            object[] parametros = { contenidoUbicacionStock };

            try
            {
                DB.FillDataTable(dtUbicacionesStock, sql, parametros);
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
                         [tus_codigo],
                         [con_codigo])
                         VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10) SELECT @@Identity";

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
                                      ubicacion.TipoUbicacion.Codigo,
                                      ubicacion.Contenido.Codigo };

            try
            {
                ubicacion.Numero = Convert.ToInt32(DB.executeScalar(sql, parametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Actualizar(Entidades.UbicacionStock ubicacion)
        {
            string sql = @"UPDATE UBICACIONES_STOCK SET 
                         ustck_codigo = @p0,
                         ustck_nombre = @p1,
                         ustck_descripcion = @p2,
                         ustck_ubicacionfisica = @p3,
                         ustck_cantidadreal = @p4,
                         ustck_cantidadvirtual = @p5,
                         umed_codigo = @p6,
                         ustck_padre = @p7,
                         ustck_activo = @p8,
                         tus_codigo = @p9,
                         con_codigo = @p10 
                         WHERE ustck_numero = @p11";

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
                                      ubicacion.TipoUbicacion.Codigo,
                                      ubicacion.Contenido.Codigo,
                                      ubicacion.Numero };

            try
            {
                DB.executeNonQuery(sql, parametros, null);
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
            string sqlMovimiento = "SELECT count(mvto_numero) FROM MOVIMIENTOS_STOCK WHERE ustck_origen = @p0 OR ustck_destino = @p0";
            string sqlProduccion = "SELECT count(ordp_numero) FROM ORDENES_PRODUCCION WHERE ustck_destino = @p0";
            string sqlTrabajo = "SELECT count(ordt_numero) FROM ORDENES_TRABAJO WHERE ustck_origen = @p0 OR ustck_destino = @p0";
            string sqlMP = "SELECT count(mp_codigo) FROM MATERIAS_PRIMAS WHERE ustck_numero = @p0";
            string sqlHR = "SELECT count(hr_codigo) FROM HOJAS_RUTA WHERE ustck_numero = @p0";
            string sqlDHR = "SELECT count(dhr_codigo) FROM DETALLE_HOJARUTA WHERE ustck_origen = @p0 OR ustck_destino = @p0";
            string sqlPadre = "SELECT count(ustck_numero) FROM UBICACIONES_STOCK WHERE ustck_padre = @p0";
            object[] parametros = { numeroUbicacionStock };

            try
            {
                int mvto = Convert.ToInt32(DB.executeScalar(sqlMovimiento, parametros, null));
                int produccion = Convert.ToInt32(DB.executeScalar(sqlProduccion, parametros, null));
                int trabajo = Convert.ToInt32(DB.executeScalar(sqlTrabajo, parametros, null));
                int mp = Convert.ToInt32(DB.executeScalar(sqlMP, parametros, null));
                int hr = Convert.ToInt32(DB.executeScalar(sqlHR, parametros, null));
                int dhr = Convert.ToInt32(DB.executeScalar(sqlDHR, parametros, null));
                int padre = Convert.ToInt32(DB.executeScalar(sqlPadre, parametros, null));

                if (mvto + produccion + trabajo + mp + hr + dhr + padre == 0) { return true; }
                else { return false; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool EsUbicacionStock(string codigo, string nombre)
        {
            string sql = "SELECT count(ustck_numero) FROM UBICACIONES_STOCK WHERE ustck_codigo = @p0 AND ustck_nombre = @p1";
            object[] parametros = { codigo, nombre };

            try
            {
                int resultado = Convert.ToInt32(DB.executeScalar(sql, parametros, null));
                if (resultado == 0) { return false; }
                else { return true; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
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
