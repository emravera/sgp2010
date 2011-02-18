using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class LocalidadDAL
    {

        #region Busquedas

        public static void ObtenerLocalidades(DataTable dtLocalidades)
        {
            string sql = "SELECT loc_codigo, pcia_codigo, loc_nombre FROM LOCALIDADES";

            try
            {
                DB.FillDataTable(dtLocalidades, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void ObtenerLocalidades(object nombre, object codProvincia, DataTable dtLocalidades)
        {
            string sql = "SELECT loc_codigo, pcia_codigo, loc_nombre FROM LOCALIDADES WHERE 1=1";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[2];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND loc_nombre LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }
            //Revisamos si pasó algun valor y si es un integer
            if (codProvincia != null && codProvincia.GetType() == cantidadParametros.GetType())
            {
                sql += " AND pcia_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(codProvincia);
                cantidadParametros++;
            }

            if (cantidadParametros > 0)
            {
                //Buscamos con filtro, armemos el array de los valores de los parametros
                object[] valorParametros = new object[cantidadParametros];
                for (int i = 0; i < cantidadParametros; i++)
                {
                    valorParametros[i] = valoresFiltros[i];
                }
                try
                {
                    DB.FillDataTable(dtLocalidades, sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
            else
            {
                //Buscamos sin filtro
                try
                {
                    DB.FillDataTable(dtLocalidades, sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
        }
        #endregion

        #region Comandos

        public static void Insertar(Entidades.Localidad localidad)
        {
            string sql = "INSERT INTO [LOCALIDADES] ([pcia_codigo], [loc_nombre]) VALUES (@p0, @p1) SELECT @@Identity";
            object[] valoresParametros = { localidad.Provincia.Codigo, localidad.Nombre };

            try
            {
                localidad.Codigo = Convert.ToInt32(DB.executeScalar(sql, valoresParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Actualizar(Entidades.Localidad localidad)
        {
            string sql = "UPDATE LOCALIDADES SET pcia_codigo = @p0, loc_nombre = @p1 WHERE loc_codigo = @p2";
            object[] valoresParametros = { localidad.Provincia.Codigo, localidad.Nombre, localidad.Codigo };

            try
            {
                DB.executeNonQuery(sql, valoresParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM LOCALIDADES WHERE loc_codigo = @p0";
            object[] valoresParametros = { codigo };

            try
            {
                DB.executeNonQuery(sql, valoresParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        #endregion

        #region Validaciones

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(loc_codigo) FROM DOMICILIOS WHERE loc_codigo = @p0";
            object[] valoresParametros = { codigo };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valoresParametros, null)) == 0) { return true; }
                else { return false; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool EsLocalidad(Entidades.Localidad localidad)
        {
            string sql = "SELECT count(loc_codigo) FROM LOCALIDADES WHERE loc_nombre = @p0 AND pcia_codigo = @p1";
            object[] valoresParametros = { localidad.Nombre, localidad.Provincia.Codigo };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valoresParametros, null)) == 0) { return false; }
                else { return true; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        #endregion

    }
}
