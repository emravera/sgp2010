using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class PlanMensualDAL
    {
        //METODO DE BUSQUEDA
        //BUSQUEDA
        public static void ObtenerTodos(int anio, string mes, Data.dsPlanMensual ds)
        {
             //Consulta para mes solo
            string sql = @"SELECT pm.pmes_codigo, pm.pan_codigo, pa.pan_anio, pm.pmes_mes, pm.pmes_fechacreacion
                        FROM PLANES_MENSUALES pm, PLANES_ANUALES as pa 
                        WHERE pm.pan_codigo=pa.pan_codigo";

            object[] valorParametros = { null };
            object[] valoresPram = { null, null };

            //Si busca solo por el nombre
            if (mes != String.Empty && anio == 0)
            {
                //Agrego la busqueda por nombre
                sql = sql + " and pm.pmes_mes LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                mes = "%" + mes + "%";
                valorParametros.SetValue(mes, 0);
            }
            else if (mes == string.Empty && anio != 0)
            {
                //Agrego la busqueda por marca
                sql = sql + " and pa.pan_anio=@p0";
                valorParametros.SetValue(anio, 0);
            }
            else if (mes != string.Empty && anio != 0)
            {
                //Agrego la busqueda por marca
                sql = sql + " and pa.pan_anio=@p0 and pm.pmes_mes LIKE @p1";
                mes = "%" + mes + "%";
                //Le doy valores a la estructura
                valoresPram.SetValue(anio, 0);
                valoresPram.SetValue(mes, 1);
            }

            //Ejecuto el comando a la BD
            try
            {
                if (valorParametros.GetValue(0) == null && valoresPram.GetValue(0) == null)
                {
                    //Se ejcuta normalmente y por defecto trae todos los elementos de la DB
                    DB.FillDataSet(ds, "PLANES_MENSUALES", sql, null);
                }
                else
                {
                    if (valoresPram.GetValue(0) == null)
                    {
                        DB.FillDataSet(ds, "PLANES_MENSUALES", sql, valorParametros);
                    }
                    else
                    {
                        DB.FillDataSet(ds, "PLANES_MENSUALES", sql, valoresPram);
                    }
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //Metodo que obtiene todos los planes mensuales
        public static void ObtenerTodos(DataTable dtPlanesMensuales)
        {
            string sql = @"SELECT pmes_codigo, pan_codigo, pmes_mes, pmes_fechacreacion
                        FROM PLANES_MENSUALES";
            try
            {
                DB.FillDataTable(dtPlanesMensuales, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //Metodo que obtiene los planes mensuales de un plan anual determinado
        public static void ObtenerPMAnio(DataTable dtPlanesMensuales, int codigoPlanAnual)
        {
            string sql = @"SELECT pmes_codigo, pan_codigo, pmes_mes, pmes_fechacreacion
                        FROM PLANES_MENSUALES where pan_codigo=@p0";

            object[] valorParametros = { codigoPlanAnual };
            try
            {
                DB.FillDataTable(dtPlanesMensuales, sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo que valida que no exista un plan mensual para ese año y mes ya creado
        public static bool Validar(int anio, string mes)
        {
            string sql = @"SELECT count(pm.pmes_codigo)
                           FROM PLANES_MENSUALES as pm, PLANES_ANUALES as pa
                           WHERE pa.pan_anio=@p0 and pm.pmes_mes LIKE @p1";
            object[] valorParametros = { anio, mes};
            try
            {
                int cantidad = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
                if (cantidad == 0) return true;
                else return false;
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo que valida que no existan planes semanales de ese plan mensual para que se pueda modificar
        public static bool ValidarActualizar(int codigoPlan)
        {
            string sql = @"SELECT count(pm.pmes_codigo)
                           FROM PLANES_MENSUALES as pm, PLANES_SEMANALES as ps 
                           WHERE pm.pmes_codigo=ps.pmes_codigo and pm.pmes_codigo=@p0";
            object[] valorParametros = { codigoPlan };
            try
            {
                int cantidad = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
                if (cantidad == 0) return true;
                else return false;
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //METODO QUE GUARDA LOS DATOS   
        public static void GuardarPlan(Entidades.PlanMensual plan, Data.dsPlanMensual planMensual)
        {

            SqlTransaction transaccion = null;

            try
            {
                //Inserto la demanda
                transaccion = DB.IniciarTransaccion();

                //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
                string sql = "INSERT INTO [PLANES_MENSUALES] ([pan_codigo], [pmes_mes], [pmes_fechacreacion]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
                object[] valorParametros = { plan.PlanAnual.Codigo, plan.Mes, plan.FechaCreacion };
                plan.Codigo = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));

                //Inserto el Detalle
                sql = "INSERT INTO [DETALLE_PLANES_MENSUALES] ([pmes_codigo], [coc_codigo], [dpmes_cantidadestimada]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
                foreach (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row in (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow[]) planMensual.DETALLE_PLANES_MENSUALES.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    object[] valorParam = { plan.Codigo,row.COC_CODIGO, row.DPMES_CANTIDADESTIMADA };
                    row.BeginEdit();
                    row.DPMES_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                    row.PMES_CODIGO = plan.Codigo;
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
        //METODO QUE GUARDA LOS DATOS DEL PLAN MODIFICADOS   
        public static void GuardarPlanModificado(Entidades.PlanMensual plan, Data.dsPlanMensual planMensual)
        {
            SqlTransaction transaccion = null;

            try
            {
                //Inserto la demanda
                transaccion = DB.IniciarTransaccion();

                string sql = string.Empty;

                //Inserto los detalles nuevos
                sql = "INSERT INTO [DETALLE_PLANES_MENSUALES] ([pmes_codigo], [coc_codigo], [dpmes_cantidadestimada]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
                foreach (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row in (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow[])planMensual.DETALLE_PLANES_MENSUALES.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    object[] valorParam = { plan.Codigo, row.COC_CODIGO, row.DPMES_CANTIDADESTIMADA };
                    row.BeginEdit();
                    row.DPMES_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                    row.PMES_CODIGO = plan.Codigo;
                    row.EndEdit();
                }
                //Guardo las modificaciones
                sql = "UPDATE [DETALLE_PLANES_MENSUALES] SET dpmes_cantidadestimada=@p0 ";
                foreach (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row in (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow[])planMensual.DETALLE_PLANES_MENSUALES.Select(null, null, System.Data.DataViewRowState.ModifiedCurrent))
                {
                    object[] valorPar = { row.DPMES_CANTIDADESTIMADA };
                    DB.executeNonQuery(sql, valorPar, transaccion);
                }

                //Elimino las que fueron sacadas
                sql = "DELETE FROM [DETALLE_PLANES_MENSUALES] WHERE dpmes_codigo=@p0 ";
                foreach (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow row in (Data.dsPlanMensual.DETALLE_PLANES_MENSUALESRow[])planMensual.DETALLE_PLANES_MENSUALES.Select(null, null, System.Data.DataViewRowState.Deleted))
                {
                    object[] valorPara = {Convert.ToInt32(row["DPMES_CODIGO", System.Data.DataRowVersion.Original])};
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
        //METODO PARA ELIMINAR DATOS DE LA BASE DE DATOS
        //Metodo que elimina de la base de datos
        public static void EliminarPlan(int codigoPlan)
        {
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Elimino el detalle del plan mensual
                string sql = "DELETE FROM DETALLE_PLANES_MENSUALES WHERE pmes_codigo = @p0";
                object[] valorParametros = { codigoPlan };
                DB.executeNonQuery(sql, valorParametros, null);

                //Elimino la demanda
                sql = "DELETE FROM PLANES_MENSUALES WHERE pmes_codigo = @p0";
                DB.executeNonQuery(sql, valorParametros, null);


                transaccion.Commit();
                DB.FinalizarTransaccion();
            }
            catch (SqlException)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException();
            }

        }



        public static int ObtenerCantidad(int anio, string mes)
        {
            int cantidad=0;

            string sql = @"SELECT sum(det.dpmes_cantidadestimada)
                        FROM PLANES_MENSUALES as pm, PLANES_ANUALES as pa, DETALLE_PLANES_MENSUALES as det
                        WHERE pa.pan_codigo=pm.pan_codigo and pm.pmes_codigo=det.pmes_codigo and
                              pa.pan_anio=@p0 and pm.pmes_mes LIKE @p1";
            
            
            mes = "%" + mes + "%";
            object[] parametros = { anio, mes };

            try
            {
               cantidad=Convert.ToInt32(DB.executeScalar(sql, parametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            return cantidad;
        }

    }
}
