﻿using System;
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
            if (origen != null && Convert.ToInt32(origen) < 0) { origen = null; }
            if (destino != null && Convert.ToInt32(destino) < 0) { destino = null; }
            if (estado != null && Convert.ToInt32(estado) <= 0) { estado = null; }
            if (fechaDesde != null) { fechaDesde = DateTime.Parse(fechaDesde.ToString()).ToString("yyyyMMdd"); }
            if (fechaHasta != null) { fechaHasta = DateTime.Parse(fechaHasta.ToString()).ToString("yyyyMMdd"); }
            DAL.MovimientoStockDAL.ObtenerTodos(fechaDesde, fechaHasta, origen, destino, estado, dt);
        }

        public static IList<Entidades.MovimientoStock> ObtenerTodos(object fechaDesde, object fechaHasta, object origen, object destino, object estado)
        {
            Data.dsStock.MOVIMIENTOS_STOCKDataTable dt = new GyCAP.Data.dsStock.MOVIMIENTOS_STOCKDataTable();
            ObtenerTodos(fechaDesde, fechaHasta, origen, destino, estado, dt);
            IList<Entidades.MovimientoStock> lista = new List<Entidades.MovimientoStock>();

            if (dt.Rows.Count > 0)
            {
                foreach (Data.dsStock.MOVIMIENTOS_STOCKRow row in dt.Rows)
	            {
                    MovimientoStock movimiento = new MovimientoStock();

                    movimiento.Numero = Convert.ToInt32(row.MVTO_NUMERO);
                    movimiento.Codigo = row.MVTO_CODIGO;
                    movimiento.Descripcion = row.MVTO_DESCRIPCION;
                    movimiento.FechaAlta = row.MVTO_FECHAALTA;
                    if(!row.IsMVTO_FECHAPREVISTANull()) { movimiento.FechaPrevista = row.MVTO_FECHAPREVISTA; }
                    if (!row.IsMVTO_FECHAREALNull()) { movimiento.FechaReal = row.MVTO_FECHAREAL; }
                    movimiento.Estado = BLL.EstadoMovimientoStockBLL.GetEstadoEntity(Convert.ToInt32(row.EMVTO_CODIGO));
                    movimiento.CantidadOrigenEstimada = row.MVTO_CANTIDAD_ORIGEN_ESTIMADA;
                    movimiento.CantidadDestinoEstimada = row.MVTO_CANTIDAD_DESTINO_ESTIMADA;
                    movimiento.CantidadOrigenReal = row.MVTO_CANTIDAD_ORIGEN_REAL;
                    movimiento.CantidadDestinoReal = row.MVTO_CANTIDAD_DESTINO_REAL;
                    movimiento.Origen = (!row.IsENTD_ORIGENNull()) ? BLL.EntidadBLL.GetEntidad(Convert.ToInt32(row.ENTD_ORIGEN)) : null;
                    movimiento.Destino = (!row.IsENTD_DESTINONull()) ? BLL.EntidadBLL.GetEntidad(Convert.ToInt32(row.ENTD_DESTINO)) : null;
                    movimiento.Duenio = (!row.IsENTD_DUENIONull()) ? BLL.EntidadBLL.GetEntidad(Convert.ToInt32(row.ENTD_DUENIO)) : null;

                    lista.Add(movimiento);
            	}
            }

            return lista;
        }
    }
}
