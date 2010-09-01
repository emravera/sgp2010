using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class ClienteDAL
    {
        //BUSQUEDA
        //Trae todos los elementos
        public static void ObtenerCliente(Data.dsMarca ds)
        {
            string sql = @"SELECT cli_codigo, cli_razonsocial, cli_telefono, cli_fechaalta, 
                           cli_fechabaja, cli_motivobaja, CLI_MAIL, CLI_ESTADO 
                           FROM CLIENTES 
                           WHERE CLI_ESTADO = 'A' ";
            try
            {
                DB.FillDataSet(ds, "CLIENTES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //BUSQUEDA
        //Metodo sobrecargado (3 Sobrecargas)
        //Busqueda por nombre
        public static void ObtenerCliente(object razonSocial, string estadoCliente, Data.dsCliente ds)
        {
            string sql = @"SELECT CLI_CODIGO, CLI_RAZONSOCIAL, CLI_TELEFONO, CLI_FECHAALTA,
                           CLI_FECHABAJA, CLI_MOTIVOBAJA, CLI_MAIL, CLI_ESTADO
                           FROM CLIENTES
                           WHERE 1 = 1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[2];
            //Empecemos a armar la consulta, revisemos que filtros aplican
    
            // RAZON SOCIAL
            if (razonSocial != null && razonSocial.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND CLI_RAZONSOCIAL LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                razonSocial = "%" + razonSocial + "%";
                valoresFiltros[cantidadParametros] = razonSocial;
                cantidadParametros++;
            }
  

            //ESTADO - Revisamos si es distinto de 0, o sea "todos"
            if (estadoCliente != string.Empty)
            {
                sql += " AND CLI_ESTADO = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = estadoCliente;
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
                    DB.FillDataSet(ds, "CLIENTES", sql, valorParametros);
                }
                else
                {
                    //Buscamos sin filtro
                    DB.FillDataSet(ds, "CLIENTES", sql, null);
                }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        /// <summary>
        /// Obtiene todos los empleados sin filtrar, los carga en una DataTable del tipo de empleado.
        /// </summary>
        /// <param name="dtEmpleado">La tabla donde cargar losd datos.</param>
        public static void ObtenerCliente(DataTable dtCliente)
        {
            string sql = @"SELECT cli_codigo, cli_razonsocial, cli_telefono, cli_fechaalta, 
                           cli_fechabaja, cli_motivobaja, CLI_MAIL, CLI_ESTADO 
                           FROM CLIENTES 
                           WHERE CLI_ESTADO = 'A' ";

            DB.FillDataTable(dtCliente, sql, null);
        }


        //ELIMINACION
        //Metodo que verifica que no este usado en otro lugar
        public static bool PuedeEliminarse(long codigo)
        {
            string sqlMCA = "SELECT count(MCA_CODIGO) FROM MARCAS  WHERE cli_codigo = @p0";
            string sqlPED = "SELECT count(PED_NUMERO) FROM PEDIDOS WHERE cli_codigo = @p0";

            object[] valorParametros = { codigo };
            try
            {
                int resultadoMCA = Convert.ToInt32(DB.executeScalar(sqlMCA, valorParametros, null));
                int resultadoPED = Convert.ToInt32(DB.executeScalar(sqlPED, valorParametros, null));
                if (resultadoMCA == 0 && resultadoPED == 0) { return true; }
                else { return false; }
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo que elimina de la base de datos
        public static void Eliminar(long codigo)
        {
            string sql = "DELETE FROM CLIENTES WHERE CLI_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //INSERTAR
        //Metodo que valida que no se intente guardar algo que ya esta en la BD
        public static bool esCliente(Entidades.Cliente cliente)
        {
            string sql = "SELECT count(cli_codigo) FROM CLIENTES WHERE cli_codigo = @p0";
            object[] valorParametros = { cliente.Codigo };
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
        public static int Insertar(Entidades.Cliente cliente)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = @"INSERT INTO [CLIENTES] ([CLI_RAZONSOCIAL], [CLI_TELEFONO], [CLI_MOTIVOBAJA],
                           [CLI_MAIL], [CLI_ESTADO], [CLI_FECHAALTA], [CLI_FECHABAJA]) 
                          VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, ) SELECT @@Identity";

            object[] valorParametros = { cliente.RazonSocial, cliente.Telefono, cliente.MotivoBaja, 
                                         cliente.Mail, cliente.Estado, cliente.FechaAlta, DBNull.Value };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.Cliente cliente)
        {
            string sql = @"UPDATE CLIENTES SET CLI_RAZONSOCIAL = @p1, CLI_TELEFONO = @p2, CLI_MOTIVOBAJA = @p3, 
                           CLI_MAIL = @p4, CLI_ESTADO = @p5, CLI_FECHABAJA = @p6
                          WHERE cli_codigo = @p0";

            object[] valorParametros = { cliente.Codigo, 
                                         cliente.RazonSocial, cliente.Telefono, cliente.MotivoBaja, 
                                         cliente.Mail, cliente.Estado, DBNull.Value };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
