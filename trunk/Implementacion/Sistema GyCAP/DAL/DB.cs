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
    public class DB
    {
        private static string conexionGonzaloN = "Data Source=NGA\\SQLEXPRESS;Initial Catalog=Proyecto;Integrated Security=True";
        private static string conexionGonzaloD = "Data Source=DGA\\GONZALO;Initial Catalog=Proyecto;Integrated Security=True";
        private static string conexionEmanuel = "Data Source=HP-EMA\\SQLEXPRESS;Initial Catalog=Proyecto;Integrated Security=True";
        private static string conexionMarcelo = "Data Source=HOMERO;Initial Catalog=Proyecto;User ID=sa";
        private static string conexionRaulD = "Data Source=DESKTOP\\SQLSERVER;Initial Catalog=Proyecto;Integrated Security=True";
        private static string conexionRaulN = "Data Source=NOTEBOOK\\SQLSERVER;Initial Catalog=Proyecto;Integrated Security=True";
        private static string conexionRemota = "Data Source=proyecto.dyndns.org,2555\\Proyecto;Initial Catalog=Proyecto;User ID=sa;Password=spg2010";
        private static string conexionInterna = "Data Source=Proyecto,2555;Initial Catalog=Proyecto;User ID=sa;Password=spg2010";
        private static string cadenaConexion;
        private static SqlTransaction transaccion = null;
        private static SqlCommand cmdReader = null;
        private static int tipoConexion = 0;
        public static readonly int tipoLocal = 0;        
        public static readonly int tipoInterna = 1;
        public static readonly int tipoRemota = 2;

        //Devuelve el nombre de la PC
        static String nombrePC = System.Environment.MachineName;

        //Setea el tipo de conexión
        public static void SetTipoConexion(int tipo)
        {
            tipoConexion = tipo;
        }
        
        //Obtiene la cadena de conexión a la base de datos.
        private static SqlConnection GetConexion()
        {
            switch (tipoConexion)
            {
                case 0: //Local
                    switch (nombrePC)
                    {
                        case "NGA": //notebook - gonzalo
                            cadenaConexion = conexionGonzaloN;
                            break;
                        case "DGA": //desktop - gonzalo
                            cadenaConexion = conexionGonzaloD;
                            break;
                        case "HOMERO": //pc - marcelo
                            cadenaConexion = conexionMarcelo;
                            break;
                        case "DESKTOP": //desktop - raul
                            cadenaConexion = conexionRaulD;
                            break;
                        case "NOTEBOOK": //notebook - raul
                            cadenaConexion = conexionRaulN;
                            break;
                        case "HP-EMA": //notebook - emanuel
                            cadenaConexion = conexionEmanuel;
                            break;
                        default:
                            throw new Entidades.Excepciones.BaseDeDatosException();
                    }
                    break;
                case 1: //Interna
                    cadenaConexion = conexionInterna;
                    break;
                case 2: //Remota
                    cadenaConexion = conexionRemota;
                    break;
                default:
                    throw new Entidades.Excepciones.BaseDeDatosException();
            }
            return new SqlConnection(cadenaConexion);
        }

        //Crea el comando necesario para interactuar con la base de datos.
        private static SqlCommand GetComando(string sql, object[] parametros, SqlTransaction transaccion)
        {
            SqlCommand cmd = new SqlCommand(sql, (SqlConnection)GetConexion());
            cmd.CommandType = System.Data.CommandType.Text;

            if (parametros != null)
            {
                int[] listaValores = { 1 };
                for (int f = 0; f < parametros.Length; f++)
                {
                    if (parametros[f].GetType() == listaValores.GetType())
                    {
                        string newParam = "@pn0";
                        listaValores = (int[])parametros[f];
                        if (listaValores.Length > 0) { cmd.Parameters.AddWithValue("pn0", listaValores[0]); }
                        else { cmd.Parameters.AddWithValue("pn0", -1); } //para mayor seguridad por si vienen vacio el array
                        for (int i = 1; i < listaValores.Length; i++)
                        {
                            newParam += ",@pn" + i;
                            cmd.Parameters.AddWithValue("pn" + i, listaValores[i]);
                        }
                        string buscar = "@p" + f;
                        cmd.CommandText = sql.Replace(buscar, newParam);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("p" + f.ToString(), parametros[f]);
                    }
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
            if (da.SelectCommand.Connection.State == ConnectionState.Open) { da.SelectCommand.Connection.Close(); }
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
            if (da.SelectCommand.Connection.State == ConnectionState.Open) { da.SelectCommand.Connection.Close(); }
        }

        /// <summary>
        /// Ejecuta una consulta sql con parámetros en la base de datos, puede utilizar transacción.
        /// Debe utilizarse cuando el resultado esperado es un sólo dato, por ejemplo un select de un sólo registro 
        /// de una sola columna.
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
            if (transaccion == null) { if (cmd.Connection.State == ConnectionState.Open) { cmd.Connection.Close();} }
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
            if (transaccion == null) { if (cmd.Connection.State == ConnectionState.Open) { cmd.Connection.Close(); } }
            return respuesta;
        }

        /// <summary>
        /// Abre una conexión a la BD e inicia una transacción. Deberá ejecutarse FinalizarTransaccion al
        /// terminar con la misma.
        /// </summary>
        /// <returns>
        /// La transacción ya iniciada.
        /// </returns>
        /// <exception cref="SQLException"></exception>
        public static SqlTransaction IniciarTransaccion()
        {
            SqlConnection cn = (SqlConnection)GetConexion();

            cn.Open();
            transaccion = cn.BeginTransaction();
            return transaccion;
        }

        /// <summary>
        /// Finaliza la última transacción llamada cerrando la conexión a la DB.
        /// </summary>
        public static void FinalizarTransaccion()
        {
            if (transaccion.Connection != null)
            {
                if (transaccion.Connection.State == System.Data.ConnectionState.Open) { transaccion.Connection.Close(); }
            }
        }

        /// <summary>
        /// Ejecuta una consulta sql a la base de datos con parámetros y puede utilizar transacción.
        /// Proporciona una forma de leer una secuencia de filas sólo hacia delante en una base de datos.
        /// Deberá ejecutarse el método CloseReader una vez finalaza la lectura de datos.
        /// </summary>
        /// <param name="sql">La consulta sql.</param>
        /// <param name="valorParametros">Los valos de los parámetros.</param>
        /// <param name="transaccion">La transacción que controlará la consulta, puede indicarse null.</param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql, object[] valorParametros, SqlTransaction transaccion)
        {
            cmdReader = (SqlCommand)GetComando(sql, valorParametros, transaccion);
            return cmdReader.ExecuteReader();
        }

        /// <summary>
        /// Cierra la conexión a la base de datos del último GetReader ejecutado.
        /// </summary>
        public static void CloseReader()
        {
            if (cmdReader.Connection.State == ConnectionState.Open) { cmdReader.Connection.Close(); }
        }

        /// <summary>
        /// Obtiene la fecha del servidor de BD.
        /// </summary>
        /// <returns>Un objeto DateTime.</returns>
        public static DateTime GetFechaServidor()
        {
            string sql = "SELECT GetDate()";
            return DateTime.Parse(executeScalar(sql, null, null).ToString());
        }
    }
}
