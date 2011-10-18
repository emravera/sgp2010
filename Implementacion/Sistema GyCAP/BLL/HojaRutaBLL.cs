using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GyCAP.Entidades;
using GyCAP.Entidades.BindingEntity;
using GyCAP.Entidades.Enumeraciones;

namespace GyCAP.BLL
{
    public class HojaRutaBLL
    {
        public static readonly int hojaRutaActiva = 1;
        public static readonly int hojaRutaInactiva = 0;

        public static void ObtenerHojasRuta(object nombre, object activa, Data.dsHojaRuta dsHojaRuta, bool obtenerDetalle)
        {
            object estado = null;
            if (activa != null)
            {
                if(Convert.ToInt32(activa.ToString()) == hojaRutaActiva || Convert.ToInt32(activa.ToString()) == hojaRutaInactiva) { estado = activa; };
            }
            DAL.HojaRutaDAL.ObtenerHojasRuta(nombre, estado, dsHojaRuta, obtenerDetalle);
        }

        public static void ObtenerHojasRuta(DataTable dtHojasRuta)
        {
            DAL.HojaRutaDAL.ObtenerHojasRuta(dtHojasRuta);
        }

        public static SortableBindingList<HojaRuta> GetAll()
        {
            Data.dsHojaRuta ds = new GyCAP.Data.dsHojaRuta();
            SortableBindingList<HojaRuta> lista = new SortableBindingList<HojaRuta>();
            ObtenerHojasRuta(null, null, ds, true);
            SortableBindingList<CentroTrabajo> centros = CentroTrabajoBLL.GetAll();
            SortableBindingList<OperacionFabricacion> operaciones = OperacionBLL.GetAll();

            foreach (Data.dsHojaRuta.HOJAS_RUTARow row in ds.HOJAS_RUTA.Rows)
            {
                HojaRuta hoja = new HojaRuta();
                hoja.Codigo = Convert.ToInt32(row.HR_CODIGO);
                hoja.Descripcion = row.HR_DESCRIPCION;
                hoja.Estado = Convert.ToInt32(row.HR_ACTIVO);
                hoja.FechaAlta = row.HR_FECHAALTA;
                hoja.Nombre = row.HR_NOMBRE;
                hoja.Detalle = new List<DetalleHojaRuta>();
                hoja.UbicacionStock = (row.IsUSTCK_NUMERONull()) ? null : new UbicacionStock() { Numero = Convert.ToInt32(row.USTCK_NUMERO) };

                foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow rowhr in row.GetDETALLE_HOJARUTARows())
                {
                    hoja.Detalle.Add(new DetalleHojaRuta()
                    {
                        Codigo = Convert.ToInt32(rowhr.DHR_CODIGO),
                        Secuencia = Convert.ToInt32(rowhr.DHR_SECUENCIA),
                        CentroTrabajo = centros.Where(p => p.Codigo == Convert.ToInt32(rowhr.CTO_CODIGO)).Single(),
                        Operacion = operaciones.Where(p => p.Codigo == Convert.ToInt32(rowhr.OPR_NUMERO)).Single(),
                        StockDestino = (rowhr.IsUSTCK_DESTINONull()) ? null : new UbicacionStock() { Numero = Convert.ToInt32(rowhr.USTCK_DESTINO) },
                        StockOrigen = (rowhr.IsUSTCK_ORIGENNull()) ? null : new UbicacionStock() { Numero = Convert.ToInt32(rowhr.USTCK_ORIGEN) }
                    });
                }

                lista.Add(hoja);
            }

            return lista;
        }

        public static int Insertar(Data.dsHojaRuta dsHojaRuta)
        {
            Data.dsHojaRuta.HOJAS_RUTARow rowhoja = dsHojaRuta.HOJAS_RUTA.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsHojaRuta.HOJAS_RUTARow;
            if (DAL.HojaRutaDAL.EsHojaRuta(rowhoja.HR_NOMBRE, Convert.ToInt32(rowhoja.HR_CODIGO))) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            int codigo = DAL.HojaRutaDAL.Insertar(dsHojaRuta);
            
            //código temporal para la presentación, quitar - gonzalo
            DAL.PresentacionDAL.AsignarHojaRutaAParteCocina(codigo);

            return codigo;
        }
        
