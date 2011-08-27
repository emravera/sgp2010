﻿using System;
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
                            ustck_cantidadreal, umed_codigo, ustck_padre, ustck_activo, tus_codigo, con_codigo  
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
                            ustck_cantidadreal, umed_codigo, ustck_padre, ustck_activo, tus_codigo, con_codigo 
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
                         [umed_codigo], 
                         [ustck_padre], 
                         [ustck_activo],
                         [tus_codigo],
                         [con_codigo])
                         VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9) SELECT @@Identity";

            object padre = DBNull.Value;
            if (ubicacion.UbicacionPadre != null) { padre = ubicacion.UbicacionPadre.Numero; };
            object[] parametros = { ubicacion.Codigo,
                                      ubicacion.Nombre,
                                      ubicacion.Descripcion,
                                      ubicacion.UbicacionFisica,
                                      ubicacion.CantidadReal,
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
                         umed_codigo = @p5,
                         ustck_padre = @p6,
                         ustck_activo = @p7,
                         tus_codigo = @p8,
                         con_codigo = @p9 
                         WHERE ustck_numero = @p10";

            object padre = DBNull.Value;
            if (ubicacion.UbicacionPadre != null) { padre = ubicacion.UbicacionPadre.Numero; };
            object[] parametros = { ubicacion.Codigo,
                                      ubicacion.Nombre,
                                      ubicacion.Descripcion,
                                      ubicacion.UbicacionFisica,
                                      ubicacion.CantidadReal,
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

        public static bool EsUbicacionStock(string codigo)
        {
            string sql = "SELECT count(ustck_numero) FROM UBICACIONES_STOCK WHERE ustck_codigo = @p0";
            object[] parametros = { codigo };

            try
            {
                int resultado = Convert.ToInt32(DB.executeScalar(sql, parametros, null));
                if (resultado == 0) { return false; }
                else { return true; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool EsUbicacionStock(string codigo, int numero)
        {
            string sql = "SELECT count(ustck_numero) FROM UBICACIONES_STOCK WHERE ustck_codigo = @p0 AND ustck_numero <> @p1";
            object[] parametros = { codigo, numero };

            try
            {
                int resultado = Convert.ToInt32(DB.executeScalar(sql, parametros, null));
                if (resultado == 0) { return false; }
                else { return true; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void ActualizarCantidadesStock(int numeroUbicacion, decimal cantidadReal, SqlTransaction transaccion)
        {
            string sql = @"UPDATE UBICACIONES_STOCK SET ustck_cantidadreal = ( 
                                                                               CASE 
                                                                                 WHEN ((ustck_cantidadreal + @p1) < 0) THEN 0 
                                                                                 ELSE (ustck_cantidadreal + @p1)
                                                                               END
                                                                             )
                           WHERE ustck_numero = @p0";

            object[] parametros = { numeroUbicacion, cantidadReal };

            if (transaccion != null) { DB.executeNonQuery(sql, parametros, transaccion); }
            else
            {
                try
                {
                    DB.executeNonQuery(sql, parametros, null);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
        }
        
        public static void ActualizarCantidadesStockAndParents(int numeroUbicacion, decimal cantidadReal, SqlTransaction transaccion)
        {
            string sql = @"WHILE (@p0 IS NOT NULL) 
                            BEGIN 
	                            UPDATE UBICACIONES_STOCK SET ustck_cantidadreal = ( 
                                                                                     CASE 
                                                                                       WHEN ((ustck_cantidadreal + @p1) < 0) THEN 0 
                                                                                       ELSE (ustck_cantidadreal + @p1)
                                                                                     END
                                                                                   )
                                WHERE ustck_numero = @p0

	                            SET @p0 = (SELECT ustck_padre FROM UBICACIONES_STOCK WHERE ustck_numero = @p0)
	                            
                                CONTINUE 
                            END";
            
            object[] parametros = { numeroUbicacion, cantidadReal };

            if (transaccion != null) { DB.executeNonQuery(sql, parametros, transaccion); }
            else
            {
                try
                {
                    DB.executeNonQuery(sql, parametros, null);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
        }

        public static decimal CantidadMateriaPrima (Entidades.MateriaPrima materiaPrima)
        {
            decimal cantidadMP = 0;

            string sql = @"SELECT ustck_cantidadreal 
                           FROM UBICACIONES_STOCK
                           WHERE ustck_numero = @p0";

            object[] parametros = { materiaPrima.UbicacionStock.Numero };

            try
            {
                cantidadMP = Convert.ToDecimal(DB.executeScalar(sql, parametros,null));
                return cantidadMP;
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        
    }
}