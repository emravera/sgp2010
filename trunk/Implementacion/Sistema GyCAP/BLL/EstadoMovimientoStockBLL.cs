using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class EstadoMovimientoStockBLL
    {
        public static void ObtenerEstadosMovimiento(DataTable dtEstadosMovimientoStock)
        {
            DAL.EstadoMovimientoStockDAL.ObtenerEstados(dtEstadosMovimientoStock);
        }
    }
}
