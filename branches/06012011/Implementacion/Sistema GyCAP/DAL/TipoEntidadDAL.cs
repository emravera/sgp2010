using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using GyCAP.Entidades;

namespace GyCAP.DAL
{
    public class TipoEntidadDAL
    {
        public const string PedidoNombre = "Pedido";
        public const string DetallePedidoNombre = "DetallePedido";
        public const string ManualNombre = "Manual";
        public const string OrdenProduccionNombre = "OrdenProduccion";
        public const string OrdenTrabajoNombre = "OrdenTrabajo";
        public const string MantenimientoNombre = "Mantenimiento";
        public const string UbicacionStockNombre = "UbicacionStock";
        public const string OrdenCompraNombre = "OrdenCompra";

        public enum TipoEntidadEnum { Pedido, DetallePedido, Manual, OrdenProduccion, OrdenTrabajo, Mantenimiento, UbicacionStock, OrdenCompra };
        
        public static void ObtenerTodos(Data.dsStock.TIPOS_ENTIDADDataTable table)
        {
            string sql = "SELECT tentd_codigo, tentd_nombre, tentd_descripcion FROM TIPOS_ENTIDAD";

            try
            {
                DB.FillDataTable(table, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static TipoEntidadEnum GetTipoEntidad(int codigoTipo)
        {
            string sql = "SELECT tentd_nombre FROM TIPOS_ENTIDAD WHERE tentd_codigo = @p0";

            try
            {
                object result = DB.executeScalar(sql, new object[] { codigoTipo }, null);

                if(result != null)
                {
                    return GetTipoEntidad(result.ToString());
                }

                throw new Entidades.Excepciones.ElementoInexistenteException();
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static string GetNombreTipoEntidad(int codigoTipo)
        {
            string sql = "SELECT tentd_nombre FROM TIPOS_ENTIDAD WHERE tentd_codigo = @p0";

            try
            {
                object result = DB.executeScalar(sql, new object[] { codigoTipo }, null);

                if (result != null)
                {
                    return result.ToString();
                }

                throw new Entidades.Excepciones.ElementoInexistenteException();
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static TipoEntidadEnum GetTipoEntidad(string nombreTipoEntidad)
        {
            switch (nombreTipoEntidad)
            {
                case PedidoNombre:
                    return TipoEntidadEnum.Pedido;
                    break;
                case DetallePedidoNombre:
                    return TipoEntidadEnum.DetallePedido;
                    break;
                case ManualNombre:
                    return TipoEntidadEnum.Manual;
                    break;
                case OrdenProduccionNombre:
                    return TipoEntidadEnum.OrdenProduccion;
                    break;
                case OrdenTrabajoNombre:
                    return TipoEntidadEnum.OrdenTrabajo;
                    break;
                case MantenimientoNombre:
                    return TipoEntidadEnum.Mantenimiento;
                    break;
                case UbicacionStockNombre:
                    return TipoEntidadEnum.UbicacionStock;
                    break;
                case OrdenCompraNombre:
                    return TipoEntidadEnum.OrdenCompra;
                    break;
                default:
                    break;
            }

            throw new Entidades.Excepciones.ElementoInexistenteException();
        }

        public static int GetCodigoTipoEntidad(TipoEntidadEnum tipo)
        {
            string sql = "SELECT tentd_codigo FROM TIPOS_ENTIDAD WHERE tentd_nombre = @p0";
            object[] parametros;

            try
            {
                switch (tipo)
                {
                    case TipoEntidadEnum.Pedido:
                        parametros = new object[] { PedidoNombre };
                        break;
                    case TipoEntidadEnum.DetallePedido:
                        parametros = new object[] { DetallePedidoNombre };
                        break;
                    case TipoEntidadEnum.Manual:
                        parametros = new object[] { ManualNombre };
                        break;
                    case TipoEntidadEnum.OrdenProduccion:
                        parametros = new object[] { OrdenProduccionNombre };
                        break;
                    case TipoEntidadEnum.OrdenTrabajo:
                        parametros = new object[] { OrdenTrabajoNombre };
                        break;
                    case TipoEntidadEnum.Mantenimiento:
                        parametros = new object[] { MantenimientoNombre };
                        break;
                    case TipoEntidadEnum.UbicacionStock:
                        parametros = new object[] { UbicacionStockNombre };
                        break;
                    case TipoEntidadEnum.OrdenCompra:
                        parametros = new object[] { OrdenCompraNombre };
                        break;
                    default:
                        parametros = new object[] { "error" };
                        break;
                }

                object result = DB.executeScalar(sql, parametros, null);

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }

                return -1;
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static TipoEntidad GetTipoEntidadEntity(string nombreTipo)
        {
            string sql = "SELECT tentd_codigo, tentd_nombre, tentd_descripcion FROM TIPOS_ENTIDAD WHERE tentd_nombre = @p0";
            object[] parametros = { nombreTipo };
            TipoEntidad tipo = new TipoEntidad();

            try
            {
                Data.dsStock.TIPOS_ENTIDADDataTable dt = new GyCAP.Data.dsStock.TIPOS_ENTIDADDataTable();
                DB.FillDataTable(dt, sql, parametros);

                if (dt.Rows.Count == 1)
                {
                    tipo.Codigo = Convert.ToInt32(dt.Rows[0]["tentd_codigo"].ToString());
                    tipo.Nombre = dt.Rows[0]["tentd_nombre"].ToString();
                    tipo.Descripcion = dt.Rows[0]["tentd_descripcion"].ToString();                    
                }

                return tipo;
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static TipoEntidad GetTipoEntidadEntity(int codigoTipoEntidad)
        {
            string sql = "SELECT tentd_codigo, tentd_nombre, tentd_descripcion FROM TIPOS_ENTIDAD WHERE tentd_codigo = @p0";
            object[] parametros = { codigoTipoEntidad };
            TipoEntidad tipo = new TipoEntidad();

            try
            {
                Data.dsStock.TIPOS_ENTIDADDataTable dt = new GyCAP.Data.dsStock.TIPOS_ENTIDADDataTable();
                DB.FillDataTable(dt, sql, parametros);

                if (dt.Rows.Count == 1)
                {
                    tipo.Codigo = Convert.ToInt32(dt.Rows[0]["tentd_codigo"].ToString());
                    tipo.Nombre = dt.Rows[0]["tentd_nombre"].ToString();
                    tipo.Descripcion = dt.Rows[0]["tentd_descripcion"].ToString();
                }

                return tipo;
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
