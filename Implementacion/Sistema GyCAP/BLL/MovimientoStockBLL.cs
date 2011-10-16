using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GyCAP.Entidades;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades.Excepciones;
using System.Data.SqlClient;

namespace GyCAP.BLL
{
    public class MovimientoStockBLL
    {        
        public static void InsertarPlanificado(Entidades.MovimientoStock movimientoStock, SqlTransaction transaccion)
        {
            ValidarMovimiento(movimientoStock);
            DAL.MovimientoStockDAL.InsertarPlanificado(movimientoStock, transaccion);
        }

        public static void InsertarFinalizado(Entidades.MovimientoStock movimientoStock, SqlTransaction transaccion)
        {
            if (transaccion == null)
            {
                try
                {
                    transaccion = DAL.DB.IniciarTransaccion();
                    ValidarMovimiento(movimientoStock);
                    DAL.MovimientoStockDAL.InsertarFinalizado(movimientoStock, transaccion);
                    transaccion.Commit();
                }
                catch (SqlException ex)
                {
                    transaccion.Rollback();
                    throw new BaseDeDatosException(ex.Message);
                }
                finally
                {
                    DAL.DB.FinalizarTransaccion();
                }
            }
            else
            {
                transaccion = DAL.DB.IniciarTransaccion();
                ValidarMovimiento(movimientoStock);
                DAL.MovimientoStockDAL.InsertarFinalizado(movimientoStock, transaccion);
            }
        }

        public static void Iniciar(MovimientoStock movimiento, SqlTransaction transaccion)
        {
            ValidarMovimiento(movimiento);
            DAL.MovimientoStockDAL.IniciarMovimiento(movimiento, transaccion);
        }

        public static void Finalizar(MovimientoStock movimientoStock, SqlTransaction transaccion)
        {
            ValidarMovimiento(movimientoStock);
            DAL.MovimientoStockDAL.FinalizarMovimiento(movimientoStock, transaccion);
        }

        public static void EliminarMovimientosPedido(int codigoEntidad)
        {
            //DAL.MovimientoStockDAL.EliminarMovimientosPedido(codigoEntidad, null);
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

        public static void ObtenerTodos(object fechaDesde, object fechaHasta, object origen, object destino, object estado, Data.dsStock.MOVIMIENTOS_STOCKDataTable dt)
        {
            if (origen != null && Convert.ToInt32(origen) < 0) { origen = null; }
            if (destino != null && Convert.ToInt32(destino) < 0) { destino = null; }
            if (estado != null && Convert.ToInt32(estado) <= 0) { estado = null; }
            if (fechaDesde != null) { fechaDesde = DateTime.Parse(fechaDesde.ToString()).ToString("yyyyMMdd"); }
            if (fechaHasta != null) { fechaHasta = DateTime.Parse(fechaHasta.ToString()).ToString("yyyyMMdd"); }
            DAL.MovimientoStockDAL.ObtenerTodos(fechaDesde, fechaHasta, origen, destino, estado, dt);
        }

        public static IList<MovimientoStock> ObtenerTodos(object fechaDesde, object fechaHasta, object origen, object destino, object estado)
        {
            Data.dsStock.MOVIMIENTOS_STOCKDataTable dt = new GyCAP.Data.dsStock.MOVIMIENTOS_STOCKDataTable();
            ObtenerTodos(fechaDesde, fechaHasta, origen, destino, estado, dt);
            IList<MovimientoStock> lista = new List<MovimientoStock>();

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
                    movimiento.Estado = EstadoMovimientoStockBLL.GetEstadoEntity(Convert.ToInt32(row.EMVTO_CODIGO));                    
                    movimiento.CantidadDestinoEstimada = row.MVTO_CANTIDAD_DESTINO_ESTIMADA;                    
                    movimiento.CantidadDestinoReal = row.MVTO_CANTIDAD_DESTINO_REAL;
                    movimiento.OrigenesMultiples = GetOrigenesMovimiento(Convert.ToInt32(row.MVTO_NUMERO));
                    movimiento.Destino = (!row.IsENTD_DESTINONull()) ? EntidadBLL.GetEntidad(Convert.ToInt32(row.ENTD_DESTINO)) : null;
                    movimiento.Duenio = (!row.IsENTD_DUENIONull()) ? EntidadBLL.GetEntidad(Convert.ToInt32(row.ENTD_DUENIO)) : null;

                    lista.Add(movimiento);
            	}
            }

