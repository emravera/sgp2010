
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class PlanSemanalDAL
    {
        //METODOS DE BUSQUEDA
        //Metodo que obtiene las semanas del plan mensual
        public static void obtenerPS(DataTable dtPlanSemanal, int codigoPlanMensual)
        {
            string sql = @"SELECT psem_codigo, pmes_codigo, psem_semana, psem_fechacreacion
                        FROM PLANES_SEMANALES where pmes_codigo=@p0";

            object[] valorParametros = { codigoPlanMensual };
            try
            {
                DB.FillDataTable(dtPlanSemanal, sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //METODO DE VALIDACION
        //Metodo que valida que no exista un plan semanal para ese año, mes y semana ya creado
        public static bool Validar(int codigoPlanMensual, int semana)
        {
            string sql = @"SELECT count(ps.psem_codigo)
                           FROM PLANES_MENSUALES as pm, PLANES_SEMANALES as ps
                           WHERE pm.pmes_codigo=ps.pmes_codigo and ps.psem_semana=@p0 and pm.pmes_codigo=@p1";

            object[] valorParametros = { semana, codigoPlanMensual };
            try
            {
                int cantidad = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
                if (cantidad == 0) return true;
                else return false;
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //METODO DE INSERCION
        public static void GuardarPlanSemanal(Entidades.PlanSemanal planSemanal, Entidades.DiasPlanSemanal diaPlanSemanal, Data.dsPlanSemanal dsPlanSemanal)
        {

            SqlTransaction transaccion = null;

            try
            {
                //Inicio la transaccion 
                transaccion = DB.IniciarTransaccion();

                //Inserto el PlanSemanal
                //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
                string sql = "INSERT INTO [PLANES_SEMANALES] ([pmes_codigo], [psem_semana], [psem_fechacreacion]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
                object[] valorParametros = { planSemanal.PlanMensual.Codigo, planSemanal.Semana, planSemanal.FechaCreacion };
                planSemanal.Codigo = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));

                //Inserto el dia del Plan Semanal
                sql = "INSERT INTO [DIAS_PLAN_SEMANAL] ([diapsem_dia], [diapsem_fecha], [psem_codigo]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
                object[] valorPara = { diaPlanSemanal.Dia, diaPlanSemanal.Fecha, diaPlanSemanal.PlanSemanal.Codigo };
                diaPlanSemanal.Codigo = Convert.ToInt32(DB.executeScalar(sql, valorPara, null));
                
                //Inserto el Detalle
                sql = "INSERT INTO [DETALLE_PLANES_SEMANALES] ([diapsem_codigo], [coc_codigo], [dpsem_cantidadestimada], [dpsem_estado]) VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";
                foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row in (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow[])dsPlanSemanal.DETALLE_PLANES_SEMANALES.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    object[] valorParam = { diaPlanSemanal.Codigo, row.COC_CODIGO, row.DPSEM_CANTIDADESTIMADA, row.DPSEM_ESTADO };
                    row.BeginEdit();
                    row.DPSEM_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, null));
                    row.DIAPSEM_CODIGO = diaPlanSemanal.Codigo;
                    row.EndEdit();
                }

                transaccion.Commit();
                DB.FinalizarTransaccion();

            }
            catch (SqlException)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException();

            }

        }
        //METODO DE MODIFICACION
        //METODO QUE GUARDA LOS DATOS DEL PLAN MODIFICADOS   
        public static void GuardarPlanModificado(Entidades.PlanSemanal planSemanal, Entidades.DiasPlanSemanal diaPlanSemanal, Data.dsPlanSemanal dsPlanSemanal)
        {
            SqlTransaction transaccion = null;

            try
            {
                //Inserto la demanda
                transaccion = DB.IniciarTransaccion();

                string sql = string.Empty;

                //Modifico el dia del plan semanal
                sql = "UPDATE [PLANES_SEMANALES] SET psem_semana=@p0";
                object[] valorParametros = { planSemanal.Semana };
                DB.executeNonQuery(sql, valorParametros,transaccion);

                //Inserto los detalles nuevos
                sql = "INSERT INTO [DETALLE_PLANES_SEMANALES] ([diapsem_codigo], [coc_codigo], [dpsem_cantidadestimada], [dpsem_estado]) VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";
                foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row in (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow[])dsPlanSemanal.DETALLE_PLANES_SEMANALES.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    object[] valorParam = { diaPlanSemanal.Codigo, row.COC_CODIGO, row.DPSEM_CANTIDADESTIMADA, row.DPSEM_ESTADO };
                    row.BeginEdit();
                    row.DPSEM_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                    row.DIAPSEM_CODIGO = diaPlanSemanal.Codigo;
                    row.EndEdit();
                }

                //Guardo las modificaciones
                sql = "UPDATE [DETALLE_PLANES_SEMANALES] SET dpsem_cantidadestimada=@p0 ";
                foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row in (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow[])dsPlanSemanal.DETALLE_PLANES_SEMANALES.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    object[] valorPar = { row.DPSEM_CANTIDADESTIMADA };
                    DB.executeNonQuery(sql, valorPar, transaccion);
                }

                //Elimino las que fueron sacadas
                sql = "DELETE FROM [DETALLE_PLANES_SEMANALES] WHERE dpsem_codigo=@p0 ";
                foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row in (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow[])dsPlanSemanal.DETALLE_PLANES_SEMANALES.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    object[] valorPara = { Convert.ToInt32(row["DPMES_CODIGO", System.Data.DataRowVersion.Original]) };
                    DB.executeNonQuery(sql, valorPara, transaccion);
                }

                transaccion.Commit();
                DB.FinalizarTransaccion();

            }
            catch (SqlException)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException();

            }

        }


    }
}
