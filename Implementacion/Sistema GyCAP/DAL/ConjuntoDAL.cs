using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class ConjuntoDAL
    {
        public static void Insertar(Data.dsEstructura dsEstructura)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sqlInsertConjunto = @"INSERT INTO [CONJUNTOS]
                                        ([conj_nombre]
                                        ,[te_codigo]
                                        ,[conj_cantidadstock]) 
                                        VALUES (@p0,@p1,@p2) SELECT @@Identity";

            //Así obtenemos el conjunto nuevo del dataset, indicamos la primer fila de las agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.CONJUNTOSRow rowConjunto = dsEstructura.CONJUNTOS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructura.CONJUNTOSRow;
            object[] valorParametros = { rowConjunto.CONJ_NOMBRE, rowConjunto.TE_CODIGO, 0 };

            string sqlInsertEstructura = @"INSERT INTO [SUBCONJUNTOSXCONJUNTOS] 
                                        ([conj_codigo]
                                        ,[sconj_codigo]
                                        ,[sconj_cantidad])
                                        VALUES (@p0, @p1, @p2) SELECT @@Identity";
                        
            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;
            
            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();
                //Insertamos el conjunto y actualizamos su código en el dataset
                rowConjunto.BeginEdit();
                rowConjunto.CONJ_CODIGO = Convert.ToInt32(DB.executeScalar(sqlInsertConjunto, valorParametros, transaccion));
                rowConjunto.EndEdit();
                //Ahora insertamos su estructura, usamos el foreach para recorrer sólo los nuevos registros del dataset
                foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTOSRow row in (Data.dsEstructura.SUBCONJUNTOSXCONJUNTOSRow[])dsEstructura.SUBCONJUNTOSXCONJUNTOS.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    //Primero actualizamos el código del conjunto nuevo en la tabla relacionada
                    row.BeginEdit();
                    row.CONJ_CODIGO = rowConjunto.CONJ_CODIGO;                    
                    row.EndEdit();
                    //Asignamos los valores a los parámetros
                    valorParametros = new object[] { row.CONJ_CODIGO, row.SCONJ_CODIGO, row.SCONJ_CANTIDAD };
                    //Ahora si insertamos en la bd y actualizamos el código de la relación
                    row.BeginEdit();                    
                    row.SXC_CODIGO = Convert.ToDecimal(DB.executeScalar(sqlInsertEstructura, valorParametros, transaccion));
                    row.EndEdit();
                }
                //Todo ok, commit
                transaccion.Commit();                
            }
            catch (SqlException)
            {
                //Error en alguna consulta, descartamos los cambios
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException();
            }
            finally
            {
                //En cualquier caso finalizamos la transaccion para que se cierre la conexion
                DB.FinalizarTransaccion();
            }
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

        public static void Actualizar(Data.dsEstructura dsEstructura)
        {
            //Esto va a ser muy largo...empecemos
            //Primero actualizaremos el conjunto y luego la estructura
            //Armemos todas las consultas
            string sqlUpdateConjunto = "UPDATE CONJUNTOS SET conj_nombre = @p0, te_codigo = @p1 WHERE conj_codigo = @p2";
            
            string sqlInsertEstructura = @"INSERT INTO [SUBCONJUNTOSXCONJUNTOS] 
                                        ([conj_codigo]
                                        ,[sconj_codigo]
                                        ,[sconj_cantidad])
                                        VALUES (@p0, @p1, @p2) SELECT @@Identity";

            string sqlUpdateEstructura = @"UPDATE SUBCONJUNTOSXCONJUNTOS SET sconj_cantidad = @p0 
                                          WHERE sxc_codigo = @p0";

            string sqlDeleteEstructura = "DELETE FROM SUBCONJUNTOSXCONJUNTOS WHERE sxc_codigo = @p0";
            
            //Así obtenemos el conjunto nuevo del dataset, indicamos la primer fila de las modificadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.CONJUNTOSRow rowConjunto = dsEstructura.CONJUNTOS.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsEstructura.CONJUNTOSRow;
            object[] valorParametros = { rowConjunto.CONJ_NOMBRE, rowConjunto.TE_CODIGO, rowConjunto.CONJ_CODIGO };

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;            
            
            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Actualizamos el conjunto
                DB.executeNonQuery(sqlUpdateConjunto, valorParametros, transaccion);
                //Actualizamos la estructura, primero insertamos los nuevos
                foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTOSRow row in (Data.dsEstructura.SUBCONJUNTOSXCONJUNTOSRow[])dsEstructura.SUBCONJUNTOSXCONJUNTOS.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    valorParametros = new object[] { row.CONJ_CODIGO, row.SCONJ_CODIGO, row.SCONJ_CANTIDAD };
                    row.BeginEdit();
                    row.SXC_CODIGO = Convert.ToDecimal(DB.executeScalar(sqlInsertEstructura, valorParametros, transaccion));
                    row.EndEdit();
                }

                //Segundo actualizamos los modificados
                foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTOSRow row in (Data.dsEstructura.SUBCONJUNTOSXCONJUNTOSRow[])dsEstructura.SUBCONJUNTOSXCONJUNTOS.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    valorParametros = new object[] { row.SCONJ_CANTIDAD, row.SXC_CODIGO };
                    DB.executeScalar(sqlUpdateEstructura, valorParametros, transaccion);
                }

                //Tercero eliminamos
                foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTOSRow row in (Data.dsEstructura.SUBCONJUNTOSXCONJUNTOSRow[])dsEstructura.SUBCONJUNTOSXCONJUNTOS.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    valorParametros = new object[] { row["sxc_codigo", System.Data.DataRowVersion.Original] };
                    Convert.ToDecimal(DB.executeScalar(sqlDeleteEstructura, valorParametros, transaccion));
                }

                //Si todo resulto correcto, commit
                transaccion.Commit();

            }
            catch (SqlException)
            {
                //Error en alguna consulta, descartamos los cambios
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException();
            }
            finally
            {
                //En cualquier caso finalizamos la transaccion para que se cierre la conexion
                DB.FinalizarTransaccion();
            }
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
        
        //Determina si existe un conjunto dado su nombre y terminación
        public static bool EsConjunto(Entidades.Conjunto conjunto)
        {
            string sql = "SELECT count(conj_codigo) FROM CONJUNTOS WHERE conj_nombre = @p0 AND te_codigo = @p1";
            object[] valorParametros = { conjunto.Nombre, conjunto.CodigoTerminacion };
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
