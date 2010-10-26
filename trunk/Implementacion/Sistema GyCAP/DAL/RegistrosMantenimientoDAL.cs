using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class RegistrosMantenimientoDAL
    {
        public static void Insertar(Data.dsRegistrarMantenimiento dsRegistrarMantenimiento)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sqlInsert = @"INSERT INTO [REGISTROS_MANTENIMIENTOS]
                               ([TMAN_CODIGO]
                               ,[MAN_CODIGO]
                               ,[MAQ_CODIGO]
                               ,[DPMAN_CODIGO]
                               ,[E_CODIGO]
                               ,[RMAN_FECHA_REALIZACION]
                               ,[RMAN_FECHA]
                               ,[RMAN_OBSERVACION]
                               ,[CF_NUMERO] ) 
                               VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8) SELECT @@Identity";

            //Así obtenemos el pedido nuevo del dataset, indicamos la primer fila de las agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOSRow rowRegistroMantenimiento = dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOSRow;
            object[] valorParametros = { rowRegistroMantenimiento.TMAN_CODIGO, 
                                         rowRegistroMantenimiento.MAN_CODIGO, 
                                         rowRegistroMantenimiento.MAQ_CODIGO,
                                         rowRegistroMantenimiento.DPMAN_CODIGO,
                                         rowRegistroMantenimiento.E_CODIGO,
                                         rowRegistroMantenimiento.RMAN_FECHA_REALIZACION,
                                         DB.GetFechaServidor(), 
                                         rowRegistroMantenimiento.RMAN_OBSERVACION, 
                                         rowRegistroMantenimiento.CF_NUMERO };

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();
                //Insertamos la pieza y actualizamos su código en el dataset
                rowRegistroMantenimiento.BeginEdit();
                rowRegistroMantenimiento.RMAN_CODIGO = Convert.ToInt64(DB.executeScalar(sqlInsert, valorParametros, transaccion));
                rowRegistroMantenimiento.EndEdit();

                //Ahora insertamos el detalle, usamos el foreach para recorrer sólo los nuevos registros del dataset
                Entidades.RepuestosRegistroMantenimiento detalle = new GyCAP.Entidades.RepuestosRegistroMantenimiento();
                foreach (Data.dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTORow row in (Data.dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTORow[])dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTO.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    Entidades.RegistrosMantenimientos lRegistroMantenimiento = new Entidades.RegistrosMantenimientos();
                    lRegistroMantenimiento.Codigo = Convert.ToInt64(rowRegistroMantenimiento.RMAN_CODIGO);
                    Entidades.Repuesto lRepuesto = new GyCAP.Entidades.Repuesto();
                    lRepuesto.Codigo = Convert.ToInt32(row.REP_CODIGO);

                    detalle.RegistroMantenimiento = lRegistroMantenimiento;
                    detalle.Repuesto = lRepuesto;
                    detalle.Cantidad = row.RRMAN_CANTIDAD;
                    RepuestosRegistroMantenimientoDAL.Insertar(detalle, transaccion);
                    row.BeginEdit();
                    row.RMAN_CODIGO = detalle.RegistroMantenimiento.Codigo;
                    row.REP_CODIGO = detalle.Repuesto.Codigo;
                    row.RRMAN_CANTIDAD = detalle.Cantidad;
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

        public static void Eliminar(long codigoRegistroMantenimiento)
        {
            string sql = "DELETE FROM REGISTROS_MANTENIMIENTOS WHERE RMAN_CODIGO = @p0";
            object[] valorParametros = { codigoRegistroMantenimiento };
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                //Primero los hijos
                RepuestosRegistroMantenimientoDAL.EliminarRepuestosRegistroMantenimiento(codigoRegistroMantenimiento, transaccion);
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

        public static void Actualizar(Data.dsRegistrarMantenimiento dsRegistrarMantenimiento)
        {
            //Primero actualizaremos la pieza y luego la estructura
            //Armemos todas las consultas
            string sqlUpdate = @"UPDATE REGISTROS_MANTENIMIENTOS SET
                                TMAN_CODIGO = @p0
                               ,MAN_CODIGO = @p1
                               ,MAQ_CODIGO = @p2
                               ,DPMAN_CODIGO = @p3
                               ,E_CODIGO = @p4
                               ,RMAN_FECHA_REALIZACION = @p5
                               ,RMAN_FECHA = @p6
                               ,RMAN_OBSERVACION = @p7
                               ,CF_NUMERO = @p8                        
                                WHERE RMAN_CODIGO = @p9";

            //Así obtenemos el pedido del dataset, indicamos la primer fila de las modificadas ya que es una sola y convertimos al tipo correcto
            Data.dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOSRow rowRegistroMantenimiento = dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOS.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOSRow;
            object[] valorParametros = { rowRegistroMantenimiento.TMAN_CODIGO, 
                                         rowRegistroMantenimiento.MAN_CODIGO, 
                                         rowRegistroMantenimiento.MAQ_CODIGO, 
                                         rowRegistroMantenimiento.DPMAN_CODIGO, 
                                         rowRegistroMantenimiento.E_CODIGO, 
                                         rowRegistroMantenimiento.RMAN_FECHA_REALIZACION,
                                         rowRegistroMantenimiento.RMAN_FECHA , 
                                         rowRegistroMantenimiento.RMAN_OBSERVACION, 
                                         rowRegistroMantenimiento.CF_NUMERO, 
                                         rowRegistroMantenimiento.RMAN_CODIGO};

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Actualizamos el detalle
                DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
                //Actualizamos la estructura, primero insertamos los nuevos
                Entidades.RepuestosRegistroMantenimiento detalle = new GyCAP.Entidades.RepuestosRegistroMantenimiento();
                foreach (Data.dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTORow row in (Data.dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTORow[])dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTO.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    Entidades.RegistrosMantenimientos lRegistroMantenimiento = new Entidades.RegistrosMantenimientos();
                    lRegistroMantenimiento.Codigo = Convert.ToInt64(rowRegistroMantenimiento.RMAN_CODIGO);
                    Entidades.Repuesto lRepuesto = new GyCAP.Entidades.Repuesto();
                    lRepuesto.Codigo = Convert.ToInt32(row.REP_CODIGO);

                    detalle.RegistroMantenimiento = lRegistroMantenimiento;
                    detalle.Repuesto = lRepuesto;
                    detalle.Cantidad = row.RRMAN_CANTIDAD;
                    RepuestosRegistroMantenimientoDAL.Actualizar(detalle, transaccion);
                    row.BeginEdit();
                    row.RMAN_CODIGO = detalle.RegistroMantenimiento.Codigo;
                    row.REP_CODIGO = detalle.Repuesto.Codigo;
                    row.RRMAN_CANTIDAD = detalle.Cantidad;
                    row.EndEdit();
                }

                //Segundo actualizamos los modificados
                foreach (Data.dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTORow row in (Data.dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTORow[])dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTO.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    Entidades.RegistrosMantenimientos lRegistroMantenimiento = new Entidades.RegistrosMantenimientos();
                    lRegistroMantenimiento.Codigo = Convert.ToInt64(rowRegistroMantenimiento.RMAN_CODIGO);
                    Entidades.Repuesto lRepuesto = new GyCAP.Entidades.Repuesto();
                    lRepuesto.Codigo = Convert.ToInt32(row.REP_CODIGO);

                    detalle.RegistroMantenimiento = lRegistroMantenimiento;
                    detalle.Repuesto = lRepuesto;
                    detalle.Cantidad = row.RRMAN_CANTIDAD;
                    RepuestosRegistroMantenimientoDAL.Actualizar(detalle, transaccion);
                }

                //Tercero eliminamos
                foreach (Data.dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTORow row in (Data.dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTORow[])dsRegistrarMantenimiento.REPUESTOS_REGISTRO_MANTENIMIENTO.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    detalle.Codigo = Convert.ToInt64(row["RRMAN_CODIGO", System.Data.DataRowVersion.Original]);
                    RepuestosRegistroMantenimientoDAL.Eliminar(detalle, transaccion);
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

        //Determina si existe una pedido dado su nombre y terminación
        public static bool EsRegistroMantenimiento(Entidades.RegistrosMantenimientos registroMantenimiento)
        {
            string sql = "SELECT count(RMAN_CODIGO) FROM REGISTROS_MANTENIMIENTOS WHERE TMAN_CODIGO = @p0 AND RMAN_OBSERVACION = @p1";
            object[] valorParametros = { registroMantenimiento.Tipo.Codigo , registroMantenimiento.Observacion };
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

        //        /// <summary>
        //        /// Obtiene una pieza por su código.
        //        /// </summary>
        //        /// <param name="codigoConjunto">El código de la pieza deseada.</param>
        //        /// <returns>El objeto pieza con sus datos.</returns>
        //        /// <exception cref="ElementoInexistenteException">En caso de que no exista la pieza.</exception>
        //        /// <exception cref="BaseDeDatosException">En caso de problemas con la base de datos.</exception>
        //        public static Entidades.Pedido ObtenerPedido(int codigoPedido)
        //        {
        //            string sql = @"SELECT PED_CODIGO, CLI_CODIGO, EPED_CODIGO, PED_FECHAENTREGAPREVISTA, PED_FECHAENTREGAREAL
        //                           , PED_FECHA_ALTA, PED_OBSERVACIONES, PED_NUMERO FROM REGISTROS_MANTENIMIENTOS WHERE PED_CODIGO = @p0";

        //            object[] valorParametros = { codigoPedido };
        //            SqlDataReader rdr = DB.GetReader(sql, valorParametros, null);
        //            Entidades.Pedido pedido = new GyCAP.Entidades.Pedido();
        //            try
        //            {
        //                if (!rdr.HasRows) { throw new Entidades.Excepciones.ElementoInexistenteException(); }
        //                rdr.Read();
        //                pedido.Codigo = codigoPedido;
        //                pedido.Cliente.Codigo = rdr["CLI_CODIGO"].ToString();
        //                pedido.EstadoPedido.Codigo = rdr["EPED_CODIGO"].ToString();
        //                pedido.FechaAlta = Convert.ToDateTime(rdr["PED_FECHA_ALTA"].ToString());
        //                pedido.FechaEntregaPrevista = Convert.ToDateTime(rdr["PED_FECHAENTREGAPREVISTA"].ToString());
        //                pedido.FechaEntregaReal = Convert.ToInt32(rdr["PED_FECHAENTREGAREAL"].ToString());
        //                pedido.Numero = rdr["PED_NUMERO"].ToString();
        //                pedido.Observaciones = rdr["PED_OBSERVACIONES"].ToString();

        //                //if (rdr["hr_codigo"] != DBNull.Value) { pedido.CodigoHojaRuta = Convert.ToInt32(rdr["hr_codigo"].ToString()); }

        //            }
        //            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        //            finally
        //            {
        //                if (rdr != null) { rdr.Close(); }
        //                DB.CloseReader();
        //            }
        //            return pedido;
        //        }

        public static void ObtenerRegistroMantenimiento(object fechaDesde, object fechaHasta, int idEmpleado,int idMaquina, int idTipoMantenimeinto, Data.dsRegistrarMantenimiento ds, bool obtenerDetalle)
        {
            string sql = @"SELECT RMAN_CODIGO, TMAN_CODIGO, MAN_CODIGO, MAQ_CODIGO, DPMAN_CODIGO,E_CODIGO
                           , RMAN_FECHA_REALIZACION, RMAN_FECHA, RMAN_OBSERVACION,CF_NUMERO
                           FROM REGISTROS_MANTENIMIENTOS where 1 = 1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[5];

            //EMPLEADO - Revisamos si es distinto de 0, o sea "todos"
            if (idEmpleado != -1)
            {
                sql += " AND E_CODIGO = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(idEmpleado);
                cantidadParametros++;
            }

            //MAQUINA - Revisamos si es distinto de 0, o sea "todos"
            if (idMaquina != -1)
            {
                sql += " AND MAN_CODIGO = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(idMaquina);
                cantidadParametros++;
            }

            //TIPO MANTENIMIENTO - Revisamos si es distinto de 0, o sea "todos"
            if (idTipoMantenimeinto != -1)
            {
                sql += " AND TMAN_CODIGO = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(idTipoMantenimeinto);
                cantidadParametros++;
            }



            if (fechaDesde != null)
            {
                sql += " AND RMAN_FECHA_REALIZACION >= @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = ((DateTime)fechaDesde).ToShortDateString();
                cantidadParametros++;
            }

            if (fechaHasta != null)
            {

                sql += " AND RMAN_FECHA_REALIZACION <= @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = ((DateTime)fechaHasta).ToShortDateString() + " 23:59:59";
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
                    DB.FillDataSet(ds, "REGISTROS_MANTENIMIENTOS", sql, valorParametros);
                    if (obtenerDetalle) { ObtenerRepuestosDelRegistroMantenimiento(ds); }
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
            else
            {
                //Buscamos sin filtro
                try
                {
                    DB.FillDataSet(ds, "REGISTROS_MANTENIMIENTOS", sql, null);
                    if (obtenerDetalle) { ObtenerRepuestosDelRegistroMantenimiento(ds); }
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
        }

        public static bool PuedeEliminarse(long codigo)
        {
            string sqlRRM = "SELECT count(RRMAN_CODIGO) FROM REPUESTOS_REGISTRO_MANTENIMIENTO WHERE RMAN_CODIGO = @p0";

            object[] valorParametros = { codigo };
            try
            {
                int resultadoRRM = Convert.ToInt32(DB.executeScalar(sqlRRM, valorParametros, null));

                if (resultadoRRM == 0) { return true; }
                else { return false; }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        private static void ObtenerRepuestosDelRegistroMantenimiento(Data.dsRegistrarMantenimiento ds)
        {
            string sql = @"SELECT RRMAN_CODIGO, RMAN_CODIGO, REP_CODIGO, RRMAN_CANTIDAD
                         FROM REPUESTOS_REGISTRO_MANTENIMIENTO WHERE RMAN_CODIGO = @p0";

            object[] valorParametros; ;

            foreach (Data.dsRegistrarMantenimiento.REGISTROS_MANTENIMIENTOSRow rowRegistroMantenimiento in ds.REGISTROS_MANTENIMIENTOS)
            {
                valorParametros = new object[] { rowRegistroMantenimiento.RMAN_CODIGO };
                DB.FillDataTable(ds.REPUESTOS_REGISTRO_MANTENIMIENTO, sql, valorParametros);
            }
        }

    }
}
