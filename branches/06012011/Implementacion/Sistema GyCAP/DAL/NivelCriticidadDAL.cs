using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class NivelCriticidadDAL
    {
        public static int Insertar(Entidades.NivelCriticidad nivelCriticidad)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO NIVELES_CRITICIDAD (NCRI_NOMBRE) VALUES (@p0) SELECT @@Identity";
            object[] valorParametros = { nivelCriticidad.Nombre};
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM NIVELES_CRITICIDAD WHERE NCRI_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.NivelCriticidad nivelCriticidad)
        {
            string sql = "UPDATE NIVELES_CRITICIDAD SET NCRI_NOMBRE = @p1 WHERE NCRI_CODIGO = @p0";
            object[] valorParametros = { nivelCriticidad.Codigo, nivelCriticidad.Nombre};
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsNivelCriticidad(Entidades.NivelCriticidad nivelCriticidad)
        {
            string sql = "SELECT count(NCRI_CODIGO) FROM NIVELES_CRITICIDAD WHERE NCRI_NOMBRE = @p0";
            object[] valorParametros = { nivelCriticidad.Nombre };
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

        public static void ObtenerNivelCriticidad(string nombre, Data.dsMantenimiento ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT NCRI_CODIGO, NCRI_NOMBRE
                              FROM NIVELES_CRITICIDAD
                              WHERE NCRI_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "NIVELES_CRITICIDAD", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = "SELECT NCRI_CODIGO, NCRI_NOMBRE FROM NIVELES_CRITICIDAD ";
                try
                {
                    DB.FillDataSet(ds, "NIVELES_CRITICIDAD", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

        public static void ObtenerNivelCriticidad(Data.dsMantenimiento ds)
        {
            string sql = @"SELECT NCRI_CODIGO, NCRI_NOMBRE
                           FROM NIVELES_CRITICIDAD ";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "NIVELES_CRITICIDAD", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(AVE_CODIGO) FROM AVERIAS WHERE NCRI_CODIGO = @p0";
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
