using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class ConjuntoDAL
    {
        public static readonly int estadoActivo = 1;
        public static readonly int estadoInactivo = 0;

        public static void Insertar(Data.dsEstructura dsEstructura)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sqlInsert = @"INSERT INTO [CONJUNTOS]
                                        ([conj_nombre]
                                        ,[te_codigo]
                                        ,[conj_descripcion]
                                        ,[conj_cantidadstock]
                                        ,[par_codigo]
                                        ,[pno_codigo]
                                        .[conj_codigoparte]) 
                                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6) SELECT @@Identity";

            //Así obtenemos el conjunto nuevo del dataset, indicamos la primer fila de las agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.CONJUNTOSRow rowConjunto = dsEstructura.CONJUNTOS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructura.CONJUNTOSRow;
            object[] valorParametros = { rowConjunto.CONJ_NOMBRE, rowConjunto.TE_CODIGO, rowConjunto.CONJ_DESCRIPCION, 0, rowConjunto.PAR_CODIGO, rowConjunto.PNO_CODIGO, rowConjunto.CONJ_CODIGOPARTE };
                        
            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;
            
            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();
                //Insertamos el conjunto y actualizamos su código en el dataset
                rowConjunto.BeginEdit();
                rowConjunto.CONJ_CODIGO = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));
                rowConjunto.EndEdit();
                //Ahora insertamos su estructura, 
                Entidades.DetalleConjunto detalle = new GyCAP.Entidades.DetalleConjunto();
                foreach (Data.dsEstructura.DETALLE_CONJUNTORow row in (Data.dsEstructura.DETALLE_CONJUNTORow[])dsEstructura.DETALLE_CONJUNTO.Select(null, null, System.Data.DataViewRowState.Added))
                {                    
                    detalle.CodigoConjunto = Convert.ToInt32(rowConjunto.CONJ_CODIGO);
                    detalle.CodigoSubconjunto = Convert.ToInt32(row.SCONJ_CODIGO);
                    detalle.Cantidad = row.DCJ_CANTIDAD;
                    DetalleConjuntoDAL.Insertar(detalle, transaccion);
                    //Actualizamos el dataset
                    row.BeginEdit();
                    row.CONJ_CODIGO = detalle.CodigoConjunto;
                    row.DCJ_CODIGO = detalle.CodigoDetalle;
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
            SqlTransaction transaccion = null;
            try
            {
                transaccion = DB.IniciarTransaccion();
                //Primero los hijos
                DetalleConjuntoDAL.EliminarDetalleDeConjunto(codigoConjunto, transaccion);
                //Ahora el padre
                DB.executeNonQuery(sql, valorParametros, transaccion);
                //Todo OK
                transaccion.Commit();
            }
            catch (SqlException)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException();
            }
            finally
            {
                DB.FinalizarTransaccion();
            }
        }

        public static void Actualizar(Data.dsEstructura dsEstructura)
        {
            //Esto va a ser muy largo...empecemos
            //Primero actualizaremos el conjunto y luego la estructura
            //Armemos todas las consultas
            string sqlUpdateConjunto = @"UPDATE CONJUNTOS SET
                                         conj_nombre = @p0
                                        ,te_codigo = @p1
                                        ,conj_descripcion = @p2
                                        ,par_codigo = @p3
                                        ,pno_codigo = @p4
                                        ,conj_codigoparte = @p5
                                        WHERE conj_codigo = @p6";

            //Así obtenemos el conjunto modificado del dataset, indicamos la primer fila de las modificadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.CONJUNTOSRow rowConjunto = dsEstructura.CONJUNTOS.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsEstructura.CONJUNTOSRow;
            object[] valorParametros = { rowConjunto.CONJ_NOMBRE, rowConjunto.TE_CODIGO, rowConjunto.CONJ_DESCRIPCION, rowConjunto.PAR_CODIGO, rowConjunto.PNO_CODIGO, rowConjunto.CONJ_CODIGOPARTE, rowConjunto.CONJ_CODIGO };

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;            
            
            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Actualizamos el conjunto
                DB.executeNonQuery(sqlUpdateConjunto, valorParametros, transaccion);
                //Actualizamos la estructura, primero insertamos los nuevos
                Entidades.DetalleConjunto detalle = new GyCAP.Entidades.DetalleConjunto();            
                foreach (Data.dsEstructura.DETALLE_CONJUNTORow row in (Data.dsEstructura.DETALLE_CONJUNTORow[])dsEstructura.DETALLE_CONJUNTO.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    detalle.CodigoConjunto = Convert.ToInt32(row.CONJ_CODIGO);
                    detalle.CodigoSubconjunto = Convert.ToInt32(row.SCONJ_CODIGO);
                    detalle.Cantidad = row.DCJ_CANTIDAD;
                    DetalleConjuntoDAL.Insertar(detalle, transaccion);
                    row.BeginEdit();
                    row.DCJ_CODIGO = detalle.CodigoDetalle;
                    row.EndEdit();
                }

                //Segundo actualizamos los modificados
                foreach (Data.dsEstructura.DETALLE_CONJUNTORow row in (Data.dsEstructura.DETALLE_CONJUNTORow[])dsEstructura.DETALLE_CONJUNTO.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    detalle.CodigoDetalle = Convert.ToInt32(row.DCJ_CODIGO);
                    detalle.CodigoConjunto = Convert.ToInt32(row.CONJ_CODIGO);
                    detalle.CodigoSubconjunto = Convert.ToInt32(row.SCONJ_CODIGO);
                    detalle.Cantidad = row.DCJ_CANTIDAD;
                    DetalleConjuntoDAL.Actualizar(detalle, transaccion);
                }

                //Tercero eliminamos
                foreach (Data.dsEstructura.DETALLE_CONJUNTORow row in (Data.dsEstructura.DETALLE_CONJUNTORow[])dsEstructura.DETALLE_CONJUNTO.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    detalle.CodigoDetalle = Convert.ToInt32(row["dcj_codigo", System.Data.DataRowVersion.Original]);
                    DetalleConjuntoDAL.Eliminar(detalle, transaccion);
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
            string sql = @"SELECT conj_nombre, te_codigo, conj_descripcion, conj_cantidadstock, par_codigo, pno_codigo, conj_codigoparte
                        FROM CONJUNTOS WHERE conj_codigo = @p0";
            object[] valorParametros = { codigoConjunto };
            SqlDataReader rdr = DB.GetReader(sql, valorParametros, null);
            Entidades.Conjunto conjunto = new GyCAP.Entidades.Conjunto();
            try
            {
                if (!rdr.HasRows) { throw new Entidades.Excepciones.ElementoInexistenteException(); }
                rdr.Read();
                conjunto.CodigoConjunto = codigoConjunto;
                conjunto.CodigoParte = rdr["conj_codigoparte"].ToString();
                conjunto.Nombre = rdr["conj_nombre"].ToString();
                conjunto.CodigoTerminacion = Convert.ToInt32(rdr["te_codigo"].ToString());
                conjunto.Descripcion = rdr["conj_descripcion"].ToString();
                conjunto.CantidadStock = Convert.ToInt32(rdr["conj_cantidadstock"].ToString());
                conjunto.CodigoEstado = Convert.ToInt32(rdr["par_codigo"].ToString());
                conjunto.CodigoPlano = Convert.ToInt32(rdr["pno_codigo"].ToString());
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            finally
            {
                if (rdr != null) { rdr.Close(); }
                DB.CloseReader();
            }
            return conjunto;
        }

        public static void ObtenerConjuntos(object nombre, object codTerminacion, Data.dsEstructura ds, bool obtenerDetalle)
        {
            string sql = @"SELECT conj_codigo, conj_nombre, conj_descripcion, te_codigo, conj_cantidadstock, par_codigo, pno_codigo, conj_codigoparte 
                        FROM CONJUNTOS WHERE 1=1";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[2];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND conj_nombre LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }
            //Revisamos si pasó algun valor y si es un integer
            if (codTerminacion != null && codTerminacion.GetType() == cantidadParametros.GetType())
            {
                sql += " AND te_codigo = @p" + cantidadParametros; 
                valoresFiltros[cantidadParametros] = Convert.ToInt32(codTerminacion);
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
                try
                {
                    DB.FillDataSet(ds, "CONJUNTOS", sql, valorParametros);
                    if (obtenerDetalle) { ObtenerDetalleConjuntos(ds); }
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
            else
            {
                //Buscamos sin filtro
                try
                {
                    DB.FillDataSet(ds, "CONJUNTOS", sql, null);
                    if (obtenerDetalle) { ObtenerDetalleConjuntos(ds); }
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
        }

        //Obtiene el detalle de todos los subconjuntos buscados, de uso interno por el método buscador ObtenerSubconjuntos
        private static void ObtenerDetalleConjuntos(Data.dsEstructura ds)
        {
            string sql = @"SELECT dcj_codigo, conj_codigo, sconj_codigo, dcj_cantidad
                         FROM DETALLE_CONJUNTO WHERE conj_codigo = @p0";

            object[] valorParametros; ;

            foreach (Data.dsEstructura.CONJUNTOSRow rowConjunto in ds.CONJUNTOS)
            {
                valorParametros = new object[] { rowConjunto.CONJ_CODIGO };
                DB.FillDataTable(ds.DETALLE_CONJUNTO, sql, valorParametros);
            }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sqlE = "SELECT count(conj_codigo) FROM CONJUNTOSXESTRUCTURA WHERE conj_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                int resultadoCXE = Convert.ToInt32(DB.executeScalar(sqlE, valorParametros, null));
                if (resultadoCXE == 0) { return true; }
                else { return false; }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        
        /// <summary>
        /// Obtiene todos los subconjuntos desde la BD por los que está formado el conjunto. Los carga
        /// dentro de la tabla DETALLE_CONJUNTO de un dataset del tipo dsEstructura.
        /// </summary>
        /// <param name="codigoConjunto">El código del conjunto cuya estructura se desea obtener.</param>
        /// <param name="ds">El dataset del tipo dsEstructura.</param>
        public static void ObtenerEstructura(int codigoConjunto, Data.dsEstructura ds)
        {
            string sql = @"SELECT dcj_codigo, conj_codigo, sconj_codigo, dcj_cantidad
                         FROM DETALLE_CONJUNTO WHERE conj_codigo = @p0";
            object[] valorParametros = { codigoConjunto };
            try
            {
                //Primero obtenemos la tabla intermedia
                DB.FillDataSet(ds, "DETALLE_CONJUNTO", sql, valorParametros);
                //Ahora los datos de los subconjuntos que estén en la consulta anterior
                Entidades.SubConjunto subconjunto = new GyCAP.Entidades.SubConjunto();
                foreach (Data.dsEstructura.DETALLE_CONJUNTORow row in ds.DETALLE_CONJUNTO)
                {
                    //Como ya tenemos todos los códigos de los subconjuntos que necesitamos, directamente
                    //se los pedimos a SubConjuntoDAL
                    subconjunto = DAL.SubConjuntoDAL.ObtenerSubconjunto(Convert.ToInt32(row.SCONJ_CODIGO));
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

        public static void ObtenerConjuntos(DataTable dtConjuntos)
        {
            string sql = @"SELECT conj_nombre, te_codigo, conj_descripcion, conj_cantidadstock, par_codigo, pno_codigo, conj_codigoparte
                        FROM CONJUNTOS";
            
            try
            {
                DB.FillDataTable(dtConjuntos, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void ObtenerConjuntos(DataTable dtConjuntos, int estado)
        {
            string sql = @"SELECT conj_nombre, te_codigo, conj_descripcion, conj_cantidadstock, par_codigo, pno_codigo, conj_codigoparte
                        FROM CONJUNTOS WHERE par_codigo = @p0 ";

            object[] valorParametros = { estado };

            try
            {
                DB.FillDataTable(dtConjuntos, sql, valorParametros);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
    }
}
