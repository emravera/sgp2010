using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class SubConjuntoDAL
    {
        public static bool EsSubConjunto(Entidades.SubConjunto subConjunto)
        {
            string sql = "SELECT count(sconj_codigo) FROM SUBCONJUNTOS WHERE sconj_nombre = @p0";
            object[] valorParametros = { subConjunto.Nombre };
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

        /// <summary>
        /// Obtiene un subconjunto por su código.
        /// </summary>
        /// <param name="codigoSubconjunto">El código del subconjunto deseado.</param>
        /// <returns>El objeto subconjunto con sus datos.</returns>
        /// <exception cref="ElementoInexistenteException">En caso de que no exista el subconjunto.</exception>
        /// <exception cref="BaseDeDatosException">En caso de problemas con la base de datos.</exception>
        public static Entidades.SubConjunto ObtenerSubconjunto(int codigoSubconjunto)
        {
            string sql = @"SELECT sconj_nombre, te_terminacion, sconj_cantidadstock
                        FROM SUBCONJUNTOS WHERE sconj_codigo = @p0";
            object[] valorParametros = { codigoSubconjunto };
            SqlDataReader rdr = DB.GetReader(sql, valorParametros, null);
            Entidades.SubConjunto subconjunto = new GyCAP.Entidades.SubConjunto();
            try
            {
                if (!rdr.HasRows) { throw new Entidades.Excepciones.ElementoInexistenteException(); }                
                rdr.Read();
                subconjunto.CodigoSubconjunto = codigoSubconjunto;
                subconjunto.Nombre = rdr["sconj_nombre"].ToString();
                subconjunto.CodigoTerminacion = Convert.ToInt32(rdr["te_codigo"].ToString());
                subconjunto.CantidadStock = Convert.ToInt32(rdr["sconj_cantidadstock"].ToString());
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            finally
            {
                if (rdr != null) { rdr.Close(); }
                DB.CloseReader();                
            }
            return subconjunto;
        }
    }
}
