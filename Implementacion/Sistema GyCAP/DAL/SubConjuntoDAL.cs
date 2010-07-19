using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class SubConjuntoDAL
    {
        public static void Insertar(Data.dsEstructura dsEstructura)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sqlInsertSubconjunto = @"INSERT INTO [SUBCONJUNTOS]
                                        ([sconj_nombre]
                                        ,[te_codigo]
                                        ,[sconj_descripcion]
                                        ,[sconj_cantidadstock]) 
                                        VALUES (@p0,@p1,@p2,@p3) SELECT @@Identity";

            //Así obtenemos el subconjunto nuevo del dataset, indicamos la primer fila de las agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.SUBCONJUNTOSRow rowSubconjunto = dsEstructura.SUBCONJUNTOS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructura.SUBCONJUNTOSRow;
            object[] valorParametros = { rowSubconjunto.SCONJ_CODIGO, rowSubconjunto.TE_CODIGO, rowSubconjunto.SCONJ_DESCRIPCION,0 };

            string sqlInsertEstructura = @"INSERT INTO [DETALLE_SUBCONJUNTO] 
                                        ([sconj_codigo]
                                        ,[pza_codigo]
                                        ,[dsc_cantidad])
                                        VALUES (@p0, @p1, @p2) SELECT @@Identity";

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();
                //Insertamos el subconjunto y actualizamos su código en el dataset
                rowSubconjunto.BeginEdit();
                rowSubconjunto.SCONJ_CODIGO = Convert.ToInt32(DB.executeScalar(sqlInsertSubconjunto, valorParametros, transaccion));
                rowSubconjunto.EndEdit();
                //Ahora insertamos su estructura, usamos el foreach para recorrer sólo los nuevos registros del dataset
                foreach (Data.dsEstructura.DETALLE_SUBCONJUNTORow row in (Data.dsEstructura.DETALLE_SUBCONJUNTORow[])dsEstructura.DETALLE_SUBCONJUNTO.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    //Primero actualizamos el código de el subconjunto nuevo en la tabla relacionada
                    row.BeginEdit();
                    row.SCONJ_CODIGO = rowSubconjunto.SCONJ_CODIGO;
                    row.EndEdit();
                    //Asignamos los valores a los parámetros
                    valorParametros = new object[] { row.SCONJ_CODIGO, row.PZA_CODIGO, row.DSC_CANTIDAD };
                    //Ahora si insertamos en la bd y actualizamos el código de la relación
                    row.BeginEdit();
                    row.DSC_CODIGO = Convert.ToDecimal(DB.executeScalar(sqlInsertEstructura, valorParametros, transaccion));
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

        public static void Eliminar(int codigoSubconjunto)
        {
            string sql = "DELETE FROM SUBCONJUNTOS WHERE sconj_codigo = @p0";
            object[] valorParametros = { codigoSubconjunto };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Data.dsEstructura dsEstructura)
        {
            //Esto va a ser muy largo...empecemos
            //Primero actualizaremos el subconjunto y luego la estructura
            //Armemos todas las consultas
            string sqlUpdateSubconjunto = @"UPDATE SUBCONJUNTOS SET
                                            sconj_nombre = @p0
                                           ,te_codigo = @p1
                                           ,sconj_descripcion = @p2
                                            WHERE sconj_codigo = @p3";

            string sqlInsertEstructura = @"INSERT INTO [DETALLE_SUBCONJUNTO] 
                                        ([sconj_codigo]
                                        ,[pza_codigo]
                                        ,[dsc_cantidad])
                                        VALUES (@p0, @p1, @p2) SELECT @@Identity";

            string sqlUpdateEstructura = @"UPDATE DETALLE_SUBCONJUNTO SET dsc_cantidad = @p0 
                                          WHERE dsc_codigo = @p0";

            string sqlDeleteEstructura = "DELETE FROM DETALLE_SUBCONJUNTO WHERE dsc_codigo = @p0";

            //Así obtenemos el subconjunto del dataset, indicamos la primer fila de las modificadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.SUBCONJUNTOSRow rowSubconjunto = dsEstructura.SUBCONJUNTOS.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsEstructura.SUBCONJUNTOSRow;
            object[] valorParametros = { rowSubconjunto.SCONJ_NOMBRE, rowSubconjunto.TE_CODIGO, rowSubconjunto.SCONJ_DESCRIPCION, rowSubconjunto.SCONJ_CODIGO };

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Actualizamos el subconjunto
                DB.executeNonQuery(sqlUpdateSubconjunto, valorParametros, transaccion);
                //Actualizamos la estructura, primero insertamos los nuevos
                foreach (Data.dsEstructura.DETALLE_SUBCONJUNTORow row in (Data.dsEstructura.DETALLE_SUBCONJUNTORow[])dsEstructura.DETALLE_SUBCONJUNTO.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    valorParametros = new object[] { row.SCONJ_CODIGO, row.PZA_CODIGO, row.DSC_CANTIDAD };
                    row.BeginEdit();
                    row.DSC_CODIGO = Convert.ToDecimal(DB.executeScalar(sqlInsertEstructura, valorParametros, transaccion));
                    row.EndEdit();
                }

                //Segundo actualizamos los modificados
                foreach (Data.dsEstructura.DETALLE_SUBCONJUNTORow row in (Data.dsEstructura.DETALLE_SUBCONJUNTORow[])dsEstructura.DETALLE_SUBCONJUNTO.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    valorParametros = new object[] { row.DSC_CANTIDAD, row.DSC_CODIGO };
                    DB.executeScalar(sqlUpdateEstructura, valorParametros, transaccion);
                }

                //Tercero eliminamos
                foreach (Data.dsEstructura.DETALLE_SUBCONJUNTORow row in (Data.dsEstructura.DETALLE_SUBCONJUNTORow[])dsEstructura.DETALLE_SUBCONJUNTO.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    valorParametros = new object[] { row["dsc_codigo", System.Data.DataRowVersion.Original] };
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

        public static void ActualizarStock(int codigoSubconjunto, int cantidad)
        {
            string sql = "UPDATE SUBCONJUNTOS SET sconj_cantidadstock = @p0 WHERE sconj_codigo = @p1";
            object[] valorParametros = { cantidad, codigoSubconjunto };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerSubconjuntos(object nombre, object terminacion, Data.dsEstructura ds)
        {
            string sql = "SELECT sconj_codigo, sconj_nombre, te_codigo, sconj_descripcion, sconj_cantidadstock FROM SUBCONJUNTOS ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[2];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //Como es el primero no revisamos si está el WHERE y si aplica el filtro lo usamos
                sql += "WHERE sconj_nombre LIKE @p" + cantidadParametros + " ";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }
            //Revisamos si pasó algun valor y si es un integer
            if (terminacion != null && terminacion.GetType() == cantidadParametros.GetType())
            {
                //Como es el segundo filtro si tiene que revisar si el anterior también aplica, busca si está el WHERE,
                //si está agrega un AND, caso contrario el WHERE
                if (sql.Contains("WHERE")) { sql += "AND te_codigo = @p" + cantidadParametros; }
                else { sql += "WHERE te_codigo = @p" + cantidadParametros; }
                valoresFiltros[cantidadParametros] = Convert.ToInt32(terminacion);
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
                DB.FillDataSet(ds, "SUBCONJUNTOS", sql, valorParametros);
            }
            else
            {
                //Buscamos sin filtro
                DB.FillDataSet(ds, "SUBCONJUNTOS", sql, null);
            }
        }

        /*public static void ObtenerSubconjuntos(Data.dsEstructura ds)
        {
            string sql = "SELECT sconj_codigo, sconj_nombre, te_codigo, sconj_cantidadstock FROM SUBCONJUNTOS";
            try
            {
                DB.FillDataSet(ds, "SUBCONJUNTOS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerSubconjuntos(string nombre, Data.dsEstructura ds)
        {
            string sql = @"SELECT sconj_codigo, sconj_nombre, te_codigo, sconj_cantidadstock
                           FROM SUBCONJUNTOS
                           WHERE sconj_nombre LIKE @p0";
            //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
            nombre = "%" + nombre + "%";
            object[] valorParametros = { nombre };
            try
            {
                DB.FillDataSet(ds, "SUBCONJUNTOS", sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerSubconjuntos(int codigoTerminacion, Data.dsEstructura ds)
        {
            string sql = @"SELECT sconj_codigo, sconj_nombre, te_codigo, sconj_cantidadstock
                           FROM SUBCONJUNTOS
                           WHERE te_codigo = @p0";

            object[] valorParametros = { codigoTerminacion };
            try
            {
                DB.FillDataSet(ds, "SUBCONJUNTOS", sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }*/

        public static bool PuedeEliminarse(int codigo)
        {
            string sqlDSCC = "SELECT count(sconj_codigo) FROM DETALLE_SUBCONJUNTO WHERE sconj_codigo = @p0";
            string sqlSCXC = "SELECT count(sconj_codigo) FROM DETALLE_CONJUNTO WHERE sconj_codigo = @p0";

            object[] valorParametros = { codigo };
            try
            {
                int resultadoDSCC = Convert.ToInt32(DB.executeScalar(sqlDSCC, valorParametros, null));
                int resultadoSCXC = Convert.ToInt32(DB.executeScalar(sqlSCXC, valorParametros, null));
                if (resultadoDSCC == 0 && resultadoSCXC == 0)
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

        ////Obtiene todas las materias primas desde la BD por los que está formada el subconjunto
        public static void ObtenerEstructura(int codigoSubconjunto, Data.dsEstructura ds)
        {
            string sql = @"SELECT dsc_codigo, sconj_codigo, pza_codigo, dsc_cantidad
                         FROM DETALLE_SUBCONJUNTO WHERE sconj_codigo = @p0";

            object[] valorParametros = { codigoSubconjunto };
            try
            {
                //Primero obtenemos la tabla intermedia
                DB.FillDataSet(ds, "DETALLE_SUBCONJUNTO", sql, valorParametros);
                //Ahora los datos de las piezas que estén en la consulta anterior
                Entidades.Pieza pieza = new GyCAP.Entidades.Pieza();
                foreach (Data.dsEstructura.DETALLE_SUBCONJUNTORow row in ds.DETALLE_SUBCONJUNTO)
                {
                    //Como ya tenemos todos los códigos de las materias primas que necesitamos, directamente
                    //se los pedimos a MateriaPrimaDAL
                    pieza = DAL.PiezaDAL.ObtenerPieza(Convert.ToInt32(row.PZA_CODIGO));
                    Data.dsEstructura.PIEZASRow rowPieza = ds.PIEZAS.NewPIEZASRow();
                    rowPieza.BeginEdit();
                    rowPieza.PZA_CODIGO = pieza.CodigoPieza;
                    rowPieza.PZA_NOMBRE = pieza.Nombre;
                    rowPieza.TE_CODIGO = pieza.CodigoTerminacion;
                    rowPieza.EndEdit();
                    ds.PIEZAS.AddPIEZASRow(rowPieza);
                }
                ds.PIEZAS.AcceptChanges();
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            catch (Entidades.Excepciones.ElementoInexistenteException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        
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
            string sql = @"SELECT sconj_nombre, te_terminacion, sconj_descripcion, sconj_cantidadstock
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
                subconjunto.Descripcion = rdr["sconj_descripcion"].ToString();
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
