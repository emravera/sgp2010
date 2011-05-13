using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class PlanMateriasPrimasDAL
    {
        //BUSQUEDA
        public static void ObtenerTodos(int anio, Data.dsPlanMP ds)
        {
            string sql = @"SELECT pmpa_codigo, pmpa_anio, pmpa_mes, pmpa_fechacreacion
                        FROM PLANES_MATERIAS_PRIMAS_ANUALES WHERE pmpa_anio=@p0";

            object[] parametros = { anio };

            try
            {
                DB.FillDataSet(ds, "PLANES_MATERIAS_PRIMAS_ANUALES", sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerTodos(Data.dsPlanMP ds)
        {
            string sql = @"SELECT pmpa_codigo, pmpa_anio, pmpa_mes, pmpa_fechacreacion
                        FROM PLANES_MATERIAS_PRIMAS_ANUALES";
            try
            {
                DB.FillDataSet(ds, "PLANES_MATERIAS_PRIMAS_ANUALES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static int Insertar(Entidades.PlanMateriaPrima plan)
        {
            SqlTransaction transaccion = null;
            int codigo = 0;

            try
            {
                //Inserto la demanda
                transaccion = DB.IniciarTransaccion();
               
                //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
                string sql = "INSERT INTO [PLANES_MATERIAS_PRIMAS_ANUALES] ([pmpa_anio], [pmpa_fechacreacion], [pmpa_mes]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
                object[] valorParametros = { plan.Anio, plan.FechaCreacion.ToShortDateString(), plan.Mes };
                codigo = Convert.ToInt32(DB.executeScalar(sql, valorParametros, transaccion));
                
                //HAgo commit de la transaccion
                transaccion.Commit();
                DB.FinalizarTransaccion();
                
            }
            catch (SqlException)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException();
            }
            
            return codigo;

        }
        public static int InsertarDetalle(Entidades.DetallePlanMateriasPrimas detallePlan)
        {          
            int codigo = 0;

            try
            {
                //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
                string sql = "INSERT INTO [DETALLE_PLAN_MATERIAS_PRIMAS_ANUAL] ([pmpa_codigo], [mp_codigo], [dpmpa_cantidad]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
                object[] valorParametros = { detallePlan.Plan.Codigo, detallePlan.MateriaPrima.CodigoMateriaPrima, detallePlan.Cantidad };
                codigo = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException)
            {
                throw new Entidades.Excepciones.BaseDeDatosException();
            }
            
            return codigo;

        }

        public static void ObtenerDetalle(int idPlan, Data.dsPlanMP ds)
        {
            string sql = @"SELECT dp.dpmpa_codigo, dp.pmpa_codigo, dp.mp_codigo, dp.dpmpa_cantidad, mp.umed_codigo
                        FROM DETALLE_PLAN_MATERIAS_PRIMAS_ANUAL as dp, MATERIAS_PRIMAS AS mp
                        WHERE dp.mp_codigo=mp.mp_codigo and dp.pmpa_codigo=@p0";

            object[] parametros = { idPlan };

            try
            {
                DB.FillDataSet(ds, "DETALLE_PLAN_MATERIAS_PRIMAS_ANUAL", sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //ELIMINACION
        //Metodo que elimina de la base de datos
        public static void Eliminar(int codigo)
        {
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Elimino el detalle de la demanda
                string sql = "DELETE FROM DETALLE_PLAN_MATERIAS_PRIMAS_ANUAL WHERE pmpa_codigo = @p0";
                object[] valorParametros = { codigo };
                DB.executeNonQuery(sql, valorParametros, transaccion);

                //Elimino la demanda
                sql = "DELETE FROM PLANES_MATERIAS_PRIMAS_ANUALES WHERE pmpa_codigo= @p0";
                DB.executeNonQuery(sql, valorParametros, transaccion);


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
