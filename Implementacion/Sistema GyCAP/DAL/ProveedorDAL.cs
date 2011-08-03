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
        //Metodo para la búsqueda de Proveedores
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

        //Metodo para guardar un proveedor nuevo
        public static int GuardarProveedor(Entidades.Proveedor proveedor, Data.dsProveedor dsProveedor)
        {
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                string sql = string.Empty;

                //Inserto la cabecera
                sql = "INSERT INTO [PROVEEDORES] ([sec_codigo], [prove_razonsocial], [prove_telprincipal], [prove_telalternativo])VALUES (@p0, @p1, @p2, @p3) SELECT @@Identity";
                
                if (proveedor.TelSecundario == string.Empty)
                {
                    object[] valoresParametros = { proveedor.Sector.Codigo, proveedor.RazonSocial, proveedor.TelPrincipal, DBNull.Value };
                    proveedor.Codigo = Convert.ToInt32(DB.executeScalar(sql, valoresParametros, transaccion));
                }
                else
                {
                    object[] valoresParametros = { proveedor.Sector.Codigo, proveedor.RazonSocial, proveedor.TelPrincipal, proveedor.TelSecundario };
                    proveedor.Codigo = Convert.ToInt32(DB.executeScalar(sql, valoresParametros, transaccion));
                }
                
                //Inserto el Detalle con los domicilios
                foreach (Data.dsProveedor.DOMICILIOSRow row in (Data.dsProveedor.DOMICILIOSRow[])dsProveedor.DOMICILIOS.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    sql = @"INSERT INTO [DOMICILIOS] ([loc_codigo], [prove_codigo], [dom_calle], [dom_numero], [dom_piso], [dom_departamento]) 
                           VALUES (@p0, @p1, @p2, @p3, @p4, @p5) SELECT @@Identity";
                                        
                    if (row.DOM_PISO == string.Empty && row.DOM_DEPARTAMENTO != string.Empty)
                    {
                        object[] valorParam = { row.LOC_CODIGO, proveedor.Codigo, row.DOM_CALLE, row.DOM_NUMERO, DBNull.Value, row.DOM_DEPARTAMENTO };
                        row.BeginEdit();
                        row.DOM_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                        row.PROVE_CODIGO = proveedor.Codigo;
                        row.EndEdit();  
                    }
                    if (row.DOM_DEPARTAMENTO == string.Empty && row.DOM_PISO != string.Empty)
                    {
                        object[] valorParam = { row.LOC_CODIGO, proveedor.Codigo, row.DOM_CALLE, row.DOM_NUMERO, row.DOM_PISO, DBNull.Value };
                        row.BeginEdit();
                        row.DOM_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                        row.PROVE_CODIGO = proveedor.Codigo;
                        row.EndEdit();  
                    }
                    if (row.DOM_DEPARTAMENTO == string.Empty && row.DOM_PISO == string.Empty)
                    {
                        object[] valorParam = { row.LOC_CODIGO, proveedor.Codigo, row.DOM_CALLE, row.DOM_NUMERO, DBNull.Value, DBNull.Value };
                        row.BeginEdit();
                        row.DOM_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                        row.PROVE_CODIGO = proveedor.Codigo;
                        row.EndEdit();
                    }
                    if (row.DOM_DEPARTAMENTO != string.Empty && row.DOM_PISO != string.Empty)
                    {
                        object[] valorParam = { row.LOC_CODIGO, proveedor.Codigo, row.DOM_CALLE, row.DOM_NUMERO, row.DOM_PISO, row.DOM_DEPARTAMENTO };
                        row.BeginEdit();
                        row.DOM_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                        row.PROVE_CODIGO = proveedor.Codigo;
                        row.EndEdit();
                    }
                                                            
                }

                transaccion.Commit();
                DB.FinalizarTransaccion();

                return proveedor.Codigo;
            }
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            } 
        }

        //Metodo para guardar un proveedor nuevo
        public static void ModificarProveedor(Entidades.Proveedor proveedor, Data.dsProveedor dsProveedor)
        {
            SqlTransaction transaccion = null;

            try
            {
                transaccion = DB.IniciarTransaccion();
                string sql = string.Empty;

                //Modifico la cabecera del Proveedor
                sql = @"UPDATE [PROVEEDORES] SET 
                            sec_codigo=@p0,
                            prove_razonsocial=@p1, 
                            prove_telprincipal=@p2, 
                            prove_telalternativo=@p3 
                        WHERE prove_codigo=@p4";

                if (proveedor.TelSecundario == string.Empty)
                {
                    object[] valoresParametros = { proveedor.Sector.Codigo, proveedor.RazonSocial, proveedor.TelPrincipal, DBNull.Value, proveedor.Codigo };
                    DB.executeNonQuery(sql, valoresParametros, transaccion);
                }
                else
                {
                    object[] valoresParametros = { proveedor.Sector.Codigo, proveedor.RazonSocial, proveedor.TelPrincipal, proveedor.TelSecundario, proveedor.Codigo};
                    DB.executeNonQuery(sql, valoresParametros, transaccion);
                }

                //Inserto los domicilios que hayan sido agregados
                foreach (Data.dsProveedor.DOMICILIOSRow row in (Data.dsProveedor.DOMICILIOSRow[])dsProveedor.DOMICILIOS.Select(null, null, System.Data.DataViewRowState.Added))
                {
                    sql = @"INSERT INTO [DOMICILIOS] ([loc_codigo], [prove_codigo], [dom_calle], [dom_numero], [dom_piso], [dom_departamento]) 
                           VALUES (@p0, @p1, @p2, @p3, @p4, @p5) SELECT @@Identity";

                    if (row.DOM_PISO == string.Empty && row.DOM_DEPARTAMENTO != string.Empty)
                    {
                        object[] valorParam = { row.LOC_CODIGO, proveedor.Codigo, row.DOM_CALLE, row.DOM_NUMERO, DBNull.Value, row.DOM_DEPARTAMENTO };
                        row.BeginEdit();
                        row.DOM_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                        row.PROVE_CODIGO = proveedor.Codigo;
                        row.EndEdit();
                    }
                    if (row.DOM_DEPARTAMENTO == string.Empty && row.DOM_PISO != string.Empty)
                    {
                        object[] valorParam = { row.LOC_CODIGO, proveedor.Codigo, row.DOM_CALLE, row.DOM_NUMERO, row.DOM_PISO, DBNull.Value };
                        row.BeginEdit();
                        row.DOM_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                        row.PROVE_CODIGO = proveedor.Codigo;
                        row.EndEdit();
                    }
                    if (row.DOM_DEPARTAMENTO == string.Empty && row.DOM_PISO == string.Empty)
                    {
                        object[] valorParam = { row.LOC_CODIGO, proveedor.Codigo, row.DOM_CALLE, row.DOM_NUMERO, DBNull.Value, DBNull.Value };
                        row.BeginEdit();
                        row.DOM_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                        row.PROVE_CODIGO = proveedor.Codigo;
                        row.EndEdit();
                    }
                    if (row.DOM_DEPARTAMENTO != string.Empty && row.DOM_PISO != string.Empty)
                    {
                        object[] valorParam = { row.LOC_CODIGO, proveedor.Codigo, row.DOM_CALLE, row.DOM_NUMERO, row.DOM_PISO, row.DOM_DEPARTAMENTO };
                        row.BeginEdit();
                        row.DOM_CODIGO = Convert.ToInt32(DB.executeScalar(sql, valorParam, transaccion));
                        row.PROVE_CODIGO = proveedor.Codigo;
                        row.EndEdit();
                    }

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

        //Metodo para eliminar un proveedor de la BD
        public static void EliminarProveedor(int codigoProveedor)
        {
            SqlTransaction transaccion = null;

            transaccion = DB.IniciarTransaccion();
            string sql = string.Empty;
            
            try
            {
                sql = "DELETE FROM PROVEEDORES WHERE prove_codigo = @p0";
                object[] valorParametros = { codigoProveedor };

                //Eliminamos los domicilios del proveedor
                DAL.DomicilioDAL.EliminarDomicilio(codigoProveedor, transaccion);

                //Eliminamos el proveedor
                DB.executeNonQuery(sql, valorParametros, transaccion);

                transaccion.Commit();
                DB.FinalizarTransaccion();
            }          
            catch (SqlException ex)
            {
                transaccion.Rollback();
                throw new Entidades.Excepciones.BaseDeDatosException(ex.Message);
            } 

        }

        public static bool EsProveedorActualizar(Entidades.Proveedor proveedor)
        {
            string sql = "SELECT count(prove_codigo) FROM PROVEEDORES WHERE prove_razonsocial = @p0 and prove_codigo <> @p1";
            object[] valoresParametros = { proveedor.RazonSocial, proveedor.Codigo };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valoresParametros, null)) != 0) { return false; }
                else { return true; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

        public static bool EsProveedorNuevo(Entidades.Proveedor proveedor)
        {
            string sql = "SELECT count(prove_codigo) FROM PROVEEDORES WHERE prove_razonsocial = @p0";
            object[] valoresParametros = { proveedor.RazonSocial };

            try
            {
                if (Convert.ToInt32(DB.executeScalar(sql, valoresParametros, null)) != 0) { return false; }
                else { return true; }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(ex.Message); }
        }

    }
}
