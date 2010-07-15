using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DesignacionDAL
    {
        //BUSQUEDA
        //Metodo sobrecargado (3 Sobrecargas)
        //Busqueda por nombre
        public static void ObtenerDesignacion(string nombre, Data.dsDesignacion ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT desig_codigo, mca_codigo, desig_nombre, desig_descripcion
                              FROM DESIGNACIONES
                              WHERE desig_nombre LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "DESIGNACIONES", sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
        }
        //Trae todos los elementos
        public static void ObtenerDesignacion(Data.dsDesignacion ds)
        {
            string sql = @"SELECT desig_codigo, mca_codigo, desig_nombre, desig_descripcion 
                           FROM DESIGNACIONES";
            try
            {
                DB.FillDataSet(ds, "DESIGNACIONES", sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //Busqueda por marca
        public static void ObtenerDesignacion(int idMarca, Data.dsDesignacion ds)
        {
            string sql = @"SELECT desig_codigo, mca_codigo, desig_nombre, desig_descripcion
                              FROM DESIGNACIONES
                              WHERE mca_codigo=@p0";

            object[] valorParametros = { idMarca };
            try
            {
                DB.FillDataSet(ds, "DESIGNACIONES", sql, valorParametros);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //ELIMINACION
        //Metodo que verifica que no este usado en otro lugar
        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(desig_codigo) FROM COCINAS WHERE desig_codigo = @p0";
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
            string sql = "DELETE FROM DESIGNACIONES WHERE desig_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //INSERTAR
        //Metodo que valida que no se intente guardar algo que ya esta en la BD
        public static bool esDesignacion(Entidades.Designacion desig)
        {
            string sql = "SELECT count(desig_codigo) FROM DESIGNACIONES WHERE desig_nombre = @p0";
            object[] valorParametros = { desig.Nombre };
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
        public static int Insertar(Entidades.Designacion desig)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO [DESIGNACIONES] ([mca_codigo], [desig_nombre], [desig_descripcion]) VALUES (@p0, @p1, @p2) SELECT @@Identity";
            object[] valorParametros = { desig.Marca.Codigo, desig.Nombre, desig.Descripcion };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.Designacion desig)
        {
            string sql =@"UPDATE DESIGNACIONES SET desig_nombre = @p0, mca_codigo = @p1, desig_descripcion = @p2
                         WHERE desig_codigo = @p3";
            object[] valorParametros = { desig.Nombre, desig.Marca.Codigo, desig.Descripcion, desig.Codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
