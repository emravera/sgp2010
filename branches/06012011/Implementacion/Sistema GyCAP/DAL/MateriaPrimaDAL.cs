using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class MateriaPrimaDAL
    {
        /// <summary>
        /// Obtiene una materia prima por su código.
        /// </summary>
        /// <param name="codigoMateriaPrima">El código de la materia prima deseada.</param>
        /// <returns>El objeto MateriaPrima con sus datos.</returns>
        /// <exception cref="ElementoInexistenteException">En caso de que no exista la materia prima.</exception>
        /// <exception cref="BaseDeDatosException">En caso de problemas con la base de datos.</exception>
        public static Entidades.MateriaPrima ObtenerMateriaPrima(int codigoMateriaPrima)
        {
            string sql = @"SELECT mp_nombre, umed_codigo, mp_descripcion, mp_costo, ustck_numero 
                        FROM MATERIAS_PRIMAS WHERE mp_codigo = @p0";
            object[] valorParametros = { codigoMateriaPrima };
            SqlDataReader rdr = DB.GetReader(sql, valorParametros, null);
            Entidades.MateriaPrima materiaPrima = new GyCAP.Entidades.MateriaPrima();
            try
            {
                if (!rdr.HasRows) { throw new Entidades.Excepciones.ElementoInexistenteException(); }
                rdr.Read();
                materiaPrima.CodigoMateriaPrima = codigoMateriaPrima;
                materiaPrima.Nombre = rdr["mp_nombre"].ToString();
                materiaPrima.CodigoUnidadMedida = Convert.ToInt32(rdr["umed_codigo"].ToString());
                materiaPrima.Descripcion = rdr["mp_descripcion"].ToString();
                materiaPrima.Costo = Convert.ToDecimal(rdr["mp_costo"].ToString());
                materiaPrima.UbicacionStock = new GyCAP.Entidades.UbicacionStock(Convert.ToInt32(rdr["ustck_numero"].ToString()));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            finally
            {
                if (rdr != null) { rdr.Close(); }
                DB.CloseReader();
            }
            return materiaPrima;
        }
        
        //Metodo que obtiene todas las materias primas
        public static void ObtenerTodos(Data.dsMateriaPrima ds)
        {
            string sql = @"SELECT mp_codigo, mp_nombre, umed_codigo, mp_descripcion, mp_costo, ustck_numero 
                        FROM MATERIAS_PRIMAS";
            try
            {
                DB.FillDataSet(ds, "MATERIAS_PRIMAS", sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }

        }

        //Metodo que obtiene todas las materias primas
        public static void ObtenerMP(Data.dsPlanMateriasPrimas ds)
        {
            string sql = @"SELECT mp_codigo, mp_nombre, umed_codigo, mp_descripcion, mp_costo, ustck_numero 
                        FROM MATERIAS_PRIMAS";
            try
            {
                DB.FillDataSet(ds, "MATERIAS_PRIMAS", sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }

        }
        
        //Metodo que obtiene todas las materias primas (con datatable)
        public static void ObtenerMP(DataTable dtMateriasPrimas)
        {
            string sql = @"SELECT mp_codigo, mp_nombre, umed_codigo, mp_descripcion, mp_costo
                        FROM MATERIAS_PRIMAS";
            try
            {
                DB.FillDataTable(dtMateriasPrimas, sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        public static void ObtenerTodos(System.Data.DataTable dt)
        {
            string sql = @"SELECT mp_codigo, mp_nombre, umed_codigo, mp_descripcion, mp_costo, ustck_numero 
                        FROM MATERIAS_PRIMAS";
            try
            {
                DB.FillDataTable(dt, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }

        }

        public static void ObtenerMateriaPrima(int codigoMP, Data.dsEstructura ds)
        {
            string sql = @"SELECT mp_codigo, mp_nombre, umed_codigo, mp_descripcion, mp_costo, ustck_numero 
                        FROM MATERIAS_PRIMAS WHERE mp_codigo = @p0";
            object[] valoresParametros = { codigoMP };
            
            try
            {
                DB.FillDataTable(ds.MATERIAS_PRIMAS, sql, valoresParametros);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        //Metodo que obtiene el precio de una materia prima
        public static decimal ObtenerPrecioMP(decimal codigoMP)
        {
            decimal costo = 0;

            string sql = @"SELECT  mp_costo FROM MATERIAS_PRIMAS where mp_codigo = @p0";
          
            object[] valoresParametros = { codigoMP };
            try
            {
                costo = Convert.ToDecimal(DB.executeScalar(sql, valoresParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }

            return costo;
        }

        //Metodo de Busqueda de Materias Primas desde el ABM de Materias Primas
        public static void ObtenerMP(string nombre, int esPrincipal, DataTable dtMateriasPrimas)
        {
            string sql = @"SELECT mp_codigo, umed_codigo, mp_nombre, mp_descripcion, mp_costo,
                           ustck_numero, mp_esprincipal, mp_cantidad FROM MATERIAS_PRIMAS WHERE 1=1";

            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[2];
            
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (nombre != null && nombre.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND mp_nombre LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                valoresFiltros[cantidadParametros] = nombre;
                cantidadParametros++;
            }
            
            //Revisamos si pasó algun valor y si es un integer
            if (esPrincipal != null && esPrincipal != 3 )
            {
                sql += " AND mp_esprincipal = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(esPrincipal);
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
                    DB.FillDataTable(dtMateriasPrimas, sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
            else
            {
                //Buscamos sin filtro
                try
                {
                    DB.FillDataTable(dtMateriasPrimas, sql, null);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
            }
        }
    }
}
