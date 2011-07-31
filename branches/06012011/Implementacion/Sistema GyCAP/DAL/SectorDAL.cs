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
        public static void ObtenerSector(string nombre, string abrev, Data.dsHojaRuta ds)
        {
            string sql = @"SELECT sec_codigo, sec_nombre, sec_descripcion, sec_abreviatura
                              FROM SECTORES WHERE 1=1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[2];
            
            if (nombre != String.Empty)
            {
                sql += " AND sec_nombre LIKE @p" + cantidadParametros;
                
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }
           
            if (abrev != string.Empty)
            {
                sql += " AND sec_abreviatura LIKE @p" + cantidadParametros;
                
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                abrev = "%" + abrev + "%";
                valoresFiltros[cantidadParametros] = abrev;
                cantidadParametros++;
            }            

            if (cantidadParametros > 0)
            {
                //Buscamos con filtro, armemos el array de los valores de los parametros
                object[] valorParametros = new object[cantidadParametros];
                for (int i = 0; i < cantidadParametros; i++)
                {
                    valorParametros[i] = valoresFiltros[i];
                }
                try
                {
                    DB.FillDataTable(ds.SECTORES, sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
            else
            {
                //Buscamos sin filtro
                try
                {
                    DB.FillDataTable(ds.SECTORES, sql, null);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }         
       }

       public static void ObtenerSector(DataTable dtSectores)
        {
            string sql = @"SELECT sec_codigo, sec_nombre, sec_descripcion, sec_abreviatura FROM SECTORES";
            try
            {
                DB.FillDataTable(dtSectores, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }

        }
                
        public static bool PuedeEliminarse(int codigo)
        {
            string sql1 = "SELECT count(sec_codigo) FROM EMPLEADOS WHERE sec_codigo = @p0";
            string sql2 = "SELECT count(sec_codigo) FROM CENTROS_TRABAJOS WHERE sec_codigo = @p0";
            string sql3 = "SELECT count(sec_codigo) FROM PROVEEDORES WHERE sec_codigo = @p0";
            
            object[] valorParametros = { codigo };
            try
            {
                int resultadoSql1 = Convert.ToInt32(DB.executeScalar(sql1, valorParametros, null));
                int resultadoSql2 = Convert.ToInt32(DB.executeScalar(sql2, valorParametros, null));                
                int resultadoSql3 = Convert.ToInt32(DB.executeScalar(sql3, valorParametros, null));
                
                if (resultadoSql1 + resultadoSql2 + resultadoSql3 == 0) { return true; }
                else { return false; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        //INSERTAR
        //Metodo que valida que no se intente guardar algo que ya esta en la BD
        public static bool esSector(Entidades.SectorTrabajo sector)
        {
            string sql = "SELECT count(sec_codigo) FROM SECTORES WHERE sec_nombre = @p0 AND sec_codigo <> @p1";
            object[] valorParametros = { sector.Nombre, sector.Codigo };
            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0) { return false; }
                else { return true; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
