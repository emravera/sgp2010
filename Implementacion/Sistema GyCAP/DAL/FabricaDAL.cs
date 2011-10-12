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
            string sql = @"SELECT P.part_numero, C.cto_horastrabajonormal, C.cto_horastrabajoextendido, 
                                  C.ct_tipo, C.cto_activo, C.cto_capacidadciclo, C.cto_horasciclo, 
                                  C.cto_tiempoantes, C.cto_tiempodespues, C.cto_eficiencia, C.cto_capacidadunidadhora 
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

        public static IList<HistoricoEficienciaCentro> GetHistoricoEficienciaCentroTrabajo(int codigoCentro, DateTime fechaDesde, DateTime fechaHasta)
        {
            IList<HistoricoEficienciaCentro> lista = new List<HistoricoEficienciaCentro>();
            DataTable dt = new DataTable();

            string sql = @"SELECT DHR.cto_codigo, CI.opr_fallidas, OP.ordp_fechafinreal, OT.ordt_cantidadreal
                            FROM centros_trabajos CTO, ordenes_trabajo OT, cierre_orden_trabajo CI, ordenes_produccion OP, detalle_hojaruta DHR
                            WHERE OP.ordp_fechafinreal >= @p0 AND OP.ordp_fechafinreal <= @p1
                            AND OP.ordp_numero = OT.ordp_numero
                            AND OT.dhr_codigo = DHR.dhr_codigo
                            AND DHR.cto_codigo = @p2
                            AND OT.ordt_numero = CI.ordt_numero";

            object[] parametros = { fechaDesde.ToString("yyyyMMdd"), fechaHasta.ToString("yyyyMMdd"), codigoCentro };

            try
            {
                DB.FillDataTable(dt, sql, parametros);

                foreach (DataRow row in dt.Rows)
                {
                    //lista.Add
                }

                return lista;
            }
            catch (SqlException ex) { throw new BaseDeDatosException(ex.Message); }
        }
    }
}
