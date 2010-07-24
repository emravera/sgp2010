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

        //Metodo para guardar el detalle
        public static int Insertar(Entidades.DetalleDemandaAnual detalle)
        {
            return DAL.DetalleDemandaAnualDAL.Insertar(detalle);
        }
        //Metodo para calcular estimacion
        public static int CantidadAñoMes(int año, string nombre, string mes)
        {
            return DAL.DetalleDemandaAnualDAL.ObtenerCantidadAñoMes(año,nombre,mes);
        }
    }
}
