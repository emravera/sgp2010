﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class PedidoDAL
    {
        public static readonly int EstadoEnCurso = 2;
        public static readonly int EstadoFinalizado = 5;
        
        //****************************************************************************
        //                      METODOS DE BUSQUEDA
        //****************************************************************************

        public static void ObtenerPedido(object nombre, object numero, int idEstadoPedido, object fechaDesde, object fechaHasta, Data.dsCliente ds, bool obtenerDetalle)
        {
            string sql = @"SELECT PED_CODIGO, PEDIDOS.CLI_CODIGO, EPED_CODIGO, PED_FECHAENTREGAREAL, 
                          PED_FECHA_ALTA, PED_OBSERVACIONES, PED_NUMERO
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
                valoresFiltros[cantidadParametros] = ((DateTime)fechaDesde).ToShortDateString();
                cantidadParametros++;
            }

            if (fechaHasta != null)
            {

                sql += " AND PED_FECHA_ALTA <= @p" + cantidadParametros;
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

        //Metodo que obtiene el pedido
        public static void ObtenerPedidoFecha(DateTime fecha, DataTable dtPedidos)
        {
            string sql = @"SELECT ped_codigo, cli_codigo, eped_codigo, ped_fechaentregareal, ped_fecha_alta, ped_numero
                           FROM PEDIDOS WHERE ped_fecha_alta >= @p0";
            string dia = "'" + fecha.ToString() + "'";
            object[] valorParametros = { fecha };
            
            try
            {
                //Se llena el Dataset
                DB.FillDataTable(dtPedidos, sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo que obtiene los pedidos de un cliente determinado en una fecha determinada
        public static void ObtenerPedidosCliente(int CodigoCliente, int estadoPedido, DataTable dtPedidos)
        {

            string sql = @"SELECT ped_codigo, cli_codigo, eped_codigo, ped_fecha_alta, ped_numero                       
                           FROM PEDIDOS WHERE cli_codigo=@p0";

            object[] valorParametros = { CodigoCliente };
            try
            {
                DB.FillDataTable(dtPedidos, sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        private static void ObtenerDetallePedido(Data.dsCliente ds)
        {
            string sql = @"SELECT DPED_CODIGO, PED_CODIGO, EDPED_CODIGO, COC_CODIGO, 
                         DPED_CANTIDAD, DPED_FECHA_CANCELACION, DPED_CODIGONEMONICO,
                         DPED_FECHA_ENTREGA_PREVISTA, DPED_FECHA_ENTREGA_REAL
                         FROM DETALLE_PEDIDOS WHERE PED_CODIGO = @p0";

            object[] valorParametros; ;

            foreach (Data.dsCliente.PEDIDOSRow rowPedido in ds.PEDIDOS)
            {
                valorParametros = new object[] { rowPedido.PED_CODIGO };
                DB.FillDataTable(ds.DETALLE_PEDIDOS, sql, valorParametros);
            }
        }

        //****************************************************************************
        //                      METODOS DE INSERCION
        //****************************************************************************
        public static int Insertar(Entidades.Pedido pedido, DataTable dtDetallePedido)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sqlInsert = @"INSERT INTO [PEDIDOS]
                               ([CLI_CODIGO]
                               ,[EPED_CODIGO]
                               ,[PED_FECHA_ALTA]
                               ,[PED_OBSERVACIONES]
                               ,[PED_NUMERO]) 
                               VALUES (@p0, @p1, @p2, @p3, @p4) SELECT @@Identity";

            //Así obtenemos el pedido nuevo del dataset, indicamos la primer fila de las agregadas ya que es una sola y convertimos al tipo correcto
            object[] valorParametros = { pedido.Cliente.Codigo, 
                                         pedido.EstadoPedido.Codigo, 
                                         pedido.FechaAlta, 
                                         pedido.Observaciones, 
                                         pedido.Numero };

            //Declaramos el objeto transaccion
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Insertamos la pieza y obtenemos su codigo
                pedido.Codigo = Convert.ToInt32(DB.executeScalar(sqlInsert, valorParametros, transaccion));

                //Hacemos update del valor del número de pedido
                pedido.Numero = pedido.Codigo.ToString();
                ActualizarNumero(pedido.Codigo, transaccion); 
                                 
                //Ahora insertamos el detalle, usamos el foreach para recorrer sólo los nuevos registros del datatable
                Entidades.DetallePedido detalle = new GyCAP.Entidades.DetallePedido();
                Entidades.EstadoDetallePedido lEstadoDetalle = new GyCAP.Entidades.EstadoDetallePedido();
                Entidades.Cocina lCocina = new GyCAP.Entidades.Cocina();

                int contador = 0;
                
                foreach (Data.dsCliente.DETALLE_PEDIDOSRow row in (Data.dsCliente.DETALLE_PEDIDOSRow[])dtDetallePedido.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    //Creamos los objetos auxiliares
                    lCocina.CodigoCocina = Convert.ToInt32(row.COC_CODIGO);
                    lEstadoDetalle.Codigo = DAL.EstadoPedidoDAL.ObtenerIDEstadosPedido("Pendiente"); 

                    //Creamos el objeto de detalle de pedido
                    contador += 1;
                    detalle.Pedido = pedido;
                    detalle.Cocina = lCocina;
                    detalle.Cantidad = row.DPED_CANTIDAD;
                    detalle.Estado = lEstadoDetalle;
                    detalle.CodigoNemonico = "P" + pedido.Codigo.ToString() + "-D" + contador.ToString();
                    detalle.FechaEntregaPrevista =Convert.ToDateTime(row.DPED_FECHA_ENTREGA_PREVISTA);
                    
                    if (detalle.Estado.Codigo == DAL.EstadoDetallePedidoDAL.ObtenerCodigoEstado("Pendiente"))
                    {
                        detalle.Codigo = DetallePedidoDAL.Insertar(detalle, transaccion);
                    }
                    else if (detalle.Estado.Codigo == DAL.EstadoDetallePedidoDAL.ObtenerCodigoEstado("Entrega Stock"))
                    {
                        //Insertamos el detalle de pedido
                        detalle.Codigo = DetallePedidoDAL.Insertar(detalle, transaccion);

                        //Ejecutamos el movimiento de stock planificado
                        //Generamos el objeto movimiento de stock
                        Entidades.MovimientoStock movStock = new GyCAP.Entidades.MovimientoStock();
                        
                        //Cargamos el estado del movimiento de stock
                        movStock.Estado = new GyCAP.Entidades.EstadoMovimientoStock(DAL.MovimientoStockDAL.EstadoPlanificado);
                        
                        //Creamos las entidades de Origen, Destino y Dueño del movimiento
                        Entidades.Entidad origen = new GyCAP.Entidades.Entidad();
                        origen.Nombre = DAL.TipoEntidadDAL.UbicacionStockNombre;
                        origen.TipoEntidad.Codigo = DAL.TipoEntidadDAL.GetCodigoTipoEntidad(DAL.TipoEntidadDAL.TipoEntidadEnum.UbicacionStock);
                        origen.EntidadExterna = row.UBICACION_STOCK;
                        DAL.EntidadDAL.GetEntidad(DAL.TipoEntidadDAL.TipoEntidadEnum.UbicacionStock, origen);
                        movStock.Origen = origen;                       

                        Entidades.Entidad destino = new GyCAP.Entidades.Entidad();
                        destino.Nombre = DAL.TipoEntidadDAL.DetallePedidoNombre;
                        destino.TipoEntidad.Codigo =DAL.TipoEntidadDAL.GetCodigoTipoEntidad(DAL.TipoEntidadDAL.TipoEntidadEnum.DetallePedido);
                        destino.EntidadExterna = detalle.Codigo;
                        DAL.EntidadDAL.GetEntidad(DAL.TipoEntidadDAL.TipoEntidadEnum.DetallePedido, destino);
                        movStock.Destino = destino;

                        Entidades.Entidad dueño = new GyCAP.Entidades.Entidad();
                        dueño.Nombre = DAL.TipoEntidadDAL.PedidoNombre;
                        dueño.TipoEntidad.Codigo = DAL.TipoEntidadDAL.GetCodigoTipoEntidad(DAL.TipoEntidadDAL.TipoEntidadEnum.Pedido);
                        dueño.EntidadExterna = row.PED_CODIGO;
                        DAL.EntidadDAL.GetEntidad(DAL.TipoEntidadDAL.TipoEntidadEnum.DetallePedido, destino);
                        movStock.Duenio = dueño;
                                                
                        //Asignamos los demas parametros para el movimiento de stock                     
                        movStock.Descripcion = "Movimiento Pedido";
                        movStock.CantidadOrigenEstimada = Convert.ToInt32(row.DPED_CANTIDAD * -1);
                        movStock.CantidadDestinoEstimada = Convert.ToInt32(row.DPED_CANTIDAD);
                        movStock.FechaPrevista = row.DPED_FECHA_ENTREGA_PREVISTA;

                        //Insertamos el movimiento de stock en la BD
                        DAL.MovimientoStockDAL.InsertarPlanificado(movStock);

                    }

                    //Actualizamos el valor del codigo del Pedido
                    row.BeginEdit();
                    row.PED_CODIGO = detalle.Pedido.Codigo;
                    row.EndEdit();
                }
                
                transaccion.Commit();
                DB.FinalizarTransaccion();

                //Devolvemos el código del pedido para actualizar
                return pedido.Codigo;
            }
            catch (SqlException ex)
            {
                //Error en alguna consulta, descartamos los cambios
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }            
        }

        //Determina si existe una pedido dado su nombre y terminación
        public static bool EsPedido(Entidades.Pedido pedido)
        {
            string sql = @"SELECT count(ped_codigo) FROM PEDIDOS 
                            WHERE PED_NUMERO = @p0 AND PED_codigo = @p1";

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

        //****************************************************************************
        //                      METODO DE ELIMINACION
        //****************************************************************************
        
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

        public static bool PuedeEliminarse(long codigo)
        {
            string sqlDPM = "SELECT count(dped_codigo) FROM DETALLE_PEDIDOS WHERE ped_codigo = @p0";

            object[] valorParametros = { codigo };
            try
            {
                int resultadoDPM = Convert.ToInt32(DB.executeScalar(sqlDPM, valorParametros, null));

                if (resultadoDPM == 0)
                {
                    //AHORA VER LOS DETALLES SI SE PUEDEN BORRAR
                    return true;
                }
                else { return false; }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //****************************************************************************
        //                      METODO DE ACTUALIZACION
        //****************************************************************************

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
                    lPedido.Codigo = Convert.ToInt32(rowPedido.PED_CODIGO);
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
                    lPedido.Codigo = Convert.ToInt32(rowPedido.PED_CODIGO);
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

        public static void ActualizarNumero(int codigo, SqlTransaction transaccion)
        {
                      
            string sqlUpdate = @"UPDATE PEDIDOS SET
                                PED_NUMERO = @p0 WHERE PED_CODIGO = @p0";

            object[] valorParametros = { codigo };
            
            try
            {
                //DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
                DB.executeNonQuery(sqlUpdate, valorParametros, transaccion);
                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                //Error en alguna consulta, descartamos los cambios
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
        }

        //****************************************************************************
        //                      CAMBIOS DE ESTADOS
        //****************************************************************************

        public static void ActualizarEstadoAEnCurso(int codigoPedido, SqlTransaction transaccion)
        {
            ActualizarEstado(codigoPedido, EstadoEnCurso, transaccion);
        }
        
        public static void ActualizarEstado(int codigoPedido, int codigoEstado, SqlTransaction transaccion)
        {
            string sql = "UPDATE PEDIDOS SET eped_codigo = @p0 WHERE ped_codigo = @p1";
            object[] parametros = { codigoEstado, codigoPedido };
            DB.executeNonQuery(sql, parametros, transaccion);
        }

        public static void ActualizarDetallePedidoAEnCurso(int codigoDetalle, SqlTransaction transaccion)
        {
            DetallePedidoDAL.ActualizarEstadoAEnCurso(codigoDetalle, transaccion);

            string sql = "SELECT ped_codigo FROM DETALLE_PEDIDOS WHERE dped_codigo = @p0";
            object[] parametros = { codigoDetalle };
            ActualizarEstadoAEnCurso(Convert.ToInt32(DB.executeScalar(sql, parametros, transaccion)), transaccion);
        }

        public static void ActualizarDetallePedidoAFinalizado(int codigoDetalle, SqlTransaction transaccion)
        {
            DetallePedidoDAL.ActualizarEstadoAFinalizado(codigoDetalle, transaccion);

            string sql = "SELECT ped_codigo FROM DETALLE_PEDIDOS WHERE dped_codigo = @p0";
            object[] parametros = { codigoDetalle };
            int pedido = Convert.ToInt32(DB.executeScalar(sql, parametros, transaccion));

            if (EsPedidoConDetalleFinalizado(pedido, transaccion))
            {
                ActualizarEstado(pedido, EstadoFinalizado, transaccion);
            }
        }

        private static bool EsPedidoConDetalleFinalizado(int codigoPedido, SqlTransaction transaccion)
        {
            string sql = "SELECT COUNT(dped_codigo) FROM DETALLE_PEDIDOS WHERE ped_codigo = @p0 AND edped_codigo <> @p1";
            object[] parametros = { codigoPedido, DetallePedidoDAL.EstadoFinalizado };

            if (Convert.ToInt32(DB.executeScalar(sql, parametros, transaccion)) == 0) { return true; }
            else { return false; }
        }

        public static void CambiarEstadoPedido(int codigoPedido, int estado)
        {
            SqlTransaction transaccion = null;

            try
            {
                //Inserto la demanda
                transaccion = DB.IniciarTransaccion();

                string sql = string.Empty;


                //Guardo las modificaciones
                sql = "UPDATE [PEDIDOS] SET eped_codigo=@p0 WHERE ped_codigo=@p1";
                object[] valorPar = { estado, codigoPedido };
                DB.executeNonQuery(sql, valorPar, transaccion);

                transaccion.Commit();
                DB.FinalizarTransaccion();
            }
            catch (SqlException)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException();
            }
        }       

    }
}
