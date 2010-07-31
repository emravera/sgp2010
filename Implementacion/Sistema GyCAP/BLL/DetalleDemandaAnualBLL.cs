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
        //metodo para traer el detalle de una cabecera
        public static int ObtenerTotal(int idDemanda)
        {
           return DAL.DetalleDemandaAnualDAL.ObtenerTotal(idDemanda);
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
        //Metodo para actualizar
        public static void Actualizar(Entidades.DetalleDemandaAnual detalle)
        {
            DAL.DetalleDemandaAnualDAL.Actualizar(detalle);
        }
        //Metodo para buscar el ID
        public static int ObtenerID(Entidades.DetalleDemandaAnual detalle)
        {
            return DAL.DetalleDemandaAnualDAL.ObtenerID(detalle);
        }

    }
}
