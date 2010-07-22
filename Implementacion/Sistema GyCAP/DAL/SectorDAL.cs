using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

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

        public static void ObtenerSector(Data.dsEmpleado ds)
        {
            string sql = @"SELECT sec_codigo, sec_nombre, sec_descripcion, sec_abreviatura
                              FROM SECTORES";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "SECTORES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
         
        }

                
        //ELIMINACION
        //Metodos que verifica que no este usado en otro lugar

        public static bool PuedeEliminarseValidacion1(int codigo)
        {
            string sql = "SELECT count(sec_codigo) FROM EMPLEADOS WHERE sec_codigo = @p0";
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

        public static bool PuedeEliminarseValidacion2(int codigo)
        {
            string sql = "SELECT count(sec_codigo) FROM PROCESOS WHERE sec_codigo = @p0";
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
        public static bool PuedeEliminarseValidacion3(int codigo)
        {
            string sql = "SELECT count(sec_codigo) FROM PROVEEDORES WHERE sec_codigo = @p0";
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
        public static bool esSector(Entidades.Sector sector)
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
        public static int Insertar(Entidades.Sector sector)
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
        public static void Actualizar(Entidades.Sector sector)
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
