using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class SectorDAL
    {
        //BUSQUEDA
        //Metodo sobrecargado (3 Sobrecargas)
        //Busqueda por nombre o por abreviatura
        public static void ObtenerSector(string nombre, string abrev, Data.dsSectorTrabajo ds)
        {
           string sql, dato;

            if (nombre != String.Empty && abrev == string.Empty)
            {
                    sql = @"SELECT sec_codigo, sec_nombre, sec_descripcion, sec_abreviatura
                              FROM SECTORES
                              WHERE sec_nombre LIKE @p0";
                    dato= nombre;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                dato = "%" + dato + "%";
                object[] valorParametros = { dato };
                try
                {
                    DB.FillDataSet(ds, "SECTORES", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
           
            if (abrev != string.Empty && nombre == string.Empty)
            {
                    sql = @"SELECT sec_codigo, sec_nombre, sec_descripcion, sec_abreviatura
                              FROM SECTORES
                              WHERE sec_abreviatura LIKE @p0";
                    dato= abrev;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                dato = "%" + dato + "%";
                object[] valorParametros = { dato };
                try
                {
                    DB.FillDataSet(ds, "SECTORES", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
            if (abrev != string.Empty && nombre != string.Empty)
            {
                 sql = @"SELECT sec_codigo, sec_nombre, sec_descripcion, sec_abreviatura
                              FROM SECTORES
                              WHERE sec_abreviatura LIKE @p0 and sec_nombre LIKE @p1";
               
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";           
                abrev = "%" + abrev + "%";
                object[] valorParametros = { nombre, abrev };
                try
                {
                    DB.FillDataSet(ds, "SECTORES", sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
            else
            {
                sql = @"SELECT sec_codigo, sec_nombre, sec_descripcion, sec_abreviatura
                              FROM SECTORES";
                try
                {
                    DB.FillDataSet(ds, "SECTORES", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
         
        }

       public static void ObtenerSector(DataTable dtSectores)
        {
            string sql = @"SELECT sec_codigo, sec_nombre, sec_descripcion, sec_abreviatura FROM SECTORES";
            try
            {
                DB.FillDataTable(dtSectores, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }
                
        public static bool PuedeEliminarse(int codigo)
        {
            string sql1 = "SELECT count(sec_codigo) FROM EMPLEADOS WHERE sec_codigo = @p0";
            string sql2 = "SELECT count(sec_codigo) FROM CENTROS_TRABAJOS WHERE sec_codigo = @p0";
            string sql3 = "SELECT count(sec_codigo) FROM PROCESOS WHERE sec_codigo = @p0";
            string sql4 = "SELECT count(sec_codigo) FROM PROVEEDORES WHERE sec_codigo = @p0";
            
            object[] valorParametros = { codigo };
            try
            {
                int resultadoSql1 = Convert.ToInt32(DB.executeScalar(sql1, valorParametros, null));
                int resultadoSql2 = Convert.ToInt32(DB.executeScalar(sql2, valorParametros, null));
                int resultadoSql3 = Convert.ToInt32(DB.executeScalar(sql3, valorParametros, null));
                int resultadoSql4 = Convert.ToInt32(DB.executeScalar(sql4, valorParametros, null));
                
                if (resultadoSql1 + resultadoSql2 + resultadoSql3 + resultadoSql4 == 0) { return true; }
                else { return false; }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }        

        //Metodo que elimina de la base de datos
        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM SECTORES WHERE sec_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //INSERTAR
        //Metodo que valida que no se intente guardar algo que ya esta en la BD
        public static bool esSector(Entidades.SectorTrabajo sector)
        {
            string sql = "SELECT count(sec_codigo) FROM SECTORES WHERE sec_nombre = @p0";
            object[] valorParametros = { sector.Nombre };
            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo que inserta en la base de datos
        public static int Insertar(Entidades.SectorTrabajo sector)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO [SECTORES] ([sec_nombre], [sec_descripcion], [sec_abreviatura]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
            object[] valorParametros = { sector.Nombre, sector.Descripcion, sector.Abreviatura };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.SectorTrabajo sector)
        {
            string sql = @"UPDATE SECTORES SET sec_nombre = @p0, sec_descripcion = @p1, sec_abreviatura = @p2
                         WHERE sec_codigo = @p3";
            object[] valorParametros = { sector.Nombre, sector.Descripcion, sector.Abreviatura, sector.Codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
