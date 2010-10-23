using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class MaquinaDAL
    {
        //BUSQUEDA
        //Metodo sobrecargado (3 Sobrecargas)
        //Busqueda por nombre
        public static void ObtenerMaquinas(object nombre,int idEstado, string cadFabricantes, string cadModelo, Data.dsMaquina ds)
        {
            string sql = @"SELECT MAQ_CODIGO,MODM_CODIGO, EMAQ_CODIGO, FAB_CODIGO, MAQ_NUMEROSERIE, MAQ_NOMBRE,
                           MAQ_MARCA, MAQ_FECHAALTA, MAQ_ES_CRITICA
                           FROM MAQUINAS
                           WHERE 1 = 1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[3];
            //Empecemos a armar la consulta, revisemos que filtros aplican

            // NOMBRE
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND MAQ_NOMBRE LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }
               
            //ESTADO - Revisamos si es distinto de 0, o sea "todos"
            if (idEstado != -1)
            {
                sql += " AND EMAQ_CODIGO = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(idEstado);
                cantidadParametros++;
            }

            if (cadModelo != string.Empty)
            {   //Ver como seria con parametros
                //sql += " AND SEC_CODIGO IN (@p" + cantidadParametros + ")";
                //valoresFiltros[cantidadParametros] = cadSectores;
                //cantidadParametros++;

                sql += " AND MODM_CODIGO IN (" + cadModelo + ")";
            }

            if (cadFabricantes != string.Empty)
            {   //Ver como seria con parametros
                //sql += " AND SEC_CODIGO IN (@p" + cantidadParametros + ")";
                //valoresFiltros[cantidadParametros] = cadSectores;
                //cantidadParametros++;

                sql += " AND FAB_CODIGO IN (" + cadFabricantes + ")";
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
                    DB.FillDataSet(ds, "MAQUINAS", sql, valorParametros);
                }
                else
                {
                    //Buscamos sin filtro
                    DB.FillDataSet(ds, "MAQUINAS", sql, null);
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //ELIMINACION
        //Metodo que verifica que no este usado en otro lugar
        public static bool PuedeEliminarse(long codigo)
        {
            string sql = "SELECT count(RMAN_CODIGO) FROM REGISTROS_MANTENIMIENTOS WHERE MAQ_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                //if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0)
                //{
                return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo que elimina de la base de datos
        public static void Eliminar(long codigo)
        {
            string sql = "DELETE FROM MAQUINAS WHERE MAQ_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //INSERTAR
        //Metodo que valida que no se intente guardar algo que ya esta en la BD
        public static bool esMaquina(Entidades.Maquina maquina)
        {
            string sql = "SELECT count(MAQ_codigo) FROM MAQUINAS WHERE MAQ_NOMBRE = @p0";
            object[] valorParametros = { maquina.Nombre };
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
        public static int Insertar(Entidades.Maquina maquina)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = @"INSERT INTO [MAQUINAS] ([MODM_CODIGO], [EMAQ_CODIGO], [FAB_CODIGO],
                           [MAQ_NOMBRE], [MAQ_NUMEROSERIE], [MAQ_MARCA], [MAQ_FECHAALTA], [MAQ_ES_CRITICA])
                          VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7) SELECT @@Identity";

            object[] valorParametros = { maquina.Modelo.Codigo, maquina.Estado.Codigo, maquina.Fabricante.Codigo, 
                                         maquina.Nombre, maquina.NumeroSerie, maquina.Marca, maquina.FechaAlta, maquina.EsCritica };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message ); }
        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.Maquina maquina)
        {
            string sql = @"UPDATE MAQUINAS SET MODM_CODIGO = @p1, EMAQ_CODIGO = @p2, FAB_CODIGO = @p3, 
                           MAQ_NOMBRE = @p4, MAQ_NUMEROSERIE = @p5, MAQ_MARCA = @p6, MAQ_ES_CRITICA = @p7
                           WHERE MAQ_CODIGO = @p0";

            object[] valorParametros = { maquina.Codigo, 
                                         maquina.Modelo.Codigo, maquina.Estado.Codigo, maquina.Fabricante.Codigo, 
                                         maquina.Nombre, maquina.NumeroSerie, maquina.Marca, maquina.EsCritica };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        /// <summary>
        /// Obtiene todas las máquinas sin filtrar, los carga en una DataTable del tipo de máquina.
        /// </summary>
        /// <param name="dtMaquina">La tabla donde cargar los datos.</param>
        public static void ObtenerMaquinas(DataTable dtMaquina)
        {
            string sql = @"SELECT MAQ_CODIGO, EMAQ_CODIGO, MODM_CODIGO, FAB_CODIGO, MAQ_NOMBRE,
                           MAQ_NUMEROSERIE, MAQ_FECHAALTA, MAQ_MARCA, MAQ_ES_CRITICA 
                           FROM MAQUINAS ";

            DB.FillDataTable(dtMaquina, sql, null);
        }

        public static void ObtenerMaquinas(Data.dsMantenimiento ds)
        {
            string sql = @"SELECT MAQ_CODIGO, EMAQ_CODIGO, MODM_CODIGO, FAB_CODIGO, MAQ_NOMBRE,
                           MAQ_NUMEROSERIE, MAQ_FECHAALTA, MAQ_MARCA, MAQ_ES_CRITICA 
                           FROM MAQUINAS ";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "MAQUINAS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
