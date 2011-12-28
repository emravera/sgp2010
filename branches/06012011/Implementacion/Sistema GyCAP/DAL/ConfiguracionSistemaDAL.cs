using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class ConfiguracionSistemaDAL
    {
        //**************************************************************************************************
        //                              METODOS QUE SE USAN EN LOS OTROS FORMULARIOS
        //**************************************************************************************************

        public static TResult GetConfiguracion<TResult>(string nombre) where TResult: struct
        {
            string sql = "SELECT conf_valor FROM CONFIGURACIONES_SISTEMA WHERE conf_nombre = @p0";

            object[] parametros = { nombre };

            try
            {
                object result = DB.executeScalar(sql, parametros, null);

                if (result == null) { throw new Entidades.Excepciones.ConfiguracionInexistenteException(); }

                return (TResult)Convert.ChangeType(result, typeof(TResult));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void SetConfiguracion(string nombre, string valor)
        {
            int codigo = GetCodigoConfiguracion(nombre);
            string sql = string.Empty;
            object[] parametros = { };

            if (codigo == 0)
            {
                sql = "INSERT INTO CONFIGURACIONES_SISTEMA (conf_nombre, conf_valor) VALUES(@p0, @p1)";
                parametros = new object[] { nombre, valor };
            }
            else
            {
                sql = "UPDATE CONFIGURACIONES_SISTEMA SET conf_valor = @p0 WHERE conf_nombre = @p1";
                parametros = new object[] { valor, nombre };
            }            

            try
            {
                DB.executeNonQuery(sql, parametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        private static int GetCodigoConfiguracion(string nombre)
        {
            string sql = "SELECT conf_codigo FROM CONFIGURACIONES_SISTEMA WHERE conf_nombre = @p0";

            object[] parametros = { nombre };

            try
            {
                object result = DB.executeScalar(sql, parametros, null);

                if (result == null) { return 0; }
                                
                return Convert.ToInt32(result);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        //**************************************************************************************************
        //                              METODOS QUE SE USAN EN EL ABM
        //**************************************************************************************************

        public static void ObtenerTodos(string nombre, string valor, DataTable dtParametros)
        {
            string sql = @"SELECT conf_codigo, conf_nombre, conf_valor
                              FROM CONFIGURACIONES_SISTEMA";

            object[] valorParametros = { null };
            object[] valoresPram = { null, null };

            //Si busca solo por el nombre
            if (nombre != String.Empty && valor != string.Empty)
            {
                //Agrego la busqueda por nombre
                sql = sql + " WHERE conf_nombre LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valorParametros.SetValue(nombre, 0);
            }
            else if (nombre == string.Empty && valor != string.Empty)
            {
                //Agrego la busqueda por valor
                sql = sql + " WHERE conf_valor LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + valor + "%";
                valorParametros.SetValue(valor, 0);
            }
            else if (nombre != string.Empty && valor != string.Empty)
            {
                //Agrego la busqueda por marca
                sql = sql + " WHERE conf_valor LIKE @p0 and conf_nombre LIKE @p1";
                nombre = "%" + nombre + "%";
                valor = "%" + valor + "%";
                
                //Le doy valores a la estructura
                valoresPram.SetValue(valor, 0);
                valoresPram.SetValue(nombre, 1);
            }

            //Ejecuto el comando a la BD
            try
            {
                if (valorParametros.GetValue(0) == null && valoresPram.GetValue(0) == null)
                {
                    //Se ejcuta normalmente y por defecto trae todos los elementos de la DB
                    DB.FillDataTable(dtParametros, sql, null);
                }
                else
                {
                    if (valoresPram.GetValue(0) == null)
                    {
                        DB.FillDataTable(dtParametros, sql, valorParametros);
                    }
                    else
                    {
                        DB.FillDataTable(dtParametros, sql, valoresPram);
                    }
                }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
