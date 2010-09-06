using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class HojaRutaDAL
    {
        public static void Insertar(Data.dsHojaRuta dsHojaRuta)
        {
            string sqlHoja = @"INSERT INTO [HOJAS_RUTA] ([hr_nombre], [hr_descripcion], [hr_activo], [hr_fechaAlta]) 
                              VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            string sqlDetalle = "INSERT INTO [DETALLE_HOJARUTA] ([cto_codigo], [hr_codigo], [dhr_secuencia], [opr_numero]) VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            Data.dsHojaRuta.HOJAS_RUTARow rowhoja = dsHojaRuta.HOJAS_RUTA.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsHojaRuta.HOJAS_RUTARow;
            object[] valorParametros = { rowhoja.HR_NOMBRE, rowhoja.HR_DESCRIPCION, rowhoja.HR_ACTIVO, rowhoja.HR_FECHAALTA };

            SqlTransaction transaccion = null;
            try
            {
                transaccion = DB.IniciarTransaccion();
                rowhoja.BeginEdit();
                rowhoja.HR_CODIGO = Convert.ToInt32(DB.executeScalar(sqlHoja, valorParametros, transaccion));
                rowhoja.EndEdit();

                foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow row in (Data.dsHojaRuta.DETALLE_HOJARUTARow[])dsHojaRuta.DETALLE_HOJARUTA.Select(null, null, DataViewRowState.Added))
                {
                    valorParametros = new object[] { row.CTO_CODIGO, rowhoja.HR_CODIGO, row.DHR_SECUENCIA, row.OPR_NUMERO };
                    row.BeginEdit();
                    row.DHR_CODIGO = Convert.ToInt32(DB.executeScalar(sqlDetalle, valorParametros, transaccion));
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

        public static void Actualizar(Data.dsHojaRuta dsHojaRuta)
        {
            string sqlHoja = "UPDATE HOJAS_RUTA SET hr_nombre = @p0, hr_descripcion = @p1, hr_activo = @p2, hr_fechaalta = @p3 WHERE hr_codigo = @p4";
            string sqlIDetalle = "INSERT INTO [DETALLE_HOJARUTA] ([cto_codigo], [hr_codigo], [dhr_secuencia], [opr_numero]) VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";
            string sqlUDetalle = "UPDATE DETALLE_HOJARUTA SET dhr_secuencia = @p0 WHERE dhr_codigo = @p1";
            string sqlDDetalle = "DELETE FROM DETALLE_HOJARUTA WHERE dhr_codigo = @p0";
            Data.dsHojaRuta.HOJAS_RUTARow rowHoja = dsHojaRuta.HOJAS_RUTA.GetChanges(DataRowState.Modified).Rows[0] as Data.dsHojaRuta.HOJAS_RUTARow;
            object[] valorParametros = { rowHoja.HR_NOMBRE, rowHoja.HR_DESCRIPCION, rowHoja.HR_ACTIVO, rowHoja.HR_FECHAALTA, rowHoja.HR_CODIGO };
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();

                DB.executeNonQuery(sqlHoja, valorParametros, transaccion);

                foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow row in (Data.dsHojaRuta.DETALLE_HOJARUTARow[])dsHojaRuta.DETALLE_HOJARUTA.Select(null, null, DataViewRowState.Added))
                {
                    valorParametros = new object[] { row.CTO_CODIGO, row.HR_CODIGO, row.DHR_SECUENCIA, row.OPR_NUMERO };
                    row.BeginEdit();
                    row.DHR_CODIGO = Convert.ToInt32(DB.executeScalar(sqlIDetalle, valorParametros, transaccion));
                    row.EndEdit();
                }

                foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow row in (Data.dsHojaRuta.DETALLE_HOJARUTARow[])dsHojaRuta.DETALLE_HOJARUTA.Select(null, null, DataViewRowState.ModifiedCurrent))
                {
                    valorParametros = new object[] { row.DHR_SECUENCIA, row.DHR_CODIGO };
                    DB.executeNonQuery(sqlUDetalle, valorParametros, transaccion);
                }

                foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow row in (Data.dsHojaRuta.DETALLE_HOJARUTARow[])dsHojaRuta.DETALLE_HOJARUTA.Select(null, null, DataViewRowState.Deleted))
                {
                    valorParametros = new object[] { Convert.ToInt32(row["dhr_codigo", DataRowVersion.Original]) };
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
            string sqlDetalle = "DELETE FROM DETALLE_HOJARUTA WHERE hr_codigo = @p0";
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
        
        public static void ObtenerHojasRuta(object nombre, object estado, Data.dsHojaRuta dsHojaRuta, bool obtenerDetalle)
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

        public static void ObtenerHojaRuta(int codigoHoja, bool detalle, Data.dsHojaRuta dsHojaRuta)
        {
            string sql = @"SELECT hr_codigo, hr_nombre, hr_descripcion, hr_activo, hr_fechaalta FROM HOJAS_RUTA WHERE hr_codigo = @p0";
            object[] valoresParametros = { codigoHoja };
            try
            {
                DB.FillDataTable(dsHojaRuta.HOJAS_RUTA, sql, valoresParametros);
                if (detalle)
                {
                    ObtenerDetalleHoja(codigoHoja, dsHojaRuta);
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        
        private static void ObtenerDetalleHoja(Data.dsHojaRuta ds)
        {
            string sql = "SELECT dhr_codigo, cto_codigo, hr_codigo, dhr_secuencia, opr_numero FROM DETALLE_HOJARUTA WHERE hr_codigo = @p0";
            object[] valorParametros;

            foreach (Data.dsHojaRuta.HOJAS_RUTARow row in ds.HOJAS_RUTA)
            {
                valorParametros = new object[] { row.HR_CODIGO };
                DB.FillDataTable(ds.DETALLE_HOJARUTA, sql, valorParametros);
            }
        }

        private static void ObtenerDetalleHoja(int codigoHoja, Data.dsHojaRuta ds)
        {
            string sql = @"SELECT dhr_codigo, cto_codigo, hr_codigo, dhr_secuencia, opr_numero FROM DETALLE_HOJARUTA 
                            WHERE hr_codigo = @p0 ORDER BY dhr_secuencia ASC";
            object[] valorParametros = { codigoHoja };

            DB.FillDataTable(ds.DETALLE_HOJARUTA, sql, valorParametros);
            foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowDHR in (Data.dsHojaRuta.DETALLE_HOJARUTARow[])ds.DETALLE_HOJARUTA.Select("hr_codigo = " + codigoHoja))
            {
                CentroTrabajoDAL.ObtenerCentroTrabajo(Convert.ToInt32(rowDHR.CTO_CODIGO), true, ds);
                OperacionDAL.ObtenerOperacion(Convert.ToInt32(rowDHR.OPR_NUMERO), ds);
            }
        }
    }
}
