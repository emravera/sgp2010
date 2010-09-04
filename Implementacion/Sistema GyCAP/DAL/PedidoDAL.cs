using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class PedidoDAL
    {
//        public static void Insertar(Data.dsCliente dsCliente)
//        {
//            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
//            string sqlInsert = @"INSERT INTO [PIEZAS]
//                               ([pza_nombre]
//                               ,[te_codigo]
//                               ,[pza_descripcion]
//                               ,[pza_cantidadstock]
//                               ,[par_codigo]
//                               ,[pno_codigo]
//                               ,[pza_codigoparte]
//                               ,[pza_costo]
//                               ,[hr_codigo]
//                               ,[pza_costofijo]) 
//                               VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9) SELECT @@Identity";

//            //Así obtenemos la pieza nueva del dataset, indicamos la primer fila de las agregadas ya que es una sola y convertimos al tipo correcto
//            Data.dsEstructura.PIEZASRow rowPieza = dsCliente.PIEZAS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructura.PIEZASRow;
//            object hojaRuta = DBNull.Value;
//            if (!rowPieza.IsHR_CODIGONull()) { hojaRuta = rowPieza.HR_CODIGO; }
//            object[] valorParametros = { rowPieza.PZA_NOMBRE, 
//                                           rowPieza.TE_CODIGO, 
//                                           rowPieza.PZA_DESCRIPCION, 
//                                           0, 
//                                           rowPieza.PAR_CODIGO, 
//                                           rowPieza.PNO_CODIGO, 
//                                           rowPieza.PZA_CODIGOPARTE, 
//                                           rowPieza.PZA_COSTO, 
//                                           hojaRuta,
//                                           rowPieza.PZA_COSTOFIJO };

//            //Declaramos el objeto transaccion
//            SqlTransaction transaccion = null;

//            try
//            {
//                //Iniciamos la transaccion
//                transaccion = DB.IniciarTransaccion();
//                //Insertamos la pieza y actualizamos su código en el dataset
//                rowPieza.BeginEdit();
//                rowPieza.PZA_CODIGO = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));
//                rowPieza.EndEdit();
//                //Ahora insertamos su estructura, usamos el foreach para recorrer sólo los nuevos registros del dataset
//                Entidades.DetallePieza detalle = new GyCAP.Entidades.DetallePieza();
//                foreach (Data.dsEstructura.MATERIASPRIMASXPIEZARow row in (Data.dsEstructura.MATERIASPRIMASXPIEZARow[])dsCliente.MATERIASPRIMASXPIEZA.Select(null, null, System.Data.DataViewRowState.Added))
//                {
//                    detalle.CodigoPieza = Convert.ToInt32(rowPieza.PZA_CODIGO);
//                    detalle.CodigoMateriaPrima = Convert.ToInt32(row.MP_CODIGO);
//                    detalle.Cantidad = row.MPXP_CANTIDAD;
//                    DetallePiezaDAL.Insertar(detalle, transaccion);
//                    row.BeginEdit();
//                    row.PZA_CODIGO = detalle.CodigoPieza;
//                    row.MPXP_CODIGO = detalle.CodigoDetalle;
//                    row.EndEdit();
//                }
//                //Todo ok, commit
//                transaccion.Commit();
//            }
//            catch (SqlException ex)
//            {
//                //Error en alguna consulta, descartamos los cambios
//                transaccion.Rollback();
//                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
//            }
//            finally
//            {
//                //En cualquier caso finalizamos la transaccion para que se cierre la conexion
//                DB.FinalizarTransaccion();
//            }
//        }

//        public static void Eliminar(int codigoPedido)
//        {
//            string sql = "DELETE FROM PEDIDOS WHERE ped_codigo = @p0";
//            object[] valorParametros = { codigoPedido };
//            SqlTransaction transaccion = null;

//            try
//            {
//                transaccion = DB.IniciarTransaccion();
//                //Primero los hijos
//                DetallePedidoDAL.EliminarDetallePedido(codigoPedido, transaccion);
//                //Ahora el padre
//                DB.executeNonQuery(sql, valorParametros, transaccion);
//                //Todo OK
//                transaccion.Commit();
//            }
//            catch (SqlException)
//            {
//                transaccion.Rollback();
//                throw new Entidades.Excepciones.BaseDeDatosException();
//            }
//            finally
//            {
//                DB.FinalizarTransaccion();
//            }
//        }

//        public static void Actualizar(Data.dsCliente dsCliente)
//        {
//            //Esto va a ser muy largo...empecemos
//            //Primero actualizaremos la pieza y luego la estructura
//            //Armemos todas las consultas
//            string sqlUpdate = @"UPDATE PIEZAS SET
//                               pza_nombre = @p0
//                               ,te_codigo = @p1
//                               ,pza_descripcion = @p2
//                               ,par_codigo = @p3
//                               ,pno_codigo = @p4
//                               ,pza_codigoparte = @p5
//                               ,pza_costo = @p6
//                               ,hr_codigo = @p7
//                               ,pza_costo = @p8
//                               WHERE pza_codigo = @p9";

//            //Así obtenemos la pieza del dataset, indicamos la primer fila de las modificadas ya que es una sola y convertimos al tipo correcto
//            Data.dsEstructura.PIEZASRow rowPieza = dsCliente.PIEZAS.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsEstructura.PIEZASRow;
//            object hojaRuta = DBNull.Value;
//            if (!rowPieza.IsHR_CODIGONull()) { hojaRuta = rowPieza.HR_CODIGO; }
//            object[] valorParametros = { rowPieza.PZA_NOMBRE, 
//                                         rowPieza.TE_CODIGO, 
//                                         rowPieza.PZA_DESCRIPCION, 
//                                         rowPieza.PAR_CODIGO, 
//                                         rowPieza.PNO_CODIGO, 
//                                         rowPieza.PZA_CODIGOPARTE, 
//                                         rowPieza.PZA_COSTO, 
//                                         hojaRuta, 
//                                         rowPieza.PZA_COSTOFIJO,
//                                         rowPieza.PZA_CODIGO };

//            //Declaramos el objeto transaccion
//            SqlTransaction transaccion = null;

//            try
//            {
//                //Iniciamos la transaccion
//                transaccion = DB.IniciarTransaccion();

//                //Actualizamos la pieza
//                DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
//                //Actualizamos la estructura, primero insertamos los nuevos
//                Entidades.DetallePieza detalle = new GyCAP.Entidades.DetallePieza();
//                foreach (Data.dsEstructura.MATERIASPRIMASXPIEZARow row in (Data.dsEstructura.MATERIASPRIMASXPIEZARow[])dsCliente.MATERIASPRIMASXPIEZA.Select(null, null, System.Data.DataViewRowState.Added))
//                {
//                    detalle.CodigoPieza = Convert.ToInt32(row.PZA_CODIGO);
//                    detalle.CodigoMateriaPrima = Convert.ToInt32(row.MP_CODIGO);
//                    detalle.Cantidad = row.MPXP_CANTIDAD;
//                    DetallePiezaDAL.Insertar(detalle, transaccion);
//                    row.BeginEdit();
//                    row.MPXP_CODIGO = detalle.CodigoDetalle;
//                    row.EndEdit();
//                }

//                //Segundo actualizamos los modificados
//                foreach (Data.dsEstructura.MATERIASPRIMASXPIEZARow row in (Data.dsEstructura.MATERIASPRIMASXPIEZARow[])dsCliente.MATERIASPRIMASXPIEZA.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
//                {
//                    detalle.CodigoDetalle = Convert.ToInt32(row.MPXP_CODIGO);
//                    detalle.CodigoPieza = Convert.ToInt32(row.PZA_CODIGO);
//                    detalle.CodigoMateriaPrima = Convert.ToInt32(row.MP_CODIGO);
//                    detalle.Cantidad = row.MPXP_CANTIDAD;
//                    DetallePiezaDAL.Actualizar(detalle, transaccion);
//                }

//                //Tercero eliminamos
//                foreach (Data.dsEstructura.MATERIASPRIMASXPIEZARow row in (Data.dsEstructura.MATERIASPRIMASXPIEZARow[])dsCliente.MATERIASPRIMASXPIEZA.Select(null, null, System.Data.DataViewRowState.Deleted))
//                {
//                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
//                    detalle.CodigoDetalle = Convert.ToInt32(row["mpxp_codigo", System.Data.DataRowVersion.Original]);
//                    DetallePiezaDAL.Eliminar(detalle, transaccion);
//                }
//                //Si todo resulto correcto, commit
//                transaccion.Commit();
//            }
//            catch (SqlException ex)
//            {
//                //Error en alguna consulta, descartamos los cambios
//                transaccion.Rollback();
//                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
//            }
//            finally
//            {
//                //En cualquier caso finalizamos la transaccion para que se cierre la conexion
//                DB.FinalizarTransaccion();
//            }
//        }

//        //Determina si existe una pieza dado su nombre y terminación
//        public static bool EsPedido(Entidades.Pedido pedido)
//        {
//            string sql = "SELECT count(ped_codigo) FROM PEDIDOS WHERE PED_NUMERO = @p0 AND PED_codigo = @p1";
//            object[] valorParametros = {pedido.Numero, pedido.Codigo};
//            try
//            {
//                if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0)
//                {
//                    return false;
//                }
//                else
//                {
//                    return true;
//                }
//            }
//            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
//        }

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
//                           , PED_FECHA_ALTA, PED_OBSERVACIONES, PED_NUMERO FROM PEDIDOS WHERE PED_CODIGO = @p0";

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

        public static void ObtenerPedido(object nombre, object numero, int idEstadoPedido, DateTime fechaDesde, DateTime fechaHasta, Data.dsCliente ds, bool obtenerDetalle)
        {
            string sql = @"SELECT PED_CODIGO, CLI_CODIGO, EPED_CODIGO, PED_FECHAENTREGAPREVISTA, PED_FECHAENTREGAREAL
                          , PED_FECHA_ALTA, PED_OBSERVACIONES, PED_NUMERO
                          FROM PEDIDOS, CLIENTES WHERE PEDIDOS.CLI_CODIGO = CLIENTES.CLI_CODIGO ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[3];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //si aplica el filtro lo usamos
                sql += " AND CLI_RAZONSOCIAL LIKE @p" + cantidadParametros + " ";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }
            //Revisamos si pasó algun valor y si es un integer
            //if (codTerminacion != null && codTerminacion.GetType() == cantidadParametros.GetType())
            //{
            //    sql += " AND te_codigo = @p" + cantidadParametros;
            //    valoresFiltros[cantidadParametros] = Convert.ToInt32(codTerminacion);
            //    cantidadParametros++;
            //}

            //if (cantidadParametros > 0)
            //{
            //    //Buscamos con filtro, armemos el array de los valores de los parametros
            //    object[] valorParametros = new object[cantidadParametros];
            //    for (int i = 0; i < cantidadParametros; i++)
            //    {
            //        valorParametros[i] = valoresFiltros[i];
            //    }
            //    try
            //    {
            //        DB.FillDataSet(ds, "PIEZAS", sql, valorParametros);
            //        if (obtenerDetalle) { ObtenerDetallePiezas(ds); }
            //    }
            //    catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            //}
            //else
            //{
            //    //Buscamos sin filtro
            //    try
            //    {
            //        DB.FillDataSet(ds, "PIEZAS", sql, null);
            //        if (obtenerDetalle) { ObtenerDetallePiezas(ds); }
            //    }
            //    catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            //}
        }

