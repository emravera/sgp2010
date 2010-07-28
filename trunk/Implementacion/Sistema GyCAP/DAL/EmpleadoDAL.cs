using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class EmpleadoDAL : UsuarioDAL
    {
        //BUSQUEDA
        //Metodo sobrecargado (3 Sobrecargas)
        //Busqueda por nombre
        public static void ObtenerEmpleado(string buscarPor, object nombre, int idEstadoEmpleado, Data.dsEmpleado ds)
        {
            string sql = @"SELECT E_CODIGO, EE_CODIGO, SEC_CODIGO, E_APELLIDO, E_NOMBRE,
                           E_FECHANACIMIENTO, E_TELEFONO, E_LEGAJO, E_FECHA_ALTA, E_FECHA_BAJA 
                           FROM EMPLEADOS
                           WHERE 1 = 1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[2];
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }


        //ELIMINACION
        //Metodo que verifica que no este usado en otro lugar
        public static bool PuedeEliminarse(long codigo)
        {
            string sql = "SELECT count(E_codigo) FROM xxx WHERE E_codigo = @p0";
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.Empleado empleado)
        {
            string sql = @"UPDATE EMPLEADOS SET EE_CODIGO = @p1, SEC_CODIGO = @p2, E_APELLIDO = @p3
                           E_NOMBRE = @p4, E_FECHANACIMIENTO = @p5, E_TELEFONO = @p6
                           E_LEGAJO = @p7, E_FECHA_ALTA = @p8, E_FECHA_BAJA = @p9
                         WHERE e_codigo = @p0";

            object[] valorParametros = { empleado.Codigo, 
                                         empleado.Estado.Codigo, empleado.Sector.Codigo, empleado.Apellido, 
                                         empleado.Nombre, empleado.FechaNacimiento, empleado.Telefono, 
                                         empleado.Legajo, empleado.FechaAlta, empleado.FechaBaja };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
