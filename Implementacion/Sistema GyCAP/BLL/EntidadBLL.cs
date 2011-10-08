using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GyCAP.Entidades;
using GyCAP.Entidades.Enumeraciones;
using System.Data.SqlClient;

namespace GyCAP.BLL
{
    public class EntidadBLL
    {        
        public static void ObtenerTodos(Data.dsStock.ENTIDADESDataTable table)
        {
            DAL.EntidadDAL.ObtenerTodos(table);
        }

        public static int ObtenerCodigoEntidad(int codigoPedido)
        {
            return DAL.EntidadDAL.ObtenerCodigoEntidad(codigoPedido);
        }     

        public static Entidad GetEntidad(EntidadEnum.TipoEntidadEnum tipo, int idEntidadExterna, SqlTransaction transaccion)
        {
            Entidad entidad = new Entidad();
            entidad.TipoEntidad = TipoEntidadBLL.GetTipoEntidadEntity(tipo);
            entidad.Nombre = tipo.ToString();

            switch (tipo)
            {
                case EntidadEnum.TipoEntidadEnum.Manual:
                    entidad.EntidadExterna = null;                   
                    break;
                case EntidadEnum.TipoEntidadEnum.Pedido:
                    entidad.EntidadExterna = new Pedido() { Codigo = idEntidadExterna };
                    break;
                case EntidadEnum.TipoEntidadEnum.DetallePedido:
                    entidad.EntidadExterna = new DetallePedido() { Codigo = idEntidadExterna };
                    break;
                case EntidadEnum.TipoEntidadEnum.OrdenProduccion:
                    entidad.EntidadExterna = new OrdenProduccion() { Numero = idEntidadExterna };
                    break;
                case EntidadEnum.TipoEntidadEnum.OrdenTrabajo:
                    entidad.EntidadExterna = new OrdenTrabajo() { Numero = idEntidadExterna };
                    break;
                case EntidadEnum.TipoEntidadEnum.Mantenimiento:
                    entidad.EntidadExterna = new Mantenimiento() { Codigo = idEntidadExterna };
                    break;
                case EntidadEnum.TipoEntidadEnum.UbicacionStock:
                    entidad.EntidadExterna = new UbicacionStock() { Numero = idEntidadExterna };
                    break;
                default:
                    break;
            }

            DAL.EntidadDAL.GetEntidad(tipo, entidad, transaccion);

            return entidad;
        }

        public static Entidad GetEntidad(int idEntidad)
        {
            Data.dsStock.ENTIDADESDataTable dt = DAL.EntidadDAL.GetEntidad(idEntidad);
            Entidad entidad = new Entidad();

            if (dt.Rows.Count > 0)
            {
                entidad.Codigo = Convert.ToInt32(dt.Rows[0]["entd_codigo"].ToString());
                entidad.Nombre = dt.Rows[0]["entd_nombre"].ToString();
                entidad.TipoEntidad = BLL.TipoEntidadBLL.GetTipoEntidadEntity(Convert.ToInt32(dt.Rows[0]["tentd_codigo"].ToString()));
            }

            switch (entidad.TipoEntidad.Codigo)
            {
                case (int)EntidadEnum.TipoEntidadEnum.Pedido:
                    entidad.EntidadExterna = new Pedido() { Codigo = Convert.ToInt32(dt.Rows[0]["entd_id"].ToString()) };
                    break;
                case (int)EntidadEnum.TipoEntidadEnum.DetallePedido:
                    entidad.EntidadExterna = new DetallePedido() { Codigo = Convert.ToInt32(dt.Rows[0]["entd_id"].ToString()) };
                    break;
                case (int)EntidadEnum.TipoEntidadEnum.Manual:
                    entidad.EntidadExterna = null;
                    break;
                case (int)EntidadEnum.TipoEntidadEnum.OrdenProduccion:
                    entidad.EntidadExterna = new OrdenProduccion() { Numero = Convert.ToInt32(dt.Rows[0]["entd_id"].ToString()) };
                    break;
                case (int)EntidadEnum.TipoEntidadEnum.OrdenTrabajo:
                    entidad.EntidadExterna = new OrdenTrabajo() { Numero = Convert.ToInt32(dt.Rows[0]["entd_id"].ToString()) };
                    break;
                case (int)EntidadEnum.TipoEntidadEnum.Mantenimiento:
                    entidad.EntidadExterna = new Mantenimiento() { Codigo = Convert.ToInt32(dt.Rows[0]["entd_id"].ToString()) };
                    break;
                case (int)EntidadEnum.TipoEntidadEnum.UbicacionStock:
                    entidad.EntidadExterna = UbicacionStockBLL.GetUbicacionStock(Convert.ToInt32(dt.Rows[0]["entd_id"].ToString()));
                    break;
                default:
                    break;
            }

            return entidad;
        }
    }
}
