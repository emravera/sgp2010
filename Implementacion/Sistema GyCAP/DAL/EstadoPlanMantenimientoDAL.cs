using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class EstadoPlanMantenimientoDAL
    {
        public static int Insertar(Entidades.EstadoPlanMantenimiento estadoPlan)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO ESTADO_PLANES_MANTENIMIENTO (EPMAN_NOMBRE, EPMAN_DESCRIPCION) VALUES (@p0,@p1) SELECT @@Identity";
            object[] valorParametros = { estadoPlan.Nombre, estadoPlan.Descripcion };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM ESTADO_PLANES_MANTENIMIENTO WHERE EPMAN_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.EstadoPlanMantenimiento estadoPlan)
        {
            string sql = "UPDATE ESTADO_PLANES_MANTENIMIENTO SET EPMAN_NOMBRE = @p1, EPMAN_DESCRIPCION = @p2 WHERE EPMAN_CODIGO = @p0";
            object[] valorParametros = { estadoPlan.Codigo, estadoPlan.Nombre, estadoPlan.Descripcion };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsEstadoPlanMantenimiento(Entidades.EstadoPlanMantenimiento estadoPlan)
        {
            string sql = "SELECT count(EPMAN_CODIGO) FROM ESTADO_PLANES_MANTENIMIENTO WHERE EPMAN_NOMBRE = @p0";
            object[] valorParametros = { estadoPlan.Nombre };
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

        public static void ObtenerEstadosPlanMantenimiento(string nombre, Data.dsMantenimiento ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT EPMAN_CODIGO, EPMAN_NOMBRE, EPMAN_DESCRIPCION
                              FROM ESTADO_PLANES_MANTENIMIENTO
                              WHERE EPMAN_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "ESTADO_PLANES_MANTENIMIENTO", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = "SELECT EPMAN_CODIGO, EPMAN_NOMBRE, EPMAN_DESCRIPCION FROM ESTADO_PLANES_MANTENIMIENTO ";
                try
                {
                    DB.FillDataSet(ds, "ESTADO_PLANES_MANTENIMIENTO", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

//        public static void ObtenerEstadosPlanMantenimiento(Data.dsEstadoMantenimiento ds)
//        {
//            string sql = @"SELECT EPMAN_CODIGO, EPMAN_NOMBRE, EPMAN_DESCRIPCION
//                           FROM ESTADO_PLANES_MANTENIMIENTO";
//            try
//            {
//                //Se llena el Dataset
//                DB.FillDataSet(ds, "ESTADO_PLANES_MANTENIMIENTO", sql, null);
//            }
//            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
//        }

        public static void ObtenerEstadosPlanMantenimiento(Data.dsMantenimiento ds)
        {
            string sql = @"SELECT EPMAN_CODIGO, EPMAN_NOMBRE, EPMAN_DESCRIPCION
                           FROM ESTADO_PLANES_MANTENIMIENTO";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "ESTADO_PLANES_MANTENIMIENTO", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerEstadosPlanMantenimiento(DataTable dtEstadoDetallePlan)
        {
            string sql = @"SELECT EPMAN_CODIGO, EPMAN_NOMBRE, EPMAN_DESCRIPCION
                           FROM ESTADO_PLANES_MANTENIMIENTO";
            try
            {
                //Se llena el Dataset
                DB.FillDataTable(dtEstadoDetallePlan, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(EPMAN_CODIGO) FROM PLANES_MANTENIMIENTO WHERE EPMAN_CODIGO = @p0";
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
