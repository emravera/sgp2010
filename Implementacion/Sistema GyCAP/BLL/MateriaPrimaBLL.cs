using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class MateriaPrimaBLL
    {
        /// <summary>
        /// Obitne una materia prima por su código.
        /// </summary>
        /// <param name="codigoMateriaPrima">El código de la materia prima deseada.</param>
        /// <returns>El objeto materiaPrima con sus datos.</returns>
        /// <exception cref="ElementoInexistenteException">En caso de que no exista la materia prima.</exception>
        /// <exception cref="BaseDeDatosException">En caso de problemas con la base de datos.</exception>
        public static Entidades.MateriaPrima ObtenerMateriaPrima(int codigoMateriaPrima)
        {
            return DAL.MateriaPrimaDAL.ObtenerMateriaPrima(codigoMateriaPrima);
        }
    }
}
