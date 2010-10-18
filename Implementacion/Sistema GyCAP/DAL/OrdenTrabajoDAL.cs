using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class OrdenTrabajoDAL
    {
        public static int Insertar(Data.dsOrdenTrabajo.ORDENES_TRABAJORow row, SqlTransaction transaccion)
        {
            string sql = @"INSERT INTO ORDENES_TRABAJO 
                         ([ordt_codigo]
                         ,[ordp_numero]
                         ,[eord_codigo]
                         ,[par_codigo]
                         ,[par_tipo]
                         ,[ordt_origen]
                         ,[ordt_cantidadestimada]
                         ,[ordt_cantidadreal]
                         ,[ordt_fechainicioestimada]
                         ,[ordt_fechainicioreal]
                         ,[ordt_fechafinestimada]
                         ,[ordt_fechafinreal]
                         ,[ordt_horainicioestimada]
                         ,[ordt_horainicioreal]
                         ,[ordt_horafinestimada]
                         ,[ordt_horafinreal]
                         ,[estr_codigo]
                         ,[cto_codigo]
                         ,[opr_numero]
                         ,[ordt_observaciones]
                         ,[ordt_ordensiguiente]
                         ,[ordt_nivel]
                         ,[ordt_secuencia]
                         ,[ustck_origen]
                         ,[ustck_destino]
                         ,[hr_codigo])
                         VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, 
                                 @p16, @p17, @p18, @p19, @p20, @p21, @p22, @p23, @p24, @p25) SELECT @@Identity";

            object siguiente = DBNull.Value, stockOrigen = DBNull.Value, stockDestino = DBNull.Value;
            if (!row.IsORDT_ORDENSIGUIENTENull()) { siguiente = row.ORDT_ORDENSIGUIENTE; }
            if (!row.IsUSTCK_ORIGENNull()) { stockOrigen = row.USTCK_ORIGEN; }
            if (!row.IsUSTCK_DESTINONull()) { stockDestino = row.USTCK_DESTINO; }
            object[] valoresParametros = { row.ORDT_CODIGO,
                                             row.ORDP_NUMERO,
                                             row.EORD_CODIGO,
                                             row.PAR_CODIGO,
                                             row.PAR_TIPO,
                                             row.ORDT_ORIGEN,
                                             row.ORDT_CANTIDADESTIMADA,
                                             row.ORDT_CANTIDADREAL,
                                             row.ORDT_FECHAINICIOESTIMADA,
                                             DBNull.Value,
                                             row.ORDT_FECHAFINESTIMADA,
                                             DBNull.Value,
                                             row.ORDT_HORAINICIOESTIMADA,
                                             DBNull.Value,
                                             row.ORDT_HORAFINESTIMADA,
                                             DBNull.Value,
                                             row.ESTR_CODIGO,
                                             row.CTO_CODIGO,
                                             row.OPR_NUMERO,
                                             row.ORDT_OBSERVACIONES,
                                             siguiente,
                                             row.ORDT_NIVEL,
                                             row.ORDT_SECUENCIA,
                                             stockOrigen,
                                             stockDestino,
                                             row.HR_CODIGO };

            return Convert.ToInt32(DB.executeScalar(sql, valoresParametros, transaccion));
        }

        public static void ObtenerOrdenesTrabajo(int numeroOrdenProduccion, DataTable dtOrdenTrabajo)
        {
            string sql = @"SELECT ordt_numero, ordt_codigo, ordp_numero, eord_codigo, par_codigo, par_tipo, ordt_origen, ordt_cantidadestimada
                         , ordt_cantidadreal, ordt_fechainicioestimada, ordt_fechainicioreal, ordt_fechafinestimada, ordt_fechafinreal
                         , ordt_horainicioestimada, ordt_horainicioreal, ordt_horafinestimada, ordt_horafinreal, estr_codigo, cto_codigo, opr_numero
                         , ordt_observaciones, ordt_ordensiguiente, ordt_nivel,ordt_secuencia, ustck_origen, ustck_destino, hr_codigo  
                        FROM ORDENES_TRABAJO WHERE ordp_numero = @p0";

            object[] parametros = { numeroOrdenProduccion };

            try
            {
                DB.FillDataTable(dtOrdenTrabajo, sql, parametros);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
