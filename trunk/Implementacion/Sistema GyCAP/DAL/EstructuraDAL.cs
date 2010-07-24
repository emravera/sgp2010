using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class EstructuraDAL
    {
        public static void Insertar(Data.dsEstructura dsEstructura)
        {
            #region SQLs

            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sqlInsertEstructura = @"INSERT INTO [ESTRUCTURAS]
                                        ([estr_nombre]
                                        ,[coc_codigo]
                                        ,[pno_codigo]
                                        ,[estr_descripcion]
                                        ,[estr_activo]
                                        ,[estr_fecha_alta]
                                        ,[estr_fecha_modificacion]
                                        .[e_legajo]) 
                                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7) SELECT @@Identity";

            //obtenemos la estructura nueva del dataset, indicamos la primer fila de las agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.ESTRUCTURASRow rowEstructura = dsEstructura.ESTRUCTURAS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructura.ESTRUCTURASRow;
            object[] valorParametros = { rowEstructura.ESTR_NOMBRE, 
                                         rowEstructura.COC_CODIGO,
                                         rowEstructura.PNO_CODIGO,
                                         rowEstructura.ESTR_DESCRIPCION,
                                         rowEstructura.ESTR_ACTIVO,
                                         rowEstructura.ESTR_FECHA_ALTA,
                                         rowEstructura.ESTR_FECHA_MODIFICACION };

            string sqlInsertGrupo = @"INSERT INTO [GRUPOS_ESTRUCTURA] 
                                    ([grp_numero]
                                    ,[estr_codigo]
                                    ,[grp_padre_codigo]
                                    ,[grp_nombre]
                                    ,[grp_descripcion])
                                    VALUES (@p0, @p1, @p2, @p3, @p4)";

            string sqlInsertConjuntosEstructura = @"INSERT INTO [CONJUNTOSXESTRUCTURA] 
                                                ([estr_codigo]
                                                ,[conj_codigo]
                                                ,[cxe_cantidad]
                                                ,[grp_codigo])
                                                VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            string sqlInsertSubconjuntosEstructura = @"INSERT INTO [SUBCONJUNTOSXESTRUCTURA] 
                                                ([estr_codigo]
                                                ,[sconj_codigo]
                                                ,[scxe_cantidad]
                                                ,[grp_codigo])
                                                VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            string sqlInsertPiezasEstructura = @"INSERT INTO [PIEZASXESTRUCTURA] 
                                                ([estr_codigo]
                                                ,[pza_codigo]
                                                ,[pxe_cantidad]
                                                ,[grp_codigo])
                                                VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            string sqlInsertMateriasPrimasEstructura = @"INSERT INTO [MATERIASPRIMASXESTRUCTURA] 
                                                        ([estr_codigo]
                                                        ,[mp_codigo]
                                                        ,[mpxe_cantidad]
                                                        ,[grp_codigo])
                                                        VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            #endregion

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();
                //Insertamos el conjunto y actualizamos su código en el dataset
                rowEstructura.BeginEdit();
                rowEstructura.ESTR_CODIGO = Convert.ToInt32(DB.executeScalar(sqlInsertEstructura, valorParametros, transaccion));
                rowEstructura.EndEdit();
                //Ahora insertamos su estructura, usamos el foreach para recorrer sólo los nuevos registros del dataset

                #region InsertGrupos

                foreach (Data.dsEstructura.GRUPOS_ESTRUCTURARow row in (Data.dsEstructura.GRUPOS_ESTRUCTURARow[])dsEstructura.GRUPOS_ESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    //Primero actualizamos el código del grupo nuevo en la tabla relacionada
                    row.BeginEdit();
                    row.ESTR_CODIGO = rowEstructura.ESTR_CODIGO;
                    row.EndEdit();
                    //Asignamos los valores a los parámetros
                    valorParametros = new object[] { row.GRP_NUMERO, row.ESTR_CODIGO, row.GRP_PADRE_CODIGO, row.GRP_NOMBRE, row.GRP_DESCRIPCION };
                    //Ahora si insertamos en la bd y actualizamos el código de la relación
                    row.BeginEdit();
                    row.GRP_CODIGO = Convert.ToDecimal(DB.executeScalar(sqlInsertGrupo, valorParametros, transaccion));
                    row.EndEdit();
                }

                #endregion

                #region InsertConjuntos
                foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    row.BeginEdit();
                    row.ESTR_CODIGO = rowEstructura.ESTR_CODIGO;
                    row.EndEdit();
                    valorParametros = new object[] { row.ESTR_CODIGO, row.CONJ_CODIGO, row.CXE_CANTIDAD, row.GRP_CODIGO };
                    row.BeginEdit();
                    row.CXE_CODIGO = Convert.ToDecimal(DB.executeScalar(sqlInsertConjuntosEstructura, valorParametros, transaccion));
                    row.EndEdit();
                }
                #endregion

                #region InsertSubconjuntos
                foreach (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow[])dsEstructura.SUBCONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    row.BeginEdit();
                    row.ESTR_CODIGO = rowEstructura.ESTR_CODIGO;
                    row.EndEdit();
                    valorParametros = new object[] { row.ESTR_CODIGO, row.SCONJ_CODIGO, row.SCXE_CANTIDAD, row.GRP_CODIGO };
                    row.BeginEdit();
                    row.SCXE_CODIGO = Convert.ToDecimal(DB.executeScalar(sqlInsertSubconjuntosEstructura, valorParametros, transaccion));
                    row.EndEdit();
                }
                #endregion

                #region InsertPiezas
                foreach (Data.dsEstructura.PIEZASXESTRUCTURARow row in (Data.dsEstructura.PIEZASXESTRUCTURARow[])dsEstructura.PIEZASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    row.BeginEdit();
                    row.ESTR_CODIGO = rowEstructura.ESTR_CODIGO;
                    row.EndEdit();
                    valorParametros = new object[] { row.ESTR_CODIGO, row.PZA_CODIGO, row.PXE_CANTIDAD, row.GRP_CODIGO };
                    row.BeginEdit();
                    row.PXE_CODIGO = Convert.ToDecimal(DB.executeScalar(sqlInsertPiezasEstructura, valorParametros, transaccion));
                    row.EndEdit();
                }
                #endregion

                #region InsertMateriaPrima
                foreach (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow row in (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow[])dsEstructura.MATERIASPRIMASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    row.BeginEdit();
                    row.ESTR_CODIGO = rowEstructura.ESTR_CODIGO;
                    row.EndEdit();
                    valorParametros = new object[] { row.ESTR_CODIGO, row.MP_CODIGO, row.MPXE_CANTIDAD, row.GRP_CODIGO };
                    row.BeginEdit();
                    row.MPXE_CODIGO = Convert.ToDecimal(DB.executeScalar(sqlInsertMateriasPrimasEstructura, valorParametros, transaccion));
                    row.EndEdit();
                }

                #endregion
                
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
        
        public static void Eliminar(int codigoEstructura)
        {
            string sql = "DELETE FROM ESTRUCTURAS WHERE estr_codigo = @p0";
            object[] valorParametros = { codigoEstructura };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Data.dsEstructura dsEstructura)
        {
            #region SqlEstructura
            string sqlUpdateEstructura = @"UPDATE ESTRUCTURAS SET
                                         coc_codigo = @p0
                                        ,pno_codigo = @p1
                                        ,estr_nombre = @p2
                                        ,estr_descripcion = @p3
                                        ,estr_activo = @p4
                                        ,estr_fecha_alta = @p5
                                        ,estr_fecha_modificacion = @p6
                                        WHERE estr_codigo = @p7";

            Data.dsEstructura.ESTRUCTURASRow rowEstructura = dsEstructura.ESTRUCTURAS.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsEstructura.ESTRUCTURASRow;
            object[] valorParametros = { rowEstructura.COC_CODIGO,
                                         rowEstructura.PNO_CODIGO,
                                         rowEstructura.ESTR_NOMBRE,
                                         rowEstructura.ESTR_DESCRIPCION,
                                         rowEstructura.ESTR_ACTIVO,
                                         rowEstructura.ESTR_FECHA_ALTA, 
                                         rowEstructura.ESTR_FECHA_MODIFICACION };

            #endregion

            #region SqlGrupo

            /*string sqlInsertGrupo = @"INSERT INTO [GRUPOS_ESTRUCTURA] 
                                    ([grp_numero]
                                    ,[estr_codigo]
                                    ,[grp_padre_codigo]
                                    ,[grp_nombre]
                                    ,[grp_descripcion])
                                    VALUES (@p0, @p1, @p2, @p3, @p4)";

            string sqlDeleteGrupo = "DELETE FROM GRUPOS_ESTRUCTURA WHERE grp_codigo = @p0";

            string sqlUpdateGrupo = @"UPDATE GRUPOS_ESTRUCTURA SET ";*/

            #endregion

            #region SqlConjuntos

            string sqlInsertConjuntosEstructura = @"INSERT INTO [CONJUNTOSXESTRUCTURA] 
                                                ([estr_codigo]
                                                ,[conj_codigo]
                                                ,[cxe_cantidad]
                                                ,[grp_codigo])
                                                VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            string sqlUpdateConjuntosEstructura = @"UPDATE CONJUNTOSXESTRUCTURA SET cxe_cantidad = @p0 WHERE cxe_codigo = @p1";
            string sqlDeleteConjuntosEstructura = "DELETE FROM CONJUNTOSXESTRUCTURA WHERE cxe_codigo = @p0";

            #endregion

            #region sqlSubconjuntos
            string sqlInsertSubconjuntosEstructura = @"INSERT INTO [SUBCONJUNTOSXESTRUCTURA] 
                                                ([estr_codigo]
                                                ,[sconj_codigo]
                                                ,[scxe_cantidad]
                                                ,[grp_codigo])
                                                VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            string sqlUpdateSubconjuntosEstructura = @"UPDATE SUBCONJUNTOSXESTRUCTURA SET scxe_cantidad = @p0 WHERE scxe_codigo = @p1";
            string sqlDeleteSubconjuntosEstructura = "DELETE FROM SUBCONJUNTOSXESTRUCTURA WHERE scxe_codigo = @p0";
            #endregion

            #region sqlPiezas
            string sqlInsertPiezasEstructura = @"INSERT INTO [PIEZASXESTRUCTURA] 
                                                ([estr_codigo]
                                                ,[pza_codigo]
                                                ,[pxe_cantidad]
                                                ,[grp_codigo])
                                                VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            string sqlUpdatePiezasEstructura = @"UPDATE PIEZASXESTRUCTURA SET pxe_cantidad = @p0 WHERE pxe_codigo = @p1";
            string sqlDeletePiezasEstructura = "DELETE FROM PIEZASXESTRUCTURA WHERE pxe_codigo = @p0";
            #endregion

            #region sqlMateria Prima
            string sqlInsertMateriasPrimasEstructura = @"INSERT INTO [MATERIASPRIMASXESTRUCTURA] 
                                                        ([estr_codigo]
                                                        ,[mp_codigo]
                                                        ,[mpxe_cantidad]
                                                        ,[grp_codigo])
                                                        VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            string sqlUpdateMateriasPrimasEstructura = @"UPDATE MATERIASPRIMASXESTRUCTURA SET mpxe_cantidad = @p0 WHERE mpxe_codigo = @p1";
            string sqlDeleteMateriasPrimasEstructura = "DELETE FROM MATERIASPRIMASXESTRUCTURA WHERE mpxe_codigo = @p0";

            #endregion

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Actualizamos la estructura
                DB.executeNonQuery(sqlUpdateEstructura, valorParametros, transaccion);
                //Actualizamos el resto, primero insertamos los nuevos

                #region Inserts

                foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    valorParametros = new object[] { row.ESTR_CODIGO, row.CONJ_CODIGO, row.CXE_CANTIDAD, row.GRP_CODIGO };
                    row.BeginEdit();
                    row.CXE_CODIGO = Convert.ToDecimal(DB.executeScalar(sqlInsertConjuntosEstructura, valorParametros, transaccion));
                    row.EndEdit();
                }

                foreach (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow[])dsEstructura.SUBCONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    valorParametros = new object[] { row.ESTR_CODIGO, row.SCONJ_CODIGO, row.SCXE_CANTIDAD, row.GRP_CODIGO };
                    row.BeginEdit();
                    row.SCXE_CODIGO = Convert.ToDecimal(DB.executeScalar(sqlInsertSubconjuntosEstructura, valorParametros, transaccion));
                    row.EndEdit();
                }

                foreach (Data.dsEstructura.PIEZASXESTRUCTURARow row in (Data.dsEstructura.PIEZASXESTRUCTURARow[])dsEstructura.PIEZASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    valorParametros = new object[] { row.ESTR_CODIGO, row.PZA_CODIGO, row.PXE_CANTIDAD, row.GRP_CODIGO };
                    row.BeginEdit();
                    row.PXE_CODIGO = Convert.ToDecimal(DB.executeScalar(sqlInsertPiezasEstructura, valorParametros, transaccion));
                    row.EndEdit();
                }

                foreach (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow row in (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow[])dsEstructura.MATERIASPRIMASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    valorParametros = new object[] { row.ESTR_CODIGO, row.MP_CODIGO, row.MPXE_CANTIDAD, row.GRP_CODIGO };
                    row.BeginEdit();
                    row.MPXE_CODIGO = Convert.ToDecimal(DB.executeScalar(sqlInsertMateriasPrimasEstructura, valorParametros, transaccion));
                    row.EndEdit();
                }

                #endregion

                #region Updates
                
                foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    valorParametros = new object[] { row.CXE_CANTIDAD, row.CXE_CODIGO };
                    DB.executeScalar(sqlUpdateConjuntosEstructura, valorParametros, transaccion);
                }

                foreach (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    valorParametros = new object[] { row.SCXE_CANTIDAD, row.SCXE_CODIGO };
                    DB.executeScalar(sqlUpdateSubconjuntosEstructura, valorParametros, transaccion);
                }

                foreach (Data.dsEstructura.PIEZASXESTRUCTURARow row in (Data.dsEstructura.PIEZASXESTRUCTURARow[])dsEstructura.PIEZASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    valorParametros = new object[] { row.PXE_CANTIDAD, row.PXE_CODIGO };
                    DB.executeScalar(sqlUpdatePiezasEstructura, valorParametros, transaccion);
                }

                foreach (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow row in (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow[])dsEstructura.MATERIASPRIMASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    valorParametros = new object[] { row.MPXE_CANTIDAD, row.MPXE_CODIGO };
                    DB.executeScalar(sqlUpdateMateriasPrimasEstructura, valorParametros, transaccion);
                }

                #endregion

                #region Deletes
               
                foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    valorParametros = new object[] { row["cxe_codigo", System.Data.DataRowVersion.Original] };
                    Convert.ToDecimal(DB.executeScalar(sqlDeleteConjuntosEstructura, valorParametros, transaccion));
                }

                foreach (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow[])dsEstructura.SUBCONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    valorParametros = new object[] { row["scxe_codigo", System.Data.DataRowVersion.Original] };
                    Convert.ToDecimal(DB.executeScalar(sqlDeleteSubconjuntosEstructura, valorParametros, transaccion));
                }
                foreach (Data.dsEstructura.PIEZASXESTRUCTURARow row in (Data.dsEstructura.PIEZASXESTRUCTURARow[])dsEstructura.PIEZASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    valorParametros = new object[] { row["pxe_codigo", System.Data.DataRowVersion.Original] };
                    Convert.ToDecimal(DB.executeScalar(sqlDeletePiezasEstructura, valorParametros, transaccion));
                }
                foreach (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow row in (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow[])dsEstructura.MATERIASPRIMASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    valorParametros = new object[] { row["mpxe_codigo", System.Data.DataRowVersion.Original] };
                    Convert.ToDecimal(DB.executeScalar(sqlDeleteMateriasPrimasEstructura, valorParametros, transaccion));
                }

                #endregion

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
        
        public static void ObtenerEstructuras(object nombre, object codPlano, object fechaCreacion, object codCocina, object codResponsable, object activoSiNo, Data.dsEstructura ds)
        {
            string sql = @"SELECT estr_codigo, estr_nombre, coc_codigo, pno_codigo, estr_descripcion, estr_activo, estr_fecha_alta, estr_fecha_modificacion, e_codigo 
                          FROM ESTRUCTURAS WHERE 1=1";

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
                valoresFiltros[cantidadParametros] = fechaCreacion.ToString();
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
                DB.FillDataSet(ds, "ESTRUCTURAS", sql, valorParametros);
            }
            else
            {
                //Buscamos sin filtro
                DB.FillDataSet(ds, "ESTRUCTURAS", sql, null);
            }
        }
        
        public static bool PuedeEliminarse(int codigoEstructura)
        {
            return false;
        }
    }
}
