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
            string sql = @"SELECT dpsem_codigo, coc_codigo, dpsem_cantidadestimada, dpsem_cantidadreal, diapsem_codigo
                        FROM DETALLE_PLANES_SEMANALES WHERE diapsem_codigo=@p0";

            object[] parametros = { idCodigo };

            try
            {
                DB.FillDataTable(dtDetalle, sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
