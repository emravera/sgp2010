using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class EstadoMovimientoStockDAL
    {
        public const string Planificado = "Planificado";
        public const string Finalizado = "Finalizado";
        public const string Cancelado = "Cancelado";
        
        public static void ObtenerEstados(DataTable dtEstadosMovimientoStock)
        {
            string sql = "SELECT emvto_codigo, emvto_nombre, emvto_descripcion FROM ESTADO_MOVIMIENTOS_STOCK";

            try
            {
                DB.FillDataTable(dtEstadosMovimientoStock, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static Entidades.EstadoMovimientoStock GetEstadoEntity(string nombreEstado)
        {
            string sql = @"SELECT emvto_codigo, emvto_nombre, emvto_descripcion FROM ESTADO_MOVIMIENTOS_STOCK
                            WHERE emvto_nombre = @p0";

            object[] parametros = { nombreEstado };

            Entidades.EstadoMovimientoStock estado = new GyCAP.Entidades.EstadoMovimientoStock();

            try
            {
                Data.dsStock.ESTADO_MOVIMIENTOS_STOCKDataTable dt = new GyCAP.Data.dsStock.ESTADO_MOVIMIENTOS_STOCKDataTable();
                DB.FillDataTable(dt, sql, parametros);

                if (dt.Rows.Count == 1)
                {
                    estado.Codigo = Convert.ToInt32(dt.Rows[0]["emvto_codigo"].ToString());
                    estado.Nombre = dt.Rows[0]["emvto_nombre"].ToString();
                    estado.Descripcion = dt.Rows[0]["emvto_descripcion"].ToString();
                }
                
                return estado;
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
