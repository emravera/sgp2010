using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class MarcaBLL
    {
        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerTodos(string nombre, Data.dsMarca ds)
        {
            DAL.MarcaDAL.ObtenerMarca(nombre, ds);
        }
        public static void ObtenerTodos(int idCliente, Data.dsMarca ds)
        {
            DAL.MarcaDAL.ObtenerMarca(idCliente, ds);
        }
        public static void ObtenerTodos(Data.dsMarca ds)
        {
            DAL.MarcaDAL.ObtenerMarca(ds);
        }



    }
}