//        public static void ObtenerPiezas(System.Data.DataTable dtPiezas)
//        {
//            string sql = @"SELECT pza_codigo, pza_nombre, pza_descripcion, te_codigo, pza_cantidadstock, par_codigo, pno_codigo
//                                , pza_codigoparte, pza_costo, hr_codigo, pza_costofijo FROM PIEZAS";

//            try
//            {
//                DB.FillDataTable(dtPiezas, sql, null);
//            }
//            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
//        }

//        public static void ObtenerPiezas(System.Data.DataTable dtPiezas, int estado)
//        {
//            string sql = @"SELECT pza_codigo, pza_nombre, pza_descripcion, te_codigo, pza_cantidadstock, par_codigo, pno_codigo
//                                , pza_codigoparte, pza_costo, hr_codigo, pza_costofijo FROM PIEZAS WHERE par_codigo = @p0";
//            object[] valorParametros = { estado };
//            try
//            {
//                DB.FillDataTable(dtPiezas, sql, valorParametros);
//            }
//            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
//        }

//        public static void ObtenerPieza(int codigoPieza, bool detalle, Data.dsEstructura ds)
//        {
//            string sql = @"SELECT pza_codigo, pza_nombre, te_codigo, pza_descripcion, pza_cantidadstock, par_codigo, pno_codigo, pza_codigoparte 
//                                  ,pza_costo, hr_codigo, pza_costofijo FROM PIEZAS WHERE pza_codigo = @p0";
//            object[] valorParametros = { codigoPieza };
//            try
//            {
//                DB.FillDataTable(ds.PIEZAS, sql, valorParametros);
//                if (detalle)
//                {
//                    //Obtenemos las materias primas que forman la pieza
//                    ObtenerDetallePieza(codigoPieza, ds);
//                }
//            }
//            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }

//        }

//        public static bool PuedeEliminarse(int codigo)
//        {
//            string sqlPXC = "SELECT count(pza_codigo) FROM PIEZASXCONJUNTO WHERE pza_codigo = @p0";
//            string sqlPXSC = "SELECT count(pza_codigo) FROM PIEZASXSUBCONJUNTO WHERE pza_codigo = @p0";
//            string sqlPXE = "SELECT count(pza_codigo) FROM PIEZASXESTRUCTURA WHERE pza_codigo = @p0";

//            object[] valorParametros = { codigo };
//            try
//            {
//                int resultadoPXC = Convert.ToInt32(DB.executeScalar(sqlPXC, valorParametros, null));
//                int resultadoPXSC = Convert.ToInt32(DB.executeScalar(sqlPXSC, valorParametros, null));
//                int resultadoPXE = Convert.ToInt32(DB.executeScalar(sqlPXE, valorParametros, null));
//                if (resultadoPXSC == 0 && resultadoPXE == 0 && resultadoPXC == 0) { return true; }
//                else { return false; }
//            }
//            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
//        }

//        private static void ObtenerDetallePiezas(Data.dsEstructura ds)
//        {
//            string sql = @"SELECT mpxp_codigo, pza_codigo, mp_codigo, mpxp_cantidad
//                         FROM MATERIASPRIMASXPIEZA WHERE pza_codigo = @p0";

