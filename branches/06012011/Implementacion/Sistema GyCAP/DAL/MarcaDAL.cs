using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class MarcaDAL
    {
        //BUSQUEDA
        //Busqueda por nombre
        public static void ObtenerMarca(string nombre, int idCliente, DataTable dt)
        {
            string sql = @"SELECT mca_codigo, cli_codigo, mca_nombre
                              FROM MARCAS";

            object[] valorParametros = { null };
            object[] valoresPram = { null, null };

            //Si busca solo por el nombre
            if (nombre != String.Empty && idCliente == 0)
            {
                //Agrego la busqueda por nombre
                sql = sql + " WHERE mca_nombre LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valorParametros.SetValue(nombre, 0);
            }
            else if (nombre == string.Empty && idCliente != 0)
            {
                //Agrego la busqueda por marca
                sql = sql + " WHERE cli_codigo=@p0";
                valorParametros.SetValue(idCliente, 0);
            }
            else if (nombre != string.Empty && idCliente != 0)
            {
                //Agrego la busqueda por marca
                sql = sql + " WHERE cli_codigo=@p0 and mca_nombre LIKE @p1";
                nombre = "%" + nombre + "%";
                //Le doy valores a la estructura
                valoresPram.SetValue(idCliente, 0);
                valoresPram.SetValue(nombre, 1);
            }

            //Ejecuto el comando a la BD
            try
            {
                if (valorParametros.GetValue(0) == null && valoresPram.GetValue(0) == null)
                {
                    //Se ejcuta normalmente y por defecto trae todos los elementos de la DB
                    DB.FillDataTable(dt, sql, null);
                }
                else
                {
                    if (valoresPram.GetValue(0) == null)
                    {
                        DB.FillDataTable(dt, sql, valorParametros);
                    }
                    else
                    {
                        DB.FillDataTable(dt,sql, valoresPram);
                    }
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerMarca(Data.dsDesignacion ds)
        {
            string sql = @"SELECT mca_codigo, cli_codigo, mca_nombre
                              FROM MARCAS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "MARCAS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerMarca(DataTable dtMarca)
        {
            string sql = "SELECT mca_codigo, cli_codigo, mca_nombre FROM MARCAS";
            try
            {
                DB.FillDataTable(dtMarca, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //ELIMINACION
        //Metodo que verifica que no este usado en otro lugar
        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(mca_codigo) FROM DESIGNACIONES WHERE mca_codigo = @p0";
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
            string sql = "DELETE FROM MARCAS WHERE mca_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //INSERTAR
        //Metodo que valida que no se intente guardar algo que ya esta en la BD
        public static bool esMarca(Entidades.Marca marca)
        {
            string sql = "SELECT count(mca_codigo) FROM MARCAS WHERE mca_nombre = @p0";
            object[] valorParametros = { marca.Nombre };
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
        public static int Insertar(Entidades.Marca marca)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO [MARCAS] ([cli_codigo], [mca_nombre]) VALUES (@p0, @p1) SELECT @@Identity";
            object[] valorParametros = { marca.Cliente.Codigo, marca.Nombre };
            if (marca.Cliente.Codigo == 0)
            {
                valorParametros.SetValue(System.Data.SqlTypes.SqlInt32.Null , 0);
            }
            
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.Marca marca)
        {
            string sql = "UPDATE MARCAS SET mca_nombre = @p0, cli_codigo = @p1 WHERE mca_codigo = @p2";
            object[] valorParametros = { marca.Nombre, marca.Cliente.Codigo, marca.Codigo };
            if (marca.Cliente.Codigo == 0)
            {
                valorParametros.SetValue(System.Data.SqlTypes.SqlInt32.Null, 1);
            }
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
