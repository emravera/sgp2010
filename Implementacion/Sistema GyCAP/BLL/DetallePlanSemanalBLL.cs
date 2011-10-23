using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class DetallePlanSemanalBLL
    {
        public static readonly int estadoGenerado = 0;
        public static readonly int estadoModificado = 1;
        public static readonly int estadoConOrden = 2;  

        //metodo para traer el detalle de una cabecera
        public static void ObtenerDetalle(int idPlan, DataTable dtDetalle)
        {
            DAL.DetallePlanSemanalDAL.ObtenerDetalle(dtDetalle, idPlan);
        }

        public static void ActualizarEstado(int codigoDetalle, int codigoEstado)
        {
            DAL.DetallePlanSemanalDAL.ActualizarEstado(codigoDetalle, codigoEstado);
        }
    }
}
