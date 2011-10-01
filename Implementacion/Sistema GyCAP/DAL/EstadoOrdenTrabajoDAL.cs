using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades;

namespace GyCAP.DAL
{
    public class EstadoOrdenTrabajoDAL
    {
        public static void ObtenerEstados(DataTable dtEstadosOrden)
        {
            string sql = "SELECT eord_codigo, eord_nombre, eord_descripcion FROM ESTADO_ORDENES_TRABAJO ORDER BY eord_nombre ASC";

            try
            {
                DB.FillDataTable(dtEstadosOrden, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static int ObtenerEstadoGenerada()
        {
            string sql = "SELECT eord_codigo FROM ESTADO_ORDENES_TRABAJO WHERE eord_nombre = @p0";
            object[] parametros = { "Generada" };

            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, parametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static EstadoOrdenTrabajo GetEstado(OrdenesTrabajoEnum.EstadoOrdenEnum estadoCocina)
        {
            string sql = "SELECT eord_codigo, eord_nombre, eord_descripcion FROM ESTADO_ORDENES_TRABAJO WHERE eord_nombre = @p0";
            object[] parametros = { OrdenesTrabajoEnum.GetFriendlyName(estadoCocina) };
            DataTable dt = new DataTable();
            EstadoOrdenTrabajo estado = new EstadoOrdenTrabajo();

            try
            {
                DB.FillDataTable(dt, sql, parametros);
                if (dt.Rows.Count > 0)
                {
                    estado.Codigo = Convert.ToInt32(dt.Rows[0]["eord_codigo"]);
                    estado.Nombre = dt.Rows[0]["eord_nombre"].ToString();
                    estado.Descripcion = dt.Rows[0]["eord_descripcion"].ToString();
                }

                return estado;
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
