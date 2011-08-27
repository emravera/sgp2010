using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using GyCAP.Entidades;

namespace GyCAP.DAL
{
    public class EntidadDAL
    {
        public static void ObtenerTodos(Data.dsStock.ENTIDADESDataTable table)
        {
            string sql = "SELECT entd_codigo, entd_nombre, tentd_codigo, entd_id FROM ENTIDADES";

            try
            {
                DB.FillDataTable(table, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void GetEntidad(TipoEntidadDAL.TipoEntidadEnum tipo, Entidad entidad)
        {
            object[] parametros = { };

            switch (tipo)
            {
                case TipoEntidadDAL.TipoEntidadEnum.Pedido:
                    entidad.Codigo = Existe(tipo, Convert.ToInt32((entidad.EntidadExterna as Pedido).Codigo));
                    parametros = new object[] { entidad.Nombre, entidad.TipoEntidad.Codigo, Convert.ToInt32((entidad.EntidadExterna as Pedido).Codigo) };
                    break;
                case TipoEntidadDAL.TipoEntidadEnum.DetallePedido:
                    entidad.Codigo = Existe(tipo, Convert.ToInt32((entidad.EntidadExterna as DetallePedido).Codigo));
                    parametros = new object[] { entidad.Nombre, entidad.TipoEntidad.Codigo, Convert.ToInt32((entidad.EntidadExterna as DetallePedido).Codigo) };
                    break;
                case TipoEntidadDAL.TipoEntidadEnum.Manual:
                    entidad.Codigo = Existe(tipo, -1);
                    parametros = new object[] { entidad.Nombre, entidad.TipoEntidad.Codigo, DBNull.Value };
                    break;
                case TipoEntidadDAL.TipoEntidadEnum.OrdenProduccion:
                    entidad.Codigo = Existe(tipo, (entidad.EntidadExterna as OrdenProduccion).Numero);
                    parametros = new object[] { entidad.Nombre, entidad.TipoEntidad.Codigo, (entidad.EntidadExterna as OrdenProduccion).Numero };
                    break;
                case TipoEntidadDAL.TipoEntidadEnum.OrdenTrabajo:
                    entidad.Codigo = Existe(tipo, (entidad.EntidadExterna as OrdenTrabajo).Numero);
                    parametros = new object[] { entidad.Nombre, entidad.TipoEntidad.Codigo, (entidad.EntidadExterna as OrdenTrabajo).Numero };
                    break;
                case TipoEntidadDAL.TipoEntidadEnum.Mantenimiento:
                    entidad.Codigo = Existe(tipo, Convert.ToInt32((entidad.EntidadExterna as Mantenimiento).Codigo));
                    parametros = new object[] { entidad.Nombre, entidad.TipoEntidad.Codigo, Convert.ToInt32((entidad.EntidadExterna as Mantenimiento).Codigo) };
                    break;
                case TipoEntidadDAL.TipoEntidadEnum.UbicacionStock:
                    entidad.Codigo = Existe(tipo, (entidad.EntidadExterna as UbicacionStock).Numero);
                    parametros = new object[] { entidad.Nombre, entidad.TipoEntidad.Codigo, (entidad.EntidadExterna as UbicacionStock).Numero };
                    break;
                case TipoEntidadDAL.TipoEntidadEnum.OrdenCompra:
                    entidad.Codigo = Existe(tipo, (entidad.EntidadExterna as OrdenCompra).Numero);
                    parametros = new object[] { entidad.Nombre, entidad.TipoEntidad.Codigo, (entidad.EntidadExterna as OrdenCompra).Numero };
                    break;
                default:
                    break;
            }

            if (entidad.Codigo > 0) { return; }

            string sql = @"INSERT INTO ENTIDADES (entd_nombre, tentd_codigo, entd_id) 
                            values (@p0, @p1, @p2)  SELECT @@Identity";

            try
            {
                entidad.Codigo = Convert.ToInt32(DB.executeScalar(sql, parametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
                        
        }

        public static int Existe(TipoEntidadDAL.TipoEntidadEnum tipoEntidad, int codigoEntidad)
        {
            string sql = string.Empty;
            object[] parametros = { };

            if (codigoEntidad > 0)
            {
                sql = "SELECT entd_codigo FROM ENTIDADES WHERE tentd_codigo = @p0 AND entd_id = @p1";
                parametros = new object[] { TipoEntidadDAL.GetCodigoTipoEntidad(tipoEntidad), codigoEntidad };
            }
            else
            {
                sql = "SELECT entd_codigo FROM ENTIDADES WHERE tentd_codigo = @p0 AND entd_id IS NULL";
                parametros = new object[] { TipoEntidadDAL.GetCodigoTipoEntidad(tipoEntidad) };
            }            

            try
            {
                object result = DB.executeScalar(sql, parametros, null);

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }

                return -1;
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static Data.dsStock.ENTIDADESDataTable GetEntidad(int codigoEntidad)
        {
            string sql = @"SELECT entd_nombre, tentd_codigo, entd_id FROM ENTIDADES WHERE entd_codigo = @p0";
            
            object[] parametros = { codigoEntidad };

            Data.dsStock.ENTIDADESDataTable dt = new GyCAP.Data.dsStock.ENTIDADESDataTable();

            try
            {
                DB.FillDataTable(dt, sql, parametros);
                return dt;
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }            
        }
    }
}
