
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
        //Metodo que trae las cantidades planificadas de una cocina
        public static int? obtenerCocinasPlanificadas(int codigoCocina, int codigoPA, int codigoPM)
        {
            int? cantidad = 0;

            string sql = @"SELECT COALESCE(sum(det.dpsem_cantidadestimada),0)
                           FROM DETALLE_PLANES_SEMANALES as det, DIAS_PLAN_SEMANAL as dia, PLANES_SEMANALES as ps, PLANES_MENSUALES as pm, PLANES_ANUALES as pa
                           WHERE det.coc_codigo=@p0 and det.diapsem_codigo=dia.diapsem_codigo and dia.psem_codigo=ps.psem_codigo and ps.pmes_codigo=pm.pmes_codigo
                           and pm.pan_codigo=pa.pan_codigo and pm.pan_codigo=@p1 and ps.pmes_codigo=@p2 and det.dped_codigo IS NULL";

            object[] valorParametros = { codigoCocina, codigoPA, codigoPM };
            try
            {
                cantidad = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            return cantidad;
        }
        //Metodo que trae las cantidades planificadas de una cocina y un pedido
        public static int? obtenerCocinasPlanificadas(int codigoCocina, int codigoPA, int codigoPM, int codigoPedido)
        {
            int? cantidad = 0;

            string sql = @"SELECT COALESCE(sum(det.dpsem_cantidadestimada),0)
                           FROM DETALLE_PLANES_SEMANALES as det, DIAS_PLAN_SEMANAL as dia, PLANES_SEMANALES as ps, PLANES_MENSUALES as pm, PLANES_ANUALES as pa
                           WHERE det.coc_codigo=@p0 and det.diapsem_codigo=dia.diapsem_codigo and dia.psem_codigo=ps.psem_codigo and ps.pmes_codigo=pm.pmes_codigo
                           and pm.pan_codigo=pa.pan_codigo and pm.pan_codigo=@p1 and ps.pmes_codigo=@p2 and det.dped_codigo=@p3";

            object[] valorParametros = { codigoCocina, codigoPA, codigoPM, codigoPedido };
            try
            {
                cantidad = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            return cantidad;
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
        //Metodo que valida que no ecista el dia que se quiere insertar en otro plan semanal
        public static bool ValidarDia(DateTime dia)
        {
            string sql = @"SELECT count(diapsem_codigo)
                           FROM DIAS_PLAN_SEMANAL
                           WHERE diapsem_fecha LIKE @p0";

            string diaTexto = "'" + dia.ToString() + "'";
            object[] valorParametros = { diaTexto };
            try
            {
                int cantidad = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
                if (cantidad == 0) return true;
                else return false;
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //METODO DE INSERCION
        public static int GuardarPlanSemanal(Entidades.PlanSemanal planSemanal, Entidades.DiasPlanSemanal diaPlanSemanal, Data.dsPlanSemanal dsPlanSemanal, bool esPrimero)
        {

            SqlTransaction transaccion = null;

            try
            {
                //Inicio la transaccion 
                transaccion = DB.IniciarTransaccion();
                string sql = string.Empty;
                int codigo = 0;

                if (esPrimero == true)
                {
                    //Inserto el PlanSemanal
                    //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
                    sql = "INSERT INTO [PLANES_SEMANALES] ([pmes_codigo], [psem_semana], [psem_fechacreacion]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
                    object[] valorParametros = { planSemanal.PlanMensual.Codigo, planSemanal.Semana, planSemanal.FechaCreacion };
                    planSemanal.Codigo = Convert.ToInt32(DB.executeScalar(sql, valorParametros, transaccion));
                    codigo = planSemanal.Codigo;
                }
                    //Inserto el dia del Plan Semanal
                    sql = "INSERT INTO [DIAS_PLAN_SEMANAL] ([diapsem_dia], [diapsem_fecha], [psem_codigo]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
                    object[] valorPara = { diaPlanSemanal.Dia, diaPlanSemanal.Fecha, diaPlanSemanal.PlanSemanal.Codigo };
                    diaPlanSemanal.Codigo = Convert.ToInt32(DB.executeScalar(sql, valorPara, transaccion));

                    //Inserto el Detalle                    
                    foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row in (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow[])dsPlanSemanal.DETALLE_PLANES_SEMANALES.Select(null, null, System.Data.DataViewRowState.Added))
                    {
                        if (row.DPED_CODIGO != 0)
                        {
                            sql = "INSERT INTO [DETALLE_PLANES_SEMANALES] ([diapsem_codigo], [coc_codigo], [dpsem_cantidadestimada], [dpsem_estado], [dped_codigo]) VALUES (@p0, @p1, @p2, @p3, @p4) SELECT @@Identity";
                            object[] valorParam = { diaPlanSemanal.Codigo, row.COC_CODIGO, row.DPSEM_CANTIDADESTIMADA, row.DPSEM_ESTADO, row.DPED_CODIGO };
                            row.BeginEdit();
                            row.DPSEM_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                            row.DIAPSEM_CODIGO = diaPlanSemanal.Codigo;
                            row.EndEdit();
                        }
                        else
                        {
                            sql = "INSERT INTO [DETALLE_PLANES_SEMANALES] ([diapsem_codigo], [coc_codigo], [dpsem_cantidadestimada], [dpsem_estado]) VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";
                            object[] valorParam = { diaPlanSemanal.Codigo, row.COC_CODIGO, row.DPSEM_CANTIDADESTIMADA, row.DPSEM_ESTADO };
                            row.BeginEdit();
                            row.DPSEM_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                            row.DIAPSEM_CODIGO = diaPlanSemanal.Codigo;
                            row.EndEdit();

                        }
                    }

                    transaccion.Commit();
                    DB.FinalizarTransaccion();

                 
                return codigo;
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
                sql = "UPDATE [DIA_PLAN_SEMANAL] SET diapsem_dia=@p0, diapsem_fecha=@p1 WHERE psem_semana=@p2";
                object[] valorParametros = { diaPlanSemanal.Dia, diaPlanSemanal.Fecha, planSemanal.Semana };
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
                sql = "UPDATE [DETALLE_PLANES_SEMANALES] SET dpsem_cantidadestimada=@p0 WHERE dpsem_codigo=@p1";
                foreach (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow row in (Data.dsPlanSemanal.DETALLE_PLANES_SEMANALESRow[])dsPlanSemanal.DETALLE_PLANES_SEMANALES.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    object[] valorPar = { row.DPSEM_CANTIDADESTIMADA, row.DPSEM_CODIGO};
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
        //METODO DE ELIMINACION DE LA BASE DE DATOS
        public static void EliminarPlan(int codigoPlan, Data.dsPlanSemanal dsPlanSemanal)
        {
            SqlTransaction transaccion = null;
            int codigoDia = 0;
            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();


                foreach (Data.dsPlanSemanal.DIAS_PLAN_SEMANALRow row in dsPlanSemanal.DIAS_PLAN_SEMANAL.Rows)
                {
                    codigoDia =Convert.ToInt32(row.DIAPSEM_CODIGO);

                    try
                    {
                        //Elimino el detalle del plan mensual
                        string sql = "DELETE FROM DETALLE_PLANES_SEMANALES WHERE diapsem_codigo = @p0";
                        object[] valorParam = { codigoDia };
                        DB.executeNonQuery(sql, valorParam, null);
                    }
                    catch (Exception)
                    {
                        throw new Entidades.Excepciones.ElementoEnTransaccionException();
                    }

                }

                foreach (Data.dsPlanSemanal.DIAS_PLAN_SEMANALRow row in dsPlanSemanal.DIAS_PLAN_SEMANAL.Rows)
                {
                    codigoDia = Convert.ToInt32(row.DIAPSEM_CODIGO);

                    //Elimino los dias
                    string sql = "DELETE FROM DIAS_PLAN_SEMANAL WHERE diapsem_codigo = @p0";
                    object[] valorParametros = { codigoDia };
                    DB.executeNonQuery(sql, valorParametros, null);
                }

                //Elimino el plan semanal
                string sqlPS = "DELETE FROM PLANES_SEMANALES WHERE psem_codigo = @p0";
                object[] valorPara = { codigoPlan };
                DB.executeNonQuery(sqlPS, valorPara, null);


                transaccion.Commit();
                DB.FinalizarTransaccion();
            }
            catch (SqlException)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException();
            }
            catch (Entidades.Excepciones.ElementoEnTransaccionException)
            {
                transaccion.Rollback();                
            }

        }

        //****************************************** Control de la Producción ****************************************************
        //METODO QUE OBTIENE EL ESTADO DE LA ORDEN DE PRODUCCION DEL DETALLE
        public static int obtenerEstadoOrden(int codigoDetallePS)
        {
            int estado = 0;

            string sql = @"SELECT op.eord_codigo
                           FROM ORDENES_PRODUCCION as op, DETALLE_PLANES_SEMANALES as dps
                           WHERE op.dpsem_codigo=dps.dpsem_codigo and op.dpsem_codigo=@p0";

            object[] valorParametros = { codigoDetallePS};
            try
            {
                estado = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            return estado;
        }
        //METODO QUE VERIFICA SI EXISTE UNA ORDEN DE PRODUCCIÓN PARA ESE DETALLE
        public static int VerificarOrdenProduccion(int codigoDetallePS)
        {
            int cantidad = 0;

            string sql = @"SELECT count(op.ordp_codigo)
                           FROM ORDENES_PRODUCCION as op
                           WHERE op.dpsem_codigo=@p0";

            object[] valorParametros = { codigoDetallePS };
            try
            {
                cantidad = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            return cantidad;
        }
        //Metodo que obtiene los planes Mesnuales creados para un plan anual
        public static void obtenerPlanesMensuales(int codigoPlanAnual, Data.dsPlanSemanal dsPlanSemanal)
        {
            string sql = @"SELECT pmes_codigo, pan_codigo, pmes_mes, pmes_fechacreacion
                        FROM PLANES_MENSUALES where pan_codigo=@p0";

            object[] valorParametros = { codigoPlanAnual };
            try
            {
                DB.FillDataSet(dsPlanSemanal, "PLANES_MENSUALES", sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
