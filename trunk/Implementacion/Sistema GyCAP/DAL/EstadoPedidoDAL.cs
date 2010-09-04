using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class EstadoPedidoDAL
    {
        public static long Insertar(Entidades.EstadoPedido estadoPedido)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO ESTADO_PEDIDOS (EPED_NOMBRE, EPED_DESCRIPCION) VALUES (@p0,@p1) SELECT @@Identity";
            object[] valorParametros = { estadoPedido.Nombre, estadoPedido.Descripcion };
            try
            {
                return Convert.ToInt64(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(long codigo)
        {
            string sql = "DELETE FROM ESTADO_PEDIDOS WHERE EPED_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.EstadoPedido estadoPedido)
        {
            string sql = "UPDATE ESTADO_PEDIDOS SET EPED_NOMBRE = @p1, EPED_DESCRIPCION = @p2 WHERE EPED_CODIGO = @p0";
            object[] valorParametros = { estadoPedido.Codigo, estadoPedido.Nombre, estadoPedido.Descripcion };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsEstadoPedido(Entidades.EstadoPedido estadoPedido)
        {
            string sql = "SELECT count(EPED_CODIGO) FROM ESTADO_PEDIDOS WHERE EPED_NOMBRE = @p0";
            object[] valorParametros = { estadoPedido.Nombre };
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

        public static void ObtenerEstadosPedido(string nombre, Data.dsEstadoPedidos ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT EPED_CODIGO, EPED_NOMBRE, EPED_DESCRIPCION
                              FROM ESTADO_PEDIDOS
                              WHERE EPED_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "ESTADO_PEDIDOS", sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = "SELECT EPED_CODIGO, EPED_NOMBRE, EPED_DESCRIPCION FROM ESTADO_PEDIDOS ";
                try
                {
                    DB.FillDataSet(ds, "ESTADO_PEDIDOS", sql, null);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

        public static void ObtenerEstadosPedido(Data.dsEstadoPedidos ds)
        {
            string sql = @"SELECT EPED_CODIGO, EPED_NOMBRE, EPED_DESCRIPCION
                           FROM ESTADO_PEDIDOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "ESTADO_PEDIDOS", sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerEstadosPedido(Data.dsCliente ds)
        {
            string sql = @"SELECT EPED_CODIGO, EPED_NOMBRE, EPED_DESCRIPCION
                           FROM ESTADO_PEDIDOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "ESTADO_PEDIDOS", sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerEstadosPedido(DataTable dtEstadoPedido)
        {
            string sql = @"SELECT EPED_CODIGO, EPED_NOMBRE, EPED_DESCRIPCION
                           FROM ESTADO_PEDIDOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataTable(dtEstadoPedido, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(long codigo)
        {
            string sql = "SELECT count(PED_CODIGO) FROM PEDIDOS WHERE EPED_CODIGO = @p0";
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
