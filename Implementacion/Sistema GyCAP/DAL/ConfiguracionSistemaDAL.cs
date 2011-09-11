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
    }
}
