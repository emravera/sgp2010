using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using GyCAP.Entidades;
using GyCAP.Entidades.Excepciones;

namespace GyCAP.DAL
{
    public class FabricaDAL
    {
        public static DataTable GetOperacionesCentrosByPartes(int[] numerosPartes)
        {
            string sql = @"SELECT P.part_numero, O.opr_horasrequerida, C.cto_horastrabajonormal, C.cto_horastrabajoextendido 
                            FROM HOJAS_RUTA H, DETALLE_HOJARUTA D, OPERACIONES O, CENTROS_TRABAJOS C, PARTES P 
                            WHERE O.opr_numero = D.opr_numero 
                            AND C.cto_codigo = D.cto_codigo 
                            AND D.hr_codigo = H.hr_codigo 
                            AND H.hr_codigo = P.hr_codigo 
                            AND P.part_numero IN (@p0)";

            object[] parametros = { numerosPartes };
            DataTable dt = new DataTable();

            try
            {
                DB.FillDataTable(dt, sql, parametros);
                return dt;
            }
            catch (SqlException ex) { throw new BaseDeDatosException(ex.Message); }
        }
    }
}
