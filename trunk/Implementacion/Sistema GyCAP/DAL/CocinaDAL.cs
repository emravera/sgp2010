using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class CocinaDAL
    {
        //Obtiene todas las cocinas, sin filtrar
        public static void ObtenerCocinas(DataTable dtCocina)
        {
            string sql = @"SELECT coc_codigo, ecoc_codigo, col_codigo, mod_codigo, mca_codigo, te_codigo, desig_codigo, 
                         coc_codigo_producto, coc_cantidadstock, coc_activo, coc_precio FROM COCINAS";

            DB.FillDataTable(dtCocina, sql, null);
        }
    }
}
