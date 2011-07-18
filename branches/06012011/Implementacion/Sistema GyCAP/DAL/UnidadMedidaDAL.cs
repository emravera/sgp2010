using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class UnidadMedidaDAL
    {
       //BUSQUEDA
        public static void ObtenerUnidad(string nombre, int idTipo, Data.dsPlanMP ds)
        {
            string sql = @"SELECT umed_codigo,tumed_codigo, umed_nombre, umed_abreviatura
                              FROM UNIDADES_MEDIDA";

            object[] valorParametros = { null };
            object[] valoresPram = { null, null };

            //Si busca solo por el nombre
            if (nombre != String.Empty && idTipo == -1)
            {
                //Agrego la busqueda por nombre
                sql = sql + " WHERE umed_nombre LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valorParametros.SetValue(nombre, 0);
            }
            else if (nombre == string.Empty && idTipo != -1)
            {
                //Agrego la busqueda por marca
                sql = sql + " WHERE tumed_codigo=@p0";
                valorParametros.SetValue(idTipo, 0);
            }
            else if (nombre != string.Empty && idTipo != -1)
            {
                //Agrego la busqueda por marca
                sql = sql + " WHERE tumed_codigo=@p0 and umed_nombre LIKE @p1";
                nombre = "%" + nombre + "%";
                //Le doy valores a la estructura
                valoresPram.SetValue(idTipo, 0);
                valoresPram.SetValue(nombre, 1);
            }

            //Ejecuto el comando a la BD
            try
            {
                if (valorParametros.GetValue(0) == null && valoresPram.GetValue(0) == null)
                {
                    //Se ejcuta normalmente y por defecto trae todos los elementos de la DB
                    DB.FillDataSet(ds, "UNIDADES_MEDIDA", sql, null);
                }
                else
                {
                    if (valoresPram.GetValue(0) == null)
                    {
                        DB.FillDataSet(ds, "UNIDADES_MEDIDA", sql, valorParametros);
                    }
                    else
                    {
                        DB.FillDataSet(ds, "UNIDADES_MEDIDA", sql, valoresPram);
                    }
                }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
       
        //Metodo para llenar desde materia primas principales
        public static void ObtenerTodos(DataTable dtUnidadMedida)
        {
            string sql = @"SELECT umed_codigo,tumed_codigo, umed_nombre, umed_abreviatura
                              FROM UNIDADES_MEDIDA";
            try
            {
                DB.FillDataTable(dtUnidadMedida, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }

        }

        //Metodo que obtiene una unidad de medida a partir de su codigo
        public static string ObtenerUnidad(int codigoUnidad)
        {
            string nombre =  string.Empty;
            string sql = @"SELECT umed_nombre FROM UNIDADES_MEDIDA WHERE umed_codigo= @p0";

            object[] valorParametros = { codigoUnidad };
            try
            {
                nombre = DB.executeScalar(sql, valorParametros, null).ToString();
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }

            return nombre;
        }
        
        //Metodo para llenar desde planificacion  materia primas principales
        public static void ObtenerUnidades(Data.dsPlanMateriasPrimas ds)
        {
            string sql = @"SELECT umed_codigo,tumed_codigo, umed_nombre, umed_abreviatura
                              FROM UNIDADES_MEDIDA";
            try
            {
                DB.FillDataSet(ds, "UNIDADES_MEDIDA", sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }

        }
        
        //ELIMINACION
        //Metodo que verifica que no este usado en otro lugar
        public static bool PuedeEliminarse(int codigo)
        {
            string sql1 = "SELECT count(umed_codigo) FROM MATERIAS_PRIMAS WHERE umed_codigo = @p0";
            string sql2 = "SELECT count(umed_codigo) FROM PARTES WHERE umed_codigo = @p0";
            string sql3 = "SELECT count(umed_codigo) FROM COMPUESTOS_PARTES WHERE umed_codigo = @p0";
            string sql4 = "SELECT count(umed_codigo) FROM DETALLE_PLANES_MANTENIMIENTO WHERE umed_codigo = @p0";
            string sql5 = "SELECT count(umed_codigo) FROM UBICACIONES_STOCK WHERE umed_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                int r1 = Convert.ToInt32(DB.executeScalar(sql1, valorParametros, null));
                int r2 = Convert.ToInt32(DB.executeScalar(sql2, valorParametros, null));
                int r3 = Convert.ToInt32(DB.executeScalar(sql3, valorParametros, null));
                int r4 = Convert.ToInt32(DB.executeScalar(sql4, valorParametros, null));
                int r5 = Convert.ToInt32(DB.executeScalar(sql5, valorParametros, null));
                if (r1 + r2 + r3 + r4 + r5 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        //INSERTAR
        //Metodo que valida que no se intente guardar algo que ya esta en la BD
        public static bool esUnidadMedida(Entidades.UnidadMedida unidadMedida)
        {
            string sql = "SELECT count(umed_codigo) FROM UNIDADES_MEDIDA WHERE umed_nombre = @p0 AND umed_codigo <> @p1";
            object[] valorParametros = { unidadMedida.Nombre, unidadMedida.Codigo };
            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0) { return false; }
                else { return true; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

   }
}
