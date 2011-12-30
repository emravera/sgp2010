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
                IList<Entidades.MovimientoStock> movimientos = new List<MovimientoStock>();

                //Se generan los movimientos de stock pertinentes
                foreach (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow row in (Data.dsEntregaProducto.DETALLE_ENTREGA_PRODUCTORow[])dtDetalleEntrega.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    //Obtenemos el detalle del pedido
                    Entidades.Entidad detalle = BLL.EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.DetallePedido, Convert.ToInt32(row.DPED_CODIGO), transaccion);
                    movimientos.Concat(BLL.MovimientoStockBLL.GetMovimientosByOwner(detalle));
                }
                
                foreach (MovimientoStock movimiento in movimientos)
                {
                    movimiento.CantidadDestinoReal = movimiento.CantidadDestinoEstimada;
                    movimiento.FechaReal = DBBLL.GetFechaServidor();

                    //Finalizamos el movimiento de stock que ya fue generado
                    BLL.MovimientoStockBLL.Finalizar(movimiento, transaccion, true);
                }

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
