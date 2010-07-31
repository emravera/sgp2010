using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class EstadoCocinaBLL
    {
        public static void ObtenerEstados(DataTable dtEstado)
        {
            DAL.EstadoCocinaDAL.ObtenerEstados(dtEstado);
        }
    }
}
