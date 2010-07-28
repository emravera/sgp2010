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
        //Metodo para obtener todo desde el formulario de Plan anual
        public static void ObtenerTodos(Data.dsPlanAnual ds)
        {
            DAL.DemandaAnualDAL.ObtenerTodos(ds);
        }
        //Metodo para obtener el año desde el formulario de Plan anual
        public static int ObtenerAño(int idDemanda)
        {
            return DAL.DemandaAnualDAL.ObtenerAño(idDemanda);
        }
        public static IList<Entidades.DetalleDemandaAnual> Insertar(Entidades.DemandaAnual demanda, IList<Entidades.DetalleDemandaAnual> detalle)
        {
            return DAL.DemandaAnualDAL.Insertar(demanda, detalle);
        }
        public static void Modificar(Entidades.DemandaAnual demanda, IList<Entidades.DetalleDemandaAnual> detalle)
        {
            DAL.DemandaAnualDAL.Actualizar(demanda, detalle);
        }
        public static bool ValidarModificacion(Entidades.DemandaAnual demanda)
        {
            return DAL.DemandaAnualDAL.Validar(demanda);
        }

        //Eliminacion
        public static void Eliminar(int codigo)
        {
          DAL.DemandaAnualDAL.Eliminar(codigo);
        }

        public static bool PuedeEliminarse(int codigo)
        {
            return DAL.DemandaAnualDAL.PuedeEliminarse(codigo);  

        }     


    }
}
