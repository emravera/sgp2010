using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class DetalleDemandaAnualBLL
    {
        //metodo para traer el detalle de una cabecera
        public static void ObtenerDetalle(int idDemanda, Data.dsEstimarDemanda ds)
        {
            DAL.DetalleDemandaAnualDAL.ObtenerDetalle(idDemanda, ds);
        }

    }
}
