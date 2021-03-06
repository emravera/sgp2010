﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class SubConjuntoDAL
    {
        public static readonly int estadoActivo = 1;
        public static readonly int estadoInactivo = 0;
        
        public static void Insertar(Data.dsEstructura dsEstructura)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sqlInsertSubconjunto = @"INSERT INTO [SUBCONJUNTOS]
                                        ([sconj_nombre]
                                        ,[sconj_descripcion]
                                        ,[par_codigo]
                                        ,[pno_codigo]
                                        ,[sconj_codigoparte]
                                        ,[sconj_costo]
                                        ,[hr_codigo]
                                        ,[sconj_costofijo]) 
                                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7) SELECT @@Identity";

            //Así obtenemos el subconjunto nuevo del dataset, indicamos la primer fila de las agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.SUBCONJUNTOSRow rowSubconjunto = dsEstructura.SUBCONJUNTOS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructura.SUBCONJUNTOSRow;
            object hojaRuta = DBNull.Value;
            if (!rowSubconjunto.IsHR_CODIGONull()) { hojaRuta = rowSubconjunto.HR_CODIGO; }
            object[] valorParametros = { rowSubconjunto.SCONJ_NOMBRE,
                                           rowSubconjunto.SCONJ_DESCRIPCION,
                                           rowSubconjunto.PAR_CODIGO, 
                                           rowSubconjunto.PNO_CODIGO, 
                                           rowSubconjunto.SCONJ_CODIGOPARTE,
                                           rowSubconjunto.SCONJ_COSTO,
                                           hojaRuta,
                                           rowSubconjunto.SCONJ_COSTOFIJO };
            
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
                Entidades.DetalleSubconjunto detalle = new GyCAP.Entidades.DetalleSubconjunto();
                foreach (Data.dsEstructura.PIEZASXSUBCONJUNTORow row in (Data.dsEstructura.PIEZASXSUBCONJUNTORow[])dsEstructura.PIEZASXSUBCONJUNTO.Select(null, null, System.Data.DataViewRowState.Added))
                {                    
                    detalle.CodigoSubconjunto = Convert.ToInt32(rowSubconjunto.SCONJ_CODIGO);
                    detalle.CodigoPieza = Convert.ToInt32(row.PZA_CODIGO);
                    detalle.Cantidad = row.PXSC_CANTIDAD;
                    DetalleSubconjuntoDAL.Insertar(detalle, transaccion);                                        
                    row.BeginEdit();
                    row.SCONJ_CODIGO = detalle.CodigoSubconjunto;
                    row.PXSC_CODIGO = detalle.CodigoDetalle;
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

        public static void Eliminar(int codigoSubconjunto)
        {
            string sql = "DELETE FROM SUBCONJUNTOS WHERE sconj_codigo = @p0";
            object[] valorParametros = { codigoSubconjunto };
            
            SqlTransaction transaccion = null;
            try
            {
                transaccion = DB.IniciarTransaccion();
                //Primero los hijos
                DetalleSubconjuntoDAL.EliminarDetalleDeSubconjunto(codigoSubconjunto, transaccion);
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
            //Primero actualizaremos el subconjunto y luego la estructura
            //Armemos todas las consultas
            string sqlUpdate = @"UPDATE SUBCONJUNTOS SET
                                sconj_nombre = @p0
                               ,sconj_descripcion = @p1
                               ,par_codigo = @p2
                               ,pno_codigo = @p3
                               ,sconj_codigoparte = @p4
                               ,sconj_costo = @p5
                               ,hr_codigo = @p6
                               ,sconj_costofijo = @p7
                               WHERE sconj_codigo = @p8";

            //Así obtenemos el subconjunto del dataset, indicamos la primer fila de las modificadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.SUBCONJUNTOSRow rowSubconjunto = dsEstructura.SUBCONJUNTOS.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsEstructura.SUBCONJUNTOSRow;
            object hojaRuta = DBNull.Value;
            if (!rowSubconjunto.IsHR_CODIGONull()) { hojaRuta = rowSubconjunto.HR_CODIGO; }
            object[] valorParametros = { rowSubconjunto.SCONJ_NOMBRE,
                                           rowSubconjunto.SCONJ_DESCRIPCION, 
                                           rowSubconjunto.PAR_CODIGO, 
                                           rowSubconjunto.PNO_CODIGO, 
                                           rowSubconjunto.SCONJ_CODIGOPARTE, 
                                           rowSubconjunto.SCONJ_COSTO,
                                           hojaRuta,
                                           rowSubconjunto.SCONJ_COSTOFIJO,
                                           rowSubconjunto.SCONJ_CODIGO };

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Actualizamos el subconjunto
                DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
                //Actualizamos la estructura, primero insertamos los nuevos
                Entidades.DetalleSubconjunto detalle = new GyCAP.Entidades.DetalleSubconjunto();
                foreach (Data.dsEstructura.PIEZASXSUBCONJUNTORow row in (Data.dsEstructura.PIEZASXSUBCONJUNTORow[])dsEstructura.PIEZASXSUBCONJUNTO.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    detalle.CodigoSubconjunto = Convert.ToInt32(row.SCONJ_CODIGO);
                    detalle.CodigoPieza = Convert.ToInt32(row.PZA_CODIGO);
                    detalle.Cantidad = row.PXSC_CANTIDAD;
                    DetalleSubconjuntoDAL.Insertar(detalle, transaccion);
                    row.BeginEdit();
                    row.PXSC_CODIGO = detalle.CodigoDetalle;
                    row.EndEdit();
                }

                //Segundo actualizamos los modificados
                foreach (Data.dsEstructura.PIEZASXSUBCONJUNTORow row in (Data.dsEstructura.PIEZASXSUBCONJUNTORow[])dsEstructura.PIEZASXSUBCONJUNTO.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    detalle.CodigoDetalle = Convert.ToInt32(row.PXSC_CODIGO);
                    detalle.CodigoSubconjunto = Convert.ToInt32(row.SCONJ_CODIGO);
                    detalle.CodigoPieza = Convert.ToInt32(row.PZA_CODIGO);
                    detalle.Cantidad = row.PXSC_CANTIDAD;
                    DetalleSubconjuntoDAL.Actualizar(detalle, transaccion);
                }

                //Tercero eliminamos
                foreach (Data.dsEstructura.PIEZASXSUBCONJUNTORow row in (Data.dsEstructura.PIEZASXSUBCONJUNTORow[])dsEstructura.PIEZASXSUBCONJUNTO.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    detalle.CodigoDetalle = Convert.ToInt32(row["pxsc_codigo", System.Data.DataRowVersion.Original]);
                    DetalleSubconjuntoDAL.Eliminar(detalle, transaccion);
                }

                //Si todo resulto correcto, commit
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

        public static void ObtenerSubconjuntos(DataTable dtSubconjuntos)
        {
            string sql = @"SELECT sconj_codigo, sconj_nombre, sconj_descripcion, par_codigo, pno_codigo, 
                                  sconj_codigoparte, sconj_costo, hr_codigo, sconj_costofijo FROM SUBCONJUNTOS";

            try
            {
                DB.FillDataTable(dtSubconjuntos, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerSubconjuntos(DataTable dtSubconjuntos, int estado)
        {
            string sql = @"SELECT sconj_codigo, sconj_nombre, sconj_descripcion, par_codigo, pno_codigo, 
                                  sconj_codigoparte, sconj_costo, hr_codigo, sconj_costofijo FROM SUBCONJUNTOS WHERE par_codigo = @p0";
            object[] valorParametros = { estado };
            try
            {
                DB.FillDataTable(dtSubconjuntos, sql, valorParametros);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
        public static void ObtenerSubconjuntos(object nombre, Data.dsEstructura ds, bool obtenerDetalle)
        {
            string sql = @"SELECT sconj_codigo, sconj_nombre, sconj_descripcion, par_codigo, pno_codigo, 
                                  sconj_codigoparte, sconj_costo, hr_codigo, sconj_costofijo FROM SUBCONJUNTOS WHERE 1=1";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[2];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //si aplica el filtro lo usamos
                sql += " AND sconj_nombre LIKE @p" + cantidadParametros;
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
                    DB.FillDataSet(ds, "SUBCONJUNTOS", sql, valorParametros);
                    if(obtenerDetalle) { ObtenerDetalleSubconjuntos(ds); }
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
            else
            {
                //Buscamos sin filtro
                DB.FillDataSet(ds, "SUBCONJUNTOS", sql, null);
                if (obtenerDetalle) { ObtenerDetalleSubconjuntos(ds); }
            }
        }

        //Obtiene el detalle de todos los subconjuntos buscadas, de uso interno por el método buscador ObtenerSubconjuntos
        private static void ObtenerDetalleSubconjuntos(Data.dsEstructura ds)
        {
            string sql = @"SELECT pxsc_codigo, sconj_codigo, pza_codigo, pxsc_cantidad
                         FROM PIEZASXSUBCONJUNTO WHERE sconj_codigo = @p0";

            object[] valorParametros; ;

            foreach (Data.dsEstructura.SUBCONJUNTOSRow rowSubconjunto in ds.SUBCONJUNTOS)
            {
                valorParametros = new object[] { rowSubconjunto.SCONJ_CODIGO };
                DB.FillDataTable(ds.PIEZASXSUBCONJUNTO, sql, valorParametros);
            }
        }

        public static void ObtenerSubconjunto(int codigoSubconjunto, bool detalle, Data.dsEstructura ds)
        {
            string sql = @"SELECT sconj_codigo, sconj_nombre, sconj_descripcion, par_codigo, pno_codigo, 
                                  sconj_codigoparte, sconj_costo, hr_codigo, sconj_costofijo FROM SUBCONJUNTOS WHERE sconj_codigo = @p0";
            object[] valorParametros = { codigoSubconjunto };

            try
            {
                DB.FillDataTable(ds.SUBCONJUNTOS, sql, valorParametros);
                if (detalle)
                {
                    //Obtenemos las piezas que forman el subconjunto y su detalle
                    ObtenerDetalleSubconjunto(codigoSubconjunto, ds);
                }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
        private static void ObtenerDetalleSubconjunto(int codigoSubconjunto, Data.dsEstructura ds)
        {
            string sql = @"SELECT pxsc_codigo, sconj_codigo, pza_codigo, pxsc_cantidad
                         FROM PIEZASXSUBCONJUNTO WHERE sconj_codigo = @p0";

            object[] valorParametros = { codigoSubconjunto };

            DB.FillDataTable(ds.PIEZASXSUBCONJUNTO, sql, valorParametros);
            //Obtenemos las piezas y su detalle
            foreach (Data.dsEstructura.PIEZASXSUBCONJUNTORow rowPxSC in (Data.dsEstructura.PIEZASXSUBCONJUNTORow[])ds.PIEZASXSUBCONJUNTO.Select("SCONJ_CODIGO = " + codigoSubconjunto))
            {
                PiezaDAL.ObtenerPieza(Convert.ToInt32(rowPxSC.PZA_CODIGO), true, ds);
            }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sqlSCXC = "SELECT count(sconj_codigo) FROM SUBCONJUNTOSXCONJUNTO WHERE sconj_codigo = @p0";

            object[] valorParametros = { codigo };
            try
            {
                int resultadoSCXC = Convert.ToInt32(DB.executeScalar(sqlSCXC, valorParametros, null));
                if (resultadoSCXC == 0) { return true; }
                else { return false; }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
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
            string sql = @"SELECT sconj_nombre, sconj_descripcion, par_codigo, pno_codigo, 
                                  sconj_codigoparte, sconj_costo, hr_codigo, sconj_costofijo FROM SUBCONJUNTOS WHERE sconj_codigo = @p0";
            object[] valorParametros = { codigoSubconjunto };
            SqlDataReader rdr = DB.GetReader(sql, valorParametros, null);
            Entidades.SubConjunto subconjunto = new GyCAP.Entidades.SubConjunto();
            try
            {
                if (!rdr.HasRows) { throw new Entidades.Excepciones.ElementoInexistenteException(); }                
                rdr.Read();
                subconjunto.CodigoSubconjunto = codigoSubconjunto;
                subconjunto.CodigoParte = rdr["sconj_codigoparte"].ToString();
                subconjunto.Nombre = rdr["sconj_nombre"].ToString();
                subconjunto.Descripcion = rdr["sconj_descripcion"].ToString();
                subconjunto.CodigoEstado = Convert.ToInt32(rdr["par_codigo"].ToString());
                subconjunto.CodigoPlano = Convert.ToInt32(rdr["pno_codigo"].ToString());
                subconjunto.Costo = Convert.ToDecimal(rdr["sconj_costo"].ToString());
                if (rdr["hr_codigo"] != DBNull.Value) { subconjunto.CodigoHojaRuta = Convert.ToInt32(rdr["hr_codigo"].ToString()); }
                subconjunto.CostoFijo = Convert.ToInt32(rdr["sconj_costofijo"].ToString());
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
