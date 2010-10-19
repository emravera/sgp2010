using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class EstadoDetalleMantenimientoDAL
    {
        public static long Insertar(Entidades.EstadoDetalleMantenimiento estadoDetalleMantenimiento)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO ESTADO_DETALLE_MANTENIMIENTOS (EDMAN_NOMBRE, EDMAN_DESCRIPCION) VALUES (@p0,@p1) SELECT @@Identity";
            object[] valorParametros = { estadoDetalleMantenimiento.Nombre, estadoDetalleMantenimiento.Descripcion };
            try
            {
                return Convert.ToInt64(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(long codigo)
        {
            string sql = "DELETE FROM ESTADO_DETALLE_MANTENIMIENTOS WHERE EDMAN_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.EstadoDetalleMantenimiento estadoDetalleMantenimiento)
        {
            string sql = "UPDATE ESTADO_DETALLE_MANTENIMIENTOS SET EDMAN_NOMBRE = @p1, EDMAN_DESCRIPCION = @p2 WHERE EDMAN_CODIGO = @p0";
            object[] valorParametros = { estadoDetalleMantenimiento.Codigo, estadoDetalleMantenimiento.Nombre, estadoDetalleMantenimiento.Descripcion };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsEstadoDetalleMantenimiento(Entidades.EstadoDetalleMantenimiento estadoDetalleMantenimiento)
        {
            string sql = "SELECT count(EDMAN_CODIGO) FROM DETALLE_PLANES_MANTENIMIENTO WHERE EDMAN_NOMBRE = @p0";
            object[] valorParametros = { estadoDetalleMantenimiento.Nombre };
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

        public static void ObtenerEstadosDetalleMantenimiento(string nombre, Data.dsMantenimiento ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT EDMAN_CODIGO, EDMAN_NOMBRE, EDMAN_DESCRIPCION
                              FROM ESTADO_DETALLE_MANTENIMIENTOS
                              WHERE EDMAN_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "ESTADO_DETALLE_MANTENIMIENTOS", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = "SELECT EDMAN_CODIGO, EDMAN_NOMBRE, EDMAN_DESCRIPCION FROM ESTADO_DETALLE_MANTENIMIENTOS ";
                try
                {
                    DB.FillDataSet(ds, "ESTADO_DETALLE_MANTENIMIENTOS", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

        public static void ObtenerEstadosDetalleMantenimiento(Data.dsMantenimiento ds)
        {
            string sql = @"SELECT EDMAN_CODIGO, EDMAN_NOMBRE, EDMAN_DESCRIPCION
                           FROM ESTADO_DETALLE_MANTENIMIENTOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "ESTADO_DETALLE_MANTENIMIENTOS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

//        public static void ObtenerEstadosDetalleMantenimiento(Data.dsCliente ds)
//        {
//            string sql = @"SELECT EDMAN_CODIGO, EDMAN_NOMBRE, EDMAN_DESCRIPCION
//                           FROM ESTADO_DETALLE_MANTENIMIENTOS";
//            try
//            {
//                //Se llena el Dataset
//                DB.FillDataSet(ds, "ESTADO_DETALLE_MANTENIMIENTOS", sql, null);
//            }
//            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
//        }

        public static void ObtenerEstadosDetalleMantenimiento(DataTable dtEstadoDetalleMantenimiento)
        {
            string sql = @"SELECT EDMAN_CODIGO, EDMAN_NOMBRE, EDMAN_DESCRIPCION
                           FROM ESTADO_DETALLE_MANTENIMIENTOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataTable(dtEstadoDetalleMantenimiento, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(long codigo)
        {
            string sql = "SELECT count(DPMAN_CODIGO) FROM DETALLE_PLANES_MANTENIMIENTO WHERE EDMAN_CODIGO = @p0";
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
