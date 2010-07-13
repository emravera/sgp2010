using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{

    public class UnidadMedidaBLL
    {
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerTodos(string nombre, Data.dsUnidadMedida ds)
        {
            DAL.UnidadMedidaDAL.ObtenerUnidad(nombre,ds);
        }
        public static void ObtenerTodos(int tipoUnidad, Data.dsUnidadMedida ds)
        {
            DAL.UnidadMedidaDAL.ObtenerUnidad(tipoUnidad, ds);
        }
        public static void ObtenerTodos(Data.dsUnidadMedida ds)
        {
            DAL.UnidadMedidaDAL.ObtenerUnidad(ds);
        }

    }
}
