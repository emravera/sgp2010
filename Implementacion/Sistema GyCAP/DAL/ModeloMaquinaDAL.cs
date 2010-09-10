using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient; 

namespace GyCAP.DAL
{
    public class ModeloMaquinaDAL
    {
        public static int Insertar(Entidades.ModeloMaquina  modeloMaquina)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO MODELOS_MAQUINAS (MODM_NOMBRE, MODM_DESCRIPCION) VALUES (@p0,@p1) SELECT @@Identity";
            object[] valorParametros = { modeloMaquina.Nombre, modeloMaquina.Descripcion };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM MODELOS_MAQUINAS WHERE MODM_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.ModeloMaquina modeloMaquina)
        {
            string sql = "UPDATE MODELOS_MAQUINAS SET MODM_NOMBRE = @p1, MODM_DESCRIPCION = @p2 WHERE MODM_CODIGO = @p0";
            object[] valorParametros = { modeloMaquina.Codigo, modeloMaquina.Nombre, modeloMaquina.Descripcion };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsModeloMaquina(Entidades.ModeloMaquina modeloMaquina)
        {
            string sql = "SELECT count(MODM_CODIGO) FROM MODELOS_MAQUINAS WHERE MODM_NOMBRE = @p0";
            object[] valorParametros = { modeloMaquina.Nombre };
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

        public static void ObtenerModeloMaquina(string nombre, Data.dsModeloMaquina ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT MODM_CODIGO, MODM_NOMBRE, MODM_DESCRIPCION
                              FROM MODELOS_MAQUINAS
                              WHERE MODM_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "MODELOS_MAQUINAS", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = "SELECT MODM_CODIGO, MODM_NOMBRE, MODM_DESCRIPCION FROM MODELOS_MAQUINAS";
                try
                {
                    DB.FillDataSet(ds, "MODELOS_MAQUINAS", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

        public static void ObtenerModeloMaquina(Data.dsMaquina ds)
        {
            string sql = @"SELECT MODM_CODIGO, MODM_NOMBRE, MODM_DESCRIPCION
                              FROM MODELOS_MAQUINAS ";
            try
            {
                DB.FillDataSet(ds, "MODELOS_MAQUINAS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(MAQ_CODIGO) FROM MAQUINAS WHERE MODM_CODIGO = @p0";
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