            return lista;
        }

        private static IList<OrigenMovimiento> GetOrigenesMovimiento(int numeroMovimiento)
        {
            Data.dsStock.ORIGENES_MOVIMIENTO_STOCKDataTable dt = DAL.MovimientoStockDAL.ObtenerOrigenes(numeroMovimiento);
            IList<OrigenMovimiento> lista = new List<OrigenMovimiento>();

            foreach (Data.dsStock.ORIGENES_MOVIMIENTO_STOCKRow row in dt.Rows)
            {
                lista.Add(new OrigenMovimiento()
                {
                    Codigo = Convert.ToInt32(row.OM_CODIGO),
                    CantidadEstimada = row.OM_CANTIDAD_ESTIMADA,
                    CantidadReal = row.OM_CANTIDAD_REAL,
                    MovimientoStock = Convert.ToInt32(row.MVTO_NUMERO),
                    Entidad = EntidadBLL.GetEntidad(Convert.ToInt32(row.ENTD_CODIGO)),
                    FechaPrevista = row.OM_FECHAPREVISTA
                });
            }

            return lista;
        }

        public static IList<MovimientoStock> ObtenerMovimientosUbicacionStock(object fechaDesde, object fechaHasta, object numeroStock)
        {
            IList<MovimientoStock> lista = new List<MovimientoStock>();

            if (numeroStock == null) { return lista; }

            int codigoEntidad = EntidadBLL.GetEntidad(EntidadEnum.TipoEntidadEnum.UbicacionStock, Convert.ToInt32(numeroStock), null).Codigo;
            
            Data.dsStock.MOVIMIENTOS_STOCKDataTable dt = DAL.MovimientoStockDAL.ObtenerMovimientosUbicacionStock(fechaDesde, fechaHasta, codigoEntidad);

            if (dt.Rows.Count > 0)
            {
                foreach (Data.dsStock.MOVIMIENTOS_STOCKRow row in dt.Rows)
                {
                    MovimientoStock movimiento = new MovimientoStock();

                    movimiento.Numero = Convert.ToInt32(row.MVTO_NUMERO);
                    movimiento.Codigo = row.MVTO_CODIGO;
                    movimiento.Descripcion = row.MVTO_DESCRIPCION;
                    movimiento.FechaAlta = row.MVTO_FECHAALTA;
                    if (!row.IsMVTO_FECHAPREVISTANull()) { movimiento.FechaPrevista = row.MVTO_FECHAPREVISTA; }
                    if (!row.IsMVTO_FECHAREALNull()) { movimiento.FechaReal = row.MVTO_FECHAREAL; }
                    movimiento.Estado = EstadoMovimientoStockBLL.GetEstadoEntity(Convert.ToInt32(row.EMVTO_CODIGO));                    
                    movimiento.CantidadDestinoEstimada = row.MVTO_CANTIDAD_DESTINO_ESTIMADA;                    
                    movimiento.CantidadDestinoReal = row.MVTO_CANTIDAD_DESTINO_REAL;
                    movimiento.OrigenesMultiples = GetOrigenesMovimiento(Convert.ToInt32(row.MVTO_NUMERO));
                    movimiento.Destino = (!row.IsENTD_DESTINONull()) ? EntidadBLL.GetEntidad(Convert.ToInt32(row.ENTD_DESTINO)) : null;
                    movimiento.Duenio = (!row.IsENTD_DUENIONull()) ? EntidadBLL.GetEntidad(Convert.ToInt32(row.ENTD_DUENIO)) : null;

                    lista.Add(movimiento);
                }
            }

            return lista;
        }

