using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.DAL
{
    public class TerminacionDAL
    {
        public static int Insertar(Entidades.Terminacion terminacion)
        {
            //Agregamos select identity para que devuelva el código creado, en caso de necesitarlo
            string sql = "INSERT INTO TERMINACIONES (TE_NOMBRE, TE_DESCRIPCION) VALUES (@p0,@p1) SELECT @@Identity";
            object[] valorParametros = { terminacion.Nombre, terminacion.Descripcion };
            return Convert.ToInt32(DB.executeScalar(sql, valorParametros, null));
        }

        public static void Eliminar(Entidades.Terminacion terminacion)
        {
            string sql = "DELETE FROM TERMINACIONES WHERE TE_CODIGO = @p0";
            object[] valorParametros = { terminacion.Codigo };
            DB.executeNonQuery(sql, valorParametros, null);
        }

        public static void Actualizar(Entidades.Terminacion terminacion)
        {
            string sql = "UPDATE TERMINACIONES SET TE_NOMBRE = @p1, TE_DESCRIPCION = @p2 WHERE TE_CODIGO = @p0";
            object[] valorParametros = { terminacion.Codigo, terminacion.Nombre, terminacion.Descripcion };
            DB.executeNonQuery(sql, valorParametros, null);
        }

        public static bool EsTeminacion(Entidades.Terminacion terminacion)
        {
            string sql = "SELECT count(TE_CODIGO) FROM TERMINACIONES WHERE TE_NOMBRE = @p0";
            object[] valorParametros = { terminacion.Nombre };
            if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void ObtenerTerminacion(string nombre, Data.dsTerminacion ds)
        {
            if (nombre != String.Empty)
            {
                string sql = @"SELECT TE_CODIGO, TE_NOMBRE, TE_DESCRIPCION
                              FROM TERMINACIONES
                              WHERE TE_NOMBRE LIKE @p0";
                //Reacomodamos el valor porque hay problemas entre el uso del LIKE y parámetros
                nombre = "%" + nombre + "%";
                object[] valorParametros = { nombre };
                DB.FillDataSet(ds, "TERMINACIONES", sql, valorParametros);
            }
            else
            {
                string sql = "SELECT TE_CODIGO, TE_NOMBRE, TE_DESCRIPCION FROM TERMINACIONES";
                DB.FillDataSet(ds, "TERMINACIONES", sql, null);
            }
        }

        public static bool PuedeEliminarse(Entidades.Terminacion terminacion)
        {
            string sql = "SELECT count(PZA_CODIGO) FROM COCINA WHERE TE_CODIGO = @p0";
            object[] valorParametros = { terminacion.Codigo };
            if (Convert.ToInt32(DB.executeScalar(sql, valorParametros, null)) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
