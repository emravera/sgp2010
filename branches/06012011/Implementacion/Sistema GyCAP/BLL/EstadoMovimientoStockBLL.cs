using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class EstadoMovimientoStockBLL
    {
        public static readonly int EstadoPlanificado = 1;
        public static readonly int EstadoFinalizado = 2;
        public static readonly int EstadoCancelado = 3;
        
        public static void ObtenerEstadosMovimiento(DataTable dtEstadosMovimientoStock)
        {
            DAL.EstadoMovimientoStockDAL.ObtenerEstados(dtEstadosMovimientoStock);
        }
    }
}
