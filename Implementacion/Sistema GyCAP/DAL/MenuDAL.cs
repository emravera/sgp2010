using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class MenuDAL
    {
        //BUSQUEDA
        //Trae todos los elementos
        public static void ObtenerTodos(Data.dsSeguridad ds)
        {
            string sql = @"SELECT * 
                             FROM MENU ";
            try
            {
                DB.FillDataSet(ds, "MENU", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        /// <summary>
        /// Obtiene todos el menu sin filtrar, los carga en una DataTable del tipo menu.
        /// </summary>
        /// <param name="dtEmpleado">La tabla donde cargar losd datos.</param>
        public static void ObtenerTodos(DataTable dtMenu)
        {
            string sql = @"SELECT *
                             FROM MENU ";

            DB.FillDataTable(dtMenu, sql, null);
        }

        //BUSQUEDA
        //Metodo sobrecargado (3 Sobrecargas)
        //Busqueda por nombre
        public static void ObtenerTodos(int usuario, Data.dsSeguridad ds)
        {
            string sql = @"SELECT MENU.*
                             FROM MENU 
                                  LEFT OUTER JOIN MENU_USUARIOS 
                                  ON MENU.MNU_CODIGO = MENU_USUARIOS.MNU_CODIGO
                            WHERE 1 = 1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[2];
            //Empecemos a armar la consulta, revisemos que filtros aplican

            //USUARIO - Revisamos si es distinto de 0, o sea "todos"
            if (usuario != -1)
            {
                sql += " AND U_CODIGO = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = usuario;
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
                    DB.FillDataSet(ds,  "MENU", sql, valorParametros);
                }
                else
                {
                    //Buscamos sin filtro
                    DB.FillDataSet(ds, "MENU", sql, null);
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
