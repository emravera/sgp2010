﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class ColorDAL
    {
        public static int Insertar(Entidades.Color color)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO [COLORES] ([col_nombre]) VALUES (@p0) SELECT @@Identity";
            object[] valorParametros = { color.Nombre };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM COLORES WHERE col_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.Color color)
        {
            string sql = "UPDATE COLORES SET col_nombre = @p0 WHERE col_codigo = @p1";
            object[] valorParametros = { color.Nombre, color.Codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        
        public static bool esColor(Entidades.Color color)
        {
            string sql = "SELECT count(col_codigo) FROM COLORES WHERE col_nombre = @p0";
            object[] valorParametros = { color.Nombre };
            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerColores(Data.dsColor ds)
        {
            string sql = "SELECT col_codigo, col_nombre FROM COLORES";
            try
            {
                DB.FillDataSet(ds, "COLORES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }            
        }

        public static void ObtenerColores(DataTable dtColor)
        {
            string sql = "SELECT col_codigo, col_nombre FROM COLORES";
            try
            {
                DB.FillDataTable(dtColor, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        
        public static void ObtenerColores(string nombre, Data.dsColor ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT col_codigo, col_nombre
                              FROM COLORES
                              WHERE col_nombre LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "COLORES", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
            else
            {
                string sql = "SELECT col_codigo, col_nombre FROM COLORES";
                try
                {
                    DB.FillDataSet(ds, "COLORES", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }            
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(coc_codigo) FROM COCINAS WHERE col_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        
    }
}
