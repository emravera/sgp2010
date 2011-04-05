using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class DomicilioDAL
    {

        public static void ObtenerDomicilios(int codigoProveedor, DataTable dtDomicilios)
        {
            string sql = @"SELECT dom.dom_codigo, dom.loc_codigo, dom.prove_codigo, dom.dom_calle, dom.dom_numero, dom.dom_piso, dom.dom_departamento 
                           FROM DOMICILIOS as dom, LOCALIDADES as loc, PROVEEDORES as pro 
                           WHERE dom.prove_codigo=@p0 and dom.loc_codigo=loc.loc_codigo and pro.prove_codigo=dom.prove_codigo";

            object[] Parametros = { codigoProveedor };

            try
            {
                DB.FillDataTable(dtDomicilios, sql, Parametros);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

    }
}
