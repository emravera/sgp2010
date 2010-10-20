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
        public static readonly int EstadoGenerado = 1;
        public static readonly int EstadoEnEspera = 2;
        public static readonly int EstadoEnProceso = 3;
        public static readonly int EstadoFinalizado = 4;
        public static readonly int EstadoCancelado = 5;
        
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

        public static void IniciarOrdenTrabajo(int numeroOrdenTrabajo, Data.dsOrdenTrabajo dsOrdenTrabajo, Data.dsStock dsStock, SqlTransaction transaccion)
        {
            string sql = "UPDATE ORDENES_TRABAJO SET eord_codigo = @p0, ordt_fechainicioreal = @p1 WHERE ordt_numero = @p2";
            object[] parametros = { dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).EORD_CODIGO, 
                                     dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).ORDT_FECHAINICIOREAL, 
                                     numeroOrdenTrabajo };

            DB.executeNonQuery(sql, parametros, transaccion);

            //Insertamos los movimientos de stock que corresponden a la OT
            Entidades.MovimientoStock movimiento;
            foreach (Data.dsStock.MOVIMIENTOS_STOCKRow rowMVTO in (Data.dsStock.MOVIMIENTOS_STOCKRow[])dsStock.MOVIMIENTOS_STOCK.Select("ORDT_NUMERO = " + numeroOrdenTrabajo))
            {
                movimiento = new GyCAP.Entidades.MovimientoStock();
                movimiento.Codigo = rowMVTO.MVTO_CODIGO;
                movimiento.Descripcion = rowMVTO.MVTO_DESCRIPCION;
                movimiento.FechaAlta = rowMVTO.MVTO_FECHAALTA;
                movimiento.FechaPrevista = rowMVTO.MVTO_FECHAPREVISTA;
                movimiento.Origen = new GyCAP.Entidades.UbicacionStock(Convert.ToInt32(rowMVTO.USTCK_ORIGEN));
                movimiento.Destino = new GyCAP.Entidades.UbicacionStock(Convert.ToInt32(rowMVTO.USTCK_DESTINO));
                movimiento.CantidadOrigen = rowMVTO.MVTO_CANTIDAD_ORIGEN;
                movimiento.CantidadDestino = rowMVTO.MVTO_CANTIDAD_DESTINO;
                movimiento.Estado = new GyCAP.Entidades.EstadoMovimientoStock(Convert.ToInt32(rowMVTO.EMVTO_CODIGO));
                movimiento.OrdenTrabajo = new GyCAP.Entidades.OrdenTrabajo(Convert.ToInt32(rowMVTO.ORDT_NUMERO));
                MovimientoStockDAL.Insertar(movimiento, transaccion);
                rowMVTO.MVTO_NUMERO = movimiento.Numero;
            }
            //Actualizamos la ubicación destino de la orden de trabajo
            UbicacionStockDAL.ActualizarCantidadesStock(Convert.ToInt32(dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).USTCK_DESTINO),
                                                        0,
                                                        dsOrdenTrabajo.ORDENES_TRABAJO.FindByORDT_NUMERO(numeroOrdenTrabajo).ORDT_CANTIDADESTIMADA,
                                                        transaccion);
        }

        public static void ActualizarEstado(int numeroOrdenTrabajo, int codigoEstado, SqlTransaction transaccion)
        {
            string sql = "UPDATE ORDENES_TRABAJO SET eord_codigo = @p0 WHERE ordt_numero = @p1";
            object[] parametros = { codigoEstado, numeroOrdenTrabajo };
            DB.executeNonQuery(sql, parametros, transaccion);
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
