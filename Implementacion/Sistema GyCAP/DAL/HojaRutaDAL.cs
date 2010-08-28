﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class HojaRutaDAL
    {
        public static void Insertar(Data.dsProduccion dsHojaRuta)
        {
            string sqlHoja = @"INSERT INTO [HOJAS_RUTA] ([hr_nombre], [hr_descripcion], [hr_activo], [hr_fechaAlta]) 
                              VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            string sqlDetalle = "INSERT INTO [CENTROSXHOJARUTA] ([cto_codigo], [hr_codigo], [cxhr_secuencia]) VALUES (@p0, @p1, @p2) SELECT @@Identity";

            Data.dsProduccion.HOJAS_RUTARow rowhoja = dsHojaRuta.HOJAS_RUTA.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsProduccion.HOJAS_RUTARow;
            object[] valorParametros = { rowhoja.HR_NOMBRE, rowhoja.HR_DESCRIPCION, rowhoja.HR_ACTIVO, rowhoja.HR_FECHAALTA };

            SqlTransaction transaccion = null;
            try
            {
                transaccion = DB.IniciarTransaccion();
                rowhoja.BeginEdit();
                rowhoja.HR_CODIGO = Convert.ToInt32(DB.executeScalar(sqlHoja, valorParametros, transaccion));
                rowhoja.EndEdit();

                foreach (Data.dsProduccion.CENTROSXHOJARUTARow row in (Data.dsProduccion.CENTROSXHOJARUTARow[])dsHojaRuta.CENTROSXHOJARUTA.Select(null, null, DataViewRowState.Added))
                {
                    valorParametros = new object[] { row.CTO_CODIGO, rowhoja.HR_CODIGO, row.CXHR_SECUENCIA };
                    row.BeginEdit();
                    row.CXHR_CODIGO = Convert.ToInt32(DB.executeScalar(sqlDetalle, valorParametros, transaccion));
                    row.HR_CODIGO = rowhoja.HR_CODIGO;
                    row.EndEdit();
                }

                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
            finally
            {
                DB.FinalizarTransaccion();
            }
        }

        public static void Actualizar(Data.dsProduccion dsHojaRuta)
        {
            string sqlHoja = "UPDATE HOJAS_RUTA SET hr_nombre = @p0, hr_descripcion = @p1, hr_activo = @p2, hr_fechaalta = @p3 WHERE hr_codigo = @p4";
            string sqlIDetalle = "INSERT INTO [CENTROSXHOJARUTA] ([cto_codigo], [hr_codigo], [cxhr_secuencia]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
            string sqlUDetalle = "UPDATE CENTROSXHOJARUTA SET cxhr_secuencia = @p0 WHERE cxhr_codigo = @p1";
            string sqlDDetalle = "DELETE FROM CENTROSXHOJARUTA WHERE cxhr_codigo = @p0";
            Data.dsProduccion.HOJAS_RUTARow rowHoja = dsHojaRuta.HOJAS_RUTA.GetChanges(DataRowState.Modified).Rows[0] as Data.dsProduccion.HOJAS_RUTARow;
            object[] valorParametros = { rowHoja.HR_NOMBRE, rowHoja.HR_DESCRIPCION, rowHoja.HR_ACTIVO, rowHoja.HR_FECHAALTA, rowHoja.HR_CODIGO };
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();

                DB.executeNonQuery(sqlHoja, valorParametros, transaccion);

                foreach (Data.dsProduccion.CENTROSXHOJARUTARow row in (Data.dsProduccion.CENTROSXHOJARUTARow[])dsHojaRuta.CENTROSXHOJARUTA.Select(null, null, DataViewRowState.Added))
                {
                    valorParametros = new object[] { row.CTO_CODIGO, row.HR_CODIGO, row.CXHR_SECUENCIA };
                    row.BeginEdit();
                    row.CXHR_CODIGO = Convert.ToInt32(DB.executeScalar(sqlIDetalle, valorParametros, transaccion));
                    row.EndEdit();
                }

                foreach (Data.dsProduccion.CENTROSXHOJARUTARow row in (Data.dsProduccion.CENTROSXHOJARUTARow[])dsHojaRuta.CENTROSXHOJARUTA.Select(null, null, DataViewRowState.ModifiedCurrent))
                {
                    valorParametros = new object[] { row.CXHR_SECUENCIA, row.CXHR_CODIGO };
                    DB.executeNonQuery(sqlUDetalle, valorParametros, transaccion);
                }

                foreach (Data.dsProduccion.CENTROSXHOJARUTARow row in (Data.dsProduccion.CENTROSXHOJARUTARow[])dsHojaRuta.CENTROSXHOJARUTA.Select(null, null, DataViewRowState.Deleted))
                {
                    valorParametros = new object[] { Convert.ToInt32(row["cxhr_codigo", DataRowVersion.Original]) };
                    DB.executeNonQuery(sqlDDetalle, valorParametros, transaccion);
                }

                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
            finally
            {
                DB.FinalizarTransaccion();
            }
        }

        public static void Eliminar(int codigo)
        {
            string sqlHoja = "DELETE FROM HOJAS_RUTA WHERE hr_codigo = @p0";
            string sqlDetalle = "DELETE FROM CENTROSXHOJARUTA WHERE hr_codigo = @p0";
            object[] valorParametros = { codigo };
            SqlTransaction transaccion = null;

            try
            {
                DB.executeNonQuery(sqlDetalle, valorParametros, transaccion);
                DB.executeNonQuery(sqlHoja, valorParametros, transaccion);
                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
            finally
            {
                DB.FinalizarTransaccion();
            }
        }

        public static bool PuedeEliminarse(int codigoHoja)
        {
            string sqlC = "SELECT count(conj_codigo) FROM CONJUNTOS WHERE hr_codigo = @p0";
            string sqlSC = "SELECT count(sconj_codigo) FROM SUBCONJUNTOS WHERE hr_codigo = @p0";
            string sqlP = "SELECT count(pza_codigo) FROM PIEZAS WHERE hr_codigo = @p0";
            object[] valorParametros = { codigoHoja };
            int resultC = 0, resultSC = 0, resultP = 0;
            try
            {
                resultC = Convert.ToInt32(DB.executeScalar(sqlC, valorParametros, null));
                resultSC = Convert.ToInt32(DB.executeScalar(sqlSC, valorParametros, null));
                resultP = Convert.ToInt32(DB.executeScalar(sqlP, valorParametros, null));

                if (resultC == 0 && resultSC == 0 && resultP == 0) { return true; }
                else { return false; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
        public static void ObtenerHojasRuta(object nombre, object estado, Data.dsProduccion dsHojaRuta, bool obtenerDetalle)
        {
            string sql = @"SELECT hr_codigo, hr_nombre, hr_descripcion, hr_activo, hr_fechaalta FROM HOJAS_RUTA WHERE 1=1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[2];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND hr_nombre LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }

            if (estado != null && estado.GetType() == cantidadParametros.GetType())
            {
                sql += " AND hr_activo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = estado;
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
                    DB.FillDataSet(dsHojaRuta, "HOJAS_RUTA", sql, valorParametros);
                    if (obtenerDetalle) { ObtenerDetalleHoja(dsHojaRuta); }
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
            else
            {
                //Buscamos sin filtro
                try
                {
                    DB.FillDataSet(dsHojaRuta, "HOJAS_RUTA", sql, null);
                    if (obtenerDetalle) { ObtenerDetalleHoja(dsHojaRuta); }
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
        }

        public static void ObtenerHojasRuta(DataTable dtHojas)
        {
            string sql = @"SELECT hr_codigo, hr_nombre, hr_descripcion, hr_activo, hr_fechaalta FROM HOJAS_RUTA";

            try
            {
                DB.FillDataTable(dtHojas, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        private static void ObtenerDetalleHoja(Data.dsProduccion ds)
        {
            string sql = "SELECT cxhr_codigo, cto_codigo, hr_codigo, cxhr_secuencia FROM CENTROSXHOJARUTA WHERE hr_codigo = @p0";
            object[] valorParametros;

            foreach (Data.dsProduccion.HOJAS_RUTARow row in ds.HOJAS_RUTA)
            {
                valorParametros = new object[] { row.HR_CODIGO };
                DB.FillDataTable(ds.CENTROSXHOJARUTA, sql, valorParametros);
            }
        }
    }
}