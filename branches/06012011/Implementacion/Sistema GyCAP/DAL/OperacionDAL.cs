using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class OperacionDAL
    {
        public static void ObtenerOperaciones(DataTable dtOperaciones)
        {
            string sql = @"SELECT opr_numero, opr_codigo, opr_nombre, opr_descripcion, opr_horasrequerida 
                         FROM OPERACIONES";
            
            try
            {
                DB.FillDataTable(dtOperaciones, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
        public static void ObtenerOperacion(int numeroOperacion, Data.dsHojaRuta ds)
        {
            string sql = @"SELECT opr_numero, opr_codigo, opr_nombre, opr_descripcion, opr_horasrequerida 
                         FROM OPERACIONES WHERE opr_numero = @p0";
            object[] valoresParametros = { numeroOperacion };

            try
            {
                DB.FillDataTable(ds.OPERACIONES, sql, valoresParametros);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
        //Busqueda con filtros desde el formulario
        public static void buscarOperacion(Data.dsOperacionesFabricacion dsOperaciones, string nombre, string codificacion)
        {
            string sql = @"SELECT opr_numero, opr_codigo, opr_nombre, opr_descripcion, opr_horasrequerida 
                           FROM OPERACIONES";

            object[] valorParametros = { null };
            object[] valoresPram = { null, null };

            //Si busca solo por el nombre
            if (nombre != String.Empty && codificacion == string.Empty)
            {
                //Agrego la busqueda por nombre
                sql = sql + " WHERE opr_nombre LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valorParametros.SetValue(nombre, 0);
            }
            else if (nombre == string.Empty && codificacion != string.Empty)
            {
                //Agrego la busqueda por marca
                sql = sql + " WHERE opr_codigo LIKE @p0";
                codificacion = "%" + codificacion + "%";
                valorParametros.SetValue(codificacion, 0);
            }
            else if (nombre != string.Empty && codificacion != string.Empty)
            {
                //Agrego la busqueda por marca
                sql = sql + " WHERE opr_codigo LIKE @p0 and opr_nombre LIKE @p1";
                codificacion = "%" + codificacion + "%";
                //Le doy valores a la estructura
                valoresPram.SetValue(codificacion, 0);
                valoresPram.SetValue(nombre, 1);
            }

            //Ejecuto el comando a la BD
            try
            {
                if (valorParametros.GetValue(0) == null && valoresPram.GetValue(0) == null)
                {
                    //Se ejcuta normalmente y por defecto trae todos los elementos de la DB
                    DB.FillDataSet(dsOperaciones, "OPERACIONES", sql, null);
                }
                else
                {
                    if (valoresPram.GetValue(0) == null)
                    {
                        DB.FillDataSet(dsOperaciones, "OPERACIONES", sql, valorParametros);
                    }
                    else
                    {
                        DB.FillDataSet(dsOperaciones, "OPERACIONES", sql, valoresPram);
                    }
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //METODO DE INSERCION
        //Metodo que inserta en la base de datos
        public static int Insertar(Entidades.OperacionFabricacion operacion)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = @"INSERT INTO [OPERACIONES] 
                        ([opr_codigo], 
                        [opr_nombre],
                        [opr_descripcion],
                        [opr_horasrequerida]) 
                        VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";
            
            object[] valorParametros = { operacion.Codificacion, 
                                           operacion.Nombre, 
                                           operacion.Descripcion, 
                                           operacion.HorasRequeridas };
            
            try
            {
                return Convert.ToInt32(DB.executeNonQuery(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //Metodo de ACTUALIZACION
        public static void ModificarOperacion(Entidades.OperacionFabricacion operacion)
        {
            string sql = @"UPDATE OPERACIONES SET opr_codigo = @p0, opr_nombre = @p1, opr_descripcion = @p2, opr_horasrequerida = @p3 
                            WHERE opr_numero = @p4";
            
            object[] valorParametros = { operacion.Codificacion, 
                                           operacion.Nombre, 
                                           operacion.Descripcion, 
                                           operacion.HorasRequeridas,
                                           operacion.Codigo };
            
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //Metodo de Eliminacion
        public static void EliminarOperacion(int codigo)
        {
            string sql = "DELETE FROM OPERACIONES WHERE opr_numero = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
