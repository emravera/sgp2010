using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class MateriaPrimaDAL
    {
        /// <summary>
        /// Obtiene una materia prima por su código.
        /// </summary>
        /// <param name="codigoMateriaPrima">El código de la materia prima deseada.</param>
        /// <returns>El objeto MateriaPrima con sus datos.</returns>
        /// <exception cref="ElementoInexistenteException">En caso de que no exista la materia prima.</exception>
        /// <exception cref="BaseDeDatosException">En caso de problemas con la base de datos.</exception>
        public static Entidades.MateriaPrima ObtenerMateriaPrima(int codigoMateriaPrima)
        {
            string sql = @"SELECT mp_nombre, umed_codigo, mp_descripcion, mp_cantidadstock, mp_precio
                        FROM MATERIAS_PRIMAS WHERE pza_codigo = @p0";
            object[] valorParametros = { codigoMateriaPrima };
            SqlDataReader rdr = DB.GetReader(sql, valorParametros, null);
            Entidades.MateriaPrima materiaPrima = new GyCAP.Entidades.MateriaPrima();
            try
            {
                if (!rdr.HasRows) { throw new Entidades.Excepciones.ElementoInexistenteException(); }
                rdr.Read();
                materiaPrima.CodigoMateriaPrima = codigoMateriaPrima;
                materiaPrima.Nombre = rdr["mp_nombre"].ToString();
                materiaPrima.CodigoUnidadMedida = Convert.ToInt32(rdr["umed_codigo"].ToString());
                materiaPrima.Descripcion = rdr["mp_descripcion"].ToString();
                materiaPrima.CantidadStock = Convert.ToInt32(rdr["mp_cantidadstock"].ToString());
                materiaPrima.Precio = Convert.ToDecimal(rdr["mp_precio"].ToString());
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            finally
            {
                if (rdr != null) { rdr.Close(); }
                DB.CloseReader();
            }
            return materiaPrima;
        }
        
        //Metodo que obtiene todas las materias primas
        public static void ObtenerTodos(Data.dsMateriaPrima ds)
        {
            string sql = @"SELECT mp_codigo, mp_nombre, umed_codigo, mp_descripcion, mp_cantidadstock, mp_precio
                        FROM MATERIAS_PRIMAS";
            try
            {
                DB.FillDataSet(ds, "MATERIAS_PRIMAS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        public static void ObtenerTodos(System.Data.DataTable dt)
        {
            string sql = @"SELECT mp_codigo, mp_nombre, umed_codigo, mp_descripcion, mp_cantidadstock, mp_precio
                        FROM MATERIAS_PRIMAS";
            try
            {
                DB.FillDataTable(dt, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

    }
}
