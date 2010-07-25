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
            string sql = @"SELECT deman_codigo, deman_anio, deman_fechacreacion, deman_nombre, deman_paramcrecimiento
                        FROM DEMANDAS_ANUALES";
            try
            {
                DB.FillDataSet(ds, "DEMANDAS_ANUALES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo para insertar
        public static int Insertar(Entidades.DemandaAnual demanda)
        {

            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO [DEMANDAS_ANUALES] ([deman_anio], [deman_fechacreacion], [deman_paramcrecimiento], [deman_nombre]) VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";
            object[] valorParametros = { demanda.Anio, demanda.FechaCreacion.ToShortDateString() , demanda.ParametroCrecimiento, demanda.Nombre };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.DemandaAnual demanda)
        {
            string sql = @"UPDATE DEMANDAS_ANUALES SET deman_anio = @p0, deman_nombre = @p1
                         WHERE deman_codigo = @p2";
            object[] valorParametros = {  demanda.Anio, demanda.Nombre, demanda.Codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
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
            string sql = "DELETE FROM DEMANDAS_ANUALES WHERE deman_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //Metodo que elimina de la base de datos el detalle
        public static void EliminarDetalle(int codigo)
        {
            string sql = "DELETE FROM DETALLE_DEMANDAS_ANUALES WHERE deman_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }


    }
}
