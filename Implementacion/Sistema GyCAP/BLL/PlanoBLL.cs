using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class PlanoBLL
    {
        public static void ObtenerTodos(System.Data.DataTable dtPlano)
        {
            DAL.PlanoDAL.ObtenerTodos(dtPlano);
        }
    }
}
