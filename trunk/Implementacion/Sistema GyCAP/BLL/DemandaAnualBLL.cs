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
        public static void Modificar(Entidades.DemandaAnual demanda)
        {
            DAL.DemandaAnualDAL.Actualizar(demanda);
        }
        public static bool ValidarModificacion(Entidades.DemandaAnual demanda)
        {
            return DAL.DemandaAnualDAL.Validar(demanda);
        }

        //Eliminacion
        public static void Eliminar(int codigo)
        {
          //Puede eliminarse
          DAL.DemandaAnualDAL.Eliminar(codigo);
        }

        public static bool PuedeEliminarse(int codigo)
        {
            return DAL.DemandaAnualDAL.PuedeEliminarse(codigo);  

        }

        public static void EliminarDetalle(int codigo)
        {
            DAL.DemandaAnualDAL.EliminarDetalle(codigo);
        }


    }
}
