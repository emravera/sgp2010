using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class MarcaDAL
    {
        //BUSQUEDA
        //Metodo sobrecargado (3 Sobrecargas)
        //Busqueda por nombre
        public static void ObtenerMarca(string nombre, Data.dsMarca ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT mca_codigo, cli_codigo, mca_nombre
                              FROM MARCAS
                              WHERE mca_nombre LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "MARCAS", sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
        }
        //Trae todos los elementos
        public static void ObtenerMarca(Data.dsMarca ds)
        {
            string sql = "SELECT mca_codigo, cli_codigo, mca_nombre FROM MARCAS";
            try
            {
                DB.FillDataSet(ds, "MARCAS", sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //Busqueda por cliente
        public static void ObtenerMarca(int idCliente, Data.dsMarca ds)
        {
            string sql = @"SELECT mca_codigo, cli_codigo, mca_nombre
                              FROM MARCAS
                              WHERE cli_codigo=@p0";

            object[] valorParametros = { idCliente };
            try
            {
                DB.FillDataSet(ds, "MARCAS", sql, valorParametros);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //ELIMINACION
        //Metodo que verifica que no este usado en otro lugar
        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(mca_codigo) FROM DESIGNACION WHERE mca_codigo = @p0";
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
            string sql = "DELETE FROM MARCAS WHERE mca_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo que inserta en la base de datos
        public static int Insertar(Entidades.Marca marca)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO [MARCAS] ([cli_codigo], [mca_nombre]) VALUES (@p0, @p1) SELECT @@Identity";
            object[] valorParametros = { marca.Cliente.Codigo, marca.Nombre };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.Marca marca)
        {
            string sql = "UPDATE UNIDADES_MEDIDA SET mca_nombre = @p0, cli_codigo = @p1 WHERE mca_codigo = @p2";
            object[] valorParametros = { marca.Nombre, marca.Cliente.Codigo, marca.Codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
