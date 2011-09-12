using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class UbicacionStockBLL
    {
        public static readonly int Activo = 1;
        public static readonly int Inactivo = 0;
        
        public static void ObtenerUbicacionesStock(DataTable dtUbicacionStock)
        {
            DAL.UbicacionStockDAL.ObtenerUbicacionesStock(dtUbicacionStock);
        }

        public static void ObtenerUbicacionesStock(DataTable dtUbicacionesStock, int contenidoUbicacionStock)
        {
            DAL.UbicacionStockDAL.ObtenerUbicacionesStock(dtUbicacionesStock, contenidoUbicacionStock);
        }
        
        public static void Insertar(Entidades.UbicacionStock ubicacion)
        {
            if (DAL.UbicacionStockDAL.EsUbicacionStock(ubicacion.Codigo)) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            DAL.UbicacionStockDAL.Insertar(ubicacion);
        }
        
        public static void Eliminar(int numeroUbicacionStock)
        {
            if (DAL.UbicacionStockDAL.PuedeEliminarse(numeroUbicacionStock)) { DAL.UbicacionStockDAL.Eliminar(numeroUbicacionStock); }
            else { throw new Entidades.Excepciones.ElementoEnTransaccionException(); }
        }

        public static void Actualizar(Entidades.UbicacionStock ubicacionStock)
        {
            if(DAL.UbicacionStockDAL.EsUbicacionStock(ubicacionStock.Codigo, ubicacionStock.Numero)) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            DAL.UbicacionStockDAL.Actualizar(ubicacionStock);
        }

        public static Entidades.UbicacionStock AsUbicacionStock(int numeroUbicacionStock, Data.dsEstructuraProducto ds)
        {
            Entidades.UbicacionStock ubicacion = new GyCAP.Entidades.UbicacionStock();
            ubicacion.Activo = Convert.ToInt32(ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_ACTIVO);
            ubicacion.CantidadReal = ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_CANTIDADREAL;
            ubicacion.Codigo = ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_CODIGO;
            ubicacion.Descripcion = ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_DESCRIPCION;
            ubicacion.Contenido = BLL.ContenidoUbicacionStockBLL.AsContenidoUbicacionStockEntity(ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).CONTENIDO_UBICACION_STOCKRow);
            ubicacion.Nombre = ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_NOMBRE;
            ubicacion.Numero = numeroUbicacionStock;
            ubicacion.TipoUbicacion = BLL.TipoUbicacionStockBLL.AsTipoUbicacionStockEntity(ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).TIPOS_UBICACIONES_STOCKRow);
            ubicacion.UbicacionFisica = ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_UBICACIONFISICA;
            ubicacion.UnidadMedida = BLL.UnidadMedidaBLL.AsUnidadMedidaEntity(Convert.ToInt32(ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).UMED_CODIGO), ds);
            if (!ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).IsUSTCK_PADRENull())
            {
                ubicacion.UbicacionPadre = BLL.UbicacionStockBLL.AsUbicacionStock(Convert.ToInt32(ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_PADRE), ds);
            }

            return ubicacion;
        }
        
        public static Entidades.UbicacionStock AsUbicacionStock(int numeroUbicacionStock, Data.dsStock dsStock)
        {
            Entidades.UbicacionStock ubicacion = new GyCAP.Entidades.UbicacionStock();
            ubicacion.Activo = Convert.ToInt32(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_ACTIVO);
            ubicacion.CantidadReal = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_CANTIDADREAL;
            ubicacion.Codigo = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_CODIGO;
            ubicacion.Descripcion = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_DESCRIPCION;
            ubicacion.Contenido = BLL.ContenidoUbicacionStockBLL.AsContenidoUbicacionStockEntity(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).CONTENIDO_UBICACION_STOCKRow);
            ubicacion.Nombre = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_NOMBRE;
            ubicacion.Numero = numeroUbicacionStock;
            ubicacion.TipoUbicacion = BLL.TipoUbicacionStockBLL.AsTipoUbicacionStockEntity(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).TIPOS_UBICACIONES_STOCKRow);
            ubicacion.UbicacionFisica = dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_UBICACIONFISICA;
            ubicacion.UnidadMedida = BLL.UnidadMedidaBLL.AsUnidadMedidaEntity(Convert.ToInt32(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).UMED_CODIGO), dsStock);
            if (!dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).IsUSTCK_PADRENull())
            {
                ubicacion.UbicacionPadre = BLL.UbicacionStockBLL.AsUbicacionStock(Convert.ToInt32(dsStock.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_PADRE), dsStock);
            }

            return ubicacion;
        }

        public static Entidades.UbicacionStock AsUbicacionStock(int numeroUbicacionStock, Data.dsHojaRuta ds)
        {
            Entidades.UbicacionStock ubicacion = new GyCAP.Entidades.UbicacionStock();
            ubicacion.Activo = Convert.ToInt32(ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_ACTIVO);
            ubicacion.CantidadReal = ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_CANTIDADREAL;
            ubicacion.Codigo = ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_CODIGO;
            ubicacion.Descripcion = ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_DESCRIPCION;
            ubicacion.Contenido = BLL.ContenidoUbicacionStockBLL.AsContenidoUbicacionStockEntity(ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).CONTENIDO_UBICACION_STOCKRow);
            ubicacion.Nombre = ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_NOMBRE;
            ubicacion.Numero = numeroUbicacionStock;
            ubicacion.TipoUbicacion = BLL.TipoUbicacionStockBLL.AsTipoUbicacionStockEntity(ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).TIPOS_UBICACIONES_STOCKRow);
            ubicacion.UbicacionFisica = ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_UBICACIONFISICA;
            ubicacion.UnidadMedida = BLL.UnidadMedidaBLL.AsUnidadMedidaEntity(Convert.ToInt32(ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).UMED_CODIGO), ds);
            if (!ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).IsUSTCK_PADRENull())
            {
                ubicacion.UbicacionPadre = BLL.UbicacionStockBLL.AsUbicacionStock(Convert.ToInt32(ds.UBICACIONES_STOCK.FindByUSTCK_NUMERO(numeroUbicacionStock).USTCK_PADRE), ds);
            }

            return ubicacion;
        }

        public static Entidades.UbicacionStock GetUbicacionStock(int numeroUbicacion)
        {
            Data.dsStock.UBICACIONES_STOCKDataTable dt = DAL.UbicacionStockDAL.GetUbicacionStock(numeroUbicacion);
            Entidades.UbicacionStock ubicacion = new GyCAP.Entidades.UbicacionStock();

            if (dt.Rows.Count == 0) { return null; }
            
            ubicacion.Numero = numeroUbicacion;
            ubicacion.Codigo = dt.Rows[0]["ustck_codigo"].ToString();
            ubicacion.Activo = Convert.ToInt32(dt.Rows[0]["ustck_activo"].ToString());
            ubicacion.Descripcion = dt.Rows[0]["ustck_descripcion"].ToString();
            ubicacion.CantidadReal = Convert.ToDecimal(dt.Rows[0]["ustck_cantidadreal"].ToString());
            ubicacion.Contenido = BLL.ContenidoUbicacionStockBLL.GetContenidoUbicacionStock(Convert.ToInt32(dt.Rows[0]["con_codigo"].ToString()));
            ubicacion.Nombre = dt.Rows[0]["ustck_nombre"].ToString();
            ubicacion.TipoUbicacion = BLL.TipoUbicacionStockBLL.GetTipoUbicacion(Convert.ToInt32(dt.Rows[0]["tus_codigo"].ToString()));
            ubicacion.UbicacionFisica = dt.Rows[0]["ustck_ubicacionfisica"].ToString();
            string numeroPadre = dt.Rows[0]["ustck_padre"].ToString();
            ubicacion.UbicacionPadre = (!string.IsNullOrEmpty(numeroPadre)) ? GetUbicacionStock(Convert.ToInt32(dt.Rows[0]["ustck_padre"].ToString())) : null;
            ubicacion.UnidadMedida = UnidadMedidaBLL.GetUnidadMedida(Convert.ToInt32(dt.Rows[0]["umed_codigo"].ToString()));
            

            return ubicacion;
        }

        public static void ActualizarCantidadesStock(int numeroUbicacion, decimal cantidadReal)
        {
            DAL.UbicacionStockDAL.ActualizarCantidadesStock(numeroUbicacion, cantidadReal, null);
            // que eventos deberia disparar ?? - gonzalo
        }
        
        public static decimal CantidadMateriaPrima(Entidades.MateriaPrima materiaPrima)
        {
            return DAL.UbicacionStockDAL.CantidadMateriaPrima(materiaPrima);
        }
    }
}
