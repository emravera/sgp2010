﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DetalleDemandaAnualDAL
    {
        public static void ObtenerDetalle(int idCodigo, Data.dsEstimarDemanda ds)
        {
            string sql = @"SELECT ddeman_codigo, deman_codigo, ddeman_mes, ddeman_cantidadmes
                        FROM DETALLE_DEMANDAS_ANUALES WHERE deman_codigo=@p0";

            object[] parametros = { idCodigo };

            try
            {
                DB.FillDataSet(ds, "DETALLE_DEMANDAS_ANUALES", sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
