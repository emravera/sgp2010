using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class ConfiguracionSistemaBLL
    {
        /// <summary>
        /// Obtiene el valor de una configuración dado su nombre.
        /// </summary>
        /// <param name="nombre">El nombre de la configuración.</param>
        /// <returns>El valor de la configuración.</returns>
        /// <exception cref="Excepciones.ConfiguracionInexistenteException">Si no existe la configuración.</exception>
        public static TResult GetConfiguracion<TResult>(string nombre) where TResult : struct
        {
            return DAL.ConfiguracionSistemaDAL.GetConfiguracion<TResult>(nombre);
        }

        /// <summary>
        /// Guarda una configuración, si ya existe se actualiza su valor.
        /// </summary>
        /// <param name="nombre">El nombre de la configuración.</param>
        /// <param name="valor">El valor de la configuración.</param>
        public static void SetConfiguracion(string nombre, string valor)
        {
            DAL.ConfiguracionSistemaDAL.SetConfiguracion(nombre, valor);
        }

        public static void ObtenerTodos(string nombre, string valor, DataTable dtParametros )
        {
            DAL.ConfiguracionSistemaDAL.ObtenerTodos(nombre, valor, dtParametros);
        }

    }
}
