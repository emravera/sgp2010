using System;
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
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
            
        }

        //Metodo que valida que no se quiera insertar algo que ya existe
        public static bool EsMateriaPrima(Entidades.MateriaPrimaPrincipal materiaPrima)
        {
            string sql = "SELECT count(mppr_codigo) FROM MATERIASPRIMASPRINCIPALES WHERE mp_codigo = @p0";


            object[] valorParametros = { materiaPrima.MateriaPrima.CodigoMateriaPrima };
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
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }
        //Metodo que valida que no se quiera modificar algo que ya existe
        public static bool ModificarMateriaPrima(Entidades.MateriaPrimaPrincipal materiaPrima)
        {
            string sql = "SELECT count(mppr_codigo) FROM MATERIASPRIMASPRINCIPALES WHERE mp_codigo = @p0 and mppr_cantidad=@p1";

            object[] valorParametros = { materiaPrima.MateriaPrima.CodigoMateriaPrima, materiaPrima.Cantidad };
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
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }


        //Metodo que trae todos los datos 
        public static void ObtenerTodos(Data.dsMateriaPrima ds)
        {
            string sql = @"SELECT pr.mppr_codigo, pr.mp_codigo, pr.mppr_cantidad, mp.umed_codigo
                        FROM MATERIASPRIMASPRINCIPALES as pr, MATERIAS_PRIMAS as mp
                        WHERE mp.mp_codigo=pr.mp_codigo";
            try
            {
                DB.FillDataSet(ds, "MATERIASPRIMASPRINCIPALES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        //Metodo que trae todos los datos 
        public static void ObtenerMPPrincipales(Data.dsPlanMateriasPrimas ds)
        {
            string sql = @"SELECT pr.mppr_codigo, pr.mp_codigo, pr.mppr_cantidad, mp.umed_codigo
                        FROM MATERIASPRIMASPRINCIPALES as pr, MATERIAS_PRIMAS as mp
                        WHERE mp.mp_codigo=pr.mp_codigo";
            try
            {
                DB.FillDataSet(ds, "MATERIASPRIMASPRINCIPALES", sql, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        //Metodo que elimina de la base de datos
        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM MATERIASPRIMASPRINCIPALES WHERE mppr_codigo = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        //MODIFICAR 
        //Metodo que modifica en la base de datos
        public static void Actualizar(Entidades.MateriaPrimaPrincipal materiaPrima)
        {
            string sql = @"UPDATE MATERIASPRIMASPRINCIPALES SET mp_codigo = @p0, mppr_cantidad = @p1
                         WHERE mppr_codigo = @p2";
            object[] valorParametros = { materiaPrima.MateriaPrima.CodigoMateriaPrima, materiaPrima.Cantidad, materiaPrima.Codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

    }
}
