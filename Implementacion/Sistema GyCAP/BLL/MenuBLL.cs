using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class MenuBLL
    {
        public static void ObtenerTodos(Data.dsSeguridad ds)
        {
            DAL.MenuDAL.ObtenerTodos(ds);
        }

        public static void ObtenerTodos(System.Data.DataTable dt)
        {
            DAL.MenuDAL.ObtenerTodos(dt);
        }

        public static void ObtenerTodos(int usuario, Data.dsSeguridad ds)
        {
            DAL.MenuDAL.ObtenerTodos(usuario, ds);
        }
    }
}
