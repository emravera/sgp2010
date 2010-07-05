using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Provee los métodos de acceso a una base de datos SQlServer.
/// </summary>

namespace GyCAP.DAL
{
    class DB
    {
        static String conexionGonzalo = "";
        static String conexionEmanuel = "";
        static String conexionMarcelo = "";
        static String conexionRaul = "";
        
        //Cambiar por la propia.
        static String cadenaConexion = conexionGonzalo;

        //Obtiene la cadena de conexión a la base de datos.
        private static SqlConnection GetConexion()
        {
            return new SqlConnection(cadenaConexion);
        }

        //Crea el comando necesario para interactuar con la base de datos.
        private static SqlCommand GetComando(string sql, string[] NombreParametros, object[] ValorParametros, SqlTransaction Transaccion)
        {
            SqlCommand cmd = new SqlCommand(sql, (SqlConnection)GetConexion());
            cmd.CommandType = CommandType.Text;
            if ((NombreParametros != null) && (ValorParametros != null) && (NombreParametros.Length == ValorParametros.Length))
            {
                for (int i = 0; i < NombreParametros.Length; i++)
                {
                    cmd.Parameters.AddWithValue(NombreParametros[i], ValorParametros[i]);
                }
            }
            if (Transaccion != null)
            {
                cmd.Connection = (SqlConnection)Transaccion.Connection;
                cmd.Transaction = (SqlTransaction)Transaccion;
            }
            else
            {
                cmd.Connection.Open();
            }

            return cmd;
        }

        /// <summary>
        /// Carga los datos de una consulta sql sencilla en una tabla de un DataSet.
        /// </summary>
        /// <param name="DS">La tabla del DataSet donde se cargarán los datos de la consulta, debe tener la misma
        /// estructura que la tabla en la base de datos.</param>
        /// <param name="nombreTabla">El nombre de la tabla a llenar en el DataSet.</param>
        /// <param name="sql">La consulta sql.</param>
        /// <exception cref="SQLException">En caso de no poder conectarse a la base de datos.</exception>
        public static void FillDataSet(DataSet DS, string nombreTabla, string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, (SqlConnection)GetConexion());
            da.Fill(DS, nombreTabla);
        }

        /// <summary>
        /// Carga los datos de una consulta sql con parámetros en una tabla de un DataSet.
        /// </summary>
        /// <param name="DS">La tabla del DataSet donde se cargarán los datos de la consulta, debe tener la misma
        /// estructura que la tabla en la base de datos.</param>
        /// <param name="nombreTabla">El nombre de la tabla a llenar en el DataSet.</param>
        /// <param name="sql">La consulta sql.</param>
        /// <param name="nombreParametros">Un array string con los nombres de los parámetros usados en la consulta sql.</param>
        /// <param name="valorParametros">Un array de objetos con los datos a insertar en los parámetros.</param>
        /// <exception cref="SQLException">En caso de inconvenientes con la base de datos.</exception>
        public static void FillDataSet(DataSet DS, string nombreTabla, string sql, string[] nombreParametros, object[] valorParametros)
        {
            SqlCommand cmd = (SqlCommand)GetComando(sql, nombreParametros, valorParametros, null);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(DS, nombreTabla);
        }

        /// <summary>
        /// Carga los datos de una consulta sql sencilla en una tabla del tipo DataTable.
        /// </summary>
        /// <param name="DT">La tabla donde se cargrán los datos.</param>
        /// <param name="sql">La consulta sql.</param>
        public static void FillDataTable(DataTable DT, string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, (SqlConnection)GetConexion());
            da.Fill(DT);
        }

        /// <summary>
        /// Ejecuta una consulta sql con parámetros en la base de datos, puede utilizar transacción.
        /// Debe utilizarse cuando el resultado devuelve un sólo dato, tippicamente un select de un sólo registro.
        /// </summary>
        /// <param name="sql">La consulta sql.</param>
        /// <param name="NombreParametros">Un array string con los nombres de los parámetros de la consulta.</param>
        /// <param name="ValorParametros">Un array de objetos con los valos a insertar en los prámetros de la consulta.</param>
        /// <param name="Transaccion">El objeto transacción que va a controlar la ejecución de la consulta. En caso de no querer
        /// utilizar transacción colocar null.</param>
        /// <returns>Un objeto con la respuesta de la consulta, deberá convertirse al tipo de dato correspondiente.</returns>
        public static object ExecuteScalar(string sql, string[] NombreParametros, object[] ValorParametros, SqlTransaction Transaccion)
        {
            SqlCommand cmd = (SqlCommand)GetComando(sql, NombreParametros, ValorParametros, Transaccion);
            object query = cmd.ExecuteScalar();
            return query;
        }

        /// <summary>
        /// Ejecuta una consulta sql con parámetros en la base de datos, puede utilizar transacción.
        /// Debe utilizarse cuando no se espera que devuelva datos, tipicamente insert, update o delete.
        /// Devuelve un int con la cantidad de registros afectados por la consulta.
        /// </summary>
        /// <param name="sql">La consulta sql.</param>
        /// <param name="NombreParametros">Un array string con los nombres de los parámetros de la consulta.</param>
        /// <param name="ValorParametros">Un array de objetos con los valores de los parámetros.</param>
        /// <param name="Transaccion">El objeto transacción que va a controlar la ejecución de la consulta. En caso de no querer
        /// utilizar transacción colocar null.</param>
        /// <returns>Un int con la cantidad de registros afectados por la consulta.</returns>
        public static int ExecuteNonQuery(string sql, string[] NombreParametros, object[] ValorParametros, SqlTransaction Transaccion)
        {
            SqlCommand cmd = (SqlCommand)GetComando(sql, NombreParametros, ValorParametros, Transaccion);
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Abre una conexión a la BD e inicia una transacción.
        /// </summary>
        /// <returns>
        /// La transacción ya iniciada.
        /// </returns>
        /// <exception cref="SQLException"></exception>
        public static SqlTransaction IniciarTransaccion()
        {
            SqlConnection cn = (SqlConnection)GetConexion();

            cn.Open();
            return cn.BeginTransaction();
        }

        /// <summary>
        /// Ejecuta una consulta sql a la base de datos con parámetros y puede utilizar transacción.
        /// Proporciona una forma de leer una secuencia de filas sólo hacia delante en una base de datos.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="NombreParametros"></param>
        /// <param name="ValorParametros"></param>
        /// <param name="Transaccion"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql, string[] NombreParametros, object[] ValorParametros, SqlTransaction Transaccion)
        {
            SqlCommand cmd = (SqlCommand)GetComando(sql, NombreParametros, ValorParametros, Transaccion);
            return cmd.ExecuteReader();
        }
    }
}
