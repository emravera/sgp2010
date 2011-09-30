using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GyCAP.DAL
{
    public class MenuUsuarioDAL
    {
        public static int Insertar(Entidades.MenuUsuario mu)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO MENU_USUARIOS (U_CODIGO, MNU_CODIGO) VALUES (@p0,@p1) SELECT @@Identity";
            object[] valorParametros = { mu.Usuario.Codigo , mu.Menu.Codigo};
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM MENU_USUARIOS WHERE U_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql,  valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerTodos(Data.dsSeguridad ds)
        {
            string sql = @"SELECT *
                           FROM MENU_USUARIOS";
            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "MENU_USUARIOS", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void ObtenerTodos(int codUsuario, Data.dsSeguridad ds)
        {
            string sql = @"SELECT *
                             FROM MENU_USUARIOS
                            WHERE U_CODIGO = @p0 ";
            object[] valorParametros = { codUsuario };

            try
            {
                //Se llena el Dataset
                DB.FillDataSet(ds, "MENU_USUARIOS", sql, valorParametros);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
