using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GyCAP.Entidades;

namespace GyCAP.BLL
{
    public class TipoEntidadBLL
    {
        public const string PedidoNombre = DAL.TipoEntidadDAL.PedidoNombre;
        public const string DetallePedidoNombre = DAL.TipoEntidadDAL.DetallePedidoNombre;
        public const string ManualNombre = DAL.TipoEntidadDAL.ManualNombre;
        public const string OrdenProduccionNombre = DAL.TipoEntidadDAL.OrdenProduccionNombre;
        public const string OrdenTrabajoNombre = DAL.TipoEntidadDAL.OrdenTrabajoNombre;
        public const string MantenimientoNombre = DAL.TipoEntidadDAL.MantenimientoNombre;
        public const string UbicacionStockNombre = DAL.TipoEntidadDAL.UbicacionStockNombre;
        public const string OrdenCompraNombre = DAL.TipoEntidadDAL.OrdenCompraNombre;
        
        public static void ObtenerTodos(Data.dsStock.TIPOS_ENTIDADDataTable table)
        {
            DAL.TipoEntidadDAL.ObtenerTodos(table);
        }

        public static IList<TipoEntidad> ObtenerTodos()
        {
            Data.dsStock.TIPOS_ENTIDADDataTable dt = new GyCAP.Data.dsStock.TIPOS_ENTIDADDataTable();

            DAL.TipoEntidadDAL.ObtenerTodos(dt);

            IList<TipoEntidad> lista = new List<TipoEntidad>();

            foreach (Data.dsStock.TIPOS_ENTIDADRow row in dt.Rows)
            {
                lista.Add(AsTipoEntidad(row));
            }

            return lista;
        }

        public static TipoEntidad AsTipoEntidad(Data.dsStock.TIPOS_ENTIDADRow row)
        {
            return new TipoEntidad()
            {
                Codigo = Convert.ToInt32(row.TENTD_CODIGO),
                Nombre = row.TENTD_NOMBRE,
                Descripcion = row.TENTD_DESCRIPCION
            };
        }

        public static DAL.TipoEntidadDAL.TipoEntidadEnum GetTipoEntidad(int codigoTipo)
        {
            return DAL.TipoEntidadDAL.GetTipoEntidad(codigoTipo);            
        }

        public static string GetNombreTipoEntidad(int codigoTipo)
        {
            return DAL.TipoEntidadDAL.GetNombreTipoEntidad(codigoTipo);
        }

        public static DAL.TipoEntidadDAL.TipoEntidadEnum GetTipoEntidad(string nombreTipoEntidad)
        {
            return DAL.TipoEntidadDAL.GetTipoEntidad(nombreTipoEntidad);
        }

        public static TipoEntidad GetTipoEntidadEntity(string nombreTipo)
        {
            return DAL.TipoEntidadDAL.GetTipoEntidadEntity(nombreTipo);
        }

        public static TipoEntidad GetTipoEntidadEntity(int codigoTipoEntidad)
        {
            return DAL.TipoEntidadDAL.GetTipoEntidadEntity(codigoTipoEntidad);
        }
    }
}
