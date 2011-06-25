using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class EstructuraDAL
    {
        public static readonly int EstructuraActiva = 1;
        public static readonly int EstructuraInactiva = 0;
        
        public static decimal Insertar(Data.dsEstructuraProducto dsEstructura)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sqlInsert = @"INSERT INTO [ESTRUCTURAS]
                               ([estr_nombre]
                               ,[coc_codigo]
                               ,[pno_codigo]
                               ,[estr_descripcion]
                               ,[estr_activo]
                               ,[estr_fecha_alta]
                               ,[estr_fecha_modificacion]
                               ,[e_codigo]
                               ,[estr_costo]) 
                               VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8) SELECT @@Identity";

            //obtenemos la estructura nueva del dataset, indicamos la primer fila de las agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructuraProducto.ESTRUCTURASRow rowEstructura = dsEstructura.ESTRUCTURAS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructuraProducto.ESTRUCTURASRow;
            //Controlemos los valores que pueden venir nulos
            object fechaModificacion = DBNull.Value, responsable = DBNull.Value;
            if (!rowEstructura.IsESTR_FECHA_MODIFICACIONNull()) { fechaModificacion = rowEstructura.ESTR_FECHA_MODIFICACION.Date; }
            if (!rowEstructura.IsE_CODIGONull()) { responsable = rowEstructura.E_CODIGO; }            
            
            object[] valorParametros = { rowEstructura.ESTR_NOMBRE, 
                                         rowEstructura.COC_CODIGO,
                                         rowEstructura.PNO_CODIGO,
                                         rowEstructura.ESTR_DESCRIPCION,
                                         rowEstructura.ESTR_ACTIVO,
                                         rowEstructura.ESTR_FECHA_ALTA.Date,
                                         fechaModificacion,
                                         responsable,
                                         rowEstructura.ESTR_COSTO };

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();
                //Insertamos el conjunto y actualizamos su código en el dataset
                rowEstructura.BeginEdit();
                rowEstructura.ESTR_CODIGO = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));
                rowEstructura.EndEdit();
                //Ahora insertamos su estructura, usamos el foreach para recorrer sólo los nuevos registros del dataset
                foreach (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompuesto in (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])dsEstructura.COMPUESTOS_PARTES.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    rowCompuesto.BeginEdit();
                    rowCompuesto.ESTR_CODIGO = rowEstructura.ESTR_CODIGO;
                    rowCompuesto.EndEdit();
                    CompuestoParteDAL.Insertar(rowCompuesto, transaccion);
                }

                //Todo ok, commit
                transaccion.Commit();
                return rowEstructura.ESTR_CODIGO;
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
        
        public static void Eliminar(int codigoEstructura)
        {
            string sql = "DELETE FROM ESTRUCTURAS WHERE estr_codigo = @p0";
            object[] valorParametros = { codigoEstructura };
            
            SqlTransaction transaccion = null;
            try
            {
                transaccion = DB.IniciarTransaccion();
                //Primero los hijos
                CompuestoParteDAL.EliminarCompuestosDeEstructura(codigoEstructura, transaccion);
                //Ahora el padre
                DB.executeNonQuery(sql, valorParametros, transaccion);
                transaccion.Commit();
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            finally { DB.FinalizarTransaccion(); }
        }

        public static void Actualizar(Data.dsEstructuraProducto dsEstructura)
        {
           string sql = @"UPDATE ESTRUCTURAS SET
                         coc_codigo = @p0
                        ,pno_codigo = @p1
                        ,estr_nombre = @p2
                        ,estr_descripcion = @p3
                        ,estr_activo = @p4
                        ,estr_fecha_alta = @p5
                        ,estr_fecha_modificacion = @p6
                        ,e_codigo = @p7
                        ,estr_costo = @p8
                        WHERE estr_codigo = @p9";

           Data.dsEstructuraProducto.ESTRUCTURASRow rowEstructura = dsEstructura.ESTRUCTURAS.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsEstructuraProducto.ESTRUCTURASRow;
           //Controlemos los valores que pueden venir nulos
           object fechaModificacion = DBNull.Value, responsable = DBNull.Value;
           if (!rowEstructura.IsESTR_FECHA_MODIFICACIONNull()) { fechaModificacion = rowEstructura.ESTR_FECHA_MODIFICACION.Date; }
           if (!rowEstructura.IsE_CODIGONull()) { responsable = rowEstructura.E_CODIGO; }
            
           object[] valorParametros = { rowEstructura.COC_CODIGO,
                                        rowEstructura.PNO_CODIGO,
                                        rowEstructura.ESTR_NOMBRE,
                                        rowEstructura.ESTR_DESCRIPCION,
                                        rowEstructura.ESTR_ACTIVO,
                                        rowEstructura.ESTR_FECHA_ALTA.Date, 
                                        fechaModificacion,
                                        responsable,
                                        rowEstructura.ESTR_COSTO,
                                        rowEstructura.ESTR_CODIGO };

           //Declaramos el objeto transaccion
           SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Actualizamos la estructura
                DB.executeNonQuery(sql, valorParametros, transaccion);
                //Actualizamos el resto, primero insertamos los nuevos, luego los updates y por último los deletes

                foreach (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompuesto in (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])dsEstructura.COMPUESTOS_PARTES.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    CompuestoParteDAL.Insertar(rowCompuesto, transaccion);
                }

                foreach (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompuesto in (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])dsEstructura.COMPUESTOS_PARTES.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    CompuestoParteDAL.Actualizar(rowCompuesto, transaccion);
                }

                foreach (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompuesto in (Data.dsEstructuraProducto.COMPUESTOS_PARTESRow[])dsEstructura.COMPUESTOS_PARTES.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    int codCompuesto = Convert.ToInt32(rowCompuesto["comp_codigo", System.Data.DataRowVersion.Original]);
                    CompuestoParteDAL.Eliminar(codCompuesto, transaccion);
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
        
        public static void ObtenerEstructuras(object nombre, object codPlano, object fechaCreacion, object codCocina, object codResponsable, object activoSiNo, Data.dsEstructuraProducto ds)
        {
            string sql = @"SELECT estr_codigo, estr_nombre, coc_codigo, pno_codigo, estr_descripcion, estr_activo, estr_fecha_alta, 
                           estr_fecha_modificacion, e_codigo, estr_costo FROM ESTRUCTURAS WHERE 1=1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[6];
            //Empecemos a armar la consulta, revisemos que filtros aplican - NOMBRE
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND estr_nombre LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }
            
            //PLANO - Revisamos si pasó algun valor y si es un integer
            if (codPlano != null && codPlano.GetType() == cantidadParametros.GetType())
            {
                sql += " AND pno_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(codPlano);
                cantidadParametros++;
            }
            
            //FECHA CREACION
            if (fechaCreacion != null && fechaCreacion.GetType() == DateTime.Today.GetType())
            {
                sql += " AND estr_fecha_alta = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = ((DateTime)fechaCreacion).Date;
                cantidadParametros++;
            }

            //COCINA
            if (codCocina != null && codCocina.GetType() == cantidadParametros.GetType())
            {
                sql += " AND coc_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(codCocina);
                cantidadParametros++;
            }

            //RESPONSABLE
            if (codResponsable != null && codResponsable.GetType() == cantidadParametros.GetType())
            {
                sql += " AND e_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(codResponsable);
                cantidadParametros++;
            }

            //ACTIVO
            if (activoSiNo != null && activoSiNo.ToString() != string.Empty)
            {
                if (activoSiNo.ToString().ToLower().CompareTo("si") == 0 || activoSiNo.ToString().CompareTo("1") == 0) { activoSiNo = 1; }
                else if (activoSiNo.ToString().ToLower().CompareTo("no") == 0 || activoSiNo.ToString().CompareTo("0") == 0) { activoSiNo = 0; }
                else { activoSiNo = 0; }
                sql += " AND estr_activo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(activoSiNo);
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
                    DB.FillDataSet(ds, "ESTRUCTURAS", sql, valorParametros);
                    ObtenerDetalleEstructura(ds);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
            else
            {
                try
                {
                    //Buscamos sin filtro
                    DB.FillDataSet(ds, "ESTRUCTURAS", sql, null);
                    ObtenerDetalleEstructura(ds);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
        }
        
        public static bool PuedeEliminarse(int codigoEstructura)
        {
            if (EsEstructuraActiva(codigoEstructura)) { return false; }
            string sql1 = "SELECT count(ordp_numero) FROM ORDENES_PRODUCCION WHERE estr_codigo = @p0";
            string sql2 = "SELECT count(ordpm_numero) FROM ORDENES_PRODUCCION_MANUAL WHERE estr_codigo = @p0";
            object[] valorParametros = { codigoEstructura };

            try
            {
                int r1 = Convert.ToInt32(DB.executeScalar(sql1, valorParametros, null));
                int r2 = Convert.ToInt32(DB.executeScalar(sql2, valorParametros, null));
                
                if (r1 + r2 == 0) { return true; }
                else { return false; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool EsEstructuraActiva(int codigoEstructura)
        {
            string sql = "SELECT count(estr_codigo) FROM ESTRUCTURAS WHERE estr_codigo = @p0 AND estr_activo = @p1";
            object[] valorParametros = { codigoEstructura, EstructuraActiva };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0) { return false; }
                else { return true; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        private static void ObtenerDetalleEstructura(Data.dsEstructuraProducto dsEstructura)
        {
            int[] codigosEstructuras = new int[dsEstructura.ESTRUCTURAS.Rows.Count];
            int i = 0;
            foreach (Data.dsEstructuraProducto.ESTRUCTURASRow row in dsEstructura.ESTRUCTURAS)
            {
                codigosEstructuras[i] = Convert.ToInt32(row.ESTR_CODIGO);
                i++;
            }

            try
            {
                DAL.CompuestoParteDAL.ObtenerCompuestosPartesEstructura(codigosEstructuras, dsEstructura.COMPUESTOS_PARTES);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            
        }

        private static void ObtenerDetalleEstructura(int codigoEstructura, Data.dsEstructura dsEstructura)
        { 
            int[] codigosEstructuras = { codigoEstructura };

            try
            {
                ConjuntoEstructuraDAL.ObtenerConjuntosEstructura(codigosEstructuras, dsEstructura);

                foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow rowCxE in (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select("estr_codigo = " + codigoEstructura))
                {
                    ConjuntoDAL.ObtenerConjunto(Convert.ToInt32(rowCxE.CONJ_CODIGO), true, dsEstructura);
                }

                PiezaEstructuraDAL.ObtenerPiezasEstructura(codigosEstructuras, dsEstructura);

                foreach (Data.dsEstructura.PIEZASXESTRUCTURARow rowPxE in (Data.dsEstructura.PIEZASXESTRUCTURARow[])dsEstructura.PIEZASXESTRUCTURA.Select("estr_codigo = " + codigoEstructura))
                {
                    PiezaDAL.ObtenerPieza(Convert.ToInt32(rowPxE.PZA_CODIGO), true, dsEstructura);
                }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }

        }

        public static void ObtenerEstructura(int codigoEstructura, Data.dsEstructura ds, bool detalle)
        {
            string sql = @"SELECT estr_codigo, estr_nombre, coc_codigo, pno_codigo, estr_descripcion, estr_activo, 
                          estr_fecha_alta, estr_fecha_modificacion, e_codigo, estr_costo   
                          FROM ESTRUCTURAS WHERE estr_codigo = @p0";

            object[] valorParametros = { codigoEstructura };
            
            try
            {
                DB.FillDataSet(ds, "ESTRUCTURAS", sql, valorParametros);
                if (detalle)
                {                    
                    ObtenerDetalleEstructura(codigoEstructura, ds);
                }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