        public static void Actualizar(Data.dsHojaRuta dsHojaRuta)
        {
            Data.dsHojaRuta.HOJAS_RUTARow rowhoja = dsHojaRuta.HOJAS_RUTA.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsHojaRuta.HOJAS_RUTARow;
            if (DAL.HojaRutaDAL.EsHojaRuta(rowhoja.HR_NOMBRE, Convert.ToInt32(rowhoja.HR_CODIGO))) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            DAL.HojaRutaDAL.Actualizar(dsHojaRuta);
        }

        public static void Eliminar(int codigoHojaRuta)
        {
            if (!DAL.HojaRutaDAL.PuedeEliminarse(codigoHojaRuta)) throw new Entidades.Excepciones.ElementoEnTransaccionException();
            DAL.HojaRutaDAL.Eliminar(codigoHojaRuta);
        }

        public static HojaRuta AsHojaRutaEntity(int codigo, Data.dsHojaRuta ds)
        {
            Data.dsHojaRuta.HOJAS_RUTARow row = ds.HOJAS_RUTA.FindByHR_CODIGO(codigo);

            Entidades.HojaRuta hoja = new GyCAP.Entidades.HojaRuta()
            {
                Codigo = codigo,
                Nombre = row.HR_NOMBRE,
                Descripcion = row.HR_DESCRIPCION,
                FechaAlta = row.HR_FECHAALTA,
                Estado = Convert.ToInt32(row.HR_ACTIVO),
                UbicacionStock = (row.IsUSTCK_NUMERONull()) ? null : UbicacionStockBLL.AsUbicacionStock(Convert.ToInt32(row.USTCK_NUMERO), ds),
                Detalle = AsDetalleHojaRuta(Convert.ToInt32(row.HR_CODIGO), ds)
            };

            return hoja;
        }

        private static IList<DetalleHojaRuta> AsDetalleHojaRuta(int codigoHojaRuta, Data.dsHojaRuta ds)
        {
            IList<DetalleHojaRuta> detalle = new List<DetalleHojaRuta>();
            
            foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow row in (Data.dsHojaRuta.DETALLE_HOJARUTARow[])ds.DETALLE_HOJARUTA.Select("hr_codigo = " + codigoHojaRuta))
            {
                detalle.Add(new DetalleHojaRuta()
                {
                    Codigo = Convert.ToInt32(row.DHR_CODIGO),
                    Secuencia = Convert.ToInt32(row.DHR_SECUENCIA),
                    StockDestino = (row.IsUSTCK_DESTINONull()) ? null : UbicacionStockBLL.AsUbicacionStock(Convert.ToInt32(row.USTCK_DESTINO), ds),
                    StockOrigen = (row.IsUSTCK_ORIGENNull()) ? null : UbicacionStockBLL.AsUbicacionStock(Convert.ToInt32(row.USTCK_ORIGEN), ds),
                    Operacion = OperacionBLL.AsOperacionFabricacionEntity(row.OPERACIONESRow),
                    CentroTrabajo = CentroTrabajoBLL.AsCentroTrabajoEntity(Convert.ToInt32(row.CTO_CODIGO), ds)
                });
            }

            return detalle;
        }

        public static void AsignarEntidades(SortableBindingList<HojaRuta> hojasRutas, SortableBindingList<UbicacionStock> ubicacionesStock)
        {
            foreach (HojaRuta ruta in hojasRutas)
            {
                if (ruta.UbicacionStock != null) { ruta.UbicacionStock = ubicacionesStock.Where(p => p.Numero == ruta.UbicacionStock.Numero).Single(); }

                foreach (DetalleHojaRuta detalle in ruta.Detalle)
                {
                    if (detalle.StockOrigen != null) { detalle.StockOrigen = ubicacionesStock.Where(p => p.Numero == detalle.StockOrigen.Numero).Single(); }
                    if (detalle.StockDestino != null) { detalle.StockDestino = ubicacionesStock.Where(p => p.Numero == detalle.StockDestino.Numero).Single(); }
                }
            }
        }
    }
}
