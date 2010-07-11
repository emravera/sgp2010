using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(Entidades.Color color)
        {
            string sql = "DELETE FROM COLORES WHERE col_codigo = @p0";
            object[] valorParametros = { color.Codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.Color color)
        {
            string sql = "UPDATE COLORES SET col_nombre = @p0 WHERE col_codigo = @p1";
            object[] valorParametros = { color.Nombre, color.Codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        
        public static void ObtenerColor(string nombre, Data.dsColor ds)
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
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
            else
            {
                string sql = "SELECT col_codigo, col_nombre FROM COLORES";
                try
                {
                    DB.FillDataSet(ds, "COLORES", sql, null);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }            
        }

        public static bool PuedeEliminarse(Entidades.Color color)
        {
            string sql = "SELECT count(coc_codigo) FROM COCINAS WHERE col_codigo = @p0";
            object[] valorParametros = { color.Codigo };
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        
    }
}
