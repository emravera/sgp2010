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
        public static object ObtenerTotalModelo(int codigoAnio, int codigoModelo)
        {
            //Funcion que obtiene la cantidad producida de un modelo 
            return DAL.StockMateriaPrimaDAL.ObtenerTotalModelo(codigoModelo, codigoAnio);
        }


    }
}
