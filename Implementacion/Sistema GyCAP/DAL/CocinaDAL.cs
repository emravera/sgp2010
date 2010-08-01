﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class CocinaDAL
    {
        public static int Insertar(Entidades.Cocina cocina)
        {
            string sql = @"INSERT INTO [COCINAS] 
                        ([ecoc_codigo]
                        ,[col_codigo]
                        ,[mod_codigo]
                        ,[mca_codigo]
                        ,[te_codigo]
                        ,[desig_codigo]
                        ,[coc_codigo_producto]
                        ,[coc_cantidadstock]
                        ,[coc_activo]
                        ,[coc_precio])
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)";

            object[] valorParametros = { cocina.Estado.Codigo,
                                         cocina.Color.Codigo,
                                         cocina.Modelo.Codigo,
                                         cocina.Marca.Codigo,
                                         cocina.TerminacionHorno.Codigo,
                                         cocina.Designacion.Codigo,
                                         cocina.CodigoProducto,
                                         0,
                                         1, //hasta ver si va o no le ponemos un valor por defecto
                                         cocina.Precio };

            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Actualizar(Entidades.Cocina cocina)
        {
            string sql = @"UPDATE COCINAS SET 
                         ecoc_codigo = @p0
                        ,col_codigo = @p1
                        ,mod_codigo = @p2
                        ,mca_codigo = @p3
                        ,te_codigo = @p4
                        ,desig_codigo = @p5
                        ,coc_codigo_producto = @p6
                        ,coc_activo = @p7
                        ,coc_precio = @p8
                        WHERE coc_codigo = @p9";

            object[] valorParametros = { cocina.Estado.Codigo,
                                         cocina.Color.Codigo,
                                         cocina.Modelo.Codigo,
                                         cocina.Marca.Codigo,
                                         cocina.TerminacionHorno.Codigo,
                                         cocina.Designacion.Codigo,
                                         cocina.CodigoProducto,
                                         1, //hasta ver si va o no le ponemos un valor por defecto
                                         cocina.Precio,
                                         cocina.CodigoCocina };

            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Eliminar(int codigoCocina)
        {
            string sql = "DELETE FROM COCINAS WHERE coc_codigo = @p0";
            object[] valorParametros = { codigoCocina };
            
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
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

        public static bool PuedeEliminarse(int codigoCocina)
        {
            string sql = "SELECT count(coc_codigo) FROM ESTRUCTURAS WHERE coc_codigo = @p0";
            object[] valorParametros = { codigoCocina };
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
