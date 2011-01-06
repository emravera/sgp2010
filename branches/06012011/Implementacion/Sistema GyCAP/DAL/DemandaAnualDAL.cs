using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DemandaAnualDAL
    {
        public static void ObtenerTodos(int anio, Data.dsEstimarDemanda ds)
        {
            string sql = @"SELECT deman_codigo, deman_anio, deman_fechacreacion, deman_nombre, deman_paramcrecimiento
                        FROM DEMANDAS_ANUALES WHERE deman_anio=@p0";
            
            object[] parametros = { anio };
            
            try
            {
                DB.FillDataSet(ds, "DEMANDAS_ANUALES", sql, parametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerTodos(Data.dsEstimarDemanda ds)
        {
            string sql = @"SELECT deman_codigo, deman_anio, deman_nombre, deman_fechacreacion, deman_paramcrecimiento
                        FROM DEMANDAS_ANUALES";
            try
            {
                DB.FillDataSet(ds, "DEMANDAS_ANUALES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo para obtener todos desde el formulario de Plan Anual
        public static void ObtenerTodos(Data.dsPlanAnual ds)
        {
            string sql = @"SELECT deman_codigo, deman_anio, deman_fechacreacion, deman_nombre, deman_paramcrecimiento
                        FROM DEMANDAS_ANUALES";
            try
            {
                DB.FillDataSet(ds, "DEMANDAS_ANUALES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //Metodo para obtener un año a partir del id
        public static int ObtenerAño(int idDemanda)
        {
            string sql = @"SELECT deman_anio
                        FROM DEMANDAS_ANUALES WHERE deman_codigo=@p0";

            object[] valorParametros = { idDemanda };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        
        //Metodo para insertar
        public static IList<Entidades.DetalleDemandaAnual> Insertar(Entidades.DemandaAnual demanda, IList<Entidades.DetalleDemandaAnual> detalle)
        {
            SqlTransaction transaccion = null;

            try
            {
                //Inserto la demanda
                transaccion = DB.IniciarTransaccion();

                //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
                string sql = "INSERT INTO [DEMANDAS_ANUALES] ([deman_anio], [deman_fechacreacion], [deman_paramcrecimiento], [deman_nombre]) VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";
                object[] valorParametros = { demanda.Anio, demanda.FechaCreacion.ToString("yyyyMMdd"), demanda.ParametroCrecimiento, demanda.Nombre };
                demanda.Codigo = Convert.ToInt32(DB.executeScalar(sql, valorParametros, transaccion));

                //Inserto el Detalle de Pedido
                foreach (Entidades.DetalleDemandaAnual det in detalle)
                {
                    det.Demanda = demanda;
                    det.Codigo= DAL.DetalleDemandaAnualDAL.Insertar(det, transaccion);
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
        public static void Actualizar(Entidades.DemandaAnual demanda, IList<Entidades.DetalleDemandaAnual> detalle)
        {
            SqlTransaction transaccion = null;

            try
            {
                //Abro la transaccion
                transaccion = DB.IniciarTransaccion();

                //Se modifica la demanda
                string sql = @"UPDATE DEMANDAS_ANUALES SET deman_anio = @p0, deman_nombre = @p1
                         WHERE deman_codigo = @p2";
                object[] valorParametros = { demanda.Anio, demanda.Nombre, demanda.Codigo };
                DB.executeNonQuery(sql, valorParametros, null);

                //Se modifica el detalle
                foreach (Entidades.DetalleDemandaAnual det in detalle)
                {
                    DAL.DetalleDemandaAnualDAL.Actualizar(det);
                }

            }
            catch (SqlException)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException();
            }
            finally
            {
                transaccion.Commit();
                DB.FinalizarTransaccion();
            }
        }
       
        //Metodo que valida que se pueda actualizar
        public static bool Validar(Entidades.DemandaAnual demanda)
        {
            string sql = @"SELECT count(pan_codigo)
                           FROM PLANES_ANUALES WHERE deman_codigo=@p0";
            object[] valorParametros = { demanda.Codigo };
            try
            {
                int cantidad=Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
                if (cantidad == 0) return true;
                else return false;
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        
        //ELIMINACION
        //Metodo que verifica que no este usado en otro lugar
        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(pan_codigo) FROM PLANES_ANUALES WHERE deman_codigo = @p0";
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
            SqlTransaction transaccion=null;

            try
            {
                //Iniciamos la transaccion
                transaccion = DB.IniciarTransaccion();

                //Elimino el detalle de la demanda
                string sql = "DELETE FROM DETALLE_DEMANDAS_ANUALES WHERE deman_codigo = @p0";
                object[] valorParametros = { codigo };
                DB.executeNonQuery(sql, valorParametros, null);

                //Elimino la demanda
                sql = "DELETE FROM DEMANDAS_ANUALES WHERE deman_codigo = @p0";                
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
