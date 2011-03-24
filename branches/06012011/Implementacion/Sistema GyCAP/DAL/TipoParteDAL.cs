using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class TipoParteDAL
    {
        public static void ObtenerTodos(DataTable dtTiposPartes)
        {
            string sql = @"SELECT tpar_codigo, tpar_nombre, tpar_descripcion, tpar_productoterminado, tpar_fantasma, 
                                  tpar_ordentrabajo, tpar_ensamblado, tpar_adquirido FROM TIPOS_PARTES";

            try
            {
                DB.FillDataTable(dtTiposPartes, sql, null);
                
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Insertar(Entidades.TipoParte tipoParte)
        {
            string sql = @"INSERT INTO [TIPOS_PARTES] 
                       ([tpar_nombre],
                        [tpar_descripcion],
                        [tpar_productoterminado],
                        [tpar_fantasma], 
                        [tpar_ordentrabajo], 
                        [tpar_ensamblado], 
                        [tpar_adquirido]) 
                        VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6) SELECT @@Identity";

            object[] parametros = { tipoParte.Nombre, 
                                      tipoParte.Descripcion, 
                                      tipoParte.ProductoTerminado,
                                      tipoParte.Fantasma,
                                      tipoParte.Ordentrabajo,
                                      tipoParte.Ensamblado,
                                      tipoParte.Adquirido };
            
            try
            {
                tipoParte.Codigo = Convert.ToInt32(DB.executeScalar(sql, parametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Actualizar(Entidades.TipoParte tipoParte)
        {
            string sql = @"UPDATE TIPOS_PARTES SET 
                        tpar_nombre = @p0, 
                        tpar_descripcion = @p1, 
                        tpar_productoterminado = @p2,
                        tpar_fantasma = @p3, 
                        tpar_ordentrabajo = @p4, 
                        tpar_ensamblado = @p5, 
                        tpar_adquirido = @p6 
                        WHERE tpar_codigo = @p7";

            object[] parametros = { tipoParte.Nombre, 
                                      tipoParte.Descripcion, 
                                      tipoParte.ProductoTerminado, 
                                      tipoParte.Fantasma,
                                      tipoParte.Ordentrabajo,
                                      tipoParte.Ensamblado,
                                      tipoParte.Adquirido,
                                      tipoParte.Codigo };

            try
            {
                DB.executeNonQuery(sql, parametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void Eliminar(int codigoTipoParte)
        {
            string sql = "DELETE FROM TIPOS_PARTES WHERE tpar_codigo = @p0";
            object[] parametros = { codigoTipoParte };

            try
            {
                DB.executeNonQuery(sql, parametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool PuedeEliminarse(int codigoTipoParte)
        {
            string sql = "SELECT count(part_numero) FROM PARTES WHERE tpar_codigo = @p0";
            object[] parametros = { codigoTipoParte };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, parametros, null)) == 0) { return true; }
                else { return false; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool EsTipoParte(Entidades.TipoParte tipoParte)
        {
            string sql = "SELECT count(tpar_codigo) FROM TIPOS_PARTES WHERE tpar_nombre = @p0";
            object[] parametros = { tipoParte.Nombre };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, parametros, null)) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static void ObtenerTiposPartes(object nombre, object fantasma, object orden, object ensamblado, object adquirido, object terminado, DataTable dtTiposPartes)
        {
            string sql = @"SELECT tpar_codigo, tpar_nombre, tpar_descripcion, tpar_productoterminado, tpar_fantasma, 
                                  tpar_ordentrabajo, tpar_ensamblado, tpar_adquirido FROM TIPOS_PARTES WHERE 1=1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[6];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //si aplica el filtro lo usamos
                sql += " AND tpar_nombre LIKE @p" + cantidadParametros + " ";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }
            //Revisamos si pasó algun valor y si es un integer
            if (fantasma != null && fantasma.GetType() == cantidadParametros.GetType())
            {
                sql += " AND tpar_fantasma = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(fantasma);
                cantidadParametros++;
            }

            if (orden != null && orden.GetType() == cantidadParametros.GetType())
            {
                sql += " AND tpar_ordentrabajo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(orden);
                cantidadParametros++;
            }

            if (ensamblado != null && ensamblado.GetType() == cantidadParametros.GetType())
            {
                sql += " AND tpar_ensamblado = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(ensamblado);
                cantidadParametros++;
            }

            if (adquirido != null && adquirido.GetType() == cantidadParametros.GetType())
            {
                sql += " AND tpar_adquirido = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(adquirido);
                cantidadParametros++;
            }

            if (terminado != null && terminado.GetType() == cantidadParametros.GetType())
            {
                sql += " AND tpar_productoterminado = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(terminado);
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
                    DB.FillDataTable(dtTiposPartes, sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
            else
            {
                //Buscamos sin filtro
                try
                {
                    DB.FillDataTable(dtTiposPartes, sql, null);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
        }
    }
}
