using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class PiezaDAL
    {
        public static void Insertar(Data.dsEstructura dsEstructura)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sqlInsertPieza = @"INSERT INTO [PIEZAS]
                                        ([pza_nombre]
                                        ,[te_codigo]
                                        ,[pza_cantidadstock]) 
                                        VALUES (@p0,@p1,@p2) SELECT @@Identity";

            //Así obtenemos la pieza nueva del dataset, indicamos la primer fila de las agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.PIEZASRow rowPieza = dsEstructura.PIEZAS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructura.PIEZASRow;
            object[] valorParametros = { rowPieza.PZA_CODIGO, rowPieza.TE_CODIGO, 0 };

            string sqlInsertEstructura = @"INSERT INTO [MATERIASPRIMASXPIEZA] 
                                        ([pza_codigo]
                                        ,[mp_codigo]
                                        ,[mp_cantidad])
                                        VALUES (@p0, @p1, @p2) SELECT @@Identity";

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();
                //Insertamos la pieza y actualizamos su código en el dataset
                rowPieza.BeginEdit();
                rowPieza.PZA_CODIGO = Convert.ToInt32(DB.executeScalar(sqlInsertPieza, valorParametros, transaccion));
                rowPieza.EndEdit();
                //Ahora insertamos su estructura, usamos el foreach para recorrer sólo los nuevos registros del dataset
                foreach (Data.dsEstructura.MATERIASPRIMASXPIEZARow row in (Data.dsEstructura.MATERIASPRIMASXPIEZARow[])dsEstructura.MATERIASPRIMASXPIEZA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    //Primero actualizamos el código de la pieza nueva en la tabla relacionada
                    row.BeginEdit();
                    row.PZA_CODIGO = rowPieza.PZA_CODIGO;
                    row.EndEdit();
                    //Asignamos los valores a los parámetros
                    valorParametros = new object[] { row.PZA_CODIGO, row.MP_CODIGO, row.MP_CANTIDAD };
                    //Ahora si insertamos en la bd y actualizamos el código de la relación
                    row.BeginEdit();
                    row.MPXP_CODIGO = Convert.ToDecimal(DB.executeScalar(sqlInsertEstructura, valorParametros, transaccion));
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

        public static void Eliminar(int codigoPieza)
        {
            string sql = "DELETE FROM PIEZAS WHERE pza_codigo = @p0";
            object[] valorParametros = { codigoPieza };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Data.dsEstructura dsEstructura)
        {
            //Esto va a ser muy largo...empecemos
            //Primero actualizaremos la pieza y luego la estructura
            //Armemos todas las consultas
            string sqlUpdatePieza = "UPDATE PIEZAS SET pza_nombre = @p0, te_codigo = @p1 WHERE pza_codigo = @p2";

            string sqlInsertEstructura = @"INSERT INTO [MATERIASPRIMASXPIEZA] 
                                        ([pza_codigo]
                                        ,[mp_codigo]
                                        ,[mp_cantidad])
                                        VALUES (@p0, @p1, @p2) SELECT @@Identity";

            string sqlUpdateEstructura = @"UPDATE MATERIASPRIMASXPIEZA SET mp_cantidad = @p0 
                                          WHERE mpxp_codigo = @p0";

            string sqlDeleteEstructura = "DELETE FROM MATERIASPRIMASXPIEZA WHERE mpxp_codigo = @p0";

            //Así obtenemos la pieza del dataset, indicamos la primer fila de las modificadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.PIEZASRow rowPieza = dsEstructura.PIEZAS.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsEstructura.PIEZASRow;
            object[] valorParametros = { rowPieza.PZA_NOMBRE, rowPieza.TE_CODIGO, rowPieza.PZA_CODIGO };

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Actualizamos la pieza
                DB.executeNonQuery(sqlUpdatePieza, valorParametros, transaccion);
                //Actualizamos la estructura, primero insertamos los nuevos
                foreach (Data.dsEstructura.MATERIASPRIMASXPIEZARow row in (Data.dsEstructura.MATERIASPRIMASXPIEZARow[])dsEstructura.MATERIASPRIMASXPIEZA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    valorParametros = new object[] { row.PZA_CODIGO, row.MP_CODIGO, row.MP_CANTIDAD };
                    row.BeginEdit();
                    row.MPXP_CODIGO = Convert.ToDecimal(DB.executeScalar(sqlInsertEstructura, valorParametros, transaccion));
                    row.EndEdit();
                }

                //Segundo actualizamos los modificados
                foreach (Data.dsEstructura.MATERIASPRIMASXPIEZARow row in (Data.dsEstructura.MATERIASPRIMASXPIEZARow[])dsEstructura.MATERIASPRIMASXPIEZA.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    valorParametros = new object[] { row.MP_CANTIDAD, row.MPXP_CODIGO };
                    DB.executeScalar(sqlUpdateEstructura, valorParametros, transaccion);
                }

                //Tercero eliminamos
                foreach (Data.dsEstructura.MATERIASPRIMASXPIEZARow row in (Data.dsEstructura.MATERIASPRIMASXPIEZARow[])dsEstructura.MATERIASPRIMASXPIEZA.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    valorParametros = new object[] { row["mpxp_codigo", System.Data.DataRowVersion.Original] };
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

        public static void ActualizarStock(int codigoPieza, int cantidad)
        {
            string sql = "UPDATE PIEZAS SET pza_cantidadstock = @p0 WHERE pza_codigo = @p1";
            object[] valorParametros = { cantidad, codigoPieza };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Determina si existe una pieza dado su nombre y terminación
        public static bool EsPieza(Entidades.Pieza pieza)
        {
            string sql = "SELECT count(pza_codigo) FROM PIEZAS WHERE pza_nombre = @p0 AND te_codigo = @p1";
            object[] valorParametros = { pieza.Nombre, pieza.CodigoTerminacion };
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
        /// Obtiene una pieza por su código.
        /// </summary>
        /// <param name="codigoConjunto">El código de la pieza deseada.</param>
        /// <returns>El objeto pieza con sus datos.</returns>
        /// <exception cref="ElementoInexistenteException">En caso de que no exista la pieza.</exception>
        /// <exception cref="BaseDeDatosException">En caso de problemas con la base de datos.</exception>
        public static Entidades.Pieza ObtenerPieza(int codigoPieza)
        {
            string sql = @"SELECT pza_nombre, te_terminacion, pza_cantidadstock
                        FROM PIEZAS WHERE pza_codigo = @p0";
            object[] valorParametros = { codigoPieza };
            SqlDataReader rdr = DB.GetReader(sql, valorParametros, null);
            Entidades.Pieza pieza = new GyCAP.Entidades.Pieza();
            try
            {
                if (!rdr.HasRows) { throw new Entidades.Excepciones.ElementoInexistenteException(); }
                rdr.Read();
                pieza.CodigoPieza = codigoPieza;
                pieza.Nombre = rdr["pza_nombre"].ToString();
                pieza.CodigoTerminacion = Convert.ToInt32(rdr["te_codigo"].ToString());
                pieza.CantidadStock = Convert.ToInt32(rdr["pza_cantidadstock"].ToString());
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            finally
            {
                if (rdr != null) { rdr.Close(); }
                DB.CloseReader();
            }
            return pieza;
        }

        public static void ObtenerPiezas(Data.dsEstructura ds)
        {
            string sql = "SELECT pza_codigo, pza_nombre, te_codigo, pza_cantidadstock FROM PIEZAS";
            try
            {
                DB.FillDataSet(ds, "PIEZAS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerPiezas(string nombre, Data.dsEstructura ds)
        {
            string sql = @"SELECT pza_codigo, pza_nombre, te_codigo, pza_cantidadstock
                           FROM PIEZAS
                           WHERE pza_nombre LIKE @p0";
            //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
            nombre = "%" + nombre + "%";
            object[] valorParametros = { nombre };
            try
            {
                DB.FillDataSet(ds, "PIEZAS", sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerPiezas(int codigoTerminacion, Data.dsEstructura ds)
        {
            string sql = @"SELECT pza_codigo, pza_nombre, te_codigo, pza_cantidadstock
                           FROM PIEZAS
                           WHERE te_codigo = @p0";
            
            object[] valorParametros = { codigoTerminacion };
            try
            {
                DB.FillDataSet(ds, "PIEZAS", sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sqlMPXP = "SELECT count(pza_codigo) FROM MATERIASPRIMASXPIEZA WHERE pza_codigo = @p0";
            string sqlPXSC = "SELECT count(pza_codigo) FROM PIEZASXSUBCONJUNTO WHERE pza_codigo = @p0";

            object[] valorParametros = { codigo };
            try
            {
                int resultadoMPXP = Convert.ToInt32(DB.executeScalar(sqlMPXP, valorParametros, null));
                int resultadoPXSC = Convert.ToInt32(DB.executeScalar(sqlPXSC, valorParametros, null));
                if ( resultadoMPXP == 0 && resultadoPXSC == 0)
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

        ////Obtiene todas las materias primas desde la BD por los que está formada la pieza
        public static void ObtenerEstructura(int codigoPieza, Data.dsEstructura ds)
        {
            string sql = @"SELECT mpxp_codigo, pza_codigo, mp_codigo, mp_cantidad
                         FROM MATERIASPRIMASXPIEZA WHERE pza_codigo = @p0";
            
            object[] valorParametros = { codigoPieza };
            try
            {
                //Primero obtenemos la tabla intermedia
                DB.FillDataSet(ds, "MATERIASPRIMASXPIEZA", sql, valorParametros);
                //Ahora los datos de las materias primas que estén en la consulta anterior
                Entidades.MateriaPrima materiaPrima = new GyCAP.Entidades.MateriaPrima();
                foreach (Data.dsEstructura.MATERIASPRIMASXPIEZARow row in ds.MATERIASPRIMASXPIEZA)
                {
                    //Como ya tenemos todos los códigos de las materias primas que necesitamos, directamente
                    //se los pedimos a MateriaPrimaDAL
                    materiaPrima = DAL.MateriaPrimaDAL.ObtenerMateriaPrima(Convert.ToInt32(row.MP_CODIGO));
                    Data.dsEstructura.MATERIAS_PRIMASRow rowMateriaPrima = ds.MATERIAS_PRIMAS.NewMATERIAS_PRIMASRow();
                    rowMateriaPrima.BeginEdit();
                    rowMateriaPrima.MP_CODIGO = materiaPrima.CodigoMateriaPrima;
                    rowMateriaPrima.MP_NOMBRE = materiaPrima.Nombre;
                    rowMateriaPrima.UMED_CODIGO = materiaPrima.CodigoUnidadMedida;
                    rowMateriaPrima.MP_DESCRIPCION = materiaPrima.Descripcion;
                    rowMateriaPrima.MP_PRECIO = materiaPrima.Precio;
                    rowMateriaPrima.EndEdit();
                    ds.MATERIAS_PRIMAS.AddMATERIAS_PRIMASRow(rowMateriaPrima);
                }
                ds.MATERIAS_PRIMAS.AcceptChanges();
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            catch (Entidades.Excepciones.ElementoInexistenteException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
