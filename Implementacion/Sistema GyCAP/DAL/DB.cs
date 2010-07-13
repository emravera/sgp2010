using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration ;


/// <summary>
/// Provee los métodos de acceso a una base de datos SQlServer.
/// </summary>

namespace GyCAP.DAL
{
    class DB
    {
        static string conexionGonzalo = "Data Source=NGA\\SQLEXPRESS;Initial Catalog=Proyecto;Integrated Security=True";
        static string conexionEmanuel = "";
        static string conexionMarcelo = "Data Source=HOMERO;Initial Catalog=Proyecto;User ID=sa";
        static string conexionRaul = "Data Source=DESKTOP\\SQLSERVER;Initial Catalog=Proyecto;Integrated Security=True";

        //Devuelve el nombre de la PC
        static String nombrePC = System.Environment.MachineName;

        static string cadenaConexion = conexionGonzalo;

        //Obtiene la cadena de conexión a la base de datos.
        private static SqlConnection GetConexion()
        {
            if (nombrePC == "HOMERO") //PC - Marcelo
            {
                return new SqlConnection(conexionMarcelo);
            }
            else
            {
                return new SqlConnection(cadenaConexion);
            } 
        }

        //Crea el comando necesario para interactuar con la base de datos.
        private static SqlCommand GetComando(string sql, object[] parametros, SqlTransaction transaccion)
        {
            SqlCommand cmd = new SqlCommand(sql, (SqlConnection)GetConexion());
            cmd.CommandType = System.Data.CommandType.Text;

            if (parametros != null)
            {
                for (int f = 0; f < parametros.Length; f++)
                {
                    cmd.Parameters.AddWithValue("p" + f.ToString(), parametros[f]);
                }
            }

            if (transaccion != null)
            {
                cmd.Connection = (SqlConnection)transaccion.Connection;
                cmd.Transaction = (SqlTransaction)transaccion;
            }
            else
            {
                cmd.Connection.Open();
            }

            return cmd;
        }

        /// <summary>
        /// Carga los datos de una consulta sql en una tabla de un DataSet, si no se utiliza una consulta con
        /// parámetros pasar como valor null.
        /// </summary>
        /// <param name="ds">El DataSet donde se cargarán los datos de la consulta.</param>
        /// <param name="nombreTabla">El nombre de la tabla a llenar en el DataSet.</param>
        /// <param name="sql">La consulta sql.</param>
        /// <param name="valorParametros">Los valores de los parámetros, poner null si no se utilizan parámetros.</param>
        /// <exception cref="SQLException">En caso de no poder conectarse a la base de datos.</exception>
        public static void FillDataSet(DataSet ds, string nombreTabla, string sql, object[] valorParametros)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = (SqlCommand)GetComando(sql, valorParametros, null);
            da.Fill(ds, nombreTabla);
            da.SelectCommand.Connection.Close();
        }
        
        /// <summary>
        /// Carga los datos de una consulta sql en una tabla del tipo DataTable. Puede utilizar consultas
        /// con parámetros, si no se desea pasar como valor null.
        /// </summary>
        /// <param name="dt">La tabla donde se cargrán los datos.</param>
        /// <param name="sql">La consulta sql.</param>
        /// <param name="valorParametros">Los valores de los parámetros.</param>
        public static void FillDataTable(DataTable dt, string sql, object[] valorParametros)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = (SqlCommand)GetComando(sql, valorParametros, null);
            da.Fill(dt);
            da.SelectCommand.Connection.Close();
        }

        /// <summary>
        /// Ejecuta una consulta sql con parámetros en la base de datos, puede utilizar transacción.
        /// Debe utilizarse cuando el resultado devuelve un sólo dato, tipicamente un select de un sólo registro.
        /// Pasar valor null en caso de no utilizar parámetros en la consulta y también null si no se usa transacción.
        /// </summary>
        /// <param name="sql">La consulta sql.</param>
        /// <param name="valorParametros">Un array de objetos con los valos a insertar en los prámetros de la consulta.</param>
        /// <param name="transaccion">El objeto transacción que va a controlar la ejecución de la consulta. En caso de no querer
        /// utilizarla colocar null.</param>
        /// <returns>Un objeto con la respuesta de la consulta, deberá convertirse al tipo de dato correspondiente.</returns>
        public static object executeScalar(string sql, object[] valorParametros, SqlTransaction transaccion)
        {
            SqlCommand cmd = (SqlCommand)GetComando(sql, valorParametros, transaccion);
            object query = cmd.ExecuteScalar();
            if (transaccion == null) { cmd.Connection.Close(); }
            return query;            
        }

        /// <summary>
        /// Ejecuta una consulta sql con parámetros en la base de datos, puede utilizar transacción.
        /// Debe utilizarse cuando no se espera que devuelva datos, tipicamente insert, update o delete.
        /// Devuelve un int con la cantidad de registros afectados por la consulta.
        /// </summary>
        /// <param name="sql">La consulta sql.</param>
        /// <param name="valorParametros">Un array de objetos con los valores de los parámetros.</param>
        /// <param name="transaccion">El objeto transacción que va a controlar la ejecución de la consulta. En caso de no querer
        /// utilizar transacción colocar null.</param>
        /// <returns>Un int con la cantidad de registros afectados por la consulta.</returns>
        public static int executeNonQuery(string sql, object[] valorParametros, SqlTransaction transaccion)
        {
            SqlCommand cmd = (SqlCommand)GetComando(sql, valorParametros, transaccion);
            int respuesta = cmd.ExecuteNonQuery();
            if (transaccion == null) { cmd.Connection.Close(); }
            return respuesta;
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
        /// <param name="valorParametros"></param>
        /// <param name="transaccion"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql, object[] valorParametros, SqlTransaction transaccion)
        {
            SqlCommand cmd = (SqlCommand)GetComando(sql, valorParametros, transaccion);
            return cmd.ExecuteReader();
        }
    }
}
