﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class EmpleadoDAL : UsuarioDAL
    {
        //BUSQUEDA
        //Metodo sobrecargado (3 Sobrecargas)
        //Busqueda por nombre
        public static void ObtenerEmpleado(string buscarPor, object nombre, int idEstadoEmpleado,string cadSectores, Data.dsEmpleado ds)
        {
            string sql = @"SELECT E_CODIGO, EE_CODIGO, SEC_CODIGO, E_APELLIDO, E_NOMBRE,
                           E_FECHANACIMIENTO, E_TELEFONO, E_LEGAJO, E_FECHA_ALTA, E_FECHA_BAJA 
                           FROM EMPLEADOS
                           WHERE 1 = 1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[3];
            //Empecemos a armar la consulta, revisemos que filtros aplican

            switch (buscarPor)
            {
                case "Legajo":
                    // LEGAJO
                    if (nombre != null && nombre.ToString() != string.Empty)
                    {
                        //Si aplica el filtro lo usamos
                        sql += " AND E_LEGAJO LIKE @p" + cantidadParametros;
                        //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                        nombre = "%" + nombre + "%";
                        valoresFiltros[cantidadParametros] = nombre;
                        cantidadParametros++;
                    }
                    break;
                case "Nombre":
                    // NOMBRE
                    if (nombre != null && nombre.ToString() != string.Empty)
                    {
                        //Si aplica el filtro lo usamos
                        sql += " AND E_NOMBRE LIKE @p" + cantidadParametros;
                        //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                        nombre = "%" + nombre + "%";
                        valoresFiltros[cantidadParametros] = nombre;
                        cantidadParametros++;
                    }
                    break;
                case "Apellido":
                    // APELLIDO
                    if (nombre != null && nombre.ToString() != string.Empty)
                    {
                        //Si aplica el filtro lo usamos
                        sql += " AND E_APELLIDO LIKE @p" + cantidadParametros;
                        //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                        nombre = "%" + nombre + "%";
                        valoresFiltros[cantidadParametros] = nombre;
                        cantidadParametros++;
                    }
                    break;
                default:
                    break;
            }

            //ESTADO - Revisamos si es distinto de 0, o sea "todos"
            if (idEstadoEmpleado != -1 )
            {
                sql += " AND EE_CODIGO = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(idEstadoEmpleado);
                cantidadParametros++;
            }

            if (cadSectores != string.Empty) 
            {   //Ver como seria con parametros
                //sql += " AND SEC_CODIGO IN (@p" + cantidadParametros + ")";
                //valoresFiltros[cantidadParametros] = cadSectores;
                //cantidadParametros++;

                sql += " AND SEC_CODIGO IN (" + cadSectores + ")";

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
                    DB.FillDataSet(ds, "EMPLEADOS", sql, valorParametros);
                }
                else
                {
                    //Buscamos sin filtro
                    DB.FillDataSet(ds, "EMPLEADOS", sql, null);
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //ELIMINACION
        //Metodo que verifica que no este usado en otro lugar
        public static bool PuedeEliminarse(long codigo)
        {
            string sqlDPM = "SELECT count(DPMAN_CODIGO) FROM DETALLE_PLANES_MANTENIMIENTO WHERE E_codigo = @p0";
            string sqlDOT = "SELECT count(MCA_CODIGO) FROM DETALLE_ORDENES_TRABAJO  WHERE E_codigo = @p0";
            string sqlESTR = "SELECT count(ESTR_CODIGO) FROM ESTRUCTURAS WHERE E_codigo = @p0";
            string sqlUSU = "SELECT count(U_CODIGO) FROM USUARIOS WHERE E_codigo = @p0";
            string sqlCXE = "SELECT count(CXE_CODIGO) FROM CAPACIDADESXEMPLEADO WHERE E_codigo = @p0";


            object[] valorParametros = { codigo };
            try
            {
                int resultadoDPM = Convert.ToInt32(DB.executeScalar(sqlDPM, valorParametros, null));
                int resultadoDOT = Convert.ToInt32(DB.executeScalar(sqlDOT, valorParametros, null));
                int resultadoESTR = Convert.ToInt32(DB.executeScalar(sqlESTR, valorParametros, null));
                int resultadoUSU = Convert.ToInt32(DB.executeScalar(sqlUSU, valorParametros, null));
                int resultadoCXE = Convert.ToInt32(DB.executeScalar(sqlCXE, valorParametros, null));

                if (resultadoDPM == 0 && resultadoDOT == 0 && resultadoESTR == 0 
                    && resultadoUSU == 0 && resultadoCXE == 0) { return true; }
                else { return false; }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo que elimina de la base de datos
        public static void Eliminar(long codigo)
        {
            string sql = "DELETE FROM EMPLEADOS WHERE E_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //INSERTAR
        //Metodo que valida que no se intente guardar algo que ya esta en la BD
        public static bool esEmpleado(Entidades.Empleado empleado)
        {
            string sql = "SELECT count(e_codigo) FROM EMPLEADOS WHERE E_LEGAJO = @p0";
            object[] valorParametros = { empleado.Legajo };
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
        public static int Insertar(Entidades.Empleado empleado)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = @"INSERT INTO [EMPLEADOS] ([EE_CODIGO], [SEC_CODIGO], [E_APELLIDO],
                           [E_NOMBRE], [E_FECHANACIMIENTO], [E_TELEFONO],
                           [E_LEGAJO], [E_FECHA_ALTA], [E_FECHA_BAJA]) 
                          VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8) SELECT @@Identity";

            object[] valorParametros = { empleado.Estado.Codigo, empleado.Sector.Codigo, empleado.Apellido, 
                                         empleado.Nombre, empleado.FechaNacimiento, empleado.Telefono, 
                                         empleado.Legajo, empleado.FechaAlta, DBNull.Value };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.Empleado empleado)
         {
            string sql = @"UPDATE EMPLEADOS SET EE_CODIGO = @p1, SEC_CODIGO = @p2, E_APELLIDO = @p3, 
                           E_NOMBRE = @p4, E_FECHANACIMIENTO = @p5, E_TELEFONO = @p6, E_LEGAJO = @p7
                          WHERE e_codigo = @p0";

            object[] valorParametros = { empleado.Codigo, 
                                         empleado.Estado.Codigo, empleado.Sector.Codigo, empleado.Apellido, 
                                         empleado.Nombre, empleado.FechaNacimiento, empleado.Telefono, 
                                         empleado.Legajo};
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        /// <summary>
        /// Obtiene todos los empleados sin filtrar, los carga en una DataTable del tipo de empleado.
        /// </summary>
        /// <param name="dtEmpleado">La tabla donde cargar losd datos.</param>
        public static void ObtenerEmpleados(DataTable dtEmpleado)
        {
            string sql = @"SELECT E_CODIGO, EE_CODIGO, SEC_CODIGO, E_APELLIDO, E_NOMBRE,
                           E_FECHANACIMIENTO, E_TELEFONO, E_LEGAJO, E_FECHA_ALTA, E_FECHA_BAJA 
                           FROM EMPLEADOS";

            DB.FillDataTable(dtEmpleado, sql, null);
        }

        public static void ObtenerEmpleados(Data.dsMantenimiento ds)
        {
            string sql = @"SELECT E_CODIGO, EE_CODIGO, SEC_CODIGO, E_APELLIDO, E_NOMBRE,
                           E_FECHANACIMIENTO, E_TELEFONO, E_LEGAJO, E_FECHA_ALTA, E_FECHA_BAJA 
                           FROM EMPLEADOS";

            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "EMPLEADOS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
