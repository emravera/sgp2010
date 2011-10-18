using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using GyCAP.Entidades;
using GyCAP.Entidades.ArbolEstructura;
using GyCAP.Entidades.ArbolOrdenesTrabajo;
using GyCAP.Entidades.Enumeraciones;
using GyCAP.Entidades.Excepciones;

namespace GyCAP.DAL
{
    /// <summary>
    /// Clase temporal utilizada para todas las tareas necesarias para la presentación.
    /// </summary>
    public class PresentacionDAL
    {
        private static int NumeroParteCocina = 26;
        

        /// <summary>
        /// Asigna la HR creada a la parte correspondiente a la cocina creada durante la presentación.
        /// </summary>
        public static void AsignarHojaRutaAParteCocina(int codigoHR)
        {
            string sql = "UPDATE PARTES set hr_codigo = @p0 WHERE part_numero = @p1";
            object[] parametros = { codigoHR, NumeroParteCocina };

            try
            {
                DB.executeNonQuery(sql, parametros, null);
            }
            catch (SqlException ex) { throw new BaseDeDatosException(ex.Message); }
        }
    }
}
