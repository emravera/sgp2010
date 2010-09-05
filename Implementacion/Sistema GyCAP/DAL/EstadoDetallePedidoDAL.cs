using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class EstadoDetallePedidoDAL
    {
        public static long Insertar(Entidades.EstadoDetallePedido estadoDetallePedido)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO ESTADO_DETALLE_PEDIDOS (EDPED_NOMBRE, EDPED_DESCRIPCION) VALUES (@p0,@p1) SELECT @@Identity";
            object[] valorParametros = { estadoDetallePedido.Nombre, estadoDetallePedido.Descripcion };
            try
            {
                return Convert.ToInt64(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(long codigo)
        {
            string sql = "DELETE FROM ESTADO_DETALLE_PEDIDOS WHERE EDPED_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.EstadoDetallePedido estadoDetallePedido)
        {
            string sql = "UPDATE ESTADO_DETALLE_PEDIDOS SET EDPED_NOMBRE = @p1, EDPED_DESCRIPCION = @p2 WHERE EDPED_CODIGO = @p0";
            object[] valorParametros = { estadoDetallePedido.Codigo, estadoDetallePedido.Nombre, estadoDetallePedido.Descripcion };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsEstadoDetallePedido(Entidades.EstadoDetallePedido estadoDetallePedido)
        {
            string sql = "SELECT count(EDPED_CODIGO) FROM ESTADO_DETALLE_PEDIDOS WHERE EDPED_NOMBRE = @p0";
            object[] valorParametros = { estadoDetallePedido.Nombre };
            try
            {
                if (Convert.ToInt64(DB.executeScalar(sql, valorParametros, null)) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerEstadosDetallePedido(string nombre, Data.dsEstadoDetallePedido ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT EDPED_CODIGO, EDPED_NOMBRE, EDPED_DESCRIPCION
                              FROM ESTADO_DETALLE_PEDIDOS
                              WHERE EDPED_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "ESTADO_DETALLE_PEDIDOS", sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = "SELECT EDPED_CODIGO, EDPED_NOMBRE, EDPED_DESCRIPCION FROM ESTADO_DETALLE_PEDIDOS ";
                try
                {
                    DB.FillDataSet(ds, "ESTADO_DETALLE_PEDIDOS", sql, null);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

        public static void ObtenerEstadosDetallePedido(Data.dsEstadoDetallePedido ds)
        {
            string sql = @"SELECT EDPED_CODIGO, EDPED_NOMBRE, EDPED_DESCRIPCION
                           FROM ESTADO_DETALLE_PEDIDOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "ESTADO_DETALLE_PEDIDOS", sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerEstadosDetallePedido(Data.dsCliente ds)
        {
            string sql = @"SELECT EDPED_CODIGO, EDPED_NOMBRE, EDPED_DESCRIPCION
                           FROM ESTADO_DETALLE_PEDIDOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "ESTADO_DETALLE_PEDIDOS", sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerEstadosDetallePedido(DataTable dtEstadoDetallePedido)
        {
            string sql = @"SELECT EDPED_CODIGO, EDPED_NOMBRE, EDPED_DESCRIPCION
                           FROM ESTADO_DETALLE_PEDIDOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataTable(dtEstadoDetallePedido, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(long codigo)
        {
            string sql = "SELECT count(DPED_CODIGO) FROM DETALLE_PEDIDOS WHERE EDPED_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                if (Convert.ToInt64(DB.executeScalar(sql, valorParametros, null)) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
