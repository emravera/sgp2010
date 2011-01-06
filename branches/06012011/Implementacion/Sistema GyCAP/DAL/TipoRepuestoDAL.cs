using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class TipoRepuestoDAL
    {
        public static long Insertar(Entidades.TipoRepuesto tipoRepuesto)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO TIPOS_REPUESTOS (TREP_NOMBRE, TREP_DESCRIPCION) VALUES (@p0,@p1) SELECT @@Identity";
            object[] valorParametros = { tipoRepuesto.Nombre, tipoRepuesto.Descripcion };
            try
            {
                return Convert.ToInt64(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM TIPOS_REPUESTOS WHERE TREP_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.TipoRepuesto tipoRepuesto)
        {
            string sql = "UPDATE TIPOS_REPUESTOS SET TREP_NOMBRE = @p1, TREP_DESCRIPCION = @p2 WHERE TREP_CODIGO = @p0";
            object[] valorParametros = { tipoRepuesto.Codigo, tipoRepuesto.Nombre, tipoRepuesto.Descripcion };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsTipoRepuesto(Entidades.TipoRepuesto tipoRepuesto)
        {
            string sql = "SELECT count(TREP_CODIGO) FROM TIPOS_REPUESTOS WHERE TREP_NOMBRE = @p0";
            object[] valorParametros = { tipoRepuesto.Nombre };
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

        public static void ObtenerTipoRepuesto(string nombre, Data.dsMantenimiento ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT TREP_CODIGO, TREP_NOMBRE, TREP_DESCRIPCION
                              FROM TIPOS_REPUESTOS
                              WHERE TREP_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "TIPOS_REPUESTOS", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = "SELECT TREP_CODIGO, TREP_NOMBRE, TREP_DESCRIPCION FROM TIPOS_REPUESTOS ";
                try
                {
                    DB.FillDataSet(ds, "TIPOS_REPUESTOS", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

        public static void ObtenerTipoRepuesto(Data.dsMantenimiento ds)
        {
            string sql = @"SELECT TREP_CODIGO, TREP_NOMBRE, TREP_DESCRIPCION
                           FROM TIPOS_REPUESTOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "TIPOS_REPUESTOS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //        public static void ObtenerTipoRepuesto(Data.dsCliente ds)
        //        {
        //            string sql = @"SELECT TREP_CODIGO, TREP_NOMBRE, TREP_DESCRIPCION
        //                           FROM TIPOS_REPUESTOS";
        //            try
        //            {
        //                //Se llena el Dataset
        //                DB.FillDataSet(ds, "ESTADO_PEDIDOS", sql, null);
        //            }
        //            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        //        }

        public static void ObtenerTipoRepuesto(DataTable dtTipoRepuesto)
        {
            string sql = @"SELECT TREP_CODIGO, TREP_NOMBRE, TREP_DESCRIPCION
                           FROM TIPOS_REPUESTOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataTable(dtTipoRepuesto, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(REP_CODIGO) FROM REPUESTOS WHERE TREP_CODIGO = @p0";
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
