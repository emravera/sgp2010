using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace GyCAP.DAL
{
    public class ProveedorDAL
    {

        public static void ObtenerProveedores(object razonSocial, object codigoSector, DataTable dtProveedor)
        {        
            string sql = @"SELECT prove_codigo, sec_codigo, prove_razonsocial, prove_telprincipal, prove_telalternativo
                           FROM PROVEEDORES WHERE 1=1";
            
            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[2];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (codigoSector != null && razonSocial.ToString() != string.Empty)
            {
                //Si aplica el filtro lo usamos
                sql += " AND pro.prove_razonsocial LIKE @p" + cantidadParametros;
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                razonSocial = "%" + razonSocial + "%";
                valoresFiltros[cantidadParametros] = razonSocial;
                cantidadParametros++;
            }
            //Revisamos si pasó algun valor y si es un integer
            if (codigoSector != null && codigoSector.GetType() == cantidadParametros.GetType())
            {
                sql += " AND pro.sec_codigo = @p" + cantidadParametros;
                valoresFiltros[cantidadParametros] = Convert.ToInt32(codigoSector);
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
                    DB.FillDataTable(dtProveedor, sql, valorParametros);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }
            else
            {
                //Buscamos sin filtro
                try
                {
                    DB.FillDataTable(dtProveedor, sql, null);
                }
                catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            }      
        
        }

        public static void GuardarProveedor(Entidades.Proveedor proveedor, Data.dsProveedor dsProveedor)
        {
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                string sql = string.Empty;
                
                //Inserto la cabexera con el proveedor
                if (proveedor.TelSecundario == null)
                {
                    proveedor.TelSecundario =Convert.ToString(0);
                }
                    sql = "INSERT INTO [PROVEEDORES] ([sec_codigo], [prove_razonsocial], [prove_telprincipal], [prove_telalternativo])VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";
                    object[] valoresParametros = { proveedor.Sector.Codigo, proveedor.RazonSocial, proveedor.TelPrincipal, proveedor.TelSecundario };
                    proveedor.Codigo = Convert.ToInt32(DB.executeScalar(sql, valoresParametros, transaccion));
                
                //Inserto el Detalle con los domicilios
                foreach (Data.dsProveedor.DOMICILIOSRow row in (Data.dsProveedor.DOMICILIOSRow[])dsProveedor.DOMICILIOS.Select(null, null, System.Data.DataViewRowState.Added))
                {
                                        
                    if (row.DOM_PISO == null)
                    {
                        row.BeginEdit();
                        row.DOM_PISO =Convert.ToString(0);
                        row.EndEdit();
                    }
                    if (row.DOM_DEPARTAMENTO == null)
                    {
                        row.BeginEdit();
                        row.DOM_DEPARTAMENTO =Convert.ToString(0);
                        row.EndEdit();
                    }
                    sql = @"INSERT INTO [DOMICILIOS] ([loc_codigo], [prove_codigo], [dom_calle], [dom_numero], [dom_piso], [dom_departamento]) 
                           VALUES (@p0, @p1, @p2, @p3, @p4, @p5) SELECT @@Identity";
                        object[] valorParam = { row.LOC_CODIGO, proveedor.Codigo, row.DOM_CALLE, row.DOM_NUMERO, row.DOM_PISO, row.DOM_DEPARTAMENTO };
                        row.BeginEdit();
                        row.DOM_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                        row.PROVE_CODIGO = proveedor.Codigo;
                        row.EndEdit();                                        
                }

                transaccion.Commit();
                DB.FinalizarTransaccion();
                
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            } 
        }

    }
}
