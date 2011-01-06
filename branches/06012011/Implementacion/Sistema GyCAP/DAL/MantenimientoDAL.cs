using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class MantenimientoDAL
    {
        //BUSQUEDA
        //Metodo sobrecargado (3 Sobrecargas)
        //Busqueda por nombre
        public static void ObtenerMantenimientos(object nombre, int idEstado, string cadFabricantes, string cadModelo, Data.dsPlanMantenimiento ds)
        {
//            string sql = @"SELECT MAN_CODIGO,MODM_CODIGO, EMAQ_CODIGO, FAB_CODIGO, MAQ_NUMEROSERIE, MAQ_NOMBRE,
//                           MAQ_MARCA, MAQ_FECHAALTA, MAQ_ES_CRITICA
//                           FROM MAQUINAS
//                           WHERE 1 = 1 ";

//            //Sirve para armar el nombre de los parámetros
//            int cantidadParametros = 0;
//            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
//            object[] valoresFiltros = new object[3];
//            //Empecemos a armar la consulta, revisemos que filtros aplican

//            // NOMBRE
//            if (nombre != null && nombre.ToString() != string.Empty)
//            {
//                //Si aplica el filtro lo usamos
//                sql += " AND MAQ_NOMBRE LIKE @p" + cantidadParametros;
//                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
//                nombre = "%" + nombre + "%";
//                valoresFiltros[cantidadParametros] = nombre;
//                cantidadParametros++;
//            }

//            //ESTADO - Revisamos si es distinto de 0, o sea "todos"
//            if (idEstado != -1)
//            {
//                sql += " AND EMAQ_CODIGO = @p" + cantidadParametros;
//                valoresFiltros[cantidadParametros] = Convert.ToInt32(idEstado);
//                cantidadParametros++;
//            }

//            if (cadModelo != string.Empty)
//            {   //Ver como seria con parametros
//                //sql += " AND SEC_CODIGO IN (@p" + cantidadParametros + ")";
//                //valoresFiltros[cantidadParametros] = cadSectores;
//                //cantidadParametros++;

//                sql += " AND MODM_CODIGO IN (" + cadModelo + ")";
//            }

//            if (cadFabricantes != string.Empty)
//            {   //Ver como seria con parametros
//                //sql += " AND SEC_CODIGO IN (@p" + cantidadParametros + ")";
//                //valoresFiltros[cantidadParametros] = cadSectores;
//                //cantidadParametros++;

//                sql += " AND FAB_CODIGO IN (" + cadFabricantes + ")";
//            }

//            try
//            {
//                if (cantidadParametros > 0)
//                {
//                    //Buscamos con filtro, armemos el array de los valores de los parametros
//                    object[] valorParametros = new object[cantidadParametros];
//                    for (int i = 0; i < cantidadParametros; i++)
//                    {
//                        valorParametros[i] = valoresFiltros[i];
//                    }
//                    DB.FillDataSet(ds, "MAQUINAS", sql, valorParametros);
//                }
//                else
//                {
//                    //Buscamos sin filtro
//                    DB.FillDataSet(ds, "MAQUINAS", sql, null);
//                }
//            }
//            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //ELIMINACION
        //Metodo que verifica que no este usado en otro lugar
        public static bool PuedeEliminarse(long codigo)
        {
            string sql = "SELECT count(DPMAN_CODIGO) FROM DETALLE_PLANES_MANTENIMIENTO WHERE MAN_CODIGO = @p0";
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
            string sql = "DELETE FROM MANTENIMIENTOS WHERE MAN_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //INSERTAR
        //Metodo que valida que no se intente guardar algo que ya esta en la BD
        public static bool esMantenimiento(Entidades.Mantenimiento mantenimiento)
        {
            string sql = "SELECT count(MAN_CODIGO) FROM MANTENIMIENTOS WHERE PMAN_DESCRIPCION = @p0";
            object[] valorParametros = { mantenimiento.Descripcion };
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
        public static int Insertar(Entidades.Mantenimiento mantenimiento)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = @"INSERT INTO [MANTENIMIENTOS] ([TMAN_CODIGO], [MAN_DESCRIPCION], [CEMP_CODIGO],
                           [MAN_OBSERVACION], [MAN_REQUIERE_PARAR_PLANTA])
                          VALUES (@p0, @p1, @p2, @p3, @p4) SELECT @@Identity";

            object[] valorParametros = { mantenimiento.Tipo.Codigo, mantenimiento.Descripcion, mantenimiento.CapacidadEmpleado.Codigo, 
                                         mantenimiento.Observacion, mantenimiento.RequierePararPlanta };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.Mantenimiento mantenimiento)
        {
            string sql = @"UPDATE MANTENIMIENTOS SET TMAN_CODIGO = @p1, MAN_DESCRIPCION = @p2, CEMP_CODIGO = @p3, 
                           MAN_OBSERVACION = @p4, MAN_REQUIERE_PARAR_PLANTA = @p5
                           WHERE MAN_CODIGO = @p0";

            object[] valorParametros = { mantenimiento.Codigo, 
                                         mantenimiento.Tipo.Codigo, mantenimiento.Descripcion, mantenimiento.CapacidadEmpleado.Codigo, 
                                         mantenimiento.Observacion, mantenimiento.RequierePararPlanta };
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
        public static void ObtenerMantenimientos(DataTable dtMantenimiento)
        {
            string sql = @"SELECT MAN_CODIGO, TMAN_CODIGO, MAN_DESCRIPCION, CEMP_CODIGO, 
                           MAN_OBSERVACION, MAN_REQUIERE_PARAR_PLANTA 
                           FROM MANTENIMIENTOS ";

            DB.FillDataTable(dtMantenimiento, sql, null);
        }

        public static void ObtenerMantenimientos(Data.dsPlanMantenimiento ds)
        {
            string sql = @"SELECT MAN_CODIGO, TMAN_CODIGO, MAN_DESCRIPCION, CEMP_CODIGO, 
                           MAN_OBSERVACION, MAN_REQUIERE_PARAR_PLANTA 
                           FROM MANTENIMIENTOS ";

            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "MANTENIMIENTOS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
