using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using GyCAP.Entidades.Excepciones;
using GyCAP.Entidades;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades.BindingEntity;

namespace GyCAP.DAL
{
    public class TipoCentroTrabajoDAL
    {
        public static void GetAll(DataTable dt)
        {
            string sql = "SELECT tc_codigo, tc_nombre FROM TIPOS_CENTRO_TRABAJO";

            try
            {
                DB.FillDataTable(dt, sql, null);
            }
            catch (SqlException ex) { throw new BaseDeDatosException(ex.Message); }
        }

        public static TipoCentroTrabajo GetTipo(RecursosFabricacionEnum.TipoCentroTrabajo tipo)
        {
            return GetTipo((int)tipo);
        }

        public static TipoCentroTrabajo GetTipo(int codigo)
        {
            string sql = "SELECT ct_codigo, ct_nombre FROM TIPOS_CENTRO_TRABAJO WHERE ct_codigo = @p0";
            object[] parametros = { codigo };

            try
            {
                TipoCentroTrabajo tipo = new TipoCentroTrabajo();
                Data.dsHojaRuta.CENTROS_TRABAJOSDataTable dt = new GyCAP.Data.dsHojaRuta.CENTROS_TRABAJOSDataTable();
                DB.FillDataTable(dt, sql, null);
                tipo.Codigo = Convert.ToInt32(dt.Rows[0]["ct_codigo"]);
                tipo.Nombre = dt.Rows[0]["ct_nombre"].ToString();

                return tipo;
            }
            catch (SqlException ex) { throw new BaseDeDatosException(ex.Message); }
        }
    }
}
