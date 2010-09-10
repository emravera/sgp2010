using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class EstadoMaquinaDAL
    {
        public static int Insertar(Entidades.EstadoMaquina estadoMaquina)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO ESTADO_MAQUINAS (EMAQ_NOMBRE, EMAQ_DESCRIPCION) VALUES (@p0,@p1) SELECT @@Identity";
            object[] valorParametros = { estadoMaquina.Nombre, estadoMaquina.Descripcion };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM ESTADO_MAQUINAS WHERE EMAQ_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.EstadoMaquina estadoMaquina)
        {
            string sql = "UPDATE ESTADO_MAQUINAS SET EMAQ_NOMBRE = @p1, EMAQ_DESCRIPCION = @p2 WHERE EMAQ_CODIGO = @p0";
            object[] valorParametros = { estadoMaquina.Codigo, estadoMaquina.Nombre, estadoMaquina.Descripcion };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsEstadoMaquina(Entidades.EstadoMaquina estadoMaquina)
        {
            string sql = "SELECT count(EMAQ_CODIGO) FROM ESTADO_MAQUINAS WHERE EMAQ_NOMBRE = @p0";
            object[] valorParametros = { estadoMaquina.Nombre };
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

        public static void ObtenerEstadosMaquina(string nombre, Data.dsEstadoMaquina ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT EMAQ_CODIGO, EMAQ_NOMBRE, EMAQ_DESCRIPCION
                              FROM ESTADO_MAQUINAS
                              WHERE EMAQ_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "ESTADO_MAQUINAS", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = "SELECT EMAQ_CODIGO, EMAQ_NOMBRE, EMAQ_DESCRIPCION FROM ESTADO_MAQUINAS ";
                try
                {
                    DB.FillDataSet(ds, "ESTADO_MAQUINAS", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

        public static void ObtenerEstadosMaquina(Data.dsEstadoMaquina ds)
        {
            string sql = @"SELECT EMAQ_CODIGO, EMAQ_NOMBRE, EMAQ_DESCRIPCION
                           FROM ESTADO_MAQUINAS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "ESTADO_MAQUINAS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerEstadosMaquina(Data.dsMaquina ds)
        {
            string sql = @"SELECT EMAQ_CODIGO, EMAQ_NOMBRE, EMAQ_DESCRIPCION
                           FROM ESTADO_MAQUINAS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "ESTADO_MAQUINAS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(MAQ_CODIGO) FROM MAQUINAS WHERE EMAQ_CODIGO = @p0";
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
