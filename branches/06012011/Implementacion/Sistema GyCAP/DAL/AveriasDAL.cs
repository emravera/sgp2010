using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class AveriasDAL
    {
        public static long Insertar(Entidades.Averia averia)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO AVERIAS (MAQ_CODIGO, NCRI_CODIGO, AVE_DESCRIPCION, AVE_FECHA_ALTA, AVE_USU_CODIGO) VALUES (@p0,@p1,@p2,@p3,@p4) SELECT @@Identity";
            object[] valorParametros = { averia.Maquina.Codigo, averia.Nivel.Codigo, averia.Descripcion, averia.FechaAlta,averia.CodUsuario };
            try
            {
                return Convert.ToInt64(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(long codigo)
        {
            string sql = "DELETE FROM AVERIAS WHERE AVE_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.Averia averia)
        {
            string sql = "UPDATE AVERIAS SET MAQ_CODIGO = @p1, NCRI_CODIGO = @p2, AVE_DESCRIPCION = @p3, AVE_FECHA_ALTA = @p4, AVE_USU_CODIGO = @p5 WHERE AVE_CODIGO = @p0";
            object[] valorParametros = { averia.Codigo, averia.Maquina.Codigo, averia.Nivel.Codigo, averia.Descripcion, averia.FechaAlta, averia.CodUsuario };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsAveria(Entidades.Averia averia)
        {
            string sql = "SELECT count(ave_CODIGO) FROM AVERIAS WHERE AVE_DESCRIPCION = @p0";
            object[] valorParametros = { averia.Descripcion };
            try
            {
                if (Convert.ToInt64(DB.executeScalar(sql, valorParametros, null)) == 0)
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

        public static void ObtenerAverias(string nombre, int codNivelCriticidad, Data.dsMantenimiento ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT *
                              FROM AVERIAS
                              WHERE 1 = 1 ";

                //Sirve para armar el nombre de los parámetros
                int cantidadParametros = 0;
                //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
                object[] valoresFiltros = new object[1];
                //Empecemos a armar la consulta, revisemos que filtros aplican

                // NOMBRE
                if (nombre != null && nombre.ToString() != string.Empty)
                {
                    //Si aplica el filtro lo usamos
                    sql += " AND AVE_DESCRPCION LIKE @p" + cantidadParametros;
                    //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                    nombre = "%" + nombre + "%";
                    valoresFiltros[cantidadParametros] = nombre;
                    cantidadParametros++;
                }

                //ESTADO - Revisamos si es distinto de 0, o sea "todos"
                if (codNivelCriticidad != -1)
                {
                    sql += " AND NCRI_CODIGO = @p" + cantidadParametros;
                    valoresFiltros[cantidadParametros] = Convert.ToInt32(codNivelCriticidad);
                    cantidadParametros++;
                }

                try
                {
                    if (cantidadParametros > 0)
                    {
                        //Buscamos con filtro, armemos el array de los valores de los parametros
                        object[] valorParametros = new object[cantidadParametros];
                        for (int i = 0; i < cantidadParametros; i++)
                        {
                            valorParametros[i] = valoresFiltros[i];
                        }
                        DB.FillDataSet(ds, "AVERIAS", sql, valorParametros);
                    }
                    else
                    {
                        //Buscamos sin filtro
                        DB.FillDataSet(ds, "AVERIAS", sql, null);
                    }
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = "SELECT * FROM AVERIAS ";
                try
                {
                    DB.FillDataSet(ds, "AVERIAS", sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

        public static void ObtenerAverias(Data.dsMantenimiento ds)
        {
            string sql = @"SELECT *
                           FROM AVERIAS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "AVERIAS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerAverias(DataTable dtAveria)
        {
            string sql = @"SELECT *
                           FROM AVERIAS";
            try
            {
                //Se llena el Dataset
                DB.FillDataTable(dtAveria, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(long codigo)
        {
            //string sql = "SELECT count(REP_CODIGO) FROM REPUESTOS WHERE TREP_CODIGO = @p0";
            //object[] valorParametros = { codigo };
            //try
            //{
            //    if (Convert.ToInt64(DB.executeScalar(sql, valorParametros, null)) == 0)
            //    {
                    return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
