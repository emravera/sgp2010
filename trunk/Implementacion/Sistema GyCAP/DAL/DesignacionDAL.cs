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
        public static void ObtenerDesignacion(string nombre, int idMarca, Data.dsDesignacion ds)
        {
            string sql = @"SELECT desig_codigo, mca_codigo, desig_nombre, desig_descripcion
                              FROM DESIGNACIONES";

            object [] valorParametros = { null };
            object[] valoresPram = { null, null };               

            //Si busca solo por el nombre
            if (nombre != String.Empty && idMarca == 0)
            {
                //Agrego la busqueda por nombre
                sql = sql + " WHERE desig_nombre LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valorParametros.SetValue(nombre, 0);
            }
            else if (nombre == string.Empty && idMarca != 0)
            {
                //Agrego la busqueda por marca
                sql = sql + " WHERE mca_codigo=@p0";
                valorParametros.SetValue(idMarca, 0);
            }
            else if (nombre != string.Empty && idMarca != 0)
            {
                //Agrego la busqueda por marca
                sql = sql + " WHERE mca_codigo=@p0 and desig_nombre LIKE @p1";
                nombre = "%" + nombre + "%";
                //Le doy valores a la estructura
                valoresPram.SetValue(idMarca, 0);
                valoresPram.SetValue(nombre,1);
            }

            //Ejecuto el comando a la BD
                try
                {
                    if (valorParametros.GetValue(0) == null && valoresPram.GetValue (0) == null)
                    {
                        //Se ejcuta normalmente y por defecto trae todos los elementos de la DB
                        DB.FillDataSet(ds, "DESIGNACIONES", sql, null);
                    }
                    else
                    {
                        if (valoresPram.GetValue(0) == null)
                        {
                            DB.FillDataSet(ds, "DESIGNACIONES", sql, valorParametros);
                        }
                        else
                        {
                            DB.FillDataSet(ds, "DESIGNACIONES", sql, valoresPram);
                        }
                    }   
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
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
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
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
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
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
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
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
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
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
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
