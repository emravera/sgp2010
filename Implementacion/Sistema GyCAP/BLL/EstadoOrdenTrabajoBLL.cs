using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class EstadoOrdenTrabajoBLL
    {
        public static void ObtenerEstadosOrden(DataTable dtEstadosOrden)
        {
            DAL.EstadoOrdenTrabajoDAL.ObtenerEstados(dtEstadosOrden);
        }
    }
}
