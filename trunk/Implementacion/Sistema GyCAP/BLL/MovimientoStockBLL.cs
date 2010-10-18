using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class MovimientoStockBLL
    {
        public static void Insertar(Entidades.MovimientoStock movimientoStock)
        {
            DAL.MovimientoStockDAL.Insertar(movimientoStock);
        }

        public static void Eliminar(int numeroMovimiento)
        {
            DAL.MovimientoStockDAL.Eliminar(numeroMovimiento);
        }
    }
}
