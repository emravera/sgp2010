using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class ProvinciaDAL
    {
        public static int Insertar(string nombre)
        {
            string sql = "INSERT INTO [PROVINCIAS] ([pcia_nombre]) VALUES (@p0) SELECT @@Identity";
            object[] valoresParametros = { nombre };

            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valoresParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM PROVINCIAS WHERE pcia_codigo = @p0";
            object[] valoresParametros = { codigo };

            try
            {
                DB.executeNonQuery(sql, valoresParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Actualizar(Entidades.Provincia provincia)
        {
            string sql = "UPDATE PROVINCIAS SET pcia_nombre = @p0 WHERE pcia_codigo = @p1";
            object[] valoresParametros = { provincia.Nombre, provincia.Codigo };

            try
            {
                DB.executeNonQuery(sql, valoresParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
        public static void ObtenerProvincias(DataTable dtProvincias)
        {
            string sql = "SELECT pcia_codigo, pcia_nombre FROM PROVINCIAS";

            try
            {
                DB.FillDataTable(dtProvincias, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(pcia_codigo) FROM LOCALIDADES WHERE pcia_codigo = @p0";
            object[] valoresParametros = { codigo };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valoresParametros, null)) == 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool EsProvincia(string nombre)
        {
            string sql = "SELECT count(pcia_codigo) FROM PROVINCIAS WHERE pcia_nombre = @p0";
            object[] valoresParametros = { nombre };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valoresParametros, null)) == 0) { return false; }
                else { return true; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
