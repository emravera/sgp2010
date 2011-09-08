﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class EstadoUsuarioDAL
    {
        public static long Insertar(Entidades.EstadoUsuario estadoUsuario)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO ESTADO_USUARIOS (EU_NOMBRE, EU_DESCRIPCION) VALUES (@p0,@p1) SELECT @@Identity";
            object[] valorParametros = { estadoUsuario.Nombre, estadoUsuario.Descripcion };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM ESTADO_USUARIOS WHERE EU_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.EstadoUsuario estadoUsuario)
        {
            string sql = "UPDATE ESTADO_USUARIOS SET EU_NOMBRE = @p1, EU_DESCRIPCION = @p2 WHERE EU_CODIGO = @p0";
            object[] valorParametros = { estadoUsuario.Codigo, estadoUsuario.Nombre, estadoUsuario.Descripcion };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsEstadoUsuario(Entidades.EstadoUsuario estadoUsuario)
        {
            string sql = "SELECT count(EU_CODIGO) FROM ESTADO_USUARIOS WHERE EU_NOMBRE = @p0";
            object[] valorParametros = { estadoUsuario.Nombre };
            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerEstadosUsuario(string nombre, Data.dsSeguridad ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT EU_CODIGO, EU_NOMBRE, EU_DESCRIPCION
                              FROM ESTADO_USUARIOS
                              WHERE EU_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "ESTADO_USUARIOS", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = "SELECT EU_CODIGO, EU_NOMBRE, EU_DESCRIPCION FROM ESTADO_USUARIOS ";
                try
                {
                    DB.FillDataSet(ds, "ESTADO_USUARIOS", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

        public static void ObtenerEstadosUsuario(Data.dsSeguridad ds)
        {
            string sql = @"SELECT EU_CODIGO, EU_NOMBRE, EU_DESCRIPCION
                           FROM ESTADO_USUARIOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "ESTADO_USUARIOS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerEstadosUsuario(DataTable dtEstadoUsuario)
        {
            string sql = @"SELECT EU_CODIGO, EU_NOMBRE, EU_DESCRIPCION
                           FROM ESTADO_USUARIOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataTable(dtEstadoUsuario, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(U_CODIGO) FROM REPUESTOS WHERE EU_CODIGO = @p0";
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
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
