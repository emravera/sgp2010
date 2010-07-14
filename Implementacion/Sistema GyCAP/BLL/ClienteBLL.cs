using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class ClienteBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda (se debe sobrecargar)
        public static void ObtenerTodos(Data.dsMarca ds)
        {
            DAL.ClienteDAL.ObtenerUnidad(ds);
        }



    }
}
