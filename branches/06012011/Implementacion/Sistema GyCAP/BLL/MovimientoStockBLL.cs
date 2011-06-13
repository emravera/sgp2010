using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class MovimientoStockBLL
    {
        public static readonly int EstadoPlanificado = 1;
        public static readonly int EstadoFinalizado = 2;
        public static readonly int EstadoCancelado = 3;
        
        public static void Insertar(Entidades.MovimientoStock movimientoStock)
        {
            DAL.MovimientoStockDAL.Insertar(movimientoStock, null);
        }

        public static void Eliminar(int numeroMovimiento)
        {
            DAL.MovimientoStockDAL.Eliminar(numeroMovimiento);
        }
    }
}
