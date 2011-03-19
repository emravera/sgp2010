using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class CapacidadEmpleadoDAL
    {
        public static int Insertar(Entidades.CapacidadEmpleado capacidadEmpleado)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO CAPACIDAD_EMPLEADOS (CEMP_NOMBRE, CEMP_DESCRIPCION) VALUES (@p0,@p1) SELECT @@Identity";
            object[] valorParametros = { capacidadEmpleado.Nombre, capacidadEmpleado.Descripcion };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM CAPACIDAD_EMPLEADOS WHERE CEMP_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.CapacidadEmpleado capacidadEmpleado)
        {
            string sql = "UPDATE CAPACIDAD_EMPLEADOS SET CEMP_NOMBRE = @p1, CEMP_DESCRIPCION = @p2 WHERE CEMP_CODIGO = @p0";
            object[] valorParametros = { capacidadEmpleado.Codigo, capacidadEmpleado.Nombre, capacidadEmpleado.Descripcion };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsCapacidadEmpleado(Entidades.CapacidadEmpleado capacidadEmpleado)
        {
            string sql = "SELECT count(CEMP_CODIGO) FROM CAPACIDAD_EMPLEADOS WHERE CEMP_NOMBRE = @p0";
            object[] valorParametros = { capacidadEmpleado.Nombre };
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

        public static void ObtenerCapacidadEmpleado(string nombre, Data.dsEmpleado ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT CEMP_CODIGO, CEMP_NOMBRE, CEMP_DESCRIPCION
                              FROM CAPACIDAD_EMPLEADOS
                              WHERE CEMP_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "CAPACIDAD_EMPLEADOS", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = "SELECT CEMP_CODIGO, CEMP_NOMBRE, CEMP_DESCRIPCION FROM CAPACIDAD_EMPLEADOS";
                try
                {
                    DB.FillDataSet(ds, "CAPACIDAD_EMPLEADOS", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

        public static void ObtenerCapacidadEmpleado(Data.dsEmpleado ds)
        {
            string sql = @"SELECT CEMP_CODIGO, CEMP_NOMBRE, CEMP_DESCRIPCION
                              FROM CAPACIDAD_EMPLEADOS ";            
            try
            {
                DB.FillDataSet(ds, "CAPACIDAD_EMPLEADOS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        public static void ObtenerCapacidadEmpleado(DataTable dtCapacidadEmpleado)
        {
            string sql = @"SELECT CEMP_CODIGO, CEMP_NOMBRE, CEMP_DESCRIPCION
                              FROM CAPACIDAD_EMPLEADOS ";        

            try
            {
                //Se llena el Dataset
                DB.FillDataTable(dtCapacidadEmpleado, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerCapacidadPorEmpleado(int E_CODIGO, Data.dsEmpleado ds)
        {
            string sql = @"SELECT CAPACIDAD_EMPLEADOS.CEMP_CODIGO, CEMP_NOMBRE, CEMP_DESCRIPCION
                           FROM CAPACIDAD_EMPLEADOS,CAPACIDADESXEMPLEADO 
                           WHERE CAPACIDAD_EMPLEADOS.CEMP_CODIGO = CAPACIDADESXEMPLEADO.CEMP_CODIGO ";

            if (E_CODIGO != 0)
            {
                sql += " AND E_CODIGO = @p0";

                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                object[] valorParametros = { E_CODIGO };
                try
                {
                    DB.FillDataSet(ds, "CAPACIDAD_EMPLEADOS", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
            else
            {
                try
                {
                    DB.FillDataSet(ds, "CAPACIDAD_EMPLEADOS", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
        }

        public static void ObtenerCapacidadPorEmpleado(Data.dsEmpleado ds)
        {
            string sql = @"SELECT CXE_CODIGO, CAPACIDADESXEMPLEADO.CEMP_CODIGO, E_CODIGO
                           FROM CAPACIDAD_EMPLEADOS,CAPACIDADESXEMPLEADO 
                           WHERE CAPACIDAD_EMPLEADOS.CEMP_CODIGO = CAPACIDADESXEMPLEADO.CEMP_CODIGO ";

            try
            {
                DB.FillDataSet(ds, "CAPACIDADESXEMPLEADO", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(CXE_CODIGO) FROM CAPACIDADESXEMPLEADO WHERE CEMP_CODIGO = @p0";
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
