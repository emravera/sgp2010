﻿using System;
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
                                        ,[conj_descripcion]
                                        ,[par_codigo]
                                        ,[pno_codigo]
                                        ,[conj_codigoparte]
                                        ,[conj_costo]
                                        ,[hr_codigo]
                                        ,[conj_costofijo]) 
                                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7) SELECT @@Identity";

            //Así obtenemos el conjunto nuevo del dataset, indicamos la primer fila de las agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.CONJUNTOSRow rowConjunto = dsEstructura.CONJUNTOS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructura.CONJUNTOSRow;
            object hojaRuta = DBNull.Value;
            if (!rowConjunto.IsHR_CODIGONull()) { hojaRuta = rowConjunto.HR_CODIGO; }
            object[] valorParametros = { rowConjunto.CONJ_NOMBRE,
                                         rowConjunto.CONJ_DESCRIPCION, 
                                         rowConjunto.PAR_CODIGO, 
                                         rowConjunto.PNO_CODIGO, 
                                         rowConjunto.CONJ_CODIGOPARTE, 
                                         rowConjunto.CONJ_COSTO, 
                                         hojaRuta,
                                         rowConjunto.CONJ_COSTOFIJO };
                        
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
                //Ahora insertamos su estructura

                Entidades.PiezasxConjunto pxc = new GyCAP.Entidades.PiezasxConjunto();
                Entidades.SubconjuntosxConjunto scxc = new GyCAP.Entidades.SubconjuntosxConjunto();

                foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow row in (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow[])dsEstructura.SUBCONJUNTOSXCONJUNTO.Select(null, null, DataViewRowState.Added))
                {
                    scxc.CodigoConjunto = Convert.ToInt32(rowConjunto.CONJ_CODIGO);
                    scxc.CodigoSubconjunto = Convert.ToInt32(row.SCONJ_CODIGO);
                    scxc.Cantidad = Convert.ToInt32(row.SCXCJ_CANTIDAD);
                    DetalleConjuntoDAL.Insertar(scxc, transaccion);
                    row.BeginEdit();
                    row.CONJ_CODIGO = scxc.CodigoConjunto;
                    row.SCXCJ_CODIGO = scxc.CodigoDetalle;
                    row.EndEdit();
                }
                
                foreach (Data.dsEstructura.PIEZASXCONJUNTORow row in (Data.dsEstructura.PIEZASXCONJUNTORow[]) dsEstructura.PIEZASXCONJUNTO.Select(null, null, DataViewRowState.Added))
                {
                    pxc.CodigoConjunto = Convert.ToInt32(rowConjunto.CONJ_CODIGO);
                    pxc.CodigoPieza = Convert.ToInt32(row.PZA_CODIGO);
                    pxc.Cantidad = Convert.ToInt32(row.PXCJ_CANTIDAD);
                    DetalleConjuntoDAL.Insertar(pxc, transaccion);
                    row.BeginEdit();
                    row.CONJ_CODIGO = pxc.CodigoConjunto;
                    row.PXCJ_CODIGO = pxc.CodigoDetalle;
                    row.EndEdit();
                }

                //Todo ok, commit
                transaccion.Commit();                
            }
            catch (SqlException ex)
            {
                //Error en alguna consulta, descartamos los cambios
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
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
                                        ,conj_descripcion = @p1
                                        ,par_codigo = @p2
                                        ,pno_codigo = @p3
                                        ,conj_codigoparte = @p4
                                        ,conj_costo = @p5
                                        ,hr_codigo = @p6
                                        ,conj_costofijo = @p7
                                        WHERE conj_codigo = @p8";

            //Así obtenemos el conjunto modificado del dataset, indicamos la primer fila de las modificadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.CONJUNTOSRow rowConjunto = dsEstructura.CONJUNTOS.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsEstructura.CONJUNTOSRow;
            object hojaRuta = DBNull.Value;
            if (!rowConjunto.IsHR_CODIGONull()) { hojaRuta = rowConjunto.HR_CODIGO; }
            object[] valorParametros = { rowConjunto.CONJ_NOMBRE, 
                                           rowConjunto.CONJ_DESCRIPCION, 
                                           rowConjunto.PAR_CODIGO, 
                                           rowConjunto.PNO_CODIGO, 
                                           rowConjunto.CONJ_CODIGOPARTE, 
                                           rowConjunto.CONJ_COSTO, 
                                           hojaRuta, 
                                           rowConjunto.CONJ_COSTOFIJO,
                                           rowConjunto.CONJ_CODIGO };

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;            
            
            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Actualizamos el conjunto
                DB.executeNonQuery(sqlUpdateConjunto, valorParametros, transaccion);
                //Actualizamos la estructura, en el orden: insertar, actualizar, eliminar

                Entidades.SubconjuntosxConjunto scxc = new GyCAP.Entidades.SubconjuntosxConjunto();
                Entidades.PiezasxConjunto pxc = new GyCAP.Entidades.PiezasxConjunto();
                foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow row in (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow[])dsEstructura.SUBCONJUNTOSXCONJUNTO.Select(null, null, DataViewRowState.Added))
                {
                    scxc.CodigoConjunto = Convert.ToInt32(row.CONJ_CODIGO);
                    scxc.CodigoSubconjunto = Convert.ToInt32(row.SCONJ_CODIGO);
                    scxc.Cantidad = Convert.ToInt32(row.SCXCJ_CANTIDAD);
                    DetalleConjuntoDAL.Insertar(scxc, transaccion);
                    row.BeginEdit();
                    row.SCXCJ_CODIGO = scxc.CodigoDetalle;
                    row.EndEdit();                                      
                }

                foreach (Data.dsEstructura.PIEZASXCONJUNTORow row in (Data.dsEstructura.PIEZASXCONJUNTORow[])dsEstructura.PIEZASXCONJUNTO.Select(null, null, DataViewRowState.Added))
                {
                    pxc.CodigoConjunto = Convert.ToInt32(row.CONJ_CODIGO);
                    pxc.CodigoPieza = Convert.ToInt32(row.PZA_CODIGO);
                    pxc.Cantidad = Convert.ToInt32(row.PXCJ_CANTIDAD);
                    DetalleConjuntoDAL.Insertar(pxc, transaccion);
                    row.BeginEdit();
                    row.PXCJ_CODIGO = pxc.CodigoDetalle;
                    row.EndEdit();                     
                }

                foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow row in (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow[])dsEstructura.SUBCONJUNTOSXCONJUNTO.Select(null, null, DataViewRowState.ModifiedCurrent))
                {
                    scxc.CodigoDetalle = Convert.ToInt32(row.SCXCJ_CODIGO);
                    scxc.CodigoConjunto = Convert.ToInt32(row.CONJ_CODIGO);
                    scxc.CodigoSubconjunto = Convert.ToInt32(row.SCONJ_CODIGO);
                    scxc.Cantidad = Convert.ToInt32(row.SCXCJ_CANTIDAD);
                    DetalleConjuntoDAL.Actualizar(scxc, transaccion); 
                }

                foreach (Data.dsEstructura.PIEZASXCONJUNTORow row in (Data.dsEstructura.PIEZASXCONJUNTORow[])dsEstructura.PIEZASXCONJUNTO.Select(null, null, DataViewRowState.ModifiedCurrent))
                {
                    pxc.CodigoDetalle = Convert.ToInt32(row.PXCJ_CODIGO);
                    pxc.CodigoConjunto = Convert.ToInt32(row.CONJ_CODIGO);
                    pxc.CodigoPieza = Convert.ToInt32(row.PZA_CODIGO);
                    pxc.Cantidad = Convert.ToInt32(row.PXCJ_CANTIDAD);
                    DetalleConjuntoDAL.Actualizar(pxc, transaccion); 
                }

                foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow row in (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow[])dsEstructura.SUBCONJUNTOSXCONJUNTO.Select(null, null, DataViewRowState.Deleted))
                {
                    scxc.CodigoDetalle = Convert.ToInt32(row["scxcj_codigo", System.Data.DataRowVersion.Original]);
                    DetalleConjuntoDAL.Eliminar(scxc, transaccion);
                }

                foreach (Data.dsEstructura.PIEZASXCONJUNTORow row in (Data.dsEstructura.PIEZASXCONJUNTORow[])dsEstructura.PIEZASXCONJUNTO.Select(null, null, DataViewRowState.Deleted))
                {
                    pxc.CodigoDetalle = Convert.ToInt32(row["pxcj_codigo", System.Data.DataRowVersion.Original]);
                    DetalleConjuntoDAL.Eliminar(pxc, transaccion);
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
        
        //Determina si existe un conjunto dado su nombre y terminación
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
            string sql = @"SELECT conj_codigo, conj_nombre, conj_descripcion, par_codigo, pno_codigo
                                 ,conj_codigoparte, conj_costo, hr_codigo, conj_costofijo FROM CONJUNTOS WHERE conj_codigo = @p0";
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
                conjunto.Descripcion = rdr["conj_descripcion"].ToString();
                conjunto.CodigoEstado = Convert.ToInt32(rdr["par_codigo"].ToString());
                conjunto.CodigoPlano = Convert.ToInt32(rdr["pno_codigo"].ToString());
                conjunto.Costo = Convert.ToDecimal(rdr["conj_costo"].ToString());
                if (rdr["hr_codigo"] != DBNull.Value) { conjunto.CodigoHojaRuta = Convert.ToInt32(rdr["hr_codigo"].ToString()); }
                conjunto.CostoFijo = Convert.ToInt32(rdr["conj_costofijo"].ToString());
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            finally
            {
                if (rdr != null) { rdr.Close(); }
                DB.CloseReader();
            }
            return conjunto;
        }

        public static void ObtenerConjuntos(object nombre, Data.dsEstructura ds, bool obtenerDetalle)
        {
            string sql = @"SELECT conj_codigo, conj_nombre, conj_descripcion, par_codigo, pno_codigo
                                  ,conj_codigoparte , conj_costo, hr_codigo, conj_costofijo FROM CONJUNTOS WHERE 1=1";

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

        public static void ObtenerConjunto(int codigoConjunto, bool detalle, Data.dsEstructura dsEstructura)
        {
            string sql = @"SELECT conj_codigo, conj_nombre, conj_descripcion, par_codigo, pno_codigo
                                 ,conj_codigoparte, conj_costo, hr_codigo, conj_costofijo FROM CONJUNTOS WHERE conj_codigo = @p0";
            object[] valorParametros = { codigoConjunto };

            try
            {
                DB.FillDataTable(dsEstructura.CONJUNTOS, sql, valorParametros);
                if (detalle)
                {
                    ObtenerDetalleConjunto(codigoConjunto, dsEstructura);
                }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
        //Obtiene el detalle de todos los conjuntos buscados, de uso interno por el método buscador ObtenerSubconjuntos
        private static void ObtenerDetalleConjuntos(Data.dsEstructura ds)
        {
            string sql = @"SELECT scxcj_codigo, conj_codigo, sconj_codigo, scxcj_cantidad
                         FROM SUBCONJUNTOSXCONJUNTO WHERE conj_codigo = @p0";

            string sql2 = @"SELECT pxcj_codigo, conj_codigo, pza_codigo, pxcj_cantidad
                         FROM PIEZASXCONJUNTO WHERE conj_codigo = @p0";

            object[] valorParametros; 

            foreach (Data.dsEstructura.CONJUNTOSRow rowConjunto in ds.CONJUNTOS)
            {
                valorParametros = new object[] { rowConjunto.CONJ_CODIGO };
                DB.FillDataTable(ds.SUBCONJUNTOSXCONJUNTO, sql, valorParametros);
                DB.FillDataTable(ds.PIEZASXCONJUNTO, sql2, valorParametros);
            }
        }

        //Obtiene el detalle del conjuntos buscado
        private static void ObtenerDetalleConjunto(int codigoConjunto, Data.dsEstructura ds)
        {
            string sql = @"SELECT scxcj_codigo, conj_codigo, sconj_codigo, scxcj_cantidad
                          FROM SUBCONJUNTOSXCONJUNTO WHERE conj_codigo = @p0";

            string sql2 = @"SELECT pxcj_codigo, conj_codigo, pza_codigo, pxcj_cantidad
                          FROM PIEZASXCONJUNTO WHERE conj_codigo = @p0";

            object[] valorParametros = { codigoConjunto };

            DB.FillDataTable(ds.SUBCONJUNTOSXCONJUNTO, sql, valorParametros);

            foreach (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow rowSCxC in (Data.dsEstructura.SUBCONJUNTOSXCONJUNTORow[])ds.SUBCONJUNTOSXCONJUNTO.Select("conj_codigo = " + codigoConjunto))
            {
                SubConjuntoDAL.ObtenerSubconjunto(Convert.ToInt32(rowSCxC.SCONJ_CODIGO), true, ds);
            }

            DB.FillDataTable(ds.PIEZASXCONJUNTO, sql2, valorParametros);

            foreach (Data.dsEstructura.PIEZASXCONJUNTORow rowPxC in (Data.dsEstructura.PIEZASXCONJUNTORow[])ds.PIEZASXCONJUNTO.Select("conj_codigo = " + codigoConjunto))
            {
                PiezaDAL.ObtenerPieza(Convert.ToInt32(rowPxC.PZA_CODIGO), true, ds);
            }
        }

        public static void ObtenerConjuntos(DataTable dtConjuntos)
        {
            string sql = @"SELECT conj_codigo, conj_nombre, conj_descripcion, par_codigo, pno_codigo,
                                  conj_codigoparte, conj_costo, hr_codigo, conj_costofijo FROM CONJUNTOS";
            
            try
            {
                DB.FillDataTable(dtConjuntos, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void ObtenerConjuntos(DataTable dtConjuntos, int estado)
        {
            string sql = @"SELECT conj_codigo, conj_nombre, conj_descripcion, par_codigo, pno_codigo
                                  ,conj_codigoparte, conj_costo, hr_codigo, conj_costofijo FROM CONJUNTOS WHERE par_codigo = @p0 ";

            object[] valorParametros = { estado };

            try
            {
                DB.FillDataTable(dtConjuntos, sql, valorParametros);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
    }
}
