using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class RepuestoDAL
    {
        public static long Insertar(Entidades.Repuesto repuesto)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = @"INSERT INTO TIPOS_REPUESTOS (TREP_CODIGO,
                                                        REP_NOMBRE, 
                                                        TREP_DESCRIPCION,
                                                        REP_CANTIDADSTOCK,
                                                        REP_COSTO) VALUES (@p0,@p1,@p2,@p3,@p4) SELECT @@Identity";
            
            object[] valorParametros = {repuesto.Tipo.Codigo, repuesto.Nombre, repuesto.Descripcion,
                                        repuesto.CantidadStock,repuesto.Costo };
            try
            {
                return Convert.ToInt64(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(long codigo)
        {
            string sql = "DELETE FROM REPUESTOS WHERE REP_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.Repuesto repuesto)
        {
            string sql = @"UPDATE REPUESTOS SET TREP_CODIGO = @p1, 
                                                TREP_NOMBRE = @p2, 
                                                TREP_DESCRIPCION = @p3,
                                                REP_CANTIDADSTOCK = @p4, 
                                                REP_COSTO = @p5 
                                                WHERE TREP_CODIGO = @p0";

            object[] valorParametros = { repuesto.Codigo, repuesto.Tipo.Codigo, repuesto.Nombre, repuesto.Descripcion,
                                         repuesto.CantidadStock, repuesto.Costo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsRepuesto(Entidades.Repuesto repuesto)
        {
            string sql = "SELECT count(REP_CODIGO) FROM REPUESTOS WHERE REP_NOMBRE = @p0";
            object[] valorParametros = { repuesto.Nombre };
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
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerRepuesto(string nombre, Data.dsMantenimiento ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT TREP_CODIGO, REP_NOMBRE, REP_DESCRIPCION
                               REP_CANTIDADSTOCK, REP_COSTO 
                              FROM REPUESTOS
                              WHERE REP_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "REPUESTOS", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = @"SELECT TREP_CODIGO, REP_NOMBRE, REP_DESCRIPCION
                               REP_CANTIDADSTOCK, REP_COSTO 
                               FROM REPUESTOS";
                try
                {
                    DB.FillDataSet(ds, "REPUESTOS", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

        public static void ObtenerRepuesto(Data.dsMantenimiento ds)
        {
            string sql = @"SELECT TREP_CODIGO, REP_NOMBRE, REP_DESCRIPCION
                               REP_CANTIDADSTOCK, REP_COSTO 
                              FROM REPUESTOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "REPUESTOS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //        public static void ObtenerTipoRepuesto(Data.dsCliente ds)
        //        {
        //            string sql = @"SELECT TREP_CODIGO, TREP_NOMBRE, TREP_DESCRIPCION
        //                           FROM TIPOS_REPUESTOS";
        //            try
        //            {
        //                //Se llena el Dataset
        //                DB.FillDataSet(ds, "ESTADO_PEDIDOS", sql, null);
        //            }
        //            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        //        }

        public static void ObtenerRepuesto(DataTable dtRepuesto)
        {
            string sql = @"SELECT REP_CODIGO, TREP_CODIGO, REP_NOMBRE, REP_DESCRIPCION,
                           REP_CANTIDADSTOCK, REP_COSTO 
                           FROM REPUESTOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataTable(dtRepuesto, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(long codigo)
        {
            string sql = "SELECT count(REP_CODIGO) FROM DETALLE_PLANES_MANTENIMIENTO WHERE DPMAN_CODIGO = @p0";
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
