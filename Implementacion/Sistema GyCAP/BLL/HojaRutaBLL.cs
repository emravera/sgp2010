using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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

        public static int Insertar(Data.dsHojaRuta dsHojaRuta)
        {
            Data.dsHojaRuta.HOJAS_RUTARow rowhoja = dsHojaRuta.HOJAS_RUTA.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsHojaRuta.HOJAS_RUTARow;
            if (DAL.HojaRutaDAL.EsHojaRuta(rowhoja.HR_NOMBRE, Convert.ToInt32(rowhoja.HR_CODIGO))) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            return DAL.HojaRutaDAL.Insertar(dsHojaRuta);
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

        public static Entidades.HojaRuta AsHojaRutaEntity(int codigo, Data.dsHojaRuta ds)
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

        private static IList<Entidades.DetalleHojaRuta> AsDetalleHojaRuta(int codigoHojaRuta, Data.dsHojaRuta ds)
        {
            IList<Entidades.DetalleHojaRuta> detalle = new List<Entidades.DetalleHojaRuta>();
            
            foreach (Data.dsHojaRuta.DETALLE_HOJARUTARow row in (Data.dsHojaRuta.DETALLE_HOJARUTARow[])ds.DETALLE_HOJARUTA.Select("hr_codigo = " + codigoHojaRuta))
            {
                detalle.Add(new GyCAP.Entidades.DetalleHojaRuta()
                {
                    Codigo = Convert.ToInt32(row.DHR_CODIGO),
                    Secuencia = Convert.ToInt32(row.DHR_SECUENCIA),
                    StockDestino = (row.IsUSTCK_DESTINONull()) ? null : UbicacionStockBLL.AsUbicacionStock(Convert.ToInt32(row.USTCK_DESTINO), ds),
                    StockOrigen = (row.IsUSTCK_ORIGENNull()) ? null : UbicacionStockBLL.AsUbicacionStock(Convert.ToInt32(row.USTCK_ORIGEN), ds),
                    Operacion = OperacionBLL.AsOperacionFabricacionEntity(row.OPERACIONESRow),
                    CentroTrabajo = CentroTrabajoBLL.AsCentroTrabajoEntity(row.CENTROS_TRABAJOSRow)
                });
            }

            return detalle;
        }
    }
}
