using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class CausaFalloDAL
    {
        public static int Insertar(Entidades.CausaFallo causaFallo)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = @"INSERT INTO CAUSAS_FALLO (CF_CODIGO,
                                                     CF_NOMBRE, 
                                                     CF_DESCRPCION) 
                            VALUES (@p0,@p1,@p2) SELECT @@Identity";

            object[] valorParametros = {causaFallo.Codigo , causaFallo.Nombre, causaFallo.Descripcion };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM CAUSAS_FALLO WHERE CF_NUMERO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.CausaFallo causaFallo)
        {
            string sql = @"UPDATE CAUSAS_FALLO SET CF_CODIGO = @p1, 
                                                CF_NOMBRE = @p2, 
                                                CF_DESCRPCION = @p3
                                                WHERE CF_NUMERO = @p0";

            object[] valorParametros = { causaFallo.Numero, causaFallo.Codigo, 
                                         causaFallo.Nombre, causaFallo.Descripcion };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsCausaFallo(Entidades.CausaFallo causaFallo)
        {
            string sql = @"SELECT count(CF_NUMERO) 
                            FROM CAUSAS_FALLO 
                            WHERE CF_CODIGO = @p0
                            AND CF_NUMERO <> @p1 ";

            object[] valorParametros = { causaFallo.Codigo, causaFallo.Numero };
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

        public static void ObtenerCausaFallo(string nombre, string codigo, Data.dsMantenimiento ds)
        {
            string sql = @"SELECT * FROM CAUSAS_FALLO WHERE 1 = 1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[2];
            //Empecemos a armar la consulta, revisemos que filtros aplican

            // NOMBRE
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND CF_NOMBRE LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }

            //CODIGO 
            if (codigo != null && codigo.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND CF_CODIGO LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                codigo = "%" + codigo + "%";
                valoresFiltros[cantidadParametros] = codigo;
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
                    DB.FillDataSet(ds, "CAUSAS_FALLO", sql, valorParametros);
                }
                else
                {
                    //Buscamos sin filtro
                    DB.FillDataSet(ds, "CAUSAS_FALLO", sql, null);
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        public static void ObtenerCausaFallo(Data.dsMantenimiento ds)
        {
            string sql = @"SELECT * FROM CAUSAS_FALLO ";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "CAUSAS_FALLO", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerCausaFallo(DataTable dtCausaFallo)
        {
            string sql = @"SELECT * FROM CAUSAS_FALLO";
            try
            {
                //Se llena el Dataset
                DB.FillDataTable(dtCausaFallo, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(RMAN_CODIGO) FROM REGISTROS_MANTENIMIENTOS  WHERE cf_numero = @p0";

            object[] valorParametros = { codigo };
            try
            {
                int resultado = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
                if (resultado == 0) { return true; }
                else { return false; }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
