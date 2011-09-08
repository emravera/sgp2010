using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class RolDAL
    {
        public static long Insertar(Entidades.Rol rol)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO ROLES (ROL_NOMBRE, ROL_DESCRIPCION, ROL_PERMISO) VALUES (@p0,@p1,@P2) SELECT @@Identity";
            object[] valorParametros = { rol.Nombre, rol.Descripcion, rol.Permiso };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM ROLES WHERE ROL_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.Rol rol)
        {
            string sql = "UPDATE ROLES SET ROL_NOMBRE = @p1, ROL_DESCRIPCION = @p2, ROL_PERMISO = @p3 WHERE ROL_CODIGO = @p0";
            object[] valorParametros = { rol.Codigo, rol.Nombre, rol.Descripcion, rol.Permiso };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsRol(Entidades.Rol rol)
        {
            string sql = "SELECT count(ROL_CODIGO) FROM ROLES WHERE ROL_NOMBRE = @p0";
            object[] valorParametros = { rol.Nombre };
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

        public static void ObtenerRoles(string nombre, Data.dsSeguridad ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT ROL_CODIGO, ROL_NOMBRE, ROL_DESCRIPCION, ROL_PERMISO
                              FROM ROLES
                              WHERE ROL_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "ROLES", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = "SELECT ROL_CODIGO, ROL_NOMBRE, ROL_DESCRIPCION, ROL_PERMISO FROM ROLES ";
                try
                {
                    DB.FillDataSet(ds, "ROLES", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

        public static void ObtenerRoles(Data.dsSeguridad ds)
        {
            string sql = @"SELECT ROL_CODIGO, ROL_NOMBRE, ROL_DESCRIPCION, ROL_PERMISO
                           FROM ROLES";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "ROLES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerRoles(DataTable dtRol)
        {
            string sql = @"SELECT ROL_CODIGO, ROL_NOMBRE, ROL_DESCRIPCION, ROL_PERMISO
                           FROM ROLES";
            try
            {
                //Se llena el Dataset
                DB.FillDataTable(dtRol, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(U_CODIGO) FROM USUARIOS WHERE ROL_CODIGO = @p0";
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
