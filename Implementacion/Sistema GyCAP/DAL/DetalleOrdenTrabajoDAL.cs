using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DetalleOrdenTrabajoDAL
    {
        public static int Insertar(Data.dsOrdenTrabajo.DETALLE_ORDENES_TRABAJORow row, SqlTransaction transaccion)
        {
            string sql = @"INSERT INTO DETALLE_ORDENES_TRABAJO 
                         ([dord_codigo]
                         ,[ord_numero]
                         ,[eord_codigo]
                         ,[par_codigo]
                         ,[par_tipo]
                         ,[dord_origen]
                         ,[dord_cantidadestimada]
                         ,[dord_cantidadreal]
                         ,[dord_fechainicioestimada]
                         ,[dord_fechainicioral]
                         ,[dord_fechafinestimada]
                         ,[dord_fechafinreal]
                         ,[dord_horainicioestimada]
                         ,[dord_horainicioreal]
                         ,[dord_horafinestimada]
                         ,[dord_horafinreal]
                         ,[estr_codigo]
                         ,[cto_codigo]
                         ,[opr_numero]
                         ,[dord_observaciones]
                         ,[dord_ordenprecendente]
                         ,[dord_ordensiguiente]
                         ,[dord_nivel])
                         VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, 
                                 @p16, @p17, @p18, @p19, @p20, @p21, @p22, @p23) SELECT @@Identity";

            object ord = DBNull.Value, precedente = DBNull.Value, siguiente = DBNull.Value;
            if (!row.IsORD_NUMERONull()) { ord = row.ORD_NUMERO; }
            if (!row.IsDORD_ORDENPRECEDENTENull()) { precedente = row.DORD_ORDENPRECEDENTE; }
            if (!row.IsDORD_ORDENSIGUIENTENull()) { siguiente = row.DORD_ORDENSIGUIENTE; }
            object[] valoresParametros = { row.DORD_CODIGO,
                                             ord,
                                             row.EORD_CODIGO,
                                             row.PAR_CODIGO,
                                             row.PAR_TIPO,
                                             row.DORD_ORIGEN,
                                             row.DORD_CANTIDADESTIMADA,
                                             DBNull.Value,
                                             row.DORD_FECHAINICIOESTIMADA,
                                             DBNull.Value,
                                             row.DORD_FECHAFINESTIMADA,
                                             DBNull.Value,
                                             row.DORD_HORAINICIOESTIMADA,
                                             DBNull.Value,
                                             row.DORD_HORAFINESTIMADA,
                                             DBNull.Value,
                                             row.ESTR_CODIGO,
                                             row.CTO_CODIGO,
                                             row.OPR_NUMERO,
                                             row.DORD_OBSERVACIONES,
                                             precedente,
                                             siguiente,
                                             row.DORD_NIVEL };

            row.DORD_NUMERO = Convert.ToInt32(DB.executeScalar(sql, valoresParametros, transaccion));
            return Convert.ToInt32(row.DORD_NUMERO);
        }

      

        
    }
}
