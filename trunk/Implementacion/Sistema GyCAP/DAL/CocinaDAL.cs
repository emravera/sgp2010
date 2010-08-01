using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class CocinaDAL
    {
        //Obtiene todas las cocinas, sin filtrar
        public static void ObtenerCocinas(DataTable dtCocina)
        {
            string sql = @"SELECT coc_codigo, ecoc_codigo, col_codigo, mod_codigo, mca_codigo, te_codigo, desig_codigo, 
                         coc_codigo_producto, coc_cantidadstock, coc_activo, coc_precio FROM COCINAS";

            try
            {
                DB.FillDataTable(dtCocina, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        //Obtiene todas las cocinas que coincidan con los filtros
        public static void ObtenerCocinas(object codigo, object codMarca, object codTerminacion, object codEstado, DataTable dtCocina)
        {
            string sql = @"SELECT coc_codigo, ecoc_codigo, col_codigo, mod_codigo, mca_codigo, te_codigo, desig_codigo, 
                        coc_codigo_producto, coc_cantidadstock, coc_activo, coc_precio FROM COCINAS WHERE 1=1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[4];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (codigo != null && codigo.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND coc_codigo_producto LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                codigo = "%" + codigo + "%";
                valoresFiltros[cantidadParametros] = codigo;
                cantidadParametros++;
            }
            //Revisamos si pasó algun valor y si es un integer
            if (codMarca != null && codMarca.GetType() == cantidadParametros.GetType())
            {
                sql += " AND mca_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(codMarca);
                cantidadParametros++;
            }

            //Revisamos si pasó algun valor y si es un integer
            if (codTerminacion != null && codTerminacion.GetType() == cantidadParametros.GetType())
            {
                sql += " AND te_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(codTerminacion);
                cantidadParametros++;
            }

            //Revisamos si pasó algun valor y si es un integer
            if (codEstado != null && codEstado.GetType() == cantidadParametros.GetType())
            {
                sql += " AND ecoc_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(codEstado);
                cantidadParametros++;
            }

            if (cantidadParametros > 0)
            {
                //Buscamos con filtro, armemos el array de los valores de los parametros
                object[] valorParametros = new object[cantidadParametros];
                for (int i = 0; i < cantidadParametros; i++)
                {
                    valorParametros[i] = valoresFiltros[i];
                }
                try
                {
                    DB.FillDataTable(dtCocina, sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
            else
            {
                try
                {
                    DB.FillDataTable(dtCocina, sql, null);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
        }
    }
}
