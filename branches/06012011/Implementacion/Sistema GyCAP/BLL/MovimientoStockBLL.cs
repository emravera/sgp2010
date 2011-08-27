using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GyCAP.Entidades;

namespace GyCAP.BLL
{
    public class MovimientoStockBLL
    {
        public const string CodigoManual = "AM";
        public const string CodigoPedido = "APED";
        public const string CodigoDetallePedido = "ADPED";
        public const string CodigoMantenimiento = "MNT";
        public const string CodigoOrdenProduccion = "ORDP";
        public const string CodigoOrdenTrabajo = "ORDT";
        public const string CodgioOrdenCompra = "ORDC";
        
        public static void InsertarPlanificado(Entidades.MovimientoStock movimientoStock)
        {
            movimientoStock.Estado = BLL.EstadoMovimientoStockBLL.GetEstadoEntity(BLL.EstadoMovimientoStockBLL.Planificado);
            DAL.MovimientoStockDAL.InsertarPlanificado(movimientoStock);
        }

        public static void InsertarFinalizado(Entidades.MovimientoStock movimientoStock)
        {
            movimientoStock.Estado = BLL.EstadoMovimientoStockBLL.GetEstadoEntity(BLL.EstadoMovimientoStockBLL.Finalizado);
            DAL.MovimientoStockDAL.InsertarFinalizado(movimientoStock);
        }

        public static void Finalizar(Entidades.MovimientoStock movimientoStock)
        {
            if (DAL.MovimientoStockDAL.EsFinalizado(movimientoStock.Numero)) { return; }
            DAL.MovimientoStockDAL.Finalizar(movimientoStock, null);
        }
        
        public static void Cancelar(int numeroMovimiento)
        {
            if (DAL.MovimientoStockDAL.EsFinalizado(numeroMovimiento)) { throw new Entidades.Excepciones.ElementoFinalizadoException(); }
            DAL.MovimientoStockDAL.Cancelar(numeroMovimiento);
        }
        
        public static void Eliminar(int numeroMovimiento)
        {
            if (!DAL.MovimientoStockDAL.PuedeEliminarse(numeroMovimiento)) { throw new Entidades.Excepciones.ElementoFinalizadoException(); }
            DAL.MovimientoStockDAL.Eliminar(numeroMovimiento);
        }

        public static MovimientoStock GenerarMovimiento()
        {
            MovimientoStock movimiento = new MovimientoStock();

            return movimiento;
        }

        public static void ObtenerTodos(object fechaDesde, object fechaHasta, object origen, object destino, object estado, Data.dsStock.MOVIMIENTOS_STOCKDataTable dt)
        {
            if (origen != null && Convert.ToInt32(origen) <= 0) { origen = null; }
            if (destino != null && Convert.ToInt32(destino) <= 0) { destino = null; }
            if (estado != null && Convert.ToInt32(estado) <= 0) { estado = null; }
            if (fechaDesde != null) { fechaDesde = DateTime.Parse(fechaDesde.ToString()).ToString("yyyyMMdd"); }
            if (fechaHasta != null) { fechaHasta = DateTime.Parse(fechaHasta.ToString()).ToString("yyyyMMdd"); }
            DAL.MovimientoStockDAL.ObtenerTodos(fechaDesde, fechaHasta, origen, destino, estado, dt);
        }
    }
}
