using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class ContenidoUbicacionStockBLL
    {
        public static readonly int ContenidoCocina = 1;
        public static readonly int ContenidoConjunto = 2;
        public static readonly int ContenidoSubconjunto = 3;
        public static readonly int ContenidoPieza = 4;
        public static readonly int ContenidoMateriaPrima = 5;
        public static readonly int ContenidoRepuesto = 6;
        public static readonly int ContenidoIntermedio = 7;
        
        public static void ObtenerContenidosUbicacionStock(DataTable dtContenidoUbicacionStock)
        {
            DAL.ContenidoUbicacionStockDAL.ObtenerContenidosUbicacionStock(dtContenidoUbicacionStock);
        }

        public static int ObtenerCodigoContenido(string nombreUbicacion)
        {
            return DAL.ContenidoUbicacionStockDAL.ObtenerCodigoUbicacion(nombreUbicacion);
        }

        public static Entidades.ContenidoUbicacionStock AsContenidoUbicacionStockEntity(Data.dsStock.CONTENIDO_UBICACION_STOCKRow row)
        {
            return new GyCAP.Entidades.ContenidoUbicacionStock()
            {
                Codigo = Convert.ToInt32(row.CON_CODIGO),
                Nombre = row.CON_NOMBRE,
                Descripcion = row.CON_DESCRIPCION
            };
        }

        public static Entidades.ContenidoUbicacionStock AsContenidoUbicacionStockEntity(Data.dsEstructuraProducto.CONTENIDO_UBICACION_STOCKRow row)
        {
            return new GyCAP.Entidades.ContenidoUbicacionStock()
            {
                Codigo = Convert.ToInt32(row.CON_CODIGO),
                Nombre = row.CON_NOMBRE,
                Descripcion = row.CON_DESCRIPCION
            };
        }

        public static Entidades.ContenidoUbicacionStock GetContenidoUbicacionStock(int codigo)
        {
            Data.dsStock.CONTENIDO_UBICACION_STOCKDataTable dt = DAL.ContenidoUbicacionStockDAL.GetContenidoUbicacionStock(codigo);
            Entidades.ContenidoUbicacionStock contenido = new GyCAP.Entidades.ContenidoUbicacionStock();

            if (dt.Rows.Count > 0)
            {
                contenido.Codigo = codigo;
                contenido.Nombre = dt.Rows[0]["con_nombre"].ToString();
                contenido.Descripcion = dt.Rows[0]["con_descripcion"].ToString();
            }

            return contenido;
        }
    }
}
