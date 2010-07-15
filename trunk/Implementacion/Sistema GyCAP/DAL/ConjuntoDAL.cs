using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class ConjuntoDAL
    {
        public static int Insertar(Entidades.Conjunto conjunto)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = @"INSERT INTO [CONJUNTOS]
                        ([conj_nombre]
                        ,[te_codigo]
                        ,[conj_cantidadstock]) 
                        VALUES (@p0,@p1,@p2) SELECT @@Identity";
            object[] valorParametros = { conjunto.Nombre, conjunto.CodigoTerminacion, conjunto.CantidadStock };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigoConjunto)
        {
            string sql = "DELETE FROM CONJUNTOS WHERE conj_codigo = @p0";
            object[] valorParametros = { codigoConjunto };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.Conjunto conjunto)
        {
            string sql = "UPDATE CONJUNTOS SET conj_nombre = @p0, te_codigo = @p1 WHERE conj_codigo = @p2";
            object[] valorParametros = { conjunto.Nombre, conjunto.CodigoTerminacion, conjunto.CodigoConjunto };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ActualizarStock(int codigoConjunto, int cantidad)
        {
            string sql = "UPDATE CONJUNTOS SET conj_cantidadstock = @p0 WHERE conj_codigo = @p1";
            object[] valorParametros = { cantidad, codigoConjunto };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        
        public static bool EsConjunto(Entidades.Conjunto conjunto)
        {
            string sql = "SELECT count(conj_codigo) FROM CONJUNTOS WHERE conj_nombre = @p0";
            object[] valorParametros = { conjunto.Nombre };
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
        /// Obtiene un conjunto por su código.
        /// </summary>
        /// <param name="codigoConjunto">El código del conjunto deseado.</param>
        /// <returns>El objeto conjunto con sus datos.</returns>
        /// <exception cref="ElementoInexistenteException">En caso de que no exista el conjunto.</exception>
        /// <exception cref="BaseDeDatosException">En caso de problemas con la base de datos.</exception>
        public static Entidades.Conjunto ObtenerConjunto(int codigoConjunto)
        {
            string sql = @"SELECT conj_nombre, te_terminacion, conj_cantidadstock
                        FROM CONJUNTOS WHERE conj_codigo = @p0";
            object[] valorParametros = { codigoConjunto };
            SqlDataReader rdr = DB.GetReader(sql, valorParametros, null);
            Entidades.Conjunto conjunto = new GyCAP.Entidades.Conjunto();
            try
            {
                if (!rdr.HasRows) { throw new Entidades.Excepciones.ElementoInexistenteException(); }
                rdr.Read();
                conjunto.CodigoConjunto = codigoConjunto;
                conjunto.Nombre = rdr["conj_nombre"].ToString();
                conjunto.CodigoTerminacion = Convert.ToInt32(rdr["te_codigo"].ToString());
                conjunto.CantidadStock = Convert.ToInt32(rdr["conj_cantidadstock"].ToString());
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            finally
            {
                if (rdr != null) { rdr.Close(); }
                DB.CloseReader();
            }
            return conjunto;
        }
        
        public static void ObtenerConjuntos(Data.dsEstructura ds)
        {
            string sql = "SELECT conj_codigo, conj_nombre, te_codigo, conj_cantidadstock FROM CONJUNTOS";
            try
            {
                DB.FillDataSet(ds, "CONJUNTOS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }            
        }
        
        public static void ObtenerConjuntos(string nombre, Data.dsEstructura ds)
        {
            string sql = @"SELECT conj_codigo, conj_nombre, te_codigo, conj_cantidadstock
                           FROM CONJUNTOS
                           WHERE conj_nombre LIKE @p0";
            //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
            nombre = "%" + nombre + "%";
            object[] valorParametros = { nombre };
            try
            {
                DB.FillDataSet(ds, "CONJUNTOS", sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerConjuntos(int codigoTerminacion, Data.dsEstructura ds)
        {
            string sql = @"SELECT conj_codigo, conj_nombre, te_codigo, conj_cantidadstock
                           FROM CONJUNTOS
                           WHERE te_codigo = @p0";
            //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
            object[] valorParametros = { codigoTerminacion };
            try
            {
                DB.FillDataSet(ds, "CONJUNTOS", sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }           
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(conj_codigo) FROM SUBCONJUNTOSXCONJUNTOS WHERE conj_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0)
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

        ////Obtiene todos los subconjuntos desde la BD por los que está formado el conjunto
        public static void ObtenerEstructura(int codigoConjunto, Data.dsEstructura ds)
        {
            string sql = @"SELECT sxc_codigo, conj_codigo, sconj_codigo, sconj_cantidad
                         FROM SUBCONJUNTOSXCONJUNTOS WHERE conj_codigo = @p0";
            object[] valorParametros = { codigoConjunto };
            try
            {
                //Primero obtenemos la tabla intermedia
                DB.FillDataSet(ds, "SUBCONJUNTOSXCONJUNTOS", sql, valorParametros);
                //Ahora los datos de los subconjuntos que estén en la consulta anterior
                Entidades.SubConjunto subconjunto = new GyCAP.Entidades.SubConjunto();
                foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTOSRow row in ds.SUBCONJUNTOSXCONJUNTOS)
                {
                    //Como ya tenemos todos los códigos de los subconjuntos que necesitamos, directamente
                    //se los pedimos a SubConjuntoDAL
                    subconjunto = DAL.SubConjuntoDAL.ObtenerSubconjunto(codigoConjunto);
                    Data.dsEstructura.SUBCONJUNTOSRow rowSubconjunto = ds.SUBCONJUNTOS.NewSUBCONJUNTOSRow();
                    rowSubconjunto.BeginEdit();
                    rowSubconjunto.SCONJ_CODIGO = subconjunto.CodigoSubconjunto;
                    rowSubconjunto.SCONJ_NOMBRE = subconjunto.Nombre;
                    rowSubconjunto.TE_CODIGO = subconjunto.CodigoTerminacion;
                    rowSubconjunto.SCONJ_CANTIDADSTOCK = subconjunto.CantidadStock;
                    rowSubconjunto.EndEdit();
                    ds.SUBCONJUNTOS.AddSUBCONJUNTOSRow(rowSubconjunto);
                }
                ds.SUBCONJUNTOS.AcceptChanges();
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            catch (Entidades.Excepciones.ElementoInexistenteException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
