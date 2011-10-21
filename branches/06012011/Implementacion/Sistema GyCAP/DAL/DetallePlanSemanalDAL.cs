using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DetallePlanSemanalDAL
    {
        //BUSQUEDA
        //Metodo de Busqueda
        public static void ObtenerDetalle(DataTable dtDetalle, int idCodigo)
        {
            string sql = @"SELECT dpsem_codigo, coc_codigo, dpsem_cantidadestimada, 
                                  dpsem_cantidadreal, diapsem_codigo, dpsem_estado, 
                                  dped_codigo, dpsem_cod_nemonico 
                           FROM DETALLE_PLANES_SEMANALES WHERE diapsem_codigo=@p0";

            object[] parametros = { idCodigo };

            try
            {
                DB.FillDataTable(dtDetalle, sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ActualizarEstado(int codigoDetalle, int codigoEstado)
        {
            string sql = @"UPDATE DETALLE_PLANES_SEMANALES 
                           SET dpsem_estado = @p0 WHERE dpsem_codigo = @p1";

            object[] parametros = { codigoEstado, codigoDetalle };

            try
            {
                DB.executeNonQuery(sql, parametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void ActualizarEstado(int[] codigosDetalle, int codigoEstado, SqlTransaction transaccion)
        {
            string sql = @"UPDATE DETALLE_PLANES_SEMANALES SET dpsem_estado = @p0 WHERE dpsem_codigo IN (@p1)";

            object[] parametros = { codigoEstado, codigosDetalle };
                        
            DB.executeNonQuery(sql, parametros, transaccion);
        }

        public static object ObtenerPedidoClienteDeDetalle(int codigoDetalle, SqlTransaction transaccion)
        {
            string sql = @"SELECT dped_codigo FROM DETALLE_PLANES_SEMANALES WHERE dpsem_codigo = @p0";

            object[] parametros = { codigoDetalle };

            try
            {
                return DB.executeScalar(sql, parametros, transaccion);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void SumarCantidadFinalizada(int codigoDetalle, int codigoCocina, int cantidad, SqlTransaction transaccion)
        {
            string sql = "UPDATE DETALLE_PLANES_SEMANALES SET dpsem_cantidadreal = dpsem_cantidadreal  + @p0 WHERE dpsem_codigo = @p1";
            object[] parametros = { cantidad, codigoDetalle };
            DB.executeNonQuery(sql, parametros, transaccion);

            sql = "SELECT diapsem_codigo FROM DETALLE_PLANES_SEMANALES WHERE dpsem_codigo = @p0";
            parametros = new object[] { codigoDetalle };

            DiasPlanSemanalDAL.SumarCantidadFinalizada(Convert.ToInt32(DB.executeScalar(sql, parametros, transaccion)), codigoCocina, cantidad, transaccion);
        }
    }
}
