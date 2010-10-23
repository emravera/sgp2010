using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class PlanMantenimientoDAL
    {
        //public static readonly int EstadoEnCurso = 2;
        //public static readonly int EstadoFinalizado = 5;

        public static void Insertar(Data.dsPlanMantenimiento dsPlanMantenimiento)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sqlInsert = @"INSERT INTO [PLANES_MANTENIMIENTO]
                               ([EPMAN_CODIGO]
                               ,[PMAN_FECHA]
                               ,[PMAN_OBSERVACIONES]
                               ,[PMAN_DESCRIPCION]) 
                               VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";

            //Así obtenemos el pedido nuevo del dataset, indicamos la primer fila de las agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsPlanMantenimiento.PLANES_MANTENIMIENTORow rowPlan = dsPlanMantenimiento.PLANES_MANTENIMIENTO.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsPlanMantenimiento.PLANES_MANTENIMIENTORow;
            object[] valorParametros = { rowPlan.EPMAN_CODIGO,  
                                         DB.GetFechaServidor(), 
                                         rowPlan.PMAN_OBSERVACIONES, 
                                         rowPlan.PMAN_DESCRIPCION };

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();
                //Insertamos la pieza y actualizamos su código en el dataset
                rowPlan.BeginEdit();
                rowPlan.PMAN_NUMERO  = Convert.ToInt64(DB.executeScalar(sqlInsert, valorParametros, transaccion));

                rowPlan.PMAN_NUMERO = rowPlan.PMAN_NUMERO;
                rowPlan.EndEdit();

                //Ahora insertamos el detalle, usamos el foreach para recorrer sólo los nuevos registros del dataset
                Entidades.DetallePlanMantenimiento detalle = new GyCAP.Entidades.DetallePlanMantenimiento();
                foreach (Data.dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTORow row in (Data.dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTORow[])dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    Entidades.PlanMantenimiento lPlan = new Entidades.PlanMantenimiento();
                    lPlan.Numero = Convert.ToInt64(rowPlan.PMAN_NUMERO);
                    
                    Entidades.Mantenimiento lMantenimiento = new Entidades.Mantenimiento();
                    lMantenimiento.Codigo = Convert.ToInt64(row.MAN_CODIGO);

                    Entidades.UnidadMedida lUnidadMedida = new Entidades.UnidadMedida();
                    lUnidadMedida.Codigo = Convert.ToInt32(row.UMED_CODIGO);

                    Entidades.EstadoDetalleMantenimiento lEstadoDetalle = new GyCAP.Entidades.EstadoDetalleMantenimiento();
                    lEstadoDetalle.Codigo = 1; //ACVTIVO - Hacer parametro, no puede quedar Hardcodado  

                    detalle.Plan = lPlan;
                    detalle.Descripcion = row.DPMAN_DESCRIPCION; 
                    detalle.Estado = lEstadoDetalle;
                    detalle.Frecuencia = row.DPMAN_FRECUENCIA;
                    detalle.Mantenimiento = lMantenimiento;
                    detalle.UnidadMedida = lUnidadMedida;
                    DetallePlanMantenimientoDAL.Insertar(detalle, transaccion);
                    row.BeginEdit();
                    row.PMAN_NUMERO = detalle.Plan.Numero;
                    row.DPMAN_DESCRIPCION = detalle.Descripcion;
                    row.EDMAN_CODIGO = detalle.Estado.Codigo;
                    row.DPMAN_FRECUENCIA = detalle.Frecuencia;
                    row.UMED_CODIGO = detalle.UnidadMedida.Codigo;
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

        public static void Eliminar(long codigoPlan)
        {
            string sql = "DELETE FROM PLANES_MANTENIMIENTO WHERE PMAN_NUMERO = @p0";
            object[] valorParametros = { codigoPlan };
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                //Primero los hijos
                DetallePlanMantenimientoDAL.EliminarDetallePlanMantenimiento(codigoPlan, transaccion);
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

        public static void Actualizar(Data.dsPlanMantenimiento dsPlanMantenimiento)
        {
            //Primero actualizaremos la pieza y luego la estructura
            //Armemos todas las consultas
            string sqlUpdate = @"UPDATE PLANES_MANTENIMIENTO SET
                                EPMAN_CODIGO = @p0
                               ,PMAN_FECHA = @p1
                               ,PMAN_OBSERVACIONES = @p2
                               ,PMAN_DESCRIPCION = @p3
                               WHERE PMAN_NUMERO = @p4";

            //Así obtenemos el pedido del dataset, indicamos la primer fila de las modificadas ya que es una sola y convertimos al tipo correcto
            Data.dsPlanMantenimiento.PLANES_MANTENIMIENTORow rowPlan = dsPlanMantenimiento.PLANES_MANTENIMIENTO.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsPlanMantenimiento.PLANES_MANTENIMIENTORow;
            object[] valorParametros = { rowPlan.EPMAN_CODIGO, 
                                         rowPlan.PMAN_FECHA, 
                                         rowPlan.PMAN_OBSERVACIONES, 
                                         rowPlan.PMAN_DESCRIPCION };

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Actualizamos el detalle
                DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
                //Actualizamos la estructura, primero insertamos los nuevos
                Entidades.DetallePlanMantenimiento detalle = new GyCAP.Entidades.DetallePlanMantenimiento();
                foreach (Data.dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTORow row in (Data.dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTORow[])dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    Entidades.PlanMantenimiento lPlan = new Entidades.PlanMantenimiento();
                    lPlan.Numero = Convert.ToInt64(rowPlan.PMAN_NUMERO);

                    Entidades.Mantenimiento lMantenimiento = new Entidades.Mantenimiento();
                    lMantenimiento.Codigo = Convert.ToInt64(row.MAN_CODIGO);

                    Entidades.UnidadMedida lUnidadMedida = new Entidades.UnidadMedida();
                    lUnidadMedida.Codigo = Convert.ToInt32(row.UMED_CODIGO);

                    Entidades.EstadoDetalleMantenimiento lEstadoDetalle = new GyCAP.Entidades.EstadoDetalleMantenimiento();
                    lEstadoDetalle.Codigo = 1; //ACVTIVO - Hacer parametro, no puede quedar Hardcodado  

                    detalle.Plan = lPlan;
                    detalle.Descripcion = row.DPMAN_DESCRIPCION;
                    detalle.Estado = lEstadoDetalle;
                    detalle.Frecuencia = row.DPMAN_FRECUENCIA;
                    detalle.Mantenimiento = lMantenimiento;
                    detalle.UnidadMedida = lUnidadMedida;
                    DetallePlanMantenimientoDAL.Actualizar(detalle, transaccion);
                    row.BeginEdit();
                    row.PMAN_NUMERO = detalle.Plan.Numero;
                    row.DPMAN_DESCRIPCION = detalle.Descripcion;
                    row.EDMAN_CODIGO = detalle.Estado.Codigo;
                    row.DPMAN_FRECUENCIA = detalle.Frecuencia;
                    row.UMED_CODIGO = detalle.UnidadMedida.Codigo;
                    row.EndEdit();

                }

                //Segundo actualizamos los modificados
                foreach (Data.dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTORow row in (Data.dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTORow[])dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    Entidades.PlanMantenimiento lPlan = new Entidades.PlanMantenimiento();
                    lPlan.Numero = Convert.ToInt64(rowPlan.PMAN_NUMERO);

                    Entidades.Mantenimiento lMantenimiento = new Entidades.Mantenimiento();
                    lMantenimiento.Codigo = Convert.ToInt64(row.MAN_CODIGO);

                    Entidades.UnidadMedida lUnidadMedida = new Entidades.UnidadMedida();
                    lUnidadMedida.Codigo = Convert.ToInt32(row.UMED_CODIGO);

                    Entidades.EstadoDetalleMantenimiento lEstadoDetalle = new GyCAP.Entidades.EstadoDetalleMantenimiento();
                    lEstadoDetalle.Codigo = 1; //ACVTIVO - Hacer parametro, no puede quedar Hardcodado  

                    detalle.Plan = lPlan;
                    detalle.Descripcion = row.DPMAN_DESCRIPCION;
                    detalle.Estado = lEstadoDetalle;
                    detalle.Frecuencia = row.DPMAN_FRECUENCIA;
                    detalle.Mantenimiento = lMantenimiento;
                    detalle.UnidadMedida = lUnidadMedida;
                    DetallePlanMantenimientoDAL.Actualizar(detalle, transaccion);
                }

                //Tercero eliminamos
                foreach (Data.dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTORow row in (Data.dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTORow[])dsPlanMantenimiento.DETALLE_PLANES_MANTENIMIENTO.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    detalle.Codigo = Convert.ToInt64(row["DPMAN_CODIGO", System.Data.DataRowVersion.Original]);
                    DetallePlanMantenimientoDAL.Eliminar(detalle, transaccion);
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
        public static bool EsPlanMantenimiento(Entidades.PlanMantenimiento planMantenimmiento)
        {
            string sql = "SELECT count(PMAN_NUMERO) FROM PLANES_MANTENIMIENTO WHERE PMAN_DESCRIPCION = @p0 AND PMAN_NUMERO = @p1";
            object[] valorParametros = { planMantenimmiento.Descripcion, planMantenimmiento.Numero };
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
        //            string sql = @"SELECT PMAN_NUMERO, CLI_CODIGO, EPED_CODIGO, PED_FECHAENTREGAPREVISTA, PED_FECHAENTREGAREAL
        //                           , PED_FECHA_ALTA, PED_OBSERVACIONES, PED_NUMERO FROM PEDIDOS WHERE PMAN_NUMERO = @p0";

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

        public static void ObtenerPlanMantenimiento(object descripcion, object numero, int idEstadoPlan, Data.dsPlanMantenimiento ds, bool obtenerDetalle)
        {
            string sql = @"SELECT PMAN_NUMERO, EPMAN_CODIGO, PMAN_FECHA, PMAN_OBSERVACIONES, PMAN_DESCRIPCION
                          FROM PLANES_MANTENIMIENTO ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[5];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (descripcion != null && descripcion.ToString() != string.Empty)
            {
                //si aplica el filtro lo usamos
                sql += " AND PMAN_DESCRIPCION LIKE @p" + cantidadParametros + " ";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                descripcion = "%" + descripcion + "%";
                valoresFiltros[cantidadParametros] = descripcion;
                cantidadParametros++;
            }

            if (numero != null && numero.ToString() != string.Empty)
            {
                //si aplica el filtro lo usamos
                sql += " AND PMAN_NUMERO LIKE @p" + cantidadParametros + " ";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                numero = "%" + numero + "%";
                valoresFiltros[cantidadParametros] = numero;
                cantidadParametros++;
            }

            //ESTADO - Revisamos si es distinto de 0, o sea "todos"
            if (idEstadoPlan != -1)
            {
                sql += " AND EPMAN_CODIGO = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(idEstadoPlan);
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
                    DB.FillDataSet(ds, "PLANES_MANTENIMIENTO", sql, valorParametros);
                    if (obtenerDetalle) { ObtenerDetallePlanMantenimiento(ds); }
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
            else
            {
                //Buscamos sin filtro
                try
                {
                    DB.FillDataSet(ds, "PLANES_MANTENIMIENTO", sql, null);
                    if (obtenerDetalle) { ObtenerDetallePlanMantenimiento(ds); }
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
        }

        public static bool PuedeEliminarse(long codigo)
        {
            string sqlDPM = "SELECT count(DPMAN_CODIGO) FROM DETALLE_PLANES_MENSUALES WHERE PMAN_NUMERO = @p0";
            string sqlMAQ = "SELECT count(MAQ_CODIGO) FROM MAQUINAS WHERE PMAN_NUMERO = @p0";

            object[] valorParametros = { codigo };
            try
            {
                int resultadoDPM = Convert.ToInt32(DB.executeScalar(sqlDPM, valorParametros, null));
                int resultadoMAQ = Convert.ToInt32(DB.executeScalar(sqlMAQ, valorParametros, null));

                if (resultadoMAQ == 0 && resultadoDPM == 0) { return true; }
                else { return false; }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        private static void ObtenerDetallePlanMantenimiento(Data.dsPlanMantenimiento ds)
        {
            string sql = @"SELECT DPMAN_CODIGO, PMAN_NUMERO, EDPMAN_CODIGO, MAN_CODIGO, UMED_CODIGO
                          , DPMAN_FRECUENCIA, DPMAN_DESCRIPCION
                          FROM DETALLE_PLANES_MANTENIMIENTO WHERE PMAN_NUMERO = @p0";

            object[] valorParametros; ;

            foreach (Data.dsPlanMantenimiento.PLANES_MANTENIMIENTORow rowPlan in ds.PLANES_MANTENIMIENTO)
            {
                valorParametros = new object[] { rowPlan.PMAN_NUMERO };
                DB.FillDataTable(ds.DETALLE_PLANES_MANTENIMIENTO, sql, valorParametros);
            }
        }

        //public static void ActualizarEstadoAEnCurso(int codigoPedido, SqlTransaction transaccion)
        //{
        //    ActualizarEstado(codigoPedido, EstadoEnCurso, transaccion);
        //}

        //public static void ActualizarEstado(int codigoPedido, int codigoEstado, SqlTransaction transaccion)
        //{
        //    string sql = "UPDATE PLANES_MANTENIMIENTO SET eped_codigo = @p0 WHERE ped_codigo = @p1";
        //    object[] parametros = { codigoEstado, codigoPedido };
        //    DB.executeNonQuery(sql, parametros, transaccion);
        //}

        //public static void ActualizarDetallePedidoAEnCurso(int codigoDetalle, SqlTransaction transaccion)
        //{
        //    DetallePedidoDAL.ActualizarEstadoAEnCurso(codigoDetalle, transaccion);

        //    string sql = "SELECT ped_codigo FROM DETALLE_PEDIDOS WHERE dped_codigo = @p0";
        //    object[] parametros = { codigoDetalle };
        //    ActualizarEstadoAEnCurso(Convert.ToInt32(DB.executeScalar(sql, parametros, transaccion)), transaccion);
        //}

        //public static void ActualizarDetallePedidoAFinalizado(int codigoDetalle, SqlTransaction transaccion)
        //{
        //    DetallePedidoDAL.ActualizarEstadoAFinalizado(codigoDetalle, transaccion);

        //    string sql = "SELECT ped_codigo FROM DETALLE_PEDIDOS WHERE dped_codigo = @p0";
        //    object[] parametros = { codigoDetalle };
        //    int pedido = Convert.ToInt32(DB.executeScalar(sql, parametros, transaccion));

        //    if (EsPedidoConDetalleFinalizado(pedido, transaccion))
        //    {
        //        ActualizarEstado(pedido, EstadoFinalizado, transaccion);
        //    }
        //}

        //private static bool EsPedidoConDetalleFinalizado(int codigoPedido, SqlTransaction transaccion)
        //{
        //    string sql = "SELECT COUNT(dped_codigo) FROM DETALLE_PEDIDOS WHERE ped_codigo = @p0 AND edped_codigo <> @p1";
        //    object[] parametros = { codigoPedido, DetallePedidoDAL.EstadoFinalizado };

        //    if (Convert.ToInt32(DB.executeScalar(sql, parametros, transaccion)) == 0) { return true; }
        //    else { return false; }
        //}
    }
}
