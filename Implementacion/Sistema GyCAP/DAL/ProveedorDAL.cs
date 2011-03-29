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
            string sql = @"SELECT pro.prove_codigo, pro.sec_codigo, pro.prove_razonsocial, pro.prove_telprincipal, pro.prove_telalternativo
                           FROM PROVEEDORES as pro WHERE 1=1";
            
            //Sirve para armar el nombre de los parámetros
            int cantidadParametros = 0;
            //Un array de object para ir guardando los valores de los filtros, con tamaño = cantidad de filtros disponibles
            object[] valoresFiltros = new object[2];
            //Empecemos a armar la consulta, revisemos que filtros aplican
            if (codigoSector != null && razonSocial != string.Empty)
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
    }
}
