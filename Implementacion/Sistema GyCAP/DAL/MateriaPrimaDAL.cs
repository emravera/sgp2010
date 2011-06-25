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
        //*****************************************************************************************
        //                              BUSQUEDA DE MATERIAS PRIMAS
        //*****************************************************************************************

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

        //*****************************************************************************************
        //                              INSERTAR MATERIAS PRIMAS
        //*****************************************************************************************
        
        //Metodo para insertar elemento en la Base de Datos
        public static int Insertar(Entidades.MateriaPrima materiaPrima)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = @"INSERT INTO [MATERIAS_PRIMAS] 
                           ([mp_nombre], [mp_descripcion], [umed_codigo], [mp_costo], [ustck_numero], [mp_esprincipal], [mp_cantidad]) 
                           VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6) SELECT @@Identity";
            object[] valorParametros = { materiaPrima.Nombre, materiaPrima.Descripcion,  
                                         materiaPrima.CodigoUnidadMedida, materiaPrima.Costo,
                                         materiaPrima.UbicacionStock.Numero, materiaPrima.EsPrincipal,
                                         materiaPrima.Cantidad};
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        //Metodo que valida que no se quiera insertar algo que ya existe
        public static bool EsMateriaPrima(Entidades.MateriaPrima materiaPrima)
        {
            string sql = "SELECT count(mp_codigo) FROM MATERIAS_PRIMAS WHERE mp_nombre = @p0";


            object[] valorParametros = { materiaPrima.Nombre };
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
        //*****************************************************************************************
        //                              MODIFICAR MATERIAS PRIMAS
        //*****************************************************************************************

        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.MateriaPrima materiaPrima)
        {
            string sql = @"UPDATE MATERIAS_PRIMAS 
                           SET mp_nombre = @p0, mp_descripcion = @p1, umed_codigo = @p2, mp_costo = @p3,
                           ustck_numero = @p4, mp_esprincipal = @p5, mp_cantidad = @p6
                           WHERE mp_codigo = @p7";
            object[] valorParametros = { materiaPrima.Nombre, materiaPrima.Descripcion,
                                         materiaPrima.CodigoUnidadMedida, materiaPrima.Costo,
                                         materiaPrima.UbicacionStock.Numero, materiaPrima.EsPrincipal,
                                         materiaPrima.Cantidad, materiaPrima.CodigoMateriaPrima };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo que valida que no se modifique algo con referencias en hoja de ruta
        public static bool EsMPHojaRuta(Entidades.MateriaPrima materiaPrima)
        {
            string sql = "SELECT count(ustck_origen) FROM DETALLE_HOJARUTA WHERE ustck_origen = @p0";


            object[] valorParametros = { materiaPrima.UbicacionStock };
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

        //*****************************************************************************************
        //                              ELIMINAR MATERIAS PRIMAS
        //*****************************************************************************************

        //Metodo que elimina de la base de datos
        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM MATERIAS_PRIMAS WHERE mp_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        //Metodo para validar que la materia prima no este asignada
        public static bool ValidarEliminar(int codigo)
        {
            //Verificamos que no haya materia primas en la estructura
            string sql = "SELECT count(*) FROM COMPUESTOS_PARTES WHERE mp_codigo = @p0";
            
            object[] valorParametros = { codigo };
            
            int mp_estructura = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));

            //Validamos que o haya materias primas en el plan de materias primas
            sql = "SELECT count(*) FROM DETALLE_PLAN_MATERIAS_PRIMAS_ANUAL WHERE mp_codigo = @p0";

             int mp_plan = Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
  
            try
            {
                if ( mp_estructura == 0 && mp_plan == 0)
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
    }
}
