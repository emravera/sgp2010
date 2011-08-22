using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GyCAP.Entidades;

namespace GyCAP.BLL
{
    public class EntidadBLL
    {        
        public static void ObtenerTodos(Data.dsStock.ENTIDADESDataTable table)
        {
            DAL.EntidadDAL.ObtenerTodos(table);
        }

        public static IList<Entidad> ObtenerTodos()
        {
            Data.dsStock.ENTIDADESDataTable dt = new GyCAP.Data.dsStock.ENTIDADESDataTable();
            ObtenerTodos(dt);
            IList<Entidad> lista = new List<Entidad>();

            foreach (Data.dsStock.ENTIDADESRow row in dt.Rows)
            {
                lista.Add(AsEntidad(row));
            }

            return lista;
        }

        public static Entidad AsEntidad(Data.dsStock.ENTIDADESRow row)
        {
            Data.dsStock.TIPOS_ENTIDADDataTable dt = new GyCAP.Data.dsStock.TIPOS_ENTIDADDataTable();
            DAL.TipoEntidadDAL.ObtenerTodos(dt);

            Entidad entidad = new Entidad();
            entidad.Codigo = Convert.ToInt32(row.ENTD_CODIGO);
            entidad.Nombre = row.ENTD_NOMBRE;
            entidad.TipoEntidad = TipoEntidadBLL.AsTipoEntidad(dt.FindByTENTD_CODIGO(row.TENTD_CODIGO));

            switch (TipoEntidadBLL.GetTipoEntidad(entidad.TipoEntidad.Nombre))
            {
                case GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.Pedido:
                    entidad.EntidadExterna = new object();
                    break;
                case GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.DetallePedido:
                    entidad.EntidadExterna = new object();
                    break;
                case GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.Manual:
                    entidad.EntidadExterna = new object();
                    break;
                case GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.OrdenProduccion:
                    entidad.EntidadExterna = new object();
                    break;
                case GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.OrdenTrabajo:
                    entidad.EntidadExterna = new object();
                    break;
                case GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.Mantenimiento:
                    entidad.EntidadExterna = new object();
                    break;
                case GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.UbicacionStock:
                    entidad.EntidadExterna = new object();
                    break;
                case GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.OrdenCompra:
                    entidad.EntidadExterna = new object();
                    break;
                default:
                    break;
            }

            return entidad;
        }

        public static Entidad GetEntidad(string nombreTipoEntidad, int idEntidad)
        {
            Entidad entidad = new Entidad();
            entidad.TipoEntidad = TipoEntidadBLL.GetTipoEntidadEntity(nombreTipoEntidad);
            DAL.TipoEntidadDAL.TipoEntidadEnum tipo = GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.Manual;

            switch (entidad.TipoEntidad.Nombre)
            {
                case TipoEntidadBLL.PedidoNombre:
                    entidad.EntidadExterna = new Pedido() { Codigo = idEntidad };
                    entidad.Nombre = TipoEntidadBLL.PedidoNombre;
                    tipo = GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.Pedido;
                    break;
                case TipoEntidadBLL.DetallePedidoNombre:
                    entidad.EntidadExterna = new DetallePedido() { Codigo = idEntidad };
                    entidad.Nombre = TipoEntidadBLL.DetallePedidoNombre;
                    tipo = GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.DetallePedido;
                    break;
                case TipoEntidadBLL.ManualNombre:
                    entidad.EntidadExterna = null;
                    entidad.Nombre = TipoEntidadBLL.ManualNombre;
                    break;
                case TipoEntidadBLL.OrdenProduccionNombre:
                    entidad.EntidadExterna = new OrdenProduccion() { Numero = idEntidad };
                    entidad.Nombre = TipoEntidadBLL.OrdenProduccionNombre;
                    tipo = GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.OrdenProduccion;
                    break;
                case TipoEntidadBLL.OrdenTrabajoNombre:
                    entidad.EntidadExterna = new OrdenTrabajo() { Numero = idEntidad };
                    entidad.Nombre = TipoEntidadBLL.OrdenTrabajoNombre;
                    tipo = GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.OrdenTrabajo;
                    break;
                case TipoEntidadBLL.MantenimientoNombre:
                    entidad.EntidadExterna = new Mantenimiento() { Codigo = idEntidad };
                    entidad.Nombre = TipoEntidadBLL.MantenimientoNombre;
                    tipo = GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.Mantenimiento;
                    break;
                case TipoEntidadBLL.UbicacionStockNombre:
                    entidad.EntidadExterna = new UbicacionStock() { Numero = idEntidad };
                    entidad.Nombre = TipoEntidadBLL.UbicacionStockNombre;
                    tipo = GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.UbicacionStock;
                    break;
                case TipoEntidadBLL.OrdenCompraNombre:
                    entidad.EntidadExterna = new OrdenCompra() { Numero = idEntidad };
                    entidad.Nombre = TipoEntidadBLL.OrdenCompraNombre;
                    tipo = GyCAP.DAL.TipoEntidadDAL.TipoEntidadEnum.OrdenCompra;
                    break;
                default:
                    break;
            }

            DAL.EntidadDAL.GetEntidad(tipo, entidad);

            return entidad;
        }
    }
}