//            object[] valorParametros; ;

//            foreach (Data.dsEstructura.PIEZASRow rowPieza in ds.PIEZAS)
//            {
//                valorParametros = new object[] { rowPieza.PZA_CODIGO };
//                DB.FillDataTable(ds.MATERIASPRIMASXPIEZA, sql, valorParametros);
//            }
//        }

//        private static void ObtenerDetallePieza(int codigoPieza, Data.dsEstructura ds)
//        {
//            string sql = @"SELECT mpxp_codigo, pza_codigo, mp_codigo, mpxp_cantidad
//                         FROM MATERIASPRIMASXPIEZA WHERE pza_codigo = @p0";

//            object[] valorParametros = { codigoPieza };

//            DB.FillDataTable(ds.MATERIASPRIMASXPIEZA, sql, valorParametros);
//            //Obtenemos las materias primas que forman la pieza
//            foreach (Data.dsEstructura.MATERIASPRIMASXPIEZARow rowMPxP in (Data.dsEstructura.MATERIASPRIMASXPIEZARow[])ds.MATERIASPRIMASXPIEZA.Select("pza_codigo = " + codigoPieza))
//            {
//                MateriaPrimaDAL.ObtenerMateriaPrima(Convert.ToInt32(rowMPxP.MP_CODIGO), ds);
//            }
//        }
    }
}
