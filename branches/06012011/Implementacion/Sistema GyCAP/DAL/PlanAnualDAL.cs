using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class PlanAnualDAL
    {
        //BUSQUEDA
        public static void ObtenerTodos(int anio, Data.dsPlanAnual ds)
        {
            string sql = @"SELECT pan_codigo, pan_anio, deman_codigo, pan_fechacreacion
                        FROM PLANES_ANUALES WHERE pan_anio=@p0";

            object[] parametros = { anio };

            try
            {
                DB.FillDataSet(ds, "PLANES_ANUALES", sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerTodos(Data.dsPlanAnual ds)
        {
            string sql = @"SELECT pan_codigo, pan_anio, deman_codigo, pan_fechacreacion
                        FROM PLANES_ANUALES";
            try
            {
                DB.FillDataSet(ds, "PLANES_ANUALES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerTodos(Data.dsPlanMateriasPrimas ds)
        {
            string sql = @"SELECT pan_codigo, pan_anio, deman_codigo, pan_fechacreacion
                        FROM PLANES_ANUALES";
            try
            {
                DB.FillDataSet(ds, "PLANES_ANUALES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerTodos(DataTable dtPlanesAnuales)
        {
            string sql = @"SELECT pan_codigo, pan_anio, deman_codigo, pan_fechacreacion
                        FROM PLANES_ANUALES";
            try
            {
                DB.FillDataTable(dtPlanesAnuales, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        
        public static int ObtenerCantidad(int anio, string mes)
        {
            int cantidad=0;

            string sql = @"SELECT det.dpan_cantidadmes
                        FROM PLANES_ANUALES as pa, DETALLE_PLAN_ANUAL as det
                        WHERE pa.pan_codigo=det.pan_codigo and pa.pan_anio=@p0 and det.dpan_mes LIKE @p1";
                        
            mes = "%" + mes + "%";
            object[] parametros = { anio, mes };

            try
            {
               cantidad=Convert.ToInt32(DB.executeScalar(sql, parametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            return cantidad;
        }

        //Metodo para insertar
        public static IList<Entidades.DetallePlanAnual> Insertar(Entidades.PlanAnual planAnual, IList<Entidades.DetallePlanAnual> detalle)
        {
            SqlTransaction transaccion = null;

            try
            {
                //Inserto la demanda
                transaccion = DB.IniciarTransaccion();

                //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
                string sql = "INSERT INTO [PLANES_ANUALES] ([pan_anio], [pan_fechacreacion], [deman_codigo]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
                object[] valorParametros = { planAnual.Anio, planAnual.FechaCreacion.ToString("yyyyMMdd"), planAnual.Demanda.Codigo };
                planAnual.Codigo = Convert.ToInt32(DB.executeScalar(sql, valorParametros, transaccion));

                //Inserto el Detalle de Pedido
                foreach (Entidades.DetallePlanAnual det in detalle)
                {
                    det.PlanAnual = planAnual;
                    det.Codigo = DAL.DetallePlanAnualDAL.Insertar(det, transaccion);
                }

                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);

            }
            finally
            {
                DB.FinalizarTransaccion();
            }
            return detalle;

        }
       
        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.PlanAnual planAnual, IList<Entidades.DetallePlanAnual> detalle)
        {
            SqlTransaction transaccion = null;

            try
            {
                //Abro la transaccion
                transaccion = DB.IniciarTransaccion();

                //Se modifica la demanda
                string sql = @"UPDATE PLANES_ANUALES SET pan_anio = @p0, deman_codigo = @p1
                         WHERE pan_codigo = @p2";
                object[] valorParametros = { planAnual.Anio , planAnual.Demanda.Codigo, planAnual.Codigo };
                DB.executeNonQuery(sql, valorParametros, null);

                //Se modifica el detalle
                foreach (Entidades.DetallePlanAnual det in detalle)
                {
                    DAL.DetallePlanAnualDAL.Actualizar(det);
                }

                //HAgo un Commit de la trnsaccion
                transaccion.Commit();
                DB.FinalizarTransaccion();
            }
            catch (SqlException)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException();
            }
           
        }

        //Metodo que valida que se pueda actualizar
        public static bool Validar(Entidades.PlanAnual planAnual)
        {
            string sql = @"SELECT count(pmes_codigo)
                           FROM PLANES_MENSUALES WHERE pan_codigo=@p0";
            object[] valorParametros = { planAnual.Codigo };
            try
            {
                int cantidad = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
                if (cantidad == 0) return true;
                else return false;
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //ELIMINACION
        //Metodo que verifica que no este usado en otro lugar
        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(pmes_codigo) FROM PLANES_MENSUALES WHERE pan_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo que elimina de la base de datos
        public static void Eliminar(int codigo)
        {
            SqlTransaction transaccion = null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Elimino el detalle del plan anual
                string sql = "DELETE FROM DETALLE_PLAN_ANUAL WHERE pan_codigo = @p0";
                object[] valorParametros = { codigo };
                DB.executeNonQuery(sql, valorParametros, null);

                //Elimino el plan anual
                sql = "DELETE FROM PLANES_ANUALES WHERE pan_codigo = @p0";
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
    }
}
