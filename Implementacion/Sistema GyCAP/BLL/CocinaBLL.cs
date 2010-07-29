using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class CocinaBLL
    {
        /// <summary>
        /// Obtiene todas las cocinas sin filtrar, las carga en un DataTable del tipo cocina.
        /// </summary>
        /// <param name="dtCocina">La tabla del tipo cocina donde cargar los datos.</param>
        public static void ObtenerCocinas(DataTable dtCocina)
        {
            DAL.CocinaDAL.ObtenerCocinas(dtCocina);
        }
    }
}