        private static void ValidarMovimiento(MovimientoStock movimiento)
        {
            //Fechas
            //if (movimiento.FechaAlta.CompareTo(DateTime.Today) > 0) { throw new MovimientoMalConfiguradoException("La fecha de alta es mayor que la fecha del presente día."); }
            //if (movimiento.FechaReal.HasValue && movimiento.FechaReal.Value.CompareTo(DateTime.Today) > 0) { throw new MovimientoMalConfiguradoException("La fecha real es mayor que la fecha del presente día."); }
            //if (movimiento.FechaPrevista.HasValue && movimiento.FechaPrevista.Value.CompareTo(DateTime.Today) < 0) { throw new MovimientoMalConfiguradoException("La fecha prevista es menor que la fecha del presente día."); }

            //Origen, destino y dueño
            if (movimiento.OrigenesMultiples.Count == 0) { throw new MovimientoMalConfiguradoException("El origen es nulo."); }
            if (movimiento.Destino == null) { throw new MovimientoMalConfiguradoException("El destino es nulo."); }
            foreach (OrigenMovimiento item in movimiento.OrigenesMultiples)
            {
                if (item.Entidad.Equals(movimiento.Destino)) { throw new MovimientoMalConfiguradoException("El origen y el destino son iguales."); }
            }
            
            if (movimiento.Duenio == null) { throw new MovimientoMalConfiguradoException("El dueño es nulo."); }
            if (movimiento.Estado == null) { throw new MovimientoMalConfiguradoException("El estado es nulo"); }
        }

        public static MovimientoStock GetMovimientoConfigurado(StockEnum.CodigoMovimiento codigo, StockEnum.EstadoMovimientoStock estado)
        {
            MovimientoStock movimiento = new MovimientoStock();
            movimiento.Numero = 0;
            movimiento.Codigo = codigo.ToString();
            movimiento.Descripcion = "poner descripcion gonzalo";
            movimiento.CantidadDestinoReal = 0;
            movimiento.Estado = EstadoMovimientoStockBLL.GetEstadoEntity(estado);
            movimiento.FechaAlta = DAL.DB.GetFechaServidor();
            movimiento.OrigenesMultiples = new List<OrigenMovimiento>();
            return movimiento;
        }

        public static IList<MovimientoStock> GetMovimientosByOwner(Entidad owner)
        {
            Data.dsStock.MOVIMIENTOS_STOCKDataTable dt = new GyCAP.Data.dsStock.MOVIMIENTOS_STOCKDataTable();
            Data.dsStock.ORIGENES_MOVIMIENTO_STOCKDataTable dtOrigenes = new GyCAP.Data.dsStock.ORIGENES_MOVIMIENTO_STOCKDataTable();
            DAL.MovimientoStockDAL.GetMovimientosByOwner(owner, dt);
            IList<MovimientoStock> listaMovimientos = new List<MovimientoStock>();
            
            foreach (Data.dsStock.MOVIMIENTOS_STOCKRow rowMVTO in dt.Rows)
            {
                MovimientoStock mvto = new MovimientoStock();
                mvto.Numero = Convert.ToInt32(rowMVTO.MVTO_NUMERO);
                mvto.Codigo = rowMVTO.MVTO_CODIGO;
                mvto.Descripcion = rowMVTO.MVTO_DESCRIPCION;
                mvto.FechaAlta = rowMVTO.MVTO_FECHAALTA;
                if (!rowMVTO.IsMVTO_FECHAPREVISTANull()) { mvto.FechaPrevista = rowMVTO.MVTO_FECHAPREVISTA; }
                if (!rowMVTO.IsMVTO_FECHAREALNull()) { mvto.FechaReal = rowMVTO.MVTO_FECHAREAL; }
                mvto.Estado = EstadoMovimientoStockBLL.GetEstadoEntity(Convert.ToInt32(rowMVTO.EMVTO_CODIGO));
                mvto.CantidadDestinoEstimada = rowMVTO.MVTO_CANTIDAD_DESTINO_ESTIMADA;
                mvto.CantidadDestinoReal = rowMVTO.MVTO_CANTIDAD_DESTINO_REAL;
                mvto.OrigenesMultiples = GetOrigenesMovimiento(Convert.ToInt32(rowMVTO.MVTO_NUMERO));
                mvto.Destino = (!rowMVTO.IsENTD_DESTINONull()) ? EntidadBLL.GetEntidad(Convert.ToInt32(rowMVTO.ENTD_DESTINO)) : null;
                mvto.Duenio = owner;

                listaMovimientos.Add(mvto);
            }

            return listaMovimientos;
        }
    }
}
