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
        public static readonly int CocinaActiva = 1;
        public static readonly int CocinaInactiva = 0;
        public enum ImagenStatus { SinImagen, ConImagen };
        
        public static int Insertar(Entidades.Cocina cocina)
        {
            string sql = @"INSERT INTO [COCINAS] 
                        ([col_codigo]
                        ,[mod_codigo]
                        ,[mca_codigo]
                        ,[te_codigo]
                        ,[desig_codigo]
                        ,[coc_codigo_producto]
                        ,[coc_activo]
                        ,[coc_has_image])
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7) SELECT @@Identity";

            object[] valorParametros = { cocina.Color.Codigo,
                                         cocina.Modelo.Codigo,
                                         cocina.Marca.Codigo,
                                         cocina.TerminacionHorno.Codigo,
                                         cocina.Designacion.Codigo,
                                         cocina.CodigoProducto,
                                         cocina.Activo,
                                         cocina.HasImage 
                                       };

            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Actualizar(Entidades.Cocina cocina)
        {
            string sql = @"UPDATE COCINAS SET 
                         col_codigo = @p0
                        ,mod_codigo = @p1
                        ,mca_codigo = @p2
                        ,te_codigo = @p3
                        ,desig_codigo = @p4
                        ,coc_codigo_producto = @p5
                        ,coc_activo = @p6
                        ,coc_has_image = @p7
                        WHERE coc_codigo = @p8";

            object[] valorParametros = { cocina.Color.Codigo,
                                         cocina.Modelo.Codigo,
                                         cocina.Marca.Codigo,
                                         cocina.TerminacionHorno.Codigo,
                                         cocina.Designacion.Codigo,
                                         cocina.CodigoProducto,
                                         cocina.Activo,
                                         cocina.HasImage,
                                         cocina.CodigoCocina 
                                       };

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
            string sql = @"SELECT coc_codigo, col_codigo, mod_codigo, mca_codigo, te_codigo, desig_codigo, 
                         coc_codigo_producto, coc_activo, coc_has_image FROM COCINAS";

            try
            {
                DB.FillDataTable(dtCocina, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }        

        //Obtiene todas las cocinas que coincidan con los filtros
        public static void ObtenerCocinas(object codigo, object codMarca, object codTerminacion, object codEstado, DataTable dtCocina)
        {
            string sql = @"SELECT coc_codigo, col_codigo, mod_codigo, mca_codigo, te_codigo, desig_codigo, 
                        coc_codigo_producto, coc_activo, coc_has_image FROM COCINAS WHERE 1=1 ";

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
                sql += " AND coc_activo = @p" + cantidadParametros;
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
            //ver otras condiciones - gonzalo
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

        public static int ObtenerCodigoEstructuraActiva(int codigoCocina)
        {
            //Cambiar este codigo para que quede una sola consulta - gonzalo
            string sql = "SELECT count(estr_codigo) FROM ESTRUCTURAS WHERE coc_codigo = @p0 AND estr_activo = @p1";
            object[] valorParametros = { codigoCocina, EstructuraDAL.EstructuraActiva };
            int codigo = 0;
            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) != 0)
                {
                    sql = "SELECT estr_codigo FROM ESTRUCTURAS WHERE coc_codigo = @p0 AND estr_activo = @p1";
                    codigo = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
                }

                return codigo;
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool TieneEstructuraActiva(int codigoCocina)
        {
            string sql = "SELECT count(estr_codigo) FROM ESTRUCTURAS WHERE coc_codigo = @p0 AND estr_activo = @p1";
            object[] valorParametros = { codigoCocina, EstructuraDAL.EstructuraActiva };
            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0) { return false; }
                return true;
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void SetImageStatus(int codigoCocina, ImagenStatus status)
        {
            string sql = "UPDATE COCINA SET coc_has_image = @p0 WHERE coc_codigo = @p1";
            object[] valorParametros = { (status == ImagenStatus.ConImagen) ? 1 : 0 , codigoCocina };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

    }
}
