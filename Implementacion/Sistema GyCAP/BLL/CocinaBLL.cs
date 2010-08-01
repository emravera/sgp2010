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

        public static void ObtenerCocinas(object codigo, object codMarca, object codTerminacion, object codEstado, DataTable dtCocina)
        {
            object marca = null, terminacion = null, estado = null;
            if (codMarca != null && Convert.ToInt32(codMarca.ToString()) > 0) { marca = codMarca; }
            if (codTerminacion != null && Convert.ToInt32(codTerminacion.ToString()) > 0) { terminacion = codTerminacion; }
            if (codEstado != null && Convert.ToInt32(codEstado.ToString()) > 0) { estado = codEstado; }
            DAL.CocinaDAL.ObtenerCocinas(codigo, marca, terminacion, estado, dtCocina);
        }
    }
}
