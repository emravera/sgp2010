using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class CompuestoParteDAL
    {
        public static void Insertar(Entidades.CompuestoParte compuesto, SqlTransaction transaccion)
        {
            string sql = @"INSERT INTO [COMPUESTOS_PARTES] 
                       ([part_numero_padre],
                        [part_numero_hijo],
                        [mp_codigo],
                        [comp_cantidad],
                        [umed_codigo]) 
                        VALUES (@p0, @p1, @p2, @p3, @p4) SELECT @@Identity";

            object hijo = DBNull.Value, mp = DBNull.Value;
            if (compuesto.ParteHijo != null) { hijo = compuesto.ParteHijo; }
            else if (compuesto.MateriaPrima != null) { mp = compuesto.MateriaPrima; }

            object[] parametros = { compuesto.PartePadre,
                                    hijo,
                                    mp,
                                    compuesto.Cantidad,
                                    compuesto.UnidadMedida.Codigo };

            compuesto.Codigo = Convert.ToInt32(DB.executeScalar(sql, parametros, transaccion));
        }

        public static void Actualizar(Entidades.CompuestoParte compuesto, SqlTransaction transaccion)
        {
            string sql = "UPDATE COMPUESTOS_PARTES SET comp_cantidad = @p0, umed_codigo = @p1 WHERE comp_codigo = @p3";
            object[] parametros = { compuesto.Cantidad, compuesto.UnidadMedida.Codigo, compuesto.Codigo };
            DB.executeNonQuery(sql, parametros, transaccion);
        }

        public static void Eliminar(Entidades.CompuestoParte compuesto, SqlTransaction transaccion)
        {
            string sql = "DELETE FROM COMPUESTOS_PARTES WHERE comp_codigo = @p0";
            object[] parametros = { compuesto.Codigo };
            DB.executeNonQuery(sql, parametros, transaccion);
        }

        public static void ObtenerCompuestosParte(int codigoPartePadre, Data.dsEstructuraProducto dsEstructura)
        {
            string sql = @"SELECT comp_codigo, part_numero_padre, part_numero_hijo, mp_codigo, comp_cantidad, umed_codigo 
                        FROM COMPUESTOS_PARTES WHERE part_numero_padre = @p0";
            object[] parametros = { codigoPartePadre };
            DB.FillDataSet(dsEstructura, "COMPUESTOS_PARTE", sql, parametros);
        }
    }
}
