using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class DetallePlanMensualDAL
    {
        //Metodo de Busqueda
        public static void ObtenerDetalle(int idCodigo, Data.dsPlanMensual ds)
        {
            string sql = @"SELECT dpmes_codigo, pmes_codigo, coc_codigo, dpmes_cantidadEstimada, dpmes_cantidadReal, dped_codigo
                        FROM DETALLE_PLANES_MENSUALES WHERE pmes_codigo=@p0";

            object[] parametros = { idCodigo };

            try
            {
                DB.FillDataSet(ds, "DETALLE_PLANES_MENSUALES", sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo de Busqueda
        public static void ObtenerDetallePM(int idCodigoAnio, string mes, DataTable dtDetalle)
        {
            string sql = @"SELECT det.dpmes_codigo, det.pmes_codigo, det.coc_codigo, det.dpmes_cantidadEstimada, det.dpmes_cantidadReal, det.dped_codigo
                        FROM DETALLE_PLANES_MENSUALES as det, PLANES_MENSUALES as pm
                        WHERE det.pmes_codigo=pm.pmes_codigo and pm.pan_codigo=@p0 and pm.pmes_mes LIKE @p1";

            mes = "%" + mes + "%";
            object[] parametros = { idCodigoAnio, mes };

            try
            {
                DB.FillDataTable(dtDetalle, sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo de Busqueda
        public static void ObtenerDetalle(DataTable dtDetalle)
        {
            string sql = @"SELECT dpmes_codigo, pmes_codigo, coc_codigo, dpmes_cantidadEstimada, dpmes_cantidadReal, dped_codigo
                        FROM DETALLE_PLANES_MENSUALES";

            try
            {
                DB.FillDataTable(dtDetalle, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
