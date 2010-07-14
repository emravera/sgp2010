using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class UnidadMedidaDAL
    {
       //BUSQUEDA
       //Metodo sobrecargado (3 Sobrecargas)
       //Busqueda por nombre
        public static void ObtenerUnidad(string nombre, Data.dsUnidadMedida ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT umed_codigo,tumed_codigo, umed_nombre, umed_abreviatura
                              FROM UNIDADES_MEDIDA
                              WHERE umed_nombre LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "UNIDADES_MEDIDA", sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
         }
        //Trae todos los elementos
        public static void ObtenerUnidad(Data.dsUnidadMedida ds)
        {
             string sql = "SELECT umed_codigo,tumed_codigo, umed_nombre, umed_abreviatura FROM UNIDADES_MEDIDA";
             try
             {
                 DB.FillDataSet(ds, "UNIDADES_MEDIDA", sql, null);
             }
             catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //Busqueda por tipo de unidad
        public static void ObtenerUnidad(int tipoUnidad, Data.dsUnidadMedida ds)
        {
                string sql = @"SELECT umed_codigo,tumed_codigo, umed_nombre, umed_abreviatura
                              FROM UNIDADES_MEDIDA
                              WHERE tumed_codigo=@p0";
                
                object[] valorParametros = { tipoUnidad };
                try
                {
                    DB.FillDataSet(ds, "UNIDADES_MEDIDA", sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        
        //ELIMINACION
        //Metodo que verifica que no este usado en otro lugar
        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(umed_codigo) FROM MATERIAS_PRIMAS WHERE umed_codigo = @p0";
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //Metodo que elimina de la base de datos
        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM UNIDADES_MEDIDA WHERE umed_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //INSERTAR
        //Metodo que valida que no se intente guardar algo que ya esta en la BD
        public static bool esUnidadMedida(Entidades.UnidadMedida unidadMedida)
        {
            string sql = "SELECT count(umed_codigo) FROM UNIDADES_MEDIDA WHERE umed_nombre = @p0";
            object[] valorParametros = { unidadMedida.Nombre };
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo que inserta en la base de datos
        public static int Insertar(Entidades.UnidadMedida unidadMedida)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO [UNIDADES_MEDIDA] ([tumed_codigo], [umed_nombre], [umed_abreviatura]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
            object[] valorParametros = {unidadMedida.Tipo.Codigo, unidadMedida.Nombre, unidadMedida.Abreviatura };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.UnidadMedida unidadMedida)
        {
            string sql = "UPDATE UNIDADES_MEDIDA SET umed_nombre = @p0, umed_abreviatura= @p1, tumed_codigo= @p2 WHERE umed_codigo = @p3";
            object[] valorParametros = { unidadMedida.Nombre, unidadMedida.Abreviatura, unidadMedida.Tipo.Codigo, unidadMedida.Codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
