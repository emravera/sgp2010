using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;


namespace GyCAP.DAL
{
    public class TerminacionDAL
    {
        public static long Insertar(Entidades.Terminacion terminacion)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO TERMINACIONES (TE_NOMBRE, TE_DESCRIPCION, TE_ABREVIATURA) VALUES (@p0,@p1,@p2) SELECT @@Identity";
            object[] valorParametros = { terminacion.Nombre, terminacion.Descripcion, terminacion.Abreviatura };
            try
            {
                return Convert.ToInt64(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM TERMINACIONES WHERE TE_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Actualizar(Entidades.Terminacion terminacion)
        {
            string sql = "UPDATE TERMINACIONES SET TE_NOMBRE = @p1, TE_DESCRIPCION = @p2, TE_ABREVIATURA = @p3 WHERE TE_CODIGO = @p0";
            object[] valorParametros = { terminacion.Codigo, terminacion.Nombre, terminacion.Descripcion, terminacion.Abreviatura };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool EsTeminacion(Entidades.Terminacion terminacion)
        {
            string sql = "SELECT count(TE_CODIGO) FROM TERMINACIONES WHERE TE_NOMBRE = @p0 AND te_codigo <> @p1";
            object[] valorParametros = { terminacion.Nombre, terminacion.Codigo };
            try
            {
                if (Convert.ToInt64(DB.executeScalar(sql, valorParametros, null)) == 0) { return false; }
                else { return true; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void ObtenerTerminacion(string nombre, System.Data.DataTable dt)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT TE_CODIGO, TE_NOMBRE, TE_DESCRIPCION, TE_ABREVIATURA 
                              FROM TERMINACIONES
                              WHERE TE_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataTable(dt, sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }

            }
            else
            {
                string sql = "SELECT TE_CODIGO, TE_NOMBRE, TE_DESCRIPCION, TE_ABREVIATURA FROM TERMINACIONES";
                try
                {
                    DB.FillDataTable(dt, sql, null);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }

            }
        }
        
        public static void ObtenerTerminacion(string nombre, Data.dsCocina ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT TE_CODIGO, TE_NOMBRE, TE_DESCRIPCION, TE_ABREVIATURA 
                              FROM TERMINACIONES
                              WHERE TE_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "TERMINACIONES", sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
                
            }
            else
            {
                string sql = "SELECT TE_CODIGO, TE_NOMBRE, TE_DESCRIPCION, TE_ABREVIATURA FROM TERMINACIONES";
                try
                {
                    DB.FillDataSet(ds, "TERMINACIONES", sql, null);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
                
            }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(COC_CODIGO) FROM COCINA WHERE TE_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                if (Convert.ToInt64(DB.executeScalar(sql, valorParametros, null)) == 0) { return true; }
                else { return false; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

    }
}
