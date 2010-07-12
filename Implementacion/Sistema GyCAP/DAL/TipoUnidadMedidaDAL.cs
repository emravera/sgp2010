using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class TipoUnidadMedidaDAL
    {
        public static int Insertar(Entidades.TipoUnidadMedida tipoUnidadMedida)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO [TIPOS_UNIDADES_MEDIDA] ([tumed_nombre]) VALUES (@p0) SELECT @@Identity";
            object[] valorParametros = { tipoUnidadMedida.Nombre };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM TIPOS_UNIDADES_MEDIDA WHERE tumed_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.TipoUnidadMedida tipoUnidadMedida)
        {
            string sql = "UPDATE TIPOS_UNIDADES_MEDIDA SET tumed_nombre = @p0 WHERE tumed_codigo = @p1";
            object[] valorParametros = { tipoUnidadMedida.Nombre, tipoUnidadMedida.Codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsTipoUnidadMedida(Entidades.TipoUnidadMedida tipoUnidadMedida)
        {
            string sql = "SELECT count(tumed_codigo) FROM TIPOS_UNIDADES_MEDIDA WHERE col_nombre = @p0";
            object[] valorParametros = { tipoUnidadMedida.Nombre };
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

        public static void ObtenerTipoUnidadMedida(string nombre, Data.dsUnidadMedida ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT tumed_codigo, tumed_nombre
                              FROM TIPOS_UNIDADES_MEDIDA
                              WHERE tumed_nombre LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "TIPOS_UNIDADES_MEDIDA", sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
            else
            {
                string sql = "SELECT tumed_codigo, tumed_nombre FROM TIPOS_UNIDADES_MEDIDA";
                try
                {
                    DB.FillDataSet(ds, "TIPOS_UNIDADES_MEDIDA", sql, null);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(tumed_codigo) FROM UNIDADES_MEDIDA WHERE tumed_codigo = @p0";
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
