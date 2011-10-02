using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using GyCAP.Entidades;
using GyCAP.Entidades.Enumeraciones;

namespace GyCAP.DAL
{
    public class TipoEntidadDAL
    {        
        public static void ObtenerTodos(Data.dsStock.TIPOS_ENTIDADDataTable table)
        {
            string sql = "SELECT tentd_codigo, tentd_nombre, tentd_descripcion FROM TIPOS_ENTIDAD";

            try
            {
                DB.FillDataTable(table, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }        
    }
}
