using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.BLL
{
    public class EstadoParteBLL
    {
        public static void ObtenerTodos(System.Data.DataTable dtEstados)
        {
            DAL.EstadoParteDAL.ObtenerTodos(dtEstados);
        }
    }
}
