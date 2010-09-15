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
        public static void Insertar(Data.dsCliente dsCliente)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sqlInsert = @"INSERT INTO [PEDIDOS]
                               ([CLI_CODIGO]
                               ,[EPED_CODIGO]
                               ,[PED_FECHAENTREGAPREVISTA]
                               ,[PED_FECHA_ALTA]
                               ,[PED_OBSERVACIONES]
                               ,[PED_NUMERO]) 
                               VALUES (@p0, @p1, @p2, @p3, @p4, @p5) SELECT @@Identity";

            //Así obtenemos el pedido nuevo del dataset, indicamos la primer fila de las agregadas ya que es una sola y convertimos al tipo correcto
            Data.dsCliente.PEDIDOSRow rowPedido = dsCliente.PEDIDOS.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsCliente.PEDIDOSRow;
            object[] valorParametros = { rowPedido.CLI_CODIGO, 
                                         rowPedido.EPED_CODIGO, 
                                         rowPedido.PED_FECHAENTREGAPREVISTA, 
                                         DB.GetFechaServidor(), 
                                         rowPedido.PED_OBSERVACIONES, 
                                         rowPedido.PED_NUMERO };

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();
                //Insertamos la pieza y actualizamos su código en el dataset
                rowPedido.BeginEdit();
                rowPedido.PED_CODIGO = Convert.ToInt64(DB.executeScalar(sqlInsert, valorParametros, transaccion));
                
                rowPedido.PED_NUMERO = rowPedido.PED_CODIGO.ToString(); 
                rowPedido.EndEdit();
                
                //Ahora insertamos el detalle, usamos el foreach para recorrer sólo los nuevos registros del dataset
                Entidades.DetallePedido detalle = new GyCAP.Entidades.DetallePedido();
                foreach (Data.dsCliente.DETALLE_PEDIDOSRow row in (Data.dsCliente.DETALLE_PEDIDOSRow[])dsCliente.DETALLE_PEDIDOS.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    Entidades.Pedido lPedido = new Entidades.Pedido();
                    lPedido.Codigo = Convert.ToInt64(rowPedido.PED_CODIGO);
                    Entidades.Cocina lCocina = new GyCAP.Entidades.Cocina();
                    lCocina.CodigoCocina = Convert.ToInt32(row.COC_CODIGO);
                    Entidades.EstadoDetallePedido lEstadoDetalle = new GyCAP.Entidades.EstadoDetallePedido();
                    lEstadoDetalle.Codigo = 1; //Hacer parametro, no puede quedar Hardcodado  

                    detalle.Pedido = lPedido;
                    detalle.Cocina = lCocina;
                    detalle.Cantidad = row.DPED_CANTIDAD;
                    detalle.Estado = lEstadoDetalle; 
                    DetallePedidoDAL.Insertar(detalle, transaccion);
                    row.BeginEdit();
                    row.PED_CODIGO = detalle.Pedido.Codigo;
                    row.COC_CODIGO = detalle.Cocina.CodigoCocina;
                    row.DPED_CANTIDAD = detalle.Cantidad;
                    row.EDPED_CODIGO = detalle.Estado.Codigo; 
                    row.EndEdit();
                }
                //Todo ok, commit
                transaccion.Commit();
                ActualizarNumero(long.Parse(rowPedido.PED_CODIGO.ToString())); 
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

        public static void Eliminar(long codigoPedido)
        {
            string sql = "DELETE FROM PEDIDOS WHERE ped_codigo = @p0";
            object[] valorParametros = { codigoPedido };
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                //Primero los hijos
                DetallePedidoDAL.EliminarDetallePedido(codigoPedido, transaccion);
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

        public static void Actualizar(Data.dsCliente dsCliente)
        {
            //Primero actualizaremos la pieza y luego la estructura
            //Armemos todas las consultas
            string sqlUpdate = @"UPDATE PEDIDOS SET
                                CLI_CODIGO = @p0
                               ,EPED_CODIGO = @p1
                               ,PED_FECHAENTREGAPREVISTA = @p2
                               ,PED_OBSERVACIONES = @p3
                               ,PED_NUMERO = @p4
                               WHERE PED_CODIGO = @p5";

            //Así obtenemos el pedido del dataset, indicamos la primer fila de las modificadas ya que es una sola y convertimos al tipo correcto
            Data.dsCliente.PEDIDOSRow rowPedido = dsCliente.PEDIDOS.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsCliente.PEDIDOSRow;
            object[] valorParametros = { rowPedido.CLI_CODIGO, 
                                         rowPedido.EPED_CODIGO, 
                                         rowPedido.PED_FECHAENTREGAPREVISTA, 
                                         rowPedido.PED_OBSERVACIONES, 
                                         rowPedido.PED_NUMERO, 
                                         rowPedido.PED_CODIGO };

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Actualizamos el detalle
                DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
                //Actualizamos la estructura, primero insertamos los nuevos
                Entidades.DetallePedido detalle = new GyCAP.Entidades.DetallePedido();
                foreach (Data.dsCliente.DETALLE_PEDIDOSRow row in (Data.dsCliente.DETALLE_PEDIDOSRow[])dsCliente.DETALLE_PEDIDOS.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    Entidades.Pedido lPedido = new Entidades.Pedido();
                    lPedido.Codigo = Convert.ToInt64(rowPedido.PED_CODIGO);
                    Entidades.Cocina lCocina = new GyCAP.Entidades.Cocina();
                    lCocina.CodigoCocina = Convert.ToInt32(row.COC_CODIGO);
                    Entidades.EstadoDetallePedido lEstadoDetalle = new GyCAP.Entidades.EstadoDetallePedido();
                    lEstadoDetalle.Codigo = 1; //Hacer parametro, no puede quedar Hardcodado  

                    detalle.Pedido = lPedido;
                    detalle.Cocina = lCocina;
                    detalle.Cantidad = row.DPED_CANTIDAD;
                    detalle.Estado = lEstadoDetalle;
                    DetallePedidoDAL.Actualizar(detalle, transaccion);
                    row.BeginEdit();
                    row.PED_CODIGO = detalle.Codigo;
                    row.COC_CODIGO = detalle.Cocina.CodigoCocina;
                    row.DPED_CANTIDAD = detalle.Cantidad;
                    row.EDPED_CODIGO = detalle.Estado.Codigo;
                    row.EndEdit();
                }

                //Segundo actualizamos los modificados
                foreach (Data.dsCliente.DETALLE_PEDIDOSRow row in (Data.dsCliente.DETALLE_PEDIDOSRow[])dsCliente.DETALLE_PEDIDOS.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent ))
                {
                    Entidades.Pedido lPedido = new Entidades.Pedido();
                    lPedido.Codigo = Convert.ToInt64(rowPedido.PED_CODIGO);
                    Entidades.Cocina lCocina = new GyCAP.Entidades.Cocina();
                    lCocina.CodigoCocina = Convert.ToInt32(row.COC_CODIGO);
                    Entidades.EstadoDetallePedido lEstadoDetalle = new GyCAP.Entidades.EstadoDetallePedido();
                    lEstadoDetalle.Codigo = 1; //Hacer parametro, no puede quedar Hardcodado  

                    detalle.Pedido = lPedido;
                    detalle.Cocina = lCocina;
                    detalle.Cantidad = row.DPED_CANTIDAD;
                    detalle.Estado = lEstadoDetalle;
                    DetallePedidoDAL.Actualizar(detalle, transaccion);
                }

                //Tercero eliminamos
                foreach (Data.dsCliente.DETALLE_PEDIDOSRow row in (Data.dsCliente.DETALLE_PEDIDOSRow[])dsCliente.DETALLE_PEDIDOS.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    //Como la fila está eliminada y no tiene datos, tenemos que acceder a la versión original
                    detalle.Codigo = Convert.ToInt64(row["dped_codigo", System.Data.DataRowVersion.Original]);
                    DetallePedidoDAL.Eliminar(detalle, transaccion);
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

        public static void ActualizarNumero(long codigo)
        {
            //Primero actualizaremos la pieza y luego la estructura
            //Armemos todas las consultas
            string sqlUpdate = @"UPDATE PEDIDOS SET
                                PED_NUMERO = @p0
                               WHERE PED_CODIGO = @p0";

            object[] valorParametros = { codigo };

            //Declaramos el objeto transaccion
            //SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                //transaccion = DB.IniciarTransaccion();

                //Actualizamos la pieza
                //DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
                DB.executeNonQuery(sqlUpdate, valorParametros, null);
            }
            catch (SqlException ex)
            {
                //Error en alguna consulta, descartamos los cambios
                //transaccion.Rollback();

                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
        }

        //Determina si existe una pedido dado su nombre y terminación
        public static bool EsPedido(Entidades.Pedido pedido)
        {
            string sql = "SELECT count(ped_codigo) FROM PEDIDOS WHERE PED_NUMERO = @p0 AND PED_codigo = @p1";
            object[] valorParametros = { pedido.Numero, pedido.Codigo };
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

        public static void ObtenerPedido(object nombre, object numero, int idEstadoPedido, object fechaDesde, object fechaHasta, Data.dsCliente ds, bool obtenerDetalle)
        {
            string sql = @"SELECT PED_CODIGO, PEDIDOS.CLI_CODIGO, EPED_CODIGO, PED_FECHAENTREGAPREVISTA, PED_FECHAENTREGAREAL
                          , PED_FECHA_ALTA, PED_OBSERVACIONES, PED_NUMERO
                          FROM PEDIDOS, CLIENTES WHERE PEDIDOS.CLI_CODIGO = CLIENTES.CLI_CODIGO ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[5];
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

            if (numero != null && numero.ToString() != string.Empty)
            {
                //si aplica el filtro lo usamos
                sql += " AND PED_NUMERO LIKE @p" + cantidadParametros + " ";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                numero = "%" + numero + "%";
                valoresFiltros[cantidadParametros] = numero;
                cantidadParametros++;
            }

            //ESTADO - Revisamos si es distinto de 0, o sea "todos"
            if (idEstadoPedido != -1)
            {
                sql += " AND EPED_CODIGO = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(idEstadoPedido);
                cantidadParametros++;
            }

            if (fechaDesde != null)
            {
                sql += " AND PED_FECHA_ALTA >= @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = ((DateTime )fechaDesde).ToShortDateString() ;
                cantidadParametros++;
            }

            if (fechaHasta != null)
            {

                sql += " AND PED_FECHA_ALTA <= @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] =((DateTime)fechaHasta).ToShortDateString() + " 23:59:59";
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
                    DB.FillDataSet(ds, "PEDIDOS", sql, valorParametros);
                    if (obtenerDetalle) { ObtenerDetallePedido(ds); }
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
            else
            {
                //Buscamos sin filtro
                try
                {
                    DB.FillDataSet(ds, "PEDIDOS", sql, null);
                    if (obtenerDetalle) { ObtenerDetallePedido(ds); }
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
        }

        public static bool PuedeEliminarse(long codigo)
        {
            string sqlDPM = "SELECT count(dped_codigo) FROM DETALLE_PLANES_MENSUALES WHERE dped_codigo = @p0";
            string sqlDPS = "SELECT count(dped_codigo) FROM DETALLE_PLANES_SEMANALES WHERE dped_codigo = @p0";

            object[] valorParametros = { codigo };
            try
            {
                int resultadoDPM = Convert.ToInt32(DB.executeScalar(sqlDPM, valorParametros, null));
                int resultadoDPS = Convert.ToInt32(DB.executeScalar(sqlDPS, valorParametros, null));

                if (resultadoDPS == 0 && resultadoDPM == 0) { return true; }
                else { return false; }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        private static void ObtenerDetallePedido(Data.dsCliente ds)
        {
            string sql = @"SELECT DPED_CODIGO, PED_CODIGO, EDPED_CODIGO, COC_CODIGO, DPED_CANTIDAD, DPED_FECHA_CANCELACION
                         FROM DETALLE_PEDIDOS WHERE PED_CODIGO = @p0";

            object[] valorParametros; ;

            foreach (Data.dsCliente.PEDIDOSRow rowPedido in ds.PEDIDOS)
            {
                valorParametros = new object[] { rowPedido.PED_CODIGO };
                DB.FillDataTable(ds.DETALLE_PEDIDOS, sql, valorParametros);
            }
        }
    }
}
