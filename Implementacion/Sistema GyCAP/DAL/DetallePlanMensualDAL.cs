using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DetallePlanMensualDAL
    {
        //Metodo de Busqueda
        public static void ObtenerDetalle(int idCodigo, Data.dsPlanMensual ds)
        {
            string sql = @"SELECT dpmes_codigo, pmes_codigo, coc_codigo, dpmes_cantidadEstimada, dpmes_cantidadReal
                        FROM DETALLE_PLANES_MENSUALES WHERE pmes_codigo=@p0";

            object[] parametros = { idCodigo };

            try
            {
                DB.FillDataSet(ds, "DETALLE_PLANES_MENSUALES", sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }


    }
}
