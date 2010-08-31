using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class DiasPlanSemanalBLL
    {
        //METODOS DE BÚSQUEDA
        //Metodo que busca los dias de un plan semanal determinado
        public static void obtenerDias(DataTable dtDiasPS, int codigoPS)
        {
            DAL.DiasPlanSemanalDAL.ObtenerPS(dtDiasPS, codigoPS);
        }

    }
}
