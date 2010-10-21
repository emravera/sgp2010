using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class StockMateriaPrimaBLL
    {
        public static int ObtenerTotalAnual(int codigoAnio)
        {
            //Funcion que trae todo lo que se debe producir en el año
            return DAL.StockMateriaPrimaDAL.ObtenerTotalAnual(codigoAnio);

        }



    }
}
