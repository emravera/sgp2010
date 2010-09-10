using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient; 

namespace GyCAP.DAL
{
    public class EstadoEmpleadoDAL
    {
        public static int Insertar(Entidades.EstadoEmpleado estadoEmpleado)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO ESTADO_EMPLEADOS (EE_NOMBRE, EE_DESCRIPCION) VALUES (@p0,@p1) SELECT @@Identity";
            object[] valorParametros = { estadoEmpleado.Nombre, estadoEmpleado.Descripcion };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM ESTADO_EMPLEADOS WHERE EE_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.EstadoEmpleado estadoEmpleado)
        {
            string sql = "UPDATE ESTADO_EMPLEADOS SET EE_NOMBRE = @p1, EE_DESCRIPCION = @p2 WHERE EE_CODIGO = @p0";
            object[] valorParametros = { estadoEmpleado.Codigo, estadoEmpleado.Nombre, estadoEmpleado.Descripcion };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsEstadoEmpleado(Entidades.EstadoEmpleado estadoEmpleado)
        {
            string sql = "SELECT count(EE_CODIGO) FROM ESTADO_EMPLEADOS WHERE EE_NOMBRE = @p0";
            object[] valorParametros = { estadoEmpleado.Nombre };
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

        public static void ObtenerEstadosEmpleado(string nombre, Data.dsEstadoEmpleado ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT EE_CODIGO, EE_NOMBRE, EE_DESCRIPCION
                              FROM ESTADO_EMPLEADOS
                              WHERE EE_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "ESTADO_EMPLEADOS", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = "SELECT EE_CODIGO, EE_NOMBRE, EE_DESCRIPCION FROM ESTADO_EMPLEADOS ";
                try
                {
                    DB.FillDataSet(ds, "ESTADO_EMPLEADOS", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

        public static void ObtenerEstadosEmpleado(Data.dsEstadoEmpleado ds)
        {
            string sql = @"SELECT EE_CODIGO, EE_NOMBRE, EE_DESCRIPCION
                           FROM ESTADO_EMPLEADOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "ESTADO_EMPLEADOS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerEstadosEmpleado(Data.dsEmpleado ds)
        {
            string sql = @"SELECT EE_CODIGO, EE_NOMBRE, EE_DESCRIPCION
                           FROM ESTADO_EMPLEADOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "ESTADO_EMPLEADOS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(E_CODIGO) FROM EMPLEADOS WHERE EE_CODIGO = @p0";
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
