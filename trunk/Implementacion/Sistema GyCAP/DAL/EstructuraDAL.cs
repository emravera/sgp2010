using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class EstructuraDAL
    {
        public static readonly int EstructuraActiva = 1;
        public static readonly int EstructuraInactiva = 0;
        
        public static void Insertar(Data.dsEstructura dsEstructura)
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
                               ,[estr_costo]
                               ,[estr_costofijo]) 
                               VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9) SELECT @@Identity";

            //obtenemos la estructura nueva del dataset, indicamos la primer fila de las agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsEstructura.ESTRUCTURASRow rowEstructura = dsEstructura.ESTRUCTURAS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructura.ESTRUCTURASRow;
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
                                         rowEstructura.E_CODIGO,
                                         rowEstructura.ESTR_COSTO,
                                         rowEstructura.ESTR_COSTOFIJO };

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

                #region InsertGrupos
                Entidades.GrupoEstructura grupoE = new GyCAP.Entidades.GrupoEstructura();
                foreach (Data.dsEstructura.GRUPOS_ESTRUCTURARow row in (Data.dsEstructura.GRUPOS_ESTRUCTURARow[])dsEstructura.GRUPOS_ESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    grupoE.CodigoEstructura = Convert.ToInt32(rowEstructura.ESTR_CODIGO);
                    grupoE.Numero = Convert.ToInt32(row.GRP_NUMERO);
                    grupoE.NombreGrupo = row.GRP_NOMBRE;
                    if (row.IsGRP_PADRE_CODIGONull()) { grupoE.CodigoPadre = -1; }
                    else { grupoE.CodigoPadre = Convert.ToInt32(row.GRP_PADRE_CODIGO); }
                    grupoE.Descripcion = row.GRP_DESCRIPCION;
                    grupoE.Concreto = Convert.ToInt32(row.GRP_CONCRETO);
                    GrupoEstructuraDAL.Insertar(grupoE, transaccion);
                    row.BeginEdit();
                    row.GRP_CODIGO = grupoE.CodigoGrupo;
                    row.EndEdit();
                }
                #endregion

                #region InsertConjuntos
                Entidades.ConjuntoEstructura conjuntoE = new GyCAP.Entidades.ConjuntoEstructura();
                foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    conjuntoE.CodigoEstructura = Convert.ToInt32(rowEstructura.ESTR_CODIGO);
                    conjuntoE.CodigoConjunto = Convert.ToInt32(row.CONJ_CODIGO);
                    conjuntoE.CantidadConjunto = Convert.ToInt32(row.CXE_CANTIDAD);
                    conjuntoE.CodigoGrupo = grupoE.CodigoGrupo;
                    DAL.ConjuntoEstructuraDAL.Insertar(conjuntoE, transaccion);                  
                    row.BeginEdit();
                    row.CXE_CODIGO = conjuntoE.CodigoDetalle;
                    row.GRP_CODIGO = conjuntoE.CodigoGrupo;
                    row.EndEdit();
                }
                #endregion

                #region InsertSubconjuntos -Desactivado en IT2-
                /*Entidades.SubconjuntoEstructura subconjuntoE = new GyCAP.Entidades.SubconjuntoEstructura();
                foreach (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow[])dsEstructura.SUBCONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    subconjuntoE.CodigoEstructura = Convert.ToInt32(rowEstructura.ESTR_CODIGO);
                    subconjuntoE.CodigoSubconjunto = Convert.ToInt32(row.SCONJ_CODIGO);
                    subconjuntoE.CantidadSubconjunto = Convert.ToInt32(row.SCXE_CANTIDAD);                    
                    subconjuntoE.CodigoGrupo = grupoE.CodigoGrupo;;
                    SubconjuntoEstructuraDAL.Insertar(subconjuntoE, transaccion);
                    row.BeginEdit();
                    row.SCXE_CODIGO = subconjuntoE.CodigoDetalle;
                    row.GRP_CODIGO = subconjuntoE.CodigoGrupo;
                    row.EndEdit();
                }*/
                #endregion

                #region InsertPiezas
                Entidades.PiezaEstructura piezaE = new GyCAP.Entidades.PiezaEstructura();
                foreach (Data.dsEstructura.PIEZASXESTRUCTURARow row in (Data.dsEstructura.PIEZASXESTRUCTURARow[])dsEstructura.PIEZASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    piezaE.CodigoEstructura = Convert.ToInt32(rowEstructura.ESTR_CODIGO);
                    piezaE.CodigoPieza = Convert.ToInt32(row.PZA_CODIGO);
                    piezaE.CantidadPieza = Convert.ToInt32(row.PXE_CANTIDAD);
                    piezaE.CodigoGrupo = grupoE.CodigoGrupo;
                    PiezaEstructuraDAL.Insertar(piezaE, transaccion);
                    row.BeginEdit();
                    row.PXE_CODIGO = piezaE.CodigoDetalle;
                    row.GRP_CODIGO = piezaE.CodigoGrupo;
                    row.EndEdit();
                }
                #endregion

                #region InsertMateriaPrima -Desactivado en IT2-
                /*Entidades.MateriaPrimaEstructura mpE = new GyCAP.Entidades.MateriaPrimaEstructura();
                foreach (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow row in (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow[])dsEstructura.MATERIASPRIMASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    mpE.CodigoEstructura = Convert.ToInt32(rowEstructura.ESTR_CODIGO);
                    mpE.CodigoMateriaPrima = Convert.ToInt32(row.MP_CODIGO);
                    mpE.CantidadMateriaPrima = row.MPXE_CANTIDAD;
                    mpE.CodigoGrupo = grupoE.CodigoGrupo;
                    MateriaPrimaEstructuraDAL.Insertar(mpE, transaccion);
                    row.BeginEdit();
                    row.MPXE_CODIGO = mpE.CodigoDetalle;
                    row.GRP_CODIGO = mpE.CodigoGrupo;
                    row.EndEdit();
                }*/

                #endregion
                
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
        
        public static void Eliminar(int codigoEstructura)
        {
            string sql = "DELETE FROM ESTRUCTURAS WHERE estr_codigo = @p0";
            object[] valorParametros = { codigoEstructura };
            
            SqlTransaction transaccion = null;
            try
            {
                transaccion = DB.IniciarTransaccion();
                //Primero los hijos
                //MateriaPrimaEstructuraDAL.DeleteDetalleEstructura(codigoEstructura, transaccion); desactivado IT2
                PiezaEstructuraDAL.DeleteDetalleEstructura(codigoEstructura, transaccion);
                //SubconjuntoEstructuraDAL.DeleteDetalleEstructura(codigoEstructura, transaccion); desactivado IT2
                ConjuntoEstructuraDAL.DeleteDetalleEstructura(codigoEstructura, transaccion);
                GrupoEstructuraDAL.DeleteGruposEstructura(codigoEstructura, transaccion);
                //Ahora el padre
                DB.executeNonQuery(sql, valorParametros, transaccion);
            }
            catch(SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Data.dsEstructura dsEstructura)
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
                        ,estr_costofijo = @p9
                        WHERE estr_codigo = @p10";

           Data.dsEstructura.ESTRUCTURASRow rowEstructura = dsEstructura.ESTRUCTURAS.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsEstructura.ESTRUCTURASRow;
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
                                        rowEstructura.ESTR_COSTOFIJO,
                                        rowEstructura.ESTR_CODIGO };

           //Declaramos el objeto transaccion
           SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Actualizamos la estructura
                DB.executeNonQuery(sql, valorParametros, transaccion);
                //Actualizamos el resto, primero insertamos los nuevos, luego los updates y por último los deletes - Falta grupos - gonzalo

                #region Inserts
                Entidades.GrupoEstructura gE = new GyCAP.Entidades.GrupoEstructura();
                foreach (Data.dsEstructura.GRUPOS_ESTRUCTURARow row in (Data.dsEstructura.GRUPOS_ESTRUCTURARow[])dsEstructura.GRUPOS_ESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    gE.CodigoGrupo = Convert.ToInt32(row.GRP_CODIGO);
                    gE.CodigoEstructura = Convert.ToInt32(row.ESTR_CODIGO);
                    gE.CodigoPadre = Convert.ToInt32(row.GRP_PADRE_CODIGO);
                    gE.Numero = Convert.ToInt32(row.GRP_NUMERO);
                    gE.NombreGrupo = row.GRP_NOMBRE;
                    gE.Descripcion = row.GRP_DESCRIPCION;
                    gE.Concreto = Convert.ToInt32(row.GRP_CONCRETO);
                    GrupoEstructuraDAL.Insertar(gE, transaccion);
                    row.GRP_CODIGO = gE.CodigoGrupo;
                }
                Entidades.ConjuntoEstructura cE = new GyCAP.Entidades.ConjuntoEstructura();
                foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    cE.CodigoEstructura = Convert.ToInt32(row.ESTR_CODIGO);
                    cE.CodigoConjunto = Convert.ToInt32(row.CONJ_CODIGO);
                    cE.CantidadConjunto = Convert.ToInt32(row.CXE_CANTIDAD);
                    if (gE.CodigoGrupo != 0) { cE.CodigoGrupo = gE.CodigoGrupo; }
                    else { cE.CodigoGrupo = Convert.ToInt32(row.GRP_CODIGO); }
                    ConjuntoEstructuraDAL.Insertar(cE, transaccion);
                    row.BeginEdit();
                    row.CXE_CODIGO = cE.CodigoDetalle;
                    if (gE.CodigoGrupo != 0) { row.GRP_CODIGO = cE.CodigoGrupo; }
                    row.EndEdit();
                }

                #region SC Desactivado en IT2
                /*Entidades.SubconjuntoEstructura scE = new GyCAP.Entidades.SubconjuntoEstructura();
                foreach (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow[])dsEstructura.SUBCONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    scE.CodigoEstructura = Convert.ToInt32(row.ESTR_CODIGO);
                    scE.CodigoSubconjunto = Convert.ToInt32(row.SCONJ_CODIGO);
                    scE.CantidadSubconjunto = Convert.ToInt32(row.SCXE_CANTIDAD);
                    if (gE.CodigoGrupo != 0) { scE.CodigoGrupo = gE.CodigoGrupo; }
                    else { scE.CodigoGrupo = Convert.ToInt32(row.GRP_CODIGO); }
                    SubconjuntoEstructuraDAL.Insertar(scE, transaccion);
                    row.BeginEdit();
                    row.SCXE_CODIGO = scE.CodigoDetalle;
                    if (gE.CodigoGrupo != 0) { row.GRP_CODIGO = scE.CodigoGrupo; }
                    row.EndEdit();
                }*/
                #endregion

                Entidades.PiezaEstructura pE = new GyCAP.Entidades.PiezaEstructura();
                foreach (Data.dsEstructura.PIEZASXESTRUCTURARow row in (Data.dsEstructura.PIEZASXESTRUCTURARow[])dsEstructura.PIEZASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    pE.CodigoEstructura = Convert.ToInt32(row.ESTR_CODIGO);
                    pE.CodigoPieza = Convert.ToInt32(row.PZA_CODIGO);
                    pE.CantidadPieza = Convert.ToInt32(row.PXE_CANTIDAD);
                    if (gE.CodigoGrupo != 0) { pE.CodigoGrupo = gE.CodigoGrupo; }
                    else { pE.CodigoGrupo = Convert.ToInt32(row.GRP_CODIGO); }
                    PiezaEstructuraDAL.Insertar(pE, transaccion);
                    row.BeginEdit();
                    row.PXE_CODIGO = pE.CodigoDetalle;
                    if (gE.CodigoGrupo != 0) { row.GRP_CODIGO = pE.CodigoGrupo; }
                    row.EndEdit();
                }

                #region MP Desactivado en IT2
                /*Entidades.MateriaPrimaEstructura mpE = new GyCAP.Entidades.MateriaPrimaEstructura();
                foreach (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow row in (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow[])dsEstructura.MATERIASPRIMASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    mpE.CodigoEstructura = Convert.ToInt32(row.ESTR_CODIGO);
                    mpE.CodigoMateriaPrima = Convert.ToInt32(row.MP_CODIGO);
                    mpE.CantidadMateriaPrima = row.MPXE_CANTIDAD;
                    if (gE.CodigoGrupo != 0) { mpE.CodigoGrupo = gE.CodigoGrupo; }
                    else { mpE.CodigoGrupo = Convert.ToInt32(row.GRP_CODIGO); }
                    MateriaPrimaEstructuraDAL.Insertar(mpE, transaccion);
                    row.BeginEdit();
                    row.MPXE_CODIGO = mpE.CodigoDetalle;
                    if (gE.CodigoGrupo != 0) { row.GRP_CODIGO = mpE.CodigoGrupo; }
                    row.EndEdit();
                }*/
                #endregion
                
                #endregion

                #region Updates

                foreach (Data.dsEstructura.GRUPOS_ESTRUCTURARow row in (Data.dsEstructura.GRUPOS_ESTRUCTURARow[])dsEstructura.GRUPOS_ESTRUCTURA.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    gE.CodigoGrupo = Convert.ToInt32(row.GRP_CODIGO);
                    gE.CodigoEstructura = Convert.ToInt32(row.ESTR_CODIGO);
                    gE.CodigoPadre = Convert.ToInt32(row.GRP_PADRE_CODIGO);
                    gE.Numero = Convert.ToInt32(row.GRP_NUMERO);
                    gE.NombreGrupo = row.GRP_NOMBRE;
                    gE.Descripcion = row.GRP_DESCRIPCION;
                    gE.Concreto = Convert.ToInt32(row.GRP_CONCRETO);
                    GrupoEstructuraDAL.Actualizar(gE, transaccion);
                }
                
                foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    cE.CodigoDetalle = Convert.ToInt32(row.CXE_CODIGO);
                    cE.CantidadConjunto = Convert.ToInt32(row.CXE_CANTIDAD);
                    ConjuntoEstructuraDAL.Actualizar(cE, transaccion);
                }

                #region SC Desactivado en IT2
                /*foreach (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow[])dsEstructura.SUBCONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    scE.CodigoDetalle = Convert.ToInt32(row.SCXE_CODIGO);
                    scE.CantidadSubconjunto = Convert.ToInt32(row.SCXE_CANTIDAD);
                    SubconjuntoEstructuraDAL.Actualizar(scE, transaccion);
                }*/
                #endregion

                foreach (Data.dsEstructura.PIEZASXESTRUCTURARow row in (Data.dsEstructura.PIEZASXESTRUCTURARow[])dsEstructura.PIEZASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    pE.CodigoDetalle = Convert.ToInt32(row.PXE_CODIGO);
                    pE.CantidadPieza = Convert.ToInt32(row.PXE_CANTIDAD);
                    PiezaEstructuraDAL.Actualizar(pE, transaccion);
                }

                #region MP desactivado en IT2
                /*foreach (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow row in (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow[])dsEstructura.MATERIASPRIMASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    mpE.CodigoDetalle = Convert.ToInt32(row.MPXE_CODIGO);
                    mpE.CantidadMateriaPrima = row.MPXE_CANTIDAD;
                    MateriaPrimaEstructuraDAL.Actualizar(mpE, transaccion);
                }*/
                #endregion

                #endregion

                #region Deletes

                foreach (Data.dsEstructura.CONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.CONJUNTOSXESTRUCTURARow[])dsEstructura.CONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    cE.CodigoDetalle = Convert.ToInt32(row["cxe_codigo", System.Data.DataRowVersion.Original]);
                    ConjuntoEstructuraDAL.Delete(cE, transaccion);
                }

                #region SC desactivado en IT2
                /*foreach (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow row in (Data.dsEstructura.SUBCONJUNTOSXESTRUCTURARow[])dsEstructura.SUBCONJUNTOSXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    scE.CodigoDetalle = Convert.ToInt32(row["scxe_codigo", System.Data.DataRowVersion.Original]);
                    SubconjuntoEstructuraDAL.Delete(scE, transaccion);
                }*/
                #endregion

                foreach (Data.dsEstructura.PIEZASXESTRUCTURARow row in (Data.dsEstructura.PIEZASXESTRUCTURARow[])dsEstructura.PIEZASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    pE.CodigoDetalle = Convert.ToInt32(row["pxe_codigo", System.Data.DataRowVersion.Original]);
                    PiezaEstructuraDAL.Delete(pE, transaccion);
                }

                #region MP desactivado en IT2
                /*foreach (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow row in (Data.dsEstructura.MATERIASPRIMASXESTRUCTURARow[])dsEstructura.MATERIASPRIMASXESTRUCTURA.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    mpE.CodigoDetalle = Convert.ToInt32(row["mpxe_codigo", System.Data.DataRowVersion.Original]);
                    MateriaPrimaEstructuraDAL.Delete(mpE, transaccion);
                }*/
                #endregion

                #endregion

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
        
        public static void ObtenerEstructuras(object nombre, object codPlano, object fechaCreacion, object codCocina, object codResponsable, object activoSiNo, Data.dsEstructura ds)
        {
            string sql = @"SELECT estr_codigo, estr_nombre, coc_codigo, pno_codigo, estr_descripcion, estr_activo, estr_fecha_alta, 
                           estr_fecha_modificacion, e_codigo, estr_costo, estr_costofijo FROM ESTRUCTURAS WHERE 1=1 ";

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
            //Determinar que condiciones necesita para poder eliminarse, de momento que no sea activa - gonzalo
            string sql = "SELECT estr_activo FROM ESTRUCTURAS WHERE estr_codigo = @p0";
            object[] valorParametros = { codigoEstructura };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0) { return true; }
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

        private static void ObtenerDetalleEstructura(Data.dsEstructura dsEstructura)
        {
            int[] codigosEstructuras = new int[dsEstructura.ESTRUCTURAS.Rows.Count];
            int i = 0;
            foreach (Data.dsEstructura.ESTRUCTURASRow row in dsEstructura.ESTRUCTURAS)
            {
                codigosEstructuras[i] = Convert.ToInt32(row.ESTR_CODIGO);
                i++;
            }

            try
            {
                GrupoEstructuraDAL.ObtenerGruposEstructura(codigosEstructuras, dsEstructura);
                ConjuntoEstructuraDAL.ObtenerConjuntosEstructura(codigosEstructuras, dsEstructura);
                PiezaEstructuraDAL.ObtenerPiezasEstructura(codigosEstructuras, dsEstructura);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            
        }

        private static void ObtenerDetalleEstructura(int codigoEstructura, Data.dsEstructura dsEstructura)
        { 
            int[] codigosEstructuras = { codigoEstructura };

            try
            {
                GrupoEstructuraDAL.ObtenerGruposEstructura(codigosEstructuras, dsEstructura);
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
                          estr_fecha_alta, estr_fecha_modificacion, e_codigo, estr_costo, estr_costofijo 
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
