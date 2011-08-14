using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class CompuestoParteDAL
    {
        public static readonly int HijoEsParte = 1;
        public static readonly int HijoEsMP = 2;
        
        public static void Insertar(Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompuesto, SqlTransaction transaccion)
        {
            string sql = @"INSERT INTO [COMPUESTOS_PARTES] 
                       ([part_numero],
                        [mp_codigo],
                        [comp_cantidad],
                        [estr_codigo],
                        [umed_codigo],
                        [comp_codigo_padre])
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5) SELECT @@Identity";

            object parte = DBNull.Value, mp = DBNull.Value, compPadre = DBNull.Value;
            if (!rowCompuesto.IsPART_NUMERONull()) { parte = rowCompuesto.PART_NUMERO; }
            else if (!rowCompuesto.IsMP_CODIGONull()) { mp = rowCompuesto.MP_CODIGO; }            
            if (!rowCompuesto.IsCOMP_CODIGO_PADRENull()) { compPadre = rowCompuesto.COMP_CODIGO_PADRE; }

            object[] parametros = { parte,
                                    mp,
                                    rowCompuesto.COMP_CANTIDAD,
                                    rowCompuesto.ESTR_CODIGO,
                                    rowCompuesto.UMED_CODIGO,
                                    compPadre };

            rowCompuesto.BeginEdit();
            rowCompuesto.COMP_CODIGO = Convert.ToInt32(DB.executeScalar(sql, parametros, transaccion));
            rowCompuesto.EndEdit();
        }

        public static void Actualizar(Data.dsEstructuraProducto.COMPUESTOS_PARTESRow rowCompuesto, SqlTransaction transaccion)
        {
            string sql = "UPDATE COMPUESTOS_PARTES SET comp_cantidad = @p0 WHERE comp_codigo = @p1";
            object[] parametros = { rowCompuesto.COMP_CANTIDAD, rowCompuesto.COMP_CODIGO };
            DB.executeNonQuery(sql, parametros, transaccion);
        }

        public static void Eliminar(int codCompuesto, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM COMPUESTOS_PARTES WHERE comp_codigo = @p0";
            object[] parametros = { codCompuesto };
            DB.executeNonQuery(sql, parametros, transaccion);
        }

        public static void EliminarCompuestosDeEstructura(int codigoEstructura, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM COMPUESTOS_PARTES WHERE estr_codigo = @p0";
            object[] parametros = { codigoEstructura };
            DB.executeNonQuery(sql, parametros, transaccion);
        }

        public static void ObtenerCompuestosEstructura(int codigoEstructura, DataTable dtCompuestos_Partes)
        {
            string sql = @"SELECT comp_codigo, part_numero, mp_codigo, comp_cantidad, 
                                    umed_codigo, estr_codigo, comp_codigo_padre   
                        FROM COMPUESTOS_PARTES WHERE estr_codigo = @p0";

            object[] parametros = { codigoEstructura };

            try
            {
                DB.FillDataTable(dtCompuestos_Partes, sql, parametros);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }            
        }
        
        public static void ObtenerCompuestosParte(int codigoPartePadre, Data.dsEstructuraProducto dsEstructura)
        {
            string sql = @"SELECT comp_codigo, part_numero, mp_codigo, comp_cantidad, 
                                    umed_codigo, estr_codigo, comp_codigo_padre  
                        FROM COMPUESTOS_PARTES WHERE part_numero_padre = @p0";

            object[] parametros = { codigoPartePadre };
            DB.FillDataSet(dsEstructura, "COMPUESTOS_PARTE", sql, parametros);
        }

        public static void ObtenerCompuestosPartesEstructura(int[] codigosEstructura, DataTable dtCompuestosPartes)
        {
            string sql = @"SELECT comp_codigo, part_numero, mp_codigo, comp_cantidad, estr_codigo, 
                                    umed_codigo, comp_codigo_padre  
                            FROM COMPUESTOS_PARTES WHERE estr_codigo IN (@p0)";

            object[] valorParametros = { codigosEstructura };
            DB.FillDataTable(dtCompuestosPartes, sql, valorParametros);
        }
    }
}
