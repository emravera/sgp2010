using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class ModeloCocinaDAL
    {
        public static int Insertar(Entidades.ModeloCocina modeloCocina)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = @"INSERT INTO [MODELOS_COCINAS] ([mod_nombre],[mod_descripcion])
                          VALUES (@p0, @p1) SELECT @@Identity";
            object[] valorParametros = { modeloCocina.Nombre, modeloCocina.Descripcion };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            //Metodo para eliminar modelo de cocinas
            string sql = "DELETE FROM MODELOS_COCINAS WHERE mod_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.ModeloCocina modeloCocina)
        {
            string sql = @"UPDATE MODELOS_COCINAS SET
                         mod_nombre = @p0
                        ,mod_descripcion = @p1
                        WHERE mod_codigo = @p2";
            object[] valorParametros = { modeloCocina.Nombre, modeloCocina.Descripcion, modeloCocina.Codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsModeloCocina(Entidades.ModeloCocina modeloCocina)
        {
            string sql = "SELECT count(mod_codigo) FROM MODELOS_COCINAS WHERE mod_nombre = @p0";
            object[] valorParametros = { modeloCocina.Nombre };
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

        public static void ObtenerModeloCocina(string nombre, Data.dsModeloCocina ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT mod_codigo, mod_nombre, mod_descripcion
                              FROM MODELOS_COCINAS
                              WHERE mod_nombre LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "MODELOS_COCINAS", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
            else
            {
                string sql = "SELECT mod_codigo, mod_nombre, mod_descripcion FROM MODELOS_COCINAS";
                try
                {
                    DB.FillDataSet(ds, "MODELOS_COCINAS", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(coc_codigo) FROM COCINAS WHERE mod_codigo = @p0";
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
