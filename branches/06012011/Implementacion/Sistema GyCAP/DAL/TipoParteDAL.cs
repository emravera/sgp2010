using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class TipoParteDAL
    {
        public static readonly int TipoConjunto = 1;
        public static readonly int TipoSubconjunto = 2;
        public static readonly int TipoPieza = 3;
        public static readonly int TipoProductoTerminado = 4;

        public static void ObtenerTodos(DataTable dtTiposPartes)
        {
            string sql = "SELECT tpar_codigo, tpar_nombre, tpar_descripcion, tpar_productoterminado FROM TIPOS_PARTES";

            try
            {
                DB.FillDataTable(dtTiposPartes, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Insertar(Entidades.TipoParte tipoParte)
        {
            string sql = @"INSERT INTO [TIPOS_PARTES] ([tpar_nombre],[tpar_descripcion],[tpar_productoterminado]) 
                        VALUES (@p0, @p1, @p2) SELECT @@Identity";

            object[] parametros = { tipoParte.Nombre, tipoParte.Descripcion, tipoParte.ProductoTerminado };
            
            try
            {
                tipoParte.Codigo = Convert.ToInt32(DB.executeScalar(sql, parametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.TipoParte tipoParte)
        {
            string sql = @"UPDATE TIPOS_PARTES SET tpar_nombre = @p0, tpar_descripcion = @p1, tpar_productoterminado = @p2 
                        WHERE tpar_codigo = @p3";

            object[] parametros = { tipoParte.Nombre, tipoParte.Descripcion, tipoParte.ProductoTerminado, tipoParte.Codigo };

            try
            {
                DB.executeNonQuery(sql, parametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigoTipoParte)
        {
        }

        public static bool PuedeEliminarse(int codigoTipoParte)
        {
            string sql = "SELECT count(tpar_codigo) FROM TIPOS_PARTES WHERE tpar_codigo = @p0";
            object[] parametros = { codigoTipoParte };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, parametros, null)) == 0)
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

        public static bool EsTipoParte(Entidades.TipoParte tipoParte)
        {
            string sql = "SELECT count(tpar_codigo) FROM TIPOS_PARTES WHERE tpar_nombre = @p0";
            object[] parametros = { tipoParte.Nombre };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, parametros, null)) == 0)
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
