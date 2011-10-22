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
    public class EntregaProductoBLL
    {
        //Metodo que recibe el codigo del cliente
        public static void ObtenerEntregas(int codigoCliente, DataTable dtEntregas)
        {
            DAL.EntregaProductoDAL.ObtenerEntregas(codigoCliente, dtEntregas);
        }

        public static void ObtenerEntregas(DateTime fechaEntrega, DataTable dtEntregas)
        {
            DAL.EntregaProductoDAL.ObtenerEntregas(fechaEntrega, dtEntregas);
        }

        public static void ObtenerEntregas(int codigoCliente, DateTime fechaEntrega, DataTable dtEntregas)
        {
            DAL.EntregaProductoDAL.ObtenerEntregas(codigoCliente,fechaEntrega, dtEntregas);
        }

        public static void ObtenerDetalleEntrega(int idEntrega, DataTable dtEntregas)
        {
            DAL.EntregaProductoDAL.ObtenerDetalleEntrega(idEntrega, dtEntregas);
        }

        //Metodo que guarda la entrega del producto
        public static void GuardarEntrega(Entidades.EntregaProducto entrega, DataTable dtDetalleEntrega)
        {
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DAL.DB.IniciarTransaccion();
                
                //Se guarda la entrega de los productos terminados
                DAL.EntregaProductoDAL.GuardarEntrega(entrega, dtDetalleEntrega, transaccion);
                
                //Se generan los movimientos de stock pertinentes
                foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow[])dtDetalleEntrega.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    int estadoDetalle = BLL.EstadoDetallePedidoBLL.ObtenerEstadoDetalle(Convert.ToInt32(row.DPED_CODIGO));

                    if(estadoDetalle == BLL.EstadoDetallePedidoBLL.ObtenerCodigoEstado("Finalizado"))
                    {
                        //Se tiene que generar un nuevo movimiento de stock para la entrega de pedido
                        MovimientoStock movimiento = MovimientoStockBLL.GetMovimientoConfigurado(StockEnum.CodigoMovimiento.DetallePedido, StockEnum.EstadoMovimientoStock.Finalizado);
                        movimiento.OrigenesMultiples.Add(new OrigenMovimiento()
                        {
                            CantidadEstimada = Convert.ToInt32(row.DENT_CANTIDAD),
                            CantidadReal = Convert.ToInt32(row.DENT_CANTIDAD),
                            Entidad = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.UbicacionStock, Convert.ToInt32(row.DENT_CONTENIDO), transaccion),
                            FechaReal = DBBLL.GetFechaServidor(),
                            FechaPrevista = DBBLL.GetFechaServidor(),
                        });
                        
                        Entidad detalle = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.DetallePedido, Convert.ToInt32(row.DPED_CODIGO), transaccion);
                        movimiento.Destino = detalle;
                        movimiento.CantidadDestinoReal = row.DENT_CANTIDAD;
                        movimiento.Codigo = StockEnum.CodigoMovimiento.DetallePedido.ToString();
                        movimiento.Duenio = detalle;
                        movimiento.FechaReal = DBBLL.GetFechaServidor();
                        movimiento.Descripcion = "Entrega de Detalle Pedido Número:" + row.DPED_CODIGO.ToString();

                        MovimientoStockBLL.InsertarFinalizado(movimiento, transaccion);
                    }
                    else if(estadoDetalle == BLL.EstadoDetallePedidoBLL.ObtenerCodigoEstado("Entrega Stock"))
                    {
                        //Obtenemos el detalle del pedido 
                        Entidades.Entidad detalle = BLL.EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.DetallePedido, Convert.ToInt32(row.DPED_CODIGO) , null);

                        IList<Entidades.MovimientoStock> movimiento = BLL.MovimientoStockBLL.GetMovimientosByOwner(detalle);
                        movimiento.First().CantidadDestinoReal = movimiento.First().CantidadDestinoEstimada;
                        movimiento.First().FechaReal = DBBLL.GetFechaServidor();

                        //Finalizamos el movimiento de stock que ya fue generado
                        BLL.MovimientoStockBLL.Finalizar(movimiento.First(), transaccion);
                    }
                }             


                transaccion.Commit();
                DAL.DB.FinalizarTransaccion();           
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);

            }

            
        }
        //Metodo que guarda la entrega modificada
        public static void GuardarEntregaModificada(Entidades.EntregaProducto entrega, Data.dsEntregaProducto dsEntregaProducto)
        {
            DAL.EntregaProductoDAL.GuardarEntregaModificada(entrega, dsEntregaProducto);
        }
        //Metodo que elimina la entrega de la BD
        public static void EliminarEntrega(int codigoEntrega)
        {
            DAL.EntregaProductoDAL.EliminarEntrega(codigoEntrega);
        }


    }
}
