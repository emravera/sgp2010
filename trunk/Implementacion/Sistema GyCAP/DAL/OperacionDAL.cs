using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class OperacionDAL
    {
        public static void ObtenerOperaciones(DataTable dtOperaciones)
        {
            string sql = "SELECT opr_numero, opr_codigo, opr_nombre, opr_descripcion, opr_horasrequerida FROM OPERACIONES";
            
            try
            {
                DB.FillDataTable(dtOperaciones, sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
        
        public static void ObtenerOperacion(int numeroOperacion, Data.dsHojaRuta ds)
        {
            string sql = "SELECT opr_numero, opr_codigo, opr_nombre, opr_descripcion, opr_horasrequerida FROM OPERACIONES WHERE opr_numero = @p0";
            object[] valoresParametros = { numeroOperacion };

            try
            {
                DB.FillDataTable(ds.OPERACIONES, sql, valoresParametros);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }
    }
}
