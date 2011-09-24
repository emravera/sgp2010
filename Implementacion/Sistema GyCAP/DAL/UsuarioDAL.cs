using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public abstract class UsuarioDAL
    {
        public static readonly int EstadoActivo = 1;
        public static readonly int EstadoInactivo = 2;

        //BUSQUEDA
        //Trae todos los elementos
        public static void ObtenerTodos(Data.dsSeguridad ds)
        {
            string sql = @"SELECT * 
                             FROM USUARIOS
                            WHERE EU_CODIGO = " + EstadoActivo;
            try
            {
                DB.FillDataSet(ds, "USUARIOS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //BUSQUEDA
        //Metodo sobrecargado (3 Sobrecargas)
        //Busqueda por nombre
        public static void ObtenerTodos(object nombre, int estado, Data.dsSeguridad ds)
        {
            string sql = @"SELECT *
                             FROM USUARIOS
                            WHERE 1 = 1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[2];
            //Empecemos a armar la consulta, revisemos que filtros aplican

            // RAZON SOCIAL
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND U_NOMBRE LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }


            //ESTADO - Revisamos si es distinto de 0, o sea "todos"
            if (estado != -1 )
            {
                sql += " AND EU_CODIGO = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = estado;
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
                    DB.FillDataSet(ds, "USUARIOS", sql, valorParametros);
                }
                else
                {
                    //Buscamos sin filtro
                    DB.FillDataSet(ds, "USUARIOS", sql, null);
                }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        /// <summary>
        /// Obtiene todos los empleados sin filtrar, los carga en una DataTable del tipo de empleado.
        /// </summary>
        /// <param name="dtEmpleado">La tabla donde cargar losd datos.</param>
        public static void ObtenerTodos(DataTable dtUsuario)
        {
            string sql = @"SELECT *
                             FROM USUARIOS 
                            WHERE EU_CODIGO = " + EstadoActivo ;

            DB.FillDataTable(dtUsuario, sql, null);
        }


        //ELIMINACION
        //Metodo que verifica que no este usado en otro lugar
        public static bool PuedeEliminarse(long codigo)
        {
            string sql = "SELECT count(MEU_CODIGO) FROM MENU_USUARIOS  WHERE U_CODIGO = @p0";

            object[] valorParametros = { codigo };
            try
            {
                int resultado = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
                if (resultado == 0 ) { return true; }
                else { return false; }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo que elimina de la base de datos
        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM USUARIOS WHERE u_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //INSERTAR
        //Metodo que valida que no se intente guardar algo que ya esta en la BD
        public static bool esUsuario(Entidades.Usuario usuario)
        {
            string sql = "SELECT count(u_codigo) FROM USUARIOS WHERE u_usuario = @p0 AND U_CODIGO <> @p1 " ;
            object[] valorParametros = { usuario.Login, usuario.Codigo };
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
        public static int Insertar(Entidades.Usuario usuario)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = @"INSERT INTO USUARIOS (EU_CODIGO, ROL_CODIGO, U_USUARIO,
                           U_PASSWORD, U_NOMBRE, U_MAIL) 
                          VALUES (@p0, @p1, @p2, @p3, @p4, @p5 ) SELECT @@Identity";

            object[] valorParametros = { usuario.Estado.Codigo, usuario.Rol.Codigo, usuario.Login, 
                                         usuario.Password, usuario.Nombre, usuario.Mail};
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.Usuario usuario)
        {
            string sql = @"UPDATE USUARIOS SET EU_CODIGO = @p1, ROL_CODIGO = @p2, U_USUARIO = @p3, 
                           U_PASSWORD = @p4, U_NOMBRE = @p5, U_MAIL = @p6
                          WHERE u_codigo = @p0";

            object[] valorParametros = { usuario.Codigo, 
                                         usuario.Estado.Codigo, usuario.Rol.Codigo, usuario.Login, 
                                         usuario.Password, usuario.Nombre, usuario.Mail };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
