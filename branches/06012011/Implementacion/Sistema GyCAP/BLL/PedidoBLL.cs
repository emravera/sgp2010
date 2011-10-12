using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using GyCAP.Entidades;
using GyCAP.Entidades.Enumeraciones;

namespace GyCAP.BLL
{
    public class PedidoBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerPedido(object nombre, object numero, int idEstadoPedido, object fechaDesde, object fechaHasta, Data.dsCliente ds , bool obtenerDetalle)
        {
            DAL.PedidoDAL.ObtenerPedido(nombre, numero, idEstadoPedido, fechaDesde, fechaHasta, ds, obtenerDetalle);
        }

        //Metodo que obtiene un pedido a partir de una fecha y para un cliente determinado
        public static void ObtenerPedidoCliente(int codigoCliente, int estadoPedido, DataTable dtPedido)
        {
            DAL.PedidoDAL.ObtenerPedidosCliente(codigoCliente, estadoPedido, dtPedido);
        }

        //Metodo que obtiene los pedidos a partir de una fecha
        public static void ObtenerPedidoFecha(DateTime fecha, DataTable dtPedidos)
        {
            DAL.PedidoDAL.ObtenerPedidoFecha(fecha, dtPedidos);
        }
        
        //Metodo para insertar un pedido y su detalle
        public static int Insertar(Entidades.Pedido pedido, DataTable dtDetallePedido)
        {
            int codigoPedido = 0;

            if (EsPedido(pedido)) throw new Entidades.Excepciones.ElementoExistenteException();
            else
            {
                SqlTransaction transaccion = null;

                try
                {
                    //Inicamos la transaccion
                    transaccion = DAL.DB.IniciarTransaccion();
                    
                    //Creamos el pedido y su detalle
                    codigoPedido = DAL.PedidoDAL.Insertar(pedido, dtDetallePedido, transaccion);

                    foreach (Data.dsCliente.DETALLE_PEDIDOSRow row in (Data.dsCliente.DETALLE_PEDIDOSRow[])dtDetallePedido.Select(null, null, System.Data.DataViewRowState.Added))
                    {
                        if (Convert.ToInt32(row.EDPED_CODIGO) == DAL.EstadoDetallePedidoDAL.ObtenerCodigoEstado("Entrega Stock"))
                        {
                            MovimientoStock movimiento = MovimientoStockBLL.GetMovimientoConfigurado(StockEnum.CodigoMovimiento.DetallePedido, StockEnum.EstadoMovimientoStock.Planificado);
                            movimiento.OrigenesMultiples.Add(new OrigenMovimiento()
                                                                  {
                                                                      CantidadEstimada = Convert.ToInt32(row.DPED_CANTIDAD),
                                                                      Entidad = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.UbicacionStock, Convert.ToInt32(row.UBICACION_STOCK), transaccion),
                                                                      FechaPrevista = Convert.ToDateTime(row.DPED_FECHA_ENTREGA_PREVISTA),
                                                                  });
                            Entidad detalle = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.DetallePedido, Convert.ToInt32(row.DPED_CODIGO), transaccion);
                            movimiento.Destino = detalle;
                            movimiento.CantidadDestinoEstimada = row.DPED_CANTIDAD;
                            movimiento.Codigo = StockEnum.CodigoMovimiento.DetallePedido.ToString();
                            movimiento.Duenio = detalle;
                            movimiento.FechaPrevista = row.DPED_FECHA_ENTREGA_PREVISTA;
                            movimiento.Descripcion = "Movimiento Detalle pedido: " + row.DPED_CODIGONEMONICO.ToString();
                            
                            MovimientoStockBLL.InsertarPlanificado(movimiento, transaccion);
                        }                    
                    }

                    //Finalizar la transaccion
                    transaccion.Commit();           
                }
                catch (SqlException ex)
                {
                    transaccion.Rollback();
                    throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
                }
                finally
                {
                    DAL.DB.FinalizarTransaccion();
                }
            }
            
            return codigoPedido;           
        }

        //Comprueba si existe una pieza dado su nombre y terminación
        public static bool EsPedido(Entidades.Pedido pedido)
        {
            return DAL.PedidoDAL.EsPedido(pedido);
        }
        
        //Metodo para modificar un pedido y su detalle
        public static void Actualizar(Entidades.Pedido pedido, DataTable dtDetallePedido)
        {
            SqlTransaction transaccion = null;

            try
            {
                //Inicamos la transaccion
                transaccion = DAL.DB.IniciarTransaccion();
                
                //Actualizamos el pedido y su detalle
                DAL.PedidoDAL.Actualizar(pedido, dtDetallePedido, transaccion);

                foreach (Data.dsCliente.DETALLE_PEDIDOSRow row in (Data.dsCliente.DETALLE_PEDIDOSRow[])dtDetallePedido.Select(null, null, System.Data.DataViewRowState.CurrentRows))
                {
                    if (Convert.ToInt32(row.EDPED_CODIGO) == DAL.EstadoDetallePedidoDAL.ObtenerCodigoEstado("Entrega Stock"))
                    {
                        //Eliminamos los movimientos de stock que estan planificados
                        DAL.MovimientoStockDAL.EliminarMovimientosPedido(Convert.ToInt32(row.PED_CODIGO), transaccion);
                        
                        MovimientoStock movimiento = MovimientoStockBLL.GetMovimientoConfigurado(StockEnum.CodigoMovimiento.DetallePedido, StockEnum.EstadoMovimientoStock.Planificado);
                        movimiento.OrigenesMultiples.Add(new OrigenMovimiento()
                                                              {
                                                                  CantidadEstimada = Convert.ToInt32(row.DPED_CANTIDAD),
                                                                  Entidad = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.UbicacionStock, Convert.ToInt32(row.UBICACION_STOCK), transaccion),
                                                              });
                        Entidad detalle = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.DetallePedido, Convert.ToInt32(row.DPED_CODIGO), transaccion);
                        movimiento.Destino = detalle;
                        movimiento.CantidadDestinoEstimada = row.DPED_CANTIDAD;
                        movimiento.Codigo = StockEnum.CodigoMovimiento.DetallePedido.ToString();
                        movimiento.Duenio = detalle;
                        movimiento.FechaPrevista = row.DPED_FECHA_ENTREGA_PREVISTA;
                        movimiento.Descripcion = "Movimiento Detalle pedido: " + row.DPED_CODIGONEMONICO.ToString();
                        
                        MovimientoStockBLL.InsertarPlanificado(movimiento, transaccion);
                    }                    
                }

                //Finalizar la transaccion
                transaccion.Commit();           
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
            finally
            {
                DAL.DB.FinalizarTransaccion();
            }
        }          
        

        //Metodo que elimina un pedido y su detalle
        public static void Eliminar(int codigo, DataTable dtDetallePedido)
        {
            //Puede eliminarse
            DAL.PedidoDAL.Eliminar(codigo);                  
        }

        //Metodo que cambia el estado del pedido
        public static void MovimientoStockPlanificado(Data.dsCliente.DETALLE_PEDIDOSRow row)
        {
            //DAL.PedidoDAL.MovimientoStockPlanificado(null, row);
        }        

        //Metodo que cambia el estado del pedido
        public static void CambiarEstadoPedido(int codigoPedido, int estado)
        {
            DAL.PedidoDAL.CambiarEstadoPedido(codigoPedido, estado);
        }        
    }
}
