using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GyCAP.DAL
{
    public class FabricanteMaquinaDAL
    {
        public static int Insertar(Entidades.FabricanteMaquina fabricanteMaquina)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO FABRICANTE_MAQUINAS (FAB_RAZONSOCIAL) VALUES (@p0) SELECT @@Identity";
            object[] valorParametros = { fabricanteMaquina.RazonSocial };
            try
            {
                return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Eliminar(int codigo)
        {
            string sql = "DELETE FROM FABRICANTE_MAQUINAS WHERE FAB_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static void Actualizar(Entidades.FabricanteMaquina fabricanteMaquina)
        {
            //Actualizar
            string sql = "UPDATE FABRICANTE_MAQUINAS SET FAB_RAZONSOCIAL = @p1 WHERE FAB_CODIGO = @p0";
            object[] valorParametros = { fabricanteMaquina.Codigo, fabricanteMaquina.RazonSocial };
            try
            {
                DB.executeNonQuery(sql, valorParametros, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }

        public static bool EsFabricanteMaquina(Entidades.FabricanteMaquina fabricanteMaquina)
        {
            string sql = "SELECT count(FAB_CODIGO) FROM FABRICANTE_MAQUINAS WHERE FAB_RAZONSOCIAL = @p0";
            object[] valorParametros = { fabricanteMaquina.RazonSocial  };
            try
            {
                if (Convert.ToInt64(DB.executeScalar(sql, valorParametros, null)) == 0)
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

        public static void ObtenerFabricanteMaquina(string nombre, Data.dsFabricanteMaquina ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT FAB_CODIGO, FAB_RAZONSOCIAL
                               FROM FABRICANTE_MAQUINAS 
                               WHERE FAB_RAZONSOCIAL LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                try
                {
                    DB.FillDataSet(ds, "FABRICANTE_MAQUINAS", sql, valorParametros);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
            else
            {
                string sql = "SELECT FAB_CODIGO, FAB_RAZONSOCIAL FROM FABRICANTE_MAQUINAS";
                try
                {
                    DB.FillDataSet(ds, "FABRICANTE_MAQUINAS", sql, null);
                }
                catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }

            }
        }

        public static void ObtenerFabricanteMaquina(Data.dsFabricanteMaquina ds)
        {
            string sql = "SELECT FAB_CODIGO, FAB_RAZONSOCIAL FROM FABRICANTE_MAQUINAS";

            try
            {
                DB.FillDataSet(ds, "FABRICANTE_MAQUINAS", sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        public static void ObtenerFabricanteMaquina(Data.dsMaquina ds)
        {
            string sql = "SELECT FAB_CODIGO, FAB_RAZONSOCIAL FROM FABRICANTE_MAQUINAS";

            try
            {
                DB.FillDataSet(ds, "FABRICANTE_MAQUINAS", sql, null);
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }

        }

        public static bool PuedeEliminarse(int codigo)
        {
            string sql = "SELECT count(MAQ_CODIGO) FROM MAQUINAS WHERE FAB_CODIGO = @p0";
            object[] valorParametros = { codigo };
            try
            {
                if (Convert.ToInt64(DB.executeScalar(sql, valorParametros, null)) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex) { throw new Entidades.Excepciones.BaseDeDatosException(); }
        }
    }
}
