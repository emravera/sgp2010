﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class MateriaPrimaPrincipalDAL
    {
        //Metodo para insertar elemento en la Base de Datos
        public static int Insertar(Entidades.MateriaPrimaPrincipal materiaPrima)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO [MATERIASPRIMASPRINCIPALES] ([mp_codigo], [mppr_cantidad]) VALUES (@p0, @p1) SELECT @@Identity";
            object[] valorParametros = { materiaPrima.MateriaPrima.CodigoMateriaPrima, materiaPrima.Cantidad };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            
        }

        //Metodo que valida que no se quiera insertar algo que ya existe
        public static bool EsMateriaPrima(Entidades.MateriaPrimaPrincipal materiaPrima)
        {
            string sql = "SELECT count(mppr_codigo) FROM MATERIASPRIMASPRINCIPALES WHERE mppr_nombre = @p0";
            object[] valorParametros = { materiaPrima.Codigo };
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
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }
    }
}