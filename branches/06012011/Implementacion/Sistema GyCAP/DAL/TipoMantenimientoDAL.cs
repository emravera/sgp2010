using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class TipoMantenimientoDAL
    {
        public static long Insertar(Entidades.TipoMantenimiento tipoMantenimiento)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO TIPOS_MANTENIMIENTOS (TMAN_NOMBRE, TMAN_DESCRIPCION) VALUES (@p0,@p1) SELECT @@Identity";
            object[] valorParametros = { tipoMantenimiento.Nombre, tipoMantenimiento.Descripcion };
            try
            {
                return Convert.ToInt64(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM TIPOS_MANTENIMIENTOS WHERE TMAN_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.TipoMantenimiento tipoMantenimiento)
        {
            string sql = "UPDATE TIPOS_MANTENIMIENTOS SET TMAN_NOMBRE = @p1, TMAN_DESCRIPCION = @p2 WHERE TMAN_CODIGO = @p0";
            object[] valorParametros = { tipoMantenimiento.Codigo, tipoMantenimiento.Nombre, tipoMantenimiento.Descripcion };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsTipoMantenimiento(Entidades.TipoMantenimiento tipoMantenimiento)
        {
            string sql = "SELECT count(TMAN_CODIGO) FROM TIPOS_MANTENIMIENTOS WHERE TMAN_NOMBRE = @p0";
            object[] valorParametros = { tipoMantenimiento.Nombre };
            try
            {
                if (Convert.ToInt64(DB.executeScalar(sql, valorParametros, null)) == 0)
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

        public static void ObtenerTipoMantenimiento(string nombre, Data.dsMantenimiento ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT TMAN_CODIGO, TMAN_NOMBRE, TMAN_DESCRIPCION
                              FROM TIPOS_MANTENIMIENTOS
                              WHERE TMAN_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "TIPOS_MANTENIMIENTOS", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = "SELECT TMAN_CODIGO, TMAN_NOMBRE, TMAN_DESCRIPCION FROM TIPOS_MANTENIMIENTOS ";
                try
                {
                    DB.FillDataSet(ds, "TIPOS_MANTENIMIENTOS", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

        public static void ObtenerTipoMantenimiento(Data.dsMantenimiento ds)
        {
            string sql = @"SELECT TMAN_CODIGO, TMAN_NOMBRE, TMAN_DESCRIPCION
                           FROM TIPOS_MANTENIMIENTOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "TIPOS_MANTENIMIENTOS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

//        public static void ObtenerTipoMantenimiento(Data.dsCliente ds)
//        {
//            string sql = @"SELECT TMAN_CODIGO, TMAN_NOMBRE, TMAN_DESCRIPCION
//                           FROM TIPOS_MANTENIMIENTOS";
//            try
//            {
//                //Se llena el Dataset
//                DB.FillDataSet(ds, "ESTADO_PEDIDOS", sql, null);
//            }
//            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
//        }

        public static void ObtenerTipoMantenimiento(DataTable dtTipoMantenimiento)
        {
            string sql = @"SELECT TMAN_CODIGO, TMAN_NOMBRE, TMAN_DESCRIPCION
                           FROM TIPOS_MANTENIMIENTOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataTable(dtTipoMantenimiento, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(MAN_CODIGO) FROM MANTENIMIENTOS WHERE TMAN_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                if (Convert.ToInt64(DB.executeScalar(sql, valorParametros, null)) == 0)
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
