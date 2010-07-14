using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.DAL
{
    public class MarcaDAL
    {
        //BUSQUEDA
        //Metodo sobrecargado (3 Sobrecargas)
        //Busqueda por nombre
        public static void ObtenerMarca(string nombre, Data.dsMarca ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT mca_codigo, cli_codigo, mca_nombre
                              FROM MARCAS
                              WHERE mca_nombre LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "MARCAS", sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
        }
        //Trae todos los elementos
        public static void ObtenerMarca(Data.dsUnidadMedida ds)
        {
            string sql = "SELECT mca_codigo, cli_codigo, mca_nombre FROM MARCAS";
            try
            {
                DB.FillDataSet(ds, "MARCAS", sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
        //Busqueda por cliente
        public static void ObtenerMarca(int idCliente, Data.dsUnidadMedida ds)
        {
            string sql = @"SELECT mca_codigo, cli_codigo, mca_nombre
                              FROM MARCAS
                              WHERE cli_codigo=@p0";

            object[] valorParametros = { idCliente };
            try
            {
                DB.FillDataSet(ds, "MARCAS", sql, valorParametros);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }


    }
}
