using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class TipoUbicacionStockBLL
    {
        public static readonly int TipoVista = 1;
        
        public static void ObtenerTiposUbicacionStock(DataTable dtTiposUbicacionStock)
        {
            DAL.TipoUbicacionStockDAL.ObtenerTiposUbicacionStock(dtTiposUbicacionStock);
        }

        public static Entidades.TipoUbicacionStock AsTipoUbicacionStockEntity(Data.dsStock.TIPOS_UBICACIONES_STOCKRow row)
        {
            return new GyCAP.Entidades.TipoUbicacionStock()
            {
                Codigo = Convert.ToInt32(row.TUS_CODIGO),
                Nombre = row.TUS_NOMBRE,
                Descripcion = row.TUS_DESCRIPCION
            };
        }

        public static Entidades.TipoUbicacionStock AsTipoUbicacionStockEntity(Data.dsEstructuraProducto.TIPOS_UBICACIONES_STOCKRow row)
        {
            return new GyCAP.Entidades.TipoUbicacionStock()
            {
                Codigo = Convert.ToInt32(row.TUS_CODIGO),
                Nombre = row.TUS_NOMBRE,
                Descripcion = row.TUS_DESCRIPCION
            };
        }

        public static Entidades.TipoUbicacionStock AsTipoUbicacionStockEntity(Data.dsHojaRuta.TIPOS_UBICACIONES_STOCKRow row)
        {
            return new GyCAP.Entidades.TipoUbicacionStock()
            {
                Codigo = Convert.ToInt32(row.TUS_CODIGO),
                Nombre = row.TUS_NOMBRE,
                Descripcion = row.TUS_DESCRIPCION
            };
        }

        public static Entidades.TipoUbicacionStock GetTipoUbicacion(int codigo)
        {
            Data.dsStock.TIPOS_UBICACIONES_STOCKDataTable dt = DAL.TipoUbicacionStockDAL.GetTipoUbicacion(codigo);
            Entidades.TipoUbicacionStock tipo = new GyCAP.Entidades.TipoUbicacionStock();

            if (dt.Rows.Count > 0)
            {
                tipo.Codigo = Convert.ToInt32(dt.Rows[0]["tus_codigo"].ToString());
                tipo.Descripcion = dt.Rows[0]["tus_descripcion"].ToString();
                tipo.Nombre = dt.Rows[0]["tus_nombre"].ToString();
            }

            return tipo;
        }
    }
}
