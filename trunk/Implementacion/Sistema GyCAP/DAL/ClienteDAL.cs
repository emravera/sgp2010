using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class ClienteDAL
    {
        //BUSQUEDA
        //Trae todos los elementos
        public static void ObtenerCliente(Data.dsMarca ds)
        {
            string sql = "SELECT cli_codigo, cli_razonsocial, cli_telefono, cli_fechaalta, cli_fechabaja, cli_motivobaja FROM CLIENTES";
            try
            {
                DB.FillDataSet(ds, "CLIENTES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
   

    }
}
