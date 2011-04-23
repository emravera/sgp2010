using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class EmpleadoDAL : UsuarioDAL
    {
        public static readonly int BuscarPorLegajo = 1;
        public static readonly int BuscarPorApellido = 2;
        public static readonly int BuscarPorNombre = 3;
        
        //BUSQUEDA
        //Metodo sobrecargado (3 Sobrecargas)
        //Busqueda por nombre
        /// <summary>
        /// Obtiene todos los empleados que coincidan con los filtros, incluye las capacidades
        /// </summary>
        /// <param name="buscarPor"></param>
        /// <param name="nombre"></param>
        /// <param name="estado"></param>
        /// <param name="sector"></param>
        /// <param name="ds"></param>
        public static void ObtenerEmpleado(object buscarPor, object nombre, object estado, object sector, Data.dsEmpleado ds)
        {
            string sql = @"SELECT E_CODIGO, EE_CODIGO, SEC_CODIGO, E_APELLIDO, E_NOMBRE,
                           E_FECHANACIMIENTO, E_LEGAJO, E_FECHA_ALTA, E_FECHA_BAJA 
                           FROM EMPLEADOS WHERE 1 = 1 ";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[3];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            
            // LEGAJO, NOMBRE O APELLIDO
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                if (Convert.ToInt32(buscarPor) == BuscarPorLegajo) { sql += " AND E_LEGAJO LIKE @p" + cantidadParametros; }
                else if (Convert.ToInt32(buscarPor) == BuscarPorApellido) { sql += " AND E_APELLIDO LIKE @p" + cantidadParametros; }
                else { sql += " AND E_NOMBRE LIKE @p" + cantidadParametros; }
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }

            //ESTADO - Revisamos si es distinto de 0, o sea "todos"
            if (estado != null && estado.GetType().Equals(typeof(int)))
            {
                sql += " AND EE_CODIGO = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(estado);
                cantidadParametros++;
            }

            if (sector != null && sector.GetType().Equals(typeof(int)))
            {
                sql += " AND SEC_CODIGO = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(estado);
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

                int[] codigos = new int[ds.EMPLEADOS.Count];
                int fila = 0;
                foreach (Data.dsEmpleado.EMPLEADOSRow row in ds.EMPLEADOS)
                {
                    codigos[fila] = Convert.ToInt32(row.E_CODIGO);
                    fila++;
                }
                DAL.CapacidadEmpleadoDAL.ObtenerCapacidadPorEmpleado(codigos, ds);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        //ELIMINACION
        //Metodo que verifica que no este usado en otro lugar
        public static bool PuedeEliminarse(long codigo)
        {
            string sqlESTR = "SELECT count(ESTR_CODIGO) FROM ESTRUCTURAS WHERE e_codigo = @p0";
            string sqlUSU = "SELECT count(U_CODIGO) FROM USUARIOS WHERE e_codigo = @p0";

            object[] valorParametros = { codigo };
            try
            {
                int resultadoESTR = Convert.ToInt32(DB.executeScalar(sqlESTR, valorParametros, null));
                int resultadoUSU = Convert.ToInt32(DB.executeScalar(sqlUSU, valorParametros, null));
                
                if (resultadoESTR == 0 && resultadoUSU == 0) { return true; }
                else { return false; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        //Metodo que elimina de la base de datos
        public static void Eliminar(long codigo)
        {
            string sql = "DELETE FROM EMPLEADOS WHERE E_codigo = @p0";
            object[] valorParametros = { codigo };
            SqlTransaction transaccion = null;
            try
            {
                transaccion = DB.IniciarTransaccion();
                CapacidadEmpleadoDAL.EliminarCapacidadesDeEmpleado(codigo, transaccion);
                DB.executeNonQuery(sql, valorParametros, transaccion);
                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
            finally { DB.FinalizarTransaccion(); }
        }

        //INSERTAR
        //Metodo que valida que no se intente guardar algo que ya esta en la BD
        public static bool esEmpleado(Entidades.Empleado empleado)
        {
            string sql = "SELECT count(e_codigo) FROM EMPLEADOS WHERE E_LEGAJO = @p0 AND e_codigo <> @p1";
            object[] valorParametros = { empleado.Legajo, empleado.Codigo };
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
        public static int Insertar(Data.dsEmpleado dsEmpleado)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = @"INSERT INTO [EMPLEADOS] 
                          ([EE_CODIGO], 
                           [SEC_CODIGO], 
                           [E_APELLIDO],
                           [E_NOMBRE], 
                           [E_FECHANACIMIENTO],
                           [E_LEGAJO],
                           [E_FECHA_ALTA], 
                           [E_FECHA_BAJA]) 
                          VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7) SELECT @@Identity";

            Data.dsEmpleado.EMPLEADOSRow row = dsEmpleado.EMPLEADOS.GetChanges(DataRowState.Added).Rows[0] as Data.dsEmpleado.EMPLEADOSRow;
            object fechaNac = DBNull.Value, fechaBaja = DBNull.Value;
            if (!row.IsE_FECHANACIMIENTONull()) { fechaNac = row.E_FECHANACIMIENTO.ToShortDateString(); }
            if (!row.IsE_FECHA_BAJANull()) { fechaBaja = row.E_FECHA_BAJA.ToShortDateString(); }
            object[] valorParametros = { 
                                           row.EE_CODIGO,
                                           row.SEC_CODIGO,
                                           row.E_APELLIDO,
                                           row.E_NOMBRE,
                                           fechaNac,
                                           row.E_LEGAJO,
                                           row.E_FECHA_ALTA.ToShortDateString(),
                                           fechaBaja
                                       };
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                row.BeginEdit();
                row.E_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParametros, transaccion));
                row.EndEdit();

                foreach (Data.dsEmpleado.CAPACIDADESXEMPLEADORow rowCxE in (Data.dsEmpleado.CAPACIDADESXEMPLEADORow[])dsEmpleado.CAPACIDADESXEMPLEADO.Select(null, null, DataViewRowState.Added))
                {
                    rowCxE.BeginEdit();
                    rowCxE.E_CODIGO = row.E_CODIGO;
                    rowCxE.EndEdit();
                    DAL.CapacidadEmpleadoDAL.InsertarCapacidadDeEmpleado(rowCxE, transaccion);
                }
                transaccion.Commit();
                return Convert.ToInt32(row.E_CODIGO);
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
            finally { DB.FinalizarTransaccion(); }
        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Data.dsEmpleado dsEmpleado)
         {
            string sql = @"UPDATE EMPLEADOS SET 
                            e_legajo = @p0, 
                            ee_codigo = @p1, 
                            sec_codigo = @p2, 
                            e_apellido = @p3, 
                            e_nombre = @p4, 
                            e_fechanacimiento = @p5,
                            e_fecha_baja = @p6 
                          WHERE e_codigo = @p7";

            //Si existe lanzamos la excepción correspondiente
            Data.dsEmpleado.EMPLEADOSRow row = dsEmpleado.EMPLEADOS.GetChanges(DataRowState.Modified).Rows[0] as Data.dsEmpleado.EMPLEADOSRow;
            object fecha = DBNull.Value, fechaBaja = DBNull.Value;
            if (!row.IsE_FECHANACIMIENTONull()) { fecha = row.E_FECHANACIMIENTO.ToShortDateString(); }
            if (!row.IsE_FECHA_BAJANull()) { fechaBaja = row.E_FECHA_BAJA.ToShortDateString(); }
            object[] valorParametros = { 
                                         row.E_LEGAJO,
                                         row.EE_CODIGO,
                                         row.SEC_CODIGO,
                                         row.E_APELLIDO,
                                         row.E_NOMBRE,
                                         fecha,
                                         fechaBaja,
                                         row.E_CODIGO
                                       };
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                DB.executeNonQuery(sql, valorParametros, transaccion);
                foreach (Data.dsEmpleado.CAPACIDADESXEMPLEADORow rowCxE in (Data.dsEmpleado.CAPACIDADESXEMPLEADORow[])dsEmpleado.CAPACIDADESXEMPLEADO.Select(null, null, DataViewRowState.Added))
                {
                    DAL.CapacidadEmpleadoDAL.InsertarCapacidadDeEmpleado(rowCxE, transaccion);
                }
                foreach (Data.dsEmpleado.CAPACIDADESXEMPLEADORow rowCxE in (Data.dsEmpleado.CAPACIDADESXEMPLEADORow[])dsEmpleado.CAPACIDADESXEMPLEADO.Select(null, null, DataViewRowState.Deleted))
                {
                    int cod = Convert.ToInt32(rowCxE["cxe_codigo", System.Data.DataRowVersion.Original]);
                    DAL.CapacidadEmpleadoDAL.EliminarCapacidadDeEmpleado(cod, transaccion);
                }
                transaccion.Commit();
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            }
            finally { DB.FinalizarTransaccion(); }
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

        /// <summary>
        /// Obtiene todos los empleados sin filtrar, no incluye capacidades
        /// </summary>
        /// <param name="ds"></param>
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
