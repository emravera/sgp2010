using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class DemandaAnualBLL
    {
        public static void ObtenerTodos(int anio, Data.dsEstimarDemanda ds)
        {
            DAL.DemandaAnualDAL.ObtenerTodos(anio, ds);
        }
        public static void ObtenerTodos(Data.dsEstimarDemanda ds)
        {
            DAL.DemandaAnualDAL.ObtenerTodos(ds);
        }
        public static int Insertar(Entidades.DemandaAnual demanda)
        {
            return DAL.DemandaAnualDAL.Insertar(demanda);
        }
        


    }
}
