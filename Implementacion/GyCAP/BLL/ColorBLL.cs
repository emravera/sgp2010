using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class ColorBLL
    {
        public static void guardar(String nombre, String descripcion)
        {
            DAL.ColorDAL.guardar(nombre, descripcion);
        }

        public static void eliminar()
        {
            
        }

        public static void actualizar()
        {
        }

        public static Boolean esColor(String nombre)
        {
            return true;
        }

        public static DataSet ObtenerTodos()
        {
            DataSet ds = new DataSet();
            return ds;
        }
    }
}
